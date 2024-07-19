import Swiper from 'swiper/bundle';

var serviceMenu = new Swiper('.js-service-menu .swiper', {
  slidesPerView: 'auto',
  spaceBetween: 0,
  navigation: {
      nextEl: ".js-service-menu .swiper-button-next",
      prevEl: ".js-service-menu .swiper-button-prev",
  },
}); 
var slidesServiceMenu = $('.js-service-menu .item').length;
if (slidesServiceMenu <= 4) {
  $('.wrap-list-service-price').addClass('no-swiper-btn');
}

var swiper5 = new Swiper('.mySwiper5', {
  slidesPerView: 1,
  slidesPerGroup: 1,
  spaceBetween: 24,
  // allowTouchMove: false,
  pagination: {
      el: ".swiper-pagination",
      clickable: true,
  },
  navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
  },
});

var swiper6 = new Swiper('.mySwiper6', {
  slidesPerView: 4,
  slidesPerGroup: 1,
  spaceBetween: 24,
  // allowTouchMove: false,
  navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
  },
});

var swiper = new Swiper(".mySwiper", {
  spaceBetween: 24,
  slidesPerView: 4,
  freeMode: true,
  watchSlidesProgress: true,
  paginationClickable: true,
});
var swiper7 = new Swiper(".mySwiper7", {
  spaceBetween: 10,
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev"
  },
  watchSlidesProgress: true,
  thumbs: {
    swiper: swiper,
  }
});

// open gallery
$('.js-open-gallery').on("click", function (e) {
  $(".lg-gallery")
    .addClass("lg-show lg-show-in")
    .css("pointer-events", "unset");
  $(".js-gallery:nth-child(3)").trigger("click");
});

$('.chat-modal.open').removeClass('.modal-prevent-scroll');

