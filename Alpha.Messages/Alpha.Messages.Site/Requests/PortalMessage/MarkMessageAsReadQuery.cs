namespace Alpha.Messsages.Site.Requests.PortalMessage
{
    using MediatR;
    using Messages.Constants;
    using Messages.Site.ViewModels;
    using Sunplus.Agent.Model.Entities;

    public class MarkMessageAsReadQuery : IRequest<bool>
    {
        public MarkMessageAsReadQuery(int custId, int messageId, bool isInternal)
        {
            CustId = custId;
            MessageId = messageId;
            IsInternal = isInternal;
        }

        public int CustId { get; }

        public int MessageId { get; }

        public bool IsInternal { get; }
    }
}