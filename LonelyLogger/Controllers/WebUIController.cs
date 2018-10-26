using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LonelyLogger.Models;
using Microsoft.AspNetCore.Mvc;

namespace LonelyLogger.Controllers
{
    [Route("webui")]
    public class WebUIController : Controller
    {
        public LoggerDiskStatus Index()
        {
            var allDrives = DriveInfo.GetDrives();
            var path = Path.GetPathRoot(Environment.CurrentDirectory);

            var currentDriveInfo = allDrives.First(x => String.Equals(x.RootDirectory.FullName, path, StringComparison.OrdinalIgnoreCase));
            var availableSpaceString = BytesToString(currentDriveInfo.TotalFreeSpace);
            var totalSpaceString = BytesToString(currentDriveInfo.TotalSize);
            return new LoggerDiskStatus()
            {
                AvailableSpace = availableSpaceString,
                TotalSpace = totalSpaceString
            };
        }

        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}