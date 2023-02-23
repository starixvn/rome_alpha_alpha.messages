declare var Clipboard: any;

"use strict";

class BackupDomainsView {
    $: JQueryStatic;
    $tableReport: JQuery;
    speedTestPath: string;
    clipboard: any;

    domains = [];

    needSorting = true;
    iDomain = 0;
    isCalculating = false;
    timeout = 20000;
    downloadTimes = 5;

    constructor($: JQueryStatic, clipboard: any) {
        this.$ = $;
        this.clipboard = clipboard;
    }

    init = () => {
        this.cacheElements();

        this.bindCopyDomain();
        this.$('.icon-refresh').click(() => {
            this.$.blockUI();
            this.refreshStatus();
        });

        this.isCalculating = true;
        this.calculateNextSpeed();

        setInterval(() => {
            if (!this.isCalculating) { this.refreshStatus(); }
        }, 6000);
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

    calculateNextSpeed = () => {
        if (this.iDomain === this.domains.length) {
            this.renderDomains();
            return;
        }

        var d = this.domains[this.iDomain];

        this.calculateArgSpeed(0, d.Domain, 0, (speed) => {
            if (speed < 0) {
                d.Error = true;
            }

            d.Speed = speed;
            this.iDomain++;
            this.calculateNextSpeed();
        });
    };

    calculateArgSpeed = (index, url, speed, callback) => {
        if (index === this.downloadTimes) {
            callback(speed / this.downloadTimes);
            return;
        }

        var startTime = (new Date()).getTime();
        var download = new Image();
        download.onload = () => {
            var endTime = (new Date()).getTime();
            speed += endTime - startTime;
            this.calculateArgSpeed(index + 1, url, speed, callback);
        };

        download.onerror = (ev: ErrorEvent) => {
            callback(-1);
        };

        startTime = (new Date()).getTime();
        download.src = 'http://' + url + this.speedTestPath + "?v=" + startTime;
    };

    renderDomains = () => {
        if (this.domains.length === 0) {
            this.$tableReport.removeClass('hide');
            this.$.unblockUI();
            return;
        }

        this.domains.sort(this.sortDomains);

        if (!this.domains.some(d => d.OldSpeed !== d.Speed)) {
            this.$.unblockUI();
            return;
        }

        var html = '';
        for (var i = 0; i < this.domains.length; i++) {
            var d = this.domains[i];
            var status = this.getStatus(i, d);
            var isInUse = d.InUse;
            var padding = this.domains.some(d => d.InUse) ? '<span class="div-icon"></span>' : '';

            html += `<tr data-domain="` + d.Domain + `">
                        <td class="center">` + (i + 1) + `</td>
                        <td class="center">`+
                `<input type="text" class="form-control txt-domain lnk" id="domain-` + i + `" autocomplete="off" readonly value="` + this.$('#domain').val() + ' ' + (i + 1) + `" data-url="` + d.Domain + `" data-isdown="` + (d.Error ? 1 : 0) + `" />` +
                (isInUse ? '<span class="icon icon-check" title="' + this.$('#inUse').val() + '" data-original-title="' + this.$('#inUse').val() + '"></span>' : padding) +
                `</td>
                        <td class="center status-`+ status + `">` + this.$('#' + status).val() + `</td>
                        <td class="center"><div class="icon icon-content-copy icon-content-copy-`+ i + `" data-clipboard-action="copy" data-clipboard-target="#domain-` + i + `" title="` + this.$('#copyDomain').val() + `"></div></td>
                     </tr>`;

            d.OldSpeed = d.Speed;
        }

        this.$tableReport.find('tbody').empty().html(html);
        var self = this;
        this.$('.txt-domain').click(function () {
            var isDown = self.$(this).data('isdown');
            var d = self.$(this).data('url');

            window.open('http://' + d);
        });

        this.bindCopyDomain();
        this.$('.icon-check').mouseenter(function () { self.$(this).tooltip('show'); });
        this.$('.icon-check').mouseleave(function () { self.$(this).tooltip('hide'); });

        this.$tableReport.removeClass('hide');
        this.isCalculating = false;
        this.$.unblockUI();
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
        this.iDomain = 0;
        this.isCalculating = true;
        this.calculateNextSpeed();
    };

    sortDomains = (a, b) => {
        if ((a.Error && b.Error) ||
            (a.Timeout == 1 && b.Timeout == 1)) {
            return 0;
        }

        if (a.Error) {
            return 1;
        }

        if (b.Error) {
            return -1;
        }

        if (a.Timeout) {
            return 1;
        }

        if (b.Timeout) {
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
    isInUse = (d) => window.location.href.indexOf(d.Domain) !== -1;
}

$(function () {
    const view = new BackupDomainsView($, Clipboard);
    view.init();
});