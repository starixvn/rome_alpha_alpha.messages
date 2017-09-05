namespace Alpha.Messages.Tests.Criteria
{
    using Alpha.Messages.Criteria.Announcement;
    using NUnit.Framework;

    [TestFixture]
    public class GetMessageTypeListCriteriaTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new GetNoticeListIsNoViewedCriteria(263);
            var expectedKey = "Age_MessageNoticeSelByCustID";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_CustIdMoreThanZeo_ReturnTrue()
        {
            //// Arrange
            var criteria = new GetNoticeListIsNoViewedCriteria(263);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }
    }
}
