using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pinewood.Customers.API.Authorization;
using Pinewood.Customers.API.Filters;
using Pinewood.Customers.API.Models.AuthModels;
using Pinewood.Customers.API.Models.Common;
using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Services.Interfaces;
using System.Reflection;

namespace Pinewood.Customers.API.Controllers;

[CustomAuthorize]
[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ExceptionFilter))]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> logger;
    private readonly IUserService userService;
    private readonly IMapper mapper;

    public UsersController(ILogger<UsersController> logger, IUserService userService, IMapper mapper)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        this.mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    public async Task<IActionResult> Authenticate(AuthenticationRequest model)
    {
        logger.LogDebug(message: $"{DateTime.Now}: Entering the method {MethodBase.GetCurrentMethod().Name} for Authenticating the User");

        var response = await userService.Authenticate(model.Email,model.Password).ConfigureAwait(false);
        var authResponse = mapper.Map<Services.Models.AuthenticationResponse, AuthenticationResponse>(response);
        logger.LogDebug(message: $"{DateTime.Now}: finished executing the method {MethodBase.GetCurrentMethod().Name} and auth Token created :  {!string.IsNullOrEmpty(authResponse.Token)}");

        return Ok(authResponse);
    }

    
    [HttpGet]
    public async Task<IActionResult> GetById(string sid)
    {
        var user =  await userService.GetById(sid).ConfigureAwait(false);
        var authResponse = mapper.Map<ApplicationUser, UserModel>(user);
        if (authResponse == null)
            return BadRequest("User not found");
        return Ok(authResponse);
    }
}