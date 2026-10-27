using Microsoft.Data.Sqlite;

// Всё про базу: открыть подключение и создать таблицы при первом запуске
public static class Db
{
    private const string ConnectionString = "Data Source=livingcorner.db";

    public static SqliteConnection Open()
    {
        var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        return conn;
    }

    public static void Init()
    {
        using var db = Open();
        // Dapper.Execute умеет выполнять и несколько команд сразу
        Dapper.SqlMapper.Execute(db, @"
            CREATE TABLE IF NOT EXISTS Tiers   (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Price INTEGER NOT NULL);
            CREATE TABLE IF NOT EXISTS Coffins (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Price INTEGER NOT NULL);
            CREATE TABLE IF NOT EXISTS Priests (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Price INTEGER NOT NULL);
            CREATE TABLE IF NOT EXISTS Morgues (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL);

            CREATE TABLE IF NOT EXISTS Users (
                Id           INTEGER PRIMARY KEY AUTOINCREMENT,
                Login        TEXT NOT NULL COLLATE NOCASE,
                PasswordHash TEXT NOT NULL,
                FirstName    TEXT NOT NULL,
                LastName     TEXT NOT NULL,
                Email        TEXT NULL,
                RegisteredAt TEXT NOT NULL
            );
            CREATE UNIQUE INDEX IF NOT EXISTS IX_Users_Login ON Users(Login);

            CREATE TABLE IF NOT EXISTS Persons (
                Id         INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId     INTEGER NULL REFERENCES Users(Id),
                Name       TEXT NOT NULL,
                BirthDate  TEXT NOT NULL,
                Status     TEXT NOT NULL DEFAULT 'Alive',
                Balance    INTEGER NOT NULL DEFAULT 0,
                DeathDate  TEXT NULL,
                TierId     INTEGER NULL REFERENCES Tiers(Id),
                MorgueId   INTEGER NULL REFERENCES Morgues(Id),
                BuriedAt   TEXT NULL
            );

            CREATE TABLE IF NOT EXISTS Orders (
                Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                PersonId  INTEGER NOT NULL REFERENCES Persons(Id),
                CoffinId  INTEGER NOT NULL REFERENCES Coffins(Id),
                TierId    INTEGER NOT NULL REFERENCES Tiers(Id),
                PriestId  INTEGER NOT NULL REFERENCES Priests(Id),
                Status    TEXT NOT NULL DEFAULT 'Pending',
                CreatedAt TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Games (
                Id       INTEGER PRIMARY KEY AUTOINCREMENT,
                PersonId INTEGER NOT NULL REFERENCES Persons(Id),
                Bombs    TEXT NOT NULL,
                Dug      TEXT NOT NULL DEFAULT '',
                Status   TEXT NOT NULL DEFAULT 'Active'
            );
        ");

        // Начальные данные (только если таблицы пустые) - цены потом можно менять как хочешь
        if (Dapper.SqlMapper.ExecuteScalar<int>(db, "SELECT COUNT(*) FROM Tiers") == 0)
        {
            Dapper.SqlMapper.Execute(db, @"
                INSERT INTO Tiers (Id, Name, Price) VALUES (1,'Бесплатный',0), (2,'Обычный',500), (3,'Премиум',2000);
                INSERT INTO Coffins (Name, Price) VALUES ('Сосновый',100), ('Дубовый',400), ('Золотой',1500);
                INSERT INTO Priests (Name, Price) VALUES ('Отец Фёдор',50), ('Отец Иоанн',200), ('Архиепископ',1000);
                INSERT INTO Morgues (Name) VALUES ('Морг №1'), ('Морг №2'), ('Морг №3');
            ");
        }
    }
}
