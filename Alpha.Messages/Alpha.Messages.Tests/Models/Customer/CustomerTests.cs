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
            var customer = new Customer(1, string.Empty, 2, 1, 1, 1, 1, string.Empty, true, "IBC");

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