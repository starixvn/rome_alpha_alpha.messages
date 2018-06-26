class Message {
    private $: JQueryStatic;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    registerEvents = () => {
        var self = this;
        this.$('.nav-tabs > li').on('click', function () {
            self.$.blockUI();
            self.$(this).find('span').val('');
        });
        this.$('.btn-submit').on('click', () => {
            this.$.blockUI();
        });

        this.initDateTimePicker('#fromDateContainer');
        this.initDateTimePicker('#toDateContainer');
    }

    private initDateTimePicker = (id: string) => {
        this.$(id).datetimepicker({
            allowInputToggle: true,
            format: 'MM/DD/YYYY'
        });
    }

}

$(() => {
    const message = new Message($);
    message.registerEvents();
});