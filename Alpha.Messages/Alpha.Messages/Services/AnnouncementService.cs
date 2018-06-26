namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Announcements.Models;
    using Announcements.Services;
    using Announcements.UnsynchronizedSetting.Specs;
    using Client;
    using Client.Requests;
    using Configurations;
    using Constants;
    using Criteria.Announcement;
    using Criteria.Message;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models.Announcement;
    using Params.Announcement;

    public interface AnnouncementService
    {
        int GetNumberOfNewAnnouncements(NumberOfNewAnnouncementsParams announcementParams);
        void UpdateLastSeenAnnouncement(int userId, int custId, int messageType, bool isAdminUser);
        IEnumerable<AlphaAnnouncement> GetAnnouncementsHistory(AnnouncementsHistoryParams announcementParams);
        LastSeenAnnouncement GetLastSeenAnnouncement(int custId);
    }

    public class AnnouncementServiceImpl : AnnouncementService
    {
        private const int CachedSecondAnnouncement = 30;
        private const string DefaultLanguage = "en-US";

        private readonly AnnouncementClient announcementClient;
        private readonly SpecificationService specificationService;
        private readonly IDynamicRepository dynamicRepository;
        private readonly ProductService productService;

        public AnnouncementServiceImpl(
            AnnouncementClient announcementClient,
            SpecificationService specificationService,
            ProductService productService,
            IDynamicRepository dynamicRepository)
        {
            this.announcementClient = announcementClient;
            this.specificationService = specificationService;
            this.dynamicRepository = dynamicRepository;
            this.productService = productService;

            DoubleCheckActivedAnnouncements = new Dictionary<string, Func<Announcement, int, bool>>
            {
                // Implement double-check actived announcements by conditions here
                {"72973",  DoubleCheckActivedAnnnouncementContainsUnsyncedSetting}
            };
        }

        public Dictionary<string, Func<Announcement, int, bool>> DoubleCheckActivedAnnouncements { get; set; }

        public void UpdateLastSeenAnnouncement(int userId, int custId, int messageType, bool isAdminUser)
        {
            if (!isAdminUser)
            {
                dynamicRepository.Execute(new UpdateLasteSeenAnnouncementCriteria(userId, custId, messageType));
            }
        }

        public int GetNumberOfNewAnnouncements(NumberOfNewAnnouncementsParams announcementParams)
        {
            var announcementToday = GetAllAnnouncementsToday(announcementParams.CustId);
            var lastSeenAnnouncement = GetLastSeenAnnouncement(announcementParams.CustId);

            var announcements = GetAnnouncementsByUser(announcementToday, announcementParams, lastSeenAnnouncement.LastSeenSystem);

            return announcements.Count(i => i.ReleasedDate >= lastSeenAnnouncement.LastSeenSystem);
        }

        public IEnumerable<AlphaAnnouncement> GetAnnouncementsHistory(AnnouncementsHistoryParams announcementParams)
        {
            var lastSeenAnnouncement = GetLastSeenAnnouncement(announcementParams.CustId);
            announcementParams.FromDate = GetAvailableLastSeenDate(announcementParams.FromDate, lastSeenAnnouncement.LastSeenSystem);
            var announcementsHistory = announcementClient.GetAnnouncementsHistory(
                new AnnouncementHistoryRequest(
                    announcementParams.FromDate,
                    announcementParams.ToDate,
                    AppSettings.Environment,
                    AppSettings.SiteGroup));
            var announcements = GetAnnouncementsByUser(announcementsHistory, announcementParams, lastSeenAnnouncement.LastSeenSystem);

            UpdateLastSeenAnnouncement(announcementParams.UserId, announcementParams.CustId, MessageType.System, announcementParams.IsAdminUser);

            return announcements.OrderByDescending(i => i.ReleasedDate);
        }

        public LastSeenAnnouncement GetLastSeenAnnouncement(int custId)
            => dynamicRepository.Get<LastSeenAnnouncement>(new GetLastSeenAnnouncementCriteria(custId));

        private static DateTime GetAvailableLastSeenDate(DateTime fromDate, DateTime lastSeenDate)
        {
            if (fromDate == DateTime.MinValue)
            {
                var threeDayAgo = DateTime.Now.AddDays(-3);
                var sixMonthAgo = DateTime.Now.AddMonths(-6);
                if (threeDayAgo < lastSeenDate)
                {
                    return threeDayAgo;
                }
                else if (lastSeenDate < threeDayAgo && lastSeenDate > sixMonthAgo)
                {
                    return lastSeenDate;
                }
                else
                {
                    return sixMonthAgo;
                }
            }

            return fromDate;
        }

        private IEnumerable<Announcement> GetAllAnnouncementsToday(int custId)
        {
            var request = new AnnouncementTodayRequest(AppSettings.Environment, AppSettings.SiteGroup);

            var allAnnouncementsToday = ConfiguredCacheService.GetOrSet(
                 $"Alpha.Messages.HotAnnouncement_{AppSettings.SiteGroup}",
                 () => announcementClient.GetAnnouncementsToday(request)
                 .OrderBy(i => i.Ordinal)
                 .ThenBy(i => i.CreatedDate),
                 new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachedSecondAnnouncement)));

            return allAnnouncementsToday.Where(announcement =>
                    !DoubleCheckActivedAnnouncements.ContainsKey(announcement.Code) || DoubleCheckActivedAnnouncements[announcement.Code](announcement, custId));
        }

        private IEnumerable<AlphaAnnouncement> GetAnnouncementsByUser(
            IEnumerable<Announcement> announcementToday,
            BaseAnnouncementsParams announcementParams,
            DateTime lastSeenAnnouncement)
        {
            foreach (var announcement in announcementToday)
            {
                if (announcement.Specifications == null
                    || !announcement.Specifications.Any()
                    || specificationService.IsSatisfied(announcement.Specifications, announcementParams.SpecificationParamter))
                {
                    var message = GetMessageByLanguage(announcement.Contents, announcementParams.Language);

                    yield return new AlphaAnnouncement(announcement.Id, message.Text, announcement.Code, "", announcement.ReleasedDate, lastSeenAnnouncement);
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
                existedUnsynchronizedSettingSpec.ProductIds = productService.GetUnconfirmSettingProductIds(custId);
                return existedUnsynchronizedSettingSpec.ProductIds.Any();
            }

            return false;
        }
    }
}
