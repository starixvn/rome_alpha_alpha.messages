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
        public const int FishingWorldNotice = 3;
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
            var announcements = new List<HotAnnouncement>();
            var messageTypes = Enumerable.Empty<string>();
            if (parameters.AdminId == 0)
            {
                messageTypes = dynamicRepository
                    .Fetch<byte>(new GetMessageTypeListCriteria(parameters.CustId))
                    .Select(messageType => messageType.ToString());

                if (IsNotAgentLevel(parameters.RoleId))
                {
                    messageTypes = messageTypes.Contains(FishingWorldNotice.ToString()) ?
                        new List<string> { FishingWorldNotice.ToString() } :
                        new List<string>();
                }
            }

            var content = GetSettingChangedMessage(messageTypes, parameters);
            if (!string.IsNullOrEmpty(content))
            {
                announcements.Add(new HotAnnouncement
                {                    
                    Content = content
                });
            }

            return announcements;
        }

        private static bool IsNotAgentLevel(byte roleId) => !Enumeration.FromValue<UserLevel>(roleId).IsAgentLevel();

        private string GetSettingChangedMessage(IEnumerable<string> messageTypes, GetHotAnnouncementsParams announcementParams)
        {
            var noticeTypes = announcementParams.NoticeTypes;

            var correctType = noticeTypes.FirstOrDefault(noticeType => ShowNotice(noticeType, messageTypes));
            var messageContent = string.Empty;

            if (correctType != null)
            {
                var resourceKey = "Notice_" + correctType;
                messageContent = Resources.ResourceManager.GetString(resourceKey);
                InsertPersonalMessage(resourceKey, announcementParams);
                UpdateViewStatus(announcementParams.CustId, correctType);
            }

            return messageContent;
        }

        private static bool ShowNotice(string noticeType, IEnumerable<string> messageTypes)
        {
            var splitChars = new[] { '_' };

            return noticeType.Split(splitChars, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(subMessageType => !messageTypes.Contains(subMessageType)) == null;
        }

        private void InsertPersonalMessage(string key, GetHotAnnouncementsParams getParams)
        {
            var rawData = dynamicRepository.Fetch<int>(new InsertPersonalMessageCriteria(key, resourceManager));
            if (!rawData.Any())
            {
                return;
            }

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

        private void UpdateViewStatus(int custId, string noticeType)
        {
            var subTypes = noticeType.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var subType in subTypes)
            {
                dynamicRepository.Fetch<bool>(new UpdateViewStatus(custId, true, Converter.ToInt(subType)));
            }
        }
    }
}
