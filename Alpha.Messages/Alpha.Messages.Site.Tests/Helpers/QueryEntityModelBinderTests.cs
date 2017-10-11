namespace Alpha.Messages.Site.Tests.Helpers
{
    using System;
    using System.Web.Mvc;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Site.Helpers;
    using Alpha.Messages.Site.Requests;
    using NUnit.Framework;

    public class QueryEntityModelBinderTests: QueryEntityModelBinder
    {
        protected override object CreateInstanceOfUserSession(Type modelType)
        {
            return "CallCreateInstanceOfUserSession";
        }

        protected override object BaseCreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return "CallBaseCreateModel";
        }

        [Test]
        public void CreateModel_ParameterIsIUserSession_CallCreateInstanceOfUserSession()
        {
            //// Arrange
            var headerAnnouncement = typeof(HeaderAnnouncementQuery);

            //// Act
            var act = CreateModel(null, null, headerAnnouncement);

            //// Assert
            Assert.AreEqual("CallCreateInstanceOfUserSession", act);
        }

        [Test]
        public void CreateModel_ParameterIsNotIUserSession_CallBaseCreateInstance()
        {
            //// Arrange
            var product = typeof(Product);

            //// Act
            var act = CreateModel(null, null, product);

            //// Assert
            Assert.AreEqual("CallBaseCreateModel", act);
        }
    }
}
