using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LonelyLogger.Client
{
    public class LonelyLoggerClient : ILonelyLoggerClient
    {
        private Uri _BaseAddress;
        private readonly string _PostLogPath = "/api/logs";
        public LonelyLoggerClient(Uri baseAddress)
        {
            _BaseAddress = baseAddress;
        }

        public async Task PostLogAsync(object objectToLog)
        {
            var client = new HttpClient();
            client.BaseAddress = _BaseAddress;
            client.Timeout = new TimeSpan(0, 0, 30);

            var serializedObject = JsonConvert.SerializeObject(objectToLog);
            var bodyContent = new StringContent(serializedObject);
            bodyContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(_PostLogPath, bodyContent);
            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("LonelyLogger did not recieve a 200 response.");
            }
        }
    }
}
