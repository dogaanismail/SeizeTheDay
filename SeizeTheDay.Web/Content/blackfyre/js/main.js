/*slider */
jQuery(window).load(function() {
    "use strict";
    var slider = jQuery('.bxslider');
    if (slider.length !== 0) {
        slider.bxSlider({

            captions: true,
            auto: false,
            nextSelector: '.bx-next-out',
            moveSlides: 1,
            breaks: [{
                screen: 0,
                slides: 2,
                pager: false
            }, {
                screen: 460,
                slides: 2
            }, {
                screen: 768,
                slides: 2
            }]
        });


        var sliderwrap = jQuery('.slider-wrapper');
        sliderwrap.css('display', 'block').fadeIn("slow");


        imagesLoaded(slider, function() {
            slider.reloadSlider();

            var text_id = slider.getCurrentSlideElement().attr('data-id');
            var text = jQuery('.slider_text_src ul li[data-id="' + text_id + '"]');
            var slidertxt = jQuery('.slider_text');
            var next_slide_text_inner = jQuery('.next_slide_text_inner');
            var pager = jQuery(document.body);

            pager.on("click", '.bx-pager-item a', function() {
                var pager_num = parseInt(jQuery(this).attr('data-slide-index')) + 1;
                var text_pg = jQuery('.slider_text_src ul li[data-id="' + pager_num + '"]');
                slidertxt.empty();
                slidertxt.append(text_pg.html());

                if (slider.getCurrentSlideElement().attr('data-id') < slider.getSlideCount()) {
                    var c = pager_num + 1;
                    var next_slide_text_inner_con = jQuery('.slider_text_src ul li[data-id="' + c + '"]');
                } else if (slider.getCurrentSlideElement().attr('data-id') == slider.getSlideCount()) {
                    var c = 1;
                    var next_slide_text_inner_con = jQuery('.slider_text_src ul li[data-id="' + c + '"]');
                }

                next_slide_text_inner.empty();
                next_slide_text_inner.append(next_slide_text_inner_con.clone().children('.slider_com_wrap').remove().end().text());

            });


            if (slider.getCurrentSlideElement().attr('data-id') < slider.getSlideCount()) {
                var c = parseInt(text_id) + 1;
                var next_slide_text_inner_con = jQuery('.slider_text_src ul li[data-id="' + c + '"]');
            }

            slidertxt.empty();
            slidertxt.append(text.html());
            next_slide_text_inner.empty();
            next_slide_text_inner.append(next_slide_text_inner_con.clone().children('.slider_com_wrap').remove().end().text());


            var streladesno = jQuery('.bx-next');
            var caption = jQuery('.bx-caption');

            streladesno.on("click", function() {

                var text_id = slider.getCurrentSlideElement().attr('data-id');
                var text = jQuery('.slider_text_src ul li[data-id="' + text_id + '"]');
                var slidertxt = jQuery('.slider_text');
                var next_slide_text_inner = jQuery('.next_slide_text_inner');

                if (slider.getCurrentSlideElement().attr('data-id') < slider.getSlideCount()) {
                    var c = parseInt(text_id) + 1;
                    var next_slide_text_inner_con = jQuery('.slider_text_src ul li[data-id="' + c + '"]');
                } else if (slider.getCurrentSlideElement().attr('data-id') == slider.getSlideCount()) {
                    var c = 1;
                    var next_slide_text_inner_con = jQuery('.slider_text_src li[data-id="' + c + '"]');
                }

                next_slide_text_inner.empty();
                next_slide_text_inner.append(next_slide_text_inner_con.clone().children('.slider_com_wrap').remove().end().text());

                slidertxt.empty();
                slidertxt.append(text.html());
            });

        });

    }
});

