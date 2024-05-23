using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Pinewood.Customers.MVC.Models;
using Pinewood.Customers.MVC.Services;
using System.Security.Claims;

namespace Pinewood.Customers.MVC.Controllers;

[AutoValidateAntiforgeryToken]
public class UsersController : Controller
{
    private readonly ILogger<CustomerController> logger;
    private readonly IConfiguration configuration;
    private readonly IUserService userService;

    public UsersController(ILogger<CustomerController> logger, IConfiguration configuration, IUserService userService)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(AuthenticationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        try
        {
            var user = await userService.AuthicateUser(configuration["AuthticationUserEndpoint"], request);

            if (user != null && !string.IsNullOrEmpty(user.Token))
            {
                TempData["AccessToken"] = user.Token;
                return Redirect("/Customer/index");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "");
            ModelState.AddModelError(string.Empty, "Invalid email or password has been supplied");
        }
        return View(request);

    }

    public IActionResult LogOut()
    {
        TempData.Clear();
        return Redirect("/Users/Login");
    }
}
