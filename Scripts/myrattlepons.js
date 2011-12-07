$(document).ready(function () {

    getmyCoupons(1);
    
});


function getmyCoupons(PageNum)
{

    $("#loader").css('display', 'block');
    var ajaxStr = "../myNetworkSrv.asmx/getmyCoupons";
    $.ajax({ "type": "POST",
        "dataType": 'json',
        "contentType": "application/json; charset=utf-8",
        "url": ajaxStr,
        "data": "{'PageNum':" + PageNum + "}",
        "success": function (msg) {
            $("#loader").css('display', 'none');
            switch (msg.d.sError) {
                case 0:
                    $('.schemer').append(msg.d.sEcho);
                    $('#nextRecords').html("<a  href='#' onclick='getmyCoupons(" + (PageNum + 1) + ");return false;'>Get Next</a>");
                    break;
                case 100:
                    $('#nextRecords').html("");
                    break;
                default:
                    pRcommonFns.errorFunction(msg.d, "ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
                    $('.closer').parent().hide();
                    $('#fade').remove();
                    break;
            }
        },
        "error": function (msg) {
            pRcommonFns.errorFunction(msg.status, "ReturnUrl=%2fsecpages%2fmyNetwork.aspx");
            $('.closer').parent().hide();
            $('#fade').remove();

            //alert(msg.status + msg.statusText);
        }
    });

    
 
}