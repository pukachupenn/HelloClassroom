using System;
using System.Web.Http;
using HelloClassroom.Commands;
using Newtonsoft.Json;
using Fuzzy.Cortana;
using System.Threading.Tasks;

namespace HelloClassroom.Controllers
{
	[RoutePrefix("api/go")]
	public class GoController : ApiController
	{
		LuisClient luisClient = new LuisClient();

		// GET: api/go/5
		[HttpGet]
		[Route("{commandName}", Name = "Get")]
		public async Task<string> Get(string commandName)
		{
			var result = await CallLuis(commandName);

			string serializedJson = JsonConvert.ToString(result);
			return serializedJson;
		}

		private async Task<bool> CallLuis(string command)
		{
			var parsedMessage = await luisClient.parseInput(command);

			switch (parsedMessage.topScoringIntent.intent)
			{
				case "Count":
					{
						int from = 1;
						int to = 10;
						foreach (lEntity ent in parsedMessage.entities)
						{
							var entityType = ent.type;
							if (entityType.Equals("To"))
							{
								to = Convert.ToInt32(ent.entity, to);
							}
							else if (entityType.Equals("From"))
							{
								from = Convert.ToInt32(ent.entity, from);
							}
						}

						String returnMe = "";
						for (int i = from; i <= to; i++)
						{
							returnMe = returnMe + i + " ";
						}
					}
					break;

				default:
					throw new InvalidOperationException();
			}

			return true;
		}

	}
}
