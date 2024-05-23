using Pinewood.Customers.MVC.Models;
using Pinewood.Customers.Services.Models;

namespace Pinewood.Customers.MVC.Services
{
    public interface ICustomerServices
    {
        Task<OperationsResponseModel> AddCustomer(string endpoint, AddCustomerModel addCustomer, string accessToken);
        Task<OperationsResponseModel> DeleteCustomer(string endpoint, int customerId, string accessToken);
        Task<List<GetCustomerModel>> GetAllCustomers(string endpoint, string accessToken);
        Task<GetCustomerModel> GetCustomerById(string endpoint, int customerId, string accessToken);
        Task<OperationsResponseModel> UpdateCustomer(string endpoint, UpdateCustomerModel updateCustomerViewModel, string accessToken);
        Task<UpdateCustomerModel> GetEditCustomerDetails(string endpoint, int id, string accessToken);
        Task<List<GetCustomerModel>> SearchCustomerByName(string endpoint, string searchString, string accessToken);
    }
}