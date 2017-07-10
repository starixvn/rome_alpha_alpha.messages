using Alpha.Announcements.Models;
using Sunplus.Agent.Model.Entities;
using System.Collections.Generic;

namespace Alpha.Messages.Site.Requests
{
    public class SpecificationParameter : IParam
    {
#pragma warning disable CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        private readonly IUserSession userSession;
#pragma warning restore CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'

#pragma warning disable CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        public SpecificationParameter(IUserSession userSession)
#pragma warning restore CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        {
            this.userSession = userSession;
        }

        public int AdminId => userSession.AdminId;

        public int ARecommendId => userSession.ARecommendId;

        public byte CurrencyId => (byte)userSession.CurrencyId;

        public int UserId => userSession.UserId;

        public bool IsInternal => userSession.IsInternal;

        public bool IsSubAcc => userSession.IsSubAcc;

        public string Language => userSession.Language;

        public string LangKey => userSession.LangKey;

        public byte LevelId => userSession.RoleId;

        public string UserName => userSession.UserName;

        public string Site => userSession.SiteName;

        public bool IsSyncedCasino => userSession.IsLiveCasinoSync;

        public IEnumerable<byte> OfferedProductIds { get; set; }
    }
}