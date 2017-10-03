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
        private static Dictionary<int, string> customerListUrls = new Dictionary<int, string> {
            {2, "_MemberInfo/CustomerList/Agent/MemberList.aspx" },
            {3, "_MemberInfo/CustomerList/Master/AgentList.aspx" },
            {4, "_MemberInfo/CustomerList/Super/MasterList.aspx" }
        };

        public const string SiteMain = "{site-main}";
        public const string ProductsTag = "{products}";

        public AnnouncementFormatServiceImpl()
        {
            // Add Code & proccessed Function to replace
            ReplacementCode = new Dictionary<string, Func<string, GetHotAnnouncementsParams, IEnumerable<ISpecification<IParam>>, string>>
            {
                {"2222", ReplaceSiteMain },
                {"72973", RenderUnsyncedProducts}
            };
        }
        public Dictionary<string, Func<string, GetHotAnnouncementsParams, IEnumerable<ISpecification<IParam>>, string>> ReplacementCode { get; set; }

        public IEnumerable<HotAnnouncement> Format(
            IEnumerable<HotAnnouncement> announcements, 
            GetHotAnnouncementsParams message)
        {
            foreach (var announcement in announcements)
            {
                if (ReplacementCode.ContainsKey(announcement.Code))
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
            var url = $"{MicroSites.OldMainUrl("~") + customerListUrls[param.RoleId]}";

            return string.Join(string.Empty, unsynchronizedSettingSpecification.ProductIds
                .Select(productId => $"<div><a href='{url}?productUnsynchronized={productId}' target='main'>{CacheRefFactory.CacheRef.GetProductName(productId)}</a></div>"));;
        }
    }
}