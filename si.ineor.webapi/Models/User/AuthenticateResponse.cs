namespace si.ineor.webapi.Models.Users;

using si.ineor.webapi.Entities;

public class AuthenticateResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }

    public Role Role { get; set; }
    public string Token { get; set; }

    public AuthenticateResponse(User user, string token)
    {
        Id = user.Id;
        Email = user.Email;
        Username = user.Username;
        Role = user.Role;
        Token = token;
    }
}