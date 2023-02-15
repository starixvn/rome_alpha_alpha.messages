declare interface Window {
    popupManager: any;
    indexManager: any;
}

"use strict";
class Feedback {
    $: JQueryStatic;
    messageLimit = 4000;
    addFeedbackUrl = "Add";

    feedback = {
        rate: 0,
        message: ''
    };

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init(): void {
        this.bindEvents();
    }

    bindEvents(): void {
        const self: any = this;

        $('.rate').click(function (): void {
            $('.rate').removeClass('active');
            var $this = $(this);
            $this.addClass('active');

            var r = $this.attr('value');
            self.feedback.rate = r;
        });

        $('#buttonLater').click(function (): void {
            window.top.popupManager.close();
        });

        $('#buttonSubmit').click(function (): void {
            self.submitFeedback();
        });
    }

    submitFeedback(): void {
        const self: any = this;
        self.feedback.message = $('.feedback-msg').val();

        if (self.feedback.message.length > self.messageLimit) {
            var text = self.$('#Feedback_MessageLimit').val();
            $('.message-limit').html(text);
            $('.message-limit').removeClass('hide');
            return;
        }

        $('.message-limit').addClass('hide');
        if (self.feedback.message.length === 0 && self.feedback.rate === 0) {
            var text = self.$('#Feedback_NoInput').val();
            $('.message-result').html(text);
            $('.message-result').removeClass('hide');
            return;
        }

        $('.message-result').addClass('hide');

        $.ajax({
            type: "POST",
            url: self.addFeedbackUrl,
            data: self.feedback,
            success: (result) => {
                if (result.ErrCode === 0) {
                    $('.feedback-box').addClass('hide');
                    $('.feedback-result').removeClass('hide');
                    setTimeout(function (): void {
                        window.top.indexManager.hideFloatingFeedbackIcon();
                        window.top.popupManager.close();
                    }, 3000);

                    return;
                }

                $('.message-result').html(result.ErrMsg);
                $('.message-result').removeClass('hide');
            },
            complete: () => {
            }
        });
    }
}

$(function (): void {
    new Feedback($).init();
});