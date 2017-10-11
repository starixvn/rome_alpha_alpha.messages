namespace Alpha.Messages.Client.Tests.Helpers
{
    using System.Collections.Specialized;
    using System.Linq;
    using Alpha.Messages.Client.Configurations;
    using Alpha.Messages.Client.Helpers;
    using NUnit.Framework;
    using RestSharp;
    using RestSharp.Authenticators;

    public class HmacAuthenticatorTests
    {
        private IAuthenticator hmacAuthenticator;
        private IRestClient client;
        private IRestRequest request;

        [SetUp]
        public void SetUp()
        {
            AppSettings.AppSettingValues = new NameValueCollection();
            client = new RestClient(@"http://ha.nexdev.net:7426/Local01/api/");
            request = new RestRequest("AnnouncementApi/GetAnnouncements", Method.POST);

            hmacAuthenticator = new HmacAuthenticator();
        }

        [Test]
        public void Authenticate_HasIPLocal_ReturnExpectedAuthorization()
        {
            //// Arrange

            //// Act
            hmacAuthenticator.Authenticate(client, request);

            //// Assert
            Assert.IsNotNull(request.Parameters.FirstOrDefault(i => i.Name == "Authorization"));
        }
    }
}
