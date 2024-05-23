using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pinewood.Customers.API.Authorization;
using Pinewood.Customers.API.Filters;
using Pinewood.Customers.API.Models.Request;
using Pinewood.Customers.API.Models.Response;
using Pinewood.Customers.Core.Entities;
using Pinewood.Customers.Services.Enums;
using Pinewood.Customers.Services.Interfaces;
using System.Reflection;

namespace Pinewood.Customers.API.Controllers;


[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(ExceptionFilter))]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> logger;
    public readonly ICustomerService customerService;
    private readonly IMapper mapper;
    private string currentMethod = string.Empty;

    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService, IMapper mapper)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        this.mapper = mapper ?? throw new ArgumentNullException($"{nameof(mapper)}");
    }

    /// <summary>
    /// Get the list of customers
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAllCustomers")]
    [CustomAuthorize(Role.Admin, Role.User)]
    public async Task<IActionResult> GetAllCustomers()
    {
        currentMethod = MethodBase.GetCurrentMethod().Name;

        logger.LogDebug(message: $"{DateTime.Now}: Entering the method {currentMethod} for retrieving the Customer List");

        var customerDetailsList = mapper.Map<IEnumerable<Customer>, IEnumerable<GetCustomerByIdModel>>(await customerService.GetAllCustomers().ConfigureAwait(false));

        if (customerDetailsList == null)
        {
            return NotFound();
        }

        logger.LogDebug(message: $"{DateTime.Now}: finished executing the method {currentMethod} and the customer count is {customerDetailsList.Count()}");

        return Ok(customerDetailsList);
    }

    /// <summary>
    /// Get Customer by id
    /// </summary>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet("GetCustomerById/{customerId}")]
    [CustomAuthorize(Role.Admin, Role.User)]
    public async Task<IActionResult> GetCustomerById(int customerId)
    {
        currentMethod = MethodBase.GetCurrentMethod().Name;

        logger.LogDebug(message: $"{DateTime.Now}: Entering the method {currentMethod} for retrieving the details for the Id : {customerId} ");

        var customerDetails = mapper.Map<GetCustomerByIdModel>(await customerService.GetCustomerById(customerId).ConfigureAwait(false));

        if (customerDetails != null)
        {
            logger.LogDebug(message: $"{DateTime.Now}: Exiting the method {currentMethod}: Successfully retrieved the details for the Id : {customerId}");
            return Ok(customerDetails);
        }
        else
        {
            logger.LogError(message: $"{DateTime.Now}: Exiting the method {currentMethod} : Failed to retrieve the details for the Id : {customerId}");
            return BadRequest();
        }
    }

    /// <summary>
    /// Add a new customer
    /// </summary>
    /// <param name="customerDetails"></param>
    /// <returns></returns>    
    [HttpPost("AddCustomer")]
    [CustomAuthorize(Role.Admin)]
    public async Task<IActionResult> AddCustomer(AddCustomerModel addCustomerRequest)
    {
        currentMethod = MethodBase.GetCurrentMethod().Name;

        logger.LogDebug(message: $"{DateTime.Now}: Entering the method {currentMethod} for adding a new Customer");

        var addCustomer = mapper.Map<Customer>(addCustomerRequest);
        var response = await customerService.AddCustomer(addCustomer).ConfigureAwait(false);

        if (response.Status == CustomerOperationStatus.Success)
        {
            logger.LogDebug(message: $"{DateTime.Now}: Exiting the method {currentMethod}:Successfully added a new customer and new Id is {response.Id}");
            return Ok(response);
        }
        else if (response.Status == CustomerOperationStatus.Duplicate)
        {
            logger.LogError(message: $"{DateTime.Now}: Exiting the method {currentMethod} : Customer with the email {addCustomer.Contact.EmailAddress} is already present");

            return Conflict($"Customer with the email {addCustomer.Contact.EmailAddress} is already present");
        }
        else //failure sc
        {
            logger.LogError(message: $"{DateTime.Now}: Exiting the method {currentMethod} : Failed to add the new customer details");
            return BadRequest("Failed to add the new customer details");
        }
    }

    /// <summary>
    /// Update the customer
    /// </summary>
    /// <param name="updateCustomerRequest"></param>
    /// <returns></returns>
    [HttpPut("UpdateCustomer")]
    [CustomAuthorize(Role.Admin)]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerModel updateCustomerRequest)
    {
        currentMethod = MethodBase.GetCurrentMethod().Name;

        logger.LogDebug(message: $"{DateTime.Now}: Entering the method {currentMethod} for updating the customer detaisl for {updateCustomerRequest.Id}");

        if (updateCustomerRequest != null)
        {
            var updateCustomer = mapper.Map<Customer>(updateCustomerRequest);

            var response = await customerService.UpdateCustomer(updateCustomer).ConfigureAwait(false);

            if (response.Status == CustomerOperationStatus.Success)
            {
                logger.LogDebug(message: $"{DateTime.Now}: Exiting the method {currentMethod}:Successfully updated the customer detaisl for {updateCustomerRequest.Id}");
                return Ok(response);
            }
            else if (response.Status == CustomerOperationStatus.Duplicate)
            {
                logger.LogError(message: $"{DateTime.Now}: Exiting the method {currentMethod} : Customer with the email {updateCustomerRequest.Contact.EmailAddress} is already present");

                return Conflict($"Customer with the email {updateCustomerRequest.Contact.EmailAddress} is already present");
            }
        }

        logger.LogDebug(message: $"{DateTime.Now}: Exiting the method {currentMethod} : Failed to update the customer details for {updateCustomerRequest.Id}");

        return BadRequest();
    }

    /// <summary>
    /// Delete customer by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("DeleteCustomer/{customerId}")]
    [CustomAuthorize(Role.Admin)]
    public async Task<IActionResult> DeleteCustomer(int customerId)
    {
        currentMethod = MethodBase.GetCurrentMethod().Name;

        logger.LogDebug(message: $"{DateTime.Now}: Entering the method {currentMethod} for deleting the customer with Id {customerId}");

        var response = await customerService.DeleteCustomer(customerId).ConfigureAwait(false);

        if (response.Status == CustomerOperationStatus.Success)
        {
            logger.LogDebug(message: $"{DateTime.Now}: Exiting the method {currentMethod}:Successfully deleted the customer with id {customerId}");
            return Ok(response);
        }
        else
        {
            logger.LogError(message: $"{DateTime.Now}: Exiting the method {currentMethod}:Failed to  delete the customer with id {customerId}");
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Serach customer by name
    /// </summary>
    /// <param name="searchString"></param>
    /// <returns></returns>
    [HttpGet("SearchCustomerByName/{searchString}")]
    [CustomAuthorize(Role.Admin,Role.User)]
    public async Task<IActionResult> SearchCustomerByName(string searchString)
    {
        currentMethod = MethodBase.GetCurrentMethod().Name;

        logger.LogDebug(message: $"{DateTime.Now}: Entering the method {currentMethod} for retrieving the details for  : {searchString} ");

        var customerDetailsList = mapper.Map<IEnumerable<Customer>, IEnumerable<GetCustomerByIdModel>>(await customerService.SearchCustomerByName(searchString).ConfigureAwait(false));

        if (customerDetailsList == null)
        {
            return NotFound();
        }

        logger.LogDebug(message: $"{DateTime.Now}: finished executing the method {currentMethod} and the customer count is {customerDetailsList.Count()}");

        return Ok(customerDetailsList);
    }

}
