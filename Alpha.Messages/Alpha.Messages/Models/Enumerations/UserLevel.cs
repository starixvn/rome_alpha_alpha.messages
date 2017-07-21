using System;

namespace Alpha.Messages.Models.Enumerations
{
    [Serializable]
    public class UserLevel : Enumeration
    {        
        public static readonly UserLevel Super = new UserLevel(4, "Super");     
        public static readonly UserLevel Master = new UserLevel(3, "Master");        
        public static readonly UserLevel Agent = new UserLevel(2, "Agent");        
        public static readonly UserLevel Member = new UserLevel(1, "Member");

        public UserLevel()
        {
        }

        private UserLevel(byte value, string displayName)
            : base(value, displayName)
        {
        }

        public bool IsAgentLevel()
        {
            return this == Agent;
        }

        public bool IsNotAgentLevel()
        {
            return !IsAgentLevel();
        }       
    }
}
