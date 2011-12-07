<%@ Page Title="" Language="C#" MasterPageFile="~/master2.master" AutoEventWireup="true" CodeFile="forgotPassWord.aspx.cs" Inherits="forgotPassWord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>myRattles - Where the world connects to its local life</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset  class="forgotPassWord "  >
  
    <asp:PasswordRecovery ID="PasswordRecoveryAg" runat="server"  
        OnVerifyingUser="PasswordRecovery1_OnVerifyingUser" Height="184px" 
        Width="500px"   
         MailDefinition-From="userWizard@patRates.com"
          MailDefinition-Subject="Account Change Information"
            >
        <UserNameTemplate>
                                    <h1 style="color: #0000FF"> Forgot Your Password? </h1> myRattles will send the instructions on how to reset your password to your email id. 
                            
                                    <h3 >Enter your User Name or Email to receive your password.</h3>
                                    <p  >
                                    
                                    <asp:TextBox ID="UserName" runat="server" CssClass="textDefaultBorderLess" Width="250px"></asp:TextBox><asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" 
                                        ValidationGroup="PasswordRecoveryAg" CssClass="loginButton" />   
                                    </p>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                        ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                        ToolTip="User Name is required." ValidationGroup="PasswordRecoveryAg"  ></asp:RequiredFieldValidator>
                                    <br />    
                            
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            
        </UserNameTemplate>
     
    </asp:PasswordRecovery>
    </fieldset>
</asp:Content>

