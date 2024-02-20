namespace Alpha.Messages.Tests.Criteria
{
    using System.Globalization;
    using Alpha.Messages.Models.Announcement;
    using Messages.Criteria.NewMessage;
    using NSubstitute;
    using NUnit.Framework;

    public class InsertPersonalMessageCriteriaTest
    {
        private ResourceManager resourceManager;

        [SetUp]
        public void SetUp()
        {
            resourceManager = Substitute.For<ResourceManager>();
            resourceManager.GetString(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns("testing");
        }

        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageCriteria(
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            var expectedKey = "Age_Msg_SetMessage";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_Always_ReturnTrue()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageCriteria(
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.True(act);
        }

        [Test]
        public void ContentES_Always_IsEmpty()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageCriteria(
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            //// Act
            var act = criteria.ContentES;

            //// Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void ContentIT_Always_IsEmpty()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageCriteria(
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            //// Act
            var act = criteria.ContentIT;

            //// Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void CreatorName_Always_IsEmpty()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageCriteria(
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            //// Act
            var act = criteria.CreatorName;

            //// Assert
            Assert.IsEmpty(act);
        }

        [Test]
        public void IsBatch_Always_True()
        {
            //// Arrange
            var criteria = new InsertPersonalMessageCriteria(
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            //// Act
            var act = criteria.IsBatch;

            //// Assert
            Assert.IsTrue(act);
        }
    }
}
