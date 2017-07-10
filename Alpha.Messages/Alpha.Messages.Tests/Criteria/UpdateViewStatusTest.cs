using Alpha.Messages.Criteria;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Criteria
{
    public class UpdateViewStatusTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange
            var criteria = new UpdateViewStatus(263, true, 1);
            var expectedKey = "Age_MessageNoticeUpdStatus";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }

        [Test]
        public void IsValid_CustIdMoreThanZero_ReturnTrue()
        {
            //// Arrange
            var criteria = new UpdateViewStatus(263, true, 1);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }
    }
}
