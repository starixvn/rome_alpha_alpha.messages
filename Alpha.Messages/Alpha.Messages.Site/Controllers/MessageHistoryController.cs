namespace Alpha.Messages.Site.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Alpha.Web.Filters;
    using Alpha.Web.Helpers;
    using Announcements.Models;
    using Configurations;
    using Constants;
    using MediatR;
    using Requests.MessageHistory;
    using Sunplus.Agent.Model.Entities;

    public class MessageHistoryController : SiteBaseController
    {
        private readonly IUserSession userSession;
        private readonly IParam specificationParam;

        public MessageHistoryController(IMediator mediator, IUserSession userSession, IParam specificationParam)
            : base(mediator)
        {
            this.userSession = userSession;
            this.specificationParam = specificationParam;
        }

        [AllowProtectedMode]
        public ActionResult Index(int messageType)
        {
            if (IsDisableMessages(messageType))
            {
                return UnAuthorizedAction(Session);
            }

            AssignIsProtectedModeToViewData();
            var query = new MessageHistoryQuery(
                userSession,
                messageType,
                DateTime.MinValue,
                DateTime.Today,
                AppSettings.LocalTest);
            query.Specification = specificationParam;

            return View(query);
        }

        [AllowProtectedMode]
        public ActionResult Messages(
            DateTime? fromDate,
            DateTime? toDate,
            int messageType = MessageType.Normal)
        {
            if (IsDisableMessages(messageType))
            {
                return UnAuthorizedAction(Session);
            }

            AssignIsProtectedModeToViewData();
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

        private bool IsDisableMessages(int messageType)
            => userSession.IsProtectedMode && messageType == MessageType.Personal;

        private static ActionResult UnAuthorizedAction(HttpSessionStateBase session)
        {
            TerminateSession(session);

            return new RedirectResult(MicroSites.OldMainUrl(FormsAuthentication.LoginUrl));
        }

        private static void TerminateSession(HttpSessionStateBase session)
        {
            session.Clear();
            session.Abandon();

            if (!session.IsCookieless)
            {
                FormsAuthentication.SignOut();
            }
        }

        private void AssignIsProtectedModeToViewData()
            => ViewData["IsProtectedMode"] = userSession.IsProtectedMode;
    }
}