
using System.Text;
using Newtonsoft.Json;
using Application.ServiceHelper;

namespace Infrastructure.Helper
{
    public class HttpExtensionRepository : IHttpExtensionRepository
    {
        public async Task<string> PostAsync(dynamic model, string uri, string apiKey)
        {
            using (var client = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(model);
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, uri);
                request.Headers.Add("X-BUSINESS-API-KEY", apiKey);
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Content = httpContent;

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();

                return jsonResponse;
            }
        }
    }
}
