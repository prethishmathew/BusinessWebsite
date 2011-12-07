var firstClick = "Y";

function checkCurrentPageSetting() {

    $("#ShowBizMainMenuAboutUs").tabs().addClass('ui-tabs-vertical ui-helper-clearfix');
    $("#ShowBizMainMenuAboutUs li").removeClass('ui-corner-top').addClass('ui-corner-left');
    $("#ShowBizMainMenuAboutUs").tabs("option", "fx", { opacity: 'toggle' });
    $("#ShowBizMainMenuAboutUs").tabs("option", "fx", { height: 'toggle' });
    $("#ShowBizMainMenuAboutUs").tabs('option', 'ajaxOptions', { async: false });
    $("#ShowBizMainMenuAboutUs").tabs('option', 'cache', true);

    $("#ShowBizMainMenuInventory").tabs().addClass('ui-tabs-vertical ui-helper-clearfix');
    $("#ShowBizMainMenuInventory li").removeClass('ui-corner-top').addClass('ui-corner-left');
    /*$("#ShowBizMainMenuInventory").tabs();*/
    $("#ShowBizMainMenuInventory").tabs("option", "fx", { opacity: 'toggle' });
    $("#ShowBizMainMenuInventory").tabs("option", "fx", { height: 'toggle' });
    $("#ShowBizMainMenuInventory").tabs('option', 'ajaxOptions', { async: false });
    $("#ShowBizMainMenuInventory").tabs('option', 'cache', true);
    $(".mainMenu").css('display', 'block');
    
    loadFancyBoxDetails();
    loadDiscountDetails();
    loadSNNetworking();
    getShopRattles();
    return;


}

function getShopRattles() {


    $.ajax({
        type: "POST",
        url: "myNetworkSrv.asmx/getShopsRattle",
        data: "{'shopID':'" + $("#ctl00_ContentPlaceHolder1_memIDHide").val() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            switch (msg.d.sError) {
                case 0:
                    //alert(msg.d.sColumns);
                    $(".myRattles").css('display', 'block');
                    $("#myRattles").css('opacity', 'none');
                    $("#myRattles").append(msg.d.sColumns);

                    //createSlider(10000, msg.d.iTotalRecords, 1);
                    //createSlider(5000, 2, 1);
                    break;
                default:
                    pRcommonFns.errorFunction("XXX", "Some prob");
                    break;
            }
        },
        "error": function (msg) {
            pRcommonFns.errorFunction(("XXX", "Some prob2"));
        }
    });
    return;
    

}

function loadDiscountDetails() {


    $("#showDiscounts").click(function(event) {

        if (firstClick == "Y") {
            $.ajax({
                type: "POST",
                url: "ProductionServices.asmx/getAllDiscounts",
                data: "{'membershipID':'" + $("#ctl00_ContentPlaceHolder1_memIDHide").val() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: AjaxSucceeded,
                error: AjaxFailed

            });

            
        }
    });

    return;

}

