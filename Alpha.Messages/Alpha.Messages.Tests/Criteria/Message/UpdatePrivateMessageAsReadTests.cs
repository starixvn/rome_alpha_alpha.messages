using NUnit.Framework;
namespace Alpha.Messages.Tests.Criteria.Message
{
    using Alpha.Messages.Criteria.Message;
    using NUnit.Framework;

    public class UpdatePrivateMessageAsReadTests
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new UpdatePrivateMessageAsReadCriteria(263,"raroz");
            var expectedKey = "Dot_Msg_SetPriMsgRead";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_ValidParameter_ReturnTrue()
        {
            //// Arrange
            var criteria = new UpdatePrivateMessageAsReadCriteria(263, "raroz");            

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void IsValid_InvalidParameter_ReturnFalse()
        {
            //// Arrange
            var criteria = new UpdatePrivateMessageAsReadCriteria(0, string.Empty);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }
    }
}
