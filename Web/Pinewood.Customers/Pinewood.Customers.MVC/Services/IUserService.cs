using Pinewood.Customers.MVC.Models;

namespace Pinewood.Customers.MVC.Services
{
    public interface IUserService
    {
        Task<UserModel> AuthicateUser(string endpoint, AuthenticationRequest authRequest);
    }
}