namespace Alpha.Messages.Tests.Services
{
    using Fanex.Data.Repository;
    using Messages.Criteria.Customer;
    using Messages.Models.Customer;
    using Messages.Services;
    using NSubstitute;
    using NUnit.Framework;

    public class CustomerServiceImplTests
    {
        private IDynamicRepository dynamicRepository;
        private CustomerServiceImpl customerService;

        [SetUp]
        public void SetUp()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            customerService = new CustomerServiceImpl(dynamicRepository);
        }

        [Test]
        public void GetCustomerInfo_Always_CallsDynamicRepository()
        {
            // Arrange
            var custId = 1;

            // Act
            customerService.GetCustomerInfo(custId);

            // Assert
            dynamicRepository.Received().Get<Customer>(Arg.Is<GetCustomerInfoCriteria>(c => c.CustId == custId));
        }
    }
}