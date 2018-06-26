namespace Alpha.Messages.Site.Controllers
{
    using System;
    using System.Web.Mvc;
    using Configurations;
    using Constants;
    using MediatR;
    using Requests.MessageHistory;
    using Sunplus.Agent.Model.Entities;
    using Web;

    public class MessageHistoryController : BaseController
    {
        private readonly IMediator mediator;
        private readonly IUserSession userSession;

        public MessageHistoryController(IMediator mediator, IUserSession userSession)
        {
            this.mediator = mediator;
            this.userSession = userSession;
        }

        public ActionResult Index(int messageType)
        {
            var viewModel = mediator.Send(new MessageHistoryQuery(
                userSession,
                messageType,
                DateTime.MinValue,
                DateTime.Today,
                AppSettings.LocalTest));

            return View(viewModel);
        }

        public ActionResult Messages(
            DateTime? startDate,
            DateTime? endDate,
            int messageType = MessageType.Normal)
        {
            var viewModel = mediator.Send(new MessageHistoryQuery(
                userSession,
                messageType,
                startDate ?? DateTime.MinValue,
                endDate ?? DateTime.Today,
                AppSettings.LocalTest));

            return View("Index", viewModel);
        }       
            
    }
}