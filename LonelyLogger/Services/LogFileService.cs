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
        private readonly string _LogFileDirectory = "logs";
        private readonly string _LogFilePrefix = "logfile";

        public void SaveLogsToFile(IList<JObject> logs)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            // Make sure the logs folder exists.
            Directory.CreateDirectory(Path.Combine(currentDirectory, _LogFileDirectory));

            var formattedDate = DateTime.Now.ToString("MM-dd-yy");
            var logFilePath = Path.Combine(currentDirectory, _LogFileDirectory);
            var logFileName = logFilePath + Path.DirectorySeparatorChar + _LogFilePrefix + formattedDate + ".json";
            var logsAsText = JsonConvert.SerializeObject(logs);
            File.WriteAllText(logFileName, logsAsText);
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
