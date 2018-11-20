using System;
using System.Collections.Generic;
using System.Text;

namespace LonelyLogger.Client
{
    public class LogResult
    {
        public bool Succeeded { get; set; }
        public LogResultEnum Result { get; set; }
        public Exception Exception { get; set; }
    }
}
