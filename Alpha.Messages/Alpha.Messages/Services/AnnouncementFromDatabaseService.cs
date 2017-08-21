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
        HotAnnouncement GetFirstHotAnnouncement(GetHotAnnouncementsParams getParams);
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

        public HotAnnouncement GetFirstHotAnnouncement(GetHotAnnouncementsParams parameters)
        {
            var noticesAreNotViewed = GetNoticesAreNotViewed(parameters);

            var announcements = GetFirstNoticeIsViewing(noticesAreNotViewed, parameters);

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

                if (IsNotAgent(parameters.RoleId))
                {
                    noticesAreNoViewed = noticesAreNoViewed.Contains(FishingWorldNotice.ToString()) ?
                        new List<string> { FishingWorldNotice.ToString() } :
                        new List<string>();
                }
            }

            return noticesAreNoViewed;
        }

        private static bool IsNotAgent(byte roleId) => !UserLevel.IsAgentLevel(roleId);

        private HotAnnouncement GetFirstNoticeIsViewing(IEnumerable<string> NoticesAreNoViewed, GetHotAnnouncementsParams announcementParams)
        {
            var firstType = announcementParams.NoticeTypes.FirstOrDefault(i => IsShowNotice(i, NoticesAreNoViewed));

            if (firstType != null)
            {
                var resourceKey = "Notice_" + firstType;
                var id = InsertPersonalNotice(resourceKey, announcementParams);
                UpdateNoticeStatusIsViewed(announcementParams.CustId, firstType);

                var content = Resources.ResourceManager.GetString(resourceKey);
                return new HotAnnouncement(id.ToString(), content, string.Empty, string.Empty);
            }

            return null;
        }

        private static bool IsShowNotice(string noticeType, IEnumerable<string> NoticesAreNoViewed)
        {
            var splitChars = new[] { '_' };

            return noticeType
                .Split(splitChars, StringSplitOptions.RemoveEmptyEntries)                
                .FirstOrDefault(subMessageType => !NoticesAreNoViewed.Contains(subMessageType)) == null;
        }

        private int InsertPersonalNotice(string key, GetHotAnnouncementsParams getParams)
        {
            var id = 0;
            var rawData = dynamicRepository.Fetch<int>(new InsertPersonalMessageCriteria(key, resourceManager));
            if (rawData.Any())
            {
                id = rawData.FirstOrDefault();
                dynamicRepository.Fetch<bool>(new InsertPersonalMessageTargetCriteria
                {
                    MessageId = id,
                    TargetId = getParams.UserId,
                    TargetName = getParams.UserName,
                    IsRead = false,
                    BetTransId = "0"
                });
            }

            return id;
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
