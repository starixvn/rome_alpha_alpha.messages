namespace Alpha.Messages.Params.Message
{
    public class UpdatePrivateMessageAsReadParam
    {
        public UpdatePrivateMessageAsReadParam(int userId, string userName, bool isInternal)
        {
            UserId = userId;
            UserName = userName;
            IsInternal = isInternal;
        }

        public int UserId { get; }

        public string UserName { get; }

        public bool IsInternal { get; }
    }
}
