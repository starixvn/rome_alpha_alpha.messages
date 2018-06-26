namespace Alpha.Messages.Site.Requests.MessageHistory
{
    using System;
    using System.Collections.Generic;
    using Alpha.Messages.Configurations;
    using Alpha.Messages.Models.Constants;
    using Constants;
    using MediatR;
    using Sunplus.Agent.Model.Entities;
    using ViewModels;

    public class MessageHistoryQuery : IRequest<MessagesHistoryViewModel>
    {
        private const int DefaultTotalMessage = 50;        
                
        public MessageHistoryQuery(
                IUserSession userSession,
                int messageType,
                DateTime startDate,
                DateTime endDate,
                bool isTest,
                int totalMessage = DefaultTotalMessage)
        {
            UserId = userSession.UserId;
            CustId = userSession.CustId;
            CustName = userSession.UserName;
            IsAdminUser = userSession.AdminId != 0;
            ParentSites = MessageConstants.ParentSitesByLevel[userSession.RoleId];
            MessageType = messageType;
            SpecialType = MessageConstants.SpecialTypes[messageType];
            StartDate = MessageType == Constants.MessageType.Special ? new DateTime(1900, 1, 1) : startDate;
            EndDate = endDate.AddDays(1).AddTicks(-1);
            IsTest = isTest;
            TotalMessage = totalMessage;
            CurrentLang = userSession.Language;
            RoleId = userSession.RoleId;
            LimitedDateToShowInfo = userSession.GetProperty<DateTime>(SessionKey.CustomerDatelimit);
        }

        public int UserId { get; }

        public int CustId { get; }

        public string CustName { get; }

        public bool IsAdminUser { get; }

        public string ParentSites { get; }

        public int MessageType { get; }

        public int SpecialType { get; }

        public int TotalMessage { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public bool IsTest { get; }

        public string CurrentLang { get; }

        public byte RoleId { get; }

        public DateTime LimitedDateToShowInfo { get; }
    }
}