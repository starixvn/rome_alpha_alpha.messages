using Fanex.Caching;
using NSubstitute;

namespace Alpha.Messages.Tests.Fixtures
{
    public class FanexCacheFixture
    {
        public FanexCacheFixture()
        {
            CacheForTesting = Substitute.For<Cache>();
            ConfiguredCacheService.SwitchCacheStorage(CacheForTesting);
        }

        public Cache CacheForTesting { get; private set; }
    }
}
