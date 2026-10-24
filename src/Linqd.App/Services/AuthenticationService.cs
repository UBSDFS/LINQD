using System.Security.Cryptography;
using Linqd.App.Data;
using Linqd.App.Models;

namespace Linqd.App.Services;

public class AuthenticationService
{
    private readonly UserRepository _userRepository;

    public AuthenticationService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // Register a user in SQLite.
    public async Task<bool> RegisterUserAsync(User user, string password)
    {
        // Prevent duplicate usernames.
        var existingUser = await _userRepository
            .GetUserByUsernameAsync(user.Username);

        if (existingUser != null ||
            user.Username.Equals("testuser", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        // Hash the password before storing it.
        user.PasswordHash = HashPassword(password);

        await _userRepository.AddUserAsync(user);

        return true;
    }

    // Authenticate using SQLite or the temporary test account.
    public async Task<User?> LoginAsync(string username, string password)
    {


        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null)
            return null;

        return VerifyPassword(password, user.PasswordHash) ? user : null;
    }

    // PBKDF2 with a unique salt for each password.
    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            210_000,
            HashAlgorithmName.SHA256,
            32);

        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(string password, string storedHash)
    {
        string[] parts = storedHash.Split(':');

        if (parts.Length != 2)
            return false;

        try
        {
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] expectedHash = Convert.FromBase64String(parts[1]);

            byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                210_000,
                HashAlgorithmName.SHA256,
                32);

            return CryptographicOperations.FixedTimeEquals(
                actualHash,
                expectedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}