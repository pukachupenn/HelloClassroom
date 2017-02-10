using System;
using System.Configuration;
using System.Web.Http;
using HelloClassroom.Commands;
using Newtonsoft.Json;
using Fuzzy.Cortana;
using System.Threading.Tasks;
using HelloClassroom.Communication;
using HelloClassroom.Models;

namespace HelloClassroom.Controllers
{
	[RoutePrefix("api/go")]
	public class GoController : ApiController
	{
	    private readonly LuisClient luisClient = new LuisClient();

		// GET: api/go/5
		[HttpGet]
		[Route("{commandName}", Name = "Get")]
		public async Task<DeviceCommand> Get(string commandName)
		{
			var deviceCommand = await CallLuis(commandName);

		    var stringMessage = JsonConvert.SerializeObject(deviceCommand);
            var sender = new CloudToDeviceMessageSender(GetConnectionString());
		    await sender.SendMessageAsync(GetDeviceName(), stringMessage);

		    return deviceCommand;
		}

	    private string GetDeviceName()
	    {
	        return "myFirstDevice";
	    }

	    private static string GetConnectionString()
	    {
            return ConfigurationManager.AppSettings["IoTHubConnectionString"];
        }

		private async Task<DeviceCommand> CallLuis(string input)
		{
			var parsedMessage = await luisClient.parseInput(input);

			CommandBase command = null;

			switch (parsedMessage.topScoringIntent.intent)
			{
				case "Count":
					command = new CountCommand(parsedMessage.entities);
					break;

				case "Location":
					command = new LocationCommand(parsedMessage.entities);
					break;

				default:
					throw new InvalidOperationException();
			}

			return await command.Run();
		}
	}
}
