namespace Alpha.Messages.Models.Customer
{
    using System.Collections.Generic;

    public class Customer
    {
        private readonly IDictionary<int, string> systems = new Dictionary<int, string>
        {
            [1] = "IBCBET",
            [7] = "789Y",
            [8] = "EU88",
            [9] = "Y987",
        };

        public Customer()
        {
        }

        public Customer(
            int custId,
            string userName,
            byte roleId,
            byte currencyId,
            int aRecommendId,
            int mRecommendId,
            int sRecommendId,
            bool isIMBC,
            string site,
            bool isSyncedCasino,
            int siteCode)
        {
            CustId = custId;
            UserName = userName;
            RoleId = roleId;
            CurrencyId = currencyId;
            ARecommendId = aRecommendId;
            MRecommendId = mRecommendId;
            SRecommendId = sRecommendId;
            Site = site;
            IsSyncedCasino = isSyncedCasino;
            SiteCode = siteCode;
        }

        public int CustId { get; }

        public string UserName { get; }

        public byte RoleId { get; }

        public byte CurrencyId { get; }

        public int ARecommendId { get; }

        public int MRecommendId { get; }

        public int SRecommendId { get; }

        public string Site { get; }

        public bool IsIMBC => Site.Equals("IMBC");

        public bool IsSyncedCasino { get; }

        public bool IsAllDayBalTransfer { get; }

        public int SiteCode { get; }

        public string System => systems[SiteCode];
    }
}