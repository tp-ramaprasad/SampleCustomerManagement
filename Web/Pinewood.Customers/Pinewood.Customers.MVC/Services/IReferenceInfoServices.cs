using Pinewood.Customers.MVC.Models;

namespace Pinewood.Customers.MVC.Services
{
    public interface IReferenceInfoServices
    {
        Task<GetReferenceInfoModel> GetReferenceInformation(string endpoint, string accessToken);
    }
}