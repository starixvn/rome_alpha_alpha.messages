namespace Alpha.Messages.Tests.Criteria.Product
{
    using Messages.Criteria.Product;
    using NUnit.Framework;

    public class GetOfferedProductCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new GetOfferedProductCriteria(1);
            var expectedKey = "Age_GetOfferedProductByCust";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_ValidCustId_ReturnTrue()
        {
            //// Arrange
            var criteria = new GetOfferedProductCriteria(1);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.True(act);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void IsValid_InvalidCustId_ReturnFalse(int custId)
        {
            //// Arrange
            var criteria = new GetOfferedProductCriteria(custId);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.False(act);
        }
    }
}