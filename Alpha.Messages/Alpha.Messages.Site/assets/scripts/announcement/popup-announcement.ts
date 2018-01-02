module PopupAnnouncement {
    export class Base {
        topWindow: any;
        protected $: JQueryStatic;
        protected announcementCookie: AnnouncementCounter;
        protected shownMessageIds: Array<string> = [];
        protected selectedMessageId: string = "";
        protected announcementSessionKey: string = "AnnouncementIds";
        protected domElements = {
            announcementModal: null,
            announcementIframe: null,
            announcementSections: null,
            announcementCookieId: null,
            listAction: null,
        };

        constructor($: JQueryStatic, announcementCookie: AnnouncementCounter, topWindow: any) {
            this.$ = $;
            this.topWindow = topWindow;
            this.announcementCookie = announcementCookie;
        }

        init = () => {
            this.cacheElements();
            this.announcementCookie.init(this.domElements.announcementCookieId.val());

            if (this.hasEmptyAnnouncementData()) {
                return;
            }

            this.showAnnouncementBase();
        };

        cacheElements = () => {
            this.domElements.announcementModal = this.topWindow.$('#announcement-modal');
            this.domElements.announcementIframe = this.topWindow.$('#annoucement-iframe');
            this.domElements.announcementSections = this.$('.js-announcement');
            this.domElements.announcementCookieId = this.$('#announcement-cookies-id');
            this.domElements.listAction = this.$('.list-action');
        };

        hasEmptyAnnouncementData = (): boolean => {
            var isEmpty = true;
            for (var i = 0; i < this.domElements.announcementSections.length; i++) {
                var announcementId = this.$(this.domElements.announcementSections[i]).find('#AnnouncementId').val();
                var isExcess = this.announcementCookie.isExcess(announcementId);
                if (!isExcess) {
                    isEmpty = false;
                }
            }

            return isEmpty;
        };

        showAnnouncementBase = () => {
            this.getSelectedMessageId();
            this.displayAnnouncementModal(this.selectedMessageId);
        };

        getSelectedMessageId = () => {
            for (var i = 0; i < this.domElements.announcementSections.length; i++) {
                var id = this.$(this.domElements.announcementSections[i]).find('#AnnouncementId').val();
                if (!this.announcementCookie.isExcess(id)) {
                    var object = sessionStorage.getItem(this.announcementSessionKey);
                    if (object != null) {
                        this.shownMessageIds = JSON.parse(object);
                    }

                    if (this.shownMessageIds.indexOf(id) == -1) {
                        this.shownMessageIds.push(id);
                        this.selectedMessageId = id;

                        var objectString = JSON.stringify(this.shownMessageIds);
                        sessionStorage.setItem(this.announcementSessionKey, objectString);

                        return;
                    }
                }
            }

            this.selectedMessageId = "";
        };

        displayAnnouncementModal = (id: string) => {
            if (this.selectedMessageId != "" && !this.announcementCookie.isExcess(id)) {
                let announcementSource = this.$("#announcement_" + id).find('#AnnouncementSource').val();
                if (announcementSource) {
                    this.setIframeSrc(announcementSource);
                } else {
                    this.calculateIframeHeight(id);
                }

                this.domElements.announcementModal.removeClass('hidden');
                this.domElements.announcementModal
                    .modal({ show: true, backdrop: false })
                    .tinyDraggable({ exclude: 'button.icon.icon-close.close, .modal-body, p, a' });

                this.increaseCounter(id);
            }
        };

        setIframeSrc = (url: string) => {
            var iframe = this.domElements.announcementIframe;
            iframe.attr("src", url);
        };

        hideAnnouncement = (id: string) => {
            this.$("#announcement_" + id).addClass('hidden');
            this.domElements.announcementModal.addClass('hidden');
        };

        calculateIframeHeight = (id: string) => {
            let currentAnnouncement = this.$("#announcement_" + id);
            currentAnnouncement.removeClass('hidden')

            let announcementContent = this.domElements.announcementIframe;
            announcementContent[0].onload = () => {
                let contentHeight = announcementContent.contents().find("body").height();
                let iframeHeightPadding = 10;
                let iframeHeight = contentHeight + iframeHeightPadding;
                announcementContent.css("height", iframeHeight);
            };
        };

        increaseCounter = (id: string) => {
            var announcementId = this.$("#announcement_" + id).find("#AnnouncementId").val();
            this.announcementCookie.increaseCounter(announcementId);
        };
    }
}

$(document).ready(() => {
    let counter = new PopupAnnouncement.AnnouncementCounter($);
    let currentWindowWidth = window.screen.width;

    if (currentWindowWidth < 768) {
        const mobileAnnouncement = new PopupAnnouncement.Mobile($, counter, window.top);
        mobileAnnouncement.init();
    } else {
        const announcement = new PopupAnnouncement.Base($, counter, window.top);
        announcement.init();
    }
})