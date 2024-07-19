import Swiper from 'swiper/bundle';
import lightGallery from 'lightgallery';
// Plugins
import lgThumbnail from 'lightgallery/plugins/thumbnail';
import lgZoom from 'lightgallery/plugins/zoom';
import lgAutoplay from 'lightgallery/plugins/autoplay';
import lgFullscreen from 'lightgallery/plugins/fullscreen';


document.addEventListener('DOMContentLoaded', function(event) {
    var detailHeaderGalleryMain = new Swiper('.gallery-main', {
        slidesPerView: 1,
        spaceBetween: 0,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
    });

    var detailHeaderGalleryThumb = new Swiper('.gallery-thumbs', {
        slidesPerView: 'auto',
        spaceBetween: 10,
    });
	
	const gallery =  document.querySelector('.js-gallery');

    const thumbItems = document.querySelectorAll('.gallery-thumbs .swiper-slide');
    thumbItems.forEach((thumb, index) => {
        thumb.addEventListener('click', function() {
            thumbItems.forEach((thumbItem) => {
                thumbItem.classList.remove('active');
            });
            thumb.classList.add('active');
            detailHeaderGalleryMain.slideTo(index);
        });
    });

    detailHeaderGalleryMain.on('transitionEnd', function(e) {
        detailHeaderGalleryThumb.slideTo(detailHeaderGalleryMain.realIndex);
        var index = detailHeaderGalleryMain.realIndex;
        thumbItems.forEach((thumbItem) => {
            thumbItem.classList.remove('active');
        });
        thumbItems[index].classList.add('active');
    });
	
	// add gallery when click to image 
    $(".swiper.gallery-main").click(function (e) {
        var senderElement = e.target;
        // Check if sender is the <div> element e.g.
        if ($(e.target).hasClass("swiper-button-next")) {
            if (detailHeaderGalleryMain && detailHeaderGalleryMain.activeIndex == 6) {
                const gallery = document.querySelector('.js-gallery');
                const plugin = lightGallery(gallery, {
                    plugins: [lgZoom, lgThumbnail, lgFullscreen, lgAutoplay],
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
                plugin.openGallery()
            }
            
        }
        else {
            document.querySelector('.js-open-gallery').click()
        }
    });
    
    let lgTemp =  lightGallery(document.querySelector('#animated-thumbnails-gallery'), {
        controls: false,
        counter: false,
        licenseKey:'your-license-key',
        hideControlOnEnd: true,
        download: false,
        autoplay: true,
    });

    
    const subPlugin = lightGallery(gallery,{
        plugins: [lgZoom, lgThumbnail, lgFullscreen, lgAutoplay],
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
  
    document.querySelector(".view-gallery.js-open-gallery").addEventListener("click",function(e){
        e.preventDefault();
        if(subPlugin){
            subPlugin.openGallery();
        }
        /*gallery.addEventListener('lgAfterClose', () => {
            plugin.destroy();
        });*/
    })

    document.querySelectorAll(".footer .swiper-slide").forEach(item=>{
        item.addEventListener("click",()=>{
            lgTemp && lgTemp.destroy();
         
            lightGallery(document.querySelector('#animated-thumbnails-gallery'), {
                plugins: [lgZoom, lgThumbnail, lgFullscreen, lgAutoplay],
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
            }).openGallery();
        })
    })

    

    // Modal Box
    var propertiesGalleryThumb = new Swiper('.properties-gallery-thumb', {
        slidesPerView: 4,
        watchOverflow: true,
        watchSlidesVisibility: true,
        watchSlidesProgress: true,
        direction: 'vertical',
        spaceBetween: 10,
    });

    var propertiesGalleryMain = new Swiper('.properties-gallery-main', {
        watchOverflow: true,
        watchSlidesVisibility: true,
        watchSlidesProgress: true,
        preventInteractionOnTransition: true,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        effect: 'fade',
        fadeEffect: {
            crossFade: true,
        },
        thumbs: {
            swiper: propertiesGalleryThumb,
        },
    });

    propertiesGalleryMain.on('slideChangeTransitionStart', function() {
        propertiesGalleryThumb.slideTo(propertiesGalleryMain.activeIndex);
    });

    propertiesGalleryThumb.on('transitionStart', function() {
        propertiesGalleryMain.slideTo(propertiesGalleryThumb.activeIndex);
    });

    var propertiesMenu = new Swiper('.properties__menu .swiper', {
        slidesPerView: 'auto',
    });

    function handleProperties() {
        var container = $('.js-modal-properties');
        var block = container.find('.block');
        var nav = container.find('.properties__menu');
        var navPosition = nav[0].getBoundingClientRect().top;
        var navHeight = nav[0].getBoundingClientRect().height;
        var headingHeight = container.find('.modal-header').outerHeight();
        container
            .scroll(function() {
                var scrollDistance = $(this).scrollTop();
                // Assign active class to nav links while scolling\
                block.each(function(i) {
                    if ($(this).position().top <= scrollDistance) {
                        container.find('.tab-item').removeClass('active');
                        container.find('.tab-item').eq(i).addClass('active').focus();
                    }
                });
                // show navigation
                if (navPosition - (100 + navHeight) <= scrollDistance) {
                    //100 is margin
                    nav.addClass('active');
                } else {
                    nav.removeClass('active');
                }
            })
            .scroll();

        function goToByScroll(id) {
            var scrollTo = $(id);
            container.animate({
                    scrollTop: scrollTo.offset().top -
                        container.offset().top +
                        container.scrollTop() -
                        (navHeight + headingHeight),
                },
                800
            );
            setTimeout(() => {
                $('.c-modal').removeClass('c-modal--visible');
            }, 1000);
        }
        //Bắt sự kiện click và gọi hàm scroll
        $('.properties__menu .swiper-slide').click(function(e) {
            e.preventDefault();
            $('.properties__menu .swiper-slide').removeClass('active');
            $(this).addClass('active');
            var index = $('.properties__menu .swiper-slide').index($(this));
            propertiesMenu.slideTo(index, 800);
            propertiesMenu.update();
            var data = $(this).attr('href');
            console.log($(this));
            goToByScroll(data);
        });
    }
    handleProperties();

    //   comment gallery
    function containerLg() {
        const containerLg = document.querySelectorAll('.js-gallery-container');
        const caption = document.createElement('div');
        caption.classList.add('lg-caption');
        containerLg.forEach((element) => {
            const parent = element.closest('.js-gallery-container');
            const childrenName = parent.querySelector('.avatar-name');
            const childrenText = parent.querySelector('.avatar-para');
            const childrenRate = parent.querySelector('.avatar-rate');
            element.addEventListener('lgAfterOpen', (event) => {
                const lgContainer = document.querySelector('.lg-show');
                const lgOuter = lgContainer.querySelector('.lg-outer');
                caption.appendChild(childrenName.cloneNode(true));
                caption.appendChild(childrenRate.cloneNode(true));
                caption.appendChild(childrenText.cloneNode(true));
                lgOuter.appendChild(caption);
            });
            element.addEventListener('lgAfterClose', (event) => {
                caption.innerHTML = '';
            });
            lightGallery(element, {
                selector: '.item',
                thumbnail: true,
                speed: 500,
                licenseKey:'your-license-key',
                thumbWidth: 80,
                thumbHeight: '80px',
                //   thumbHeight: 80,
                thumbMargin: 8,
                closable: true,
                flipHorizontal: false,
                flipVertical: false,
                addClass: `lg-comment`,
                download: false,
                getCaptionFromTitleOrAlt: false,
                mobileSettings: {
                    controls: true,
                    showCloseIcon: true,
                },
                plugins: [lgZoom, lgThumbnail, lgFullscreen, lgAutoplay],
                
            });
        });
    }
    containerLg();
});

window.addEventListener('load', function() {
    // store tabs variable
    var tabs = document.querySelectorAll('.js-tab');
    tabs.forEach((tab) => {
        var tabIems = tab.querySelectorAll('.tab-item');

        function myTabClicks(tabClickEvent) {
            for (var i = 0; i < tabIems.length; i++) {
                tabIems[i].classList.remove('active');
            }
            var clickedTab = tabClickEvent.currentTarget;
            clickedTab.classList.add('active');
            tabClickEvent.preventDefault();
            var myContentPanes = document.querySelectorAll('.tab-pane');
            for (i = 0; i < myContentPanes.length; i++) {
                myContentPanes[i].classList.remove('active');
            }
            var anchorReference = tabClickEvent.target;
            var activePaneId = anchorReference.getAttribute('href');
            console.log(activePaneId);
            var activePane = document.querySelector(activePaneId);
            activePane.classList.add('active');
        }
        for (i = 0; i < tabIems.length; i++) {
            tabIems[i].addEventListener('click', myTabClicks);
        }
    });

    //selected

    var selects = document.querySelectorAll('.js-select');
    selects.forEach((select) => {
        var selectItems = select.querySelectorAll('.item');
        const handleClick = (e) => {
            e.preventDefault();
            selectItems.forEach((node) => {
                node.classList.remove('active');
            });
            e.currentTarget.classList.add('active');
        };
        selectItems.forEach((item) => {
            item.addEventListener('click', handleClick);
        });
    });

    //   accordion
    // var btn = document.querySelectorAll('.accordion .js-accordion-action');

    // btn.forEach((i) => i.addEventListener('click', toggleItem, false));

    // function toggleItem(e) {
    //     var accItem = this.closest('.accordion').querySelectorAll('.accordion-tab');
    //     var accBtn = this.closest('.accordion').querySelectorAll('.js-accordion-action');
    //     for (let i = 0; i < accItem.length; i++) {
    //         accItem[i].className = 'accordion-tab';
    //     }
    //     this.closest('.accordion-tab').className = 'accordion-tab open';

    //     accBtn.forEach((node) => {
    //         node.classList.remove('open');
    //     });
    //     e.currentTarget.classList.add('open');
    // }

    const fnAccordion = () => {
        var tabAccordion = $('.accordion-tab');
        var detailQuestion = $('.detail__question');
        if (detailQuestion.length > 0) {
            tabAccordion.each(function() {
                var btnAccordion = $(this).find('.js-accordion-action');

                btnAccordion.click(function() {
                    const that = $(this);
                    if (that.closest(tabAccordion).hasClass('open')) {
                        that.closest(tabAccordion).removeClass('open');
                    } else {
                        tabAccordion.removeClass('open');
                        that.closest(tabAccordion).addClass('open');
                    }

                });

            });
        }
    }
    fnAccordion();

    //   disable scrollbar
    var modal = document.getElementsByClassName('js-modal')[0];
    modal.addEventListener('modalIsOpen', function(event) {
        document.body.classList.add('disable-scroll');
    });
    modal.addEventListener('modalIsClose', function(event) {
        document.body.classList.remove('disable-scroll');
    });

});

$(document).ready(function(){

    var swiper = new Swiper(".slideIncentives", {
        slidesPerView: 2,
        slidePerGroup: 2,
        spaceBetween: 12,
        speed: 900,
        navigation: {
          nextEl: ".sw-control-next",
          prevEl: ".sw-control-prev",
        },
      });
});
//click button next or prev open gallery
// $(".gallery-main .swiper-button").on("click", () => {
//     $(".js-open-gallery").trigger("click");
// });




