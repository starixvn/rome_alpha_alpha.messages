using Alpha.Announcements.Models;
using Alpha.Announcements.Services;
using Alpha.Messages.Client;

using Alpha.Messages.Client.Requests;
using Alpha.Messages.Configurations;
using Alpha.Messages.Constants;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Fanex.Caching;
using Sunplus.Agent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Messages.Services
{
    public interface AnnouncementFromApiService
    {
        IEnumerable<HotAnnouncement> GetHotAnnouncements(GetHotAnnouncementsParams getParam);
    }

    public class AnnoucemtnFromApiServiceImpl : AnnouncementFromApiService
    {
        private readonly AnnouncementClient announcementClient;
        private readonly SpecificationService specificationSerivce;


        public AnnoucemtnFromApiServiceImpl(
            AnnouncementClient announcementClient,
            SpecificationService specificationSerivce)
        {
            this.announcementClient = announcementClient;
            this.specificationSerivce = specificationSerivce;
        }

        public IEnumerable<HotAnnouncement> GetHotAnnouncements(GetHotAnnouncementsParams announcementParams)
        {
            var request = new QueryAnnouncementRequest
            {
                Date = DateTime.Now.Date,
                Environment = AppSettings.Environment,
                System = AppSettings.SiteGroup
            };

            var allNouncementsToday = ConfiguredCacheService.GetOrSet(
                $"Alpha.Messages.HotAnnouncement_{AppSettings.SiteGroup}",
                () => announcementClient.GetAnnouncements(request),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(10)));

            var userAnnouncements = GetAnnouncementsByUser(allNouncementsToday, announcementParams);

            return userAnnouncements;
        }

        private IEnumerable<HotAnnouncement> GetAnnouncementsByUser(IEnumerable<Announcement> announcements, GetHotAnnouncementsParams announcementParams)
        {
            foreach (var announcement in announcements)
            {
                if (announcement.Specifications == null
                    || !announcement.Specifications.Any()
                    || specificationSerivce.IsSatisfied(announcement.Specifications, announcementParams.SpecificationParamter))
                {
                    yield return new HotAnnouncement
                    {
                        Code = announcement.Code,
                        Content = GetMessageContentByLanguage(announcement.Contents, announcementParams),
                        Id = announcement.Id,
                        Languague = announcementParams.Language,
                        CookieId = Encryption.PearsonEncode(announcementParams.UserId.ToString() + announcementParams.SubAccId.ToString())
                    };
                }
            }
        }

        private static string GetMessageContentByLanguage(IEnumerable<MessageContent> messages, GetHotAnnouncementsParams announcementParams)
        {
            var findingContent = messages.First(i => i.LanguageKey == announcementParams.Language);
            var messageContent = findingContent != null ? findingContent.Text : GetMessageContentDefault(messages);

            return RewriteMessageContent(messageContent, announcementParams);
        }

        private static string GetMessageContentDefault(IEnumerable<MessageContent> messages)
        {
            var contentDefault = messages.First(i => i.LanguageKey.Equals(Language.Default));

            return contentDefault != null ? contentDefault.Text : string.Empty;
        }

        private static string RewriteMessageContent(string messageContent, GetHotAnnouncementsParams announcementParams)
        {
            var content = messageContent;
            foreach(var item in announcementParams.Replacement)
            {
                content = messageContent.Replace(item.Key, item.Value);
            }

            return content;
        }
    }
}
