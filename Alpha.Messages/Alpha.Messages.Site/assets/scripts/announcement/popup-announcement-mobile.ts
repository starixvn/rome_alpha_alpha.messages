module PopupAnnouncement {
    export class Mobile extends Base {
        init = () => {
            this.cacheElements();
            this.showRedCircleInIconAnnouncement(false);
            this.announcementCookie.init(this.domElements.announcementCookieId.val());

            if (this.hasEmptyAnnouncementData()) {
                this.showEmptyAnnoucementNotification(this.topWindow.$('#no-info').val());
                this.showRedCircleInIconAnnouncement(false);
            } else {
                this.showRedCircleInIconAnnouncement();
                this.showAnnouncementBase();
            }

            this.bindEvents();
        };

        bindEvents = () => {
            this.$(".prevButton").on('click', () => {
                this.goToPrevAnnouncement();
            });

            this.$(".nextButton").on('click', () => {
                this.goToNextAnnouncement();
            });
        };

        goToNextAnnouncement = () => {
            this.hideAnnouncement(this.selectedMessageId);

            var nextAnnouncement = this.$('#announcement_' + this.selectedMessageId).next();
            if (!nextAnnouncement) return;

            var id = this.$(nextAnnouncement).find('#AnnouncementId').val();
            if (!id) return;

            this.selectedMessageId = id;
            if (this.shownMessageIds.indexOf(id) == -1) {
                this.shownMessageIds.push(id);
            }

            this.displayAnnouncementModal(this.selectedMessageId);
        };

        goToPrevAnnouncement = () => {
            this.hideAnnouncement(this.selectedMessageId);

            var prevAnnouncement = this.$('#announcement_' + this.selectedMessageId).prev();
            if (!prevAnnouncement) return;

            var id = this.$(prevAnnouncement).find('#AnnouncementId').val();
            if (!id) return;

            this.selectedMessageId = id;
            if (this.shownMessageIds.indexOf(id) == -1) {
                this.shownMessageIds.push(id);
            }

            this.displayAnnouncementModal(this.selectedMessageId);
        };

        private showRedCircleInIconAnnouncement = (isShow: boolean = true) => {
            if (isShow) {
                this.topWindow.$('.icon-bell').find('.circle-red').show();
            } else {
                this.topWindow.$('.icon-bell').find('.circle-red').hide();
            }
        };

        private showEmptyAnnoucementNotification = (text: string) => {
            this.topWindow.toastr.options.positionClass = 'toast-top-full-width';
            this.topWindow.toastr.options.timeOut = 2000;
            this.topWindow.toastr.options.preventDuplicates = true;
            this.topWindow.toastr.info(text);
        };
    }
}