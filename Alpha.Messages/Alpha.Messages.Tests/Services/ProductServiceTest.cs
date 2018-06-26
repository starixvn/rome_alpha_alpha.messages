namespace Alpha.Messages.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Data.Repository;
    using Fixtures;
    using Messages.Criteria;
    using Messages.Criteria.Announcement;
    using Messages.Criteria.Product;
    using Messages.Models.Announcement;
    using Messages.Models.Product;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;

    public class ProductServiceTest
    {
        private IDynamicRepository dynamicRepository;
        private ProductServiceImpl productService;
        private FanexCacheFixture fanexCacheFixture;
        private const string ProductsCacheKey = "Alpha.Message.Services.GetAllProducts";
        private Func<int, bool> canViewProduct;
        private IEnumerable<Product> productList;

        [SetUp]
        public void SetUp()
        {
            canViewProduct = Substitute.For<Func<int, bool>>();
            canViewProduct.Invoke(Arg.Any<int>()).Returns(true);
            dynamicRepository = Substitute.For<IDynamicRepository>();
            productService = new ProductServiceImpl(dynamicRepository, canViewProduct);

            productList = Enumerable.Empty<Product>();
            fanexCacheFixture = new FanexCacheFixture();
            fanexCacheFixture.CacheForTesting.TryGet(ProductsCacheKey, out productList);
        }

        [Test]
        public void GetOfferedProductsByCustomer_CustIdMoreThanZero_ReturnEmptyList()
        {
            //// Arrange

            //// Act
            var act = productService.GetOfferedProductsByCustomer(0);

            //// Assert
            Assert.False(act.Any());
        }

        [Test]
        public void GetOfferedProductsByCustomer_NotExistInCache_AlwaysCallDynamicRepository()
        {
            //// Arrange

            //// Act
            productService.GetOfferedProductsByCustomer(263);

            //// Assert
            dynamicRepository.Received().Fetch<Product>(Arg.Any<GetProductsCriteria>());
        }

        [Test]
        public void GetOfferedProductsByCustomer_NotExistInCache_AlwaysCallCanViewProduct()
        {
            //// Arrange

            //// Act
            productService.GetOfferedProductsByCustomer(263);

            //// Assert
            canViewProduct.Received();
        }

        [Test]
        public void GetGetUnconfirmSettingProductIds_Always_AlwaysCallDynamicRepository()
        {
            //// Arrange

            //// Act
            productService.GetUnconfirmSettingProductIds(263);

            //// Assert
            dynamicRepository.Received().Fetch<int>(Arg.Any<UnsynchronizedSettingCriteria>());
        }
    }
}
