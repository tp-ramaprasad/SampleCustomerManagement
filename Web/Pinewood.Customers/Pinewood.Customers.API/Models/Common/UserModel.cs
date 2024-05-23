namespace Pinewood.Customers.API.Models.Common;
//public class UserModel
//{
//    public int Id { get; set; }
//    public string FirstName { get; set; }
//    public string LastName { get; set; }
//    public string Email { get; set; }
//    public string Username { get; set; }
//    public bool EmailConfirmed { get; set; }
//    public bool LockoutEnabled { get; set; }
//    public Role Role { get; set; }
//    public string Token { get; set; }
//}

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