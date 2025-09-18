namespace ContosoUniv.Lib;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public static bool CheckPassword(string password, string stored)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, stored);

    }
}