function AjaxSucceeded(result) {

    
    $("#ShowBizMainMenuDiscounts").html(result.d);
    $("#ShowBizMainMenuDiscounts").append('<div style="clear:both"></div>');
    firstClick = "N";
}
function AjaxSucceeded1(result) {

    xml = (result.d);
    var XMLcount = 0;
    //$("#ShowBizMainMenuDiscounts").html(tryme);    
    // Develop the table to populate the discounts

    var oTable = document.createElement("TABLE");
    oTable.className = "tblsCustom";
    var oTBody0 = document.createElement("TBODY");
    var oTBody1 = document.createElement("TBODY");
    //var oTHead = oTable.createTHead();
    var oTHead = document.createElement("THEAD");
    var oTFoot = oTable.createTFoot();
    var oCaption = oTable.createCaption();
    var oRow, oCell;

    var heading = new Array();
    
    heading[0] = "Coupon";
    heading[1] = "ExpiryDate";
    heading[2] = " ";

    var couponList = new Array();

    xmlDoc = createXMLdoc(xml);

    $(xmlDoc).find("xmlDiscountDtls").each(function() {

        var disid = ($(this).find('bizDisCountID').text()).replace(/'/gi, " ");
        var head =  ($(this).find('DiscountHeader').text()).replace(/'/gi, " ");
        var desc =  ($(this).find('DiscountDesc').text()).replace(/'/gi, " ");
        var excp =   $(this).find('DiscountExp1').text().replace(/'/gi, " ");
        var sDte =   $(this).find('DiscountStartdate').text();
        var eDte =   $(this).find('DiscountExpirydate').text();

        disid = removeAllComments(disid);
        head  = removeAllComments(head) ;
        excp  = removeAllComments(excp) ;
        desc  = removeAllComments(desc) ;
        sDte  = removeAllComments(sDte) ;
        eDte  = removeAllComments(eDte) ;


        couponList[XMLcount] = new Array(head, eDte,
		"<input id='cpnPrint" + XMLcount + "'  type='button' value='View Details' " +
		                        " onclick=" + '"' + "javascript:genDynamicfancyBox('" + head + "','" + desc + "','" + excp + "','" +
		                       eDte + "')" + '"' + "' />");
        XMLcount++;
    });

    oTable.appendChild(oTHead);
    oTable.appendChild(oTBody0);
    oTable.appendChild(oTBody1);

    oRow = document.createElement("TR");
    oTHead.appendChild(oRow);

    for (i = 0; i < heading.length; i++) {
        oCell = document.createElement("TH");
        if (i == 0) {
            oCell.width = "65%";
        }
        oCell.innerHTML = heading[i];
        oRow.appendChild(oCell);
    }


    for (i = 0; i < couponList.length; i++) {
        var oBody = (i < 2) ? oTBody0 : oTBody1;
        oRow = oBody.insertRow(-1);
        for (j = 0; j < couponList[i].length; j++) {
            oCell = oRow.insertCell(-1);
            oCell.innerHTML = couponList[i][j];
        }
    }

    /*
    $(oTable).append(oTHead);
    $(oTable).append(oTBody0);
    $(oTable).append(oTBody1);
    */

    if (XMLcount > 0) {
        $("#ShowBizMainMenuDiscounts").append(oTable);
        
    }
    firstClick = "N";

}

function AjaxFailed(result) {
    //alert(result.status + '-' + result.statusText);
    firstClick = "Y";
}

function loadFancyBoxDetails() {


    $("a[rel=gallery_group]").fancybox({ 'transitionIn': 'none', 'transitionOut': 'none', 'titlePosition': 'over' });
    return;

}

function genDynamicfancyBox(var1, var2, var3, var4) {

    var htmlfancyBox = genFBhtml(var1, var2, var3, var4);

    $("#couponGen").remove();
    $("#discountDtlsContainer").append(htmlfancyBox);
    $("#discountDtlsContainer").css("printdata");
    var printCoupon = "<div id='printCpn' class='printCpn'>\
	<input id='cpnPrint'  type='button' class='btn' value='Print Coupon'\
	onclick=" + '"' +
    //"javascript:genPrintPage('" + var1 + "','" + var2 + "','"  + var3 + "')" + '"' +
	"javascript:genPrintPage('#discountDtlsContainer')" + '"' +
	" /> </div>";

    $.fancybox({ 'autoScale': false,
        'autoDimensions': false,
        //'content': htmlfancyBox + printCoupon,
        'content': printCoupon + $("#discountDtlsContainer").html(),
        //'height'      : 'auto',
        //'width'       :'54',
        'padding': 0
    });
    $.fancybox.center;
    $.fancybox.resize;

}

function genFBhtml(var1, var2, var3, var4) {

    var htmlfancyBox = "<div id='couponGen' class='couponGen'><h2>" + $('h2:eq(0)').html() + "</h2>";
    htmlfancyBox = htmlfancyBox + "<h3>" + $('h3:eq(0)').html() + "</h3>";
    htmlfancyBox = htmlfancyBox + "<p class='LargerFont leftJustified'>" + var1 + "</p>";
    htmlfancyBox = htmlfancyBox + "<p class='mediumFont leftJustified' >" + var2 + "</p>";
    htmlfancyBox = htmlfancyBox + "<p class='smallFont leftJustified'>" + var3 + "</p>";
    htmlfancyBox = htmlfancyBox + "<p class='mediumFont leftJustified'> Expiry Date:" + var4 + "</p>";
    htmlfancyBox = htmlfancyBox + "<p class='smallFont leftJustified'> Specials and Discounts brought " +
	            "to you by myRattles.com. Please note that the owner and myRattles.com has the right to cancel " +
	            "any Coupons and discounts without any notice. The owner of the entity has the right to " +
	            "dishonour the Coupons and discounts without any prior notice or Explanations. Please inform " +
	            "the owner of intent to use this coupon before availing any service. </p> " +
	            "<img id='cpn_ImageLogo' src='Images/myRattlesblueV1.png' style='border-width:0px;' />";




    htmlfancyBox = htmlfancyBox + "</div>";

    return htmlfancyBox;

}


function loadSNNetworking()
{
    var htmlTag;

    $.ajax({ "type": "POST",
        "dataType": 'json',
        "contentType": "application/json; charset=utf-8",
        "url": "myNetworkSrv.asmx/isMyShop",
        "data": "{'id': '" + $("#ctl00_ContentPlaceHolder1_memIDHide").val() + "'}",
        "success": function(msg) {
            if (msg.d == false) {
                htmlTag = '<button type="button" onclick="genAddMyShop();"  class="twtBtn" style="font-size: 14px ;"  > <img src="Images/edit-add-2.png" alt=""  /> Follow Me! </button>   ';
            }
            else {
                htmlTag = '<button type="button" onclick="return false;"  class="twtBtn" style="font-size: 14px ;"  > <img src="Images/dialog-accept.png" alt=""  /> I am a Fan! </button>    ';
            }
            $("#SNBtnDiv").html("");
            $("#SNBtnDiv").append(htmlTag);

        },
        "error": function(msg) {
            //alert(msg.status + msg.statusText);
        }
    });
    
}



function genAddMyShop() {

    $.ajax({ "type": "POST",
        "dataType": 'json',
        "contentType": "application/json; charset=utf-8",
        "url": "myNetworkSrv.asmx/addMyShop",
        "data": "{'id': '" + $("#ctl00_ContentPlaceHolder1_memIDHide").val() + "'," +
                  "'grp':'" + null + "'}",
        "success": function(msg) {
            switch (msg.d) {
                case "0":
                    var htmlTag = '<button type="button" onclick="return false;"  class="twtBtn" style="font-size: 14px ;"  > <img src="Images/dialog-accept.png" alt=""  /> I am a Fan! </button>    ';
                    $("#SNBtnDiv").html("");
                    $("#SNBtnDiv").append(htmlTag);

                    break;
                default:
                    var rtnUrl = "showbiz.aspx?id=" + $("#ctl00_ContentPlaceHolder1_memIDHide").val();
                    pRcommonFns.errorFunction(msg.d,rtnUrl );
                    break;
            }
        },
        "error": function(msg) {
            var rtnUrl = "showbiz.aspx?id=" + $("#ctl00_ContentPlaceHolder1_memIDHide").val();
            pRcommonFns.errorFunction(msg.status + msg.statusText,rtnUrl );
        }
    });


}

function getLikes(id) {

    var to = 100;
    var from  = 1;
    var final = Math.round([Math.random() * (to - from +1)] + from) ;
    $("#L" + id).html(final);
    return false;
    
}