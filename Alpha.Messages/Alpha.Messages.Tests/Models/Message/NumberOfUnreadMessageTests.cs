using Alpha.Messages.Models.Message;
using NUnit.Framework;

namespace Alpha.Messages.Tests.Models.Message
{
    public class NumberOfUnreadMessageTests
    {
        [Test]
        public void System_Always_SetOnConstructor()
        {
            //// Arrange
            var system = 123;
            var model = new NumberOfUnreadMessage
            {
                Normal = 1,
                Special = 2,
                System = system,
                Personal = 4
            };

            //// Act
            var act = model.System;

            //// Assert
            Assert.AreEqual(system, act);
        }

        [Test]
        public void Total_Always_SumAllProperties()
        {
            //// Arrange
            var expected = 10;
            var model = new NumberOfUnreadMessage
            {
                Normal = 1,
                Special = 2,
                System = 3,
                Personal = 4
            };

            //// Act
            var act = model.Total;

            //// Assert
            Assert.AreEqual(expected, act);
        }
    }
}
