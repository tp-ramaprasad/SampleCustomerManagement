using Pinewood.Customers.MVC.Common;
using Pinewood.Customers.MVC.Models;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Pinewood.Customers.MVC.Services
{

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly HttpClient client;

        public UserService(ILogger<UserService> logger, HttpClient client)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<UserModel> AuthicateUser(string endpoint, AuthenticationRequest authRequest)
        {
            logger.LogDebug($"{DateTime.Now}: Started executing the method:{MethodBase.GetCurrentMethod().Name} to update the customer");

            var dataTobePosted = JsonSerializer.Serialize(authRequest, JsonExtenstions.GetJsonSerializerOptions());

            var data = new StringContent(dataTobePosted, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"));

            //call api end points
            var response = await client.PostAsync(endpoint, data);

            logger.LogDebug($"{DateTime.Now}: {MethodBase.GetCurrentMethod().Name} : API call response is {response.IsSuccessStatusCode}");

            return await response.ReadContentAsync<UserModel>();
        }
    }
}