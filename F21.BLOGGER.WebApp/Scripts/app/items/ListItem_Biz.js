
var fnSetItemsCount = function (id, count) {
    $(id).text(count + ' Items');
};

var fnSetSearchText = function (searchText) {
    return $("#searchText").val(searchText);
}

var fnGetSearchText = function () {
    return $("#searchText").val();
}


var fnItemsChangeBySort = function () {
    
};


//정렬 정보를 확인합니다.
var fnSetSortData = function () {
    //Sort logic
    if ($('.drop_c.b_gray span.selected').length > 0) {
        $.listitem.param.sort.sortType = $('.drop_c.b_gray span.selected').data('sort');
    }
};

//정렬 정보를 확인합니다.
var fnGetSortData = function () {
    //Sort logic
    return ($('.drop_c.b_gray span.selected').data('sort') == null ? "01" : $('.drop_c.b_gray span.selected').data('sort'));
};

//페이지사이즈 정보를 확인합니다.
var fnSetPageSizeData = function () {
    //PageSize logic
    if ($('.pagesize.selected.t_pink').length > 0) {
        $.listitem.param.pageSize = $('.pagesize.selected.t_pink').data('pagesize');
    }
};

//페이지사이즈 정보를 확인합니다.
var fnGetPageSizeData = function () {
    //PageSize logic
    return ($('.pagesize.selected.t_pink').data('pagesize') == null ? "10" : $('.pagesize.selected.t_pink').data('pagesize'));
};