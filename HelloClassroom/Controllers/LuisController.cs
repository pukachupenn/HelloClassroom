using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelloClassroom.Commands;
using Newtonsoft.Json;

namespace HelloClassroom.Controllers
{
    public class LuisController : ApiController
    {
        // GET: api/Luis
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Luis/5
        public string Get(int id)
        {
	        return null;
        }

        // POST: api/Luis
        public async void Post([FromBody]string value)
        {
	        CommandBase command = null;

	        if (string.Equals(value, "count", StringComparison.OrdinalIgnoreCase))
	        {
		        command = new CountCommand("foo");
	        }
			else if (string.Equals(value, "location", StringComparison.OrdinalIgnoreCase))
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

        // PUT: api/Luis/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Luis/5
        public void Delete(int id)
        {
        }
    }
}
