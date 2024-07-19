const chooseBtnGr = () => {
    const btnLocal = document.querySelector('.abbot-local'),
        btnLocation = document.querySelector('.abbot-location'),
        abbotChoose = document.querySelector('.abbot-choose');
    btnLocal.addEventListener("click", (e) => {
        e.preventDefault()
        btnLocal.classList.add("active")
        btnLocation.classList.remove("active")
        abbotChoose.classList.remove('close')
    });
    btnLocation.addEventListener("click", (e) => {
        e.preventDefault()
        btnLocal.classList.remove("active")
        btnLocation.classList.add("active")
        abbotChoose.classList.add('close')
    });
}

const chooseBranch = () => {
    document.querySelectorAll("input[name='grp1']").forEach((input) => {
        input.addEventListener('change', () => {
            const abbotMap = document.querySelector('.abbot-map')
            abbotMap.classList.remove('close')
            abbotMap.querySelector('.abbot-map-header').textContent = input.nextElementSibling.textContent
            const abbotMapText = abbotMap.querySelector('.abbot-map-text')
            abbotMapText.children[1].textContent = input.nextElementSibling.textContent
        });
    });
}

const fnSearch = () => {
    const autocomplete = $('.js-suggest');
    //ascii
    var convertToAscii = function (str) {
        str = str.toLowerCase();
        str = str
            .replace(/ /g, '-')
            .replace(/đ/g, 'd')
            .replace(/Đ/g, 'D')
            .replace(/-+-/g, '-')
            .replace(/ + /g, '-')
            .replace(/^\-+|\-+$/g, '')
            .replace(/^\-+|\-+$/g, '')
            .replace(/ì|í|ị|ỉ|ĩ/g, 'i')
            .replace(/ỳ|ý|ỵ|ỷ|ỹ/g, 'y')
            .replace(/Ì|Í|Ị|Ỉ|Ĩ/g, 'I')
            .replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, 'Y')
            .replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, 'e')
            .replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, 'u')
            .replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, 'E')
            .replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, 'U')
            .replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, 'a')
            .replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, 'o')
            .replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, 'A')
            .replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, 'O')
            .replace(
                /!|@|\$|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\:|\'| |\"|\&|\#|\[|\]|~/g,
                ' '
            )
            .replace(
                /!|@|\$|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\'| |\"|\&|\#|\[|\]|~/g,
                ' '
            ).replace(/ /g, '');
        return str;
    };

    //check nameAscii
    const tokenFilter = function (nameFilter, searchSplit) {
        return nameFilter.filter(function () {
            var result = $(this).attr('data-city').toLowerCase().indexOf(searchSplit) > -1;
            return result;
        }).show();
    };

    autocomplete.each(function () {
        const that = $(this);
        const formSearch = that.find('.form-search'),
            inputSearch = that.find('.js-form-input'),
            btnClear = that.find('.js-form-clear'),
            autocompleteList = that.find('.js-suggest-list'),
            autocompleteItem = autocompleteList.find('.js-suggest-item'),
            nameAscii = convertToAscii(inputSearch.val().trim());

        btnClear.hide();

        //suggest item
        autocompleteItem.each(function () {
            const context = convertToAscii($(this).text().trim());
            console.log("${$(this).text().trim()}", $(this).text().trim());
            $(this).text(`${$(this).text().trim()}`);
            $(this).attr('data-city', `${context}`);
        }).click(function (e) {
            e.preventDefault();
            const $that = $(this),
                valueItem = $that.text().trim();

            btnClear.css('display','inline-flex');
            unvisible();
            inputSearch.val(`${valueItem}`);
        });;

        const unvisible = () => {
            autocomplete.removeClass('results-visible');
        }

        const fnvisible = (formInput) => {
            autocomplete.addClass('results-visible');
        }

        //input autocomplete
        inputSearch.off('keyup').on('keyup', function () {
            const _that = $(this);
            const $keywords = _that.val().trim(), $nameAscii = convertToAscii($keywords);

            unvisible();
            autocompleteItem.hide();
            btnClear.hide();

            if ($keywords.length) {
                fnvisible($keywords);
                tokenFilter(autocompleteItem, $nameAscii);
                btnClear.css('display','inline-flex');
            }

        }).focus(function () {
            const getKeywords = inputSearch.val().trim();
            if(getKeywords.length){
                btnClear.css('display','inline-flex');
            }
            fnvisible(getKeywords);
            tokenFilter(autocompleteItem, convertToAscii(getKeywords));
        });

        //buttton cancel clear value
        btnClear.click(function () {
            inputSearch.val('').focus();
            autocomplete.removeClass('results-visible');
            btnClear.hide();
            if(inputSearch.is(":focus")){
                autocomplete.addClass('results-visible');
            }
        });

    });

    //click outside
    $(document).click(function(e){
        if(autocomplete.has(e.target).length === 0){
            autocomplete.removeClass('results-visible');
        }
    });
};

let __init = function () {
    chooseBtnGr();
    chooseBranch();
    fnSearch();
}();