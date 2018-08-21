namespace Alpha.Messages.Api.Tests.Controllers
{
    using Api.Controllers;
    using MediatR;
    using NSubstitute;
    using NUnit.Framework;
    using Requests;

    public class AnnouncementControllerTests
    {
        private IMediator mediator;
        private AnnouncementController announcementController;

        [SetUp]
        public void Setup()
        {
            mediator = Substitute.For<IMediator>();
            announcementController = new AnnouncementController(mediator);
        }

        [Test]
        public void GetAnnouncements_Always_CallsMediatorSendGetAnnouncementsQuery()
        {
            // Act
            announcementController.GetAnnouncements(new GetAnnouncementsQuery());

            // Assert
            mediator.Received().Send(Arg.Any<GetAnnouncementsQuery>());
        }
    }
}