import Swiper from 'swiper/bundle';

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
            for (var i = 0; i < myContentPanes.length; i++) {
                myContentPanes[i].classList.remove('active');
            }
            var anchorReference = tabClickEvent.target;
            var activePaneId = anchorReference.getAttribute('href');
            console.log(anchorReference);
            var activePane = document.querySelector(activePaneId);
            activePane.classList.add('active');
        }
        for (var i = 0; i < tabIems.length; i++) {
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
});
document.addEventListener('DOMContentLoaded', function(event) {
    var categoryMenu = new Swiper('.js-category-menu .swiper', {
        slidesPerView: 'auto',
        spaceBetween: 0,
        navigation: {
            nextEl: ".js-category-menu .swiper-button-next",
            prevEl: ".js-category-menu .swiper-button-prev",
        },
    });
    //   $('.js-category-menu .item').click(function (e) {
    //     e.preventDefault();
    //     $('.js-category-menu .item').removeClass('active');
    //     $(this).addClass('active');
    //   });
});