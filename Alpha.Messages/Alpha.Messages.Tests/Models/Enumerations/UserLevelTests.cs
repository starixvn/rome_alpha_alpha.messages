namespace Alpha.Messages.Tests.Models.Enumerations
{
    using Messages.Models.Constants;
    using NUnit.Framework;


    public class UserLevelTests
    {
        [Test]
        public void IsMember_ValueIs1_ReturnTrue()
        {
            //// Arrange

            //// Act
            var act = UserLevel.IsMember(1);

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void IsAgent_ValueIs2_ReturnTrue()
        {
            //// Arrange

            //// Act
            var act = UserLevel.IsAgent(2);

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void IsMaster_ValueIs3_ReturnTrue()
        {
            //// Arrange

            //// Act
            var act = UserLevel.IsMaster(3);

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void IsSuper_ValueIs4_ReturnTrue()
        {
            //// Arrange

            //// Act
            var act = UserLevel.IsSuper(4);

            //// Assert
            Assert.IsTrue(act);
        }
    }
}
