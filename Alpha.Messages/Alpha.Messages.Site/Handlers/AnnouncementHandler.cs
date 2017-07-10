using Alpha.Messages.Client.Requests;
using Alpha.Messages.Constants;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Alpha.Messages.Services;
using Alpha.Messages.Site.Requests;
using Alpha.Messages.Site.ViewModels;
using Alpha.Web.Helpers;
using MediatR;
using Sunplus.Agent.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha.Messages.Site.Handlers
{
    public class AnnouncementHandler :
         IRequestHandler<HotAnnouncementQuery, HotAnnouncementViewModel>
    {
        private readonly AnnouncementService announcementService;
        private readonly ProductService productService;

        public AnnouncementHandler(AnnouncementService announcementService, ProductService productService)
        {
            this.announcementService = announcementService;
            this.productService = productService;
        }

        public HotAnnouncementViewModel Handle(HotAnnouncementQuery message)
        {
            var parameters = message.AssignValue();

            parameters.AddReplacement(TagMessage.SiteMain, message.IsMobileDevice ? MicroSites.MainUrl("~") : string.Empty);
            parameters.AddReplacement(TagMessage.ProductList, BuildProductListForReplace(message.CustId));

            var hotAnnouncement = announcementService.GetHotAnnouncement(parameters);

            return new HotAnnouncementViewModel(hotAnnouncement);
        }

        private string BuildProductListForReplace(int custId)
        {
            var products = productService.GetOfferedProductsByCustomer(custId);

            var productNamesBuilder = new StringBuilder("<ol>");
            int index = 0;
            foreach (var name in products)
            {
                productNamesBuilder.Append("<li>")
                    .Append(++index)
                    .Append(". ")
                    .Append(name)
                    .Append("</li>");
            }

            productNamesBuilder.Append("</ol>");

            return productNamesBuilder.ToString();
        }
    }
}