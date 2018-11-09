using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonelyLogger.Models
{
    public class ServerSettings
    {
        public string AdminUsername { get; set; }
        public string AdminPassword { get; set; }
        public bool UseApiAuthentication { get; set; }
    }
}
