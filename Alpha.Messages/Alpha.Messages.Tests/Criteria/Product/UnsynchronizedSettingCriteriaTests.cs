using Alpha.Messages.Criteria.Product;
using NUnit.Framework;

namespace Alpha.Messages.Tests.Criteria.Product
{
    public class UnsynchronizedSettingCriteriaTests
    {
        [Test]
        public void CustId_Always_SetOnConstructor()
        {
            //// Arrange
            var custId = 123;
            var criteria = new UnsynchronizedSettingCriteria(custId);

            //// Act
            var act = criteria.CustId;

            //// Assert
            Assert.AreEqual(custId, act);
        }

        [Test]
        public void GetSettingKey_Always_IsAcc_CustomerSelUnSyncProducts()
        {
            //// Arrange
            var key = "Acc_CustomerSelUnSyncProducts";
            var criteria = new UnsynchronizedSettingCriteria(123);

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(key, act);
        }

        [Test]
        public void IsValid_CustIdIsZero_ReturnFalse()
        {
            //// Arrange
            var criteria = new UnsynchronizedSettingCriteria(0);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsFalse(act);
        }

        [Test]
        public void IsValid_CustIdIsMoreThanZero_ReturnTrue()
        {
            //// Arrange
            var criteria = new UnsynchronizedSettingCriteria(123);

            //// Act
            var act = criteria.IsValid();

            //// Assert
            Assert.IsTrue(act);
        }
    }
}
