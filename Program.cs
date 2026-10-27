using Dapper;
using Microsoft.Data.Sqlite;

// ===== Настройки игры (меняй под себя) =====
const int GridSize = 9;          // поле 3x3
const int BombCount = 2;         // две ямы с псом патроном
const int PitReward = 10;        // валюта за каждую яму
const int WinPrize = 90;         // приз за все 9 ям, если откопал все 7 безопасных
const int ResurrectPrice = 100;  // воскрешение после взрыва
const int FreeTierId = 1;        // нищий уровень

Db.Init();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Разрешаем фронту ходить к API с любого адреса
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();   // страница для тестов: /swagger

static IResult Err(string message) => Results.BadRequest(new { error = message });

static List<int> ParseCells(string s) =>
    s.Length == 0 ? new List<int>() : s.Split(',').Select(int.Parse).ToList();

// Превращает человека в антиоживленного: дата смерти = сегодня, назначаем морг и уровень
static void MakeDead(Microsoft.Data.Sqlite.SqliteConnection db, int personId, int tierId)
{
    db.Execute(@"UPDATE Persons SET Status='Dead', DeathDate=@d, TierId=@tierId,
                 MorgueId=(SELECT Id FROM Morgues ORDER BY RANDOM() LIMIT 1)
                 WHERE Id=@personId",
        new { d = DateTime.Today.ToString("yyyy-MM-dd"), tierId, personId });
}

// ===== 0. Акаунти: реєстрація та вхід =====
// Хеш для неіснуючого логіна: щоб час відповіді не видавав, чи є такий користувач
var dummyHash = PasswordHasher.Hash("dummy-password");

app.MapPost("/api/auth/register", (UserRegisterRequest req) =>
{
    var errors = AuthValidator.ValidateRegistration(req);
    if (errors.Count > 0) return Results.BadRequest(new { error = errors[0], errors });

    var login = req.Login.Trim();
    using var db = Db.Open();

    if (db.ExecuteScalar<int>("SELECT COUNT(*) FROM Users WHERE Login=@login", new { login }) > 0)
        return Results.Conflict(new { error = "Користувач з таким логіном вже існує" });

    try
    {
        var id = db.ExecuteScalar<int>(@"
            INSERT INTO Users (Login, PasswordHash, FirstName, LastName, Email, RegisteredAt)
            VALUES (@login, @hash, @first, @last, @email, @now);
            SELECT last_insert_rowid();",
            new
            {
                login,
                hash = PasswordHasher.Hash(req.Password),
                first = req.FirstName.Trim(),
                last = req.LastName.Trim(),
                email = string.IsNullOrWhiteSpace(req.Email) ? null : req.Email.Trim(),
                now = DateTime.Now.ToString("s")
            });
        return Results.Ok(new { id, message = "Реєстрація успішна. Тепер увійдіть." });
    }
    catch (SqliteException ex) when (ex.SqliteErrorCode == 19) // порушення UNIQUE: хтось встиг раніше
    {
        return Results.Conflict(new { error = "Користувач з таким логіном вже існує" });
    }
});

app.MapPost("/api/auth/login", (LoginRequest req) =>
{
    const string bad = "Невірний логін або пароль";   // одне загальне повідомлення для всіх помилок
    if (string.IsNullOrWhiteSpace(req.Login) || string.IsNullOrEmpty(req.Password))
        return Results.Json(new { error = bad }, statusCode: 401);

    using var db = Db.Open();
    var user = db.QuerySingleOrDefault<User>("SELECT * FROM Users WHERE Login=@login",
        new { login = req.Login.Trim() });

    var ok = PasswordHasher.Verify(req.Password, user?.PasswordHash ?? dummyHash) && user is not null;
    if (!ok) return Results.Json(new { error = bad }, statusCode: 401);

    var dto = new UserDto(user!.Id, user.Login, user.FirstName, user.LastName, user.Email, user.RegisteredAt);
    return Results.Ok(dto);
});

// Персонажі акаунта (щоб після входу фронт знав, кого завантажувати)
app.MapGet("/api/users/{id:int}/persons", (int id) =>
{
    using var db = Db.Open();
    return Results.Ok(db.Query<Person>("SELECT * FROM Persons WHERE UserId=@id", new { id }));
});

