namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Criteria.Product;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Models.Product;

    public interface ProductService
    {
        IEnumerable<Product> GetOfferedProductsByCustomer(int custId);

        IEnumerable<int> GetUnconfirmSettingProductIds(int custId);

        IEnumerable<byte> GetOfferedProductIdsByCustomer(int custId);
    }

    public class ProductServiceImpl : ProductService
    {
        private const int ExpiredTimeOfferredProductMinutes = 5;
        private readonly IDynamicRepository dynamicRepository;
        private readonly Func<int, bool> canViewProduct;
        private const string ProductsCacheKey = "Alpha.Message.Services.GetAllProducts";
        private const string OfferedProductCacheKey = "Alpha.Message.Services.ProductServiceImpl.OfferedProduct";

        public ProductServiceImpl(
            IDynamicRepository dynamicRepository,
            Func<int, bool> canViewProduct)
        {
            this.dynamicRepository = dynamicRepository;
            this.canViewProduct = canViewProduct;
        }

        public IEnumerable<int> GetUnconfirmSettingProductIds(int custId)
            => dynamicRepository.Fetch<int>(new UnsynchronizedSettingCriteria(custId));

        public IEnumerable<byte> GetOfferedProductIdsByCustomer(int custId)
            => ConfiguredCacheService.GetOrSet(
                $"{OfferedProductCacheKey}.{custId}",
                () =>
                {
                    var products = dynamicRepository.Fetch<Product>(new GetOfferedProductCriteria(custId));

                    return products != null
                        ? products.Where(p => !p.IsHidden).Select(p => (byte)p.Id)
                        : Enumerable.Empty<byte>();
                },
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(ExpiredTimeOfferredProductMinutes)));

        public IEnumerable<Product> GetOfferedProductsByCustomer(int custId)
           => GetAllProducts().Where(product => canViewProduct(product.Id));

        private IEnumerable<Product> GetAllProducts()
            => ConfiguredCacheService
            .GetOrSet(
                ProductsCacheKey,
                () => dynamicRepository.Fetch<Product>(new GetProductsCriteria()),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));
    }
}