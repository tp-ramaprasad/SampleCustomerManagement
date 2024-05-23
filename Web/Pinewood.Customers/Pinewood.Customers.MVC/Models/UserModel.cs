namespace Pinewood.Customers.MVC.Models;
public class UserModel
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool LockoutEnabled { get; set; }
    public Role Role { get; set; }
    public string Token { get; set; }
}