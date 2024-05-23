using Microsoft.AspNetCore.Identity;

namespace Pinewood.Customers.Core.Entities;

public class ApplicationUser:IdentityUser
{
    public Role Role { get; set; }  
}
