function checkCurrentPageSetting() {

	Sys.WebForms.PageRequestManager.getInstance().add_endRequest(createCalendar);
	return;


}




function deleteInvFile() {

var location = "i";
$.ajax({
	type: "POST",
	url: "../prodServices.asmx/deleteInvdoc",
	data: "{'zipOrCity':'" + location + "'}",
	contentType: "application/json; charset=utf-8",
	dataType: "json",
	success: function(result) {
		if (result.d == "Success") {
			__doPostBack('ctl00_ContentPlaceHolder1_BtnDeleteInvDoc', 'Delete$');
		}
		else {
			notificationShow("Oops.. Error while deleting Inventory doc. Please try later.. " + Date());
		}
	},
	error: function() {
	notificationShow("Oops.. Error while deleting Inventory doc. Please try later.. ");
	}
});
	return false;
}


function turnRight(obj) {


    var leftPos = $(obj).parent().next().position().left;
    $(obj).parent().next().css({ left: leftPos + 500 });
    $(obj).parent().next().css("overflow", "hidden");
    $(obj).parent().stop(true, true);
    $(obj).parent().next().stop(true, true);
    $(obj).parent().animate({ "left": leftPos - 700 }, 'fast')
                           .queue(function () {
                               $(obj).parent()
                                    .css('display', 'none');
                               $(obj).parent().next().css('display', 'block')
                                .animate({ "left": leftPos }, { duration: 'slow', queue: false })
                           });


}

function turnLeft(obj) {

    $(obj).parent().stop(true, true);
    $(obj).parent().prev().stop(true, true);
    var leftPos = $(obj).parent().prev().position().left;

    $(obj).parent().animate({ "left": leftPos + 500 }, 'fast')
                    .queue(function () {
                        $(obj).parent().css('display', 'none');
                        $(obj).parent().prev().css('display', 'block')
                                        .animate({ "left": leftPos }, { duration: 'slow', queue: false })
                    });

}

function createReviewCoupon(obj) {

    var htmlStr = "<h3 style='color:blue;text-align:center;'>" + $("#TextAreaDiscountHeader").val() + "</h3>";    
    htmlStr += "<b style='color:green;float:right;'>Original Price: $" + $("#orgPrice").val() + "</b></br>";
    htmlStr += "<b style='color:green;float:right;'>You get it for: $" + $("#couponPrice").val() + "</b></br>";
    htmlStr += "<b style='color:green;float:right;'>Total Coupon Cost: $" + $("#couponPrice").val() * $("#TextMaxPrints").val() + "</b></br>";
    htmlStr += $("#TextAreaDiscountDesc").val() + "<br>";
    htmlStr += "<i>This coupon is valid between " + $("#DiscountStartdate").val() + " and " + $("#DiscountExpirydate").val();
    htmlStr += ("</i><br/><br/> ");
    htmlStr += "<span class='smallFont'>" + $("#TextAreaDiscountExcep").val() + "</span> </br></br>";
    
    $("#disCountreviewText").html(htmlStr);
    turnRight(obj);
}