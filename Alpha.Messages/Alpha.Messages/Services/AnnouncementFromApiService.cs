using Alpha.Announcements.Models;
using Alpha.Announcements.Services;
using Alpha.Messages.Client;

using Alpha.Messages.Client.Requests;
using Alpha.Messages.Configurations;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Fanex.Caching;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Messages.Services
{
    public interface AnnouncementFromApiService
    {
        IEnumerable<HotAnnouncement> GetPopupAnnouncements(GetHotAnnouncementsParams announcementParams);

        IEnumerable<HotAnnouncement> GetHeaderAnnouncements(GetHotAnnouncementsParams announcementParams);
    }

    public class AnnoucementFromApiServiceImpl : AnnouncementFromApiService
    {
        private const int CachedSecondAnnouncement = 10;
        private const string DefaultLanguage = "en-US";

        private readonly AnnouncementClient announcementClient;
        private readonly SpecificationService specificationService;


        public AnnoucementFromApiServiceImpl(
            AnnouncementClient announcementClient,
            SpecificationService specificationSerivce)
        {
            this.announcementClient = announcementClient;
            this.specificationService = specificationSerivce;
        }

        public IEnumerable<HotAnnouncement> GetPopupAnnouncements(GetHotAnnouncementsParams announcementParams)
        {
            var allNouncementsToday = GetAllAnnouncementToday();

            var userAnnouncements = GetPopuAnnouncementsByUser(allNouncementsToday, announcementParams);

            return userAnnouncements;
        }

        public IEnumerable<HotAnnouncement> GetHeaderAnnouncements(GetHotAnnouncementsParams announcementParams)
        {
            var allNouncementsToday = GetAllAnnouncementToday();

            var userAnnouncements = GetHeaderAnnouncementsByUser(allNouncementsToday, announcementParams);

            return userAnnouncements;
        }

        private IEnumerable<Announcement> GetAllAnnouncementToday()
        {
            var request = new QueryAnnouncementRequest
            {
                Environment = AppSettings.Environment,
                System = AppSettings.SiteGroup
            };

            var allNouncementsToday = ConfiguredCacheService.GetOrSet(
                $"Alpha.Messages.HotAnnouncement_{AppSettings.SiteGroup}",
                () => announcementClient.GetAnnouncements(request),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachedSecondAnnouncement)));

            return allNouncementsToday;
        }

        private IEnumerable<HotAnnouncement> GetPopuAnnouncementsByUser(IEnumerable<Announcement> announcements, GetHotAnnouncementsParams announcementParams)
        {
            foreach (var announcement in announcements.Where(i => i.AnnouncementType == AnnouncementType.Popup))
            {
                if (announcement.Specifications == null
                    || !announcement.Specifications.Any()
                    || specificationService.IsSatisfied(announcement.Specifications, announcementParams.SpecificationParamter))
                {
                    var message = GetMessageContentByLanguage(announcement.Contents, announcementParams.Language);

                    yield return new HotAnnouncement(announcement.Id, message.Text, announcement.Code, message.Url);
                }
            }
        }

        private IEnumerable<HotAnnouncement> GetHeaderAnnouncementsByUser(IEnumerable<Announcement> announcements, GetHotAnnouncementsParams announcementParams)
        {
            foreach (var announcement in announcements.Where(i => i.AnnouncementType == AnnouncementType.HeaderPopup))
            {
                if (announcement.Specifications == null
                    || !announcement.Specifications.Any()
                    || specificationService.IsSatisfied(announcement.Specifications, announcementParams.SpecificationParamter))
                {
                    var message = GetMessageContentByLanguage(announcement.Contents, announcementParams.Language);

                    yield return new HotAnnouncement(announcement.Id, message.Text, announcement.Code, message.Url);
                }
            }
        }

        private static MessageContent GetMessageContentByLanguage(IEnumerable<MessageContent> messages, string languageKey)
        {
            var findingContent = messages.FirstOrDefault(i => i.LanguageKey == languageKey);
            var messageContent = findingContent ?? GetMessageContentDefault(messages);

            return messageContent;
        }

        private static MessageContent GetMessageContentDefault(IEnumerable<MessageContent> messages)
        {
            var contentDefault = messages.FirstOrDefault(i => i.LanguageKey.Equals(DefaultLanguage));

            return contentDefault;// != null ? contentDefault : new MessageContent();
        }
    }
}
