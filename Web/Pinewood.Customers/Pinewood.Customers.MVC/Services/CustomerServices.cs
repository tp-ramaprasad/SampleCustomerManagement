using AutoMapper;
using Pinewood.Customers.MVC.Common;
using Pinewood.Customers.MVC.Models;
using Pinewood.Customers.Services.Models;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Pinewood.Customers.MVC.Services;

public class CustomerServices :ICustomerServices
{
    private readonly ILogger<CustomerServices> logger;
    private readonly HttpClient client;
    private readonly IMapper mapper;

    public CustomerServices(ILogger<CustomerServices> logger, HttpClient client,IMapper mapper)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.client = client ?? throw new ArgumentNullException(nameof(client));
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<GetCustomerModel>> GetAllCustomers(string endpoint,string accessToken)
    {
        logger.LogDebug($"{DateTime.Now}: Started executing the method:{MethodBase.GetCurrentMethod().Name} for retrieving the customers");

        client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.GetAsync(endpoint);

        logger.LogDebug($"{DateTime.Now}: {MethodBase.GetCurrentMethod().Name} : API call response is {response.IsSuccessStatusCode}");

        return await response.ReadContentAsync<List<GetCustomerModel>>();
    }

    public async Task<GetCustomerModel> GetCustomerById(string endpoint,int id,string accessToken)
    {
        logger.LogDebug($"{DateTime.Now}: Started executing the method:{MethodBase.GetCurrentMethod().Name} for retrieving the customer details");
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.GetAsync($"{endpoint}/{id}");

        logger.LogDebug($"{DateTime.Now}: {MethodBase.GetCurrentMethod().Name} : API call response is {response.IsSuccessStatusCode}");

        return await response.ReadContentAsync<GetCustomerModel>();
    }

    public async Task<OperationsResponseModel> AddCustomer(string endpoint, AddCustomerModel addCustomerViewModel,string accessToken)
    {
        logger.LogDebug($"{DateTime.Now}: Started executing the method:{MethodBase.GetCurrentMethod().Name}  for adding a new customer");

        var dataTobePosted = JsonSerializer.Serialize(addCustomerViewModel, JsonExtenstions.GetJsonSerializerOptions());

        var data = new StringContent(dataTobePosted, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        //post data to API
        var response = await client.PostAsync(endpoint, data);

        logger.LogDebug($"{DateTime.Now}: {MethodBase.GetCurrentMethod().Name} : API call response is {response.IsSuccessStatusCode}");

        return await response.ReadContentAsync<OperationsResponseModel>();
    }

    public async Task<OperationsResponseModel> UpdateCustomer(string endpoint, UpdateCustomerModel updateCustomerViewModel, string accessToken)
    {
        logger.LogDebug($"{DateTime.Now}: Started executing the method:{MethodBase.GetCurrentMethod().Name} to update the customer");

        var dataTobePosted = JsonSerializer.Serialize(updateCustomerViewModel, JsonExtenstions.GetJsonSerializerOptions());

        var data = new StringContent(dataTobePosted, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //call api end points
        var response = await client.PutAsync(endpoint, data);

        logger.LogDebug($"{DateTime.Now}: {MethodBase.GetCurrentMethod().Name} : API call response is {response.IsSuccessStatusCode}");

        return await response.ReadContentAsync<OperationsResponseModel>();
    }

    public async Task<OperationsResponseModel> DeleteCustomer(string endpoint, int customerId, string accessToken)
    {
        logger.LogDebug(message: $"{DateTime.Now}: Started executing the method:{MethodBase.GetCurrentMethod().Name}");

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //post data to API for deletion
        var response = await client.DeleteAsync($"{endpoint}/{customerId}");

        logger.LogDebug($"{DateTime.Now}: Delete customer API call response is {response.IsSuccessStatusCode}");

        return await response.ReadContentAsync<OperationsResponseModel>();
    }

    public async Task<UpdateCustomerModel> GetEditCustomerDetails(string endpoint, int id,string accessToken)
    {
        logger.LogDebug($"{DateTime.Now}: Started executing the method:{MethodBase.GetCurrentMethod().Name} for retrieving the customer details");
        
        var response = await GetCustomerById(endpoint, id,accessToken);

        logger.LogDebug($"{DateTime.Now}: {MethodBase.GetCurrentMethod().Name} : Customer id is  {response.Id}");

        return mapper.Map<UpdateCustomerModel>(response);
    }

    public async Task<List<GetCustomerModel>> SearchCustomerByName(string endpoint, string searchString, string accessToken)
    {
        logger.LogDebug($"{DateTime.Now}: Started executing the method:{MethodBase.GetCurrentMethod().Name} for retrieving the customers");

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await client.GetAsync($"{endpoint}/{searchString}");

        logger.LogDebug($"{DateTime.Now}: {MethodBase.GetCurrentMethod().Name} : API call response is {response.IsSuccessStatusCode}");

        return await response.ReadContentAsync<List<GetCustomerModel>>();
    }
}

