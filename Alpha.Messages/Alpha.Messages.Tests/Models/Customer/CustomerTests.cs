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

        [TestCase(1, "IBCBet")]
        [TestCase(7, "789Y")]
        [TestCase(8, "EU88")]
        [TestCase(9, "Y987")]
        public void System_Always_ReturnsCorrectValue(int siteCode, string system)
        {
            // Arrange & Act
            var customer = new Customer(1, string.Empty, 2, 1, 1, 1, 1, string.Empty, true, siteCode);

            // Assert
            Assert.AreEqual(system, customer.System);
        }
    }
}