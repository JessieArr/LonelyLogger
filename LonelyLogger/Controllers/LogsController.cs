using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LonelyLogger.Models;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LonelyLogger.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private static List<JObject> Logs = new List<JObject>();

        // GET api/logs
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return Logs;
        }

        // GET api/logs/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/logs
        [HttpPost]
        public void Post([FromBody] JObject newLog)
        {
            newLog["log_time"] = DateTime.Now;
            Logs.Add(newLog);
        }

        // PUT api/logs/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
