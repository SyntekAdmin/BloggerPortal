
var SITE_PREFIX = '';

var regularExpression = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;

$(document).ready(function () {
  
});
    var fnRemoveDOM = function (id) {
        $('#' + id).remove();
    }

    var fnClickShowPW = function () {
        var control = [];
        var showText = 'SHOW', hideText = 'HIDE';

        //Because it does not work on Firefox, must defined evnet.
        var event;
        if (arguments.length == 5) {
            event = arguments[0], showText = arguments[1], hideText = arguments[2], control[0] = arguments[3], control[1] = arguments[4];
        }
        else if (arguments.length == 4) {
            event = arguments[0], showText = arguments[1], hideText = arguments[2], control[0] = arguments[3];
        }
        else {
            return alert("Parameter Error");
        }

        //if (!event) event = window.event;
        
        for (var i = 0; i < control.length; i++) {
            if ($(control[i]).attr('type') == 'password') {
                $(control[i]).attr('type', 'text');
                event.target.innerHTML = hideText;
            }
            else if ($(control[i]).attr('type') == 'text') {
                $(control[i]).attr('type', 'password');
                event.target.innerHTML = showText;
            }
        }
    };


    fnActiveMenu = function (id, menuName) {
        $("#" + id).addClass('active');
        $("#menuName").html(menuName);
    };
















