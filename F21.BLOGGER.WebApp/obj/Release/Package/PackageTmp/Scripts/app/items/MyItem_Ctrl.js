// Code goes here
var myApp = angular.module('forever21App', ['angularUtils.directives.dirPagination']);

myApp.directive('myRepeatDirective', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            //window.alert("im the last!");
            $(document).ready(function () {
                var owlSlider = $('.item_slider').owlCarousel({ // Images Slider
                    navigation: true,
                    pagination: true,
                    items: 1,
                    autoPlay: false,
                    slideSpeed: 800,
                    responsive: true,
                    singleItem: true
                });
                // Picked Icon Toggle
                //$('.owl_list .tag_container.round_tag_rb').on('click', function (e) {
                //    var $tag = $('.owl_list .tag_container.round_tag_rb');
                //    if ($(this).hasClass('active')) {
                //        $(this).removeClass('active');
                //    } else {
                //        $tag.removeClass('active');
                //        $(this).addClass('active');
                //    }
                //});
                ///* Favorite Icon Toggle */
                //$('.icon_star').click(function (e) {
                //    if ($(this).hasClass('active')) {
                //        $(this).removeClass('active');
                //    } else {
                //        $(this).addClass('active');
                //    }
                //});
                /* Quick View Slide */
                jQuery(".qv_slide").on('click', function () {
                    $("body, .r_container, .desktop_overlay").addClass("open");
                });
            });
        }
    };
});

myApp.directive('myRepeatDirective2', function () {
    return function (scope, element, attrs) {
        $(".loading").show();
        if (scope.$last) {
            $(".loading").hide();
            //window.alert("im the last!");
        }
    };
});

