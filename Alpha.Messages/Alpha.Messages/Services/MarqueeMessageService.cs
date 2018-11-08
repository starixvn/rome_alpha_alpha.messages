namespace Alpha.Messages.Services
{
    using System;
    using Configurations;
    using Constants;
    using Criteria.Message;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models.Announcement;
    using Models.Constants;
    using Models.Message;

    public interface MarqueeMessageService
    {
        GeneralMarqueeMessage GetLatestPublicMessage(byte roleId, string languageKey, bool isTest);

        GeneralMarqueeMessage GetLatestPrivateMessage(int custId, string languageKey, bool isTest);

        GeneralMoveBetsMessage GetMoveBetsMessage(string languageKey);
    }

    public class MarqueeMessageServiceImpl : MarqueeMessageService
    {
        private const int CachedSecond = 30;
        private readonly IDynamicRepository dynamicRepository;

        public MarqueeMessageServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
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

        public GeneralMarqueeMessage GetLatestPublicMessage(byte roleId, string languageKey, bool isTest)
        {
            var siteId = MessageConstants.ParentSitesByLevel[roleId];

            var message = ConfiguredCacheService.GetOrSet(
                $"{DataBaseMessageType.Others}_{siteId}",
                () => dynamicRepository.Get<MarqueeMessage>(new GetLatestPublicMessageCriteria(DataBaseMessageType.Others, siteId, isTest)),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(CachedSecond)));

            return new GeneralMarqueeMessage(message.Id, message.GetContentByKey(languageKey), message.BetTransId, message.IsBatch);
        }

        public GeneralMarqueeMessage GetLatestPrivateMessage(int custId, string languageKey, bool isTest)
        {
            var generalMessage = new GeneralMarqueeMessage();
            var hasLatestPrivateMessage = HasLatestPrivateMessage(custId, isTest);
            if (hasLatestPrivateMessage)
            {
                var message = dynamicRepository.Get<MarqueeMessage>(new GetLatestPrivateMessageCriteria(custId, isTest));
                if (message != null)
                {
                    generalMessage = new GeneralMarqueeMessage(message.Id, message.GetContentByKey(languageKey), message.BetTransId, message.IsBatch);
                }
            }

            return generalMessage;
        }

        private bool HasLatestPrivateMessage(int custId, bool isTest)
        {
            var id = dynamicRepository.Get<int>(new HasLatestPrivateMessageCriteria(custId, isTest));

            return id > 0;
        }
    }
}