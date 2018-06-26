namespace Alpha.Messages.Site.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using KellermanSoftware.CompareNetObjects;
    using Messages.Constants;
    using Messsages.Site.Requests.PortalMessage;
    using Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Ploeh.AutoFixture;
    using Services;
    using Site.Handlers;
    using Site.ViewModels.PortalMessage;
    using Sunplus.Agent.Model.Entities;

    [TestFixture]
    public class PortalMessageHandlerTest
    {
        private PortalMessageService messageService;
        private PortalMessageHandler messageHandler;
        private IUserSession userSession;
        private Fixture fixture;

        [SetUp]
        public void SetUp()
        {
            messageService = Substitute.For<PortalMessageService>();
            messageHandler = new PortalMessageHandler(messageService);

            userSession = Substitute.For<IUserSession>();
            userSession.Language.Returns(Language.English);
            userSession.CustId.Returns(263);
            userSession.RoleId.Returns(RoleId.Agent);
            userSession.GetProperty<DateTime>(SessionKey.CustomerDatelimit).Returns(DateTime.MinValue);
            fixture = new Fixture();
        }        
        

        [Test]
        public void Handle_NotNullQuery_CallGetPortalMessages()
        {
            // Arrange
            var messages = FakeDataHelper.CreateMany<PortalMessage>().ToList();
            FakeReturnGetMessagesService(messages);

            var query = new PortalMessageQuery(
                userSession,
                1,
                false);

            // Act
            messageHandler.Handle(query);

            // Assert
            messageService
                .Received()
                .GetPortalMessages(
                    Arg.Is<int>(cud => cud == query.CustId),
                    Arg.Is<string>(ps => ps == query.ParentSites),
                    Arg.Is<int>(tm => tm == query.TotalMessage),
                    Arg.Is<bool>(it => it == query.IsTest));
        }

        [Test]
        public void Handle_NotNullQuery_ReturnMessagesViewModel()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            UrlHelperTestSetUp.SetUp();

            var messages = FakeDataHelper.CreateMany<PortalMessage>().ToList();
            FakeReturnGetMessagesService(messages);

            var query = new PortalMessageQuery(
                userSession,
                1,
                false);
            var messageWrappers = new List<PortalMessageWrapper>(messages.Select(m => new PortalMessageWrapper(m, query.CurrentLang)));

            var expectedViewModel = new PortalMessagesViewModel(
                messageWrappers);

            // Act
            var viewModel = messageHandler.Handle(query);

            // Assert
            Assert.IsTrue(compareLogic.Compare(expectedViewModel, viewModel).AreEqual);
        }

        private void FakeReturnGetMessagesService(List<PortalMessage> messages)
        {
            messageService.GetPortalMessages(
                        Arg.Any<int>(),
                        Arg.Any<string>(),
                        Arg.Any<int>(),
                        Arg.Any<bool>())
                  .Returns(messages);
        }
    }
}