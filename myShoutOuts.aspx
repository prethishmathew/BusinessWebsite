<%@ Page Language="C#" AutoEventWireup="true" CodeFile="myShoutOuts.aspx.cs" Inherits="BizUsers_myShoutOuts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css" >
    .myShouts {
     background-color:#D3F9BC ; 
     font-weight:lighter;
     font-style:italic ;
     font-size:small ;
     padding:1px;
     width:405px;
      border:thin solid #C5F7A6; 
    }
    .myShouts .myShoutsMsg textarea{ height:auto ; overflow:hidden ;  }
    .myShouts .myShoutsMsg textarea:focus{    outline-color:Blue; outline-width:thin; outline-style:solid ;         }
    .myShouts .myShoutsMsg .myShoutsBtn {  width:400px;text-align:right  ;}
    .myShouts .myShoutSubmit { }
    .myShouts .myShoutOuts{}
    .myShouts .shoutOutlist{
       list-style-type:none ;  
        font-size:15px ;   
       font-style:normal ;
        color:#444;
       width:403px;
           padding:0px;
           text-indent:0px;     
    }
    .myShouts .shoutOutlist ul{margin-left:0px;padding-left:0px;
        border-bottom-width:2px;
        border-bottom-color:#C5F7A6;  
        border-bottom-style:solid ; 
        
    }
    .myShouts .shoutOutlist li{
        padding:5px;
        margin-left:0px;padding-left:0px;
        border-top-width:2px;
        border-top-color:#C5F7A6;  
        border-top-style:solid ; 
         list-style-type:none ; 
        padding-top:10px;
        padding-bottom:20px;
     
    }
    .myShouts .shoutOutlist li:hover{ background-color:#C5F7A6 }
    .myShouts .shoutOutlist li:focus{ background-color:#C5F7A6 }
     
    .myShouts .shoutOutlist li a{
        font-size:10px ;   
       font-style:normal ;               
    }
    
    </style> 
    <script src="Scripts/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" >

        var shoutoutFns = { alertMyHi: function (typ) { alert("hi" + typ); } }
        jQuery.extend(shoutoutFns, {
            fnadjustlen: function (obj) {

                var cntLen = $(obj).val().length;
                var wordCnt = 120 - cntLen;
                if (cntLen > 40) {
                    var ht = 40 * 2;
                    if ($(obj).height() < ht) { $(obj).height(ht); };
                }
                if (cntLen < 40) {
                    var ht = 40;
                    if ($(obj).height() > ht) { $(obj).height(ht); };
                }

                $('.myShoutsCnt').html(wordCnt);

                if (wordCnt < 0 || wordCnt > 119) { $('.myShoutSubmit').attr("disabled", "true"); } else
                { $('.myShoutSubmit').removeAttr("disabled"); }

            }

        });

        $(document).ready(function () {

            $('.myShoutsMsgInput').keyup(function (event) {
                shoutoutFns.fnadjustlen(this);
            });
        });

        function postRattles() {

            var list = "#shoutoutitemlist";
            rattleMsg = $('#myShoutsInp').val();
            $('#myShoutsInp').val("");
            if (rattleMsg.length > 0) {
                $(list).prepend('<li>' + rattleMsg + '</list>');
                $('.myShoutSubmit').attr("disabled", "true");
                $('.myShoutsCnt').html("120"); 
            }
            else {
                alert("Empty Rattle");
            }
            
        }
    </script> 
</head>
<body style="margin:0%"  >
    <form id="form1" runat="server" style="padding:0; margin:0px; "  >
    <div class="myShouts">
    <div class="myShoutText">Let the world know what's happening at your place:</div>
    <div class="myShoutsMsg">
        
        <textarea class="myShoutsMsgInput" id='myShoutsInp' rows="auto" cols="50"  ></textarea>
        
        <div class="myShoutsBtn" ><p><label class="myShoutsCnt">120</label>  
        <input type="button" class="myShoutSubmit" disabled="disabled"    value="Rattle"    style="width:50px; height:40px; padding:0px " onclick="postRattles()"    />  </p>
        </div> 
        
    </div>
    <div class="myShoutOuts" id="myShoutOuts" >
    <div class="shoutOutlist">
        <ul id="shoutoutitemlist"> 
        <li id="Li1"><div style="text-align:right"  ><i>Posted:12/2/2009 12:00:34</i> </div> <div>myshouts Let the world know how important it is for a person like you to get a very important life and make this world a better </div><a href="#" title="delete">Delete</a></li>
        <li id="Li2"><div style="text-align:right"  ><i>Posted:12ss/2/2009 12:00:34</i> </div> <div>myshouts Let the world know how important it is for a person like you to get a very important life and make this world a better</div> <a href="#" title="delete">Delete</a>  </li>
        <li id="Li3"><div style="text-align:right"  ><i>Posted:12/2/2009 12:00:34</i> </div> <div>myshouts Let the world know how important it is for a person like you to get a very important life and make this world a better</div><a href="#" title="delete">Delete</a>   </li>
        <li id="Li4"><div style="text-align:right"  ><i>Posted:12/2/2009 12:00:34</i> </div> <div>myshouts Let the world know how important it is for a person like you to get a very important life and make this world a better</div><a href="#" title="delete">Delete</a>   </li>
        </ul> 
    </div>
    </div>
    </div>
    </form>
    
</body>
</html>
