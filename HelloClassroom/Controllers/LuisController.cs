﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using HelloClassroom.Commands;
using HelloClassroom.Models;
using Newtonsoft.Json;

namespace HelloClassroom.Controllers
{
	[RoutePrefix("api/luis")]
	public class LuisController : ApiController
    {
		// GET: api/Luis/5
		[HttpGet]
		[Route("{commandName}", Name = "Get")]
		public async Task<DeviceCommand> Get(string commandName)
        {
			CommandBase command = null;

			if (string.Equals(commandName, "count", StringComparison.OrdinalIgnoreCase))
			{
				command = new CountCommand("foo");
			}
			else if (string.Equals(commandName, "location", StringComparison.OrdinalIgnoreCase))
			{
				command = new LocationCommand("poo");
			}
			else
			{
				throw new InvalidOperationException();
			}

			return await command.Run();
			//return output;
			//string serializedJson = JsonConvert.SerializeObject(output);
			//return serializedJson;
        }

		// POST: api/Luis
		[HttpPost]
		[Route("{commandName}", Name = "Post")]
		public async void Post([FromBody]string commandName)
        {
	        CommandBase command = null;

	        if (string.Equals(commandName, "count", StringComparison.OrdinalIgnoreCase))
	        {
		        command = new CountCommand("foo");
	        }
			else if (string.Equals(commandName, "location", StringComparison.OrdinalIgnoreCase))
			{
				command = new LocationCommand("poo");
			}
			else
			{
				throw new InvalidOperationException();
			}

	        var output = await command.Run();

	        string serializedJson = JsonConvert.SerializeObject(output);

	        // TODO: Send it to the IOT app
        }
    }
}
