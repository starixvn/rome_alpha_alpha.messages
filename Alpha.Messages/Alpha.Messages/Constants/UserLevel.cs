namespace Alpha.Messages.Models.Constants
{
    public static class UserLevel
    {
        public const int Member = 1;
        public const int Agent = 2;
        public const int Master = 3;
        public const int Super = 4;

        public static bool IsMemberLevel(int level)
        {
            return Member == level;
        }

        public static bool IsAgentLevel(int level)
        {
            return Agent == level;
        }

        public static bool IsMasterLevel(int level)
        {
            return Master == level;
        }

        public static bool IsSuperLevel(int level)
        {
            return Super == level;
        }
    }
}
