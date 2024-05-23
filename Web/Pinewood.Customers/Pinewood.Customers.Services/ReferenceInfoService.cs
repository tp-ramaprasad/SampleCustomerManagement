using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Core.Interfaces;
using Pinewood.Customers.Services.Enums;
using Pinewood.Customers.Services.Interfaces;

namespace Pinewood.Customers.Services;

public class ReferenceInfoService : IReferenceInfoService
{
    private readonly IUnitOfWork unitOfWork;

    public ReferenceInfoService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region "Public methods"

    /// <summary>
    /// Retrieve the all the gender Details
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Gender>> GetGenderInfo()
    {
        var genderList = await unitOfWork.Genders.GetAll().ConfigureAwait(false);
        return genderList;
    }

    /// <summary>
    /// Retrieve the all the countryies Details
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Country>> GetCountries()
    {
        var genderList = await unitOfWork.Countries.GetAll().ConfigureAwait(false);
        return genderList;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public  IEnumerable<string> GetContactPreference()
    {
        return Enum.GetNames(typeof(ContactPreference));
    }
    #endregion
}


