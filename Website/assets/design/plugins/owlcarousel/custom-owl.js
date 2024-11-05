jQuery(document).ready(function() {
  jQuery("#owl-demo").owlCarousel({
    loop:true,
	autoWidth:false,
	autoplay:true,
    autoplayTimeout:3000,
    margin:10,
	transitionStyle: "fade",
	//mouseDrag: true,
    animateIn: 'fadeIn',
    animateOut: 'fadeOut',
    responsive:{
        0:{
            items:1,
			loop:true,
        },
        600:{
            items:3,
			loop:true,
        },
        1000:{
            items:3,
			loop:true,
        }
    }
});
});