using NSubstitute;
using Sunplus.Agent.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Site.Tests.Fixtures
{
    public class UserSessionFixture
    {
        public UserSessionFixture()
        {
#pragma warning disable CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
            UserSession = Substitute.For<IUserSession>();
#pragma warning restore CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        }

#pragma warning disable CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
        public IUserSession UserSession { get; private set; }
#pragma warning restore CS0618 // 'IUserSession' is obsolete: 'Should rename to Sunplus.Agent.Model.Base'
    }
}
