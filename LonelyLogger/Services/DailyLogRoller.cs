using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LonelyLogger.Constants;
using LonelyLogger.Models;
using Newtonsoft.Json.Linq;

namespace LonelyLogger.Services
{
    public class DailyLogRoller : ILogRoller
    {
        private readonly string _LogFilePrefix = "logfile";

        public IList<LogFile> GetLogFilesForLogs(IList<LogWithMetaData> logs)
        {
            var logGroups = logs.GroupBy(x =>
            {
                var date = x.MetaData.ReceivedTime;
                var dateObject = DateTime.Parse(date.ToString());
                return dateObject.Date;
            });

            var logFiles = new List<LogFile>();
            foreach(var group in logGroups)
            {
                var logFile = new LogFile()
                {
                    FileName = _LogFilePrefix + "-" + group.Key.ToString("MM-dd-yy") + ".json",
                    Logs = logGroups.SelectMany(x => x).ToList()
                };
                logFiles.Add(logFile);
            }

            return logFiles;
        }
    }
}
