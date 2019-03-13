namespace Alpha.Messages.Params
{
    using Announcement;
    using Announcements.Models;
    using System;

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
            int messageType, 
            IParam specificationParamter)
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
            SpecificationParamter = specificationParamter;
        }

        public int SpecialType { get; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; }
        public DateTime LimitedDate { get; }
        public bool IsTest { get; }
        public int MessageType { get; }
    }
}
