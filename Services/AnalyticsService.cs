using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dfreeze.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private const string ServiceUrl = "https://api.amplitude.com/httpapi";
        private const string ApiKey = "602decd88052d00b0b91666d84e330dc";
        private readonly ILogger _logger;

        public AnalyticsService(ILogger<AnalyticsService> logger)
        {
            _logger = logger;
        }

        public void SendEvent(string name)
        {
            SendEvent(name, null);
        }

        public async void SendEvent(string name, IDictionary<string, object> data)
        {
            var args = new Dictionary<string, string>
            {
                ["api_key"] = ApiKey,
                ["event"] = GetEventJson(name, data)
            };

            try
            {
                await PostEventAsync(args);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);
            }
        }

        private async Task PostEventAsync(Dictionary<string, string> args)
        {
            var requestUri = new Uri(ServiceUrl);
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(args);
                using (var result = await client.PostAsync(requestUri, content))
                {
                    result.EnsureSuccessStatusCode();
                }
            }
        }

        private string GetEventJson(string eventType, IDictionary<string, object> data)
        {

            var result = new Dictionary<string, object>
            {
                ["user_id"] = "2ColdApi",
                ["event_type"] = eventType
            };

            if (data != null)
            {
                result["event_properties"] = data;
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}