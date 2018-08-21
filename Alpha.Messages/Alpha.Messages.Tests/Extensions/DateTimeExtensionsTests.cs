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

        [TestCase(0, 0, 0, "11/24/2016 12:00:00 AM")]
        [TestCase(1, 0, 0, "11/24/2016 01:00:00 AM")]
        [TestCase(11, 59, 59, "11/24/2016 11:59:59 AM")]
        [TestCase(12, 0, 0, "11/24/2016 12:00:00 PM")]
        [TestCase(13, 0, 0, "11/24/2016 01:00:00 PM")]
        [TestCase(23, 59, 59, "11/24/2016 11:59:59 PM")]
        public void ToLongDateWithSlash_Always_ReturnsLongDayWithSlash(int hour, int minute, int second, string expectedDay)
        {
            // Arrange
            var dateTime = new DateTime(2016, 11, 24, hour, minute, second);

            // Act
            var actualDay = dateTime.ToLongDateWithSlash();

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

        [TestCase(10, "Monday")]
        [TestCase(11, "Tuesday")]
        [TestCase(12, "Wednesday")]
        [TestCase(13, "Thursday")]
        [TestCase(14, "Friday")]
        [TestCase(15, "Saturday")]
        [TestCase(16, "Sunday")]
        [TestCase(17, "Monday")]
        public void ToFullDateName_Always_ReturnsFullDateName(int day, string expectedFullDateName)
        {
            // Arrange
            var dateTime = new DateTime(2017, 4, day);

            // Act
            var actualFullDateName = dateTime.ToFullDateName();

            // Assert
            Assert.AreEqual(expectedFullDateName, actualFullDateName);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(31, 1)]
        [TestCase(1, 6)]
        [TestCase(2, 6)]
        [TestCase(30, 6)]
        [TestCase(1, 12)]
        [TestCase(2, 12)]
        [TestCase(31, 12)]
        public void FirstDayOfMonth_Always_ReturnsFirstDayOfMonth(int day, int month)
        {
            // Arrange
            var dateTime = new DateTime(2016, month, day);

            // Act
            var actualDate = dateTime.FirstDayOfMonth();

            // Assert
            Assert.AreEqual(month, actualDate.Month);
            Assert.AreEqual(1, actualDate.Day);
        }

        [TestCase(1, 1)]
        [TestCase(1, 15)]
        [TestCase(1, 31)]
        [TestCase(6, 1)]
        [TestCase(6, 15)]
        [TestCase(6, 30)]
        [TestCase(12, 1)]
        [TestCase(12, 15)]
        [TestCase(12, 31)]
        public void FirstDayOfYear_Always_ReturnsFirstDayOfYear(int month, int day)
        {
            // Arrange
            var date = new DateTime(2016, month, day);
            var expectedDate = new DateTime(2016, 1, 1);

            // Act
            var actualDate = date.FirstDayOfYear();

            // Assert
            Assert.AreEqual(expectedDate, actualDate);
        }
    }
}