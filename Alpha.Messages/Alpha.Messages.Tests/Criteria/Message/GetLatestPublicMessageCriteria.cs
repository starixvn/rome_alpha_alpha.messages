namespace Alpha.Messages.Tests.Criteria.Message
{
    using Alpha.Messages.Criteria.Message;
    using NUnit.Framework;

    public class GetLatestPublicMessageCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new GetLatestPublicMessageCriteria(3, "3000", false);
            var expectedKey = "Acc_Msg_GetTopPubMessage";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_InvalidParameter_ReturnFalse()
        {
            //// Arrange
            var criteria = new GetLatestPublicMessageCriteria(-1, null, false);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }

        [Test]
        public void IsValid_ValidParameter_ReturnTrue()
        {
            //// Arrange
            var criteria = new GetLatestPublicMessageCriteria(3, "3000", false);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void GetLatestPublicMessageCriteria_ManySites_ReturnCorrectSite()
        {
            //// Arrange && Act
            var criteria = new GetLatestPublicMessageCriteria(3, "222,333,4444", false);

            //// Assert
            Assert.AreEqual("222", criteria.Site1);
            Assert.AreEqual("333", criteria.Site2);
            Assert.AreEqual("4444", criteria.Site3);
        }

        [Test]
        public void LatestMessageId_Always_Return0()
        {
            //// Arrange
            var value0 = 0;
            var criteria = new GetLatestPublicMessageCriteria(3, "3000", true);

            //// Act
            var act = criteria.LatestMessageId;

            //// Assert
            Assert.AreEqual(value0, act);
        }

        [Test]
        public void IsTest_Always_ReturnValueInConstructor()
        {
            //// Arrange
            var valueInConstructor = true;
            var criteria = new GetLatestPublicMessageCriteria(3, "3000", valueInConstructor);

            //// Act
            var act = criteria.IsTest;

            //// Assert
            Assert.AreEqual(valueInConstructor, act);
        }
    }
}
