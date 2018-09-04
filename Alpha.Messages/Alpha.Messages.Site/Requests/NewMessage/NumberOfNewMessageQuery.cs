namespace Alpha.Messages.Site.Requests.NewMessage
{
    using Constants;
    using MediatR;
    using Models.Message;
    using Params.Announcement;
    using Sunplus.Agent.Model.Entities;

    public class NumberOfNewMessageQuery : MessageHistoryBaseRequest, IRequest<NumberOfUnreadMessage>
    {
        public NumberOfNewMessageQuery(IUserSession userSession) : base(userSession)
        {
        }

        public NumberOfUnreadMessage CurrentNumberOfUnreadMessage
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


        public NumberOfNewMessageParams ConvertToNumberOfNewMessageParameters()
            => new NumberOfNewMessageParams
            {
                AdminId = AdminId,
                CustId = CustId,
                Language = Language,
                RoleId = RoleId,
                Site = Site,
                SubAccId = SubAccId,
                UserName = UserName,
                SpecificationParamter = Specification,
                ParentSites = ParentSites,
                UnreadMessage = CurrentNumberOfUnreadMessage
            };
    }
}
