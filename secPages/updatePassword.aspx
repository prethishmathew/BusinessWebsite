<%@ Page Title="" Language="C#" MasterPageFile="~/master2.master" AutoEventWireup="true" CodeFile="updatePassword.aspx.cs" Inherits="secPages_updatePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<title >

myRattles.com - Where the world connects to its local life..
</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="passwrdUpdatePanel"  runat="server"   UpdateMode="Conditional"  >
<ContentTemplate >
<div class="secPagediv" >

<div style=" width: 90%;  " >
Password Update:<label id="labelMessage" runat="server" ></label>
<br />
<label id="Label3" class="bizLabel" >Current Password: </label>
<asp:TextBox  runat="server" ID="oldPassword"  
TextMode="Password"   CssClass="actTxt" 
  ></asp:TextBox>
 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Cannot be empty!" ControlToValidate="oldPassword" ></asp:RequiredFieldValidator>
<br />

<label id="Label1" class="bizLabel" >New Password: </label>

<asp:TextBox  runat="server" ID="password1"  
TextMode="Password"   CssClass="actTxt" 
onkeypress="javascript:return ErrChkRtn('verifyPasswordstrength','#ctl00_ContentPlaceHolder1_password1','#errPassword')" ></asp:TextBox>
<span id="errPassword"></span>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Cannot be empty!" ControlToValidate="password1" ></asp:RequiredFieldValidator>
<br />
<label id="Label2" class="bizLabel">Confirm New Password: </label>

<asp:TextBox  runat="server" ID="password2"  TextMode="Password"   CssClass="actTxt" 
 >
</asp:TextBox>

<asp:CompareValidator ID="CompareValidator1" 
runat="server" 
ErrorMessage="Passwords have to be similar" 
ControlToCompare="password1" 
ControlToValidate="password2" ></asp:CompareValidator>

<div style="text-align: center"  >
<asp:Button ID="updateBtn" runat="server" 
Text="Update"  ToolTip="Click to Save Changes"
 OnClick="updateBtn_click" 
CssClass="btnMasterSignUp"/>
</div>


</div>

<div style="float: right; width: 10%">

</div> 
<asp:UpdateProgress ID="UpdateProgress1" runat="server"
      >
      <ProgressTemplate >
       <div class="updatePanelProgress">
        <asp:Image ID="Image1" runat="server"  Width="100px"  Height="100px"  ImageUrl="~/Images/loading.gif" />
       </div>
      </ProgressTemplate>  
 </asp:UpdateProgress>
    
</div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

