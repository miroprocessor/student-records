using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace StudentRecords.web.Clients
{
    public abstract class ClientBase
    {
        private static string BaseAddress => ConfigurationManager.AppSettings["StudentApiUrl"];

        protected string ProjectKey { get; private set; }
        protected HttpClient HttpClient { get; private set; }

        protected ClientBase()
        { }
        protected ClientBase(string projectKey, string projectToken)
        {
            ProjectKey = projectKey;
            HttpClient = GetHttpClient(projectKey, projectToken);
        }

        private static HttpClient GetHttpClient(string projectKey, string projectToken)
        {
            var httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = GetBasicAuthenticationHeader(projectKey, projectToken);

            return httpClient;
        }

        private static AuthenticationHeaderValue GetBasicAuthenticationHeader(string key, string token)
        {
            var parameters = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", key, token));
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(parameters));
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
