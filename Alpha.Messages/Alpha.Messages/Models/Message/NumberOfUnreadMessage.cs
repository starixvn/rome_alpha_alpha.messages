using System;

namespace Alpha.Messages.Models.Message
{
    [Serializable]
    public class NumberOfUnreadMessage
    {
        public NumberOfUnreadMessage()
        {
        }

        public NumberOfUnreadMessage(int normal, int special, int system, int personal)
        {
            Normal = normal;
            Special = special;
            System = system;
            Personal = personal;
        }

        public int Normal { get; }
        public int Special { get; }
        public int System { get; }
        public int Personal { get; }

        public int Total => Normal + Special + System + Personal;
    }
}
