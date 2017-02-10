using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Fuzzy.Cortana
{
    public class LuisClient
    {
        private string luisId { get; }
        private string luisSubscriptionId { get; }

        public string luisEndpoint = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" +
                                     "{0}" +
                                     "?subscription-key={1}" +
                                     "&q={2}";

        public LuisClient()
        {
            this.luisId = "04e97667-6980-4071-9c3a-0a548fd24a1f";
            this.luisSubscriptionId = "e9e5f0f8f031491e9869ccf3c03b57ea";
        }

        public LuisClient(string luisId, string luisSubscriptionId)
        {
            this.luisId = luisId;
            this.luisSubscriptionId = luisSubscriptionId;
        }

        public async Task<LuisModel> parseInput(string strInput)
        {
            string strRet = string.Empty;
            string strEscaped = Uri.EscapeDataString(strInput);

            using(var client = new HttpClient()) {
                string uri = String.Format(luisEndpoint, luisId, luisSubscriptionId, strEscaped);

				HttpResponseMessage msg = await client.GetAsync(uri);

                if(msg.IsSuccessStatusCode) {
                    var jsonResponse = await msg.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<LuisModel>(jsonResponse);
                    return data;
                }
            }
            return null;
        }
    }
}