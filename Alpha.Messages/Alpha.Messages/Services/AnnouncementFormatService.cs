namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using Alpha.Web.Helpers;
    using Models.Announcement;
    using Params.Announcement;
    using Params;
    using Web.Helpers;
    using System.Linq;
    using Announcements.Models;
    using Announcements.Specs;

    public interface AnnouncementFormatService
    {
        IEnumerable<HotAnnouncement> Format(IEnumerable<HotAnnouncement> announcements, GetHotAnnouncementsParams message);
    }

    public class AnnouncementFormatServiceImpl : AnnouncementFormatService
    {
        public Dictionary<string, Func<string, GetHotAnnouncementsParams, IEnumerable<ISpecification<IParam>>, string>> ReplacementCode { get; set; }
        public const string SiteMain = "{site-main}";
        public const string ProductsTag = "{products}";

        private static Dictionary<int, string> customerListUrls = new Dictionary<int, string> {
            {2, "_MemberInfo/CustomerList/Agent/MemberList.aspx" },
            {3, "_MemberInfo/CustomerList/Master/AgentList.aspx" },
            {4, "_MemberInfo/CustomerList/Super/MasterList.aspx" }
        };

        public AnnouncementFormatServiceImpl()
        {
            ReplacementCode = new Dictionary<string, Func<string, GetHotAnnouncementsParams, IEnumerable<ISpecification<IParam>>, string>>();

            // Add Code & proccessed Function to replace
            ReplacementCode.Add("2222", ReplaceSiteMain);

            ReplacementCode.Add("8102702", RenderUnsyncedProducts);
        }

        public IEnumerable<HotAnnouncement> Format(IEnumerable<HotAnnouncement> announcements, GetHotAnnouncementsParams message)
        {
            foreach (var announcement in announcements)
            {
                var containsCode = ReplacementCode.ContainsKey(announcement.Code);
                if (containsCode)
                {
                    announcement.Content = ReplacementCode[announcement.Code](announcement.Content, message, announcement.Specifications);
                }

                yield return announcement;
            }
        }

        private static string ReplaceSiteMain(string content, GetHotAnnouncementsParams param, IEnumerable<ISpecification<IParam>> specs)
            => content.Replace(SiteMain, param.IsMobileDevice ? MicroSites.MainUrl("~") : string.Empty);

        private static string RenderUnsyncedProducts(string content, GetHotAnnouncementsParams param, IEnumerable<ISpecification<IParam>> specs)
        {
            StringBuilder builder = new StringBuilder();
            var spec = specs.FirstOrDefault(rule => rule.GetType() == typeof(UnsynchronizedSettingSpecification));
            var unsynchronizedSettingSpecification = (UnsynchronizedSettingSpecification)spec;
            foreach (var product in unsynchronizedSettingSpecification.Products)
            {
                var hrefLink = $"<div><a href='{MicroSites.OldMainUrl("~") + customerListUrls[param.RoleId]}?productId={product.Key}' target='main'>";
                builder.Append(hrefLink);
                builder.Append(product.Value);
                builder.Append("</a></div>");
            }

            return content.Replace(ProductsTag, builder.ToString());
        }
    }
}