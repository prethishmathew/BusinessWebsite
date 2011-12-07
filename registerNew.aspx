<%@ Page Title="" Language="C#" MasterPageFile="~/master2.master" AutoEventWireup="true" CodeFile="registerNew.aspx.cs" Inherits="registerNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title >Register</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div  class="stanDardDiv" >
<br />
<asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
              ContinueDestinationPageUrl="~/Default.aspx"
              OnCreateUserError="CreateUserWizard1_CreateUserError"
              OnSendMailError="CreateUserWizard1_sendEmailError"   
             OnCreatedUser="CreateUserWizard1_CreatedUser"
              CreateUserButtonText="Create  My Account"
               DuplicateEmailErrorMessage="Duplicate Email id. Use a different Email Id"
                DuplicateUserNameErrorMessage="User Id already take. Please choose a new id"  
                NavigationStyle-HorizontalAlign ="Center" 
                  HeaderStyle-HorizontalAlign="Left"
                  HeaderText = " Create a New Account "
                   HeaderStyle-Font-Bold="true"
                    HeaderStyle-Font-Underline="false" 
                     HeaderStyle-Font-Size="18px"      
                StepStyle-HorizontalAlign = "Center"
                 ErrorMessageStyle-ForeColor="Red"   

              >
            
            <ContinueButtonStyle CssClass="btn" />

<HeaderStyle HorizontalAlign="Center" Font-Bold="True" ForeColor="Blue"    ></HeaderStyle>

            <CreateUserButtonStyle CssClass=" loginButton "
             Width="200 px"   
             />

<NavigationStyle HorizontalAlign="Center"></NavigationStyle>

<StepStyle HorizontalAlign="Center"></StepStyle>
            <WizardSteps>
              
                <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                
                    <ContentTemplate>
                    <br />
                    <table>
                    <tr>
                    <td>       <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" class="bizLabelAct">User Name:</asp:Label>
                           <br />
                           <br />
                             </td>
                    <td><asp:TextBox ID="UserName" runat="server" CssClass="actTxt"  ></asp:TextBox> 
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                        ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                        ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                                    <br /><span class="smallComment "  > 
                                                               
                                    Your webiste:http://www.myRattles.com/yourusername
                                    </span>
                    
                             
                    </td>
                    
                    </tr>
                    <tr>
                    <td>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="bizLabelAct "  >Password:</asp:Label>

                    </td>
                    <td>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="actTxt "></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                        ControlToValidate="Password" ErrorMessage="Password is required." 
                                        ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>

                    </td>
                    
                    </tr>
                    <tr>
                    <td>
                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                        AssociatedControlID="ConfirmPassword" CssClass="bizLabelAct " >Confirm Password:</asp:Label>
                    
                    </td>
                    <td>
                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password" CssClass="actTxt "></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" 
                                        ControlToValidate="ConfirmPassword" 
                                        ErrorMessage="Confirm Password is required." 
                                        ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    
                    </td>
                    
                    </tr>
                    <tr>
                    <td>
                     <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="bizLabelAct " >E-mail:</asp:Label>                    
                    </td>
                    <td>
                                    <asp:TextBox ID="Email" runat="server" CssClass="actTxt "></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" 
                                        ControlToValidate="Email" ErrorMessage="E-mail is required." 
                                        ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                    
                    </td>
                    
                    </tr>
                    <tr>
                    <td>       <asp:Label ID="LocationLabel" runat="server" AssociatedControlID="location" CssClass = "bizLabelAct ">Location:</asp:Label>
                     </td>
                    <td>
                                   <asp:TextBox ID="location" runat="server" CssClass= "actTxt "></asp:TextBox>   
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                        ControlToValidate="location" ErrorMessage="Location is required." 
                                        ToolTip="Enter your location" ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                    <cc2:AutoCompleteExtender ID="location_AutoCompleteExtender" runat="server" 
                                        DelimiterCharacters="" 
                                        Enabled="True" 
                                        ServiceMethod="cityStateDrop" 
                                        MinimumPrefixLength="3"  
                                        ServicePath="~/WebService.asmx"   
                                        TargetControlID="location"
                                        EnableCaching="true"  
                                       
                                    >
                                    </cc2:AutoCompleteExtender>    
                                    
                     </td>
                    </tr>
                    <tr>
                    <td><asp:Label ID="Label1" runat="server" CssClass = "bizLabelAct ">Are you a business owner:</asp:Label>
                    
                    </td>
                    <td align="left"  >
                    <asp:CheckBox ID="isBizUserChkBox" runat="server" />
                    </td>
                    
                    </tr>
                    </table>
                    <br />
                    <br />
                                        <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                            ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                            Display="Dynamic" 
                                            ErrorMessage="The Password and Confirmation Password must match." 
                                            ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                        <div style="color: #FF0000" >
                                        
                                        <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False" ></asp:Literal>
                                        </div>
                                        <div align="left">
                                        By clicking the “Create My Account” button below, 
                                        I certify that I have read and agree to the <a href= TnC.htm > myRattles Terms and Conditions</a>  and to receive account related communications from myRattles electronically.
                                    </div>
                    </ContentTemplate>
                
                </asp:CreateUserWizardStep>
            <asp:TemplatedWizardStep StepType="Complete" runat="server" ID="CompleteStepMessage"  >
             <ContentTemplate  >
                <img src="Images/myRattlesblueV1.png" alt="myRattles.com" />
                Welcome to myRattles.com - where the world meets its local life... 
                You can know search for your local business and favorite places and get the latest news from them.
                
                 <br />
                 <asp:Label ID="LabelCompleteMessage" runat="server" Text="Label"></asp:Label>
                 <br />
                 User Name:
                 <asp:Label ID="LabelCompleteUserName" runat="server" Text="Label"></asp:Label>
                 <br />
                 Email ID&nbsp;&nbsp; :
                 <asp:Label ID="LabelCompleteEmail" runat="server" Text="Label"></asp:Label>
                 &nbsp;&nbsp;
                 <br />
                 <br />
                 <asp:Button ID="BtnregisterCompleteContinue" runat="server" CssClass="loginButton"  Text="Continue >>" OnClick="loadDefaultPage" />
                 <br />
                 <br />
                 <br />
             </ContentTemplate>
             
            </asp:TemplatedWizardStep> 
               
<asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server"></asp:CompleteWizardStep>
                
            </WizardSteps>
        </asp:CreateUserWizard>
        
</div>
</asp:Content>

