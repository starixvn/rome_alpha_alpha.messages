namespace Alpha.Messages.Tests.Models.Customer
{
    using Messages.Models.Customer;
    using NUnit.Framework;

    public class CustomerTests
    {

        [Test]
        public void IsIMBC_SiteIsNotEqualIMBC_ReturnsFalse()
        {
            // Arrange & Act
            var customer = new Customer
            {
                CustId = 1,
                UserName = string.Empty,
                RoleId = 2,
                CurrencyId = 1,
                ARecommendId = 1,
                MRecommendId = 1,
                SRecommendId = 1,
                Site = string.Empty,
                IsAllDayBalTransfer = true,
                SiteLabel = "IBC"
            };

            // Assert
            Assert.IsFalse(customer.IsIMBC);
        }


        [Test]
        public void IsAllDayTransfer_DefaultCtor_Always_ReturnFalse()
        {
            // Arrange & Act
            var customer = new Customer();

            // Assert
            Assert.IsFalse(customer.IsAllDayBalTransfer);
        }
    }
}