// ===== 1. Регистрация персонажа: имя + день + месяц + год =====
app.MapPost("/api/register", (RegisterRequest req) =>
{
    if (string.IsNullOrWhiteSpace(req.Name)) return Err("Укажите имя");

    DateOnly birth;
    try { birth = new DateOnly(req.Year, req.Month, req.Day); }
    catch { return Err("Такой даты не существует"); }
    if (birth > DateOnly.FromDateTime(DateTime.Today)) return Err("Дата рождения в будущем");

    using var db = Db.Open();
    if (req.UserId is not null &&
        db.ExecuteScalar<int>("SELECT COUNT(*) FROM Users WHERE Id=@UserId", new { req.UserId }) == 0)
        return Err("Такого акаунта не існує");

    var id = db.ExecuteScalar<int>(
        @"INSERT INTO Persons (UserId, Name, BirthDate) VALUES (@UserId, @Name, @Birth); SELECT last_insert_rowid();",
        new { req.UserId, Name = req.Name.Trim(), Birth = birth.ToString("yyyy-MM-dd") });
    return Results.Ok(db.QuerySingle<Person>("SELECT * FROM Persons WHERE Id=@id", new { id }));
});

app.MapGet("/api/persons/{id:int}", (int id) =>
{
    using var db = Db.Open();
    var p = db.QuerySingleOrDefault<Person>("SELECT * FROM Persons WHERE Id=@id", new { id });
    return p is null ? Results.NotFound() : Results.Ok(p);
});

