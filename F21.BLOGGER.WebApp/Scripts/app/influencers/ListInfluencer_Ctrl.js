// Code goes here
var myApp = angular.module('forever21App', ['angularUtils.directives.dirPagination']);

myApp.directive('myRepeatDirective', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            //window.alert("im the last!");
            $(document).ready(function () {
                /* Dropdown */
                $(".select_container_in").find('.drop_p').each(function () {
                    $(this).click(function () {
                        if ($(this).next().hasClass('drop_c'))
                            $(this).next().toggleClass('open');
                        else
                            $(this).children('.drop_c').toggleClass('open');
                    });
                });
                $(".select_container_in").find('div.drop_c.b_gray').each(function () {
                    $(this).click(function () {
                        $(this).toggleClass('open');
                    });
                });
            });
        }
    };
});

function InfluencerController($scope, $document, $http) {
    var vm = this;
    $scope.currentPage = 1;
    //$scope.pageSize = 10000
    $scope.pageSize = 10,
    $scope.meals = [];
    $scope.status = [{ "text": "Select", "value": "" }
        , { "text": "Regist", "value": "Regist" }
        , { "text": "Accept", "value": "Accept" }
        , { "text": "Pending", "value": "Pending" }
        , { "text": "Reject", "value": "Reject" }
        , { "text": "Inactivate", "value": "Inactivate" }
        , { "text": "Reactivate", "value": "Reactivate" }
        , { "text": "Block", "value": "Block" }
        , { "text": "Delete", "value": "Delete" }];
    $scope.model = {
        param: {
            pageNum: $scope.currentPage,
            pageSize: $scope.pageSize,
            adminSeq: '-1',
            orderText: '00'
        },
        biz: {
            fnGetData: function () {
                
                var result1 = null;
                executeAJAXToApiGet("/api/admin/influencers/" + $scope.model.param.orderText + "/" + $scope.model.param.adminSeq + "/" + $scope.model.param.pageNum + "/" + $scope.model.param.pageSize, JSON.stringify($scope.model.param), function (result) {
                    result1 = result;
                })
                return result1;
            },
            fnGetDataCnt: function () {
                var result1 = 0;
                executeAJAXToApiGet("/api/admin/influencerscnt/" + $scope.model.param.adminSeq, JSON.stringify($scope.model.param), function (result) {
                    fnSetItemsCount('#influencers-count', result);
                    result1 = result;
                })
                return result1;
            }
        }
    };


    $scope.initialize = function () {
        $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
    };

    {
        $scope.fnSortChange = function () {
            $(".sort_type").removeClass("selected t_pink");
            $(event.target).addClass("selected t_pink");

            $scope.model.param.orderText = fnGetSortData();
            $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];

        };
   
        $scope.sortMenuToggle = function () {
            this.sortMenuOpen = !this.sortMenuOpen;
        };

        $scope.fnDelete = function (email) {
     
            if (confirm("Do you want to delete?")) {
                    var data = 0;
                    var InfUser = new Object();
                    InfUser.USER_EMAIL = email;

                    executeAJAXToApi("/api/admin/deleteinfluencer", JSON.stringify(InfUser), function (result) {
                        data = result;
                    })

                    if (data > 0) {
                        alert("Deleted");
                        $scope.initialize();
                        //location.reload();
                    }
                    else {
                        alert("Delete error");
                    }
            }
        };

        $scope.fnChangeStatus = function (index, scope) {
            jQuery.support.cors = true;

            if (scope.meals[index].STATUS_CD == "" || scope.meals[index].STATUS_CD == null)
            {
                alert("Can't Change");
            }
            else
            {
                if (scope.meals[index].STATUS_CD == "Reject") {
                    $("body, .r_container, .desktop_overlay").addClass("open");
                    $('#div_reject_influence').show();

                    $('#hd_rejectuseremail').val(scope.meals[index].USER_EMAIL);
                }
                else {
                        var data = 0;
                        var InfUser = new Object();
                        InfUser.USER_EMAIL = scope.meals[index].USER_EMAIL;
                        InfUser.STATUS_CD = scope.meals[index].STATUS_CD;
                        InfUser.REJECT = "";

                        executeAJAXToApi("/api/admin/updatestatusinfluencer", JSON.stringify(InfUser), function (result) {
                            data = result;
                        })

                        if (data > 0) {
                            alert("Status Changed");

                            $scope.initialize();
                            //location.reload();
                        }
                        else {
                            alert("Status Changed error");
                        }
                }
            }

        };

        $scope.fnChangeStatusdl = function (email, status) {
            jQuery.support.cors = true;

            if (status == "" || status == null) {
                alert("Can't Change");
            }
            else {
                if (status == "Reject") {
                    $("body, .r_container, .desktop_overlay").addClass("open");
                    $('#div_reject_influence').show();

                    $('#hd_rejectuseremail').val(email);
                }
                else {
                        var data = 0;
                        var InfUser = new Object();
                        InfUser.USER_EMAIL = email;
                        InfUser.STATUS_CD = status;
                        InfUser.REJECT = "";

                        executeAJAXToApi("/api/admin/updatestatusinfluencer", JSON.stringify(InfUser), function (result) {
                            data = result;
                        })

                        if (data > 0) {
                            alert("Status Changed");
                            $scope.initialize();
                            //location.reload();
                        }
                        else {
                            alert("Status Changed error");
                        }
                  
                }
            }

        };

        $scope.fnChangeAssignee = function (index, scope) {
            if (scope.meals[index].ADMIN_SEQ == "" || scope.meals[index].ADMIN_SEQ == null) {
                alert("Can't Change");
            }
            else {
                    var data = 0;
                    var InfUser = new Object();
                    InfUser.USER_SEQ = scope.meals[index].USER_SEQ;
                    InfUser.STATUS_CD = scope.meals[index].STATUS_CD;
                    InfUser.ADMIN_SEQ = scope.meals[index].ADMIN_SEQ;

                    executeAJAXToApi("/api/admin/updateAssignInfluencer", JSON.stringify(InfUser), function (result) {
                        data = result;
                    })

                    if (data > 0) {
                        alert("Assignee Changed");

                        $scope.initialize();
                        //location.reload();
                    }
                    else {
                        alert("Assignee Changed error");
                    }
            }

            //alert(index);
            //alert(scope.meals[index].ADMIN_SEQ);
            //alert(scope.meals[index].USER_EMAIL);
        };

        $scope.fnChangeAssigneedl = function (userseq, status, adminseq) {

            if (adminseq == "" || adminseq == null) {
                alert("Can't Change");
            }
            else {
                    var data = 0;
                    var InfUser = new Object();
                    InfUser.USER_SEQ = userseq;
                    InfUser.STATUS_CD = status;
                    InfUser.ADMIN_SEQ = adminseq;

                    executeAJAXToApi("/api/admin/updateAssignInfluencer", JSON.stringify(InfUser), function (result) {
                        data = result;
                    })

                    if (data > 0) {
                        alert("Assignee Changed");

                        $scope.initialize();
                        //location.reload();
                    }
                    else {
                        alert("Assignee Changed error");
                    }
            }

        };


        $document.bind('click', function (event) {

            if ($("[list-option-class]").find(event.target).length > 0)
                return;

            $("#div-sort").removeClass("open");
            $("#div-size").removeClass("open");
            $(".select_container_in").find('div.drop_c.b_gray').removeClass("open");
           // $scope.sortMenuOpen = false;
            $scope.$apply();
        });
    }
    //
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

    $scope.initialize();
}

myApp.controller('InfluencerController', InfluencerController);