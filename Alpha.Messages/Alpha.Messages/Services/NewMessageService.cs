namespace Alpha.Messages.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Criteria.Message;
    using Criteria.NewMessage;
    using Fanex.Data.Repository;
    using Models.Announcement;
    using Models.Constants;
    using Models.Message;
    using Params.Announcement;

    public interface NewMessageService
    {
        NumberOfUnreadMessage GetNumberOfUnreadMessage(BaseAnnouncementsParams announcementParams);

        NumberOfUnreadMessage CheckNewUnreadMessage(NumberOfNewMessageParams announcementParams);

        void MoveNewMessage(int custId, string custName, int levelId, bool isUserAdmin);
    }

    public class NewMessageServiceImpl : NewMessageService
    {
        private const int FishingWorld = 3;
        private readonly IDynamicRepository dynamicRepository;
        private readonly AnnouncementService announcementService;

        private readonly ResourceManager resourceManager;
        public NewMessageServiceImpl(IDynamicRepository dynamicRepository
            , ResourceManager resourceManager
            , AnnouncementService announcementService)
        {
            this.dynamicRepository = dynamicRepository;
            this.resourceManager = resourceManager;
            this.announcementService = announcementService;
        }

        public void MoveNewMessage(int custId, string custName, int levelId, bool isUserAdmin)
        {
            var unreadMessagesOnUser = GetUnreadMessagesOnUser(custId, levelId, isUserAdmin);
            foreach (var messageType in unreadMessagesOnUser)
            {
                var key = "Notice_" + messageType;
                InsertMessageToAlpha(key, custId, custName);

                UpdateMessageAsReadOnUser(custId, messageType);
            }
        }

        public NumberOfUnreadMessage CheckNewUnreadMessage(NumberOfNewMessageParams announcementParams)
        {
            return announcementParams.UnreadMessage != null && announcementParams.UnreadMessage.Total >= MessageConstants.DefaultTotalMessageHistory ?
                announcementParams.UnreadMessage :
                GetNumberOfUnreadMessage(announcementParams);
        }

        public NumberOfUnreadMessage GetNumberOfUnreadMessage(BaseAnnouncementsParams announcementParams)
        {
            var numberUnreadMessage = new NumberOfUnreadMessage();
            var unreadMessage = dynamicRepository.Get<NumberOfUnreadMessage>(
                new GetNumberOfUnreadMessageCriteria(
                    announcementParams.CustId,
                    announcementParams.SubAccId,
                    announcementParams.ParentSites,
                    announcementParams.Site
                    )
                );

            if (CustomerHasInitData(unreadMessage))
            {
                var unreadAnnouncement = announcementService.GetNumberOfNewAnnouncements(announcementParams);
                numberUnreadMessage = new NumberOfUnreadMessage(
                unreadMessage.Normal,
                unreadMessage.Special,
                unreadAnnouncement,
                unreadMessage.Personal);
            }


            return numberUnreadMessage;
        }

        private static bool CustomerHasInitData(NumberOfUnreadMessage unreadMessage)
            => unreadMessage != null;

        private void UpdateMessageAsReadOnUser(int custId, int messageType)
            => dynamicRepository.Fetch<bool>(new UpdateMessageAsReadOnUserCritria(custId, messageType));

        private void InsertMessageToAlpha(string key, int custId, string custName)
        {
            var newMessage = dynamicRepository.Fetch<int>(new InsertPersonalMessageCriteria(key, resourceManager));
            if (newMessage.Any())
            {
                var messageId = newMessage.FirstOrDefault();
                dynamicRepository.Fetch<bool>(new AssignMessageToUserCriteria(messageId, custId, custName));
            }
        }

        private IEnumerable<int> GetUnreadMessagesOnUser(int custId, int levelId, bool isUserAdmin)
        {
            var newMessage = Enumerable.Empty<int>();
            if (!isUserAdmin)
            {
                newMessage = dynamicRepository
                    .Fetch<int>(new GetUnreadMessagesOnUserCriteria(custId));

                if (UserLevel.IsNotAgent(levelId))
                {
                    newMessage = newMessage.Where(i => i == FishingWorld);
                }
            }

            return newMessage.Distinct();
        }
    }
}