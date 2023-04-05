namespace Alpha.Messages.Site.Controllers
{
    using System.Threading.Tasks;
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
            => View(mediator.Send(message));

        protected ActionResult Json<TResponse>(IRequest<TResponse> message)
            => new JsonResult { Data = mediator.Send(message) };

        protected async Task<ActionResult> JsonAsync<TResponse>(IAsyncRequest<TResponse> message)
            => Json(await mediator.SendAsync(message));
    }
}