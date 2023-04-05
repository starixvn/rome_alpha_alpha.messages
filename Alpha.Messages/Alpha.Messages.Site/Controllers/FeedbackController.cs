namespace Alpha.Messages.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.Messages.Site.Requests.Feedback;
    using Alpha.Web.Filters;
    using MediatR;

    public class FeedbackController : SiteBaseController
    {
        public FeedbackController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet, AllowProtectedMode]
        public ViewResult Index() => View();

        [HttpGet, AllowProtectedMode]
        public Task<ActionResult> CheckExist(CheckExistsFeedbackQuery query)
            => JsonAsync(query, JsonRequestBehavior.AllowGet);

        [HttpGet, AllowProtectedMode]
        public async Task<ActionResult> Function(GetFunctionFeedbackQuery query)
        {
            var viewModel = await mediator.SendAsync(query);

            if (viewModel.FunctionId == 0)
            {
                return new EmptyResult();
            }

            return View(viewModel);
        }

        [AllowProtectedMode]
        public Task<ActionResult> Add(AddFeedbackQuery query) => JsonAsync(query);
    }
}