using SQLite;
namespace Linqd.App.Models;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int UserId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
}
