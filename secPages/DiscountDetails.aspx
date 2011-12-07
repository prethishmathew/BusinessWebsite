<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoScriptManager.master" AutoEventWireup="true" CodeFile="DiscountDetails.aspx.cs" Inherits="secPages_DiscountDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css" >

.discountPageMain
{
width:700px; 
padding:30px; 
background-color:White ; 
}

.discountPageMain .leftSide
{
    float:left; 
    text-align:right; 
    border-right-width:1px; 
    border-right-style:solid;  
    border-right-color:#789; 
    padding-right:3px;
}

.discountPageMain .mainSide
{
    width:65%;  float:left; padding:10px; background-color:White;    
}

.TimerWrapper
{
font-size:12px; text-align:left; color:Green   
}

.discountPercentSign
{   float:right; 
    width:100px; 
    height:50px; 
    text-align:center  ;
    background-repeat:no-repeat; 
    background-position:top   ;       
    background-image:url(../Images/bgDealtorq.png); 
    color:White;
    color:White; 
    font-weight:bolder; 
    font-size:17px;
 }

 .getDisBtn
 {
 float:right;      
 background-image:url(../Images/bgBtnBlue.png); 
 background-repeat:no-repeat ; 
 width:100px; 
 height:45px; 
 text-align:center ;
 color:White; 
 font-size:18px; 
 font-family:Arial ; 
 font-weight:bold; 
 padding-top:10px;
 text-decoration:none;
 }

 .getDisBtn:hover
 {
  cursor:pointer ; 
 }
</style>
<link href="../Styles/jquery.countdown.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="../Scripts/jquery.countdown.min.js" type="text/javascript"></script>
<script  type="text/javascript"  >
    $(function () {
        var yydt = $("#ctl00_ContentPlaceHolder1_stopYear").val();
        var mmdt = $("#ctl00_ContentPlaceHolder1_stopMonth").val();
        var dddt = $("#ctl00_ContentPlaceHolder1_stopDay").val();
        var hhdt = $("#ctl00_ContentPlaceHolder1_stopHrs").val();
        var mindt = $("#ctl00_ContentPlaceHolder1_stopMins").val();
        var austDay = new Date();
        //austDay = new Date(austDay.getFullYear() + 1, 1 - 1, 26, 12, 40, 0, 0);
        austDay = new Date(yydt, mmdt, dddt, hhdt, mindt, 0, 0);
        $('#countDown1').countdown({ until: austDay, format: 'HMS' });
        //$('#aSignin').click(function() {
        $('#showDtls').click(function () {
            $("#dtlsMore").slideToggle('');
            if ($('#showDtls').html() == 'Show More..') {
                $('#showDtls').html('Less..');
            }
            else {
                $('#showDtls').html('Show More..');
            }
            return false;
        });
        // $('#year').text(austDay.getFullYear());

        $(".getDisBtn").click(function () {



        });
    });


</script>
<asp:HiddenField runat="server" ID="stopYear" />
<asp:HiddenField runat="server" ID="stopMonth" />
<asp:HiddenField runat="server" ID="stopDay" />
<asp:HiddenField runat="server" ID="stopHrs" />
<asp:HiddenField runat="server" ID="stopMins" />

<fieldset  class="discountPageMain"   >
    <asp:PlaceHolder ID="discountPlaceHolder" runat="server" ></asp:PlaceHolder> 
    
    <div class='leftSide'   >
    <asp:Image runat="server" id="profImg" ImageUrl="~/profileImages/galleryPics/profilepic6.png" Width="200" Height="200"   /> 
    <div style='font-size:17px;font-weight:bold'  > 
                    <label  style='text-align:right ;  ' >Actual Value: $ </label> <asp:Label id="ActualValue" runat="server"  >00</asp:Label> <br />
                    <label style='text-align:right ;'>You Pay: $</label>  <asp:Label id="CpnValue" runat="server"  >00</asp:Label>
                    <br />
                    <div class='TimerWrapper'  >Time Left: 
                    <div id='countDown1' style=' width:200px; border-style:none;    background-color:White;font-size:14px;  ' >  </div>
                    </div>
                    <div style='clear:both'></div>  
    </div> 
    </div>
    <div class='mainSide'    >
                    <asp:Label ID="bizDiscountHeader" runat="server" Font-Size="17px" Font-Bold="True" ForeColor="Blue">This Deal Has Expired.</asp:Label> 
                    <div class='discountPercentSign'  >
                    <div  style='padding:10px;'   >
                    <asp:Label ID="percentOff" runat="server" >0</asp:Label>% Off
                    </div>
                    </div>
                    <br />
                    <b style='color:#e9ba26' >
                     <asp:Label ID="bizName" runat="server" ></asp:Label>  
                    </b> 
                    <br /><br />
                    <div  style='color:Black ' >
                    <asp:Label runat="server" ID="bizDiscountDesc"></asp:Label> 
                    </div><br />
                    <span>This deal is valid between <asp:Label id="bizDiscountStartdate" runat="server"  >######</asp:Label>   and  <asp:Label id="bizDiscountExpirydate" runat="server"  >#######</asp:Label> ( Both Dates inclusive ) </span><br /><br />
                    <asp:LinkButton ID="buyLink"  CssClass="getDisBtn" runat="server" Text="Buy!!"  OnClick="Ondisbtn_Click"  ></asp:LinkButton>     
                    <br />
                    <br /><br />
                    <a href='#' style='float:right; font-weight:bold; font-size:17px;  color:#e9ba26 '  > <asp:Label ID="discountSold" runat="server" ></asp:Label> </a><br />
                Fine Print:
                <div style='font-size:10px'>
                <asp:Label id="discoutexceptions" runat="server"  >Deal ended. </asp:Label>
                </div>
                </div>

    
</fieldset>

</asp:Content>
