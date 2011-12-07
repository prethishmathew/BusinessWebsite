<%@ Page Language="C#" MasterPageFile="~/master2.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Login" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title >myRattles - where the world connects to its local life </title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="left_Color" > 
<fieldset class="loginpage" > 
    <asp:Login ID="Login1" runat="server" CssClass="loginpage" 
        TextLayout="TextOnTop" UserNameLabelText="Enter your Email ID or User Name:" 
        TitleText="Members Login"  
        MembershipProvider="AspNetSqlMembershipProvider1" 
        DestinationPageUrl="~/Default.aspx" 
        CreateUserUrl="registerNew.aspx" 
        CreateUserText="Don't have an Id? Sign Up!" onauthenticate="Login1_Authenticate" 
        PasswordRecoveryText= "Forgot password?"
        PasswordRecoveryUrl= "~/forgotPassWord.aspx"   
        >
        <TextBoxStyle Width="300px" />
        <LoginButtonStyle CssClass="loginButton" /> 
        <LayoutTemplate>
            <table border="0" cellpadding="1" cellspacing="0" 
                style="border-collapse:collapse;">
                <tr>
                    <td>
                        <table border="0" cellpadding="0">
                            <tr>
                                <td align="center">
                                    Members Login</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Enter your Email ID or User Name:</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="UserName" runat="server" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                        ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                        ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                        ControlToValidate="Password" ErrorMessage="Password is required." 
                                        ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." TabIndex="40"  />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="color:Red;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="loginButton"
                                        Text="Log In" ValidationGroup="Login1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HyperLink ID="CreateUserLink" runat="server" 
                                        NavigateUrl="registerNew.aspx">Don&#39;t have an Id? Sign Up!</asp:HyperLink>
                                    <br />
                                    <asp:HyperLink ID="PasswordRecoveryLink" runat="server" 
                                        NavigateUrl="~/forgotPassWord.aspx">Forgot password?</asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:Login>
</fieldset>
</div>

</asp:Content>

