<%@ Page Language="C#" AutoEventWireup="true" CodeFile="webGraphics.aspx.cs" Inherits="BizUsers_webGraphics" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title >myRattles - where the world connects to its local life </title>
</head>
<body>
    <form id="form1" runat="server">
    
    
    
    <fieldset style="width: 700px; border-bottom-style:ridge ; ">
               <asp:Label ID="LabelbgColor" runat="server" Text="Background Color:" Width="160px" class="bizLabel"></asp:Label>
               <asp:TextBox ID="bgColorText" runat="server" Width="130px" CssClass="bizTxt"></asp:TextBox>
               <br />
               
               <asp:Label ID="LabelfgColor" runat="server" Text="Font Color:" Width="160px" class="bizLabel" ></asp:Label>
               <asp:TextBox ID="fgColorText" runat="server" Width="130px" CssClass="bizTxt"></asp:TextBox>
               <br />
               
               <asp:Label ID="LabelbgImage" runat="server" Text="Background Image:" Width="160px" class="bizLabel"></asp:Label>
               <asp:FileUpload ID="FileUploadbgImage"   Width="200px"  runat="server" />
               <br />
               
               <asp:Label ID="LabeTheme" runat="server" Text="Website Theme:" Width="160px" class="bizLabel"></asp:Label>
               <asp:DropDownList ID="webTheme" runat="server" CssClass="bizTxt" ></asp:DropDownList>
               <br />
               
               <asp:Button ID="btnGraphicsSave"  Text="Save" runat="server"
                OnClientClick="javascript:return saveWebGraphics();" />
                
                
               <br />
               <br />
               <asp:Label ID="LabelwebGraphicsRem" runat="server"  Width="500px" ></asp:Label>
                </fieldset>
    </form>
</body>
</html>
