using System.Threading.Tasks;
using System.Web.Mvc;
using Alpha.Auth.Configurations;
using Alpha.Messages.Site.Requests.BackupDomains;
using Alpha.Messages.Site.ViewModels;
using Alpha.Web.Filters;
using MediatR;
using Sunplus.Agent.Model.Entities;

namespace Alpha.Messages.Site.Controllers
{
    public class BackupDomainsController : SiteBaseController
    {
        private readonly IUserSession userSession;

        public BackupDomainsController(IMediator mediator, IUserSession userSession)
            : base(mediator)
        {
            this.userSession = userSession;
        }

        [HttpGet, AllowProtectedMode]
        public async Task<ActionResult> Index(BackupDomainsQuery message)
        {
            var roleId = userSession.RoleId;
            message.SiteLabel = AppSettings.GetConfigValue("SiteLabel");

            var domains = await mediator.SendAsync(message);

            var viewModel = new BackupDomainsViewModel(roleId, domains);

            return View("~/Views/BackupDomains/Index.cshtml", viewModel);
        }
    }
}