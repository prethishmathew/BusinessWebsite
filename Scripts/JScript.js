///<reference path="jquery-1.3.2-vsdoc.js" />

var loadDisCountData = "N";

function javaAlert() {
	alert("On Focus");
	return false;
}

function javablur() {
	alert("On Blur");
  
}

function signinGenrator() {

	var htmlString;	
	htmlString = "<fieldset id='signin_menu' >";
    htmlString = htmlString + "<label for='username'>Username or email</label>";
    htmlString = htmlString + "<input type='text' id='username' name='session[username_or_email]' value='' title='username'  tabindex='4'/>";
    htmlString = htmlString + "</fieldset>";
    
    //document.getElementById("spanSignin").innerHTML = htmlString;
    
    $("spanSignin").html("<p> hello </p>");
   
    $("test1").html("<p> hello </p>");
       
    //return false;

   }
   
   
   function removeHTML(strElement,strText) {
   	
   	$(strElement).innerHTML =strText;
   	
   }


   function saveWebGraphics() {
   
   	var remarkDiv = "#ctl00_ContentPlaceHolder1_LabelwebGraphicsRem";
   	$(remarkDiv).empty();  
   	
   	var colorChkRegEx = new RegExp("^#?([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$","i");
   	var bgColorCde = ($("#ctl00_ContentPlaceHolder1_bgColorText").val()).toLowerCase();
   	var fgColorCde = ($("#ctl00_ContentPlaceHolder1_fgColorText").val()).toLowerCase();
   	var webTheme = ($("#ctl00_ContentPlaceHolder1_webTheme").val());
   	if (!colorChkRegEx.test(bgColorCde)) {
   		$(remarkDiv).html("Invalid Background Color Code" + bgColorCde); 
   		return false;
   	}

   	if (!colorChkRegEx.test(fgColorCde)) {
   		$(remarkDiv).html("Invalid Font Color Code");
   		return false;
   	}

    $(remarkDiv).html('<img src="../Images/ajaxPreLoader.gif" alt="in progress" /> ..Database Update in Progress..'); 
   	
   	WebService.updateGraphicsData(bgColorCde, fgColorCde,webTheme, graphicsSuccess, graphicsNoresults);

   	return false;

   }

   function graphicsNoresults() {
   	
   	$("#ctl00_ContentPlaceHolder1_LabelwebGraphicsRem").html("Server Calls not Responding.. Please Try Again Later");
   	return false;
   
   }

   function graphicsSuccess(rtnCde) {

   	
   	switch (rtnCde) {

   		case 0:
   			$("#ctl00_ContentPlaceHolder1_LabelwebGraphicsRem").html("Graphics Information Updated.");
   			break;
   		case 1:
   			$("#ctl00_ContentPlaceHolder1_LabelwebGraphicsRem").html("Invalid Background Color Code.");
   			break;
   		case 2:
   			$("#ctl00_ContentPlaceHolder1_LabelwebGraphicsRem").html("Invalid font Color Code.");
   			break;
   		case 3:
   			$("#ctl00_ContentPlaceHolder1_LabelwebGraphicsRem").html("Heavy data transfer traffic encountered. Please try again later");
   			break;
   		default:
   			$("#ctl00_ContentPlaceHolder1_LabelwebGraphicsRem").html("Unkown Error while Updating. Please try again later");
   			break;
   		
   	}
   	return false;
   }

// Doc Ready Function for Jquery:




   $(document).ready(function () {

       hideEleOnLoad();
       createCalendar();
       createDialogContents();

       $("#pageHeader").css("opacity", "0.9");
       $("#myRattles").css("opacity", "0.9");
       try { checkCurrentPageSetting(); } catch (err) { }
       try { createdefaultSearchSettings(); } catch (err) { }

       createMasterPageCorosel();
       //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(createCalendar);

       $("#tabs").tabs();
       $("#tabs").tabs('option', 'ajaxOptions', { async: false });
       $("#tabs").tabs('option', 'cache', true);

       $("#tabsPics").tabs();
       $("#tabsPics").tabs('option', 'ajaxOptions', { async: false });
       $("#tabsPics").tabs('option', 'cache', true);



       $("#tabsProfile").tabs();
       $("#tabsProfile").tabs('option', 'ajaxOptions', { async: false });
       $("#tabsProfile").tabs('option', 'cache', true);



       $(".bizdata_container").css({ visibility: "visible" });

       /*
   	
       $(".signinFeild").hide("fast");
       $(".menuHomeCom").hide("fast");
       $(".menuGraphicsCom").hide("fast");
       $(".menuInventoryCom").hide("fast");
       $(".menuSpecialsCom").hide("fast");

       */

       // This 

       //$('#aSignin').toggle(function() {
       //	$(this).html("Close");
       //	//$(this).addClass("signinFeild");/
       //}, function() {
       //	$(this).html("Login");
       //	//$(this).removeClass("signinFeild");
       //});

       //$('#aSignin').click(function() {
       //	$(".signinFeild").toggle("slow");

       //});
       $("h2.menudropDown").toggle(function () {
           $(this).addClass("active");
       }, function () {
           $(this).removeClass("active");
       });

       $("h2.menudropDown").click(function () {
           $(this).next(".bizdata_container").slideToggle("slow");
       });



       $("#ctl00_ContentPlaceHolder1_PnlTitleMenuDis").click(function () {
           if (loadDisCountData == "N") {
               loadDisCountData = "Y";
           }
       });


       $('body').click(function (e) {
           var target = $(e.target);
           if (notificationOn == "Y") {
               notificationOn = "N";
           }
           else {
               //$(".notifyCss").fadeOut("slow");
               //$(".notifyCss").css('display', 'none');
               $(".notifyCss").hide(2000);


           }
       });

       $('.textSearchFlds').focus(function (e) {

           if ($(this).val() == "I'm Looking for ?? ( Restaurants, Mamma Mia's)" ||
                    $(this).val() == "City,State(Aitkin,MN) Or Zip") {
               $(this).val("");
           }

       });

       $('.textSearchFlds').blur(function (e) {

           if ($.trim($(this).val()).length == 0) {
               var CurrID = $(this).attr('id');
               if (CurrID == 'ctl00_TextBoxBizType') {
                   $(this).val("I'm Looking for ?? ( Restaurants, Mamma Mia's)");
               }
               if (CurrID == 'ctl00_TextBoxLoc')
               { $(this).val('City,State(Aitkin,MN) Or Zip'); }
           }

       });

   });

   	
   	
   	
   	
   	
   	
   	
   