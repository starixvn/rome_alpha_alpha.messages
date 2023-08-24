#pragma warning disable 612, 618

namespace Alpha.Messages.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Helpers;
    using KellermanSoftware.CompareNetObjects;
    using Messages.Constants;
    using Messsages.Site.Requests.PortalMessage;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Services;
    using Site.Handlers;
    using Site.ViewModels;
    using AlphaPlus.User.Models;

    [TestFixture]
    public class PortalMessageHandlerTests
    {
        private PortalMessageService noticeService;
        private PortalMessageHandler noticeHandler;
        private IUserSession userSession;

        [SetUp]
        public void SetUp()
        {
            noticeService = Substitute.For<PortalMessageService>();
            noticeHandler = new PortalMessageHandler(noticeService);

            userSession = Substitute.For<IUserSession>();
            userSession.CustId.Returns(CustId.ValidId);
            userSession.RoleId.Returns(RoleId.Agent);
        }      

        [Test]
        public void HandleNoticeQuery_NotNullQuery_CallGetNoticeService()
        {
            // Arrange
            var noticeMessages = FakeDataHelper.CreateMany<PortalMessage>().ToList();
            FakeReturnGetNoticeMessagesService(noticeMessages);

            var query = new PortalMessageQuery(userSession, 10, false);

            // Act
            noticeHandler.Handle(query);

            // Assert
            noticeService
                .Received()
                .GetPortalMessages(
                    Arg.Is<int>(cid => cid == query.CustId),
                    Arg.Is<string>(pid => pid == query.ParentSites),
                    Arg.Is<int>(tm => tm == query.TotalMessage),
                    Arg.Is<bool>(it => it == query.IsTest));
        }

        [Test]
        public void HandleMarkMessageAsReadQuery_NotNullQuery_CallMarkMessageAsReadService()
        {
            // Arrange
            var query = new MarkMessageAsReadQuery(CustId.ValidId, 1, false);

            // Act
            noticeHandler.Handle(query);

            // Assert
            noticeService
                .Received()
                .MarkMessageAsRead(
                    Arg.Is<int>(cid => cid == query.CustId),
                    Arg.Is<int>(mid => mid == query.MessageId),
                    Arg.Is<bool>(mid => mid == query.IsInternal));
        }

        [Test]
        public void HandleNoticeQuery_NotNullQuery_ReturnNoticeViewModel()
        {
            // Arrange
            var expectedNoticeMessages = FakeDataHelper.CreateMany<PortalMessage>().ToList();
            FakeReturnGetNoticeMessagesService(expectedNoticeMessages);

            var query = new PortalMessageQuery(userSession, 100, false);

            // Act
            var viewModel = noticeHandler.Handle(query);

            // Numbers of notice message
            var expectedCount = expectedNoticeMessages
                .Where(m => m.TopLevel >= (byte)MessageRange.Normal)
                .Count();

            // Assert
            Assert.AreEqual(expectedCount, viewModel.NormalMessages.Count());
        }

        [Test]
        public void HandleHightlightQuery_NotNullQuery_ReturnNoticeViewModel()
        {
            // Arrange
            var expectedNoticeMessages = FakeDataHelper.CreateMany<PortalMessage>().ToList();
            FakeReturnGetNoticeMessagesService(expectedNoticeMessages);

            var query = new PortalMessageQuery(userSession, 100, false);

            // Act
            var viewModel = noticeHandler.Handle(query);

            // Numbers of highlight message
            var expectedCount = expectedNoticeMessages.Where(m => m.TopLevel <= (byte)MessageRange.Highlight).Count();

            // Assert
            Assert.AreEqual(expectedCount, viewModel.HighlightMessages.Count());
        }

        [Test]
        public void Handle_HightlightOrderByTopLevelAsc_ThenByPostTimeDesc_Query_NotNullQuery_ReturnNoticeViewModel()
        {
            // Arrange
            var noticeMessages = new List<PortalMessage>
            {
                CreateNoticeMessage(1, new DateTime(2017, 5, 1), 1),
                CreateNoticeMessage(2, new DateTime(2017, 5, 2), 3),
                CreateNoticeMessage(3, new DateTime(2017, 5, 2), 1)
            };

            FakeReturnGetNoticeMessagesService(noticeMessages);
            var expectedMessages = new List<PortalMessageWrapper>
            {
                new PortalMessageWrapper(CreateNoticeMessage(3, new DateTime(2017, 5, 2), 1), userSession.Language),
                new PortalMessageWrapper(CreateNoticeMessage(1, new DateTime(2017, 5, 1), 1), userSession.Language),
                new PortalMessageWrapper(CreateNoticeMessage(2, new DateTime(2017, 5, 2), 3), userSession.Language)
            };

            var query = new PortalMessageQuery(userSession, 100, false);

            var compareLogic = new CompareLogic();

            // Act
            var viewModel = noticeHandler.Handle(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedMessages, viewModel.HighlightMessages.ToList()).AreEqual);
        }

        [Test]
        public void Handle_NormalOrderByPostTimeDesc_Query_NotNullQuery_ReturnNoticeViewModel()
        {
            // Arrange
            var noticeMessages = new List<PortalMessage>
            {
                CreateNoticeMessage(1, new DateTime(2017, 5, 1), 41),
                CreateNoticeMessage(2, new DateTime(2017, 5, 2), 33),
                CreateNoticeMessage(3, new DateTime(2017, 5, 2), 45)
            };

            FakeReturnGetNoticeMessagesService(noticeMessages);
            var expectedMessages = new List<PortalMessageWrapper>
            {
                new PortalMessageWrapper(CreateNoticeMessage(2, new DateTime(2017, 5, 2), 33), userSession.Language),
                new PortalMessageWrapper(CreateNoticeMessage(3, new DateTime(2017, 5, 2), 45), userSession.Language),
                new PortalMessageWrapper(CreateNoticeMessage(1, new DateTime(2017, 5, 1), 41), userSession.Language)
            };

            var query = new PortalMessageQuery(userSession, 100, false);

            var compareLogic = new CompareLogic();

            // Act
            var viewModel = noticeHandler.Handle(query);

            // Assert
            Assert.True(compareLogic.Compare(expectedMessages, viewModel.NormalMessages.ToList()).AreEqual);
        }

        private void FakeReturnGetNoticeMessagesService(List<PortalMessage> noticeMessages)
        {
            noticeService.GetPortalMessages(
                      Arg.Any<int>(),
                      Arg.Any<string>(),
                      Arg.Any<int>(),
                      Arg.Any<bool>())
                  .Returns(noticeMessages);
        }

        private PortalMessage CreateNoticeMessage(int id, DateTime postTime, byte topLevel)
            => new PortalMessage(
                id,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                postTime,
                0,
                true,
                string.Empty,
                true,
                topLevel);
    }
}