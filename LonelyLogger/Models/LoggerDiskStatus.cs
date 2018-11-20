using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonelyLogger.Models
{
    public class LoggerServerStatus
    {
        public string AvailableSpace { get; set; }
        public string TotalSpace { get; set; }
        public string CurrentRAMUsage { get; set; }
        public DateTime StartupTime { get; set; }
        public string Uptime { get; set; }
    }
}
