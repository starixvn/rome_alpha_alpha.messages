namespace Alpha.Messages.Services
{
    using Alpha.Messages.Criteria;
    using Alpha.Messages.Models;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface ProductService
    {
        IEnumerable<Product> GetOfferedProductsByCustomer(int custId);
    }

    public class ProductServiceImpl: ProductService
    {        
        private readonly IDynamicRepository dynamicRepository;
        private readonly Func<int, bool> canViewProduct;
        private const string ProductsCacheKey = "Alpha.Message.Services.GetAllProducts";

        public ProductServiceImpl(
            IDynamicRepository dynamicRepository,
            Func<int, bool> canViewProduct)
        {
            this.dynamicRepository = dynamicRepository;
            this.canViewProduct = canViewProduct;
        }

        public IEnumerable<Product> GetOfferedProductsByCustomer(int custId)
        {
            var products = GetAllProducts()
                        .Where(product => canViewProduct(product.Id));

            return products;
        }

        private IEnumerable<Product> GetAllProducts()
        {
            var products = ConfiguredCacheService
                .GetOrSet(
                ProductsCacheKey,
                () => dynamicRepository.Fetch<Product>(new GetProductsCriteria()),
                new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1)));

            return products;
        }
    }
}
