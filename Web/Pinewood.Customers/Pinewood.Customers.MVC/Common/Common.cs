using System.Net.Http.Headers;
using System.Text.Json;

namespace Pinewood.Customers.MVC.Common
{
    public static class JsonExtenstions
    {
        public static JsonSerializerOptions GetJsonSerializerOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }
    }

    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            //if (response.IsSuccessStatusCode == false)
            //    throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");
            response.EnsureSuccessStatusCode();

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<T>(dataAsString, JsonExtenstions.GetJsonSerializerOptions());

            return result;
        }
        public static Action<HttpClient> GetHttpClientOptions(WebApplicationBuilder builder)
        {
            return options =>
            {
                options.BaseAddress = new Uri(builder.Configuration[key: "APIUrl"].ToString());
                options.DefaultRequestHeaders.Accept.Clear();
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            };
        }
    }
}
