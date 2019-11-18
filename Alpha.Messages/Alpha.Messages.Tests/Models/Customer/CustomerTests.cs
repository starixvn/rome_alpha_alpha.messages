namespace Alpha.Messages.Tests.Models.Customer
{
    using Messages.Models.Customer;
    using NUnit.Framework;

    public class CustomerTests
    {
        [Test]
        public void IsIMBC_SiteIsIMBC_ReturnsTrue()
        {
            // Arrange & Act
            var customer = new Customer(1, string.Empty, 2, 1, 1, 1, 1, "IMBC", true, 1);

            // Assert
            Assert.IsTrue(customer.IsIMBC);
        }

        [Test]
        public void IsIMBC_SiteIsNotEqualIMBC_ReturnsFalse()
        {
            // Arrange & Act
            var customer = new Customer(1, string.Empty, 2, 1, 1, 1, 1, string.Empty, true, 1);

            // Assert
            Assert.IsFalse(customer.IsIMBC);
        }

        [Test]
        public void IsSyncedCasino_DefaultCtor_Always_ReturnFalse()
        {
            // Arrange & Act
            var customer = new Customer();

            // Assert
            Assert.IsFalse(customer.IsSyncedCasino);
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