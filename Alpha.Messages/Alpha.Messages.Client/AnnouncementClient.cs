namespace Alpha.Messages.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Announcements.Models;
    using Configurations;
    using Fanex.Logging;
    using Helpers;
    using Newtonsoft.Json;
    using Requests;
    using RestSharp;

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

            Logger.Log.Info(JsonConvert.SerializeObject(request));

            restRequest.AddJsonBody(request);
            try
            {
                IRestResponse response = restClient.Execute(restRequest);
                Logger.Log.Info(response.StatusCode.ToString());
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Announcement>>(
                        response.Content,
                        new SpecificationConverter());
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
            }
            // write log
            return Enumerable.Empty<Announcement>();
        }
    }
}