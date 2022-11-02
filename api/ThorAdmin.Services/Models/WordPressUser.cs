namespace ThorAdmin.Services.Models;

public class WordPressUser
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public DateTime Registered { get; set; }

    public string DisplayName { get; set; }

    public string Nickname { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}
