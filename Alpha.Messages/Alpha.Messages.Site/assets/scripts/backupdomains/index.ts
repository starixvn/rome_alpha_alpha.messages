declare var Clipboard: any;

"use strict";

class BackupDomainsView {
    $: JQueryStatic;
    $tableReport: JQuery;
    speedTestPath: string;
    clipboard: any;

    domains = [];

    needSorting = true;
    downloadInterval = 60000;
    downloadTimes = 1;
    loadedItems = 0;
    checkSpeedsTimeout = 0;


    constructor($: JQueryStatic, clipboard: any) {
        this.$ = $;
        this.clipboard = clipboard;
    }

    init = () => {
        this.cacheElements();

        this.bindCopyDomain();
        this.$('.icon-refresh').click(() => {
            this.refreshStatus();
        });

        this.loadedItems = 0;
        this.calculateSpeeds();
    };

    cacheElements = () => {
        this.$tableReport = this.$('#tbl-report');
        this.speedTestPath = this.$('#speedTestPath').val();

        this.domains = JSON.parse(this.$('#Domains').val());

        for (var i = 0; i < this.domains.length; i++) {
            var d = this.domains[i];
            d.InUse = this.isInUse(d);
        }
    };

    calculateSpeeds = () => {
        for (var i = 0; i < this.domains.length; i++) {
            var d = this.domains[i];
            d.Speed = 0;
            this.calculateTotalSpeed(d.Domain, i);
        }
    };

    calculateTotalSpeed = (domain, i) => {
        for (var seq = 0; seq < this.downloadTimes; seq++) {
            this.sumSpeeds(domain, i, seq);
        }
    };

    sumSpeeds = (domain, i, seq) => {
        var startTime = (new Date()).getTime();
        var download = new Image();
        var d = this.domains[i];

        download.onload = () => {
            var endTime = (new Date()).getTime();
            d.Speed = d.Speed == -1 ? d.Speed + endTime - startTime : d.Speed;

            this.completeCheckSeq(d);
        };

        download.onerror = (ev: ErrorEvent) => {
            d.Error = true;
            d.Speed = -1;
            this.completeCheckSeq(d);
        };

        startTime = (new Date()).getTime();
        download.src = '//' + domain + this.speedTestPath + "?v=" + startTime;
    };

    completeCheckSeq = (d) => {
        this.loadedItems++;

        if (this.loadedItems !== this.downloadTimes * this.domains.length) {
            return;
        }

        this.renderDomains();
        this.loadedItems = 0;

        if (this.checkSpeedsTimeout) {
            clearTimeout(this.checkSpeedsTimeout);
        }

        this.checkSpeedsTimeout = setTimeout(() => {
            if (this.loadedItems === 0) { this.refreshStatus(); }
        }, this.downloadInterval);
    };

    renderDomains = () => {
        this.domains.sort(this.sortDomains);

        var html = '';
        for (var i = 0; i < this.domains.length; i++) {
            var d = this.domains[i];
            var status = this.getStatus(i, d);
            var isInUse = d.InUse;
            var padding = this.domains.some(d => d.InUse) ? '<span class="div-icon"></span>' : '';

            html += `<tr data-domain="` + d.Domain + `">
                        <td class="center">` + (i + 1) + `</td>
                        <td class="center">`+
                `<input type="text" class="form-control txt-domain lnk" id="domain-` + i + `" autocomplete="off" readonly value="` + (this.$('#domain').val() + ' ' + d.Alias) + `" data-url="` + d.Domain + `" data-isdown="` + (d.Error ? 1 : 0) + `" />` +
                (isInUse ? '<span class="icon icon-check" title="' + this.$('#inUse').val() + '" data-original-title="' + this.$('#inUse').val() + '"></span>' : padding) +
                `</td>
                        <td class="center status-col status-`+ status + `">` + this.$('#' + status).val() + `</td>
                        <td class="center"><div class="icon icon-content-copy icon-content-copy-`+ i + `" data-clipboard-action="copy" data-clipboard-target="#domain-` + i + `" title="` + this.$('#copyDomain').val() + `"></div></td>
                     </tr>`;

            d.OldSpeed = d.Speed;
        }

        this.$tableReport.find('tbody').empty().html(html);
        var self = this;
        this.$('.txt-domain').click(function () {
            var isDown = self.$(this).data('isdown');
            var d = self.$(this).data('url');

            window.open('//' + d);
        });

        this.bindCopyDomain();
        this.$('.icon-check').mouseenter(function () { self.$(this).tooltip('show'); });
        this.$('.icon-check').mouseleave(function () { self.$(this).tooltip('hide'); });

        this.$tableReport.removeClass('hide');
    };

    bindCopyDomain = () => {
        var self = this;
        for (var i = 0; i < this.domains.length; i++) {
            var d = this.domains[i];

            var clipboard = new this.clipboard('.icon-content-copy-' + i, {
                text: function (trigger) { return self.$(trigger).parent().parent().data('domain'); }
            });

            this.copyDomain(clipboard);
        }
    };
    copyDomain = (clipboard) => {
        var self = this;
        if (window.document.queryCommandSupported
            && window.document.queryCommandSupported("copy")) {
            clipboard.on("success", function (e) {
                self.completeCopyDomain(self.$(e.trigger));
            }).on("error", function (e) {
                self.$(e.trigger).remove();
                window.console.error("Action:", e.action);
                window.console.error("Trigger:", e.trigger);
            });
        }
    };
    completeCopyDomain = ($elm) => {
        $elm.attr('title', this.$('#copied').val());
        $elm.tooltip('show');

        setTimeout(() => {
            $elm.tooltip('hide');
            $elm.attr('title', this.$('#copyDomain').val());
        }, 3000);
    };

    refreshStatus = () => {
        this.loadedItems = 0;
        this.$('.status-col').html(this.$('#loading').val() + '...');
        this.calculateSpeeds();
    };

    sortDomains = (a, b) => {
        if (a.Error && b.Error) {
            return 0;
        }

        if (a.Error) {
            return 1;
        }

        if (b.Error) {
            return -1;
        }

        return b.Speed - a.Speed;
    };
    getStatus = (i, d) => {
        if (d.Error) {
            return 'down';
        }

        if (i == 0) {
            return 'very-good';
        }

        if (i == 1) {
            return 'good';
        }

        if (i == this.domains.filter(d => !d.Error).length - 1) {
            return 'bad';
        }

        return 'normal';
    };
    isInUse = (d) => window.location.href.indexOf(d.Domain.replace('www.', '')) !== -1;
}

$(function () {
    const view = new BackupDomainsView($, Clipboard);
    view.init();
});