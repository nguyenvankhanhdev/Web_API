import Swiper from 'swiper/bundle';
import lightGallery from 'lightgallery';
// Plugins
import lgThumbnail from 'lightgallery/plugins/thumbnail';
import lgZoom from 'lightgallery/plugins/zoom';

document.addEventListener('DOMContentLoaded', function (event) {
  var swiper = new Swiper(".mySwiper", {
    spaceBetween: 24,
    slidesPerView: 4,
    freeMode: true,
    watchSlidesProgress: true,
    paginationClickable: true,
  });
  var swiper2 = new Swiper(".mySwiper2", {
    spaceBetween: 10,
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev"
    },
    watchSlidesProgress: true,
    thumbs: {
      swiper: swiper,
    },
    on: {
      autoplayTimeLeft(s, time, progress) {
        progressCircle.style.setProperty("--progress", 1 - progress);
        progressContent.textContent = `${Math.ceil(time / 1000)}s`;
      },
    },
  });
});

// open gallery
$('.js-open-gallery').on("click", function (e) {
  $(".lg-gallery")
    .addClass("lg-show lg-show-in")
    .css("pointer-events", "unset");
  $(".js-gallery:nth-child(3)").trigger("click");
});
