using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NetFrameworkWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        static List<string> stringofvalues = new List<string>()
        { "Xavier","Madhu", "Calvyn"};

        // GET api/values
        public IEnumerable<string> Get()
        {
            return stringofvalues;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return stringofvalues[id];
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
            stringofvalues.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
            stringofvalues[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            stringofvalues.RemoveAt(id);
        }
    }
}
