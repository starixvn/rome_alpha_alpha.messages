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
        IEnumerable<HotAnnouncement> GetPopupAnnouncements(GetHotAnnouncementsParams announcementParams);

        IEnumerable<HotAnnouncement> GetHeaderAnnouncements(GetHotAnnouncementsParams announcementParams);
    }

    public class AnnoucemtnFromApiServiceImpl : AnnouncementFromApiService
    {
        private const int CachedSecondAnnouncement = 10;
        private readonly AnnouncementClient announcementClient;
        private readonly SpecificationService specificationService;


        public AnnoucemtnFromApiServiceImpl(
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
                    yield return new HotAnnouncement
                    {
                        Code = announcement.Code,
                        Content = GetMessageContentByLanguage(announcement.Contents, announcementParams.Language),
                        Id = announcement.Id
                    };
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
                    yield return new HotAnnouncement
                    {
                        Code = announcement.Code,
                        Content = GetMessageContentByLanguage(announcement.Contents, announcementParams.Language),
                        Id = announcement.Id
                    };
                }
            }
        }

        private static string GetMessageContentByLanguage(IEnumerable<MessageContent> messages, string languageKey)
        {
            var findingContent = messages.First(i => i.LanguageKey == languageKey);
            var messageContent = findingContent != null ? findingContent.Text : GetMessageContentDefault(messages);

            return messageContent;
        }

        private static string GetMessageContentDefault(IEnumerable<MessageContent> messages)
        {
            var contentDefault = messages.First(i => i.LanguageKey.Equals(Language.Default));

            return contentDefault != null ? contentDefault.Text : string.Empty;
        }
    }
}
