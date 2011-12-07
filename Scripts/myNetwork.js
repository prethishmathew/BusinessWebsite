var shopTable;
var fansTable;
var lClick;
var firstClick = true;

    var fnChggrpMethods = { alertMyHi: function(typ) { alert("hi" + typ); } }

    jQuery.extend(fnChggrpMethods, {

        fnChgGrpScr: function(id, typ) {
            /*Show the screen to change grp */

            var number = $.data(document.body, 'apos');
            var dataVar = new Array();
            var ajaxVar;
            $.data(document.body, 'move2Grp', null);
            switch (typ) {
                case "myFans":
                    dataVar = $.data(document.body, 'myFanGrps');
                    break;
                case "myShops":
                    dataVar = $.data(document.body, 'myShopGrps');
                    break;
            }


            var dialogCon = "#dyNumPopUpCon";
            $(dialogCon).html("");
            $(dialogCon).css('left-padding', '40px');
            $(dialogCon).append("<div id='chgGrpDyNumDiv' ></div>");
            dialogCon = "#chgGrpDyNumDiv";
            $(dialogCon).append("<h3>Select the group to move to </h3><ul id='myUlDynum' style='left-padding:20px;list-style:none;'>");
            dialogCon = "#myUlDynum";
            $.each(dataVar, function(intIndex, myGrp) {
                $(dialogCon).append('<li><Input type="radio" Name="rGrps" Value="' + myGrp + '" onclick="fnChggrpMethods.fnsetChgGrpValue(this.value) ">' + myGrp + '</li>');
            });
            dialogCon = "#chgGrpDyNumDiv";
            $(dialogCon).append('<input id="ButtonGrpChng" type="button" class="twtBtn" value="Change Group"  onclick="fnChggrpMethods.ajaxChangeGrp(' + "'" + id + "','" + typ + "','" + number + "'" + ')" /> ');
            dialogCon = ".opaque";
            $(dialogCon).html("Change group:");
            createDialogBox();

        }, /*end */

        ajaxChangeGrp: function(id, typ, num) {
            var toGrp = $.data(document.body, 'move2Grp');
            var ajaxStr, currTab;
            if (toGrp == null) {
                notificationShow("No group was selected. Please select a group to proceed.");
                return false;
            }

            switch (typ) {
                case "myShops":
                    ajaxStr = "../myNetworkSrv.asmx/MyShopChgGrp";
                    currTab = "tableShops";
                    break;
                case "myFans":
                    ajaxStr = "../myNetworkSrv.asmx/MyFanChgGrp";
                    currTab = "tableFans";
                    break;
            }

            pRcommonFns.appendHTMLtoDom("#dyNumPopUpCon", '<img alt="" src="../Images/ajaxPreLoaderBlocks.gif"  />');

            $.ajax({ "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": ajaxStr,
                "data": "{'id': '" + id + "'," +
                  "'toGrp':'" + toGrp + "'}",
                "success": function (msg) {
                    switch (msg.d) {
                        case "0":
                            //    fansTable.fnUpdate(toGrp, num, 2);
                            //$(currTab).rows[num].cells[2].innerHTML = toGrp;
                            document.getElementById(currTab).rows[parseInt(num) + 1].cells[2].innerHTML = toGrp;

                            //        shopTable.fnUpdate(toGrp, num, 2);
                            $('.closer').parent().hide();
                            $('#fade').remove();
                            notificationShow("The group was changed.");
                            break;
                        default:
                            pRcommonFns.errorFunction(msg.d, "ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                            break;
                    }
                },
                "error": function (msg) {
                    pRcommonFns.errorFunction(msg.d, "ReturnUrl=%2fsecpages%2fmyNetwork.aspx");

                    //alert(msg.status + msg.statusText);
                }
            });
            return false;
        },


        fnCreateGrpBtn: function (typ) {

            var htmlBtn = '<div style="text-align:center;"><br> <input type="button"  class="twtBtn" value="New Group" onclick="fnChggrpMethods.fnShowAddGrpScr(' + "'" + typ + "'" + ')" /></div>';
            return htmlBtn;
        },

        fnShowAddGrpScr: function(typ) {

            var dialogCon = "#dyNumPopUpCon";
            $(dialogCon).html("");
            $(dialogCon).append('<br> Group Name: <input type="text" id="grpChgVal" /> ');

            $(dialogCon).append('<br>  <input type="button" class="twtBtn"  value="Add Group" name="Add Group" onclick="fnChggrpMethods.fnAddGroup(' + "'" + typ + "'" + ')" />');
            dialogCon = ".opaque";
            $(dialogCon).html("Add a new group:");
            createDialogBox();
        },

        fnAddGroup: function(typ) {
            var grpName = $("#grpChgVal").val();
            if (!checkforNonAlphaNumeric(grpName)) {
                notificationShow("Invalid Character in Group Name. Only Numbers and alphabets allowed for grp Name");
                return false; }
            if (!isSpaces(grpName)) {
                notificationShow("Enter a meaningful group Name. Blanks are not allowed.");
                return false;
            } 
            var ajaxStr;
            var domName;
            var wrapper;
            var dataVar = new Array();
            var cacheName;
            switch (typ) {
                case "myShops":
                    domName = "#myShopsGrps";
                    wrapper = "#myShops";
                    dataVar = $.data(document.body, 'myShopGrps');
                    cacheName = 'myShopGrps';
                    break;
                case "myFans":
                    domName = "#myFansGrps";
                    wrapper = "#myFans";
                    dataVar = $.data(document.body, 'myFanGrps');
                    cacheName = 'myFanGrps';
                    break;
            }

            ajaxStr = "../myNetworkSrv.asmx/addNewGrp";
            pRcommonFns.appendHTMLtoDom("#dyNumPopUpCon", '<img alt="" src="../Images/ajaxPreLoaderBlocks.gif"  />');
            $.ajax({ "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": ajaxStr,
                "data": "{'grpName': '" + grpName + "'," +
                        "'typ':'" + typ + "'}",
                "success": function (msg) {
                    switch (msg.d) {
                        case "0":
                            notificationShow("New group '" + grpName + "' was added to your groups");
                            /* $("<br><a href='#'>" + grpName + "</a>").prependTo(domName);
                            if (($(domName).height() > $(wrapper).height() - 20)) {
                            $(wrapper).height($(domName).height() + 30);
                            }*/
                            $('.closer').parent().hide();
                            $('#fade').remove();
                            dataVar.push(grpName);
                            setUpGrpDtls(dataVar, domName, false);
                            break;
                        case "811":
                            notificationShow("The group already exists");
                            $('.closer').parent().hide();
                            $('#fade').remove();
                            break;
                        default:
                            pRcommonFns.errorFunction(msg.d, "ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                            $('.closer').parent().hide();
                            $('#fade').remove();                            
                            break;
                    }
                },
                "error": function(msg) {
                    pRcommonFns.errorFunction(msg.status,"ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                    $('.closer').parent().hide();
                    $('#fade').remove();
                            
                    //alert(msg.status + msg.statusText);
                }
            });
        },

        fnsetChgGrpValue: function(grp) {
            $.data(document.body, 'move2Grp', grp);
        },

        errorFunction: function(errCode) { }

    });
    
        
function checkCurrentPageSetting() {
//    $.fnChggrpMethods.fnShowDataScreen();
    $(".mainDiv").tabs();
    setUpDataTablesForPage("#tableShops", "../myNetworkSrv.asmx/getMyShops", "#myShopsGrps");

    $("#showFans").click(function(event) {

        if (firstClick) {
            setUpDataTablesForPage("#tableFans", "../myNetworkSrv.asmx/getMyFans", "#myFansGrps");
            firstClick = false;
        }
    });
    //setUpMenuForPage();
    //setUpDatTabForOne();

}

function setUpDataTablesForPage(tabID,ajaxSrvId,grpID) {

    $(document).ready(function() {
    
        if (tabID == '#tableShops') {
            shopTable = $(tabID).dataTable({
                "bJQueryUI": true,
                //"sPaginationType": "full_numbers",
                //"bServerSide": true,
                "sDom": '<"H"lf>t<"F"rip>',
                "sAjaxSource": ajaxSrvId,
                "bLengthChange": false,
                "bPaginate": false,
                "bInfo": false,
                "bFilter": true,
                "bProcessing": true,
                "fnServerData": function(sSource, aoData, fnCallback) {
                    $.ajax({ "type": "POST",
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "url": sSource,
                        "data": "{'sEcho': '1'}",
                        "success": function(msg) {
                            fnCallback(msg.d);
                            setUpMenuForPage(tabID);
                            setUpGrpDtls(msg.d.iDataGrp, grpID, true);
                        },
                        "error": function(msg) {
                        pRcommonFns.errorFunction(msg.d,"ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                           // alert(msg.status + msg.statusText);
                        }
                    });
                }
            });
        }
        if (tabID == '#tableFans') {
            fansTable = $(tabID).dataTable({
                "bJQueryUI": true,
                //"sPaginationType": "full_numbers",
                //"bServerSide": true,
                "sDom": '<"H"fl>t<"F"ipr>',
                "sAjaxSource": ajaxSrvId,
                "bLengthChange": false,
                "bPaginate": false,
                "bInfo": false,
                "bFilter": true,
                "bProcessing": true,
                "fnServerData": function(sSource, aoData, fnCallback) {
                    $.ajax({ "type": "POST",
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "url": sSource,
                        "data": "{'sEcho': '1'}",
                        "success": function(msg) {
                            fnCallback(msg.d);
                            setUpMenuForPage(tabID);
                            setUpGrpDtls(msg.d.iDataGrp, grpID, true);
                        },
                        "error": function(msg) {
                        pRcommonFns.errorFunction(msg.status,"ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                            //alert(msg.status + msg.statusText);
                        }
                    });
                }
            });
        }
    });
    
    return false;
}


function setUpMenuForPage(domDiv) {

    /*This unbind is to prevent the duplicate propogation of the click event on myshops */
    $('.menuDrop li a').unbind('click').click(function(e) {
            e.stopPropagation();    /*Prevents the duplicate propogation of the click event on myshops */
        });
    
    
    $(document).ready(function() {
        $(".menuDrop li a").click(function() {
            var hidden = $(this).parents("li").children("ul").is(":hidden");
            $(".menuDrop>ul>li>ul").hide()
            $(".menuDrop>ul>li>a").removeClass();
            if (hidden) {
                $(this)
                        .parents("li").children("ul").show()
						.parents("li").children("a").addClass("zoneCur");
            }
            return false;
        });
    });
        
    $(".dataTables_filter").css('float', 'right');

    $(document).click(function(event) {
        if (!$(event.target).parents("li").children("a").hasClass('zoneCur')) {
            $(".menuDrop>ul>li>ul").hide();
            $(".menuDrop>ul>li>a").removeClass();
            return false;
        } 
    });

    $('.validLink').unbind('click').click(function(e) {
            e.stopPropagation();
    });
    $('.dialog').unbind('click').click(function(e) {
            e.stopPropagation();
    });


    //$(domDiv + ' tbody a').click(function() {
    $("#tableFans" + ' tbody a').click(function() {
    
        //$('.myShopsTab tbody tr').click(function() {
        var aRow = fansTable.fnGetPosition($(this).closest("tr").get(0));
        jQuery.data(document.body, 'apos', aRow);

    });
    $("#tableShops" + ' tbody a').click(function () {

        //$('.myShopsTab tbody tr').click(function() {        
        var aRow = shopTable.fnGetPosition($(this).closest("tr").get(0));        
        jQuery.data(document.body, 'apos', aRow);

    });

    //var hts = $(domDiv).height() + 100;
    $(domDiv).parent().parent().parent().height($(domDiv).height() + 50); 
     
}


function confirmRemoveShop(id,typ) {

    var number = jQuery.data(document.body, 'apos');
    var objData;
    if (typ == "myFans") {
        objData = fansTable.fnGetData(number).slice();
    }
    else { objData = shopTable.fnGetData(number).slice(); }
var dialogCon = "#dyNumPopUpCon";
$(dialogCon).html("");
$(dialogCon).css('left-padding', '20px');
$(dialogCon).append("<div id='chgGrpDyNumDiv' ></div>");
dialogCon = "#chgGrpDyNumDiv";
$(dialogCon).append("<br> Do you want to remove <b><u>'" + objData[1] + "'</b></u> from your list ? <br>");
$(dialogCon).append("<i> Press 'Yes' to confirm or close the box to cancel </i> <br>");
$(dialogCon).append('<input id="ButtonGrpChng" type="button" value="Yes"  class="twtBtn" onclick="removeMyShop(' + "'" + id + "','" + number + "','" + typ + "'" + ')" /> <br><br>');
dialogCon = ".opaque";
$(dialogCon).html("Confirm Delete:");
            
createDialogBox(null,false);



}
function removeMyShop(id,number,typ) {
    /*Also used to remove my fans*/
    
    if (number == null) {
        number = jQuery.data(document.body, 'apos');
    }
    var ajaxStr;
    var currTab;
    if (typ == "myShops")
    { ajaxStr = "../myNetworkSrv.asmx/deleteMyShop"; }
    else
        if (typ == "myFans")
        { ajaxStr = "../myNetworkSrv.asmx/deleteMyFan"; }

        pRcommonFns.appendHTMLtoDom("#dyNumPopUpCon", '<img alt="" src="../Images/ajaxPreLoaderBlocks.gif"  />');
        //$("#dyNumPopUpCon").append('<img alt="" src="../Images/ajaxPreLoaderBlocks.gif"  />' );
        
    //$.fn.dataTableExt.iApiIndex = "0";
        $.ajax({ "type": "POST",
            "dataType": 'json',
            "contentType": "application/json; charset=utf-8",
            "url": ajaxStr,
            "data": "{'id': '" + id + "'}",
            "success": function (msg) {
                switch (msg.d) {
                    case "0":
                        if (typ == "myShops") {
                            shopTable.fnDeleteRow(number);
                            notificationShow("Shop removed from My shops.");

                        } else if (typ == "myFans") {
                            fansTable.fnDeleteRow(number);
                            notificationShow("Fan Removed from your list");
                        }

                        $('.closer').parent().hide();
                        $('#fade').remove();
                        break;
                    default:                        
                        pRcommonFns.errorFunction(msg.d, "ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                        break;
                }
            },
            "error": function (msg) {
                
                pRcommonFns.errorFunction(msg.status);
               // alert(msg.status + msg.statusText);
            }
        });


}

function setUpGrpDtls(arr, domName,firstTime) {

    
    var grpArr = new Array();
    $(domName).html("");
    var objDtls;
    $.each(arr, function(intIndex, ObjValue) {
        if (firstTime) {
            var objDtlsAr = ObjValue.slice();
            objDtls = objDtlsAr[0];
            
        }
        else {
            objDtls = ObjValue;

        }
        var desc = '<br> <a href="#" onclick="dataPageSelectGrp(' + "'" + objDtls + "'" +
        ')" >' + objDtls + ' </a>';

        //var desc = '<br> <a href="#" onclick="dataPageSelectGrp(' + "'" + objDtls[0] + "'" +
        //')" >' + objDtls[0] + '(' + objDtls[1] + ') </a>';
        $(desc).appendTo(domName);
        //grpArr[intIndex] = objDtls[0];
        grpArr[intIndex] = objDtls;
    });
    var btncode;
    var wrapper;
    switch (domName) {
        case "#myShopsGrps":
            btnCode = fnChggrpMethods.fnCreateGrpBtn("myShops");
            wrapper = "#myShops";
            $.data(document.body, 'myShopGrps', grpArr);
            break;
        case "#myFansGrps":
            btnCode = fnChggrpMethods.fnCreateGrpBtn("myFans");
            wrapper = "#myFans";
            $.data(document.body, 'myFanGrps', grpArr);
            break;
    }
    $(btnCode).appendTo(domName);

    if ($(domName).height() > $(wrapper).height() - 20) {
        $(wrapper).height($(domName).height() + 30);
    }
}
function dataPageSelectGrp(myGrp,tabpos) {


    return false;
    
}

function changeMyGrp(id, typ) {

    var number = $.data(document.body, 'apos');
    var dataVar = new Array();
    var ajaxVar;
    $.data(document.body, 'move2Grp', null);
    switch (typ) {    
    case "myFans":
        dataVar = $.data(document.body, 'myFanGrps');
        break;
    case "myShops":
        dataVar = $.data(document.body, 'myShopGrps');
        break;
}


var dialogCon = "#dyNumPopUpCon";
$(dialogCon).html("");
$(dialogCon).css('left-padding', '40px');
$(dialogCon).append("<div id='chgGrpDyNumDiv' ></div>");
dialogCon = "#chgGrpDyNumDiv";
$(dialogCon).append("<h3>Select the group to move to </h3><ul id='myUlDynum' style='left-padding:20px;list-style:none;'>");
dialogCon = "#myUlDynum";
$.each(dataVar, function(intIndex, myGrp) {
$(dialogCon).append('<li><Input type="radio" Name="rGrps" Value="' + myGrp + '" onclick="setChgGrpValue(this.value) ">' + myGrp + '</li>');
});
dialogCon = "#chgGrpDyNumDiv";
$(dialogCon).append('<input id="ButtonGrpChng" type="button" value="Change Group" class="twtBtn" onclick="grpChgCall(' + "'" + id + "','" + typ + "','" + number + "'" + ')" /> ');
createDialogBox(null,true);
}

function setChgGrpValue(grp) {
    
    $.data(document.body, 'move2Grp', grp);

}

function grpChgCall(id, typ,num) {

    
    var toGrp = $.data(document.body, 'move2Grp');
    var ajaxStr;
    if (toGrp == null) { 
    notificationShow("No group was selected. Please select a group to proceed."); 
    return false;
    }

    switch (typ) {
        case "myShops":
            ajaxStr = "../myNetworkSrv.asmx/MyShopChgGrp";
            $.fn.dataTableExt.iApiIndex = "0";
            break;
        case "myFans":
            ajaxStr = "../myNetworkSrv.asmx/MyFanChgGrp";
            $.fn.dataTableExt.iApiIndex = "1";
            break;
    }


    $.ajax({ "type": "POST",
        "dataType": 'json',
        "contentType": "application/json; charset=utf-8",
        "url": ajaxStr,
        "data": "{'id': '" + id + "'," +
                  "'toGrp':'" + toGrp + "'}",
        "success": function(msg) {
            switch (msg.d) {
                case "0":

                    switch (typ) {
                        case "myShops":
                            $.fn.dataTableExt.iApiIndex = "1";
                            shopTable.fnUpdate(toGrp, num, 2);
                            break;
                        case "myFans":
                            $.fn.dataTableExt.iApiIndex = "1";
                            shopTable.fnUpdate(toGrp, num, 2);
                            break;
                    }

                    $('.closer').parent().hide();
                    $('#fade').remove();
                    notificationShow("Your group change was sucessful");
                    break;
                default:
                    pRcommonFns.errorFunction(msg.d,"ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                    break;
            }
        },
        "error": function(msg) {
        pRcommonFns.errorFunction(msg.status);
            //alert(msg.status + msg.statusText);
        }
    });
    return false;
}

function updateMsgFlag(id, flag, msgMode,obj) {

    
    //var e = window.event;
    var num = shopTable.fnGetPosition($(obj).closest("tr").get(0));    
    var ajaxStr;
    var HTMLChg;
    var currTab = "tableShops";
    var colNum ;
   
    
    switch (msgMode) {

        case 'SMS':
            ajaxStr = "../myNetworkSrv.asmx/updateSMS";
            colNum = 4;
            break;
        case 'Email':
            ajaxStr = "../myNetworkSrv.asmx/updateEmail";
            colNum = 3
            break;
        default:
            ajaxStr = "";
            return false;
            break;
    }
    
    htmlVar = '<img alt="request pending" src="../Images/ajaxPreLoader.gif" style="outline-style:none;border-style:none;" />';
    $(obj).html(htmlVar); 
    
    $.ajax({ "type": "POST",
        "dataType": 'json',
        "contentType": "application/json; charset=utf-8",
        "url": ajaxStr,
        "data": "{'id': '" + id + "'," +
                  "'flag':'" + flag + "'}",
        "success": function (msg) {            
            switch (msg.d.sError) {
                case 0:
                    document.getElementById(currTab).rows[parseInt(num) + 1].cells[colNum].innerHTML = msg.d.sEcho;
                    notificationShow("Notification request reset");
                    break;
                default:
                    pRcommonFns.errorFunction(msg.d.sError, "ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                    break;
            }
        },
        "error": function (msg) {
            pRcommonFns.errorFunction(msg.status);
            //alert(msg.status + msg.statusText);
        }
    });

    

}