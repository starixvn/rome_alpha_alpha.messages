namespace Alpha.Messages.Models.Enumerations
{
    public static class UserLevel
    {
        private const int Member = 1;
        private const int Agent = 2;
        private const int Master = 3;
        private const int Super = 4;

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
