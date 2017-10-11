namespace Alpha.Messages.Params.Announcement
{
    using System;
    using Alpha.Announcements.Models;

    public class GetHotAnnouncementsParams
    {
        public GetHotAnnouncementsParams()
        {
            NoticeTypes = Array.Empty<string>();
        }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public byte RoleId { get; set; }

        public int AdminId { private get; set; }

        public bool IsUserAdmin
        {
            get
            {
                return AdminId != 0;
            }
        }

        public int CustId { get; set; }

        public int ARecommendId { get; set; }

        public int MRecommendId { get; set; }

        public int SRecommendId { get; set; }

        public bool IsSuper { get; set; }

        public bool IsMaster { get; set; }

        public bool IsAgent { get; set; }

        public bool IsIMBCCustomer { get; set; }

        public string Language { get; set; }

        public string UserName { get; set; }

        public string Site { get; set; }

        public string[] NoticeTypes { get; set; }

        public bool IsMobileDevice { get; set; }

        public IParam SpecificationParamter { get; set; }        
    }
}
