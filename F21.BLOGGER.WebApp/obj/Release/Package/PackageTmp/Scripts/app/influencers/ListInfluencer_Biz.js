
var fnSetItemsCount = function (id, count) {
    $(id).text(count + ' Items');
};

var fnItemsChangeBySort = function () {

};

var fnSetSortData = function () {
    if ($('.drop_c.b_gray span.selected').length > 0) {
        $.listitem.param.sort.sortType = $('.drop_c.b_gray span.selected').data('sort');
    }
};

var fnGetSortData = function () {
    return ($('.drop_c.b_gray span.selected').data('sort') == null ? "00" : $('.drop_c.b_gray span.selected').data('sort'));
};

var fnSetPageSizeData = function () {
    if ($('.pagesize.selected.t_pink').length > 0) {
        $.listitem.param.pageSize = $('.pagesize.selected.t_pink').data('pagesize');
    }
};

var fnGetPageSizeData = function () {
    return ($('.pagesize.selected.t_pink').data('pagesize') == null ? "10" : $('.pagesize.selected.t_pink').data('pagesize'));
};