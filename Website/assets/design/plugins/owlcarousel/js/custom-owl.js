jQuery(document).ready(function() {
 
  jQuery("#owl-demo").owlCarousel({
    loop:true,
	autoplay:true,
    autoplayTimeout:1000,
   
    margin:10,
    
	transitionStyle: "fade",
	//mouseDrag: true,
              animateIn: 'fadeIn',
              animateOut: 'fadeOut',
			  items : 3,
      itemsDesktop : [1199,3],
      itemsDesktopSmall : [979,3]
    //responsive:{
//        0:{
//            items:1
//        },
//        600:{
//            items:3
//        },
//        1000:{
//            items:3
//        }
//    }
});
 
 
 
 
});