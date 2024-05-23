namespace Pinewood.Customers.API.Models.AuthModels;
public class AuthenticationResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool LockoutEnabled { get; set; }
    public Role Role { get; set; }
    public string Token { get; set; }
}
