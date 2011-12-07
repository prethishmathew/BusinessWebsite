<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bizPicsShow.aspx.cs" Inherits="BizUsers_bizPicsShow"  Debug="true"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title >myRattles - where the world connects to its local life </title>
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
     color:black; 
    }
    </style> 

    <script type="text/javascript" >

        function showStop(header) {

            var dialogCon = "#dyNumPopUpCon";
            $(dialogCon).html("");
            $(dialogCon).css('left-padding', '40px');
            $(dialogCon).append("<div id='chgGrpDyNumDiv' ></div>");
            dialogCon = "#chgGrpDyNumDiv";
            pRcommonFns.appendHTMLtoDom(dialogCon, '<img alt="" src="../Images/ajaxPreLoaderBlocks.gif" /> ..Update in Progress ..');
            //$(dialogCon).append("<h3>");
            dialogCon = ".opaque";
            $(dialogCon).html(header);
            createDialogBox(null,true);
        }
    </script> 
</head>
<body  class="iframeNoBorder"  >
    <form id="form1" runat="server" class="left_Color" style="color:Black; font-size:x-small ;  "  >
    <div class="myShopsTab "  style="width:600px"   >       
        <table >
        <tr>
        <td  >
        <asp:Image ID="ProfileImage" runat="server"  AlternateText="Profile Picture" ImageUrl="~/profileImages/applications-system-4.ico" 
                 Visible="true"   BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  Width="100px" Height="100px"       />
        </td>
        <td>
        Profile Image&nbsp;&nbsp; :
        <asp:FileUpload ID="FileUploadProfImg" runat="server"  CssClass="btn" 
            Width="234px" />
            <br />
            Image
            Caption&nbsp;&nbsp;: <asp:TextBox ID="txtCaptionProfImg" runat="server" width="225px"></asp:TextBox>
            <br />
        <asp:Button     ID="BtnUploadProfPic" runat="server" Text="Upload Image" CssClass="btn"   OnClick="UploadBtn_Click" OnClientClick="showStop('Uploading Picture');"  />
        <asp:Button     ID="BtnDelProfPic" runat="server" Text="Delete Image" CssClass="btn"   OnClick="DelImgBtn_Click" OnClientClick="showStop('Deleting Picture');"  />
            
        </td>
        </tr>
        </table>
        <br />
        Picture gallery - Upload Upto 5 images of your enterprise for the picture Gallery
        <br /><br />
        <table    >
        <tr>
        <th >
        
        </th>
        <th>
        </th>
        </tr>
        <tr>
        <td>
        <asp:Image ID="Img1Image" runat="server"  AlternateText="Gallery Picture"  ImageUrl="~/profileImages/applications-system-4.ico" 
                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  Width="75px" Height="75px"  ImageAlign="Middle" Visible="true"   />
        </td>
        <td>
            <asp:FileUpload ID="FileUploadImg1" runat="server"  CssClass="btn" 
            Width="200px" />
            <asp:Button  ID="BtnUploadImg1" runat="server" Text="Upload" CssClass="btn"   OnClick="UploadBtn_Click"  OnClientClick="showStop('Uploading Picture');"  />
            <asp:Button  ID="BtnDelImg1" runat="server" Text="Delete" CssClass="btn"   OnClick="DelImgBtn_Click" OnClientClick="showStop('Deleting Picture');" /> 
        </td>
        
        
    </tr>
        <tr>
        <td>
        <asp:Image ID="Img2Image" runat="server"  AlternateText="Gallery Picture" ImageUrl="~/profileImages/applications-system-4.ico" 
                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  Width="75px" Height="75px"  ImageAlign="Middle" Visible="true"   />
        </td>
        <td>
            <asp:FileUpload ID="FileUploadImg2" runat="server"  CssClass="btn" 
            Width="200px" />
            <asp:Button  ID="BtnUploadImg2" runat="server" Text="Upload" CssClass="btn"   OnClick="UploadBtn_Click" OnClientClick="showStop('Uploading Picture');" />
            <asp:Button  ID="BtnDelImg2" runat="server" Text="Delete" CssClass="btn"   OnClick="DelImgBtn_Click" OnClientClick="showStop('Deleting Picture');" />
        </td>
    </tr>
        <tr>
        <td>
        <asp:Image ID="Img3Image" runat="server"  AlternateText="Gallery Picture" ImageUrl="~/profileImages/applications-system-4.ico"  
                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  Width="75px" Height="75px"  ImageAlign="Middle" Visible="true"   />
        </td>
        <td>
            <asp:FileUpload ID="FileUploadImg3" runat="server"  CssClass="btn" 
            Width="200px" />
            <asp:Button     ID="BtnUploadImg3" runat="server" Text="Upload" CssClass="btn"   OnClick="UploadBtn_Click" OnClientClick="showStop('Uploading Picture');"/>
            <asp:Button  ID="BtnDelImg3" runat="server" Text="Delete" CssClass="btn"   OnClick="DelImgBtn_Click" OnClientClick="showStop('Deleting Picture');"/>
        </td>
</tr>
        <tr>
        <td>
        <asp:Image ID="Img4Image" runat="server"  AlternateText="Gallery Picture"  ImageUrl="~/profileImages/applications-system-4.ico" 
                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  Width="75px" Height="75px"  ImageAlign="Middle" Visible="true"   />
        </td>
        <td>
            <asp:FileUpload ID="FileUploadImg4" runat="server"  CssClass="btn" 
            Width="200px" />
            <asp:Button     ID="BtnUploadImg4" runat="server" Text="Upload" CssClass="btn"   OnClick="UploadBtn_Click" OnClientClick="showStop('Uploading Picture');" />
            <asp:Button  ID="BtnDelImg4" runat="server" Text="Delete" CssClass="btn"   OnClick="DelImgBtn_Click" OnClientClick="showStop('deleting Picture');"/>
        </td>
</tr>
        <tr>
        <td>
        <asp:Image ID="Img5Image" runat="server"  AlternateText="Gallery Picture"  ImageUrl="~/profileImages/applications-system-4.ico" 
                BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  Width="75px" Height="75px"  ImageAlign="Middle" Visible="true"   />
        </td>
        <td>
            <asp:FileUpload ID="FileUploadImg5" runat="server"  CssClass="btn" 
            Width="200px" />
            <asp:Button     ID="BtnUploadImg5" runat="server" Text="Upload" CssClass="btn"   OnClick="UploadBtn_Click" OnClientClick="showStop('Uploading Picture');"/>
            <asp:Button  ID="BtnDelImg5" runat="server" Text="Delete" CssClass="btn"   OnClick="DelImgBtn_Click" OnClientClick="showStop('deleting Picture');"/>
        </td>
</tr>    
        </table>
        
    </div>

    <div id="dyNumPopUp" class= "dialog "    >
    <div id="dyNumPopUpCon" class="content "> <br />
    
            
            
     
     <br />
     <br />
    
    </div>
    <div class="opaque " ></div>
    
    </div>

    </form>
    
</body>
</html>
