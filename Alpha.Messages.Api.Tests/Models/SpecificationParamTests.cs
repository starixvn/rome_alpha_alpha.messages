namespace Alpha.Messages.Api.Tests.Models
{
    using Alpha.Announcements.Models;
    using Alpha.Messages.Api.Models;
    using Alpha.Messages.Models.Customer;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    public class SpecificationParamTests
    {
        private Customer customer;
        private IEnumerable<byte> offerredProducts;
        private SpecificationParam parameter;
        private IParam param;

        [SetUp]
        public void SetUp()
        {
            customer = Substitute.For<Customer>();
            offerredProducts = Substitute.For<IEnumerable<byte>>();
            parameter = new SpecificationParam(customer, offerredProducts);
            param = Substitute.For<SpecificationParam>(customer, offerredProducts);
        }

        [Test]
        public void ARecommendId_Always_ReturnExpectedId()
        {
            //// Arrange
            var expectedId = 0;     

            //// Act
            var act = parameter.ARecommendId;

            //// Assert
            Assert.AreEqual(expectedId, act);
        }

        [Test]
        public void CurrencyId_Always_ReturnExpectedId()
        {
            //// Arrange
            byte expectedId = 0;

            //// Act
            var act = parameter.CurrencyId;

            //// Assert
            Assert.AreEqual(expectedId, act);
        }

        [Test]
        public void IsInternal_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = parameter.IsInternal;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void IsSubAcc_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = parameter.IsSubAcc;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Language_Always_ReturnExpectedValue()
        {
            //// Arrange
            string expectedValue = null;

            //// Act
            var act = parameter.Language;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void LangKey_Always_ReturnExpectedValue()
        {
            //// Arrange
            string expectedValue = null;

            //// Act
            var act = parameter.LangKey;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void LevelId_Always_ReturnExpectedId()
        {
            //// Arrange
            byte expectedId = 0;

            //// Act
            var act = parameter.LevelId;

            //// Assert
            Assert.AreEqual(expectedId, act);
        }

        [Test]
        public void UserName_Always_ReturnExpectedValue()
        {
            //// Arrange
            string expectedValue = null;

            //// Act
            var act = parameter.UserName;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Site_Always_ReturnExpectedValue()
        {
            //// Arrange
            string expectedValue = null;

            //// Act
            var act = parameter.Site;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void EnableYesterdayTransfer_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = parameter.EnableYesterdayTransfer;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void MRecommendId_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = 0;

            //// Act
            var act = parameter.MRecommendId;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void SRecommendId_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = 0;

            //// Act
            var act = parameter.SRecommendId;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void IsSuper_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = parameter.IsSuper;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void IsMaster_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = parameter.IsMaster;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void IsAgent_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = parameter.IsAgent;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void ARecommendId_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = 0;
            
            //// Act
            var act = param.ARecommendId;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void CurrencyId_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = 0;

            //// Act
            var act = ((int)param.CurrencyId);

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void IsEnableYesterdayTransfer_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = param.EnableYesterdayTransfer;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Agent_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = param.IsAgent;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Master_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = param.IsMaster;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Super_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = param.IsSuper;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void SubAcc_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = param.IsSubAcc;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }       

        [Test]
        public void Internal_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = param.IsInternal;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }
    }
}
