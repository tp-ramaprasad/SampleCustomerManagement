using Microsoft.AspNetCore.Mvc;
using Pinewood.Customers.MVC.Common;
using Pinewood.Customers.MVC.Models;
using Pinewood.Customers.MVC.Services;
using System.Net;
using System.Reflection;

namespace Pinewood.Customers.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> logger;
        private readonly IConfiguration configuration;
        private readonly ICustomerServices customerServices;
        private readonly IReferenceInfoServices referenceInfoServices;
        private readonly int pageSize = 0;
        private readonly string? accessToken = string.Empty;
        public CustomerController(ILogger<CustomerController> logger, IConfiguration configuration, 
            ICustomerServices customerServices,IReferenceInfoServices referenceInfoServices)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.customerServices = customerServices ?? throw new ArgumentNullException(nameof(customerServices));
            this.referenceInfoServices = referenceInfoServices ?? throw new ArgumentNullException(nameof(referenceInfoServices));
            pageSize = Convert.ToInt32(configuration["PageSize"]);            
        }

        public async Task<IActionResult> Index(string? searchString, int? pageNumber, string? currentFilter)
        {
            logger.LogDebug(message: $"{DateTime.Now}: Entering the action {MethodBase.GetCurrentMethod().Name} for retrieving the Customer List");

            if(!string.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;
            var accessToken = GetAccessToken();

            var customers = string.IsNullOrEmpty(searchString) ? await customerServices.GetAllCustomers(configuration["GetAllCustomerEndpoint"], accessToken) 
                : await customerServices.SearchCustomerByName(configuration["SearchCustomerByNameEndpoint"],searchString, accessToken);

            logger.LogDebug(message: $"{DateTime.Now}: Existing the action {MethodBase.GetCurrentMethod().Name} and the record count is {customers.Count()}");

            return View(PaginatedList<GetCustomerModel>.Create(customers, pageNumber ?? 1, pageSize));
        }


        [HttpGet]
        public async Task<IActionResult> AddCustomer()
        {
            var customerModel = new AddCustomerModel
            {
                ReferenceInfoData = await GetDefaultDropDownData()
            };

            return View(customerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(AddCustomerModel addCustomerRequestModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    addCustomerRequestModel.GenderId = (addCustomerRequestModel.GenderId >0 ? addCustomerRequestModel.GenderId.Value : null);

                    var addCustomerResponse = await customerServices.AddCustomer(configuration["CreateCustomerEndpoint"], addCustomerRequestModel, GetAccessToken());

                    if (addCustomerResponse.StatusDescription == CustomerOperationStatus.Success)
                    {
                        TempData["SuccessMessage"] = Constants.CUSTOMER_CREATED_SUCCESSFULLY;
                    }
                }
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                logger.LogError(ex, "");
                TempData["ErrorMessage"] = Constants.CUSTOMER_ALREADY_EXISTS;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                TempData["ErrorMessage"] = Constants.CUSTOMER_CREATION_FAILED;
            }

            return View(new AddCustomerModel { ReferenceInfoData = await GetDefaultDropDownData() });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            logger.LogDebug(message: $"{DateTime.Now}: Entering the action {MethodBase.GetCurrentMethod().Name} for deleting the customer id {id}");

            var response = await customerServices.DeleteCustomer(configuration["DeleteCustomerEndpoint"], id, GetAccessToken());

            string message;
            if (response.StatusDescription == CustomerOperationStatus.Success)
            {
                message = $"{Constants.CUSTOMER_DELETED_SUCCESSFULLY}-{id}";
            }
            else
            {
                message = $"{Constants.CUSTOMER_DELETION_FAILED}-{id}";

            }
            logger.LogDebug(message: $"{DateTime.Now}: {message}");

            return Json(new { status = response.StatusDescription, msg = message });
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerDetails(int id)
        {
            var response = await customerServices.GetCustomerById(configuration["GetCustomerByIdEndpoint"], id, GetAccessToken());

            if (response == null)
            {
                return NotFound();
            }
            response.ReferenceInfoData = await GetDefaultDropDownData();
            return View(response);
        }

        [HttpGet("EditCustomer")]
        public async Task<IActionResult> EditCustomer(int id,int pageNumber =1)
        {
            ViewBag.pageNumber = pageNumber;
            var response = await customerServices.GetEditCustomerDetails(configuration["EditCustomerEndpoint"],id, GetAccessToken());

            if (response == null)
            {
                return NotFound();
            }
            response.ReferenceInfoData = await GetDefaultDropDownData();
            return View(response);
        }

        [HttpPost("EditCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(UpdateCustomerModel updateCustomerRequestModel,int pageNumber=1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await customerServices.UpdateCustomer(configuration["UpdateCustomerEndpoint"], updateCustomerRequestModel, GetAccessToken());
                                       
                    if (response.StatusDescription == CustomerOperationStatus.Success)
                    {
                        return RedirectToAction(nameof(Index),pageNumber);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Failed to update the customer with the id {updateCustomerRequestModel.Id}";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                TempData["ErrorMessage"] = $"Failed to update the customer with the id {updateCustomerRequestModel.Id}";
            }            

            //error case handing
            updateCustomerRequestModel.ReferenceInfoData = await GetDefaultDropDownData();

            return View(updateCustomerRequestModel);
        }


        [NonAction]
        private async Task<GetReferenceInfoModel> GetDefaultDropDownData()
        {
            return await referenceInfoServices.GetReferenceInformation(configuration["GetReferenceInfoEndpoint"], GetAccessToken());
        }

        [NonAction]
        private string? GetAccessToken()
        {
            var accessToken = TempData["AccessToken"]?.ToString();
            TempData.Keep("AccessToken");
            return accessToken;
        }
    }
}