// ===== 2. Кнопка [Мертвые]: имя, дата рождения, дата смерти, уровень =====
app.MapGet("/api/dead", () =>
{
    using var db = Db.Open();
    return Results.Ok(db.Query<DeadRow>(@"
        SELECT p.Name, p.BirthDate, p.DeathDate, t.Name AS Tier
        FROM Persons p JOIN Tiers t ON t.Id = p.TierId
        WHERE p.Status='Dead' ORDER BY p.DeathDate DESC"));
});

// ===== 3. Кнопка [В ожидании]: мёртвые, которых ещё не похоронили =====
app.MapGet("/api/waiting", () =>
{
    using var db = Db.Open();
    return Results.Ok(db.Query<WaitingRow>(@"
        SELECT p.Id AS PersonId, m.Name AS Morgue, p.Name, p.DeathDate, t.Name AS Tier
        FROM Persons p
        JOIN Tiers t ON t.Id = p.TierId
        JOIN Morgues m ON m.Id = p.MorgueId
        WHERE p.Status='Dead' AND p.BuriedAt IS NULL
        ORDER BY p.DeathDate"));
});

// Похоронить (убирает человека из "В ожидании"; вызывает основная логика Кента)
app.MapPost("/api/waiting/{personId:int}/bury", (int personId) =>
{
    using var db = Db.Open();
    var n = db.Execute(
        "UPDATE Persons SET BuriedAt=@d WHERE Id=@personId AND Status='Dead' AND BuriedAt IS NULL",
        new { d = DateTime.Today.ToString("yyyy-MM-dd"), personId });
    if (n == 0) return Err("Такого человека нет в ожидании");
    // если у него был заказ - помечаем выполненным
    db.Execute("UPDATE Orders SET Status='Done' WHERE PersonId=@personId AND Status='Pending'", new { personId });
    return Results.Ok(new { buried = true });
});

// ===== 4. Кнопка [Заказы] =====
app.MapGet("/api/orders", () =>
{
    using var db = Db.Open();
    return Results.Ok(db.Query<OrderRow>(@"
        SELECT o.Id, p.Name, p.BirthDate, c.Name AS Coffin, t.Name AS Tier, pr.Name AS Priest, o.Status
        FROM Orders o
        JOIN Persons p  ON p.Id  = o.PersonId
        JOIN Coffins c  ON c.Id  = o.CoffinId
        JOIN Tiers t    ON t.Id  = o.TierId
        JOIN Priests pr ON pr.Id = o.PriestId
        ORDER BY o.Id DESC"));
});

// ===== 5. Кнопка [Магазин]: что можно купить заранее =====
app.MapGet("/api/shop", () =>
{
    using var db = Db.Open();
    return Results.Ok(new
    {
        coffins = db.Query<Item>("SELECT * FROM Coffins"),
        tiers   = db.Query<Item>("SELECT * FROM Tiers"),
        priests = db.Query<Item>("SELECT * FROM Priests")
    });
});

app.MapPost("/api/shop/buy", (BuyRequest req) =>
{
    using var db = Db.Open();
    var person = db.QuerySingleOrDefault<Person>("SELECT * FROM Persons WHERE Id=@PersonId", req);
    if (person is null) return Results.NotFound();
    if (person.Status != "Alive") return Err("Заказывать может только живой");

    var coffin = db.QuerySingleOrDefault<Item>("SELECT * FROM Coffins WHERE Id=@CoffinId", req);
    var tier   = db.QuerySingleOrDefault<Item>("SELECT * FROM Tiers WHERE Id=@TierId", req);
    var priest = db.QuerySingleOrDefault<Item>("SELECT * FROM Priests WHERE Id=@PriestId", req);
    if (coffin is null || tier is null || priest is null) return Err("Неверный гроб, уровень или священник");

    var total = coffin.Price + tier.Price + priest.Price;
    if (person.Balance < total) return Err($"Не хватает валюты: нужно {total}, у вас {person.Balance}");

    using var tx = db.BeginTransaction();
    db.Execute("UPDATE Persons SET Balance = Balance - @total WHERE Id=@PersonId",
        new { total, req.PersonId }, tx);
    db.Execute(@"INSERT INTO Orders (PersonId, CoffinId, TierId, PriestId, CreatedAt)
                 VALUES (@PersonId, @CoffinId, @TierId, @PriestId, @now)",
        new { req.PersonId, req.CoffinId, req.TierId, req.PriestId, now = DateTime.Now.ToString("s") }, tx);
    tx.Commit();
    return Results.Ok(new { spent = total, balance = person.Balance - total });
});

// ===== 6. Игра: откапывание гробов (3x3, в двух ямах песпатрон-бомба) =====
app.MapPost("/api/game/start", (StartGameRequest req) =>
{
    using var db = Db.Open();
    var person = db.QuerySingleOrDefault<Person>("SELECT * FROM Persons WHERE Id=@PersonId", req);
    if (person is null) return Results.NotFound();
    if (person.Status != "Alive") return Err("Играть может только живой");

    // закрываем старую незавершённую игру, если была
    db.Execute("UPDATE Games SET Status='Cashed' WHERE PersonId=@PersonId AND Status='Active'", req);

    var bombs = Enumerable.Range(0, GridSize).OrderBy(_ => Random.Shared.Next()).Take(BombCount).OrderBy(x => x);
    var gameId = db.ExecuteScalar<int>(
        "INSERT INTO Games (PersonId, Bombs) VALUES (@PersonId, @bombs); SELECT last_insert_rowid();",
        new { req.PersonId, bombs = string.Join(",", bombs) });
    // где псы - фронту НЕ отдаём, иначе будет читерство
    return Results.Ok(new { gameId, gridSize = GridSize, bombCount = BombCount });
});

app.MapPost("/api/game/{gameId:int}/dig", (int gameId, DigRequest req) =>
{
    using var db = Db.Open();
    var g = db.QuerySingleOrDefault<Game>("SELECT * FROM Games WHERE Id=@gameId", new { gameId });
    if (g is null) return Results.NotFound();
    if (g.Status != "Active") return Err("Игра уже закончена");
    if (req.Cell < 0 || req.Cell >= GridSize) return Err("Клетка от 0 до 8");

    var bombs = ParseCells(g.Bombs);
    var dug = ParseCells(g.Dug);
    if (dug.Contains(req.Cell)) return Err("Эта яма уже выкопана");

    // попали на пса
    if (bombs.Contains(req.Cell))
    {
        db.Execute("UPDATE Games SET Status='Exploded' WHERE Id=@gameId", new { gameId });
        db.Execute("UPDATE Persons SET Status='Exploded' WHERE Id=@PersonId", new { g.PersonId });
        return Results.Ok(new { exploded = true, bombs, dug,
            message = "БА-БАХ! Теперь выбирай: воскрешение, платная смерть или бесплатная смерть" });
    }

    dug.Add(req.Cell);
    var safeCells = GridSize - BombCount;

    // выкопал все безопасные (7 из 9) - игра заканчивается, приз как за все 9 ям
    if (dug.Count == safeCells)
    {
        db.Execute("UPDATE Games SET Status='Won', Dug=@d WHERE Id=@gameId",
            new { d = string.Join(",", dug), gameId });
        db.Execute("UPDATE Persons SET Balance = Balance + @WinPrize WHERE Id=@PersonId",
            new { WinPrize, g.PersonId });
        return Results.Ok(new { exploded = false, finished = true, reward = WinPrize, bombs, dug });
    }

    db.Execute("UPDATE Games SET Dug=@d WHERE Id=@gameId", new { d = string.Join(",", dug), gameId });
    return Results.Ok(new { exploded = false, finished = false, dug, currentReward = dug.Count * PitReward });
});

// Кнопка [Выход]: забрать валюту за выкопанные ямы
app.MapPost("/api/game/{gameId:int}/cashout", (int gameId) =>
{
    using var db = Db.Open();
    var g = db.QuerySingleOrDefault<Game>("SELECT * FROM Games WHERE Id=@gameId", new { gameId });
    if (g is null) return Results.NotFound();
    if (g.Status != "Active") return Err("Игра уже закончена");

    var reward = ParseCells(g.Dug).Count * PitReward;
    db.Execute("UPDATE Games SET Status='Cashed' WHERE Id=@gameId", new { gameId });
    db.Execute("UPDATE Persons SET Balance = Balance + @reward WHERE Id=@PersonId", new { reward, g.PersonId });
    return Results.Ok(new { reward });
});

// ===== 7. После взрыва: [Воскрешение] / [Смерть] / бесплатная смерть =====
app.MapPost("/api/persons/{id:int}/resurrect", (int id) =>
{
    using var db = Db.Open();
    var p = db.QuerySingleOrDefault<Person>("SELECT * FROM Persons WHERE Id=@id", new { id });
    if (p is null) return Results.NotFound();
    if (p.Status != "Exploded") return Err("Воскрешать нужно только взорвавшегося");
    if (p.Balance < ResurrectPrice) return Err($"Не хватает валюты: нужно {ResurrectPrice}");

    db.Execute("UPDATE Persons SET Status='Alive', Balance = Balance - @ResurrectPrice WHERE Id=@id",
        new { ResurrectPrice, id });
    return Results.Ok(new { balance = p.Balance - ResurrectPrice });
});

// Платная смерть: выбираешь уровень 2 или 3 и платишь его цену
app.MapPost("/api/persons/{id:int}/die", (int id, DieRequest req) =>
{
    using var db = Db.Open();
    var p = db.QuerySingleOrDefault<Person>("SELECT * FROM Persons WHERE Id=@id", new { id });
    if (p is null) return Results.NotFound();
    if (p.Status != "Exploded") return Err("Умереть можно только после взрыва");

    var tier = db.QuerySingleOrDefault<Item>("SELECT * FROM Tiers WHERE Id=@TierId", req);
    if (tier is null) return Err("Нет такого уровня");
    if (p.Balance < tier.Price) return Err($"Не хватает валюты: нужно {tier.Price}");

    db.Execute("UPDATE Persons SET Balance = Balance - @Price WHERE Id=@id", new { tier.Price, id });
    MakeDead(db, id, tier.Id);
    return Results.Ok(new { dead = true, tier = tier.Name });
});

// Не можешь / не хочешь платить: бесплатная (нищая) смерть
app.MapPost("/api/persons/{id:int}/abandon", (int id) =>
{
    using var db = Db.Open();
    var p = db.QuerySingleOrDefault<Person>("SELECT * FROM Persons WHERE Id=@id", new { id });
    if (p is null) return Results.NotFound();
    if (p.Status != "Exploded") return Err("Умереть можно только после взрыва");

    MakeDead(db, id, FreeTierId);
    return Results.Ok(new { dead = true, tier = "Бесплатный" });
});

app.Run();
