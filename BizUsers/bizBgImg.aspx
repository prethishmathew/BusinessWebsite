<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bizBgImg.aspx.cs" Inherits="BizUsers_bizBgImg" Debug="true"   %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/pradsStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ui.all.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/demos.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/eggPlantModified.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.8.14.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/CommonCallFunctions.js"></script>
    <script src="../Scripts/Default.js" type="text/javascript"></script>

    <style type="text/css" >
    
    body{
     background-image:none;
     height:200px;
     color:black; 
    }
    </style> 

    <script type="text/javascript" >

        function showStop() {

            var dialogCon = "#mySpan";
            $(dialogCon).html("");
            $('input[type=submit]', this).attr('disabled', 'disabled'); 
            pRcommonFns.appendHTMLtoDom(dialogCon, '<img alt="" src="../Images/ajaxPreLoader.gif" /> ..Update in Progress ..');

            }
    </script> 
</head>
<body class="iframeNoBorder">
    <form id="form1" runat="server" class="left_Color" style="color:Black; font-size:x-small ;  "  >
    <div>
    <fieldset  title="BackGround Image"  style="width:683px; position:relative; padding:10px; padding-right:0px;    "  >
                <legend >BackGround Image: </legend> <br />
                
                    <asp:Image ID="Img1Image" runat="server"  AlternateText="BackGround"  ImageUrl="~/profileImages/applications-system-4.ico" 
                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  Width="75px" Height="75px"  ImageAlign="Middle" Visible="true"   />
                    <asp:FileUpload ID="FileUploadImg1" runat="server"  CssClass="btn" 
                        Width="200px" /> &nbsp;&nbsp;
                        Repeat Image:
                        <asp:DropDownList ID="repeatImgDropList" runat="server">
                            <asp:ListItem Value="No" Selected="True"  >No</asp:ListItem> 
                            <asp:ListItem Value="Yes">Yes</asp:ListItem> 
                    </asp:DropDownList>&nbsp;&nbsp;
                    
                    <asp:Button  ID="BtnUploadImg1" runat="server" Text="Upload" CssClass="btn"   OnClick="UploadBtn_Click"   OnClientClick="showStop();"  />
                    <asp:Button  ID="BtnDelImg1" runat="server" Text="Delete" CssClass="btn"   OnClick="DelImgBtn_Click"  OnClientClick="showStop();"/> 
                    <span id="mySpan"></span> 
    </fieldset> 
    </div>
    </form>
</body>
</html>
