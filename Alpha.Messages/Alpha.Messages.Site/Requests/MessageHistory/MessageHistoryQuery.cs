namespace Alpha.Messages.Site.Requests.MessageHistory
{
    using System;
    using Constants;
    using MediatR;
    using Params;
    using Sunplus.Agent.Model.Entities;
    using ViewModels;

    public class MessageHistoryQuery : MessageHistoryBaseRequest, IRequest<MessagesHistoryViewModel>
    {
        public MessageHistoryQuery(
                IUserSession userSession,
                int messageType,
                DateTime startDate,
                DateTime endDate,
                bool isTest) : base(userSession)
        {
            MessageType = messageType;
            SpecialType = MessageConstants.SpecialTypes[messageType];
            StartDate = MessageType == Constants.MessageType.Special ? new DateTime(1900, 1, 1) : startDate;
            EndDate = endDate.AddDays(1).AddTicks(-1);
            IsTest = isTest;
            LimitedDateToShowInfo = userSession.GetProperty<DateTime>(SessionKey.CustomerDatelimit);
        }

        public int MessageType { get; }

        public int SpecialType { get; }
        
        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public bool IsTest { get; }        

        public DateTime LimitedDateToShowInfo { get; }

        public MessageHistoryParams ConvertToMessageParameters()
            => new MessageHistoryParams(
                    this.CustId,
                    this.SubAccId,
                    this.AdminId,
                    this.ParentSites,
                    this.SpecialType,
                    this.StartDate,
                    this.EndDate,
                    this.LimitedDateToShowInfo,
                    this.IsTest,
                    this.MessageType);
    }
}