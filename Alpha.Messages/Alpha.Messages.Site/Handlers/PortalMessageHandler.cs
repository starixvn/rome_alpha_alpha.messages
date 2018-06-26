namespace Alpha.Messages.Site.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using MediatR;
    using Messsages.Site.Requests.PortalMessage;
    using Services;
    using ViewModels.PortalMessage;

    public class PortalMessageHandler :
        IRequestHandler<PortalMessageQuery, PortalMessagesViewModel>,
        IRequestHandler<MarkMessageAsReadQuery, bool>
    {        
        private readonly PortalMessageService noticeService;

        public PortalMessageHandler(PortalMessageService noticeService)
        {
            this.noticeService = noticeService;
        }

        public PortalMessagesViewModel Handle(PortalMessageQuery message)
        {
            var noticeMessages = noticeService.GetPortalMessages(
                                    message.CustId,
                                    message.ParentSites,
                                    message.TotalMessage,
                                    message.IsTest);

            var noticeMessageWrappers = new List<PortalMessageWrapper>(
                noticeMessages.Select(m => new PortalMessageWrapper(m, message.CurrentLang)));

            var viewModel = new PortalMessagesViewModel(noticeMessageWrappers);

            return viewModel;
        }

        public bool Handle(MarkMessageAsReadQuery message)
        {
            var isSuccess = noticeService.MarkMessageAsRead(message.CustId, message.MessageId, message.IsInternal);

            return isSuccess;
        }

    }
}