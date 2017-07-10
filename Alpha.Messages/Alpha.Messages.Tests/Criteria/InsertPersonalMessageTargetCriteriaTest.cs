using Alpha.Messages.Criteria;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Criteria
{
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
