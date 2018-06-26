namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using Constants;
    using Criteria.Message;
    using DataAccesses;
    using Models.Announcement;
    using Models.Message;
    using Params;

    public interface MessageHistoryService
    {
        IEnumerable<MessageHistory> GetMessagesHistory(MessageHistoryParams messageParams);
    }

    public class MessageHistoryServiceImpl : MessageHistoryService
    {
        private readonly MessageDataAccess messageRepository;
        private readonly AnnouncementService announcementService;
        public MessageHistoryServiceImpl(
            MessageDataAccess messageRepository
            , AnnouncementService announcementService)
        {
            this.messageRepository = messageRepository;
            this.announcementService = announcementService;
        }

        public IEnumerable<MessageHistory> GetMessagesHistory(MessageHistoryParams messageParams)
        {
            messageParams.StartDate = GetAvailableLastSeenDate(messageParams.StartDate, messageParams.MessageType, messageParams.CustId);
            var fromDate = GetLimitedDate(messageParams.StartDate, messageParams.LimitedDate, messageParams.MessageType);

            var criteria = new GetMessagesHistoryCriteria(
                messageParams.CustId,
                messageParams.ParentSites,
                messageParams.SpecialType,
                messageParams.TotalMessage,
                fromDate,
                messageParams.EndDate,
                messageParams.IsTest,
                messageParams.MessageType);

            announcementService.UpdateLastSeenAnnouncement(messageParams.UserId, messageParams.CustId, messageParams.MessageType, messageParams.IsAdminUser);

            return messageRepository.GetMessagesHistory(criteria);
        }

        private DateTime GetAvailableLastSeenDate(DateTime fromDate, int messageType, int custId)
        {
            var lastSeenAnnouncement = announcementService.GetLastSeenAnnouncement(custId);
            var lastSeenDate = lastSeenAnnouncement.GetLastSeenByType(messageType);
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

        private static DateTime GetLimitedDate(DateTime fromDate, DateTime limitedDate, int messageType)
            => messageType == MessageType.Personal && fromDate < limitedDate
                ? limitedDate
                : fromDate;
    }
}