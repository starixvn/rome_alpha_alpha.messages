namespace Alpha.Messages.Site.Tests.Fixtures
{
    using NSubstitute;
    using AlphaPlus.User.Models;

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
