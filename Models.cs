// ===== Модели: то, что лежит в БД и то, что отдаём фронту =====

// Игрок / клиент службы
public class Person
{
    public int Id { get; set; }
    public int? UserId { get; set; }              // чий це персонаж (акаунт)
    public string Name { get; set; } = "";
    public string BirthDate { get; set; } = "";   // "2008-05-17"
    public string Status { get; set; } = "";      // Alive / Exploded / Dead
    public int Balance { get; set; }              // валюта
    public string? DeathDate { get; set; }
    public int? TierId { get; set; }
    public int? MorgueId { get; set; }
    public string? BuriedAt { get; set; }         // null = ещё в ожидании
}

// Игра "раскопки" (поле 3x3)
public class Game
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string Bombs { get; set; } = "";       // "2,6" - клетки с псом
    public string Dug { get; set; } = "";         // "0,1,4" - уже найденные
    public string Status { get; set; } = "";      // Active / Exploded / Cashed / Won
}

// Строки для списков на кнопках
public class DeadRow
{
    public string Name { get; set; } = "";
    public string BirthDate { get; set; } = "";
    public string DeathDate { get; set; } = "";
    public string Tier { get; set; } = "";
}

public class WaitingRow
{
    public int PersonId { get; set; }
    public string Morgue { get; set; } = "";
    public string Name { get; set; } = "";
    public string DeathDate { get; set; } = "";
    public string Tier { get; set; } = "";
}

public class OrderRow
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string BirthDate { get; set; } = "";
    public string Coffin { get; set; } = "";
    public string Tier { get; set; } = "";
    public string Priest { get; set; } = "";
    public string Status { get; set; } = "";
}

// Товар магазина (гроб / уровень / священник - у всех одинаковая форма)
public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Price { get; set; }
}

// Запросы от фронта
public record RegisterRequest(string Name, int Day, int Month, int Year, int? UserId = null);
public record StartGameRequest(int PersonId);
public record DigRequest(int Cell);          // 0..8
public record DieRequest(int TierId);        // 2 или 3 (платная смерть)
public record BuyRequest(int PersonId, int CoffinId, int TierId, int PriestId);
