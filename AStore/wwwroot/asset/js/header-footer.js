import Swiper from 'swiper/bundle';
import lightGallery from 'lightgallery';
import lgThumbnail from 'lightgallery/plugins/thumbnail';
import lgZoom from 'lightgallery/plugins/zoom';
import lgFullscreen from 'lightgallery/plugins/fullscreen';
import lgAutoplay from 'lightgallery/plugins/autoplay';

const fnGallerySlide = () => {
  let swiperGallery = new Swiper('.slideGallery', {
    effect: 'coverflow',
    grabCursor: true,
    centeredSlides: true,
    slidesPerView: 'auto',
    initialSlide: 1,
    spaceBetween: 8,
    coverflowEffect: {
      rotate: 50,
      stretch: 0,
      depth: 100,
      modifier: 1,
      slideShadows: true,
    },
    // pagination: {
    //   el: ".swiper-pagination",
    // },
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
  });

  lightGallery(document.getElementById('animated-thumbnails-gallery'), {
    plugins: [lgAutoplay, lgThumbnail, lgZoom],
    speed: 500,
    licenseKey: 'your_license_key',
    showZoomInOutIcons: true,
    actualSize: false,
    appendSubHtmlTo: '.lg-outer',
    appendCounterTo: '.lg-content',
    animateThumb: true,
    addClass: `lg-gallery`,
    mobileSettings: {
    showCloseIcon: true,
    controls: false,
      // showZoomInOutIcons: false
    },
  });
};

fnGallerySlide();
