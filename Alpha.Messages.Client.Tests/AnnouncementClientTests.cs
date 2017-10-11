namespace Alpha.Messages.Client.Tests
{
    using Alpha.Messages.Client.Configurations;
    using Alpha.Messages.Client.Requests;
    using NSubstitute;
    using NUnit.Framework;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class AnnouncementClientTests
    {
        private IRestClient restClient;
        private AnnouncementClientImpl client;

        [SetUp]
        public void SetUp()
        {
            this.restClient = Substitute.For<IRestClient>();
            client = new AnnouncementClientImpl(restClient);
        }

        [Test]        
        public void AnnouncementClientImpl_Always_NoThrowException()
        {
            //// Arrange
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["Announcement:ApiUrl"] = "http://ha.nexdev.net:7426/Local01/api/"
            };
            
            //// Act && Assert
            Assert.DoesNotThrow(() => new AnnouncementClientImpl());
        }

        [Test]
        public void GetAnnouncements_Always_CallApi()
        {
            //// Arrange
            var request = new QueryAnnouncementRequest();
            restClient.Execute(Arg.Any<IRestRequest>()).Returns(new RestResponse());

            //// Act
            var act = client.GetAnnouncements(request);

            //// Assert
            restClient.Received().Execute(Arg.Any<IRestRequest>());
        }

        [Test]
        public void GetAnnouncements_ApiError_ReturnEmptyList()
        {
            //// Arrange
            var request = new QueryAnnouncementRequest();
            restClient.Execute(Arg.Any<IRestRequest>()).Returns(new RestResponse() { StatusCode = HttpStatusCode.BadRequest });

            //// Act
            var act = client.GetAnnouncements(request);

            //// Assert
            Assert.False(act.Any());
        }

        [Test]
        public void GetAnnouncements_ApiOK_NotThrowException()
        {
            //// Arrange
            var request = new QueryAnnouncementRequest();
            restClient.Execute(Arg.Any<IRestRequest>()).Returns(
                new RestResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = FixtureAnnouncementContent.Content
                });

            //// Act
            var act = client.GetAnnouncements(request);

            //// Assert
            Assert.Pass();

        }
    }
}
