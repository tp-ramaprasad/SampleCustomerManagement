using Pinewood.Customers.Core.Entities;

namespace Pinewood.Customers.Services.Interfaces
{
    public interface IReferenceInfoService
    {
        Task<IEnumerable<Gender>> GetGenderInfo();
        IEnumerable<string> GetContactPreference();
        Task<IEnumerable<Country>> GetCountries();
    }
}