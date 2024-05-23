using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Core.Interfaces;
using Pinewood.Customers.Services.Enums;
using Pinewood.Customers.Services.Interfaces;
using Pinewood.Customers.Services.Models;

namespace Pinewood.Customers.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    #region "Public methods"

    /// <summary>
    /// Retrieve the all the customer Details
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Customer>> GetAllCustomers()
    {
        var customerDetailsList = await unitOfWork.Customers.GetAll().ConfigureAwait(false);
        return customerDetailsList.OrderBy(x => x.FirstName);
    }

    /// <summary>
    /// Retrieve the customer Details for a specific id
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns>Task<Customer?></returns>
    public async Task<Customer?> GetCustomerById(int customerId)
    {
        if (customerId > 0)
        {
            var customerDetails = await unitOfWork.Customers.GetById(customerId).ConfigureAwait(false);
            if (customerDetails != null)
            {
                return customerDetails;
            }
        }
        return null;
    }

    /// <summary>
    /// save changes and return the newly created customer Id back
    /// </summary>
    /// <param name="addCustomer"></param>
    /// <returns>Task<ServiceResponse></returns>
    public async Task<ServiceResponse> AddCustomer(Customer addCustomer)
    {
        var response = GetDefaultFailureResponse(0);

        if (addCustomer != null)
        {
            if (!await ExistsAsync(addCustomer).ConfigureAwait(false))
            {
                addCustomer.CreatedDateTime = addCustomer.LastUpdatedDateTime = DateTime.Now;
                await unitOfWork.Customers.Add(addCustomer);
                var result = await unitOfWork.SaveAsync().ConfigureAwait(false);
                if (result > 0)
                {
                    SetSuccessResponse(addCustomer.Id, response);
                }
            }
            else
            {
                SetConflictResponse(response);
            }
        }

        return response;
    }

    /// <summary>
    /// update customer details
    /// </summary>
    /// <param name="updateCustomer"></param>
    /// <returns></returns>
    public async Task<ServiceResponse> UpdateCustomer(Customer updateCustomer)
    {
        var response = GetDefaultFailureResponse(updateCustomer.Id);

        if (updateCustomer != null)
        {
            var customer = await unitOfWork.Customers.GetById(updateCustomer.Id).ConfigureAwait(false);
            if (customer != null)
            {
                //check same email id is used by other user or not if both the email id's are different
                if (customer.Contact.EmailAddress.ToUpper() != updateCustomer.Contact.EmailAddress.ToUpper())
                {
                    if (await ExistsAsync(updateCustomer).ConfigureAwait(false))
                    {
                        SetConflictResponse(response);

                        return response;
                    }
                }

                MapViewModelsToDTOs(updateCustomer, customer);
                unitOfWork.Customers.Update(customer);
                var result = await unitOfWork.SaveAsync().ConfigureAwait(false);
                if (result > 0)
                {
                    SetSuccessResponse(updateCustomer.Id, response);
                }
            }
        }

        return response;
    }

    /// <summary>
    /// Delete the details from db for the customerId passed
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns>Task<bool></returns>
    public async Task<ServiceResponse> DeleteCustomer(int customerId)
    {
        var response = GetDefaultFailureResponse(customerId);

        if (customerId > 0)
        {
            var customerDetails = await unitOfWork.Customers.GetById(customerId).ConfigureAwait(false);
            if (customerDetails != null)
            {
                unitOfWork.Customers.Delete(customerDetails);
                var result = await unitOfWork.SaveAsync().ConfigureAwait(false);

                if (result > 0)
                {
                    SetSuccessResponse(customerId, response);
                }
            }
        }
        return response;
    }

    /// <summary>
    /// Retrieve the customer Details for a specific name
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns>Task<Customer?></returns>
    public async Task<IEnumerable<Customer>> SearchCustomerByName(string searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            var customerDetails = await unitOfWork.Customers.GetAll().ConfigureAwait(false);
            return customerDetails
                .Where(e => e.FirstName.ToLower().Contains(searchString.ToLower()) || e.LastName.ToLower().Contains(searchString.ToLower()))
                .OrderBy(x => x.FirstName).ToList();

        }
        return await GetAllCustomers().ConfigureAwait(false);
    }

    #endregion

    #region "Private methods"

    /// <summary>
    /// Checks a customer with the email id is already exists or not
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    private async Task<bool> ExistsAsync(Customer customer)
    {
        bool isExists = false;
        if (customer != null)
        {
            isExists = await unitOfWork.Customers.ExistsAsync(c => c.Contact.EmailAddress.ToLower()==customer.Contact.EmailAddress.ToLower()).ConfigureAwait(false);
        }

        return isExists;
    }


    /// <summary>
    /// map view models to domain entities
    /// </summary>
    /// <param name="updateCustomer"></param>
    /// <param name="customer"></param>
    private static void MapViewModelsToDTOs(Customer updateCustomer, Customer customer)
    {
        customer.FirstName = updateCustomer.FirstName;
        customer.LastName = updateCustomer.LastName;
        customer.DateOfBirth = updateCustomer.DateOfBirth;
        if (updateCustomer.Gender != null)
            customer.GenderId = updateCustomer.Gender.Id > 0 ? updateCustomer.Gender.Id : null;
        if (updateCustomer.Contact != null)
        {
            customer.Contact.PhoneNumber = updateCustomer.Contact.PhoneNumber;
            customer.Contact.EmailAddress = updateCustomer.Contact.EmailAddress;
        }

        if (updateCustomer.Preference != null)
        {
            customer.Preference.ContactPreference = updateCustomer.Preference.ContactPreference;
            customer.Preference.Brand = updateCustomer.Preference.Brand;
            customer.Preference.VehicleType = updateCustomer.Preference.VehicleType;
        }

        customer.LastUpdatedDateTime = DateTime.Now;
        if (updateCustomer.Address != null)
        {
            customer.Address.AddressLine1 = updateCustomer.Address.AddressLine1;
            customer.Address.AddressLine2 = updateCustomer.Address.AddressLine2;
            customer.Address.City = updateCustomer.Address.City;
            customer.Address.State = updateCustomer.Address.State;
            if (updateCustomer.Address.CountryId!= customer.Address.CountryId)
            {
                if (updateCustomer.Address.CountryId.HasValue)
                    customer.Address.CountryId = updateCustomer.Address.CountryId.Value;
                else
                    customer.Address.CountryId = null;
            }
            customer.Address.ZipCode = updateCustomer.Address.ZipCode;
        }
    }

    /// <summary>
    /// set the response object to success
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="response"></param>
    private static void SetSuccessResponse(int customerId, ServiceResponse response)
    {
        response.Id = customerId;
        response.Status = CustomerOperationStatus.Success;
        response.StatusDescription = Enum.GetName(CustomerOperationStatus.Success);
    }

    /// <summary>
    /// Set Failure response
    /// </summary>
    /// <param name="customerId"></param>   
    /// <returns></returns>
    private static ServiceResponse GetDefaultFailureResponse(int customerId)
    {
        return new ServiceResponse
        {
            Id = customerId,
            Status = CustomerOperationStatus.Failure,
            StatusDescription = Enum.GetName(CustomerOperationStatus.Failure)
        };
    }

    /// <summary>
    /// set conclict response
    /// </summary>
    /// <param name="response"></param>
    private static void SetConflictResponse(ServiceResponse response)
    {
        response.Status = CustomerOperationStatus.Duplicate;
        response.StatusDescription = Enum.GetName(CustomerOperationStatus.Duplicate);
    }

    #endregion
}