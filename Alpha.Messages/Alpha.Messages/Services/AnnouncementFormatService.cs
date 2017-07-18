using Alpha.Messages.Constants;
using Alpha.Messages.Models;
using Alpha.Messages.Params;
using Alpha.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alpha.Messages.Services
{
    public interface AnnouncementFormatService
    {
        IEnumerable<HotAnnouncement> Format(IEnumerable<HotAnnouncement> announcements, GetHotAnnouncementsParams message);

    }

    public class AnnouncementFormatServiceImpl : AnnouncementFormatService
    {
        private readonly Dictionary<string, Func<string, GetHotAnnouncementsParams, string>> replacementCode;
        private readonly ProductService productService;

        public AnnouncementFormatServiceImpl(ProductService productService)
        {
            this.productService = productService;
            replacementCode = new Dictionary<string, Func<string, GetHotAnnouncementsParams, string>>();
            replacementCode.Add("1111", ReplaceOfferedProducts);
            replacementCode.Add("2222", ReplaceSiteMain);
        }

        public IEnumerable<HotAnnouncement> Format(IEnumerable<HotAnnouncement> announcements, GetHotAnnouncementsParams message)
        {
            foreach (var announcement in announcements)
            {
                var containsCode = replacementCode.ContainsKey(announcement.Code);
                if (containsCode)
                {
                    announcement.Content = replacementCode[announcement.Code](announcement.Content, message);
                }

                yield return announcement;
            }
        }

        private static string ReplaceSiteMain(string content, GetHotAnnouncementsParams param)
        {
            return content.Replace(TagMessage.SiteMain, param.IsMobileDevice ? MicroSites.MainUrl("~") : string.Empty);
        }

        private string ReplaceOfferedProducts(string content, GetHotAnnouncementsParams message)
        {
            var products = productService.GetOfferedProductsByCustomer(message.CustId);

            var productNamesBuilder = new StringBuilder("<ol>");
            int index = 0;
            foreach (var product in products)
            {
                productNamesBuilder.Append("<li>")
                    .Append(++index)
                    .Append(". ")
                    .Append(product.Name)
                    .Append("</li>");
            }

            productNamesBuilder.Append("</ol>");

            return content.Replace(TagMessage.ProductList, productNamesBuilder.ToString());
        }
    }
}