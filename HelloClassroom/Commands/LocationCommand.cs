using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fuzzy.Cortana;
using HelloClassroom.Models;
using HelloClassroom.Utils;
using Newtonsoft.Json.Linq;

namespace HelloClassroom.Commands
{
	public class LocationCommand : CommandBase
	{
		private Dictionary<string, object> _data;

		public LocationCommand(IEnumerable<lEntity> entities) : base(entities)
		{
		}

		public override async Task ProcessCommand()
		{
			var location = GetLocationName();
			_data = await GetLocationInfo(location);
		}

		public override DeviceCommand GenerateJsonPayload()
		{
			var command = new DeviceCommand()
			{
				Type = CommandType.Location,
				Data = _data
			};

			return command;
		}

		private string GetLocationName()
		{
			return _entities.First(x => string.Equals(x.type, "Location", StringComparison.OrdinalIgnoreCase))?.entity;
		}

		private static async Task<Dictionary<string, object>> GetLocationInfo(string location)
		{
			HttpClient bingClient = new HttpClient();
			HttpResponseMessage response = await bingClient.GetAsync(BuildResponseUri(location)).ConfigureAwait(false);
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

			locationData["Thumbnail"] = ImageUtils.ConvertImageUrlToBase64((string) entityObject.SelectToken("image.thumbnailUrl"));

			return locationData;
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