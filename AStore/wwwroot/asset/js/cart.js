// "use strict" 

$(document).ready(function (e) {

    // declare available
    let frmHomeDelivery = $('.js-home-delivery'),
        frmStoreDelivery = $('.js-store-delivery'),
        frmBank = $('.group-bank-vnpay');
    frmBankVisa = $('.group-bank-visa');
    countItems = $('.have-items');

    // hide div
    frmHomeDelivery.hide();
    frmStoreDelivery.hide();
    frmBank.hide();
    frmBankVisa.hide();

    let isChecked = $('.js-input-checked');

    // event click
    isChecked.on('click', function (e) {
        var dataName = $(this).attr("data-name");
        if (dataName === "athome") {
            frmHomeDelivery.show();
            frmStoreDelivery.hide();
        } else {
            frmHomeDelivery.hide();
            frmStoreDelivery.show();
        }

    });


    // event click option payment
    $('.cls-list-payment').on('click', function (e) {
        // click open div payment vnpay
        $(this).attr("data-name") === 'open-bank-vnpay' ? frmBank.show() : frmBank.hide();
        // click open div payment visa
        $(this).attr("data-name") === 'open-bank-visa' ? frmBankVisa.show() : frmBankVisa.hide();
    });

    // hide footer
    function hideFooter() {
        let footerEffect = $('.gallery-off');
        console.log(footerEffect.length);
        if (footerEffect.length != 0) {
            $('.footer .section__gallery').css({ display: 'none' });
        }
    }
    hideFooter();
});

