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
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models.Announcement;
    using Params.Announcement;

    public interface AnnouncementService
    {
        int GetNumberOfNewAnnouncements(BaseAnnouncementsParams announcementParams);
        void UpdateLastSeenAnnouncement(int custId, int messageType, bool isAdminUser, int subAccId);
        IEnumerable<AlphaAnnouncement> GetAnnouncementsHistory(AnnouncementsHistoryParams announcementParams);

        AlphaAnnouncement GetPopUpAnnouncements(PopupAnnouncementParams announcementParams);

        LastSeenAnnouncement GetLastSeenAnnouncement(int custId, int subAccId);
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
                {"72973",  IsUnsyncedProductSetting}
            };
        }

        public Dictionary<string, Func<Announcement, int, bool>> DoubleCheckActivedAnnouncements { get; set; }

        public void UpdateLastSeenAnnouncement(int custId, int messageType, bool isAdminUser, int subAccId)
        {
            if (!isAdminUser)
            {
                dynamicRepository.Execute(new UpdateLasteSeenAnnouncementCriteria(custId, messageType, subAccId));
            }
        }

        public int GetNumberOfNewAnnouncements(BaseAnnouncementsParams announcementParams)
        {
            var lastSeenAnnouncement = GetLastSeenAnnouncement(announcementParams.CustId, announcementParams.SubAccId);

            var fromDate = GetAvailableLastSeenDate(DateTime.MinValue, lastSeenAnnouncement.LastSeenSystem);

            var newAnnouncements = GetNewAnnouncements(fromDate);

            var announcements = GetAnnouncementsByUser(newAnnouncements, announcementParams, lastSeenAnnouncement.LastSeenSystem);

            return announcements.Count(i => i.ReleasedDate >= lastSeenAnnouncement.LastSeenSystem);
        }

        public IEnumerable<AlphaAnnouncement> GetAnnouncementsHistory(AnnouncementsHistoryParams announcementParams)
        {
            var lastSeenAnnouncement = GetLastSeenAnnouncement(announcementParams.CustId, announcementParams.SubAccId);

            announcementParams.FromDate = GetAvailableLastSeenDate(announcementParams.FromDate, lastSeenAnnouncement.LastSeenSystem);

            var announcementsHistory = GetAnnouncementsHistory(announcementParams.FromDate, announcementParams.ToDate);
            var announcements = GetAnnouncementsByUser(announcementsHistory, announcementParams, lastSeenAnnouncement.LastSeenSystem);

            UpdateLastSeenAnnouncement(announcementParams.CustId, MessageType.System, announcementParams.IsAdminUser, announcementParams.SubAccId);

            return announcements.OrderByDescending(i => i.ReleasedDate);
        }

        public AlphaAnnouncement GetPopUpAnnouncements(PopupAnnouncementParams announcementParams)
        {
            if (announcementParams.IsShowedPopup)
            {
                return null;
            }

            var announcementsHistory = GetAvailablePopUpAnnouncements();
            var announcements = GetAnnouncementsByUser(announcementsHistory, announcementParams, DateTime.Now);

            return announcements.OrderByDescending(i => i.ReleasedDate).FirstOrDefault();
        }

        public LastSeenAnnouncement GetLastSeenAnnouncement(int custId, int subAccId)
        {
            var lastSeen = dynamicRepository.Get<LastSeenAnnouncement>(new GetLastSeenAnnouncementCriteria(custId, subAccId));

            return lastSeen ?? new LastSeenAnnouncement(custId, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

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

        private IEnumerable<Announcement> GetNewAnnouncements(DateTime fromDate)
        {
            var newAnnouncements = ConfiguredCacheService.GetOrSet(
                 $"Alpha.Messages.Announcement_{fromDate}_{AppSettings.SiteGroup}",
                 () => GetAnnouncementsHistory(fromDate, DateTime.Now),
                 new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachedSecondAnnouncement)));

            return newAnnouncements;
        }

        private IEnumerable<Announcement> GetAvailablePopUpAnnouncements()
        {
            var request = new AnnouncementHistoryRequest(DateTime.Today.AddDays(-30), DateTime.Now, AppSettings.Environment, AppSettings.SiteGroup);
            var availablePopupAnnouncements = ConfiguredCacheService.GetOrSet(
                $"Alpha.Messages.PopupAnnouncement_{AppSettings.SiteGroup}",
                () => announcementClient.GetAnnouncementsHistory(request)
                .Where(i => DateTime.Now <= i.ExpiredDate && i.AnnouncementType == AnnouncementType.EmergencyPopup),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachedSecondAnnouncement)));

            return availablePopupAnnouncements;
        }

        private IEnumerable<Announcement> GetAnnouncementsHistory(DateTime fromDate, DateTime toDate)
        {
            var request = new AnnouncementHistoryRequest(fromDate, toDate, AppSettings.Environment, AppSettings.SiteGroup);

            return announcementClient.GetAnnouncementsHistory(request)
                .Where(i => i.ReleasedDate <= DateTime.Now)
                .OrderBy(i => i.ReleasedDate);
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
                    || specificationService.IsSatisfied(announcement.Specifications, announcementParams.SpecificationParamter)
                    || IsUnsyncedProductSetting(announcement, announcementParams.CustId))
                {
                    var messageContent = GetMessageContentByLanguage(announcement.Contents, announcementParams.Language);

                    yield return new AlphaAnnouncement(
                        announcement.Id,
                        messageContent,
                        announcement.Code,
                        announcement.ReleasedDate,
                        lastSeenAnnouncement,
                        announcement.Specifications);
                }
            }
        }

        private static string GetMessageContentByLanguage(IEnumerable<MessageContent> messages, string languageKey)
        {
            var message = messages.FirstOrDefault(i => i.LanguageKey.Equals(languageKey) && !string.IsNullOrEmpty(i.Text));

            if (message == null)
            {
                message = messages.FirstOrDefault(i => i.LanguageKey.Equals(DefaultLanguage) && !string.IsNullOrEmpty(i.Text));
            }

            return message == null ? string.Empty : message.Text;
        }


        private bool IsUnsyncedProductSetting(Announcement announcement, int custId)
        {
            if (DoubleCheckActivedAnnouncements.ContainsKey(announcement.Code))
            {
                var spec = announcement.Specifications.FirstOrDefault(s => s.GetType() == typeof(UnsynchronizedSettingSpecification));

                if (spec != null)
                {
                    var unsynchronizedSetting = (UnsynchronizedSettingSpecification)spec;
                    unsynchronizedSetting.ProductIds = productService.GetUnconfirmSettingProductIds(custId);

                    return unsynchronizedSetting.ProductIds.Any();
                }
            }

            return true;
        }
    }
}
