namespace Alpha.Messages.Tests.Extensions
{
    using System;
    using Messages.Extensions;
    using NUnit.Framework;

    public class DateTimeExtensionsTests
    {
        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(12, 0, 0)]
        [TestCase(13, 0, 0)]
        [TestCase(23, 59, 59)]
        public void ToShortDayWithSlash_Always_ReturnsShortDayWithSlash(int hour, int minute, int second)
        {
            // Arrange
            var dateTime = new DateTime(2016, 11, 24, hour, minute, second);
            var expectedDay = "11/24/2016";

            // Act
            var actualDay = dateTime.ToShortDateWithSlash();

            // Assert
            Assert.AreEqual(expectedDay, actualDay);
        }

        [TestCase(0, 0, 0, "12:00:00 AM")]
        [TestCase(1, 0, 0, "01:00:00 AM")]
        [TestCase(12, 0, 0, "12:00:00 PM")]
        [TestCase(13, 0, 0, "01:00:00 PM")]
        [TestCase(23, 59, 59, "11:59:59 PM")]
        public void ToLongTime_Always_ReturnsLongTime(int hour, int minute, int second, string expectedTime)
        {
            // Arrange
            var dateTime = new DateTime(2016, 11, 24, hour, minute, second);

            // Act
            var actualTime = dateTime.ToLongTime();

            // Assert
            Assert.AreEqual(expectedTime, actualTime);
        }
    }
}