namespace Alpha.Messages.Api.Tests.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using Announcements.Models;
    using Api.Handlers;
    using Configurations;
    using Fanex.Logging;
    using KellermanSoftware.CompareNetObjects;
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
            AppSettings.AppSettingValues = new NameValueCollection
            {
                ["Environment"] = "LOCAL"
            };
            announcementHandler = new AnnouncementHandler(
                customerService,
                productService,
                announcementService,
                formatAnnouncementService,
                Substitute.For<ILogger>());
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

            var customer = new Customer(1, string.Empty, 2, 20, 1, 1, 1, string.Empty, true, "MBC");
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

            var customer = new Customer(1, string.Empty, 2, 20, 1, 1, 1, string.Empty, true, "MBC");
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

            var customer = new Customer(1, string.Empty, 2, 20, 1, 1, 1, string.Empty, true, "MBC");
            customerService.GetCustomerInfo(Arg.Any<int>()).Returns(customer);

            // Act
            var actualResult = announcementHandler.Handle(message);

            // Assert
            formatAnnouncementService.Received().Format(Arg.Any<IEnumerable<AlphaAnnouncement>>(), Arg.Any<BaseAnnouncementsParams>());
        }

        [Test]
        public void Handle_AnnouncementsIsNotNullOrEmpty_ReturnsCorrectResult()
        {
            // Arrange
            var compareLogic = new CompareLogic();
            var message = CreateGetAnnouncementsQuery();
            var announcements = new List<Announcement>
            {
                new Announcement()
            };
            var alphaAnnouncement = new List<AlphaAnnouncement> {
                new AlphaAnnouncement
                {
                    Content="Content"
                }
            };
            announcementService.GetAnnouncements(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(announcements);

            var customer = new Customer(1, string.Empty, 2, 20, 1, 1, 1, string.Empty, true, "MBC");
            customerService.GetCustomerInfo(Arg.Any<int>()).Returns(customer);

            var expectedResult = new List<AnnouncementViewModel>
            {
                new AnnouncementViewModel(1, "Content")
            };

            formatAnnouncementService.Format(
                Arg.Any<IEnumerable<AlphaAnnouncement>>(),
                Arg.Any<BaseAnnouncementsParams>())
                .Returns(alphaAnnouncement);

            // Act
            var actualResult = announcementHandler.Handle(message);

            // Assert
            Assert.IsTrue(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Test]
        public void Handle_AnnouncementsIsEmpty_ReturnsCorrectResult()
        {
            // Arrange
            var message = CreateGetAnnouncementsQuery();
            var announcements = Enumerable.Empty<Announcement>();

            announcementService.GetAnnouncements(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(announcements);

            // Act
            var actualResult = announcementHandler.Handle(message);

            var expectedResult = Enumerable.Empty<AnnouncementViewModel>();

            // Assert
            Assert.IsTrue(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Test]
        public void Handle_AnnouncementsIsNull_ReturnsCorrectResult()
        {
            // Arrange
            var message = CreateGetAnnouncementsQuery();
            IEnumerable<Announcement> announcements = null;

            announcementService.GetAnnouncements(Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(announcements);

            // Act
            var actualResult = announcementHandler.Handle(message);

            IEnumerable<AnnouncementViewModel> expectedResult = Enumerable.Empty<AnnouncementViewModel>();

            // Assert
            Assert.IsTrue(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
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