namespace Alpha.Messages.Tests.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Models.Constants;
    using Alpha.Messages.Services;
    using Fanex.Data.Repository;
    using Messages.Criteria.Message;
    using Messages.Criteria.NewMessage;
    using Messages.Models.Message;
    using NSubstitute;
    using NUnit.Framework;
    using Params.Announcement;

    public class NewMessageServiceTests
    {
        private NewMessageService service;
        private ResourceManager resourceManager;
        private AnnouncementService announcementService;
        private IDynamicRepository dynamicRepository;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            resourceManager = Substitute.For<ResourceManager>();
            announcementService = Substitute.For<AnnouncementService>();

            resourceManager.GetString("Notice_1", Arg.Any<CultureInfo>()).Returns("notice1");
            resourceManager.GetString("Notice_2", Arg.Any<CultureInfo>()).Returns("notice2");
            resourceManager.GetString("Notice_3", Arg.Any<CultureInfo>()).Returns("fishing world");
            service = new NewMessageServiceImpl(dynamicRepository, resourceManager, announcementService);
        }

        [Test]
        public void MoveNewMessage_NotNewMessage_StopProcess()
        {
            //// Arrange
            dynamicRepository.Fetch<int>(Arg.Any<GetUnreadMessagesOnUserCriteria>()).Returns(Enumerable.Empty<int>());

            //// Act
            service.MoveNewMessage(123, "razor", UserLevel.Agent, false);

            //// Assert
            dynamicRepository.DidNotReceive().Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>());
            dynamicRepository.DidNotReceive().Fetch<bool>(Arg.Any<AssignMessageToUserCriteria>());
            dynamicRepository.DidNotReceive().Fetch<bool>(Arg.Any<UpdateMessageAsReadOnUserCritria>());
        }

        [Test]
        public void MoveNewMessage_IsUserAdmin_StopProcess()
        {
            //// Arrange            
            var isUserAdmin = true;

            //// Act
            service.MoveNewMessage(123, "razor", UserLevel.Agent, isUserAdmin);

            //// Assert
            dynamicRepository.DidNotReceive().Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>());
            dynamicRepository.DidNotReceive().Fetch<bool>(Arg.Any<AssignMessageToUserCriteria>());
            dynamicRepository.DidNotReceive().Fetch<bool>(Arg.Any<UpdateMessageAsReadOnUserCritria>());
        }

        [Test]
        public void MoveNewMessage_UserIsSuper_NotMoveBettingLimitAndPTMessage()
        {
            //// Arrange
            var bettingLimitAndPT = new List<int> { 1, 2 };
            dynamicRepository.Fetch<int>(Arg.Any<GetUnreadMessagesOnUserCriteria>()).Returns(bettingLimitAndPT);

            //// Act
            service.MoveNewMessage(123, "razor", UserLevel.Super, false);

            //// Assert
            dynamicRepository.DidNotReceive().Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>());
            dynamicRepository.DidNotReceive().Fetch<bool>(Arg.Any<AssignMessageToUserCriteria>());
            dynamicRepository.DidNotReceive().Fetch<bool>(Arg.Any<UpdateMessageAsReadOnUserCritria>());
        }

        [Test]
        public void MoveNewMessage_UserIsMaster_NotMoveBettingLimitAndPTMessage()
        {
            //// Arrange
            var bettingLimitAndPT = new List<int> { 1, 2 };
            dynamicRepository.Fetch<int>(Arg.Any<GetUnreadMessagesOnUserCriteria>()).Returns(bettingLimitAndPT);

            //// Act
            service.MoveNewMessage(123, "razor", UserLevel.Master, false);

            //// Assert
            dynamicRepository.DidNotReceive().Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>());
            dynamicRepository.DidNotReceive().Fetch<bool>(Arg.Any<AssignMessageToUserCriteria>());
            dynamicRepository.DidNotReceive().Fetch<bool>(Arg.Any<UpdateMessageAsReadOnUserCritria>());
        }

        [Test]
        public void MoveNewMessage_UserIsSuper_MoveFishWordMessage()
        {
            //// Arrange
            var newMessageType = new List<int> { 1, 2, 3 };
            dynamicRepository.Fetch<int>(Arg.Any<GetUnreadMessagesOnUserCriteria>()).Returns(newMessageType);
            dynamicRepository.Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>()).Returns(new List<int> { 1 });

            //// Act
            service.MoveNewMessage(123, "razor", UserLevel.Super, false);

            //// Assert
            dynamicRepository.Received().Fetch<int>(Arg.Is<InsertPersonalMessageCriteria>(i => i.ContentEN == "fishing world"));
            dynamicRepository.Received().Fetch<bool>(Arg.Any<AssignMessageToUserCriteria>());
            dynamicRepository.Received().Fetch<bool>(Arg.Any<UpdateMessageAsReadOnUserCritria>());
        }

        [Test]
        public void MoveNewMessage_UserIsMaster_MoveFishWordMessage()
        {
            //// Arrange
            var newMessageType = new List<int> { 1, 2, 3 };
            dynamicRepository.Fetch<int>(Arg.Any<GetUnreadMessagesOnUserCriteria>()).Returns(newMessageType);
            dynamicRepository.Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>()).Returns(new List<int> { 1 });

            //// Act
            service.MoveNewMessage(123, "razor", UserLevel.Master, false);

            //// Assert
            dynamicRepository.Received().Fetch<int>(Arg.Is<InsertPersonalMessageCriteria>(i => i.ContentEN == "fishing world"));
            dynamicRepository.Received().Fetch<bool>(Arg.Any<AssignMessageToUserCriteria>());
            dynamicRepository.Received().Fetch<bool>(Arg.Any<UpdateMessageAsReadOnUserCritria>());
        }

        [Test]
        public void MoveNewMessage_UserIsAgent_MoveFullMessageType()
        {
            //// Arrange
            var newMessageType = new List<int> { 1, 2, 3 };
            dynamicRepository.Fetch<int>(Arg.Any<GetUnreadMessagesOnUserCriteria>()).Returns(newMessageType);
            dynamicRepository.Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>()).Returns(new List<int> { 1 });

            //// Act
            service.MoveNewMessage(123, "razor", UserLevel.Agent, false);

            //// Assert
            dynamicRepository
                .Received()
                .Fetch<int>(Arg.Any<InsertPersonalMessageCriteria>());
            dynamicRepository.Received().Fetch<bool>(Arg.Any<AssignMessageToUserCriteria>());
            dynamicRepository.Received().Fetch<bool>(Arg.Any<UpdateMessageAsReadOnUserCritria>());
        }

        [Test]
        public void GetNumberOfUnreadMessage_Always_CallDynamicRepository()
        {
            //// Arrange
            dynamicRepository.Get<NumberOfUnreadMessage>(Arg.Any<GetNumberOfUnreadMessageCriteria>()).Returns(new NumberOfUnreadMessage());

            //// Act
            service.GetNumberOfUnreadMessage(new BaseAnnouncementsParams());

            //// Assert
            dynamicRepository.Received().Get<NumberOfUnreadMessage>(Arg.Any<GetNumberOfUnreadMessageCriteria>());
            announcementService.Received().GetNumberOfNewAnnouncements(Arg.Any<BaseAnnouncementsParams>());
        }
    }
}
