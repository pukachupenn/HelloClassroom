using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HelloClassroom.Models;
using Newtonsoft.Json.Linq;

namespace HelloClassroom.Commands
{
	public class LocationCommand : CommandBase
	{
		public LocationCommand(string jsonData) : base(jsonData)
		{
		}

		public override Task ProcessCommand()
		{
			throw new System.NotImplementedException();
		}

		public override DeviceCommand GenerateJsonPayload()
		{
			throw new System.NotImplementedException();
		}

		private static async Task<Dictionary<string, object>> GetLocationInfo(string location)
		{
			HttpClient bingClient = new HttpClient();
			HttpResponseMessage response = await bingClient.GetAsync(BuildResponseUri(location));
			string jsonResponse = await response.Content.ReadAsStringAsync();

			return CreateLocationDataFromResponse(jsonResponse);
		}


		private static Dictionary<string, object> CreateLocationDataFromResponse(string jsonResponse)
		{
			Dictionary<string, object> locationData = new Dictionary<string, object>();

			JObject responseObject = JObject.Parse(jsonResponse);
			JToken entityObject = responseObject.SelectToken("entities.value[0]");

			locationData["Description"] = (string)entityObject.SelectToken("description");
			locationData["Name"] = (string)entityObject.SelectToken("name");

			JToken entityPresentationInformation = entityObject.SelectToken("entityPresentationInfo");

			locationData["Type"] = (string)entityPresentationInformation.SelectToken("entityTypeHints[0]");

			JToken formattedFacts = entityPresentationInformation.SelectToken("formattedFacts");
			foreach (JToken formattedFact in formattedFacts.Children())
			{
				switch ((string)formattedFact.SelectToken("label"))
				{
					case "Population":
						locationData["Population"] = (string)formattedFact.SelectToken("items[0].text");
						break;
					case "Local time":
						locationData["Local time"] = (string)formattedFact.SelectToken("items[0].text");
						break;
					case "Area":
						locationData["Area"] = (string)formattedFact.SelectToken("items[0].text");
						break;
				}
			}

			locationData["Thumbnail"] = ConvertImageUrlToBase64((string) entityObject.SelectToken("image.thumbnailUrl"));

			return locationData;
		}

		private static string ConvertImageUrlToBase64(string url)
		{
			StringBuilder stringBuilder = new StringBuilder();

			byte[] _byte = GetImage(url);

			stringBuilder.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

			return stringBuilder.ToString();
		}

		private static byte[] GetImage(string url)
		{
			byte[] buffer = null;

			try
			{
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

				HttpWebResponse response = (HttpWebResponse)req.GetResponse();
				Stream stream = response.GetResponseStream();

				if(stream != null)
				{
					using (BinaryReader br = new BinaryReader(stream))
					{
						int len = (int)(response.ContentLength);
						buffer = br.ReadBytes(len);
						br.Close();
					}

					stream.Close();
				}

				response.Close();
			}
			catch (Exception)
			{
				buffer = null;
			}

			return buffer;
		}

		private static string BuildResponseUri(string location)
		{
			const string subscriptionKey = "025ac6f644d04dc5a3600484a96f49fc";

			return "https://api.cognitive.microsoft.com/bing/v5.0/knowledge?"
					+ $"q={location}"
					+ "&detectEntities=true"
					+ $"&subscription-key={subscriptionKey}";
		}
	}
}