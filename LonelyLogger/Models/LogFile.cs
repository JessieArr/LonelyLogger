using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonelyLogger.Models
{
    public class LogFile
    {
        public string FileName;
        public IList<JObject> Logs;
    }
}
