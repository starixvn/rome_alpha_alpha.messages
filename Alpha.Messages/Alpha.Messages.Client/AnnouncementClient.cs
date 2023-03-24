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
    using Sunplus.Agent.Utilities;

    public interface AnnouncementClient
    {
        IEnumerable<Announcement> GetAnnouncementsHistory(AnnouncementHistoryRequest request);
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

        public IEnumerable<Announcement> GetAnnouncementsHistory(AnnouncementHistoryRequest request)
        {
            var restRequest = new RestRequest("AnnouncementApi/GetAnnouncementsHistory", Method.POST);
            restRequest.AddJsonBody(request);

            IRestResponse response = restClient.Execute(restRequest);
            Logs.WriteInfo(response.Content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Announcement>>(
                    response.Content,
                    new SpecificationConverter());
            }

            return Enumerable.Empty<Announcement>();
        }
    }
}