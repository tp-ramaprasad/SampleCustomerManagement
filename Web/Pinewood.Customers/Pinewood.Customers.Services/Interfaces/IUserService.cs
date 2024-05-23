using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Services.Models;

namespace Pinewood.Customers.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationResponse> Authenticate(string userName, string password);
        Task<ApplicationUser> GetById(string? id);
        Task<string> ValidateJwtToken(string? token);
    }
}