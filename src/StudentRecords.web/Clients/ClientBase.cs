using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace StudentRecords.web.Clients
{
    public abstract class ClientBase
    {
        private static string BaseAddress => "https://localhost:44375/";

        protected HttpClient HttpClient { get; private set; }

        protected ClientBase()
        {
            HttpClient = GetHttpClient();
        }

        private static HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        protected T Deserialize<T>(HttpResponseMessage response) where T : class
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();

            var result = response.Content;
            var content = result.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content.Result);
        }
        protected StringContent Serialize<T>(T input)
        {
            var folderAsJosn = JsonConvert.SerializeObject(input);
            return new StringContent(folderAsJosn, Encoding.UTF8, "application/json");
        }
    }
}
