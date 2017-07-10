using Alpha.Messages.Params;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Tests.Params
{
    public class GetHotAnnouncementsParamsTest
    {
        [Test]
        public void AddReplacement_ExistKey_UpdateValue()
        {
            //// Arrange
            var key = "abc";
            var newValue = "zyx";
            var oldValue = "xyz";
            var replacement = new Dictionary<string, string>(); replacement.Add(key, oldValue);
            var replacementParam = new GetHotAnnouncementsParams(replacement);

            //// Act
            replacementParam.AddReplacement(key, newValue);

            //// Assert
            Assert.AreEqual(newValue, replacementParam.Replacement[key]);
        }

        [Test]
        public void AddReplacement_NewKey_AddNewValue()
        {
            //// Arrange
            var replacementParam = new GetHotAnnouncementsParams();
            var key = "zyx";
            var value = "abc";

            //// Act
            replacementParam.AddReplacement(key, value);

            //// Assert
            Assert.AreEqual(value, replacementParam.Replacement[key]);
        }
    }
}
