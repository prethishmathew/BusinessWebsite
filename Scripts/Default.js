// INTC:

oldTab = 0;
//function checkCurrentPageSetting() {
function createdefaultSearchSettings() {
    var sPath = document.URL;
    var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
    var sPage = sPage.substring(0, sPage.lastIndexOf('.aspx') + 5);    // Removes all Extra strings on the url
    getSrchResults(sPage);
	genLoginID();
	tabNavNLoad();

	
	$("#pageMiddle").css('background', 'inherit'); 
	return false;


}


function getSrchResults(sPage) {

    var webUrl = "prodServices.asmx/getSrchResults";
    switch (sPage) {
        case "bizinfo.aspx":
        case "accountInfo.aspx":
        case "myNetwork.aspx":
        case "DiscountDetails.aspx":
        case "myrattlepons.aspx":
            {

                webUrl = "../prodServices.asmx/getSrchResults";
                break;
            }
        default:
            {
                webUrl = "prodServices.asmx/getSrchResults";
                break;
            }
    }
    $("#ctl00_searchBtn").click(function (event) {

        //if (!isSpaces($("#ctl00_ContentPlaceHolder1_TextBoxLoc").val())) {
        if (!isSpaces($("#ctl00_TextBoxLoc").val())) {
            notificationShow("Zip Code is empty. Please Enter a Valid State or city"); ; return false;

        }

        var location = $("#ctl00_TextBoxLoc").val();
        var biztype;
        var distance = $("#ctl00_DropDownRadius").val();

        if (!isSpaces($("#ctl00_TextBoxBizType").val())) {
            biztype = null;
        }
        else {
            biztype = $("#ctl00_TextBoxBizType").val();
        }

        if (location == "City,State(Aitkin,MN) Or Zip") {
            notificationShow("Zip Code is empty. Please Enter a Valid State or city");
            return false;
        }

        if (biztype == "I'm Looking for ?? ( Restaurants, Mamma Mia's)") {
            notificationShow("Enter a valid search string.");
            return false;
        }

        $("#headerLoadImg").css('display', 'inline-block');
        
        $.ajax({
            type: "POST",
            url: webUrl,
            data: "{'zipOrCity':'" + location + "'," +
				        "'distance':'" + distance + "'," +
				        "'bizDetails':'" + biztype + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: srchCallSuccess,
            error: srchCallFailed
        });
        return false;
    });

   }

   function srchCallSuccess(result) {
       
   	var htmlData = result.d;   	
   	$("#pager").html(htmlData);
   	$("#taber").tabs();
   	$(".tabs-bottom .ui-tabs-nav, .tabs-bottom .ui-tabs-nav > *")
 		.removeClass("ui-corner-all ui-corner-top")
  		.addClass("ui-corner-bottom");
   	tabNavNLoad();
   	callUploadShuffler('.ScrolldisCnt');
   	createDialogBox("#masterContentHolder", true);
   	$("#headerLoadImg").css("display", "none");
    return false;
   
    }
    function srchCallFailed(result) {

        $("#headerLoadImg").css("display", "none");
        pRcommonFns.errorFunction("XXX","Error calling the webservice. Try again after some time");
        return false;
    }



    function tabNavNLoad() {

    	$("#tabListing  a").each(function(index) {
    		$(this).click(function() {
    			var selTab = index;

    			var pageNum = index + 1;
    			var pageName = "#page" + pageNum

    			if ($(pageName).data('panelLoaded') != 'Y' && pageNum != 1) {

    				$.ajax({
    					type: "POST",
    					url: "prodServices.asmx/getAdditionalPage",
    					data: "{'tempTabs':'" + $("#bConDSTempName").val() + "'," +
				        "'pageNum':'" + pageNum + "'}",
    					contentType: "application/json; charset=utf-8",
    					dataType: "json",
    					success: function(result) {

    						pageAddCallSuccess(result.d, pageNum);
    						$(pageName).data('panelLoaded', 'Y');
    						callUploadShuffler('.ScrolldisCnt');
    					},
    					error: srchCallFailed
    				});
    			}

    			if (selTab > oldTab) {
    				oldTab = selTab;
    				var left1 = $('#page1').position().left;
    				$(".tabs-botton-content").css({ overflow: "hidden" });
    				$(".tabs-botton-content").css({ left: left1 + 600 });
    				$(".tabs-botton-content").animate({ "left": left1 }, 'slow')
                                             .queue(function() {
                                             	$(".tabs-botton-content")
    				                         .css('overflow', 'auto')
    				                         .dequeue();
                                             });
    				return false;
    			}
    			else if (selTab < oldTab) {
    				oldTab = selTab;
    				var right1 = $('#page1').position().left;
    				$(".tabs-botton-content").css({ overflow: "hidden" });
    				$(".tabs-botton-content").css({ left: right1 - 600 });
    				$(".tabs-botton-content").animate({ "left": right1 }, 'slow')
                                             .queue(function() {
                                             	$(".tabs-botton-content")
    				                         .css('overflow', 'auto')
    				                         .dequeue();
                                             });

    			}
    		});

    	});



    }

    function pageAddCallSuccess(hCode,Idx){
    	
    	$("#page" + Idx).html(hCode);

    }


    function leftScroller(contentID) {

        var contentID = "#" + contentID;
        var right = $(confirm).position().left;
           
    
    }
