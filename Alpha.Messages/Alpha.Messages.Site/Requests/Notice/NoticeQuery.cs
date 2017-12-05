#pragma warning disable 618, 612

namespace Alpha.Messsages.Site.Requests.Notice
{
    using System;
    using System.Collections.Generic;
    using MediatR;
    using Messages.Configurations;
    using Messages.Models.Constants;
    using Messages.Site.ViewModels;
    using Sunplus.Agent.Model.Entities;

    public class NoticeQuery : IRequest<NoticeMessagesViewModel>
    {
        private readonly Dictionary<byte, string> parentSitesByLevel;

        private readonly byte roleId;

        public NoticeQuery(
            IUserSession userSession,
            DateTime lastPostTime,
            int totalMessage,
            bool isTest)
        {
            parentSitesByLevel = InitiateParentSitesByLevel();

            roleId = userSession.RoleId;
            CustId = userSession.CustId;
            CurrentLang = userSession.Language;
            LastPostTime = lastPostTime;
            TotalMessage = totalMessage;
            IsTest = isTest;
        }

        public byte Level => roleId;

        public int CustId { get; }

        public DateTime LastPostTime { get; }

        public int TotalMessage { get; }

        public bool IsTest { get; }

        public string CurrentLang { get; }

        public string ParentSites => parentSitesByLevel[Level];

        private Dictionary<byte, string> InitiateParentSitesByLevel()
        {
            return new Dictionary<byte, string>
            {
                [UserLevel.Super] = string.IsNullOrWhiteSpace(AppSettings.MessageSuper) ? "3000,3100" : AppSettings.MessageSuper,
                [UserLevel.Master] = string.IsNullOrWhiteSpace(AppSettings.MessageMaster) ? "3000,3200" : AppSettings.MessageMaster,
                [UserLevel.Agent] = string.IsNullOrWhiteSpace(AppSettings.MessageAgent) ? "3000,3300" : AppSettings.MessageAgent
            };
        }
    }
}