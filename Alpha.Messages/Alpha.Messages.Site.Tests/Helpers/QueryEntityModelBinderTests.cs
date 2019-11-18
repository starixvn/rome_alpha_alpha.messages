namespace Alpha.Messages.Site.Tests.Helpers
{
    using System;
    using System.Web.Mvc;
    using Alpha.Messages.Site.Helpers;
    using Models.Product;
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
        public void CreateModel_ParameterIsNotIUserSession_CallBaseCreateInstance()
        {
            //// Arrange
            var product = typeof(Product);

            //// Act
            var act = CreateModel(null, null, product);

            //// Assert
            Assert.AreEqual("CallBaseCreateModel", act);
        }

        [Test]
        public void CreateModel_ParameterIsNotIUserSession_CreateInstanceOfUserSession()
        {
            //// Arrange
            var product = typeof(Product);

            //// Act
            var act = CreateInstanceOfUserSession(product);

            //// Assert
            Assert.AreEqual("CallCreateInstanceOfUserSession", act);
        }
    }
}
