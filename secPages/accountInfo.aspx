<%@ Page Language="C#" MasterPageFile="~/master2.master" AutoEventWireup="true" CodeFile="accountInfo.aspx.cs" Inherits="secPages_accountInfo" Title="myRattles - where the world connects to its local life" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <title > myRattles - where the world connects to its local life</title>

    <script src="../Scripts/accountInfo.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate >
    <asp:Label  runat="server"  ID="errorMsg" ></asp:Label>

<div style="width: 70%; float: right; " >
<br />
<label id="LabelUserName" class="bizLabel">User Name: </label>
<asp:TextBox  runat="server" ID="userNameTxt"  CssClass="actTxt" Enabled="false"    
onchange="javascript:return ErrChkRtn('validUserName','#ctl00_ContentPlaceHolder1_userNameTxt','#errUserName')"  > </asp:TextBox>
<span id="errUserName"></span> <br />

<label id="Label3" class="bizLabel">Email: </label>
<asp:TextBox  runat="server" ID="EmailTextBox"     CssClass="actTxt" OnTextChanged="emailText_changed"  > </asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
runat="server" 
ErrorMessage="Cannot be empty!" 
ControlToValidate="EmailTextBox" ></asp:RequiredFieldValidator>

<br />

<label id="Label6" class="bizLabel">Location: </label>
<asp:TextBox  runat="server" ID="LocationBox"     
CssClass="actTxt"  OnTextChanged="LocationText_changed" ></asp:TextBox>
  <br />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
      >
      <ProgressTemplate >
       <div class="updatePanelProgress">
        <asp:Image ID="Image1" runat="server"  Width="100px"  Height="100px"  ImageUrl="~/Images/loading.gif" />
       </div>
      </ProgressTemplate>  
    </asp:UpdateProgress>
    <div style="width: 50%; float: right">
    <asp:Button ID="Button2" runat="server" Text="Save"  ToolTip="Click to Save Changes" CssClass="btnMasterSignUp"/>
    </div>
</div>
<div style="width: 30%; float: left;" >
<br /><br />
<asp:LinkButton ID="LinkUpdatePasswrd" runat="server" PostBackUrl="~/secPages/updatePassword.aspx"  >Change Your Password</asp:LinkButton><br />
<asp:LinkButton ID="LinkUpGradeAccount" runat="server"   OnClientClick="return displayBizWarning();" OnClick="LinkUpGradeAccount_Click"  Enabled ="false"      >Upgrade to Business Account</asp:LinkButton><br />
<asp:LinkButton ID="LinkUpDisableAccount" runat="server" OnClientClick="return displayConsumerWarning();" OnClick="LinkUpGradeAccount_Click" Enabled="false"      >Disable Business Account</asp:LinkButton><br />
</div>

</ContentTemplate>
</asp:UpdatePanel>  
 </div>
<div id="businessAccountSignUp" class="dialog" >
    <div class="content "> <br />
    We have very few rules for business owners but we take them seriously and expect all <strong >myRattles</strong> users and bizOwners to do the same. <br />
<br />    
1.	You MUST be the legal owner of the business, or you MUST be in a contract with the business to create a <strong >myRattles</strong> account.
<br />
2.	You warrant that you will not post any messages that are obscene, vulgar, sexually-oriented, hateful, threatening, or violate any laws  of the appropriate jurisdiction of the United States of America.
<br />
3.	By creating or upgrading to a business you agree to our Terms & Privacy Policies. Our Terms and Conditions can be viewed by clicking over <a href="../TnC.htm">here.</a>
    <br /><br />
     <div style="text-align:center ;"><asp:Button ID="acceptButton" runat="server" Text="Accept >>" OnClick="acceptButton_Click"  /></div>     
     <br />
     <br />
    
    </div>
    <div class="opaque "></div>
    <div class="closer"><img alt="Close" src="../fancybox/fancy_close.png" /></div>
</div>

<div id="businessAccountDisable" class="dialog" >
    <div class="content "> <br />
    
    You have chosen to disable your Business account. On disabling your account your data will not
    be available for public view. Press the continue button to proceed. 
            
            
     <div style="text-align:center ;"><asp:Button ID="disAbleBtn" runat="server" Text="Disable Biz Account >>" OnClick="disAbleBtn_Click"  /></div>     
     <br />
     <br />
    
    </div>
    <div class="opaque "></div>
    <div class="closer"><img alt="Close" src="../fancybox/fancy_close.png" /></div>
</div>


    </asp:Content>

