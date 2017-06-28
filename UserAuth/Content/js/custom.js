// JavaScript Document

//jquery for increasing num of endoscopy suites using plus minus buttons start
$( ".plus-icon" ).click(function() {
  $(".suite-div2").slideDown(500);
  $(".suite-div1").slideUp(500);
  $(".minus-icon").css("display","inline-block");
  $(".plus-icon").css("display","none");
});
$( ".minus-icon" ).click(function() {
  $(".suite-div2").slideUp(500);
  $(".suite-div1").slideDown(500);
  $(".minus-icon").css("display","none");
  $(".plus-icon").css("display","inline-block");
});
//jquery for increasing num of endoscopy suites using plus minus buttons start


//Procedure Information : jquery for increasing procedure in ots (calculation screen 3) 
var htmlString = $("tr.sel-procedure").html(); //extracting html from tr of class="sel-procedure"
$(".plus-icon" ).click(function() {
    $(this).siblings('#tbl_sel_procedure').children('tbody').append('<tr class="separator" /><tr class="sel-procedure">'+ htmlString +'</tr>');
});

// vertical scrollbar
$(".scroll-bar").mCustomScrollbar({
    axis:"y" 
});