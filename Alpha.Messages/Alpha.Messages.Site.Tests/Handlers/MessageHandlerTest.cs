namespace Alpha.Messages.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Helpers;
    using KellermanSoftware.CompareNetObjects;
    using Messages.Constants;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Params.Message;
    using Ploeh.AutoFixture;
    using Services;
    using Site.Handlers;
    using Site.Requests.Message;
    using Sunplus.Agent.Model.Entities;
    using ViewModels;

    [TestFixture]
    public class MessageHanlderTests
    {
        private MessageService messageService;
        private MessageHandler messageHandler;
#pragma warning disable CS0618 // Type or member is obsolete
        private IUserSession userSession;
#pragma warning restore CS0618 // Type or member is obsolete
        private Fixture fixture;
        [SetUp]
        public void SetUp()
        {
            messageService = Substitute.For<MessageService>();
            messageHandler = new MessageHandler(messageService);

#pragma warning disable CS0618 // Type or member is obsolete
            userSession = Substitute.For<IUserSession>();
#pragma warning restore CS0618 // Type or member is obsolete
            userSession.Language.Returns(Language.English);
            userSession.CustId.Returns(263);
            userSession.RoleId.Returns(RoleId.Agent);
            fixture = new Fixture();
        }
        
        [Test]
        public void HandleLatestMessageQuery_Always_Call4Methods()
        {
            //// Arrange
            messageService.GetLatestPublicMessage(Arg.Any<byte>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralLatestMessage>());
            messageService.GetLatestPrivateMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralLatestMessage>());
            messageService.CountUnreadPrivateMessage(Arg.Any<int>(), Arg.Any<string>())
                .Returns(fixture.Create<int>());
            messageService.GetMoveBetsMessage(Arg.Any<string>())
                .Returns(fixture.Create<GeneralMoveBetsMessage>());

            //// Act
            messageHandler.Handle(new LatestMessageQuery(userSession, false));

            //// Assert
            messageService.Received().GetLatestPublicMessage(Arg.Any<byte>(), Arg.Any<string>(), Arg.Any<bool>());
            messageService.Received().GetLatestPrivateMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>());
            messageService.Received().CountUnreadPrivateMessage(Arg.Any<int>(), Arg.Any<string>());
            messageService.Received().GetMoveBetsMessage(Arg.Any<string>());
        }

        [Test]
        public void HandleLatestMessageQuery_Always_ReturnViewModel()
        {
            //// Arrange
            messageService.GetLatestPublicMessage(Arg.Any<byte>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralLatestMessage>());
            messageService.GetLatestPrivateMessage(Arg.Any<int>(), Arg.Any<string>(), Arg.Any<bool>())
                .Returns(fixture.Create<GeneralLatestMessage>());
            messageService.CountUnreadPrivateMessage(Arg.Any<int>(), Arg.Any<string>())
                .Returns(fixture.Create<int>());
            messageService.GetMoveBetsMessage(Arg.Any<string>())
                .Returns(fixture.Create<GeneralMoveBetsMessage>());

            //// Act
            var act = messageHandler.Handle(new LatestMessageQuery(userSession, false));

            //// Assert
            Assert.IsInstanceOf(typeof(LatestMessageViewModel), act);
        }

        [Test]
        public void HandleUpdatePrivateAsRead_Always_CallServiceUpdate()
        {
            //// Arrange
            userSession.IsInternal.Returns(true);
            var query = new UpdatePrivateMessageAsReadQuery(userSession);

            //// Act
            messageHandler.Handle(query);

            //// Assert
            messageService.Received().UpdatePrivateAsRead(Arg.Any<UpdatePrivateMessageAsReadParam>());
        }

        [Test]
        public void HandleUpdatePrivateAsRead_Always_ReturnTrue()
        {
            //// Arrange
            userSession.IsInternal.Returns(true);
            var query = new UpdatePrivateMessageAsReadQuery(userSession);

            //// Act
            var act = messageHandler.Handle(query);

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void HandleMessageQuery_NullQuery_ThrowArgumentNullException()
        {
            // Arrange
            MessageQuery nullQuery = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => messageHandler.Handle(nullQuery));
        }

        [Test]
        public void HandleMessageQuery_NotNullQuery_CallGetMessageService()
        {
            // Arrange
            var messages = FakeDataHelper.CreateMany<Message>().ToList();
            FakeReturnGetMessagesService(messages);

            var query = new MessageQuery(
                userSession,
                1,
                DateTime.Today,
                DateTime.Today,
                false);

            // Act
            messageHandler.Handle(query);

            // Assert
            messageService
                .Received()
                .GetMessagesHistory(
                    Arg.Is<int>(cud => cud == query.CustId),
                    Arg.Is<string>(ps => ps == query.ParentSites),
                    Arg.Is<int>(st => st == query.SpecialType),
                    Arg.Is<int>(tm => tm == query.TotalMessage),
                    Arg.Is<DateTime>(st => st == query.StartDate),
                    Arg.Is<DateTime>(ed => ed == query.EndDate),
                    Arg.Is<bool>(it => it == query.IsTest),
                    Arg.Is<int>(mt => mt == query.MessageType));
        }

        [Test]
        public void HandleMessageQuery_NotNullQuery_ReturnMessagesViewModel()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            UrlHelperTestSetUp.SetUp();

            var messages = FakeDataHelper.CreateMany<Message>().ToList();
            FakeReturnGetMessagesService(messages);

            var query = new MessageQuery(
                userSession,
                1,
                DateTime.Today,
                DateTime.Today,
                false);
            var messageWrappers = new List<MessageWrapper>(messages.Select(m => new MessageWrapper(m, query.CurrentLang)));

            var expectedViewModel = new MessagesViewModel(
                messageWrappers,
                query.StartDate,
                query.EndDate,
                query.MessageType,
                query.RoleId);

            // Act
            var viewModel = messageHandler.Handle(query);

            // Assert
            Assert.IsTrue(compareLogic.Compare(expectedViewModel, viewModel).AreEqual);
        }

        private void FakeReturnGetMessagesService(List<Message> messages)
        {
            messageService.GetMessagesHistory(
                        Arg.Any<int>(),
                        Arg.Any<string>(),
                        Arg.Any<int>(),
                        Arg.Any<int>(),
                        Arg.Any<DateTime>(),
                        Arg.Any<DateTime>(),
                        Arg.Any<bool>(),
                        Arg.Any<int>())
                  .Returns(messages);
        }
    }
}