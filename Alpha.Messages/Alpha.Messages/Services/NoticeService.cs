namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using Criteria.Notice;
    using Fanex.Data.Repository;
    using Models.Notice;

    public interface NoticeService
    {
        IEnumerable<NoticeMessage> GetNoticeMessages(
            int custId,
            string parentSites,
            DateTime lastPostTime,
            int totalMessage,
            bool isTest);

        bool MarkMessageAsRead(int custId, int messageId, bool isInternal);
    }

    public class NoticeServiceImpl : NoticeService
    {
        private readonly IDynamicRepository dynamicRepository;

        public NoticeServiceImpl(
            IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public IEnumerable<NoticeMessage> GetNoticeMessages(
            int custId,
            string parentSites,
            DateTime lastPostTime,
            int totalMessage,
            bool isTest)
        {
            var criteria = new GetNoticeMessagesCriteria(custId, parentSites, lastPostTime, totalMessage, isTest);

            var message = dynamicRepository.Fetch<NoticeMessage>(criteria);

            return message;
        }

        public bool MarkMessageAsRead(int custId, int messageId, bool isInternal)
        {
            var command = new MarkMessageAsReadCommand(custId, messageId.ToString());

            if (!command.IsValid() || isInternal)
            {
                return false;
            }

            dynamicRepository.Execute(command);

            return true;
        }
    }
}