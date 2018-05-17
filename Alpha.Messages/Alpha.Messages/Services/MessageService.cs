namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using Configurations;
    using Constants;
    using Criteria.Message;
    using DataAccesses;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models.Constants;
    using Models.Message;
    using Params.Message;

    public interface MessageService
    {
        GeneralLatestMessage GetLatestPublicMessage(byte roleId, string languageKey, bool isTest);

        GeneralLatestMessage GetLatestPrivateMessage(int custId, string languageKey, bool isTest);

        int CountUnreadPrivateMessage(int userId, string userName);

        void UpdatePrivateAsRead(UpdatePrivateMessageAsReadParam param);

        GeneralMoveBetsMessage GetMoveBetsMessage(string languageKey);

        IEnumerable<Message> GetMessagesHistory(
            int custId,
            string parentSites,
            int specialType,
            int totalMessage,
            DateTime startDate,
            DateTime endDate,
            DateTime limitedDate,
            bool isTest,
            int messageType);
    }

    public class MessageServiceImpl : MessageService
    {
        private const int CachedSecond = 30;
        private readonly IDynamicRepository dynamicRepository;
        private readonly MessageDataAccess messageRepository;

        public MessageServiceImpl(IDynamicRepository dynamicRepository, MessageDataAccess messageRepository)
        {
            this.dynamicRepository = dynamicRepository;
            this.messageRepository = messageRepository;
        }

        public int CountUnreadPrivateMessage(int userId, string userName)
            => dynamicRepository.Get<int>(new CountPrivateMessageCriteria(userId, userName));

        public void UpdatePrivateAsRead(UpdatePrivateMessageAsReadParam param)
        {
            if (!param.IsInternal)
            {
                dynamicRepository.Execute(new UpdatePrivateMessageAsReadCriteria(param.UserId, param.UserName));
            }
        }

        public GeneralMoveBetsMessage GetMoveBetsMessage(string languageKey)
        {
            string key = "Alpha.Message.Services.NotificationMoveBets__";

            var message = ConfiguredCacheService.GetOrSet(
                key,
                () => dynamicRepository.Get<MoveBetsMessage>(new GetMoveBetInformationCriteria()),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachedSecond)));

            return new GeneralMoveBetsMessage(message.GetContentByKey(languageKey), message.MoveBetsEndTime);
        }

        public GeneralLatestMessage GetLatestPublicMessage(byte roleId, string languageKey, bool isTest)
        {
            var messageType = 3;

            var siteId = GetSiteIdByRoleId(roleId);

            var message = ConfiguredCacheService.GetOrSet(
                $"{messageType}_{siteId}",
                () => dynamicRepository.Get<LatestMessage>(new GetLatestPublicMessageCriteria(messageType, siteId, isTest)),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachedSecond)));

            return new GeneralLatestMessage(message.Id, message.GetContentByKey(languageKey));
        }

        public GeneralLatestMessage GetLatestPrivateMessage(int custId, string languageKey, bool isTest)
        {
            var generalMessage = new GeneralLatestMessage();
            var hasLatestPrivateMessage = HasLatestPrivateMessage(custId, isTest);
            if (hasLatestPrivateMessage)
            {
                var message = dynamicRepository.Get<LatestMessage>(new GetLatestPrivateMessageCriteria(custId, isTest));
                if (message != null)
                {
                    generalMessage = new GeneralLatestMessage(message.Id, message.GetContentByKey(languageKey));
                }
            }

            return generalMessage;
        }

        public IEnumerable<Message> GetMessagesHistory(
            int custId,
            string parentSites,
            int specialType,
            int totalMessage,
            DateTime startDate,
            DateTime endDate,
            DateTime limitedDate,
            bool isTest,
            int messageType)
        {
            var fromDate = messageType == MessageType.Private && startDate < limitedDate ? limitedDate : startDate;

            var criteria = new GetMessageCriteria(
                custId,
                parentSites,
                specialType,
                totalMessage,
                fromDate,
                endDate,
                isTest,
                messageType);

            return messageRepository.GetMessagesHistory(criteria);
        }

        private bool HasLatestPrivateMessage(int custId, bool isTest)
        {
            var id = dynamicRepository.Get<int>(new HasLatestPrivateMessageCriteria(custId, isTest));

            return id > 0;
        }

        private static string GetSiteIdByRoleId(byte roleId)
        {
            var siteId = AppSettings.MessageAgent;
            if (roleId == UserLevel.Super)
            {
                siteId = AppSettings.MessageSuper;
            }

            if (roleId == UserLevel.Master)
            {
                siteId = AppSettings.MessageMaster;
            }

            return siteId;
        }
    }
}