using LonelyLogger.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LonelyLogger.Services
{
    public static class ServerSettingsService
    {
        public static readonly string SettingsFileName = "settings.json";
        private static ServerSettings _ServerSettings;

        public static ServerSettings GetServerSettings()
        {
            if(_ServerSettings == null)
            {
                LoadSettings();
            }
            return _ServerSettings;
        }

        private static void LoadSettings()
        {
            var fileText = File.ReadAllText(SettingsFileName);
            _ServerSettings = JsonConvert.DeserializeObject<ServerSettings>(fileText);
        }
    }
}
