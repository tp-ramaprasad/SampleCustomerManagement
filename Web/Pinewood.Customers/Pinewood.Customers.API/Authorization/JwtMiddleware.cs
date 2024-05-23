namespace Pinewood.Customers.API.Authorization;

using Pinewood.Customers.Services.Interfaces;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = await userService.ValidateJwtToken(token).ConfigureAwait(false);
        if (userId != null)
        {
            // attach user to context on successful jwt validation
            var user= await userService.GetById(userId).ConfigureAwait(false);
            context.Items["User"] = user;
        }

        await _next(context);
    }
}