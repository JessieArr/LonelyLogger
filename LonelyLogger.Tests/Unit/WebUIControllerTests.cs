using LonelyLogger.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LonelyLogger.Tests.Unit
{
    public class WebUIControllerTests
    {
        [Fact]
        public void Test()
        {
            var numberOfSecondsInHour = 3600;
            var numberOfHoursInDay = 24;
            var numberOfSecondsInDay = numberOfSecondsInHour * numberOfHoursInDay;

            var oneDay = WebUIController.SecondsToString(numberOfSecondsInDay + 1);

            Assert.Equal("1 days, 0 hours, 0 minutes", oneDay);

            var elevenHours = WebUIController.SecondsToString(numberOfSecondsInHour * 11);
            Assert.Equal("0 days, 11 hours, 0 minutes", elevenHours);

            var thirteenHours = WebUIController.SecondsToString(numberOfSecondsInHour * 13);
            Assert.Equal("0 days, 13 hours, 0 minutes", thirteenHours);

            var thirtyOneMinutes = WebUIController.SecondsToString(31 * 60);
            Assert.Equal("0 days, 0 hours, 31 minutes", thirtyOneMinutes);

            var twentyNineSeconds = WebUIController.SecondsToString(29);
            Assert.Equal("0 days, 0 hours, 0 minutes", twentyNineSeconds);

        }
    }
}
