using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonelyLogger.Models
{
    public class LogWithMetaData
    {
        public JObject Log { get; set; }
        public LogMetaData MetaData { get; set; }
    }
}
