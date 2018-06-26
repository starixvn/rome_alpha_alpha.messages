namespace Alpha.Messages.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Fanex.Data.Repository;
    using Messages.Models.Message;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Tests.Helpers;

    [TestFixture]
    public class PortalMessageServiceImplTests
    {
        private IDynamicRepository dynamicRepository;
        private PortalMessageServiceImpl service;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();

            service = new PortalMessageServiceImpl(dynamicRepository);
        }

        [Test]
        public void GetNoticeMessages_InvalidCustIdAndParentSiteIds_ReturnEmpty()
        {
            // Act
            var noticeMessages = service.GetPortalMessages(
                CustId.InvalidId,
                string.Empty,
                10,
                false);

            // Assert
            Assert.AreEqual(noticeMessages, new List<PortalMessage>());
        }

        [Test]
        public void GetNoticeMessages_ValidCustIdAndParentSiteIds_ReturnNoticeMessages()
        {
            // Arrange
            var expectedNoticeMessages = FakeDataHelper.CreateMany<PortalMessage>().ToList();

            dynamicRepository.Fetch<PortalMessage>(Arg.Any<ICriteria>()).Returns(expectedNoticeMessages);

            // Act
            var noticeMessages = service.GetPortalMessages(CustId.ValidId, "1", 10, false);

            // Assert
            Assert.AreEqual(noticeMessages, expectedNoticeMessages);
        }

        [Test]
        public void MarkMessageAsRead_InvalidCustId_ReturnFalse()
        {
            // Act
            var isSuccess = service.MarkMessageAsRead(CustId.InvalidId, 1, false);

            // Assert
            Assert.IsFalse(isSuccess);
        }

        [Test]
        public void MarkMessageAsRead_ValidCustIdAndMessageIds_ReturnTrue()
        {
            // Act
            var isSuccess = service.MarkMessageAsRead(CustId.ValidId, 1, false);

            // Assert
            Assert.IsTrue(isSuccess);
        }

        [Test]
        public void MarkMessageAsRead_UserIsInternal_ReturnFalse()
        {
            // Act
            var act = service.MarkMessageAsRead(CustId.ValidId, 1, true);

            // Assert
            Assert.IsFalse(act);
        }

        [Test]
        public void MarkMessageAsRead_UserIsInternal_NotCallRepository()
        {
            // Act
            var act = service.MarkMessageAsRead(CustId.ValidId, 1, true);

            // Assert
            dynamicRepository.DidNotReceive().Execute(Arg.Any<INonQueryCommand>());
        }
    }
}