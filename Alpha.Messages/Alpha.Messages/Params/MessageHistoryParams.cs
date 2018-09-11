namespace Alpha.Messages.Params
{
    using System;
    using Announcement;

    public class MessageHistoryParams : BaseAnnouncementsParams
    {
        public MessageHistoryParams(
            int custId,
            int subAccId,
            int adminId,
            string parentSites,
            int specialType,
            DateTime startDate,
            DateTime endDate,
            DateTime limitedDate,
            bool isTest,
            int messageType)
        {
            CustId = custId;
            SubAccId = subAccId;
            AdminId = adminId;
            ParentSites = parentSites;
            SpecialType = specialType;
            StartDate = startDate;
            EndDate = endDate;
            LimitedDate = limitedDate;
            IsTest = isTest;
            MessageType = messageType;
        }

        public int SpecialType { get; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; }
        public DateTime LimitedDate { get; }
        public bool IsTest { get; }
        public int MessageType { get; }
    }
}
