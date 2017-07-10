using Fanex.Caching;
using NSubstitute;

namespace Alpha.Messages.Tests.Fixtures
{
    public class FanexCacheFixture
    {
        public FanexCacheFixture()
        {
            CacheForTesting = Substitute.For<Cache>();
#pragma warning disable CS0618 // 'ConfiguredCacheService.SwitchCacheStorage(Cache)' is obsolete: 'This method will be removed soon. Use 'new CacheService(Cache cacheStorage)' instead.'
            ConfiguredCacheService.SwitchCacheStorage(CacheForTesting);
#pragma warning restore CS0618 // 'ConfiguredCacheService.SwitchCacheStorage(Cache)' is obsolete: 'This method will be removed soon. Use 'new CacheService(Cache cacheStorage)' instead.'
        }

        public Cache CacheForTesting { get; private set; }
    }
}
