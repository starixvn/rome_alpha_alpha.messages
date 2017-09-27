namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using Alpha.Web.Helpers;
    using System.Linq;
    using Models.Announcement;
    using Params.Announcement;
    using Announcements.Models;
    using Announcements.Specs;
    using Params;
    using RemotingRef;
    using Web.Helpers;

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
            // Add Code & proccessed Function to replace
            ReplacementCode = new Dictionary<string, Func<string, GetHotAnnouncementsParams, IEnumerable<ISpecification<IParam>>, string>>
            {
                {"2222", ReplaceSiteMain },
                {"8102702", RenderUnsyncedProducts}
            };
        }

        public IEnumerable<HotAnnouncement> Format(
            IEnumerable<HotAnnouncement> announcements, 
            GetHotAnnouncementsParams message)
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

        private static string ReplaceSiteMain(
            string content, 
            GetHotAnnouncementsParams param, 
            IEnumerable<ISpecification<IParam>> specs)
            => content.Replace(SiteMain, param.IsMobileDevice ? MicroSites.MainUrl("~") : string.Empty);

        private static string RenderUnsyncedProducts(
            string content, 
            GetHotAnnouncementsParams param, 
            IEnumerable<ISpecification<IParam>> specs)
        {
            var unsynchronizedSettingSpecification = (UnsynchronizedSettingSpecification)specs.FirstOrDefault(rule => rule.GetType() == typeof(UnsynchronizedSettingSpecification));

            if (unsynchronizedSettingSpecification != null)
            {
                return content.Replace(ProductsTag, BuildProductTag(unsynchronizedSettingSpecification, param));
            }

            return content;
        }

        private static string BuildProductTag(
            UnsynchronizedSettingSpecification unsynchronizedSettingSpecification,
            GetHotAnnouncementsParams param)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var productId in unsynchronizedSettingSpecification.ProductIds)
            {
                var hrefLink = $"{MicroSites.OldMainUrl("~") + customerListUrls[param.RoleId]}?productUnSync={productId}";
                builder.Append($"<div><a href='{hrefLink}' target='main'>{CacheRef.GetProductName(productId)}</a></div>");
            }

            return builder.ToString();
        }
    }
}