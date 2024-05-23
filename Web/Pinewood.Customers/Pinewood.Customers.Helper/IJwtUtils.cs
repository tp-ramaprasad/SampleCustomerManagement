using Pinewood.Customers.Core.Entities;

namespace Pinewood.Customers.Helper;
public interface IJwtUtils
{
    string GenerateJwtToken(ApplicationUser user);
    Task<string> ValidateJwtToken(string? token);
}
