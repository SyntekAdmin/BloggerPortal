// Code goes here
var myApp = angular.module('forever21App', ['angularUtils.directives.dirPagination']);

myApp.directive('myRepeatDirective', function () {
    return function (scope, element, attrs) {
        $(".loading").show();
        if (scope.$last) {
            //window.alert("im the last!");
            $(document).ready(function () {
                $(".loading").hide();
                /* Quick View Slide */
                jQuery(".qv_slide").on('click', function () {
                    $("body, .r_container, .desktop_overlay").addClass("open");
                });
            });
        }
    };
});

function PhotosOfInfListController($scope, $document, $http) {
    var vm = this;
    $scope.currentPage = 1;
    $scope.pageSize = 1000000;
    $scope.meals = [];
    $scope.options = [{ "text": "Select", "value": "Select" }, { "text": "Reviewing", "value": "Reviewing" }, { "text": "Picked", "value": "Picked" }];

    // Download
    $scope.download = function (scope) {
        var file = scope.meals.PICKED_PHOTO_URL;
        $("#ifrmDownload").prop("src", "/admin/items/download/" + file);
    };
    // Upload
    $scope.fileList = [];
    $scope.curFile;
    $scope.ImageProperty = {
        file: ''
    }
    $scope.ImmediatelyUploadFile = function (element) {
        $scope.fileList = [];
        // get the files
        var files = element.files;
        for (var i = 0; i < files.length; i++) {
            $scope.UploadFileIndividual(files[i],
                files[i].name,
                files[i].type,
                files[i].size,
                i);
            $scope.ImageProperty.file = files[i];

            $scope.fileList.push($scope.ImageProperty);
            $scope.ImageProperty = {};
            $scope.$apply();
        }
    }

    $scope.UploadFileIndividual = function (fileToUpload, name, type, size, index) {
        //Create XMLHttpRequest Object
        var reqObj = new XMLHttpRequest();

        //event Handler
        reqObj.upload.addEventListener("progress", uploadProgress, false)
        reqObj.addEventListener("load", uploadComplete, false)
        reqObj.addEventListener("error", uploadFailed, false)
        reqObj.addEventListener("abort", uploadCanceled, false)


        //open the object and set method of call(get/post), url to call, isasynchronous(true/False)
        reqObj.open("POST", "/api/admin/setretouched", true);

        //set Content-Type at request header.For file upload it's value must be multipart/form-data
        reqObj.setRequestHeader("Content-Type", "multipart/form-data");

        //Set Other header like file name,size and type
        reqObj.setRequestHeader('X-File-Name', name);
        reqObj.setRequestHeader('X-File-Type', type);
        reqObj.setRequestHeader('X-File-Size', size);
        reqObj.setRequestHeader('X-File-AdminSeq', $("#adminSeq").val());
        reqObj.setRequestHeader('X-File-PhotoSeq', $("#photoSeq").val());

        var form_data = new FormData();
        form_data.append("file", fileToUpload);
        form_data.append('PhotoSeq', '2');


        // send the file
        reqObj.send(fileToUpload);

        function uploadProgress(evt) {
            $(".loading").show();
        }

        function uploadComplete(evt) {
            $(".loading").hide();
            $("#file").val("");
            $scope.initialize();
            $scope.$apply();
        }

        function uploadFailed(evt) {
            //document.getElementById('P' + index).innerHTML = 'Upload Failed..';
        }

        function uploadCanceled(evt) {

            //document.getElementById('P' + index).innerHTML = 'Canceled....';
        }

    }
        
    function SaveToDisk(fileURL, fileName) {
        // for non-IE
        if (!window.ActiveXObject) {
            var save = document.createElement('a');
            save.href = fileURL;
            save.target = '_blank';
            save.download = fileName || 'unknown';

            var event = document.createEvent('Event');
            event.initEvent('click', true, true);
            save.dispatchEvent(event);
            (window.URL || window.webkitURL).revokeObjectURL(save.href);
        }

        // for IE
        else if (!!window.ActiveXObject && document.execCommand) {
            var _window = window.open(fileURL, '_blank');
            _window.document.close();
            _window.document.execCommand('SaveAs', true, fileName || fileURL)
            _window.close();
        }
    }

    $scope.model = {
        param: {
            //필수
            pageNum: $scope.currentPage,
            pageSize: $scope.pageSize,
            //옵션
            adminSeq : '-1',
            productNo : '',
            skuNo: '',
            userSeq: '-1',
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
                executeAJAXToApiGet("/api/admin/getmyitem" + params, JSON.stringify(""), function (result) {
                    result1 = result;
                })
                return result1;
            },
            fnGetData : function () {
                //return [];
                var result1 = null;
                var params = "?";
                params += "adminSeq=" + o_adminSeq + "&";
                params += "productNo=" + o_productNo + "&";
                params += "skuNo=" + o_skuNo + "&";
                params += "userSeq=" + o_userSeq + "&";
                executeAJAXToApiGet("/api/admin/getmyitemlistview" + params, JSON.stringify(""), function (result) {
                    result1 = result;
                })
                return result1;
            },
            fnGetDataCnt : function () {
                return 1;
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
        $scope.model.param.userSeq = o_userSeq;
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