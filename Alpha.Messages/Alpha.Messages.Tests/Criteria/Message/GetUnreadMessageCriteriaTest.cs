namespace Alpha.Messages.Tests.Criteria
{
    using Alpha.Messages.Criteria.Announcement;
    using Messages.Criteria.Message;
    using NUnit.Framework;

    [TestFixture]
    public class GetUnreadMessageCriteriaTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new GetNumberOfUnreadMessageCriteria(263, 0, string.Empty);
            var expectedKey = "Age_MsgCustReadStatus_SelUnread";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_CustIdMoreThanZeo_ReturnTrue()
        {
            //// Arrange
            var criteria = new GetNumberOfUnreadMessageCriteria(263, 0, string.Empty);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void CustId_Always_SetOnConstructor()
        {
            //// Arrange
            var custId = 123;
            var criteria = new GetNumberOfUnreadMessageCriteria(custId, 0, string.Empty);

            //// Act
            var act = criteria.CustId;

            //// Assert
            Assert.AreEqual(custId, act);
        }
    }
}
