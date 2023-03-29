namespace Alpha.Messages.Site.Tests.Requests
{
    using System.Linq;
    using Alpha.Messages.Services;
    using Models.Product;
    using NSubstitute;
    using NUnit.Framework;
    using Site.Requests.Announcement;
    using Sunplus.Agent.Model.Entities;

    public class SpecificationParameterTests
    {
#pragma warning disable CS0618 // Type or member is obsolete
        private IUserSession userSession;
#pragma warning restore CS0618 // Type or member is obsolete
        private ProductService productService;
        private CustomerService customerService;
        private SpecificationParameter parameter;
        private Announcements.Models.IParam param;

        [SetUp]
        public void SetUp()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            userSession = Substitute.For<IUserSession>();
#pragma warning restore CS0618 // Type or member is obsolete
            productService = Substitute.For<ProductService>();
            productService.GetOfferedProductsByCustomer(Arg.Any<int>()).Returns(Enumerable.Empty<Product>());
            customerService = Substitute.For<CustomerService>();
            customerService.IsAllDayBalanceTransfer(Arg.Any<int>()).Returns(false);
            parameter = new SpecificationParameter(userSession, productService, customerService);
            param = Substitute.For<SpecificationParameter>(userSession, productService);
        }

        [Test]
        public void OfferedProductIds_Always_CallProductService()
        {
            //// Arrange
            userSession.UserId.Returns(1);

            //// Act
            var act = parameter.OfferedProductIds;

            //// Assert
            productService.Received().GetOfferedProductsByCustomer(Arg.Any<int>());
        }

        [Test]
        public void AdminId_Always_ReturnExpectedId()
        {
            //// Arrange
            var expectedId = 1;
            userSession.AdminId.Returns(expectedId);

            //// Act
            var act = parameter.AdminId;

            //// Assert
            Assert.AreEqual(expectedId, act);
        }

        [Test]
        public void ARecommendId_Always_ReturnExpectedId()
        {
            //// Arrange
            var expectedId = 1;
            userSession.ARecommendId.Returns(expectedId);

            //// Act
            var act = parameter.ARecommendId;

            //// Assert
            Assert.AreEqual(expectedId, act);
        }

        [Test]
        public void CurrencyId_Always_ReturnExpectedId()
        {
            //// Arrange
            var expectedId = 1;
            userSession.CurrencyId.Returns(expectedId);

            //// Act
            var act = parameter.CurrencyId;

            //// Assert
            Assert.AreEqual(expectedId, act);
        }

        [Test]
        public void IsInternal_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;
            userSession.IsInternal.Returns(expectedValue);

            //// Act
            var act = parameter.IsInternal;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void IsSubAcc_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;
            userSession.IsSubAcc.Returns(expectedValue);

            //// Act
            var act = parameter.IsSubAcc;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Language_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = "English";
            userSession.Language.Returns(expectedValue);

            //// Act
            var act = parameter.Language;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void LangKey_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = "EN-US";
            userSession.LangKey.Returns(expectedValue);

            //// Act
            var act = parameter.LangKey;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void LevelId_Always_ReturnExpectedId()
        {
            //// Arrange
            byte expectedId = 1;
            userSession.RoleId.Returns(expectedId);

            //// Act
            var act = parameter.LevelId;

            //// Assert
            Assert.AreEqual(expectedId, act);
        }

        [Test]
        public void UserName_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = "Agent01";
            userSession.UserName.Returns(expectedValue);

            //// Act
            var act = parameter.UserName;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void Site_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = "Agent01";
            userSession.Site.Returns(expectedValue);

            //// Act
            var act = parameter.Site;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }        

        [Test]
        public void EnableYesterdayTransfer_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;
            customerService.IsAllDayBalanceTransfer(Arg.Any<int>()).Returns(expectedValue);

            //// Act
            var act = parameter.EnableYesterdayTransfer;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void MRecommendId_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = 123;
            userSession.MRecommendId.Returns(expectedValue);

            //// Act
            var act = parameter.MRecommendId;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void SRecommendId_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = 123;
            userSession.SRecommendId.Returns(expectedValue);

            //// Act
            var act = parameter.SRecommendId;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void IsSuper_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;
            userSession.IsSuper.Returns(expectedValue);

            //// Act
            var act = parameter.IsSuper;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void IsMaster_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;
            userSession.IsMaster.Returns(expectedValue);

            //// Act
            var act = parameter.IsMaster;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void IsAgent_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;
            userSession.IsAgent.Returns(expectedValue);

            //// Act
            var act = parameter.IsAgent;

            //// Assert
            Assert.AreEqual(expectedValue, act);
        }

        [Test]
        public void ARecommendId_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = 1;
            
            //// Act
            var act = param.ARecommendId.Returns(1);

            //// Assert
            Assert.AreEqual(expectedValue, param.ARecommendId);
        }

        [Test]
        public void CurrencyId_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = 1;

            //// Act
            var act = ((int)param.CurrencyId).Returns(1);

            //// Assert
            Assert.AreEqual(expectedValue, param.CurrencyId);
        }

        [Test]
        public void IsEnableYesterdayTransfer_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;

            //// Act
            var act = param.EnableYesterdayTransfer.Returns(true);

            //// Assert
            Assert.AreEqual(expectedValue, param.EnableYesterdayTransfer);
        }

        [Test]
        public void Agent_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;

            //// Act
            var act = param.IsAgent.Returns(true);

            //// Assert
            Assert.AreEqual(expectedValue, param.IsAgent);
        }

        [Test]
        public void Master_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;

            //// Act
            var act = param.IsMaster.Returns(true);

            //// Assert
            Assert.AreEqual(expectedValue, param.IsMaster);
        }

        [Test]
        public void Super_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;

            //// Act
            var act = param.IsSuper.Returns(true);

            //// Assert
            Assert.AreEqual(expectedValue, param.IsSuper);
        }

        [Test]
        public void SubAcc_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = true;

            //// Act
            var act = param.IsSubAcc.Returns(true);

            //// Assert
            Assert.AreEqual(expectedValue, param.IsSubAcc);
        }


        [Test]
        public void Internal_Always_ReturnExpectedValue()
        {
            //// Arrange
            var expectedValue = false;

            //// Act
            var act = param.IsInternal.Returns(false);

            //// Assert
            Assert.AreEqual(expectedValue, param.IsInternal);
        }
    }
}
