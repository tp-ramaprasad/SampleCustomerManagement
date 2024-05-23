using Microsoft.AspNetCore.Identity;
using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Core.Interfaces;
using Pinewood.Customers.Helper;
using Pinewood.Customers.Services.Interfaces;
using Pinewood.Customers.Services.Models;

namespace Pinewood.Customers.Services;
public class UserService : IUserService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IJwtUtils jwtUtils;

    public UserService(IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
    {
        this.unitOfWork = unitOfWork;
        this.jwtUtils = jwtUtils;
    }

    /// <summary>
    /// two steps followed here - Validate the user and then create a token
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<AuthenticationResponse> Authenticate(string email, string password)
    {
        var users = await unitOfWork.Users.GetAll().ConfigureAwait(false);

        var user = users.FirstOrDefault(x => x.Email == email);
        // validate
        if (user == null ||  new PasswordHasher<ApplicationUser>().VerifyHashedPassword(user,user.PasswordHash, password)==PasswordVerificationResult.Failed)
            throw new AppException("Username or password is incorrect");


        // authentication successful so generate jwt token
        var jwtToken = jwtUtils.GenerateJwtToken(user);

        return new AuthenticationResponse(user, jwtToken);
    }

    public async Task<string> ValidateJwtToken(string? token)
    {
        return await jwtUtils.ValidateJwtToken(token).ConfigureAwait(false);
    }

    public async Task<ApplicationUser> GetById(string? id)
    {
        var allUsers = await unitOfWork.Users.GetAll().ConfigureAwait(false); 
        var user = allUsers.FirstOrDefault(x => x.Id == id);

        return user ?? throw new KeyNotFoundException("User not found");
    }
}