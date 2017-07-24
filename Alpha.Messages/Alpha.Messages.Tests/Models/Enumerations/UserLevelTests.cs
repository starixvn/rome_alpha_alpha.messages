using Alpha.Messages.Models.Enumerations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Models.Enumerations
{
    public class UserLevelTests
    {
        [Test]
        public void IsMemberLevel_ValueIs1_ReturnTrue()
        {
            //// Arrange

            //// Act
            var act = UserLevel.IsMemberLevel(1);

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void IsAgentLevel_ValueIs2_ReturnTrue()
        {
            //// Arrange

            //// Act
            var act = UserLevel.IsAgentLevel(2);

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void IsMasterLevel_ValueIs3_ReturnTrue()
        {
            //// Arrange

            //// Act
            var act = UserLevel.IsMasterLevel(3);

            //// Assert
            Assert.IsTrue(act);
        }

        [Test]
        public void IsSuperLevel_ValueIs4_ReturnTrue()
        {
            //// Arrange

            //// Act
            var act = UserLevel.IsSuperLevel(4);

            //// Assert
            Assert.IsTrue(act);
        }
    }
}
