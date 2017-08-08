/*
 *  Forever21 common.js
 */
//var SITE_PREFIX = '';

$(document).ready(function () {

});

$(document).ajaxStart(function () {
     $(".loading").show();
   // $(".loading").css("display", "table");
});

$(document).ajaxStop(function () {
    setTimeout(function () {
       $(".loading").hide();
      //  $(".loading").css("display", "none");
    }, 300);
});

function executeAJAXToApi() {
       var url = null;
    var type = "POST";
    var contentType = "application/json";
    var data = null;
    var successFunction = null;

    $('.loading').show();

    url = arguments[0];
    data = arguments[1];
    successFunction = arguments[2];

    $.ajax({
        async: false,
        type: type,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            successFunction(result);
            setTimeout(function () {
                $(".loading").hide();
            }, 300);
        },
        error: function (e) {
            alert('webservice error');
            $('.loading').hide();
        }
    });
};

function executeAJAXToApiGet() {
    var url = null;
    var type = "GET";
    var contentType = "application/json";
    var data = null;
    var successFunction = null;

    $('.loading').show();

    url = arguments[0];
    data = arguments[1];
    successFunction = arguments[2];

    $.ajax({
        async: false,
        type: type,
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            successFunction(result);
            setTimeout(function () {
                $(".loading").hide();
            }, 300);
        },
        error: function (e) {
            alert('webservice error');
            $('.loading').hide();
        }
    });
};

function executeAJAXToModel() {
    var url = null;
    var type = "POST";
    var contentType = "application/json";
    var data = null;
    var successFunction = null;
    var errorFunction = null;
    var isLoadingOpt = null;

    if (true) {
        $('.loading').show();
    }

    url = arguments[0];
    data = arguments[1];
    successFunction = arguments[2];
    errorFunction = arguments[3];
    isLoadingOpt = arguments[4];

    $.ajax({
        url: SITE_PREFIX + url,
        type: type,
        contentType: contentType,
        data: data,
        aysncType: true,
        dataType: "JSON",
        success: function (result) {
            if (successFunction) {
                successFunction(result);
            }
            else {
                //alert(result);
            }
            $('.loading').hide();
        },
        error: function (e) {
            if (errorFunction) {
                errorFunction(e);
            }

            if (isLoadingOpt)
                $('.loading').hide();
        }
    });
};

//-- AJAX JQuery overloading function --
//How to use ->
function executeAJAX() {
    var url = null;
    var type = null;
    var dataType = null;
    var data = null;
    var asyncType = null;
    var successFunction = null;
    var errorFunction = null;
    var isLoadingOpt = null;

    if (arguments.length >= 9 || arguments.length < 3) {
        return alert("Parameter Error");
    }
    else if (arguments.length == 8) {
        url = arguments[0];
        type = arguments[1];
        dataType = arguments[2];
        data = arguments[3];
        asyncType = arguments[4];
        successFunction = arguments[5];
        errorFunction = arguments[6];
        isLoadingOpt = arguments[7];
    }
    else if (arguments.length == 7) {
        url = arguments[0];
        type = arguments[1];
        dataType = arguments[2];
        data = arguments[3];
        asyncType = arguments[4];
        successFunction = arguments[5];
        errorFunction = arguments[6];
    }
    else if (arguments.length == 6) {
        url = arguments[0];             
        type = arguments[1];            
        dataType = arguments[2];        
        data = arguments[3];
        asyncType = arguments[4];
        successFunction = arguments[5];
    }
    else if (arguments.length == 5) {
        url = arguments[0];
        type = arguments[1];
        dataType = arguments[2];
        data = arguments[3];
        asyncType = arguments[4];
    }
    else if (arguments.length == 4) {
        url = arguments[0];
        type = arguments[1];
        dataType = arguments[2];
        data = arguments[3];
        asyncType = true;
    }
    else {
        url = arguments[0];
        type = arguments[1];
        dataType = arguments[2];
        data = "GET";
        asyncType = true;
    }

    if (isLoadingOpt)
        $('.loading').show();

    $.ajax({
        url: SITE_PREFIX + url,
        type: type,
        dataType: dataType,
        data: data,
        async: asyncType,
        success: function (result) {
            if (successFunction != null) {
                successFunction(result);
            }
            else {
                //alert(result);
            }

            if (isLoadingOpt)
                $('.loading').hide();
        },
        error: function (e) {
            if (errorFunction != null) {
                errorFunction(e);
            }
            else {
                //alert("Error");
            }

            if (isLoadingOpt)
                $('.loading').hide();
        }
    });
};

 var fnChangeSelectControlCommon = function (itemType, control, value, name) {
        if ($(control).is("dd")) {
            $(control).parents('.select_container').find('select').val(value);
            $(control).parents('.drop_c').find('.t_pink').each(function () {
                $(this).removeClass('t_pink');
            });
            $(control).find('span').addClass('t_pink');
        }
        else if ($(control).is("select")) {
            value = $(control).val();
            name = $(control).find('option[value=\'' + value + '\']').text();
        }

        // change Desktop 
        $(control).parents('.select_container').find('p').html(name);

        //save the selected value
        $('#hd_' + itemType.toLowerCase()).val(value);
        //    $('#hd_' + itemType.toLowerCase() + '_n').val(name);
        //    $('#hd_' + itemType.toLowerCase() + '-error').html(''); //tooltip
}


var fnChangedl = function (itemType, control, value, name) {

        fnChangeSelectControlCommon(itemType, control, value, name);

        if ($(control).is("select")) {
            value = $(control).val();
            name = $(control).find('option[value=\'' + value + '\']').text();
        }

}



