namespace Alpha.Messages.Site.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Alpha.Messages.AppResources;
    using Alpha.Messages.Site.Requests.ToastMessage;
    using Alpha.Web.Filters;
    using Alpha.Web.Models;
    using AlphaPlus.User.Models;
    using MediatR;

    public class ToastMessageController : SiteBaseController
    {
        private readonly IUserSession userSession;

        public ToastMessageController(IMediator mediator, IUserSession userSession)
            : base(mediator)
        {
            this.userSession = userSession;
        }

        [AllowProtectedMode]
        public ActionResult Get(ToastMessageQuery message)
        {
            var model = mediator.Send(message);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [AllowProtectedMode]
        public ActionResult MarkRead(MarkReadMessageQuery message)
        {
            if (userSession.IsInternal)
            {
                return Json(new Result((int)HttpStatusCode.Unauthorized, Resources.NotAuthorized));
            }

            var model = mediator.Send(message);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}