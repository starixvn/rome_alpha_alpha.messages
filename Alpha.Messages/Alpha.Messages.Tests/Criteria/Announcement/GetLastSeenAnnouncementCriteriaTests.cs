using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Messages.Criteria.Announcement;
using NUnit.Framework;

namespace Alpha.Messages.Tests.Criteria.Announcement
{
    public class GetLastSeenAnnouncementCriteriaTests
    {
        [Test]
        public void CustId_Always_SetOnConstructor()
        {
            //// Arrange
            var custId = 123;
            var criteria = new GetLastSeenAnnouncementCriteria(custId);

            //// Act
            var act = criteria.CustId;

            //// Assert
            Assert.AreEqual(custId, act);
        }

        [Test]
        public void GetSettingKey_Always_IsAge_MsgCustReadStatus_Sel()
        {
            //// Arrange
            var key = "Age_MsgCustReadStatus_Sel";
            var criteria = new GetLastSeenAnnouncementCriteria(123);

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(key, act);
        }

        [Test]
        public void IsValid_CustIdIsZero_ReturnFalse()
        {
            //// Arrange
            var criteria = new GetLastSeenAnnouncementCriteria(0);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }

        [Test]
        public void IsValid_CustIdIsMoreThanZero_ReturnTrue()
        {
            //// Arrange
            var criteria = new GetLastSeenAnnouncementCriteria(1000);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }
    }
}
