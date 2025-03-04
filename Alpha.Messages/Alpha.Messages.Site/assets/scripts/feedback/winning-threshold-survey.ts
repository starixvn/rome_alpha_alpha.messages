"use strict";
class WinningThresholdSurvey {
    $: JQueryStatic;
    addFeedbackUrl = "Add";

    feedback = {
        rate: 0,
        message: '',
        functionId: 0
    };

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init(): void {
        this.bindEvents();
        this.feedback.functionId = $('#FunctionId').val();
    }

    bindEvents(): void {
        const self: any = this;

        var isInternal = $('#IsInternal').val() === '1';
        if (isInternal) {
            return;
        }

        $('#btn-not-useful').click(function (): void {
            self.submitFeedback(0);
        });

        $('#btn-useful').click(function (): void {
            self.submitFeedback(1);
        });
    }

    submitFeedback(useful: boolean): void {
        const self: any = this;

        self.$.blockUI();
        self.feedback.message = useful ? 1 : 0;

        $('.message-result').addClass('hide');

        $.ajax({
            type: "POST",
            url: self.addFeedbackUrl,
            data: self.feedback,
            success: (result) => {
                self.$.unblockUI();
                if (result.ErrCode === 0) {
                    $('.feedback-box').addClass('hide');
                    $('.survey-result').removeClass('hide');
                    setTimeout(function (): void {
                        window.top.location.reload(true);
                        window.top.indexManager.domElements.winningThresholdSurveyPopup.close();
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
    new WinningThresholdSurvey($).init();
}); 