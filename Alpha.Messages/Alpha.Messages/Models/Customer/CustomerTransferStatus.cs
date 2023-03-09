namespace Alpha.Messages.Models.Customer
{
    public class CustomerTransferStatus
    {
        public CustomerTransferStatus()
        {
        }

        public CustomerTransferStatus(int custId, bool isAllDayBalTransfer)
        {
            CustId = custId;
            IsAllDayBalTransfer = isAllDayBalTransfer;
        }

        public int CustId { get; }

        public bool IsAllDayBalTransfer { get; }
    }
}
