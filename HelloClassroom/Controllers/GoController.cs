using System;
using System.Web.Http;
using HelloClassroom.Commands;
using Newtonsoft.Json;
using Fuzzy.Cortana;
using System.Threading.Tasks;
using HelloClassroom.Models;

namespace HelloClassroom.Controllers
{
	[RoutePrefix("api/go")]
	public class GoController : ApiController
	{
		LuisClient luisClient = new LuisClient();

		// GET: api/go/5
		[HttpGet]
		[Route("{commandName}", Name = "Get")]
		public async Task<DeviceCommand> Get(string commandName)
		{
			return await CallLuis(commandName);
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
