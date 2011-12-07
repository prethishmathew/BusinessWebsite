///<reference path="jquery-1.3.2-vsdoc.js" />

var newTabCreated = "N";
var emptyListNumber;
var processingTab;
var notificationOn = "N";
var pRcommonFns = { delTabRow: function(tabName, rowNumber) {
    $(tabName).deleteRow(rowNumber );
}
}

jQuery.extend(pRcommonFns, {

    delArrRow: function (delArr, rowNumber) {
        delArr.splice(rowNumber, 1);
    },
    updatetabRow: function (tabName, txtUpdate, row, col) {
        $(tabName).rows[row].cells[col].innerHTML = txtUpdate;
    },
    updateArrRow: function (arr, updateTxt, row) {
        arr.splice(row, 1, updateTxt);
    },
    errorFunction: function (errCode, rtnPage) {
        switch (errCode) {
            case "904":
            case 904:
                notificationShow("User Not authenticated. Please login");
                window.location = "http://www.myRattles.com/login.aspx?ReturnUrl=" + rtnPage;
                break;
            case "911":
            case 911:
                notificationShow("Server Busy. Try again later");
                break;
            case "912":
            case 912:
                notificationShow("Sever Not responding. Please try after some time.");
                break;
            case "0":
            case 0:
                notificationShow("Request completed sucessfully");
                break;
            case "101":
            case 101:
                notificationShow("The Input was blanks or spaces. Enter a valid value");
                break;
            case "XXX":
            case 004:
                notificationShow(rtnPage); /*Pass custom Messages in the rtnPage variable - Same as Calling notification Show directly*/
                break;
            default:
                notificationShow("Too many users logged in. Server has reached its limit. Please try after some time. ");
                break;
        }
    },
    appendHTMLtoDom: function (domName, htmlStr) {
        $(domName).append(htmlStr);
        return;
    },

    getMyrattles: function (pageNum, Wrapper, getMore) {

        var ajaxStr = "myNetworkSrv.asmx/getMyRattles";
        $("#moreDataLink").html("<img alt='Loading rattles' src='Images/ajaxPreLoaderBlocks.gif' /> Loading Your Rattles..");
        $.ajax(
        { "type": "POST",
            "dataType": 'json',
            "contentType": "application/json; charset=utf-8",
            "url": ajaxStr,
            "data": "{'pageNum': '" + pageNum + "'}",
            "success": function (msg) {
                $("#moreDataLink").html("");
                switch (msg.d.sError) {
                    case 0:
                        if (msg.d.iTotalRecords > 0) {
                            $.each(msg.d.iDataGrp, function (intIndex, ObjValue) {
                                var objDtlsAr = ObjValue.slice();
                                $("#" + Wrapper).append(objDtlsAr[1]);
                             
                            });
                        }
                        if (getMore == true) {
                            if (msg.d.iTotalRecords > 0) {
                                pageNum++;
                                $("#moreDataLink").html("<a href='#' onclick='pRcommonFns.getMyrattles(" + pageNum + "," + '"' + Wrapper + '",true' + ");return false;' > Fetch More Rattles >></a>");
                            }
                            else {
                                $("#" + Wrapper).append("<li style='color:#789;'><i>No More records to fetch</i></li>");
                            }
                        }
                        else {
                            $("#moreDataLink").html("");
                        }
                        $(".myRattles>ul>li").css('display', 'block');
                        break;
                    case 100:
                        $("#" + Wrapper).append("<li style='color:#789;'><i>At End of Last Record</i></li>");
                        $(".myRattles>ul>li").css('display', 'block');
                        $("#moreDataLink").html("");
                        notificationShow("No More rattles to fetch");
                        break;
                    default:
                        pRcommonFns.errorFunction(msg.d, "ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                        break;
                }
            },
            "error": function (msg) {
                $("#moreDataLink").html("");
                pRcommonFns.errorFunction(msg.status);
                // alert(msg.status + msg.statusText);
            }
        });

        return;

    }

});

function createCalendar() {

    try {
        $(".datepicker").datepicker({ minDate: -1, maxDate: '+1M +10D' });
    }
    catch (Err) {
        //alert("error");
 }	
	
	return;

}


function mycarousel_initCallback(carousel) {
	// Disable autoscrolling if the user clicks the prev or next button.
	/*carousel.buttonNext.bind('click', function() {
		carousel.startAuto(0);
	});

	carousel.buttonPrev.bind('click', function() {
		carousel.startAuto(0);
	});*/

	// Pause autoscrolling if the user moves with the cursor over the clip.
	carousel.clip.hover(function() {
		carousel.stopAuto();
	}, function() {
		carousel.startAuto();
	});
};



function createMasterPageCorosel() {

    try {
    // Display has to happen before .jcarousel due to erractic behaviour
        $('#lftCarousel').css('display', 'inherit');
		jQuery('#mycarousel').jcarousel({
			vertical: true,
			auto: 5,
			wrap: 'circular',
			initCallback: mycarousel_initCallback,
			buttonNextHTML: null,
			buttonPrevHTML: null,
			animation: 'slow',
			scroll: 1
			
		});

		$('.newBizUsersAds').cycle({
			pause: 1
			//timeout: 1000,
			//speed: 10000
			

		});
		
	
	}
	catch (err) {

		return;
	}
	
}

function hideEleOnLoad() {

	$(".signinFeild").hide("fast");
	$(".menuHomeCom").hide("fast");
	$(".menuGraphicsCom").hide("fast");
	$(".menuInventoryCom").hide("fast");
	$(".menuSpecialsCom").hide("fast");
	$(".notifyCss").hide("fast");
	$(".bizdata_container").hide();
	return;

}

function insertNewCpn() {


	var currentTime = new Date();
	var todaysDate = currentTime.getMonth() + "/" + currentTime.getDay() + "/" + currentTime.getFullYear() ;
	var futureDate = (currentTime.getMonth() + 1) + "/" + currentTime.getDay() + "/" + currentTime.getFullYear();

	var header = $("#TextAreaDiscountHeader").val();
	var desc = $("#TextAreaDiscountDesc").val();
	var excp = $("#TextAreaDiscountExcep").val();
	var Startdate = fncValDate($("#DiscountStartdate").val(), todaysDate);
	var expirydate = fncValDate($("#DiscountExpirydate").val(), futureDate);
	var maxCount = $("#TextMaxPrints").val();
	var orgPrice = $("#orgPrice").val();
	var cpnPrice = $("#couponPrice").val();
	//alert("maxCount is " + maxCount + "desc-" + desc);
	disCntApps.InsertDiscountForApps(header,desc, excp, Startdate, expirydate, maxCount, orgPrice,cpnPrice ,  cpnInsertSuccess, showNoNewCpn);
	
	return false;
	
}

function restoreErrorPage(pageName,errmsg) {

    var pageNum = "#" + pageName 
    $('.disCountSteps').css('display', 'none');
    $('#discountInsertstep1').css('display', 'none');
    $(pageNum).css('left', '0px');
    $(pageNum).css('display', 'block');
    $('.remark').remove(); 
    $(pageNum).append("<br> <span class='remark' style='color:red;background-color:inherit'>" + errmsg + "</span>");
    

}

function cpnInsertSuccess(returnCode) {
	var xmldata = "#remarksCpnDis";
	$(xmldata).css("remark");

	
	switch (returnCode) {


		case 0:
			notificationShow("Discount Entry has been created on " + Date());
			$('.disCountInsert').css('display','none');
			__doPostBack('ctl00_ContentPlaceHolder1_bizDisDynum', 'Insert$');
			
			break;
        case 1:
            notificationShow("The Coupon Start date is in an unknown Format. Change the date to mm/dd/yyyy format and submit again");
            restoreErrorPage("discountInsertstep2", "The Coupon Start date is in an unknown Format. Change the date to mm/dd/yyyy format and submit again");
            break;
		case 2:
		    notificationShow("The Coupon Expiry date is in an unknown Format. Change the date to mm/dd/yyyy format and submit again");
		    restoreErrorPage("discountInsertstep2", "The Coupon Start date is in an unknown Format. Change the date to mm/dd/yyyy format and submit again");
			break;
		case 3:
		    notificationShow("The Maximum Number of coupons to be printed has to be a number.");
		    restoreErrorPage("discountInsertstep2", "The Maximum Number of coupons to be printed has to be a number.");
			break;
        case 4:
            notificationShow("The Coupon Header cannot be empty");
            restoreErrorPage("discountInsertstep1", "The Coupon Header cannot be empty");
            break;
        case 5:
            notificationShow("The Coupon description cannot be empty");
            restoreErrorPage("discountInsertstep1", "The Coupon description cannot be empty");
            break;
        case 6:
            notificationShow("The Original Price cannot be empty");
            restoreErrorPage("discountInsertstep3", "The Original Price cannot be empty");
            break;
        case 7:
            notificationShow("The Coupon Price cannot be empty");
            restoreErrorPage("discountInsertstep3", "The Coupon Price cannot be empty");
            break;

		default:
			notificationShow("A unknown error was generated while updating the discounts information. Please logout and try again later.");
	}
}

	function createNewCpn() {

	    $("#ajaxdisCountLoader").html("<img src='../Images/ajaxPreLoaderBlocks.gif' alt='loading discount creator' style='vertical-align:middle'/>");
		var cpnCnt = disCntApps.getDiscountCounter(genNewCpn, showNoresults);
		
		return false;
	}

	function showNoNewCpn() {

		var xhtmlStr;
		var xmldata = "#discountCreateNewCpn";
		xhtmlStr = " You Have reached the maximum number of coupons you are authorized to create. Please upgrade your account to the next level to create more coupons.\
		You may also delete existing coupons to add new ones.";
		$(xmldata).css("remark");
		$(xmldata).html(xhtmlStr);
		return false;
		//Not in Use Any More !!!!
	}

	function genNewCpn(cpnCtr) {
	    $("#ajaxdisCountLoader").html("");
		var xhtmlStr;
		var xmldata = "#discountCreateNewCpn";
		if (cpnCtr < 11 && cpnCtr > -1) {

			$('.disCountInsert').css('display', 'block');
		}
		else {
			notificationShow("You Have reached the maximum number of coupons you are authorized to create. " );
			
		}

		

		if (cpnCtr > -1) {

			createCalendar();
		}
		return false;

	}

	function emptyNewCpn() {


		//var xmldata = "#discountCreateNewCpn";
		//$(xmldata).html("");
	    $('.disCountInsert').css('display', 'none');
	    $('.disCountSteps').css('display', 'none');
	    $('#discountInsertstep1').css('display', 'block');
	    $('#discountInsertstep1').css('left', '0px');
		return false;
	}
	
	
	
	function createNewList(listName) {


		//WebService.getListNumber(genNewList, showNoresults);

	    //if (newTabCreated == "N") --No Longer needed
        {
	        $("#ListSavedRemarks").append('<img alt="Updating" src="../Images/ajaxPreLoader.gif" />');  
			var dataReturn = WebService.getListNumber(genNewList, showNoresults);
			newTabCreated = "Y";
		}
		return false;
	}

	function delInventoryList(listName) {

	    processingTab = $('#tabs').tabs('option', 'selected');
	    $("#ListSavedRemarks").append('<img alt="Updating" src="../Images/ajaxPreLoader.gif" />');
		WebService.deleteInventoryList(listName, showDelResults, showNoresults);
		return false;

	}

	function updateInventoryList(listName,createNewList) {

		/* This is called to update the inventory list  */


		var Cnter = 0;
		var rwCnt = 0;
		var xmlStr;
		var listDesc;

		var xmldata;
		var xmlpassStr;
		xmlpassStr = "<product>";

		if (createNewList != true) {
		    while (rwCnt < 20) {

		        rwCnt = rwCnt + 1;
		        Cnter = Cnter + 1;

		        xmlpassStr = xmlpassStr + '<inv code="' + rwCnt + '">';
		        xmldata = "#ctl00_ContentPlaceHolder1_invTextItem" + listName + Cnter;
		        xmlpassStr = xmlpassStr + "<item>" + $(xmldata).val() + "</item>";

		        Cnter = Cnter + 1;
		        xmldata = "#ctl00_ContentPlaceHolder1_invTextItem" + listName + Cnter;
		        xmlpassStr = xmlpassStr + "<desc>" + $(xmldata).val() + "</desc>";

		        Cnter = Cnter + 1;
		        xmldata = "#ctl00_ContentPlaceHolder1_invTextItem" + listName + Cnter;
		        xmlpassStr = xmlpassStr + "<cost>" + $(xmldata).val() + "</cost>";

		        xmlpassStr = xmlpassStr + "</inv>"

		    }
		}
		xmlpassStr = xmlpassStr + "</product>";

		xmldata = "#ctl00_ContentPlaceHolder1_txtBoxListName" + listName;
		listDesc = $(xmldata).val();
/*
		xmldata = "#ListSavedRemarks";
		$(xmldata).css("remark");
		$(xmldata).html("Updating your Changes on our DataBase..");*/
		notificationInprogress("Updating your Changes on our DataBase..");

		WebService.updateInventoryList(xmlpassStr, listName, listDesc, showResults, showNoresults);

		if (emptyListNumber == listName && newTabCreated == "Y") {
			newTabCreated = "N";
			var delBtnHtml = '<input type="submit" name="ctl00$ContentPlaceHolder1$btnSaveList' + listName + '" value="Delete List" onclick="javascript:return delInventoryList(' + listName + ');" id="ctl00_ContentPlaceHolder1_btnDelList' + listName + '" />';
			var htmldiv = '#dynumheader' + listName;
			$(htmldiv).append(delBtnHtml);

		}

		return false;
	}

	function showResults(results) {
		/*var xmldata;
		xmldata = "#ListSavedRemarks";
		$(xmldata).css("remark");
		$(xmldata).html("Inventory List last updated on " + Date());*/
		notificationShow("Inventory List last updated on " + Date());
		__doPostBack('__Page', 'MyCustomArgument');

	}

	function showNoresults(results) {
		/*var xmldata;
		xmldata = "#ListSavedRemarks";
		$(xmldata).html("Error In calling Webservice " + results);		*/
		notificationShow("Error In calling Webservice " + results);
		__doPostBack('__Page', 'MyCustomArgument');

		return false;
	}

	function genNewList(results) {

	    $("#ListSavedRemarks").html('');  		
        switch (results) {

        	case "1":
				genhtmlNewListNew(results);
				break;

			case "2":
				genhtmlNewListNew(results);
				break;

			case "3":
				genhtmlNewListNew(results);
				break;

			case "4":
				genhtmlNewListNew(results);
				break;

            case "5":
                notificationShow("You can create only upto 4 Sub Headings");
                newTabCreated = "N";
                break;

			case "6":
			    notificationShow("You can create only upto 4 Sub Headings");
				newTabCreated = "N";
				break;

			default:
			    notificationShow("Error please try again later");
				newTabCreated = "N";
				break;

		}

		return;
	}

	
	function genhtmlNewListNew(listNum) {


		var divName;
		var xhtmlStr;
		var xhtmlStr2;
		var controlCnter = 1;
		emptyListNumber = listNum;
		divName = "#UpdatePanel" + listNum;
        


		xhtmlStr =
        '<div id="dynumheader' + listNum + ' " style="height:150px;"  ><br/>' +
        'What is the title of this section :' +
        '<input name="ctl00$ContentPlaceHolder1$txtBoxListName' + listNum + '" type="text"  id="ctl00_ContentPlaceHolder1_txtBoxListName' + listNum + '" style="width:200px;height:20px;" class="textSearchFlds" /> &nbsp;' +
                    '<input type="submit" name="ctl00$ContentPlaceHolder1$btnSaveList' + listNum + '" value="Create " onclick="javascript:return updateInventoryList(' + listNum + ',true);" id="ctl00_ContentPlaceHolder1_btnSaveList' + listNum + '" class="twtBtn"  />' +
                    '<br/> <span class="smallComment italics">"BreakFast Menu","Fiction Books","Lake Houses","Drinks"</span>' +
                    '<br> </div>'  ;
		$("#dyNumPopUpCon").html(xhtmlStr);
		//$("#pager").css("background-color", "#FFCCFF"); 
		createDialogBox(null, false);
		
/*
		$(divName).html(xhtmlStr);
		
		while (controlCnter < 60) {

			$(divName).append('<input  type="text" style="width: 300px" id="ctl00_ContentPlaceHolder1_invTextItem' + listNum + controlCnter + '" />');
			controlCnter++;
			$(divName).append('<input  type="text"  style="width: 300px"  id="ctl00_ContentPlaceHolder1_invTextItem' + listNum + controlCnter + '" />');
			controlCnter++;
			$(divName).append('<input  type="text" style="width: 70px"  id="ctl00_ContentPlaceHolder1_invTextItem' + listNum + controlCnter + '" />');
			controlCnter++;
			
			
			$(divName).append('<br />');
		}
		var $tabs = $("#tabs").tabs();

		if (($tabs.tabs("length")) < 4) {
			$tabs.tabs("add", divName, "NewTab");
			$tabs.tabs("select", listNum);
		}
        */
	}






	function showDelResults(results) {

	    $("#ListSavedRemarks").append('');
		var divName = "#UpdatePanel" + results;
		var $tabs = $("#tabs").tabs();
		//$(divName).html("");
		/*
		xmldata = "#ListSavedRemarks";
		$(xmldata).css("remark");
		$(xmldata).html("Inventory list deleted on " + Date());*/
		notificationShow("Inventory list deleted on " + Date());
		$tabs.tabs('remove', processingTab);
		$("#bizInventory").append("<div id='UpdatePanel" + results + "'> </div>");

	}

    function xmlHeaderConstruct() {

        var xmlHeader = "<?xml version='1.0'?>";
        return xmlHeader;

    }

	function fncValDate(chkDate, defaultDate) {

		var dateformat = /([1][012]|[0][1-9]|[1-9])[\/]([12][0-9]|[3][01]|[0][1-9]|[1-9])[\/]20\d\d/;

		if (dateformat.test(chkDate)) {
			if (defaultDate == " "){
				return true;
				}
			else {
				return chkDate;
			}
		}
		else {
			if (defaultDate == " ") {
				return false;
			}
			else {
				return defaultDate;
			}
		}
	}


function updateProfileDetails() {


	var fNameTxt = "#ctl00_ContentPlaceHolder1_fNameTxt";
	var lNameTxt = "#ctl00_ContentPlaceHolder1_lNameTxt";
	var bizNameTxt = "#ctl00_ContentPlaceHolder1_bizNameTxt";
	var bizCatgryTxt = "#ctl00_ContentPlaceHolder1_bizCatgryTxt";
	var bizSubCatgryTxt = "#ctl00_ContentPlaceHolder1_bizSubCatgryTxt";
	var bizDesTxt = "#ctl00_ContentPlaceHolder1_bizDesTxt";
	var capTxt = "#ctl00_ContentPlaceHolder1_capTxt";
	var webSite = "#ctl00_ContentPlaceHolder1_extWebSiteTxt"; 
	
	var xmlStr = xmlHeaderConstruct();

	xmlStr = xmlStr + "<profileCatalog>";
	xmlStr = xmlStr + "<Name1>" +  $(fNameTxt).val() +   "</Name1>";
	xmlStr = xmlStr + "<Name2>" + $(lNameTxt).val() + "</Name2>";
	xmlStr = xmlStr + "<copName>" + $(bizNameTxt).val() + "</copName>";
	xmlStr = xmlStr + "<copCat>" + $(bizCatgryTxt).val() + "</copCat>";
	xmlStr = xmlStr + "<copSubCat>" + $(bizSubCatgryTxt).val() + "</copSubCat>";
	xmlStr = xmlStr + "<copDesc>" + $(bizDesTxt).val() + "</copDesc>";
	xmlStr = xmlStr + "<copCaption>" + $(capTxt).val() + "</copCaption>";
	xmlStr = xmlStr + "<copWeb>" + $(webSite).val() + "</copWeb>";
	xmlStr = xmlStr + "</profileCatalog>";
	
	WebService.updateProfileData(xmlStr, notificationUpdatePass, notificationUpdateFail);
	return false;
}


function updateProfileContactDetails() {


	var address1 = "#ctl00_ContentPlaceHolder1_add1Txt";
	var address2 = "#ctl00_ContentPlaceHolder1_add2Txt";
	var zipCode = "#ctl00_ContentPlaceHolder1_zipTxt";
	var phNumber = "#ctl00_ContentPlaceHolder1_phoneTxt";
	var email   = "#ctl00_ContentPlaceHolder1_emailTxt";
	
	var xmlStr = xmlHeaderConstruct();

	xmlStr = xmlStr + "<profileCatalog>";
	xmlStr = xmlStr + "<add1>" + $(address1).val() + "</add1>";
	xmlStr = xmlStr + "<add2>" + $(address2).val() + "</add2>";
	xmlStr = xmlStr + "<zipCode>" + $(zipCode).val() + "</zipCode>";
	xmlStr = xmlStr + "<Phone>" + $(phNumber).val() + "</Phone>";
	xmlStr = xmlStr + "<Email>" + $(email).val() + "</Email>";
	xmlStr = xmlStr + "</profileCatalog>";

	WebService.updateProfileData(xmlStr, notificationUpdatePass, notificationUpdateFail);
	return false;
	
}

function updateProfileBizHrsDetails() {


	var regOpentime = "#ctl00_ContentPlaceHolder1_regOpenTime";
	var regCloTime = "#ctl00_ContentPlaceHolder1_regCloseTime";
	var lunFromTime = "#ctl00_ContentPlaceHolder1_lunFromTime";
	var lunToTime = "#ctl00_ContentPlaceHolder1_lunToTime";
	var splDay1 = "#ctl00_ContentPlaceHolder1_DropDownListSplDay1";
	var splTime1From = "#ctl00_ContentPlaceHolder1_splTime1From";
	var splTime1To = "#ctl00_ContentPlaceHolder1_splTime1To";
	var splDay2 = "#ctl00_ContentPlaceHolder1_DropDownListSplDay2";
	var splTime2From = "#ctl00_ContentPlaceHolder1_splTime2From";
	var splTime2To = "#ctl00_ContentPlaceHolder1_splTime2To";
	var closedays1 = "#ctl00_ContentPlaceHolder1_DropDownListClosedDays1";
	var closedays2 = "#ctl00_ContentPlaceHolder1_DropDownListClosedDays2";
	var closedays3 = "#ctl00_ContentPlaceHolder1_DropDownListClosedDays3";
	var closedays4 = "#ctl00_ContentPlaceHolder1_DropDownListClosedDays4";
	var xmlStr = xmlHeaderConstruct();

	xmlStr = xmlStr + "<profileCatalog>";
	xmlStr = xmlStr + "<workhrsstart>" + $(regOpentime).val() + "</workhrsstart>";
	xmlStr = xmlStr + "<workhrsclose>" + $(regCloTime).val() + "</workhrsclose>";
	xmlStr = xmlStr + "<lunchhrstart>" + $(lunFromTime).val() + "</lunchhrstart>";
	xmlStr = xmlStr + "<lunchhrend>" + $(lunToTime).val() + "</lunchhrend>";
	xmlStr = xmlStr + "<splDay1>" + $(splDay1).val() + "</splDay1>";
	xmlStr = xmlStr + "<splTime1From>" + $(splTime1From).val() + "</splTime1From>";
	xmlStr = xmlStr + "<splTime1To>" + $(splTime1To).val() + "</splTime1To>";
	xmlStr = xmlStr + "<splDay2>" + $(splDay2).val() + "</splDay2>";
	xmlStr = xmlStr + "<splTime2From>" + $(splTime2From).val() + "</splTime2From>";
	xmlStr = xmlStr + "<splTime2To>" + $(splTime2To).val() + "</splTime2To>";
	xmlStr = xmlStr + "<closedays>" + $(closedays1).val() + ";" + $(closedays2).val() +
            ";" + $(closedays3).val() + ";" + $(closedays4).val() + "</closedays>";
	xmlStr = xmlStr + "</profileCatalog>";
	
	WebService.updateProfileData(xmlStr, notificationUpdatePass, notificationUpdateFail);
	return false;
	
}

function notificationUpdatePass(rtnCde){

	notificationOn = "Y";

	//TEST CODE REMOVE BEFORE PROD MOVE - START HERE

	//$("#notification").html(rtnCde);
	//$(".notifyCss").show(2000);
	//return false;

	//TEST CODE REMOVE BEFORE PROD MOVE - END HERE



	if (rtnCde == "0") {
	/*
		$("#notification").html(" Update Completed successfully ! ");
		//$(".notifyCss").css('display', 'block');
		$(".notifyCss").show(2000);*/
	    notificationShow(" Update Completed successfully ! ");
	}
	else {
		notificationUpdateFail(rtnCde);
	}
	return false;

}

function notificationUpdateFail(rtnCde) {

	notificationOn = "Y";
	/*
	$("#notification").html("Error E" + rtnCde + "! - Server error. Try again later. ");
	//$(".notifyCss").css('display', 'block');
	$(".notifyCss").show(2000);*/
	notificationShow("Error E" + rtnCde + "! - Server error. Try again later. ");

}

function notificationShow(notifyMsg,fontSize,animateSpeed) {

    if (fontSize == null) {
        fontSize = "small";
    }
    if (animateSpeed == null) {
        animateSpeed = "fast";
    }
    notificationOn = "Y";
    $("#notification").css('font-size',fontSize );
    $("#notification").html(notifyMsg);
    $(".notifyCss").fadeIn (animateSpeed);
	setTimeout("closeCurrentDiv('#notification')", 7000);
	return false;
}

function closeCurrentDiv(divName,aniamteSpeed) {

    //$(divName).hide(1000);
    if (aniamteSpeed == null){aniamteSpeed = 'slow'};
    $(divName).fadeOut(aniamteSpeed); 
    return false;
}
function notificationInprogress(notifyMsg) {

	notificationOn = "Y";
	$("#notification").html(notifyMsg);
	$(".notifyCss").show(2000);
	$(".notifyCss").hide(5000);
	return;
}

/******************************************************************************************************************************************************/
/* Check the Current page Name and do the necessary page loads Loads and Updates for each page.                                                                          */
/* checkCurrentPageSetting                                                                                                                                                                                                         */
/*                                                                                                                                                                                                                                                       */
/*                                                                                                                                                                                                                                                       */
/******************************************************************************************************************************************************/

/*  THIS CODE IS NOW MOVED TO INDIVIDUAL PAGES
function checkCurrentPageSetting() {
	var sPath = window.location.pathname;
	var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
	alert(sPage);
	switch (sPage) {
		case "showBiz.aspx":
                showBizPafeFunction();
                break;
        default:
                break;
              }
     return;
}*/


function showBizPafeFunction() {

	$("#ShowBizMainMenuAboutUs").tabs().addClass('ui-tabs-vertical ui-helper-clearfix');
	$("#ShowBizMainMenuAboutUs li").removeClass('ui-corner-top').addClass('ui-corner-left');

	$("#ShowBizMainMenuAboutUs").tabs();
	$("#ShowBizMainMenuAboutUs").tabs('option', 'ajaxOptions', { async: false });
	$("#ShowBizMainMenuAboutUs").tabs('option', 'cache', true);

	return;

}

function createXMLdoc(xmlstring) {
	try {
		var Internetbrowser = navigator.appName;
		var doc;
		if (Internetbrowser == 'Microsoft Internet Explorer') {
			doc = new ActiveXObject('Microsoft.XMLDOM');
			doc.async = 'false'
			doc.loadXML(xmlstring);
		}
		else {
			doc = (new DOMParser()).parseFromString(xmlstring, 'text/xml');
		}
		//alert("here");

		return doc;
	}
	catch (err) {
		//alert("Error description: " + err.description);
}
   }


/************************************************************************************
*                                                                                   *
*    genPrintPage - To print HTML Content.                                          *
*   pass the html content to print ;                                                *
*                                                                                   *
*                                                                                   *
*************************************************************************************/

function genPrintPage(contentHolder) {


	var WindowObject = window.open('', "PrinterWindow",
	                    "fullscreen=yes, scrollbars=auto,toolbars=yes");
	WindowObject.document.write('<html><head><title>Coupon Printer</title>')
	WindowObject.document.write('<link href="Styles/pradsStyle.css" rel="stylesheet" type="text/css" />');
	WindowObject.document.write('<link href="Styles/printStyle.css" rel="stylesheet" type="text/css"  media="print"  />');
	WindowObject.document.write('</head><body><center>');
	WindowObject.document.write($(contentHolder).html());
	WindowObject.document.close();
	WindowObject.focus();
	WindowObject.print();
	WindowObject.close();

}

function removeAllComments(comStr) {

	comStr = comStr.replace(/"/gi, '');
	comStr = comStr.replace(/'/gi, '');
	return comStr;

}


function isSpaces(comStr){


	if (comStr == '') {
		return false;
	}
	else {
		return true;
		 }

}
		

function isNumeric(comStr) {

	var re = new RegExp("\p{N}*");
	return comStr.match(re);
}

function callUploadShuffler(cssIDwithDot) {

	$('.ScrolldisCnt').cycle({
	pause: 1,
	fit: 1
		
	});


}
/*****************************************/
/*****************************************/
/*                                       */
/* Err Check Routine for Login functions */
/* Main Functions:ErrChkRtn              */
/*                                       */
/* SubRoutines:                          */
/* checkForValidUser                     */
/* verifyPasswordstrength                */
/*                                       */
/*                                       */
/*                                       */
/*****************************************/
/*****************************************/

function ErrChkRtn(reqType, inpVal, displayHol) {

    switch (reqType) {

        case 'validUserName':
            checkForValidUser($(inpVal).val(), displayHol);
            break;

        case 'verifyPasswordstrength':
            verifyPasswordstrength($(inpVal).val(), displayHol);
            break;
        default:
            break;


    }


}


function checkForValidUser(userName, displayHol) {

    $(displayHol).html("Loading..");
    $.ajax({
        type: "POST",
        url: "../prodServices.asmx/deleteInvdoc",
        data: "{'userName':'" + userName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(result) {
            if (result.d == "validUser") {
                $(displayHol).html("Available..");
                $(displayHol).css('color', 'green');
            }
            else if (result.d == "invalidUser") {
                $(displayHol).html("The names already taken! Try Another Name");
                $(displayHol).css('color', '#E55B3C');
            }
        },
        error: function() {
            $(displayHol).html(userName);
            $(displayHol).css('color', 'inherit');

        }
    });


}

function verifyPasswordstrength(password, displayHol) {

    var noofchar = /^.*(?=.{6,}).*$/;
    var checkspace = /\s/;
    var best = /^.*(?=.{6,})(?=.*[A-Z])(?=.*[\d])(?=.*[\W]).*$/;
    var strong = /^[a-zA-Z\d\W_]*(?=[a-zA-Z\d\W_]{6,})(((?=[a-zA-Z\d\W_]*[A-Z])(?=[a-zA-Z\d\W_]*[\d]))|((?=[a-zA-Z\d\W_]*[A-Z])(?=[a-zA-Z\d\W_]*[\W_]))|((?=[a-zA-Z\d\W_]*[\d])(?=[a-zA-Z\d\W_]*[\W_])))[a-zA-Z\d\W_]*$/;
    var weak = /^[a-zA-Z\d\W_]*(?=[a-zA-Z\d\W_]{6,})(?=[a-zA-Z\d\W_]*[A-Z]|[a-zA-Z\d\W_]*[\d]|[a-zA-Z\d\W_]*[\W_])[a-zA-Z\d\W_]*$/;
    var bad = /^((^[a-z]{6,}$)|(^[A-Z]{6,}$)|(^[\d]{6,}$)|(^[\W_]{6,}$))$/;



    if (true == checkspace.test(password)) {
        $(displayHol).html('Spaces are not allowed');
        $(displayHol).css('color', 'inherit');
    }
    else if (false == noofchar.test(password)) {
        $(displayHol).html('must be 6 char');
        $(displayHol).css('color', 'inherit');
    }
    else if (best.test(password)) {
        $(displayHol).html('Most Secure Password ');
        $(displayHol).css('color', 'green');
    }
    else if (strong.test(password)) {

        $(displayHol).html('Strong Password');
        $(displayHol).css('color', 'inherit');

    }
    else if (weak.test(password) == true && bad.test(password) == false) {
        $(displayHol).html('Weak');
        $(displayHol).css('color', 'inherit');

    }
    else if (bad.test(password)) {
        $(displayHol).html('Bad Password.');
        $(displayHol).css('color', '#E55B3C');
    }


}


function createDialogContents()
{

    $('.closer').click(function() {
    $(this).parent().hide();
    $('#fade').remove();
    });
     
    
} 
         
function createDialogBox(dialogDiv,needBlackBG) {

    if (needBlackBG == true) {
        $('body').append('<div id="fade"></div>');
        $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn("slow");
    }
    if (dialogDiv == null) {
        $('.dialog').css('display', 'block');
    }
    else {
        $(dialogDiv).css('display', 'block');
        
    }
    $('.opaque').css({ 'filter': 'alpha(opacity=40)' });
  //  $('body').height($('.dialog').height () + 700)  ;

}

function checkforNonAlphaNumeric(str) {

    var checkStr = /[^0-9a-zA-Z!@#$%^&*()+\s]/;
    if (true == checkStr.test(str)) {
        return false;
    }
    else
        return true;

}

function genLoginID() {

    
    $('#aSignin').click(function () {
        $(".signinFeild").show('fast');
        $(this).html('');
    });

    $('#aSigninClose').click(function () {
        $(".signinFeild").hide('fast');
        $(".signinFeild").hide('fast');
        //$("#aSignin").html('<input id="Button1" type="button" value="Login" class="btnHeader" />');
        $("#aSignin").html('<input type="image"  id="ImageButton1" src="Images/login.png" style="border-width:0px;" />');
    });




}