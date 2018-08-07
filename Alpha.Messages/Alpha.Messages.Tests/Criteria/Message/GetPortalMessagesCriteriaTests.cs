using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Messages.Criteria.Message;
using NUnit.Framework;

namespace Alpha.Messages.Tests.Criteria.Message
{
    public class GetPortalMessagesCriteriaTests
    {
        [Test]
        public void CustId_Always_SetOnConstructor()
        {
            //// Arrange
            var custId = 123;
            var criteria = new GetPortalMessagesCriteria(custId, "parentSite", 10, true);

            //// Act
            var act = criteria.CustId;

            //// Assert
            Assert.AreEqual(custId, act);
        }

        [Test]
        public void ParentSites_Always_SetOnConstructor()
        {
            //// Arrange
            var parentSites = "ParentSites";
            var criteria = new GetPortalMessagesCriteria(123, parentSites, 10, true);

            //// Act
            var act = criteria.ParentSites;

            //// Assert
            Assert.AreEqual(parentSites, act);
        }

        [Test]
        public void TotalMessage_Always_SetOnConstructor()
        {
            //// Arrange
            var totalMessage  = 123;
            var criteria = new GetPortalMessagesCriteria(123 , "parentSite", totalMessage, true);

            //// Act
            var act = criteria.TotalMessage;

            //// Assert
            Assert.AreEqual(totalMessage , act);
        }

        [Test]
        public void IsTest_Always_SetOnConstructor()
        {
            //// Arrange
            var isTest = true;
            var criteria = new GetPortalMessagesCriteria(123, "parentSite", 10, isTest);

            //// Act
            var act = criteria.IsTest;

            //// Assert
            Assert.AreEqual(isTest, act);
        }

        [Test]
        public void GetSettingKey_Always_IsAge_CustomerSelMessages()
        {
            //// Arrange
            var key = "Age_CustomerSelMessages";
            var criteria = new GetPortalMessagesCriteria(123, "parentSite", 10, true);

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(key, act);
        }

        [Test]
        public void IsValid_ValidParamater_ReturnTrue()
        {
            //// Arrange
            var criteria = new GetPortalMessagesCriteria(123, "parentSite", 10, true);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void IsValid_InValidCustIdParamater_ReturnFalse()
        {
            //// Arrange
            var criteria = new GetPortalMessagesCriteria(0, "parentSite", 10, true);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }

        [Test]
        public void IsValid_InValidParentSitesParamater_ReturnFalse()
        {
            //// Arrange
            var criteria = new GetPortalMessagesCriteria(100, string.Empty, 10, true);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }
    }
}
