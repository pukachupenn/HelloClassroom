using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
            return "value";
        }

        // POST: api/Luis
        public void Post([FromBody]string value)
        {
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
