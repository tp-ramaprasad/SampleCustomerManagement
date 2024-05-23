using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Services.Models;

namespace Pinewood.Customers.Services.Interfaces;

public interface ICustomerService
{
    Task<ServiceResponse> AddCustomer(Customer customerDetails);
    Task<IEnumerable<Customer>> GetAllCustomers();
    Task<Customer?> GetCustomerById(int customerId);
    Task<ServiceResponse> UpdateCustomer(Customer customerDetails);
    Task<ServiceResponse> DeleteCustomer(int customerId);
    Task<IEnumerable<Customer>> SearchCustomerByName(string searchString);
}


