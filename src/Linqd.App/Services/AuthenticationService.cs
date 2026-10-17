namespace Linqd.App.Services;

public class AuthenticationService
{
    private const string TestUsername = "testuser";
    private const string TestPassword = "password1";

    public bool ValidateLogin(string username, string password)
    {
        return username == TestUsername && password == TestPassword;
    }
}
