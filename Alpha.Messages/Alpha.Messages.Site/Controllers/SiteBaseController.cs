namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using MediatR;
    using Web;

    public abstract class SiteBaseController : BaseController
    {
        protected readonly IMediator mediator;

        protected SiteBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected ActionResult View<TResponse>(IRequest<TResponse> message)
        {
            AssignDarkModeViewData();
            return View(mediator.Send(message));
        }

        protected ActionResult Json<TResponse>(IRequest<TResponse> message)
            => new JsonResult { Data = mediator.Send(message) };
    }
}