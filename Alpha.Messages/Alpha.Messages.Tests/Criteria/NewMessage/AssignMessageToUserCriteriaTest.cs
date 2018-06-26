namespace Alpha.Messages.Tests.Criteria
{
    using Messages.Criteria.NewMessage;
    using NUnit.Framework;

    public class AssignMessageToUserCriteriaTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new AssignMessageToUserCriteria(123, 321, "razor");
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
            var criteria = new AssignMessageToUserCriteria(123, 321, "razor");

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.True(act);
        }

        [Test]
        public void CustId_Always_SetOnConstructor()
        {
            //// Arrange
            var custId = 12121212;
            var criteria = new AssignMessageToUserCriteria(123, custId, "razor");

            //// Act
            var act = criteria.CustId;

            //// Assert
            Assert.AreEqual(custId, act);
        }

        [Test]
        public void CustName_Always_SetOnConstructor()
        {
            //// Arrange
            var custName = "custName";
            var criteria = new AssignMessageToUserCriteria(123, 121, custName);

            //// Act
            var act = criteria.CustName;

            //// Assert
            Assert.AreEqual(custName, act);
        }

        [Test]
        public void BetTransId_Always_EqualZero()
        {
            //// Arrange
            var criteria = new AssignMessageToUserCriteria(123, 1212, "razor");

            //// Act
            var act = criteria.BetTransId;

            //// Assert
            Assert.AreEqual("0", act);
        }
    }
}
