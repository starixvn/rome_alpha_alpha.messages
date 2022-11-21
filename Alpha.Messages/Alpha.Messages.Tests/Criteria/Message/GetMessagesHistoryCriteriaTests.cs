namespace Alpha.Messages.Tests.Criteria.Message
{
    using System;
    using System.Collections.Generic;
    using Alpha.Messages.Constants;
    using Alpha.Messages.Criteria.Message;
    using Alpha.Messages.Site.Tests.Helpers;
    using NUnit.Framework;

    [TestFixture]
    public class GetMessagesHistoryCriteriaTests
    {
        [TestCase(MessageType.Personal, "Age_Msg_GetPrivateMessage")]
        [TestCase(MessageType.Normal, "Age_Msg_GetPublicMessage")]
        [TestCase(MessageType.Special, "Age_Msg_GetSpecialMessage")]
        public void GetSettingKey_Always_ReturnValidSettingKeyByLevel(int messageType, string settingKey)
        {
            // Arrange
            var criteria = FakeDataHelper.Create<GetMessagesHistoryCriteria>("messageType", messageType);

            // Act
            var actualSettingKey = criteria.GetSettingKey();

            // Assert
            Assert.AreEqual(settingKey, actualSettingKey);
        }

        [Test]
        public void IsValid_CustIdIsGreaterThanZeroAndParentSitesIsNotEmpty_ReturnTrue()
        {
            // Arrange
            var parameters = new Dictionary<string, dynamic>();
            parameters.Add("messageType", MessageType.Normal);
            parameters.Add("custId", 69);
            parameters.Add("parentSites", "1");

            var criteria = FakeDataHelper.Create<GetMessagesHistoryCriteria>(parameters);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.IsTrue(actual);
        }

        [TestCase(0, "")]
        [TestCase(-1, "")]
        public void IsValid_CustIdIsLessThanOrEqualToZeroAndParentSitesIsEmpty_ReturnFalse(int custId, string parentSites)
        {
            // Arrange
            var parameters = new Dictionary<string, dynamic>();
            parameters.Add("messageType", MessageType.Normal);
            parameters.Add("custId", custId);
            parameters.Add("parentSites", parentSites);

            var criteria = FakeDataHelper.Create<GetMessagesHistoryCriteria>(parameters);

            // Act
            var actual = criteria.IsValid();

            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void AllProperties_Always_ReturnValuesSetFromCtor()
        {
            // Arrange
            var custId = 69;
            var parentSites = "1";
            var specialType = 1;
            var startDate = DateTime.Today;
            var endDate = DateTime.Today;
            var isTest = false;

            // Act
            var criteria = new GetMessagesHistoryCriteria(
                custId,
                parentSites,
                specialType,
                startDate,
                endDate,
                isTest,
                MessageType.Personal);

            // Assert
            Assert.IsTrue(custId == criteria.CustId &&
                          parentSites == criteria.ParentSites &&
                          specialType == criteria.Special &&
                          startDate == criteria.StartDate &&
                          endDate == criteria.EndDate &&
                          isTest == criteria.IsTest);
        }
    }
}
