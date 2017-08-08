// Code goes here
var myApp = angular.module('forever21App', ['angularUtils.directives.dirPagination']);

function MyOrderListController($scope, $document, $http) {
    var vm = this;
    $scope.currentPage = 1;
    $scope.pageSize = 3;
    $scope.meals = [];
    $scope.model = {
        param: {
            //필수
            pageNum: $scope.currentPage,
            pageSize: $scope.pageSize
        },
        biz: {
            fnGetData : function () {
                //return [];
                var result1 = null;
                executeAJAXToApi("/api/influencers/myorders", JSON.stringify($scope.model.param), function (result) {
                    result1 = result;
                })
                return result1;
            },
            fnGetDataCnt : function () {
                //return 0;
                var result1 = 0;
                executeAJAXToApi("/api/influencers/myordercnt", JSON.stringify($scope.model.param), function (result) {
                    result1 = result;
                })
                return result1;
            }
        }
    };
    
    // 데이터 가져오기
    $scope.initialize = function () {
        // 추가
        // 주문리스트 초기
        $('.loading').show();
        $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
        $('.loading').hide();
        
    };
    $scope.fnAddItems = function ($scope, index, parentindex) {

        //alert($scope.meals[parentindex].meal[index]);
      //  alert($scope.item[index].ProductId);
       // alert(item);
    }
    $scope.fnAddMyItems = function ($scope, index) {

        /*
        jQuery.support.cors = true;
        var scope = angular.element($("#div_member_popup")).scope();
        $.ajax({
            url: '/api/admin/insertmyitems',
            type: 'POST',
            dataType: 'json',
            data: {
                USER_EMAIL: email
            },
            success: function (data) {
                if (data > 0) {
                    alert("delete success");
                    //location.reload();
                    $scope.initialize();
                    $scope.$apply();
                }
                else {
                    alert("update error");
                }
            },
            error: function () {
                alert('webservice error');
            }
        });
        */
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
        //정렬팝업 Toggle
        $scope.sortMenuToggle = function () {
            this.sortMenuOpen = !this.sortMenuOpen;
        };

        // 팝업메뉴 초기화
        $document.bind('click', function (event) {
            //정렬팝업
            if ($("[list-sort-class]").find(event.target).length > 0)
                return;

            $scope.sortMenuOpen = false;
            $scope.$apply();
        });
    }
    //
    $scope.initialize();
}

myApp.controller('MyOrderListController', MyOrderListController);