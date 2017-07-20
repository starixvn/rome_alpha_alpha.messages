using Fanex.Caching;
using NSubstitute;

namespace Alpha.Messages.Tests.Fixtures
{
    public class FanexCacheFixture
    {
        public FanexCacheFixture()
        {
            CacheForTesting = Substitute.For<Cache>();
#pragma warning disable CS0618 // Type or member is obsolete
            ConfiguredCacheService.SwitchCacheStorage(CacheForTesting);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public Cache CacheForTesting { get; private set; }
    }
}
