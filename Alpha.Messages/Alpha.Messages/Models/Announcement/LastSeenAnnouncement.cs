namespace Alpha.Messages.Models.Announcement
{
    using System;
    using Messages.Constants;

    public class LastSeenAnnouncement
    {
        public LastSeenAnnouncement()
        {

        }

        public LastSeenAnnouncement(
            int custId
            , DateTime lastSeenNormal
            , DateTime lastSeenPersonal
            , DateTime lastSeenSpecial
            , DateTime lastSeenSystem)
        {
            CustId = custId;
            LastSeenNormal = lastSeenNormal;
            LastSeenPersonal = lastSeenPersonal;
            LastSeenSystem = lastSeenSystem;
            LastSeenSpecial = lastSeenSpecial;
        }

        public int CustId { get; set; }

        public DateTime LastSeenNormal { get; set; }

        public DateTime LastSeenSpecial { get; set; }

        public DateTime LastSeenSystem { get; set; }

        public DateTime LastSeenPersonal { get; set; }

        public DateTime GetLastSeenByType(int messageType)
        {
            if(messageType == MessageType.Normal)
            {
                return LastSeenNormal;
            }
            else if(messageType == MessageType.Personal)
            {
                return LastSeenPersonal;
            }
            else if(messageType == MessageType.Special)
            {
                return LastSeenSpecial;
            }
            else
            {
                return LastSeenSystem;
            }
        }
    }
}
