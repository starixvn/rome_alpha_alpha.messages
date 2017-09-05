namespace Alpha.Messages.Tests.Criteria
{
    using Alpha.Messages.Criteria.Announcement;
    using NUnit.Framework;

    public class InsertPersonalMessageTargetCriteriaTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageTargetCriteria();
            var expectedKey = "Dot_Msg_SetUsrMessage";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_MessageMoreThanZero_ReturnTrue()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageTargetCriteria() { MessageId = 263 };

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.True(act);
        }
    }
}
