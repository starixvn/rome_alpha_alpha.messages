using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Messages.Criteria.Announcement;
using NUnit.Framework;

namespace Alpha.Messages.Tests.Criteria.Announcement
{
    public class UpdateLastSeenAnnouncementCriteriaTests
    {
        [Test]
        public void CustId_Always_SetOnConstructor()
        {
            //// Arrange
            var custId = 123;
            var criteria = new UpdateLasteSeenAnnouncementCriteria(321, custId, 1);

            //// Act
            var act = criteria.CustId;

            //// Assert
            Assert.AreEqual(custId, act);
        }

        [Test]
        public void GetSettingKey_Always_IsAge_MsgCustReadStatus_Upd()
        {
            //// Arrange
            var criteria = new UpdateLasteSeenAnnouncementCriteria(321, 123, 1);
            var key = "Age_MsgCustReadStatus_Upd";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(key, act);
        }

        [Test]
        public void IsValid_CustIdIsZero_ReturnFalse()
        {
            //// Arrange
            var criteria = new UpdateLasteSeenAnnouncementCriteria(321, 0, 1);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }

        [Test]
        public void IsValid_CustIdIsMoreThanZero_ReturnTrue()
        {
            //// Arrange
            var criteria = new UpdateLasteSeenAnnouncementCriteria(321, 100, 1);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }
    }
}
