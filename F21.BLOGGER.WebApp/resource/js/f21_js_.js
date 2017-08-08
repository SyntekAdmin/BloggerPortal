
$(function(){
	/* Mobile Slide Menu //delete */
	$(".icon_menu").on("click",function() {
		$("body, .c_container, .mobile_overlay, .l_container, .signup_wrapper").addClass("open");
	});
	$(".mobile_overlay").on("click",function() {
		$("body, .c_container, .mobile_overlay, .l_container, .signup_wrapper").removeClass("open");
	});

	/* Quick View Slide*/
	/*$(".qv a, .mybag div a, #want a").on("click",function() {
		$("body, .r_container, .desktop_overlay").addClass("open");
	});*/
	$(".r_container .icon_close, .desktop_overlay").on("click",function() {
		$("body, .r_container, .desktop_overlay").removeClass("open");
	});

	/* Mobile Search Box //delete 
	$(".m_header .nav_secondary .icon_search").on("click",function(){
		$(".m_search_container").slideToggle();
	});
	$(".m_search_container .icon_close").on("click",function(){
   		$(".m_search_container").slideToggle();
   	});*/
	

	/* Desktop Search Box //delete
	$(".show_desktop .icon_search").click(function(){
   		$(".d_search_container").animate({width: 'toggle'});
   	});
   	$(".d_search_container .icon_close").click(function(){
   		$(".d_search_container").animate({width: 'toggle'});
   	});*/
	
	/* Scroll */
	/*$(window).scroll(function () {
	    if ($(this).scrollTop() > 400) {
	        $('.scrollToTop').fadeIn();
	    } else {
	        $('.scrollToTop').fadeOut("fast");
	    }
	});*/
	$('.scrollToTop').click(function () {
	    $('html, body').animate({
	        scrollTop: 0
	    }, 900);
	    return false;
	});

	/* Mobile Left Menu */
	$('.l_container .mega_menu').each(function () {
	    $(this).click(function () {
	        $(this).toggleClass('active');
	        $(this).find('.mega_sub').slideToggle();
	    });
	});

	/* Dropdown //modify
	$('.drop_p').each(function () {
	    $(this).click(function () {
	        $(this).next().addClass('open');
	    });
	});*/

	/* Checkout Slide */
	$('.expand_p').each(function () {
	    $(this).click(function () {
	        $(this).toggleClass('active');
	        $(this).next().slideToggle();
	    });
	});

	/* Checkout Input //modify
	$('.input_label').each(function () {
		$(this).click(function (){
			$(this).addClass('focus');
		});
	});

	$('.tab_title').each(function () { //delete
	    $(this).click(function () {
	        $(this).find('.tab_content').slideToggle();
	    });
	});*/

	/* Mobile Dropmenu  //modify
	if (window.devicePixelRatio > 1 ) {
	  $('h1').click(function(){
			$('.nav_c').slideToggle();
			$('.side_left_menu').slideToggle();
		})
	}*/

	/* Filter Dropdown //delete
	$('.m_filer').click(function(){
		$('.side_menu').slideToggle();
	});*/

	/* 500 Under //delete
	$('.want_free').click(function(){
		$('#want').slideToggle();
	}); */

	/* tab Menu
	if(window.innerWidth >= 768) {
		$(".tab_m > div").click(function(){
			var tabGroup = $(this).parent().parent();
			var tabMenuGroup = tabGroup.find(".tab_m > div");
									
			tabMenuGroup.removeClass("active");
			$(this).addClass("active");
			
			var index = tabMenuGroup.index(this); 

			tabGroup.find(".tab_c > div").addClass("hide");	
			tabGroup.find(".tab_c > div:eq(" + index + ")").css('display','none').removeClass("hide").fadeIn(500);
			
		});	
	} else {
		$(".tab_m > div").mouseenter(function(){
			var tabGroup = $(this).parent().parent();
			var tabMenuGroup = tabGroup.find(".tab_m > div");
									
			tabMenuGroup.removeClass("active");
			$(this).addClass("active");
			
			var index = tabMenuGroup.index(this); 

			tabGroup.find(".tab_c > div").addClass("hide");	
			tabGroup.find(".tab_c > div:eq(" + index + ")").css('display','none').removeClass("hide").fadeIn(500);
			
		});	
	}*/


	/* Blgger Potal (Influencers) add-on */
	/* Quick View Slide */
	jQuery(".qv_slide").on('click', function() {
		$("body, .r_container, .desktop_overlay").addClass("open");
	});

	/* Dropdown */
	$('.drop_p').each(function () {
		$(this).click(function () {
			if ($(this).next().hasClass('drop_c')) 
				$(this).next().toggleClass('open');
			else 
				$(this).children('.drop_c').toggleClass('open');
			});
		});
		$('div.drop_c.b_gray').each(function () {
			$(this).click(function () {
			$(this).toggleClass('open');
		});
	});

	/* Mobile Menu */
	$('h1').click(function () {
		if (window.innerWidth < 768) {
			$('.side_left_menu').slideToggle();
		}
	});
	$(window).on('resize load', function(e) { // desktop sideMenu display
		if ($(window).width() > 767) {	
			$('.side_left_menu').removeAttr('style');
		}
	});
	
	/* Input */
	$('.input_label').each(function () {
		$(this).click(function () {
		$(this).children('input').focus();
			$('.input_label').each(function () {
			if ($(this).children('input').val() == '') {
				$(this).removeClass('focus');
				$(this).find('.icon_delete').hide();
			}
		});
		$(this).addClass('focus');
	});

	/* Focus and focusout for input */
	$(this).find('input').focus(function () {
		$(this).parent('.input_label').addClass('focus');
	}).blur(function () {
		if ($(this).val() == '') {
			$(this).parent('.input_label').removeClass('focus');
			$(this).parent('.input_label').find('.icon_delete').hide();
		}
	});

	/* Clear input */
	$('.icon_delete').click(function(){
		$(this).parent('.input_label').find('input').each(function () {
			$(this).val('');
			$(this).valid();
		});
	});

        // Show and hide delete icon
	$(this).find('input').keydown(function () {
		if ($(this).val() == '') 
			$(this).parent('.input_label').find('.icon_delete').hide();
		else
			$(this).parent('.input_label').find('.icon_delete').show();
		});
	});

	/* Tab */
jQuery(document).on('click','.tablayer li a',function(e){
	var $tab_li = $(this).parent(),
		tabindex = $tab_li.index();
	e.preventDefault();
	$(this).parent().addClass('active').siblings().removeClass('active');

	var tab_content = $(this).parents('.tab_wrap').siblings('.tab_container').find('.tab_content');
	if ($(tab_content).length > 0){
		$(tab_content).removeClass('active');
		$(tab_content).eq(tabindex).addClass('active');
	};
});

	/* Header Fixed Scroll */
	$(window).scroll(function () {
		if ($(this).scrollTop() > 86) {
			$('header').addClass('header_fixed');
			$('main,footer').addClass('main_fixed');
		} else {
			$('header').removeClass('header_fixed');
			$('main,footer').removeClass('main_fixed');
		}
	});
});

// 기본파일첨부
function setFilePath(oObj,tObj) {
	$("#"+tObj).val($("#"+oObj).val());
}
function selUpFile(tObj) {
	$("#"+tObj).click();
}
function resetUpFile(tObj) {
	$("#"+tObj).val('');
}