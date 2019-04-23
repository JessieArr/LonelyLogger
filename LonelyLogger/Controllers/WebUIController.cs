using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LonelyLogger.Models;
using LonelyLogger.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace LonelyLogger.Controllers
{
    [Route("webui")]
    public class WebUIController : Controller
    {
        public LoggerServerStatus Index()
        {
            var allDrives = DriveInfo.GetDrives();
            var path = Path.GetPathRoot(Environment.CurrentDirectory);

            var currentDriveInfo = allDrives.First(x => String.Equals(x.RootDirectory.FullName, path, StringComparison.OrdinalIgnoreCase));
            var availableSpaceString = BytesToString(currentDriveInfo.TotalFreeSpace);
            var totalSpaceString = BytesToString(currentDriveInfo.TotalSize);
            var currentRAMUsage = BytesToString(GC.GetTotalMemory(false));
            var startupTime = LonelyLoggerLifetimeService.StartupTime;
            var uptimeInSeconds = DateTime.UtcNow.Subtract(LonelyLoggerLifetimeService.StartupTime).TotalSeconds;
            var uptimeString = SecondsToString(uptimeInSeconds);
            return new LoggerServerStatus()
            {
                AvailableSpace = availableSpaceString,
                TotalSpace = totalSpaceString,
                CurrentRAMUsage = currentRAMUsage,
                StartupTime = startupTime,
                Uptime = uptimeString
            };
        }

        public static String SecondsToString(double secondCount)
        {
            var secondsInMinute = 60;
            var secondsInHour = secondsInMinute * 60;
            var secondsInDay = secondsInHour * 24;
            var days = Math.Floor(secondCount / secondsInDay);
            var timeLeftAfterDays = secondCount % secondsInDay;
            var hours = Math.Floor(timeLeftAfterDays / secondsInHour);
            var timeLeftAfterHours = secondCount % secondsInHour;
            var minutes = Math.Floor(timeLeftAfterHours / secondsInMinute);
            return $"{days} days, {hours} hours, {minutes} minutes";
        }

        public static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        [Route("/webui/login")]
        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {
            var serverSettings = ServerSettingsService.GetServerSettings();
            if(!String.Equals(username, serverSettings.AdminUsername, StringComparison.OrdinalIgnoreCase)
                || password != serverSettings.AdminPassword)
            {
                return Redirect("~/web/login.html");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return Redirect("~/web/index.html");
        }

        [Route("/webui/logout")]
        [HttpGet]
        public async Task<ActionResult> Logout(string username, string password)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/web/index.html");
        }
    }
}