$(document).ready(function() {
    "use strict";

    // Newsticker
    jQuery('#webticker').webTicker()

    // Tooltip
    $('[data-rel="tooltip"]').tooltip();

    // Tabs

    $('ul.tabs li').on("click", function() {
        var tab_id = $(this).attr('data-tab');

        $('ul.tabs li').removeClass('current');
        $('.tab-content').removeClass('current');

        $(this).addClass('current');
        $("#" + tab_id).addClass('current');
    })


    $('ul.tabs2 li').on("click", function() {
        var tab_id = $(this).attr('data-tab');

        $('ul.tabs2 li').removeClass('current');
        $('.tab2-content').removeClass('current');

        $(this).addClass('current');
        $("#" + tab_id).addClass('current');
    })



    $('ul.tabs3 li').on("click", function() {
        var tab_id = $(this).attr('data-tab');

        $('ul.tabs3 li').removeClass('current');
        $('.tab3-content').removeClass('current');

        $(this).addClass('current');
        $("#" + tab_id).addClass('current');
    })


    $('ul.tabs-shop li').on("click", function() {
        var tab_id = $(this).attr('data-tab');

        $('ul.tabs-shop li').removeClass('current');
        $('.ts-content').removeClass('current');

        $(this).addClass('current');
        $("#" + tab_id).addClass('current');
    })



    $('ul.tab-news li').on("click", function() {
        var tab_id = $(this).attr('data-tab');

        $('ul.tab-news li').removeClass('current');
        $('.tabn-content').removeClass('current');

        $(this).addClass('current');
        $("#" + tab_id).addClass('current');
    })

    $('ul.tab-clan li').on("click", function() {
        var tab_id = $(this).attr('data-tab');

        $('ul.tab-clan li').removeClass('current');
        $('.tabc-content').removeClass('current');

        $(this).addClass('current');
        $("#" + tab_id).addClass('current');
    })

    // Clients
    $('.sponsor').slick({
        dots: false,
        infinite: true,
        speed: 300,
        slidesToShow: 5,
        slidesToScroll: 1,
        responsive: [{
            breakpoint: 1024,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1,
                infinite: true
            }
        }, {
            breakpoint: 600,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1
            }
        }, {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    });

    // Blog Carousel
    $('.blog-carousel').slick({
        dots: false,
        infinite: true,
        speed: 300,
        slidesToShow: 3,
        slidesToScroll: 1,
        responsive: [{
            breakpoint: 1024,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1,
                infinite: true
            }
        }, {
            breakpoint: 600,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1
            }
        }, {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }]
    });
})

$(".back-to-top").on("click", function() {
    $('body,html').animate({
        scrollTop: 0
    }, 1000);
    return false;
});

// Range slider
$("#sliderRange")
    .slider({
        range: true,
        min: 0,
        max: 500,
        step: 1,
        values: [75, 300],
        slide: function(event, ui) {
            var price1 = ui.values[0];
            var price2 = ui.values[1];
            $("#price1")
                .val("\u20a4" + price1);
            $("#price2")
                .val("\u20a4" + price2);
        }
    });

    $('#price1')
    .on('keyup', function() {
        var from = $(this)
            .val();
        var to = $('#price2')
            .val();
        $('#sliderRange')
            .slider('option', 'values', [from, to]);
    });

    $('#price2')
    .on('keyup', function() {
        var from = $('#price1')
            .val();
        var to = $(this)
            .val();
        $('#sliderRange')
            .slider('option', 'values', [from, to]);
    });

/*toogle*/
jQuery(document).ready(function(){
"use strict";
		var toogle_wrap_login = jQuery('.login-tooltip');
		var login_button = jQuery('.login-btn');
		var closelog = jQuery('#login_tooltip .closeto');
		var navwrap = jQuery('.navbar-wrapper');

		login_button.on("click", function() {
    		toogle_wrap_login.fadeTo( "fast", 1,  function() {
		     jQuery( this ).css( "top", "50%");
		      navwrap.css( "z-index", "2147483647");
		   });


		});

		closelog.on( "click", function() {
			toogle_wrap_login.fadeTo( "fast", 0,  function() {
		      jQuery( this ).css( "top", "-5000px");
		      navwrap.css( "z-index", "99999999");
		   });

		});

});



