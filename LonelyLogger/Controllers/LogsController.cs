using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LonelyLogger.Constants;
using LonelyLogger.Models;
using LonelyLogger.Services;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LonelyLogger.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private static IList<LogWithMetaData> _Logs;
        private static IList<LogWithMetaData> Logs {
            get {
                if(_Logs == null)
                {
                    var fileService = new LogFileService(new DailyLogRoller());
                    _Logs = fileService.LoadCurrentLogFile();
                }
                return _Logs;
            }
        }

        // GET api/logs
        [HttpGet]
        public IEnumerable<object> Get()
        {
            if (ServerSettingsService.GetServerSettings().UseApiAuthentication)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.StatusCode = 401;
                    return null;
                }
            }
            return Logs;
        }

        // GET api/logs/5
        [HttpGet]
        [Route("/api/log")]
        public object Get(Guid id)
        {
            if (ServerSettingsService.GetServerSettings().UseApiAuthentication)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.StatusCode = 401;
                    return null;
                }
            }
            return Logs.First(x => x.MetaData.LogId == id);
        }

        // POST api/logs
        [HttpPost]
        public void Post([FromBody] JObject newLog)
        {
            var logWithMetaData = new LogWithMetaData()
            {
                Log = newLog,
                MetaData = new LogMetaData()
                {
                    LogId = Guid.NewGuid(),
                    ReceivedTime = DateTime.UtcNow
                }
            };
            Logs.Insert(0, logWithMetaData);

            var fileService = new LogFileService(new DailyLogRoller());
            fileService.SaveLogsToFile(Logs);
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
