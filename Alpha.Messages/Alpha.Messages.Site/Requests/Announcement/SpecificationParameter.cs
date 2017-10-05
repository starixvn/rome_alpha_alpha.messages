namespace Alpha.Messages.Site.Requests
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Announcements.Models;
    using Alpha.Messages.Services;
    using Sunplus.Agent.Model.Entities;

    public class SpecificationParameter : IParam
    {
        private readonly IUserSession userSession;
        private readonly ProductService productService;

        public SpecificationParameter(IUserSession userSession, ProductService productService)
        {
            this.userSession = userSession;
            this.productService = productService;
        }

        public int AdminId => userSession.AdminId;

        public int ARecommendId => userSession.ARecommendId;

        public byte CurrencyId => (byte)userSession.CurrencyId;

        public int UserId => userSession.CustId;

        public bool IsInternal => userSession.IsInternal;

        public bool IsSubAcc => userSession.IsSubAcc;

        public string Language => userSession.Language;

        public string LangKey => userSession.LangKey;

        public byte LevelId => userSession.RoleId;

        public string UserName => userSession.UserName;

        public string Site => userSession.Site;

        public bool IsSyncedCasino => userSession.IsCasinoSync;

        public int MRecommendId => userSession.MRecommendId;

        public int SRecommendId => userSession.SRecommendId;

        public bool IsSuper => userSession.IsSuper;

        public bool IsMaster => userSession.IsMaster;

        public bool IsAgent => userSession.IsAgent;

        public bool IsIMBCCustomer => userSession.IsIMBCCustomer;

        public Dictionary<string, object> DynamicParams { get; }

        public IEnumerable<byte> OfferedProductIds
            => productService.GetOfferedProductsByCustomer(UserId).Select(i => (byte)i.Id);

        public bool EnableYesterdayTransfer => userSession.IsAllDayBalTransfer;
    }
}