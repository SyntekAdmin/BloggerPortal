

(function ($) {
    $.fn.bspager = function (options) {

        var settings = $.extend({
            pageIndex: 1,
            pageSize: 20,
            pageGroupSize: 10,
            totalCount: 0,
            pagerCss: '',
            onPagerClick: function () { }
        }, options);

        //console.log(JSON.stringify(settings));

        var pageGroupStart = (parseInt(settings.pageIndex / settings.pageGroupSize) * settings.pageGroupSize) + 1;
        var pageGroupEnd = (parseInt(settings.pageIndex / settings.pageGroupSize) * settings.pageGroupSize) + settings.pageGroupSize;
        var totalCountPageEnd = Math.ceil(settings.totalCount / settings.pageSize);

        if (totalCountPageEnd < pageGroupEnd)
            pageGroupEnd = totalCountPageEnd;

        //console.log('pageGroup: ' + pageGroupStart + '~' + pageGroupEnd);

        var h = '<ul class="pagination"' + (settings.pagerCss == '' ? '' : ' style="' + settings.pagerCss + '"') + '>';

        if (pageGroupStart > 1)
            h += '<li><a class="PagerLink" href="javascript:void(0);" pageindex="' + (pageGroupStart - settings.pageGroupSize) + '">&laquo;</a></li>';

        for (var i = pageGroupStart; i <= pageGroupEnd; i++) {
            h += '<li' + (i == settings.pageIndex ? ' class="active"' : '') + '><a href="javascript:void(0);" class="PagerLink" pageindex="' + i + '">' + i + '</a></li>';
        }

        if (totalCountPageEnd > pageGroupEnd)
            h += '<li><a class="PagerLink" href="javascript:void(0);" pageindex="' + (pageGroupEnd + 1) + '">&raquo;</a></li>';

        h += '</ul>';

        this.html(h);

        this.find('a.PagerLink').click(function (e) {
            settings.onPagerClick.call(e, $(this).attr('pageindex'));
        });
    };
}(jQuery));

