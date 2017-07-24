using Alpha.Messages.AppResources;
using Alpha.Messages.Criteria;
using Alpha.Messages.Models;
using Alpha.Messages.Models.Enumerations;
using Alpha.Messages.Params;
using Fanex.Common;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Services
{
    public interface AnnouncementFromDatabaseService
    {
        IEnumerable<HotAnnouncement> GetHotAnnouncements(GetHotAnnouncementsParams getParams);
    }

    public class AnnouncementFromDatabaseServiceImpl : AnnouncementFromDatabaseService
    {
        private const int FishingWorldNotice = 3;
        private static string[] splitChars = { "_" };

        private readonly IDynamicRepository dynamicRepository;
        private readonly ResourceManager resourceManager;

        public AnnouncementFromDatabaseServiceImpl(
            IDynamicRepository dynamicRepository,
            ResourceManager resourceManager)
        {
            this.dynamicRepository = dynamicRepository;
            this.resourceManager = resourceManager;
        }

        public IEnumerable<HotAnnouncement> GetHotAnnouncements(GetHotAnnouncementsParams parameters)
        {
            var noticesAreNotViewed = GetNoticesAreNotViewed(parameters);

            var announcements = GetNoticesAreViewing(noticesAreNotViewed, parameters);

            return announcements;
        }

        private IEnumerable<string> GetNoticesAreNotViewed(GetHotAnnouncementsParams parameters)
        {
            var noticesAreNoViewed = Enumerable.Empty<string>();
            if (!parameters.IsUserAdmin)
            {
                noticesAreNoViewed = dynamicRepository
                    .Fetch<byte>(new GetNoticeListIsNoViewedCriteria(parameters.CustId))
                    .Select(i => i.ToString());

                if (IsNotAgentLevel(parameters.RoleId))
                {
                    noticesAreNoViewed = noticesAreNoViewed.Contains(FishingWorldNotice.ToString()) ?
                        new List<string> { FishingWorldNotice.ToString() } :
                        new List<string>();
                }
            }

            return noticesAreNoViewed;
        }

        private static bool IsNotAgentLevel(byte roleId) => Enumeration.FromValue<UserLevel>(roleId).IsNotAgentLevel();

        private IEnumerable<HotAnnouncement> GetNoticesAreViewing(IEnumerable<string> NoticesAreNoViewed, GetHotAnnouncementsParams announcementParams)
        {
            var correctType = announcementParams.NoticeTypes.Where(i => IsShowNotice(i, NoticesAreNoViewed));

            foreach (var type in correctType)
            {
                var resourceKey = "Notice_" + type;
                InsertPersonalNotice(resourceKey, announcementParams);
                UpdateNoticeStatusIsViewed(announcementParams.CustId, type);

                yield return new HotAnnouncement
                {
                    Content = Resources.ResourceManager.GetString(resourceKey)
                };
            }
        }

        private static bool IsShowNotice(string noticeType, IEnumerable<string> NoticesAreNoViewed)
        {
            return noticeType.Split(splitChars, StringSplitOptions.RemoveEmptyEntries)
                .Any(n => NoticesAreNoViewed.Contains(n));
        }

        private void InsertPersonalNotice(string key, GetHotAnnouncementsParams getParams)
        {
            var rawData = dynamicRepository.Fetch<int>(new InsertPersonalMessageCriteria(key, resourceManager));
            if (rawData.Any())
            {
                var firstMessage = rawData.FirstOrDefault();
                dynamicRepository.Fetch<bool>(new InsertPersonalMessageTargetCriteria
                {
                    MessageId = firstMessage,
                    TargetId = getParams.UserId,
                    TargetName = getParams.UserName,
                    IsRead = false,
                    BetTransId = "0"
                });
            }
        }

        private void UpdateNoticeStatusIsViewed(int custId, string noticeType)
        {
            var subTypes = noticeType.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

            foreach (var subType in subTypes)
            {
                dynamicRepository.Fetch<bool>(new UpdateViewStatus(custId, isViewed: true, typeId: Converter.ToInt(subType)));
            }
        }
    }
}
