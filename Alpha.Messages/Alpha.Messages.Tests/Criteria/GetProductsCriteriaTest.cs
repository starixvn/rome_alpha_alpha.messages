namespace Alpha.Messages.Tests.Criteria
{
    using Alpha.Messages.Criteria.Announcement;
    using NUnit.Framework;

    public class GetProductsCriteriaTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new GetProductsCriteria();
            var expectedKey = "Age_Rpt_GetProductList";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_Always_ReturnTrue()
        {
            //// Arrange
            var criteria = new GetProductsCriteria();

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.True(act);
        }
    }
}
