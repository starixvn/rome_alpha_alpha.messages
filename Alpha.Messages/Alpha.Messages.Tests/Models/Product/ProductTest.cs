using Alpha.Messages.Models.Product;
using NUnit.Framework;

namespace Alpha.Messages.Tests.Models
{
    [TestFixture]
    public class ProductTest
    {
        private Product _product;
        private Product _productDefaultCtor;

        [SetUp]
        public void SetUp()
        {
            _product = new Product(1, "test", false);
            _productDefaultCtor = new Product();
        }

        [Test]
        public void Product_Id_IsGreaterThanZero()
        {
            Assert.Greater(_product.Id, 0);
        }

        [Test]
        public void Product_Name_IsNotNull()
        {
            Assert.IsNotNull(_product.Name);
        }

        [Test]
        public void Product_Name_IsNull_Default()
        {
            Assert.IsNull(_productDefaultCtor.Name);
        }

        [Test]
        public void Product_Id_IsZero_Default()
        {
            Assert.GreaterOrEqual(_productDefaultCtor.Id, 0);
        }

        [Test]
        public void Product_IsHidden_AlwaysFalse_Default()
        {
            Assert.IsFalse(_productDefaultCtor.IsHidden);
        }

        [TearDown]
        public void TearDown()
        {
            _product = null;
        }
    }
}