using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Razenager_modded.Web
{
    internal class PostNager : IDisposable
    {
        private readonly HttpClient client;
        private readonly HttpRequestMessage request;

        public PostNager()
        {
            client = new HttpClient();
            request = new HttpRequestMessage();
            request.Headers.Add("Accept", "application/json, text/plain, */*");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36");
        }

        public async Task<string> AuthToken(string mooToken, Uri url, HttpMethod method, string mooSubjectToken = null)
        {
            request.RequestUri = url;
            request.Method = method;

            string bodyContent;

            if (mooSubjectToken == null) bodyContent = "{\"params\":{\"appVersion\":\"1.0.0-PROD\",\"lang\":\"es-PE\",\"mooToken\":\"" + mooToken + "\",\"platform\":\"web\"}}";
            else bodyContent = "{\"params\":{\"appVersion\":\"1.0.0-PROD\",\"lang\":\"es-PE\",\"mooToken\":\"" + mooToken + "\",\"mooSubjectToken\":\"" + mooSubjectToken + "\",\"platform\":\"web\"}}";

            request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JObject.Parse(response.Content.ReadAsStringAsync().Result).ToString();
            }

            return null;
        }

        public void Dispose()
        {
            request?.Dispose();
            client?.Dispose();
        }
    }
}
