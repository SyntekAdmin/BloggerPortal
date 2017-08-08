var fnSetSearchText = function (searchText) {
    return $("#searchText").val(searchText);
}

var fnGetSearchText = function () {
    return $("#searchText").val();
}


var fnMyOrdersChangeBySort = function () {
    
};


//정렬 정보를 확인합니다.
var fnSetSortData = function () {
    //Sort logic
    if ($('.drop_c.b_gray span.selected').length > 0) {
        $.listMyOrder.param.sort.sortType = $('.drop_c.b_gray span.selected').data('sort');
    }
};

//정렬 정보를 확인합니다.
var fnGetSortData = function () {
    //Sort logic
    return ($('.drop_c.b_gray span.selected').data('sort') == null ? "01" : $('.drop_c.b_gray span.selected').data('sort'));
};