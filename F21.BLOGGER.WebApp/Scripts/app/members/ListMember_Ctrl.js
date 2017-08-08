// Code goes here
var myApp = angular.module('forever21App', ['angularUtils.directives.dirPagination']);

myApp.directive('myRepeatDirective', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            //window.alert("im the last!");
            /* Quick View Slide */
            jQuery(".qv_slide").on('click', function () {
                $("body, .r_container, .desktop_overlay").addClass("open");
            });
        }
    };
});

myApp.directive('myRepeatDirective2', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            //window.alert("im the last!");
            /* Dropdown */
            $("#div_member_popup").find('.drop_p').each(function () {
                $(this).click(function () {
                    if ($(this).next().hasClass('drop_c'))
                        $(this).next().toggleClass('open');
                    else
                        $(this).children('.drop_c').toggleClass('open');
                });
            });
            $("#div_member_popup").find('div.drop_c.b_gray').each(function () {
                $(this).click(function () {
                    $(this).toggleClass('open');
                });
            });
        }
    };
});

function MemberQuickController($scope, $document, $http) {

    var vm = this;
    $scope.model = {
        param: {
            pageNum: 1,
            pageSize: 10000
        },
        biz: {
            fnGetQuickAdminData: function () {
                var result1 = null;
                executeAJAXToApiGet("/api/admin/activeadmins", JSON.stringify($scope.model.param), function (result) {
                    result1 = result;
                })
                return result1;
            }
        }
    };
    $scope.admins = ($scope.model.biz.fnGetQuickAdminData) ? $scope.model.biz.fnGetQuickAdminData() : [];
    $scope.infUserName = "Select";
    $scope.infUserSeq = 0;
    
    $scope.fnReleaseAssign = function (email) {
        var scope = angular.element(document.getElementById("member_Container")).scope();

        jQuery.support.cors = true;
      //  if (confirm("연결을 해제하시겠습니까?")) {
            $.ajax({
                url: '/api/admin/releaseinfluenceradmin',
                type: 'POST',
                dataType: 'json',
                data: {
                    USER_EMAIL: email
                },
                success: function (data) {
                    if (data > 0) {
                        //location.reload();
                        scope.initialize();
                        scope.$apply();
                        $("body, .r_container, .desktop_overlay").removeClass("open");
                    }
                    else {
                        alert("update error");
                    }
                },
                error: function () {
                    alert('webservice error');
                }
            });
     //   }
    };

    $scope.fntext = function (scope, index) {
        $scope.infUserName = scope.influencers[index].USER_NAME;
        $scope.infUserSeq = scope.influencers[index].USER_SEQ;
    };
    $scope.fnStatusSet = function (scope, status, parentIndex) {
        scope.meals_inf[parentIndex].STATUS_CD = status;
    };
    $scope.fnAdminSet = function (scope, index, parentIndex) {
        scope.meals_inf[parentIndex].ADMIN_NAME = scope.admins[index].USER_NAME;
        scope.meals_inf[parentIndex].ADMIN_SEQ = scope.admins[index].USER_SEQ;
    };
    $scope.fnSaveAssign = function ($scope, parentIndex, USER_SEQ, ADMIN_SEQ) {
        var scope = angular.element(document.getElementById("member_Container")).scope();
        
        $.ajax({
            url: '/api/admin/updateAssignInfluencer',
            type: 'POST',
            dataType: 'json',
            data: {
                STATUS_CD: $scope.meals_inf[parentIndex].STATUS_CD,
                ADMIN_SEQ: $scope.meals_inf[parentIndex].ADMIN_SEQ,
                USER_SEQ: USER_SEQ
            },
            success: function (data) {
                if (data > 0) {
                    //location.reload();
                    scope.initialize();
               //     scopeQuick.meals_inf = (scope.model.biz.fnGetData_inf) ? scope.model.biz.fnGetData_inf(ADMIN_SEQ) : [];
                    scope.$apply();
                    $("body, .r_container, .desktop_overlay").removeClass("open");
                //    scopeQuick.$apply();
                }else {
                    alert("Influencer정보수정에 실패하였습니다. 관리자에게 문의하시기 바랍니다.");
                }
            },
            error: function () {
                alert("Influencer정보수정시 에러가 발생하였습니다. 관리자에게 문의하시기 바랍니다.");
            }
        });
    };
}
function validateEmail(email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test(email);
}
function MemberController($scope, $document, $http) {
    
    var vm = this;
    $scope.currentPage = 1;
    $scope.pageSize = 100000;
    $scope.meals = [];
    $scope.meals_inf = [];
    $scope.model = {
        param: {
            pageNum: $scope.currentPage,
            pageSize: $scope.pageSize,
            orderText: '00'
        },
        param2: {
            pageNum: 1,
            pageSize: 100000
        },
        biz: {
            fnGetData : function () {
                var result1 = null;
                executeAJAXToApiGet("/api/admin/admins/" + $scope.model.param.orderText + "/" + $scope.model.param.pageNum + "/" + $scope.model.param.pageSize, JSON.stringify($scope.model.param), function (result) {
                    result1 = result;
                })
                return result1;
            },
            fnGetDataCnt: function () {
                var result1 = 0;
                executeAJAXToApiGet("/api/admin/adminscnt/", JSON.stringify($scope.model.param), function (result) {
                    result1 = result;
                })
                return result1;
            },
            fnGetData_inf: function (adminSeq) {
                //adminSeq = -1;
                var result1 = null;
                executeAJAXToApiGet("/api/admin/influencers/02/" + adminSeq + "/1/10000", JSON.stringify($scope.model.param2), function (result) {
                    result1 = result;
                })
                return result1;
            }
        }
    };
    
    $scope.initialize = function () {
        $scope.influencers = ($scope.model.biz.fnGetInfluencerData) ? $scope.model.biz.fnGetInfluencerData() : [];
        $('.loading').show();
        $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
        $('.loading').hide();
        $scope.infUserName = "Select";
        $scope.infUserSeq = 0;
    };

    $scope.fnSortChange = function () {
        $(".sort_type").removeClass("selected t_pink");
        $(event.target).addClass("selected t_pink");

        $scope.model.param.orderText = fnGetSortData();
        $('.loading').show();
        $scope.meals = ($scope.model.biz.fnGetData) ? $scope.model.biz.fnGetData() : [];
        $('.loading').hide();
    };

    $scope.sortMenuToggle = function () {
        this.sortMenuOpen = !this.sortMenuOpen;
    };
    
    $scope.fnDeleteAdmin = function (email) {
        jQuery.support.cors = true;
    //    if (confirm("삭제하시겠습니까?")) {
            $.ajax({
                url: '/api/admin/deleteadmin',
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
   //     }
    };

    $scope.fnGetProfile = function (userseq) {
        jQuery.support.cors = true;
        var scope = angular.element($("#div_member_popup")).scope();
        $.ajax({
            url: '/api/admin/admins/' + userseq,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                $('#MODIFY_USERSEQ').text(userseq);
                $('#MODIFY_NAME').text(data.USER_NAME);
                $('#MODIFY_PHOTO').attr("src", data.PHOTO_URL);
                $('#MODIFY_EMAIL').text(data.USER_EMAIL);
                $('#MODIFY_JOINDATE').text(data.JOIN_DATE);
                $('#MODIFY_LASTSIGNIN').text(data.LAST_SIGN_IN);
                if (data.NEW_USER == "1") {
                    $('#MODIFY_NEWUSER').css('display', '');
                } else {
                    $('#MODIFY_NEWUSER').css('display', 'none');
                }
                $('#MODIFY_INFLUENCERS').text(data.INFLUENCERS + " influencers");
                scope.meals_inf = ($scope.model.biz.fnGetData_inf) ? $scope.model.biz.fnGetData_inf(userseq) : [];
                scope.$apply();
                $('#div_add_member').hide();
                $('#div_modify_member').show();
            },
            error: function () {
                alert('error');
            }
        });
    };

    $document.bind('click', function (event) {

        if ($("[list-option-class]").find(event.target).length > 0)
            return;

        $("#div-sort").removeClass("open");
        $(".select_container_in").find('div.drop_c.b_gray').removeClass("open");
        // $scope.sortMenuOpen = false;
        $scope.$apply();
    });
    $scope.initialize();
}

myApp.controller('MemberQuickController', MemberQuickController);
myApp.controller('MemberController', MemberController);
