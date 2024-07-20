// dropdown
document.addEventListener(
  "DOMContentLoaded", // make sure DOM is fully parsed before accessing elements!
  function () {
    const dropdowns = document.querySelectorAll(".js-dropdown");
    for (const dropdown of dropdowns) {
      dropdown.addEventListener("click", function (event) {
        if (event.target.matches(".dropdown-button")) {
          // make sure it's not a click from the dropdown that has bubbled up
          dropdown.querySelector(".dropdown-menu").classList.add("open");
        }
      });
    }
    // on outside click, close all dropdowns
    document.addEventListener(
      "click",
      function (event) {
        const clickedElement = event.target;
        if (!clickedElement.closest(".dropdown-menu")) {
          var menu = document.querySelectorAll(".dropdown-menu");
          menu.forEach((i) => {
            i.classList.remove("open");
          });
        }
        // if (clickedElement.closest(".js-dropdown") === null) {
        //   for (const dropdown of dropdowns) {
        //     dropdown.querySelector(".dropdown-menu").classList.remove("open");
        //   }
        // }
      }, {
      capture: true,
    }
    );
  }
);

// event listeners cart
document.addEventListener(
  "DOMContentLoaded", // make sure DOM is fully parsed before accessing elements!
  function () {
    const dropdowns = document.querySelectorAll(".js-dropdown-open");
    for (const dropdown of dropdowns) {
      dropdown.addEventListener("click", function (event) {
        if (event.target.matches(".c-dropdown-button")) {
          // make sure it's not a click from the dropdown that has bubbled up
          dropdown.querySelector(".c-dropdown-menu").classList.add("open");
        }
      });
    }

    // on outside click, close all dropdowns
    document.addEventListener(
      "click",
      function (event) {
        const clickedElement = event.target;
        if (!clickedElement.closest(".c-dropdown-menu")) {
          var menu = document.querySelectorAll(".c-dropdown-menu");
          menu.forEach((i) => {
            i.classList.remove("open");
          });
        }

        // Click text 'hủy bỏ' close wrapper
        if (clickedElement.closest(".st-search__close")) {
          var menu = document.querySelectorAll(".c-dropdown-menu");
          menu.forEach((i) => {
            i.classList.remove("open");
          });
        }
      }, {
      capture: true,
    }
    );
  }
);


$(document).ready(function () {
  $('.dropdown').cDropdown();
})
jQuery.fn.extend({
  cDropdown: function () {
    return this.each(function () {
      var containermenu = $(this);
      var button = containermenu.find(".dropdown-button");
      var menu = containermenu.find(".dropdown-menu");
      var list = containermenu.find(".dropdown-menu-wrapper");
      var item = list.children();
      var option = button.find("span");
      button.click(function (e) {
        menu.addClass("open");
      });
      item.click(function (e) {
        e.preventDefault();
        $(this).siblings().removeClass("active");
        $(this).addClass("active");
        var txt = $(this).find("span").text();
        option.text(txt);
        menu.removeClass("open");
      });
      $(document).click(function (e) {
        e.stopPropagation();
        var container = containermenu;
        if (container.has(e.target).length === 0) {
          menu.removeClass("open");
        }
      });
    });
  },
});

// get item and fill data into dropdown button
function triggerItem() {

  var wrapperDropdown = $('.js-dropdown-open');

  wrapperDropdown.each(function (e) {

    var wrapperItem = $(this).find('.c-dropdown-menu');

    var getItem = $(this).find('.c-dropdown-menu .c-dropdown-menu__wrapper').children('.item-region');

    var fillValue = $(this).find('.c-dropdown-button');

    getItem.on('click', function (e) {

      // disable jumping in top
      e.preventDefault();

      if (getItem.hasClass('active')) {

        getItem.removeClass('active');

      }

      $(this).addClass('active');

      // get text item
      getText = $(this).text();

      // fill text form item to dropdown
      fillValue.text(getText);

      // fillValue.append(`<span class ='ic-arrow-select'></span>`);

      wrapperItem.removeClass('open');

    });

  })
}

function handelInput() {
  var dummyEl = $('.js-input-open');
  var dropdownInput = $('.c-dropdown-menu');

  dummyEl.each(function (e) {
    dummyEl.on('click', function (e) {

      if (dropdownInput) {
        var findDropdownMb = $(this).closest('.cs-suggest__wrapper').find('.c-dropdown-menu');
        
        findDropdownMb.addClass('open');

        // set timeout focus input when click ( Mobile)
        setTimeout(function (e) {
          $(".js-input-typing").focus();
        }, 200);

      }
      $('.js--suggestion-w-product').addClass('open');

    });
    // Click close wrapper
    document.addEventListener(
      "click",
      function (event) {
        const clickedElement = event.target;
        if (!clickedElement.closest(".js-input-open")) {
          var menu = document.querySelectorAll(".js--suggestion-w-product");
          menu.forEach((i) => {
            i.classList.remove("open");
          });
        }
      }, {
      capture: true,
    }
    );
  });
};


function checkTyping() {
  //  Declare avariable
  var inputSearch = $(".js-input-typing");
  var closeX = $(".close-btn");
  // Hide icon X
  closeX.hide();
  // Event typing
  inputSearch.each(function () {
    $(this).bind("keypress keyup keydown", function (e) {
      // Find icon X
      var resetInput = $(this).parent().find('.js-form-clear');
      // Show icon X when input != null
      if ($(this).val() != '') {
        resetInput.show();
        // click icon X clear input
        resetInput.on('click', function (e) {
          inputSearch.val('');
          resetInput.hide();
        });
      }
      //  Hide icon X when input == null
      else {
        resetInput.hide();
      }
    });
  });
}

//  Active functions
function run() {
  handelInput();
  checkTyping();
  triggerItem();
}

run();