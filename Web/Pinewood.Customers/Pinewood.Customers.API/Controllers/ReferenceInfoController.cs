using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pinewood.Customers.API.Authorization;
using Pinewood.Customers.API.Filters;
using Pinewood.Customers.API.Models.Common;
using Pinewood.Customers.API.Models.Response;
using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Services.Interfaces;
using System.Reflection;

namespace Pinewood.Customers.API.Controllers;

[CustomAuthorize]
[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(ExceptionFilter))]
public class ReferenceInfoController : ControllerBase
{
    private readonly ILogger<ReferenceInfoController> logger;
    private readonly IReferenceInfoService referenceInfoService;
    private readonly IMapper mapper;   

    public ReferenceInfoController(ILogger<ReferenceInfoController> logger,IReferenceInfoService referenceInfoService, IMapper mapper)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));        
        this.referenceInfoService = referenceInfoService ?? throw new ArgumentNullException(nameof(referenceInfoService));
        this.mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
    }
    
    /// <summary>
    /// Get the list of countries, genders and contact preferences
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetReferenceInfo")]
    public async Task<IActionResult> GetReferenceInfo()
    {
      
        logger.LogDebug(message: $"{DateTime.Now}: Entering the method {MethodBase.GetCurrentMethod().Name} for retrieving the Reference Information");

        var referenceModel = new GetReferenceInfoModel
        {
            Genders = mapper.Map<IEnumerable<Gender>, IEnumerable<GenderModel>>(await referenceInfoService.GetGenderInfo().ConfigureAwait(false)),

            Countries = mapper.Map<IEnumerable<Country>, IEnumerable<CountryModel>>(await referenceInfoService.GetCountries().ConfigureAwait(false)),

            Preferences = referenceInfoService.GetContactPreference()
        };

        if (referenceModel.Preferences == null || referenceModel.Genders==null || referenceModel.Countries==null)
        {
            return NotFound();
        }

        logger.LogDebug(message: $"{DateTime.Now}: finished executing the method {MethodBase.GetCurrentMethod().Name}");

        return Ok(referenceModel);
    }
}
