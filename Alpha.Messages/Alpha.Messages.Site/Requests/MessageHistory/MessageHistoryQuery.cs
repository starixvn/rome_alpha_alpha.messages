namespace Alpha.Messages.Site.Requests.MessageHistory
{
    using System;
    using Constants;
    using MediatR;
    using Models.Message;
    using Params;
    using Params.Announcement;
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

        public NumberOfUnreadMessage NumberOfUnreadMessage
        {
            get
            {
                return userSession.GetProperty<NumberOfUnreadMessage>(SessionKey.NumberOfUnreadMessage);
            }
            set
            {
                userSession.SetProperty(SessionKey.NumberOfUnreadMessage, value);
            }
        }        

        public override BaseAnnouncementsParams ConvertToParameters()
            => new MessageHistoryParams(
                     CustId,
                     SubAccId,
                     AdminId,
                     ParentSites,
                     SpecialType,
                     StartDate,
                     EndDate,
                     LimitedDateToShowInfo,
                     IsTest,
                     MessageType);
    }
}