namespace Alpha.Messages.Services
{
    using System;
    using Configurations;
    using Criteria.Message;
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
    }

    public class MessageServiceImpl : MessageService
    {
        private const int CachedSecond = 30;
        private readonly IDynamicRepository dynamicRepository;

        public MessageServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
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