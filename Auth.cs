using System.Net.Mail;
using System.Security.Cryptography;

// ===== Безпечне зберігання паролів: PBKDF2 + випадкова сіль =====
// У БД лежить тільки рядок "PBKDF2$ітерації$сіль$хеш". Сам пароль не зберігається ніде.
public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 210_000;

    public static string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
        return $"PBKDF2${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    public static bool Verify(string password, string stored)
    {
        var parts = stored.Split('$');
        if (parts.Length != 4 || parts[0] != "PBKDF2") return false;

        var iterations = int.Parse(parts[1]);
        var salt = Convert.FromBase64String(parts[2]);
        var expected = Convert.FromBase64String(parts[3]);
        var actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, expected.Length);
        // порівняння за постійний час, щоб не можна було підбирати хеш за часом відповіді
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}

// ===== Моделі користувачів =====
public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string? Email { get; set; }
    public string RegisteredAt { get; set; } = "";
}

// Те, що віддаємо фронту (без хеша пароля!)
public record UserDto(int Id, string Login, string FirstName, string LastName, string? Email, string RegisteredAt);

public record UserRegisterRequest(
    string Login, string Password, string ConfirmPassword,
    string FirstName, string LastName, string? Email = null);

public record LoginRequest(string Login, string Password);

// ===== Валідація форми реєстрації =====
public static class AuthValidator
{
    public static List<string> ValidateRegistration(UserRegisterRequest r)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(r.Login)) errors.Add("Вкажіть логін");
        else if (r.Login.Trim().Length < 3) errors.Add("Логін має містити щонайменше 3 символи");
        else if (r.Login.Trim().Length > 50) errors.Add("Логін занадто довгий (максимум 50 символів)");

        if (string.IsNullOrEmpty(r.Password)) errors.Add("Вкажіть пароль");
        else if (r.Password.Length < 6) errors.Add("Пароль має містити щонайменше 6 символів");
        else if (r.Password.Length > 100) errors.Add("Пароль занадто довгий (максимум 100 символів)");

        if (string.IsNullOrEmpty(r.ConfirmPassword)) errors.Add("Підтвердіть пароль");
        else if (r.Password != r.ConfirmPassword) errors.Add("Паролі не збігаються");

        if (string.IsNullOrWhiteSpace(r.FirstName)) errors.Add("Вкажіть ім'я");
        if (string.IsNullOrWhiteSpace(r.LastName)) errors.Add("Вкажіть прізвище");

        // email необов'язковий, але якщо вказаний - має бути коректним
        if (!string.IsNullOrWhiteSpace(r.Email) && !IsValidEmail(r.Email.Trim()))
            errors.Add("Некоректний формат email");

        return errors;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email && email.Contains('.', StringComparison.Ordinal)
                   && email.IndexOf('@') < email.LastIndexOf('.');
        }
        catch { return false; }
    }
}
