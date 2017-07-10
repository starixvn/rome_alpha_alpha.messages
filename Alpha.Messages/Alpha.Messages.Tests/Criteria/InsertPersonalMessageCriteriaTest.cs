using Alpha.Messages.Criteria;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Criteria
{
    public class InsertPersonalMessageCriteriaTest
    {
        [Test]
        public void GetSettingKey_Always_ReturnCorrectKey()
        {
            //// Arrange            
            var resource = NSubstitute.Substitute.For<ResourceManager>();
            resource.GetString(Arg.Any<string>(), Arg.Any<CultureInfo>()).Returns("testing");
            var criteria = new InsertPersonalMessageCriteria("vn-VI", resource);
            var expectedKey = "Dot_Msg_SetMessage";

            //// Act
            var act = criteria.GetSettingKey();

            //// Assert
            Assert.AreEqual(expectedKey, act);
        }
    }
}
