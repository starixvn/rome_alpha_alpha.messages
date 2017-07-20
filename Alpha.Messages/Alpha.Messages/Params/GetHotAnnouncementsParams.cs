
using Alpha.Announcements.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Messages.Params
{
    public class GetHotAnnouncementsParams
    {
        public GetHotAnnouncementsParams()
        {
            NoticeTypes = Array.Empty<string>();
        }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public byte RoleId { get; set; }

        public int AdminId { get; set; }

        public int CustId { get; set; }

        public string Language { get; set; }

        public string UserName { get; set; }

        public string Site { get; set; }

        public string[] NoticeTypes { get; set; }

        public bool IsMobileDevice { get; set; }

        public IParam SpecificationParamter { get; set; }        
    }
}