function PhotosOfInfListController($scope, $document, $http) {
    var vm = this;
    $scope.currentPage = 1;
    $scope.pageSize = 1000000;
    $scope.meals = [];
    $scope.options = [{ "text": "Select", "value": "Select" }, { "text": "Reviewing", "value": "Reviewing" }, { "text": "Picked", "value": "Picked" }];
    $scope.model = {
        param: {
            //필수
            pageNum: $scope.currentPage,
            pageSize: $scope.pageSize,
            //옵션
            adminSeq : '-1',
            productNo : '',
            skuNo : '',
            orderText : '01'
        },
        biz: {
            fnGetDataItem: function () {
                //return [];
                var result1 = null;
                var params = "?";
                params += "adminSeq=" + o_adminSeq + "&";
                params += "productNo=" + o_productNo + "&";
                params += "skuNo=" + o_skuNo + "&";
                params += "t=" + (new Date()).getTime();
                executeAJAXToApiGet("/api/admin/getmyitem" + params, JSON.stringify(""), function (result) {
                    result1 = result;
                })
                return result1;
            },
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
                executeAJAXToApiGet("/api/admin/getmyitemlist" + params, JSON.stringify(""), function (result) {
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
                executeAJAXToApiGet("/api/admin/getmyitemlistcnt" + params, JSON.stringify(""), function (result) {
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
            },
            fnSetPickedStatus: function (adminSeq, productNo, skuNo, userSeq, myitemSeq, photoSeq) {
                //return 0;
                var result1 = 0;
                //
                var data = { "adminSeq": adminSeq, "productNo": productNo, "skuNo": skuNo, "userSeq": userSeq, "myitemSeq": myitemSeq, "photoSeq": photoSeq};
                executeAJAXToApi("/api/admin/setpickedstatus", JSON.stringify(data), function (result) {
                    result1 = result;
                })
                return result1;
            },
            fnSetNominated: function (adminSeq, productNo, skuNo, userSeq) {
                //return 0;
                var result1 = 0;
                //
                var data = { "adminSeq": adminSeq, "productNo": productNo, "skuNo": skuNo, "userSeq": userSeq };
                executeAJAXToApi("/api/admin/setnominated", JSON.stringify(data), function (result) {
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
        $scope.model.param.adminSeq = o_adminSeq;
        $scope.model.param.productNo = o_productNo;
        $scope.model.param.skuNo = o_skuNo;
        // 추가
        // 아이템리스트 초기
        $scope.meal_detail = ($scope.model.biz.fnGetDataItem) ? $scope.model.biz.fnGetDataItem() : [];
        $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
    };

    // 정렬관련
    {
        // 데이터 가져오기 (새로고침) 
        $scope.fnSortChange = function () {
            $(".sort_type").removeClass("selected t_pink");
            $(event.target).addClass("selected t_pink");
            //
            $scope.model.param.orderText = fnGetSortData();
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
        $scope.fnGetViewAll = function (productNm, skuNm) {
            var scope = angular.element($("#PhotoViewAllController")).scope();
            scope.productNm = productNm;
            scope.skuNm = skuNm;
            scope.photos = $scope.meal_detail.Photos;

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

        //$scope.$apply();
    });
    $scope.fnSetPicked = function (parentIndex, index, scope) {
        if (confirm("Do you want to change status?")) {
            $(".loading").show();
            var adminSeq = scope.meals[parentIndex].ADMIN_SEQ;
            var productNo = scope.meals[parentIndex].PRODUCT_NO;
            var skuNo = scope.meals[parentIndex].SKU_NO;
            var userSeq = scope.meals[parentIndex].USER_SEQ;
            var myitemSeq = scope.meals[parentIndex].Photos[index].MYITEM_SEQ;
            var photoSeq = scope.meals[parentIndex].Photos[index].PHOTO_SEQ;
            var pickedStatus = scope.meals[parentIndex].Photos[index].PICKED_STATUS;
            //
            if (pickedStatus == "Picked") {
                scope.model.biz.fnSetReviewingStatus(adminSeq, productNo, skuNo);
            }
            else {
                scope.model.biz.fnSetPickedStatus(adminSeq, productNo, skuNo, userSeq, myitemSeq, photoSeq);
            }
            scope.initialize();
            //$(".loading").hide();
            //scope.$apply();
        }
    };
    $scope.fnSetNominated = function (index, scope) {
        if (confirm("Do you want to change nominate?")) {
            $(".loading").show();
            var adminSeq = scope.meals[index].ADMIN_SEQ;
            var productNo = scope.meals[index].PRODUCT_NO;
            var skuNo = scope.meals[index].SKU_NO;
            var userSeq = scope.meals[index].USER_SEQ;
            //
            scope.model.biz.fnSetNominated(adminSeq, productNo, skuNo, userSeq);
            scope.initialize();
            $(".loading").hide();
            //scope.$apply();
        }
    };
    $scope.fnSelectChange = function (scope) {
        if (scope.meal_detail.PICKED_STATUS != "Reviewing" || $("#pickedStatus").val() != "Picked") {
            scope.meal_detail.PICKED_STATUS = $("#pickedStatus").val();
        }
        else {
            if (confirm("Do you want to change status?")) {
                $(".loading").show();
                var adminSeq = scope.meal_detail.ADMIN_SEQ;
                var productNo = scope.meal_detail.PRODUCT_NO;
                var skuNo = scope.meal_detail.SKU_NO;
                //
                scope.model.biz.fnSetReviewingStatus(adminSeq, productNo, skuNo);
                //
                $scope.meal_detail = ($scope.model.biz.fnGetDataItem) ? $scope.model.biz.fnGetDataItem() : [];
                $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
                $(".loading").hide();
                //scope.$apply();
            }
            else {
                scope.meals[index].PICKED_STATUS = $("#pickedStatus").val();
            }
        }
    }
    $scope.fnSelectDropChange = function (value, scope) {
        scope.meal_detail.PICKED_STATUS = value;
        if (scope.meal_detail.PICKED_STATUS != "Reviewing" || $("#pickedStatus").val() != "Picked") {
            scope.meal_detail.PICKED_STATUS = $("#pickedStatus").val();
        }
        else {
            if (confirm("Do you want to change status?")) {
                $(".loading").show();
                var adminSeq = scope.meal_detail.ADMIN_SEQ;
                var productNo = scope.meal_detail.PRODUCT_NO;
                var skuNo = scope.meal_detail.SKU_NO;
                //
                scope.model.biz.fnSetReviewingStatus(adminSeq, productNo, skuNo);
                //
                $scope.meal_detail = ($scope.model.biz.fnGetDataItem) ? $scope.model.biz.fnGetDataItem() : [];
                $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
                $(".loading").hide();
                //scope.$apply();
            }
            else {
                scope.meal_detail.PICKED_STATUS = $("#pickedStatus").val();
            }
        }
    }
    //
    $scope.initialize();
}

myApp.controller('PhotosOfInfListController', PhotosOfInfListController);
myApp.filter('urlencode', function () {
    return function (input) {
        return window.encodeURIComponent(input);
    }
});


function PhotoViewAllController($scope, $document, $http) {
    
}

myApp.controller('PhotoViewAllController', PhotoViewAllController);