namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models.Announcement;
    using Params.Announcement;
    using Announcements.Models;
    using Announcements.Services;
    using Announcements.UnsynchronizedSetting.Specs;
    using Client;
    using Client.Requests;
    using Configurations;
    using Fanex.Caching;
    using Models;
    using Params;

    public interface AnnouncementFromApiService
    {
        IEnumerable<HotAnnouncement> GetPopupAnnouncements(GetHotAnnouncementsParams announcementParams);

        IEnumerable<HotAnnouncement> GetHeaderAnnouncements(GetHotAnnouncementsParams announcementParams);
    }

    public class AnnoucementFromApiServiceImpl : AnnouncementFromApiService
    {
        private const int CachedSecondAnnouncement = 30;
        private const string DefaultLanguage = "en-US";
        private const string PrefixId = "M7";

        private readonly AnnouncementClient announcementClient;
        private readonly SpecificationService specificationService;
        private readonly AnnouncementFromDatabaseService announcementFromDatabaseService;

        public AnnoucementFromApiServiceImpl(
            AnnouncementClient announcementClient,
            SpecificationService specificationService,
            AnnouncementFromDatabaseService announcementFromDatabaseService)
        {
            this.announcementClient = announcementClient;
            this.specificationService = specificationService;
            this.announcementFromDatabaseService = announcementFromDatabaseService;

            DoubleCheckActivedAnnouncements = new Dictionary<string, Func<Announcement, int, bool>>
            {
                // Implement double-check actived announcements by conditions here
                {"72973",  DoubleCheckActivedAnnnouncementContainsUnsyncedSetting}
            };
        }

        public Dictionary<string, Func<Announcement, int, bool>> DoubleCheckActivedAnnouncements { get; set; }

        public IEnumerable<HotAnnouncement> GetPopupAnnouncements(GetHotAnnouncementsParams announcementParams)
            => GetPopupAnnouncementsByUser(GetAllAnnouncementsToday(announcementParams.CustId), announcementParams);

        public IEnumerable<HotAnnouncement> GetHeaderAnnouncements(GetHotAnnouncementsParams announcementParams)
            => GetHeaderAnnouncementsByUser(GetAllAnnouncementsToday(announcementParams.CustId), announcementParams);

        private IEnumerable<Announcement> GetAllAnnouncementsToday(int custId)
        {
            var request = new QueryAnnouncementRequest
            {
                Environment = AppSettings.Environment,
                System = AppSettings.SiteGroup
            };

            var allAnnouncementsToday = ConfiguredCacheService.GetOrSet(
                 $"Alpha.Messages.HotAnnouncement_{AppSettings.SiteGroup}",
                 () => announcementClient.GetAnnouncements(request)
                 .OrderBy(i => i.Ordinal)
                 .ThenBy(i => i.CreatedDate),
                 new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachedSecondAnnouncement)));

            return allAnnouncementsToday.Where(announcement => 
                    !DoubleCheckActivedAnnouncements.ContainsKey(announcement.Code) || DoubleCheckActivedAnnouncements[announcement.Code](announcement, custId));
        }

        private IEnumerable<HotAnnouncement> GetPopupAnnouncementsByUser(IEnumerable<Announcement> announcements, GetHotAnnouncementsParams announcementParams)
        {
            foreach (var announcement in announcements.Where(i => i.AnnouncementType == AnnouncementType.Popup))
            {
                if (announcement.Specifications == null
                    || !announcement.Specifications.Any()
                    || specificationService.IsSatisfied(announcement.Specifications, announcementParams.SpecificationParamter))
                {
                    var message = GetMessageByLanguage(announcement.Contents, announcementParams.Language);

                    yield return new HotAnnouncement(PrefixId + announcement.Id.ToString(), message.Text, announcement.Code, message.Url, announcement.Specifications);
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
                    var message = GetMessageByLanguage(announcement.Contents, announcementParams.Language);

                    yield return new HotAnnouncement(PrefixId + announcement.Id.ToString(), message.Text, announcement.Code, message.Url);
                }
            }
        }

        private static MessageContent GetMessageByLanguage(IEnumerable<MessageContent> messages, string languageKey)
        {
            var message = messages.FirstOrDefault(i => i.LanguageKey == languageKey);
            if (message != null && string.IsNullOrEmpty(message.Text))
            {
                message.Text = GetMessageContentDefault(messages).Text;
            }

            if (message == null)
            {
                message = GetMessageContentDefault(messages);
            }

            return message;
        }

        private static MessageContent GetMessageContentDefault(IEnumerable<MessageContent> messages)
            => messages.FirstOrDefault(i => i.LanguageKey.Equals(DefaultLanguage));

        private bool DoubleCheckActivedAnnnouncementContainsUnsyncedSetting(Announcement announcement, int custId)
        {
            var existedUnsynchronizedSettingSpec = (UnsynchronizedSettingSpecification)announcement.Specifications
                .FirstOrDefault(spec => spec.GetType() == typeof(UnsynchronizedSettingSpecification));

            if (existedUnsynchronizedSettingSpec != null)
            {
                existedUnsynchronizedSettingSpec.ProductIds = announcementFromDatabaseService.GetUnconfirmSettingProductIds(custId);
                return existedUnsynchronizedSettingSpec.ProductIds.Any();
            }

            return false;
        }
    }
}
