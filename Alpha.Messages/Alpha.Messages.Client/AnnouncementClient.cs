namespace Alpha.Messages.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Announcements.Models;
    using Configurations;
    using Helpers;
    using Newtonsoft.Json;
    using Requests;
    using RestSharp;

    public interface AnnouncementClient
    {
        IEnumerable<Announcement> GetAnnouncements(QueryAnnouncementRequest request);
    }

    public class AnnouncementClientImpl : AnnouncementClient
    {
        private readonly IRestClient restClient;

        public AnnouncementClientImpl() : this(null)
        {

        }

        public AnnouncementClientImpl(IRestClient restClient)
        {
            this.restClient = restClient ?? new RestClient(new Uri(AppSettings.AnnouncementApi))
            {
                Authenticator = new HmacAuthenticator()
            };
        }

        public IEnumerable<Announcement> GetAnnouncements(QueryAnnouncementRequest request)
        {
            var restRequest = new RestRequest("AnnouncementApi/GetAnnouncements", Method.POST);
            restRequest.AddQueryParameter("environment", request.Environment);
            restRequest.AddQueryParameter("system", request.System);
            IRestResponse response = restClient.Execute(restRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Announcement>>(
                    response.Content,
                    new SpecificationConverter());
            }

            // write log
            return Enumerable.Empty<Announcement>();
        }
    }
}