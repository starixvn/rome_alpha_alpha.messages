namespace Alpha.Messages.Site.Handlers
{
    using System.Collections.Generic;
    using Alpha.Messages.Models.ToastMessage;
    using Alpha.Messages.Params.ToastMessage;
    using Alpha.Messages.Services;
    using Alpha.Messages.Site.Requests.ToastMessage;
    using Alpha.Web.Models;
    using MediatR;

    public class ToastMessageHandler :
        IRequestHandler<ToastMessageQuery, IEnumerable<ToastMessageModel>>,
        IRequestHandler<MarkReadMessageQuery, Result>
    {
        private readonly ToastMessageService service;

        public ToastMessageHandler(ToastMessageService service)
        {
            this.service = service;
        }

        public IEnumerable<ToastMessageModel> Handle(ToastMessageQuery message)
        {
            var messageParams = new GetToastMessagesParams
            {
                CustId = message.CustId,
                SubAccId = message.SubAccId,
                Language = message.Language,
                LastToastId = message.LastToastId
            };

            var messages = service.Get(messageParams);

            return messages;
        }

        public Result Handle(MarkReadMessageQuery message)
        {
            var messageParams = new MarkReadMessageParams
            {
                Id = message.Id,
                TargetId = message.CustId,
                SubAccId = message.SubAccId
            };

            return service.MarkRead(messageParams);
        }
    }
}
