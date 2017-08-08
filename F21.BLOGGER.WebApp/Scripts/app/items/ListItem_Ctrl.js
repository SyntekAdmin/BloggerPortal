// Code goes here
var myApp = angular.module('forever21App', ['angularUtils.directives.dirPagination']);

myApp.directive('myRepeatDirective', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            //window.alert("im the last!");
            /* Dropdown */
            $(document).ready(function () {
                $('.p_list .drop_p').each(function () {
                    $(this).click(function () {
                        if ($(this).next().hasClass('drop_c'))
                            $(this).next().toggleClass('open');
                        else {
                            $(this).children('.drop_c').toggleClass('open');
                        }
                    });
                });
                $(".select_container").find(".drop_c.b_gray").each(function () {
                    $(this).click(function () {
                        $(this).toggleClass('open');
                    });
                });
                /* Quick View Slide */
                jQuery(".qv_slide").on('click', function () {
                    $("body, .r_container, .desktop_overlay").addClass("open");
                });
            });
        }
    };
});

function ItemListController($scope, $document, $http) {
    var vm = this;
    $scope.currentPage = 1;
    $scope.pageSize = 10;
    $scope.meals = [];
    $scope.options = [{ "text": "Select", "value": "Select" }, { "text": "Reviewing", "value": "Reviewing" }, { "text": "Picked", "value": "Picked" }];
    $scope.model = {
        param: {
            //필수
            pageNum: $scope.currentPage,
            pageSize: $scope.pageSize,
            //옵션
            adminSeq: o_adminSeq,
            productNo : '',
            skuNo : '',
            searchText : '',
            orderText: '01'
        },
        biz: {
            fnGetData : function () {
                //return [];
                var result1 = null;
                var params = "?";
                params += "pageNum=" + $scope.model.param.pageNum + "&";
                params += "pageSize=" + $scope.model.param.pageSize + "&";
                params += "adminSeq=" + $scope.model.param.adminSeq + "&";
                params += "productNo=" + $scope.model.param.productNo + "&";
                params += "skuNo=" + $scope.model.param.skuNo + "&";
                params += "searchText=" + $scope.model.param.searchText + "&";
                params += "orderText=" + $scope.model.param.orderText + "&";
                params += "t=" + (new Date()).getTime();
                //
                executeAJAXToApiGet("/api/admin/getitemslist" + params, JSON.stringify(""), function (result) {
                    result1 = result;
                })
                return result1;
            },
            fnGetDataCnt : function () {
                //return 0;
                var result1 = 0;
                var params = "?";
                params += "pageNum=" + $scope.model.param.pageNum + "&";
                params += "pageSize=" + $scope.model.param.pageSize + "&";
                params += "adminSeq=" + $scope.model.param.adminSeq + "&";
                params += "productNo=" + $scope.model.param.productNo + "&";
                params += "skuNo=" + $scope.model.param.skuNo + "&";
                params += "searchText=" + $scope.model.param.searchText + "&";
                params += "orderText=" + $scope.model.param.orderText + "&";
                params += "t=" + (new Date()).getTime();
                //
                executeAJAXToApiGet("/api/admin/getitemslistcnt" + params, JSON.stringify(""), function (result) {
                    fnSetItemsCount('#products-count', result);
                    result1 = result;
                })
                return result1;
            },
            fnSetReviewingStatus: function (adminSeq, productNo, skuNo) {
                //return [];
                var result1 = null;
                //
                var data = { "adminSeq": adminSeq, "productNo": productNo, "skuNo": skuNo };
                executeAJAXToApi("/api/admin/setreviewingstatus", JSON.stringify(data), function (result) {
                    result1 = result;
                })
                return result1;
            }
        }
    };
    
    // 데이터 가져오기
    $scope.initialize = function () {
        // 추가
        // 아이템리스트 초기
        $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
    };
    // 검색관련
    {
        $scope.fnSearchTextChange = function () {
            $("#searchText").val($("#searchText2").val());
            //
            //$.param.data.pageNum = 1;
            //$.param.data.searchText = fnGetSearchText();
            ////
            //$scope.currentPage = $.param.data.pageNum;
            //$scope.meals = $.listitem.data.listitem();
            $scope.model.param.pageNum = 1;
            $scope.model.param.searchText = fnGetSearchText();
            //
            $scope.currentPage = $scope.model.param.pageNum;
            $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
        };
    }

    // 정렬관련
    {
        // 데이터 가져오기 (새로고침) 
        $scope.fnSortChange = function () {
            $(".sort_type").removeClass("selected t_pink");
            $(event.target).addClass("selected t_pink");
            //
            $scope.model.param.orderText = fnGetSortData();
            //
            $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
        };
    }
    // 페이지사이즈관련
    {
        // 데이터 가져오기 (새로고침) 
        $scope.fnPageSizeChange = function () {
            $(".pagesize").removeClass("selected t_pink");
            $(event.target).addClass("selected t_pink");
            //
            $scope.model.param.pageNum = 1;
            $scope.model.param.pageSize = fnGetPageSizeData();
            //
            $scope.pageNum = 1;
            $scope.pageSize = $scope.model.param.pageSize;
            //
            $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
        };
    }
    // 포토 ViewAll
    {
        $scope.fnGetViewAll = function (productNm, skuNm, index) {
            var scope = angular.element($("#PhotoViewAllController")).scope();
            scope.productNm = productNm;
            scope.skuNm = skuNm;
            scope.photos = $scope.meals[index].Photos;

            //$scope.$apply();
        }
    }
    // 팝업메뉴 초기화
    $document.bind('click', function (event) {
        //정렬팝업
        if ($("[list-option-class]").find(event.target).length > 0) {
            return;
        }

        $("#div-sort").removeClass("open");
        $("#div-size").removeClass("open");
        $('.p_list div.drop_c.b_gray').each(function () {
            $(this).removeClass("open");
        });
        
        //$scope.$apply();
    });

    $scope.fnSelectChange = function (index, scope) {
        if (scope.meals[index].PICKED_STATUS != "Reviewing" || $("#pickedStatus_" + index).val() != "Picked") {
            scope.meals[index].PICKED_STATUS = $("#pickedStatus_" + index).val();
        }
        else {
            if (confirm("Do you want to change status?")) {
                $(".loading").show();
                var adminSeq = scope.meals[index].ADMIN_SEQ;
                var productNo = scope.meals[index].PRODUCT_NO;
                var skuNo = scope.meals[index].SKU_NO;
                //
                $scope.model.biz.fnSetReviewingStatus(adminSeq, productNo, skuNo);
                $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
                $(".loading").hide();
                //scope.$apply();
            }
            else {
                scope.meals[index].PICKED_STATUS = $("#pickedStatus_" + index).val();
            }
        }
    }
    $scope.fnSelectDropChange = function (index, value, scope) {
        scope.meals[index].PICKED_STATUS = value;
        if (scope.meals[index].PICKED_STATUS != "Reviewing" || $("#pickedStatus_" + index).val() != "Picked") {
            scope.meals[index].PICKED_STATUS = $("#pickedStatus_" + index).val();
        }
        else {
            if (confirm("Do you want to change status?")) {
                $(".loading").show();
                var adminSeq = scope.meals[index].ADMIN_SEQ;
                var productNo = scope.meals[index].PRODUCT_NO;
                var skuNo = scope.meals[index].SKU_NO;
                //
                $scope.model.biz.fnSetReviewingStatus(adminSeq, productNo, skuNo);
                $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
                $(".loading").hide();
                //scope.$apply();
            }
            else {
                scope.meals[index].PICKED_STATUS = $("#pickedStatus_" + index).val();
            }
        }
    }

    $scope.initialize();
}

myApp.controller('ItemListController', ItemListController);
myApp.filter('urlencode', function () {
    return function (input) {
        return window.encodeURIComponent(input);
    }
});

function PhotoViewAllController($scope, $document, $http) {
    
}

myApp.controller('PhotoViewAllController', PhotoViewAllController);


