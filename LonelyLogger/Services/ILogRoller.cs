using LonelyLogger.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LonelyLogger.Services
{
    public interface ILogRoller
    {
        IList<LogFile> GetLogFilesForLogs(IList<LogWithMetaData> logs);
    }
}
