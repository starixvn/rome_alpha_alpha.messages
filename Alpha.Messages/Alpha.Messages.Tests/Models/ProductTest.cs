using System;
using Alpha.Messages.Models.Announcement;
using NUnit.Framework;

namespace Alpha.Messages.Tests.Models
{
    [TestFixture]
    public class ProductTest
    {
        private Product _product;

        [SetUp]
        public void SetUp()
        {
            _product = new Product(1, "test", new DateTime(2017, 1, 1));
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

        [TearDown]
        public void TearDown()
        {
            _product = null;
        }
    }
}