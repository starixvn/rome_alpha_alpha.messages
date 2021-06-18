namespace Alpha.Messages.Site.Controllers
{
    using System;
    using System.Web.Mvc;
    using Announcements.Models;
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
        private readonly IParam specificationParam;

        public MessageHistoryController(IMediator mediator, IUserSession userSession, IParam specificationParam)
        {
            this.mediator = mediator;
            this.userSession = userSession;
            this.specificationParam = specificationParam;
        }

        public ActionResult Index(int messageType)
        {
            var query = new MessageHistoryQuery(
                userSession,
                messageType,
                DateTime.MinValue,
                DateTime.Today,
                AppSettings.LocalTest);
            query.Specification = specificationParam;

            var viewModel = mediator.Send(query);

            return View(viewModel);
        }

        public ActionResult Messages(
            DateTime? fromDate,
            DateTime? toDate,
            int messageType = MessageType.Normal)
        {
            var query = new MessageHistoryQuery(
                userSession,
                messageType,
                fromDate ?? DateTime.MinValue,
                toDate ?? DateTime.Today,
                AppSettings.LocalTest);
            query.Specification = specificationParam;

            var viewModel = mediator.Send(query);

            return View("Index", viewModel);
        }
    }
}