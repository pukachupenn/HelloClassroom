namespace HelloClassroom.Controllers
{
	using System;
	using System.Configuration;
	using System.Web.Http;
	using HelloClassroom.Commands;
	using Newtonsoft.Json;
	using Fuzzy.Cortana;
	using System.Threading.Tasks;
	using HelloClassroom.Communication;
	using HelloClassroom.Models;

	[RoutePrefix("api/go")]
	public class GoController : ApiController
	{
		private readonly LuisClient luisClient = new LuisClient();

		// GET: api/go/5
		[HttpGet]
		[Route("{commandName}", Name = "Get")]
		public async Task<IHttpActionResult> Get(string commandName)
		{
			var deviceCommand = await CallLuisAsync(commandName);

			await SendDeviceCommandAsync(deviceCommand);

			return Ok();
		}

		[HttpGet]
		[Route("test/{commandName}", Name = "GetDeviceCommand")]
		public async Task<DeviceCommand> GetDeviceCommand(string commandName)
		{
			var deviceCommand = await CallLuisAsync(commandName);
			return deviceCommand;
		}

		private async Task SendDeviceCommandAsync(DeviceCommand deviceCommand)
		{
			var stringMessage = JsonConvert.SerializeObject(deviceCommand);
			var sender = new CloudToDeviceMessageSender(GetConnectionString());
			await sender.SendMessageAsync(GetDeviceName(), stringMessage);
		}

		private string GetDeviceName()
		{
			return "myFirstDevice";
		}

		private static string GetConnectionString()
		{
			return ConfigurationManager.AppSettings["IoTHubConnectionString"];
		}

		private async Task<DeviceCommand> CallLuisAsync(string input)
		{
			var parsedMessage = await luisClient.parseInput(input);

			CommandBase command;

			switch (parsedMessage.topScoringIntent.intent)
			{
				case "Count":
					command = new CountCommand(parsedMessage.entities);
					break;

				case "Location":
					command = new LocationCommand(parsedMessage.entities);
					break;

				case "Calculation":
					command = new CalculationCommand(parsedMessage.entities);
					break;

				case "Timer":
					command = new TimerCommand(parsedMessage.entities);
					break;

				default:
					command = new NoneCommand();
					break;
			}

			return await command.Run();
		}
	}
}
