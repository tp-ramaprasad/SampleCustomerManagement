using Pinewood.Customers.MVC.Common;
using Pinewood.Customers.MVC.Models;
using System.Net.Http.Headers;
using System.Reflection;

namespace Pinewood.Customers.MVC.Services
{
    /// <summary>
    /// Service class for retrieving the rerfence details like gender, preference etc.
    /// </summary>
    public class ReferenceInfoServices : IReferenceInfoServices
    {
        private readonly ILogger<ReferenceInfoServices> logger;
        private readonly HttpClient client;       

        public ReferenceInfoServices(ILogger<ReferenceInfoServices> logger, HttpClient client)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<GetReferenceInfoModel> GetReferenceInformation(string endpoint,string accessToken)
        {
            logger.LogDebug($"{DateTime.Now}: Started executing the method:{MethodBase.GetCurrentMethod().Name} for retrieving the rference informations related to customers");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync(endpoint);
            logger.LogDebug($"{DateTime.Now}: {MethodBase.GetCurrentMethod().Name} : API call response is {response.IsSuccessStatusCode}");
            return await response.ReadContentAsync<GetReferenceInfoModel>();
        }
    }
}