using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pinewood.Customers.Core.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pinewood.Customers.Helper;
public class JwtUtils : IJwtUtils
{
    private readonly AppSettings appSettings;

    public JwtUtils(IOptions<AppSettings> appSettings )
    {
        this.appSettings= appSettings.Value;
    }
    public string? GenerateJwtToken(ApplicationUser user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(appSettings.SigningKey);       
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience= appSettings.Audience,
            Issuer= appSettings.Issuer,
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(appSettings.ExpirationDays),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<string?> ValidateJwtToken(string? token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(appSettings.SigningKey);
        try
        {
            
            var tokenValidationResult=await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero,
                
            });

            var jwtToken = (JwtSecurityToken)tokenValidationResult.SecurityToken;           
            
            var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
            
            return userId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }
}