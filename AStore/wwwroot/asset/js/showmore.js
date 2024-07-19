
$(document).ready(function () {

  showMore();

  function showMore() {
    let btnShowMore = $('.showMore');
    let listItem = $('.policy__table');
    
    listItem.addClass('policy-point');

    btnShowMore.on('click', function (e) {
      listItem.removeClass('policy-point');
      $(this).hide();
    });
  }
});