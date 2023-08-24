namespace Alpha.Messages.Site.Requests.NewMessage
{
    using Constants;
    using MediatR;
    using Models.Message;
    using Params.Announcement;
    using AlphaPlus.User.Models;

    public class NumberOfNewMessageQuery : MessageHistoryBaseRequest, IRequest<NumberOfUnreadMessage>
    {
        public NumberOfNewMessageQuery(IUserSession userSession) : base(userSession)
        {
        }

        public NumberOfUnreadMessage UnreadMessage
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
                UnreadMessage = UnreadMessage
            };
    }
}
