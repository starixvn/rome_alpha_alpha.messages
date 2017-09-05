namespace Alpha.Messages.Tests.Criteria
{
    using Alpha.Messages.Criteria.Message;
    using NUnit.Framework;

    public class CountPrivateMessageCriteriaTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new CountPrivateMessageCriteria(263, "razor");
            var expectedKey = "Dot_Msg_GetPriMsgCount";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_InvalidParameter_ReturnFalse()
        {
            //// Arrange
            var criteria = new CountPrivateMessageCriteria(0, "");

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }

        [Test]
        public void IsValid_ValidParameter_ReturnTrue()
        {
            //// Arrange
            var criteria = new CountPrivateMessageCriteria(263, "razor");

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }
    }
}
