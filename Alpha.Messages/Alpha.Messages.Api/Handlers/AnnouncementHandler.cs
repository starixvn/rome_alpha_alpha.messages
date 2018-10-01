namespace Alpha.Messages.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Configurations;
    using Constants;
    using Extensions;
    using MediatR;
    using Models;
    using Params.Announcement;
    using Requests;
    using Services;
    using ViewModels;

    public class AnnouncementHandler : IRequestHandler<GetAnnouncementsQuery, IEnumerable<AnnouncementViewModel>>
    {
        private readonly CustomerService customerService;
        private readonly ProductService productService;
        private readonly AnnouncementService announcementService;
        private readonly AnnouncementFormatService formatAnnouncementService;

        public AnnouncementHandler(
            CustomerService customerService,
            ProductService productService,
            AnnouncementService announcementService,
            AnnouncementFormatService formatAnnouncementService)
        {
            this.customerService = customerService;
            this.productService = productService;
            this.announcementService = announcementService;
            this.formatAnnouncementService = formatAnnouncementService;
        }

        public IEnumerable<AnnouncementViewModel> Handle(GetAnnouncementsQuery message)
        {
            if (message.Customers == null || !message.Customers.Any())
            {
                return Enumerable.Empty<AnnouncementViewModel>();
            }

            var fromTime = message.FromTime.ToLocalTime();
            var toTime = message.ToTime.ToLocalTime();

            var announcements = announcementService.GetAnnouncements(fromTime, toTime);

            if (announcements == null || !announcements.Any())
            {
                return Enumerable.Empty<AnnouncementViewModel>();
            }

            var result = new List<AnnouncementViewModel>();

            foreach (var customer in message.Customers)
            {
                var custInfo = customerService.GetCustomerInfo(customer.CustId);

                if (!AppSettings.Environment.Equals("PRO", StringComparison.OrdinalIgnoreCase)
                    && custInfo.CurrencyId != 20)
                {
                    break;
                }

                var offeredProductIds = productService.GetOfferedProductIdsByCustomer(customer.CustId);
                var specificationParam = new SpecificationParam(custInfo, offeredProductIds);
                var announcementParam = CreateAnnouncementParam(specificationParam, fromTime, toTime, customer.Language);

                var filteredAnnouncements = announcementService.GetAnnouncementsByUser(
                    announcements.Where(a => a.Systems.Contains(custInfo.System, StringComparer.OrdinalIgnoreCase)),
                    announcementParam,
                    toTime);

                var formatedAnnouncements = formatAnnouncementService.Format(filteredAnnouncements, announcementParam);

                var custAnnouncement = formatedAnnouncements.Select(a => new AnnouncementViewModel(customer.CustId, a.Content));

                result.AddRange(custAnnouncement);
            }

            return result;
        }

        private static AnnouncementsHistoryParams CreateAnnouncementParam(
            SpecificationParam param,
            DateTime fromDate,
            DateTime toDate,
            string language)
            => new AnnouncementsHistoryParams
            {
                AdminId = param.AdminId,
                CustId = param.UserId,
                Language = language,
                RoleId = param.LevelId,
                Site = param.Site,
                ParentSites = MessageConstants.ParentSitesByLevel[param.LevelId],
                UserName = param.UserName,
                FromDate = fromDate,
                ToDate = toDate,
                SpecificationParamter = param
            };
    }
}