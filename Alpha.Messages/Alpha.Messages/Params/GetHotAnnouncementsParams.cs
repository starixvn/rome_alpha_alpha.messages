
using Alpha.Announcements.Models;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Messages.Params
{
    public class GetHotAnnouncementsParams
    {
        public GetHotAnnouncementsParams() : this(null)
        {
        }

        public GetHotAnnouncementsParams(Dictionary<string, string> replacement)
        {
            Replacement = replacement ?? new Dictionary<string, string>();
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

        public IParam SpecificationParamter { get; set; }

        public Dictionary<string, string> Replacement { get; }

        public void AddReplacement(string key, string value)
        {
            if (Replacement.Any(i => i.Key == key))
            {
                Replacement[key] = value;
            }
            else
            {
                Replacement.Add(key, value);
            }
        }
    }
}
