using Alpha.Messages.Models;
using Alpha.Messages.Services;
using Alpha.Messages.Site.Requests;
using NSubstitute;
using NUnit.Framework;
using Sunplus.Agent.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Site.Tests.Requests
{
    public class SpecificationParameterTests
    {
#pragma warning disable CS0618 // Type or member is obsolete
        private IUserSession userSession;
#pragma warning restore CS0618 // Type or member is obsolete
        private ProductService productService;
        private SpecificationParameter parameter;

        [SetUp]
        public void SetUp()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            userSession = Substitute.For<IUserSession>();
#pragma warning restore CS0618 // Type or member is obsolete
            productService = Substitute.For<ProductService>();
            productService.GetOfferedProductsByCustomer(Arg.Any<int>()).Returns(Enumerable.Empty<Product>());
            parameter = new SpecificationParameter(userSession, productService);
        }

        [Test]
        public void OfferedProductIds_Always_CallProductService()
        {
            //// Arrange
            userSession.UserId.Returns(1);

            //// Act
            var act = parameter.OfferedProductIds;

            //// Assert
            productService.Received().GetOfferedProductsByCustomer(Arg.Any<int>());
        }
    }
}
