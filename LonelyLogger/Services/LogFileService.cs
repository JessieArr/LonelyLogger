using LonelyLogger.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LonelyLogger.Services
{
    public class LogFileService
    {
        private readonly string _LogFilePrefix = "logfile";
        private readonly string _LogFileDirectory = "logs";
        private readonly ILogRoller _LogRoller;

        public LogFileService(ILogRoller logRoller)
        {
            _LogRoller = logRoller;
        }

        public void SaveLogsToFile(IList<JObject> logs)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            // Make sure the logs folder exists.
            var logFileDirectory = Path.Combine(currentDirectory, _LogFileDirectory);
            Directory.CreateDirectory(logFileDirectory);

            var logFiles = _LogRoller.GetLogFilesForLogs(logs);

            foreach(var logToWrite in logFiles)
            {
                var logsAsText = JsonConvert.SerializeObject(logToWrite.Logs);
                File.WriteAllText(logFileDirectory + Path.DirectorySeparatorChar + logToWrite.FileName, logsAsText);
            }
        }

        public IList<JObject> LoadCurrentLogFile()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            // Make sure the logs folder exists.
            Directory.CreateDirectory(Path.Combine(currentDirectory, _LogFileDirectory));

            var formattedDate = DateTime.Now.ToString("MM-dd-yy");
            var logFilePath = Path.Combine(currentDirectory, _LogFileDirectory);
            var logFileName = logFilePath + Path.DirectorySeparatorChar + _LogFilePrefix + formattedDate + ".json";

            if (!File.Exists(logFileName))
            {
                // If we have no logs yet, return an empty list.
                return new List<JObject>();
            }
            else
            {
                var serializedContents = File.ReadAllText(logFileName);
                var logs = JsonConvert.DeserializeObject<List<JObject>>(serializedContents);

                return logs;
            }            
        }
    }
}
