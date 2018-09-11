namespace Alpha.Messages.Tests.Models.Dashboard.Notice
{
    using System;
    using System.Collections.Generic;
    using Messages.Models.Message;
    using NUnit.Framework;
    using Site.Tests.Helpers;

    [TestFixture]
    public class PortalMessageTests
    {
        [Test]
        public void BaseConstructor_Always_Set7Language()
        {
            //// Arrange
            var message = FakeDataHelper.Create<PortalMessage>();

            //// Act           
            var act = message.Contents;

            //// Assert
            Assert.AreEqual(7, act.Count);
        }

        [Test]
        public void PostTime_Always_SetValueFromCtor()
        {
            // Arrange
            DateTime postTime = DateTime.Today;

            // Act
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("postTime", postTime);

            // Assert
            Assert.AreEqual(postTime, noticeMessage.PostTime);
        }

        [Test]
        public void Type_Always_SetValueFromCtor()
        {
            // Arrange
            byte messageType = 1;

            // Act
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("type", messageType);

            // Assert
            Assert.AreEqual(messageType, noticeMessage.MessageType);
        }

        [Test]
        public void IsSticky_Always_SetValueFromCtor()
        {
            // Arrange
            bool isSticky = false;

            // Act
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("isSticky", isSticky);

            // Assert
            Assert.AreEqual(isSticky, noticeMessage.IsSticky);
        }

        [Test]
        public void BetTransId_Always_SetValueFromCtor()
        {
            // Arrange
            string betTransId = "1";

            // Act
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("betTransId", betTransId);

            // Assert
            Assert.AreEqual(betTransId, noticeMessage.BetTransId);
        }
        
        [Test]
        public void IsTopLevel_Always_SetValueFromCtor()
        {
            // Arrange
            List<int> range = new List<int>();
            for (int i = 1; i <= 255; i++)
            {
                range.Add(i);
            }

            // Act
            var noticeMessage = FakeDataHelper.Create<PortalMessage>("topLevel", range);

            // Assert
            Assert.Contains(noticeMessage.TopLevel, range);
        }
    }
}
