namespace Alpha.Messages.Api.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Announcements.Models;
    using Api.Handlers;
    using Messages.Models.Announcement;
    using Messages.Models.Customer;
    using NSubstitute;
    using NUnit.Framework;
    using Params.Announcement;
    using Requests;
    using Services;
    using ViewModels;

    public class AnnouncementHandlerTests
    {
        private CustomerService customerService;
        private ProductService productService;
        private AnnouncementService announcementService;
        private AnnouncementFormatService formatAnnouncementService;
        private AnnouncementHandler announcementHandler;

        [SetUp]
        public void SetUp()
        {
            customerService = Substitute.For<CustomerService>();
            productService = Substitute.For<ProductService>();
            announcementService = Substitute.For<AnnouncementService>();
            formatAnnouncementService = Substitute.For<AnnouncementFormatService>();

            announcementHandler = new AnnouncementHandler(
                customerService,
                productService,
                announcementService,
                formatAnnouncementService);
        }

        [Test]
        public void Handle_CustomersIsNull_ReturnsEmptyListAnnouncementViewModel()
        {
            // Arrange
            var message = new GetAnnouncementsQuery
            {
                Customers = null
            };

            // Act
            var actualResult = announcementHandler.Handle(message);

            // Assert
            Assert.AreEqual(Enumerable.Empty<AnnouncementViewModel>(), actualResult);
        }

        [Test]
        public void Handle_CustomersIsEmpty_ReturnsEmptyListAnnouncementViewModel()
        {
            // Arrange
            var message = new GetAnnouncementsQuery
            {
                Customers = Enumerable.Empty<CustomerInfo>()
            };

            // Act
            var actualResult = announcementHandler.Handle(message);

            // Assert
            Assert.AreEqual(Enumerable.Empty<AnnouncementViewModel>(), actualResult);
        }

        [Test]
        public void Handle_CustomersIsNotNullOrEmpty_CallsAnnouncementServiceGetAnnouncements()
        {
            // Arrange
            var message = CreateGetAnnouncementsQuery();

            // Act
            var actualResult = announcementHandler.Handle(message);

            // Assert
            announcementService.Received().GetAnnouncements(Arg.Any<DateTime>(), Arg.Any<DateTime>());
        }

        [Test]
        public void Handle_AnnouncementsIsEmpty_ReturnsEmptyListAnnouncementViewModel()
        {
            // Arrange
            var message = CreateGetAnnouncementsQuery();
            announcementService.GetAnnouncements(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(Enumerable.Empty<Announcement>());

            // Act
            var actualResult = announcementHandler.Handle(message);

            // Assert
            Assert.AreEqual(Enumerable.Empty<AnnouncementViewModel>(), actualResult);
        }

        [Test]
        public void Handle_AnnouncementsIsNotNullOrEmpty_CallsCustomerServiceGetCustomerInfo()
        {
            // Arrange
            var message = CreateGetAnnouncementsQuery();
            var announcements = new List<Announcement>
            {
                new Announcement()
            };
            announcementService.GetAnnouncements(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(announcements);

            var customer = new Customer(1, string.Empty, 2, 1, 1, 1, 1, string.Empty, true, 1);
            customerService.GetCustomerInfo(Arg.Any<int>()).Returns(customer);

            // Act
            var actualResult = announcementHandler.Handle(message);

            // Assert
            customerService.Received().GetCustomerInfo(Arg.Any<int>());
        }

        [Test]
        public void Handle_AnnouncementsIsNotNullOrEmpty_CallsGetOfferedProductIdsByCustomer()
        {
            // Arrange
            var message = CreateGetAnnouncementsQuery();
            var announcements = new List<Announcement>
            {
                new Announcement()
            };
            announcementService.GetAnnouncements(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(announcements);

            var customer = new Customer(1, string.Empty, 2, 1, 1, 1, 1, string.Empty, true, 1);
            customerService.GetCustomerInfo(Arg.Any<int>()).Returns(customer);

            // Act
            var actualResult = announcementHandler.Handle(message);

            // Assert
            productService.Received().GetOfferedProductIdsByCustomer(Arg.Any<int>());
        }

        [Test]
        public void Handle_AnnouncementsIsNotNullOrEmpty_CallsFormatAnnouncementService()
        {
            // Arrange
            var message = CreateGetAnnouncementsQuery();
            var announcements = new List<Announcement>
            {
                new Announcement()
            };
            announcementService.GetAnnouncements(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(announcements);

            var customer = new Customer(1, string.Empty, 2, 1, 1, 1, 1, string.Empty, true, 1);
            customerService.GetCustomerInfo(Arg.Any<int>()).Returns(customer);

            // Act
            var actualResult = announcementHandler.Handle(message);

            // Assert
            formatAnnouncementService.Received().Format(Arg.Any<IEnumerable<AlphaAnnouncement>>(), Arg.Any<BaseAnnouncementsParams>());
        }

        private GetAnnouncementsQuery CreateGetAnnouncementsQuery()
            => new GetAnnouncementsQuery
            {
                FromTime = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                ToTime = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                Customers = new List<CustomerInfo>
                {
                    new CustomerInfo
                    {
                        CustId = 1,
                        RoleId = 2,
                        Language = "en-US"
                    }
                }
            };
    }
}