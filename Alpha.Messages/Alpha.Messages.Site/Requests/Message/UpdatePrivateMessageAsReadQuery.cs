namespace Alpha.Messages.Site.Requests.Message
{
    using MediatR;
    using Sunplus.Agent.Model.Entities;

    public class UpdatePrivateMessageAsReadQuery : IRequest<bool>
    {
        public UpdatePrivateMessageAsReadQuery(IUserSession userSession)
        {
            UserId = userSession.CustId;
            
            UserName = userSession.UserName;

            IsInternal = userSession.IsInternal;
        }
        public bool IsInternal { get; }

        public int UserId { get; }
        
        public string UserName { get; }
    }
}