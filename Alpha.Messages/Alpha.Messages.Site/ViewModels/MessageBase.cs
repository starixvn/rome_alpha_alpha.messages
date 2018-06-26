namespace Alpha.Messages.Site.ViewModels
{
    using Alpha.Messages.Models.Message;
    using Models.Constants;

    public class MessageBase
    {
        public MessageBase(
            int messageType,
            byte roleId,
            NumberOfUnreadMessage unreadMessage)
        {
            RoleId = roleId;
            MessageType = messageType;
            UnreadMessage = unreadMessage ?? new NumberOfUnreadMessage();
        }

        public NumberOfUnreadMessage UnreadMessage { get; }

        public int MessageType { get; protected set; }

        public byte RoleId { get; }

        public string RoleName => UserLevel.GetDisplayName(RoleId);
    }
}