namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using Criteria.Message;
    using Fanex.Data.Repository;
    using Models.Message;
    using Params;

    public interface MessageHistoryService
    {
        IEnumerable<MessageHistory> GetMessagesHistory(MessageHistoryParams messageParams);
    }

    public class MessageHistoryServiceImpl : MessageHistoryService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly AnnouncementService announcementService;
        public MessageHistoryServiceImpl(
            AnnouncementService announcementService,
            IDynamicRepository dynamicRepository)
        {
            this.announcementService = announcementService;
            this.dynamicRepository = dynamicRepository;
        }

        public IEnumerable<MessageHistory> GetMessagesHistory(MessageHistoryParams messageParams)
        {
            messageParams.StartDate = GetAvailableLastSeenDate(messageParams.StartDate, messageParams.MessageType, messageParams.CustId, messageParams.SubAccId);
            var fromDate = GetLimitedDate(messageParams.StartDate, messageParams.LimitedDate, messageParams.MessageType);

            var criteria = new GetMessagesHistoryCriteria(
                messageParams.CustId,
                messageParams.ParentSites,
                messageParams.SpecialType,
                fromDate,
                messageParams.EndDate,
                messageParams.IsTest,
                messageParams.MessageType,
                messageParams.Site);

            announcementService.UpdateLastSeenAnnouncement(messageParams.CustId, messageParams.MessageType, messageParams.IsAdminUser, messageParams.SubAccId);

            return dynamicRepository.Fetch<MessageHistory>(criteria);
        }

        private DateTime GetAvailableLastSeenDate(DateTime fromDate, int messageType, int custId, int subAccId)
        {
            var lastSeenAnnouncement = announcementService.GetLastSeenAnnouncement(custId, subAccId);
            var lastSeenDate = lastSeenAnnouncement.GetLastSeenByType(messageType);
            if (fromDate == DateTime.MinValue)
            {
                var threeDayAgo = DateTime.Today.AddDays(-3);
                var sixMonthAgo = DateTime.Today.AddMonths(-6);
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

        private static DateTime GetLimitedDate(DateTime fromDate, DateTime limitedDate, int messageType)
            => messageType == MessageType.Personal && fromDate < limitedDate
                ? limitedDate
                : fromDate;
    }
}