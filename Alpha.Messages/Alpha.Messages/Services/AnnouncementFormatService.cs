namespace Alpha.Messages.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Web.Helpers;
    using Announcements.UnsynchronizedSetting.Specs;
    using Constants;
    using Models.Announcement;
    using Models.Constants;
    using Params.Announcement;
    using RemotingRef;

    public interface AnnouncementFormatService
    {
        IEnumerable<AlphaAnnouncement> Format(IEnumerable<AlphaAnnouncement> announcements, BaseAnnouncementsParams message);
        AlphaAnnouncement Format(AlphaAnnouncement announcement, BaseAnnouncementsParams message);
    }

    public class AnnouncementFormatServiceImpl : AnnouncementFormatService
    {
        internal Dictionary<string, Func<AlphaAnnouncement, BaseAnnouncementsParams, string>> ReplacementCode { get; set; }
        private static Dictionary<int, string> customerListUrls = new Dictionary<int, string> {
            {UserLevel.Agent, "_MemberInfo/CustomerList/Agent/MemberList.aspx" },
            {UserLevel.Master, "_MemberInfo/CustomerList/Master/AgentList.aspx" },
            {UserLevel.Super, "_MemberInfo/CustomerList/Super/MasterList.aspx" }
        };

        public AnnouncementFormatServiceImpl()
        {
            ReplacementCode = new Dictionary<string, Func<AlphaAnnouncement, BaseAnnouncementsParams, string>>
            {
                {"72973", RenderUnsyncedProducts}
            };
        }

        public AlphaAnnouncement Format(AlphaAnnouncement announcement, BaseAnnouncementsParams message)
        {
            if (announcement != null)
            {
                if (ReplacementCode.ContainsKey(announcement.Code))
                {
                    announcement.Content = ReplacementCode[announcement.Code](announcement, message);
                }

                announcement.Content = ReplaceMessageSiteTag(announcement.Content, message.IsMobileDevice);
            }

            return announcement;
        }

        public IEnumerable<AlphaAnnouncement> Format(IEnumerable<AlphaAnnouncement> announcements, BaseAnnouncementsParams message)
            => announcements.Select(i => Format(i, message));

        private static string ReplaceMessageSiteTag(string content, bool isMoblie)
            => content.Replace(MessageConstants.SiteMessageTag, isMoblie ? MicroSites.MessageUrl("~") : MicroSites.MessageUrl());

        private static string RenderUnsyncedProducts(AlphaAnnouncement annoucement, BaseAnnouncementsParams param)
        {
            var content = annoucement.Content;
            var unsynchronizedSettingSpecification = annoucement.Specifications.FirstOrDefault(rule => rule.GetType() == typeof(UnsynchronizedSettingSpecification));

            if (unsynchronizedSettingSpecification != null)
            {
                var unsynchronizedSetting = (UnsynchronizedSettingSpecification)unsynchronizedSettingSpecification;
                content = content.Replace(
                    MessageConstants.ProductsTag,
                    BuildProductTag(unsynchronizedSetting.ProductIds, param));
            }

            return content;
        }

        private static string BuildProductTag(IEnumerable<int> productIds, BaseAnnouncementsParams param)
        {
            var url = MicroSites.OldMainUrl(customerListUrls[param.RoleId]);

            return string.Join(
                string.Empty,
                productIds.Select(productId => $"<div><a href='{url}?productUnsynchronized={productId}' target='main'>{CacheRefFactory.CacheRef.GetProductName(productId)}</a></div>"));
        }
    }
}