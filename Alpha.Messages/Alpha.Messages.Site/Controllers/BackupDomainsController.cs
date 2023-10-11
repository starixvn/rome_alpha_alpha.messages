namespace Alpha.Messages.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.Messages.Site.Requests.BackupDomains;
    using Alpha.Web.Filters;
    using MediatR;

    public class BackupDomainsController : SiteBaseController
    {
        public BackupDomainsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet, AllowProtectedMode]
        public async Task<ActionResult> Index(BackupDomainsQuery message)
            => View("~/Views/BackupDomains/Index.cshtml", await mediator.SendAsync(message));
    }
}