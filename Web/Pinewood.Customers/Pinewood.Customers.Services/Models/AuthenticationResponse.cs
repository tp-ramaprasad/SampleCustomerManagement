namespace Pinewood.Customers.Services.Models;

using Pinewood.Customers.Core.Entities;

public class AuthenticationResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    //public string LastName { get; }
    //public string FirstName { get; }
    public bool LockoutEnabled { get; }
    public bool EmailConfirmed { get; }
    public string Email { get; set; }    
    public Role Role { get; set; }
    public string Token { get; set; }

    public AuthenticationResponse(ApplicationUser user, string token)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        LockoutEnabled = user.LockoutEnabled;
        EmailConfirmed = user.EmailConfirmed;        
        Role = user.Role;
        Token = token;
    }
}