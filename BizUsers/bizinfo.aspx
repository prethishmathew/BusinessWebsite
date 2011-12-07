<%@ Page Language="C#" MasterPageFile="~/master2.master" AutoEventWireup="true" CodeFile="bizinfo.aspx.cs" Inherits="BizUsers_bizinfo" Title="Untitled Page"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/master2.master"   %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title >myRattles - where the world connects to its local life </title>
    <style type="text/css">
        #MenuPicsComment
        {
            width: 281px;
        }
    </style>
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <!-- Start Of Menu Profile -->
        
   <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtenderProfile" runat="server"
        TargetControlID="PnlContentProfile" ExpandControlID="PnlTitleProfile" 
        CollapseControlID="PnlTitleProfile" TextLabelID="PnlProfileLabel2" 
        CollapsedText="Show Details (...)" ExpandedText="Hide Details (...)" 
        ImageControlID="PnlProfileImage" ExpandedImage= "~/Images/button_minus_blue.png"
        CollapsedImage="~/Images/button_plus_blue.png"
        Collapsed="True" SuppressPostBack="true"         
        >
         
    </cc1:CollapsiblePanelExtender>  
   
   <asp:Panel ID="PnlTitleProfile" runat="server" CssClass="collapsePanelHeader">
        <asp:Image ID = "PnlProfileImage" runat="server" ImageUrl= "~/Images/button_plus_blue.png" />  
        <asp:Label ID="PnlProfileLabel1" runat="server" Text="Profile Information.... " 
        Height="26px" 
        CssClass="labelxr"/>
        <asp:Label ID="PnlProfileLabel2" runat="server" Text="Show Details(...)" Height="23px"
        CssClass="labelxr"
         />
   </asp:Panel> 
     
   <asp:Panel ID="PnlContentProfile" runat="server" CssClass="collapsePanel" >
   
   <div id="tabsProfile" class="bizInv "           >
    <ul >
            
               <li><a href="#profileDtls"><span>Profile Details</span></a></li>
               <li><a href="#profileContact"><span>Contact Info</span></a></li>
               <li><a href="#profilehrs"><span>Business Hours</span></a></li>

            </ul>


<div id="profileDtls"  style="font-weight:normal  "   >
        
        <span class="bizLabel" >First Name:</span> 
        <asp:TextBox ID="fNameTxt" runat="server" CssClass="bizTxt" ></asp:TextBox><br />
        <asp:Label ID="lNameLabel"  CssClass="bizLabel"   runat="server" Text="Last Name:"></asp:Label>
        <asp:TextBox ID="lNameTxt" runat="server" CssClass="bizTxt"></asp:TextBox><br />
        <asp:Label ID="bizNameLabel" runat="server"  CssClass="bizLabel" Text="Business Name:"></asp:Label>
        <asp:TextBox ID="bizNameTxt" runat="server" CssClass="bizTxt"></asp:TextBox><br />
        <asp:Label ID="bizCatgryLabel" runat="server" CssClass="bizLabel" Text="Business Category:"></asp:Label>
        <asp:TextBox ID="bizCatgryTxt" runat="server" CssClass="bizTxt"></asp:TextBox><label class="smallComment italics"   >"Restaurant","Real Estate","Child Care"</label><br />
        <asp:Label ID="bizSubCatgryLabel" runat="server" CssClass="bizLabel" Text="Business Sub Category:"></asp:Label>
        <asp:TextBox ID="bizSubCatgryTxt" runat="server" CssClass="bizTxt"></asp:TextBox><label class="smallComment italics"   >"Sushi","Commercial Properties"</label><br />
        <asp:Label ID="capLabel" runat="server" CssClass="bizLabel" Text="Business Caption:"></asp:Label>
        <asp:TextBox ID="capTxt" runat="server"  CssClass="bizTxt"></asp:TextBox>
        <br />
        <asp:Label ID="extWebSiteLabel" runat="server" CssClass="bizLabel" Text="Your Biz Website"></asp:Label>
        <asp:TextBox ID="extWebSiteTxt" runat="server"  CssClass="bizTxt"></asp:TextBox><label class="smallComment italics"   > (http://www.myplace.com/)</label> 
        <br /><br />
        
        
        <asp:Label ID="Label1" runat="server" CssClass="bizLabel"  Width="210px" 
            Text="A Description about the Business:"  style="text-align:left " ></asp:Label>
        <label class="smallComment italics"   >
        &nbsp;&nbsp;&nbsp;&nbsp; The description would appear under the Home Tab on your web page</label><br />
        <asp:TextBox ID="bizDesTxt" runat="server"  CssClass="bizTxt" Height="110px"  Width="650px"
            MaxLength="1000" TextMode="MultiLine"></asp:TextBox>
        <br />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <div  style="text-align:right; width:600px "  >
        <asp:Button ID="ButtonSaveProfile" runat="server" CssClass="twtBtn " 
            Text="Save Changes"   
            OnClientClick="javascript:return updateProfileDetails();"  />
        &nbsp;
        </div>
        </div> 

<div id="profileContact"  style="font-weight:normal  ">
        <asp:Label ID="add1Label" runat="server" CssClass="bizLabel" Text="Address Line 1:"></asp:Label>
        <asp:TextBox ID="add1Txt" runat="server" CssClass="bizTxt"></asp:TextBox><br />
        <asp:Label ID="add2Label" runat="server" CssClass="bizLabel" Text="Address Line 2:"></asp:Label>
        <asp:TextBox ID="add2Txt" runat="server" CssClass="bizTxt"></asp:TextBox><br />
        <asp:Label ID="zipLabel" runat="server" CssClass="bizLabel" Text="Zip Code:"></asp:Label>
        <asp:TextBox ID="zipTxt" runat="server"  CssClass="bizTxt"></asp:TextBox><br />
            <asp:Label ID="ContactLabel" runat="server" CssClass="bizLabel" Text="How can your Customers reach you?"></asp:Label>
        
        <br />
        <asp:Label ID="phoneLabel" runat="server" CssClass="bizLabel" Text="Phone Number:"></asp:Label>
        <asp:TextBox ID="phoneTxt" runat="server"  CssClass="bizTxt"></asp:TextBox><br />
        <asp:Label ID="emailLabel" runat="server" CssClass="bizLabel" Text="Email:"></asp:Label>
    <asp:TextBox ID="emailTxt" runat="server"  CssClass="bizTxt"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonSaveContacts" runat="server" CssClass="twtBtn " 
            Text="Save Changes"   OnClientClick="javascript:return updateProfileContactDetails();"  />
        &nbsp;</div> 

    
<div id="profilehrs" style="font-weight:normal  " >
    
    <asp:Label ID="Label2" runat="server" CssClass="bizLabel" Text="Working Hours:"></asp:Label>
    <asp:Label ID="Label3" runat="server" CssClass="bizLabel" Text="Regular hours:"></asp:Label>
    <asp:Label ID="Label12" runat="server" CssClass="bizLabel" Text="Open Time:" Width="75px" ></asp:Label>
     
        <asp:TextBox ID="regOpenTime" runat="server"></asp:TextBox>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtenderregOpenTime" runat="server"
         TargetControlID= "regOpenTime"
         Mask="99:99" 
         MaskType="Time"
         AcceptAMPM="true"
         ClearTextOnInvalid="true"
         ErrorTooltipEnabled="true" 
         UserTimeFormat="None"      
         MessageValidatorTip="true"
         AutoComplete="true"     
        
        >
 
        </cc1:MaskedEditExtender>
        
        <cc1:MaskedEditValidator ID="MaskedEditValidatorregOpenTime" runat="server"
         ControlExtender="MaskedEditExtenderregOpenTime" 
         ControlToValidate="regOpenTime"  
         IsValidEmpty="true"
         InvalidValueMessage="The time entered is invalid."  
         CssClass="btn" 
        >
        
        </cc1:MaskedEditValidator>        
        
        <br />
    <asp:Label ID="Label4" runat="server" CssClass="bizLabel" Text=" " ></asp:Label>
    <asp:Label ID="Label5" runat="server" CssClass="bizLabel" Text=" " ></asp:Label>
    <asp:Label ID="Label13" runat="server" CssClass="bizLabel" Text="Close Time:" Width="75px" ></asp:Label>
    
        <asp:TextBox ID="regCloseTime" runat="server"></asp:TextBox>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtenderregCloseTime" runat="server"
         TargetControlID= "regCloseTime"
         Mask="99:99" 
         MaskType="Time"
         AcceptAMPM="true"
         ClearTextOnInvalid="true"
         ErrorTooltipEnabled="true" 
         UserTimeFormat="None"      
         MessageValidatorTip="true"
         AutoComplete="true"     
        
        >
 
        </cc1:MaskedEditExtender>
        
        <cc1:MaskedEditValidator ID="MaskedEditValidatorregCloseTime" runat="server"
         ControlExtender="MaskedEditExtenderregCloseTime" 
         ControlToValidate="regCloseTime"  
         IsValidEmpty="true"
         InvalidValueMessage="The time entered is invalid."  
         CssClass="btn" 
        >
        
        </cc1:MaskedEditValidator>        
    
    
    
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" CssClass="bizLabel" Text=" " ></asp:Label>
        <asp:Label ID="Label7" runat="server" CssClass="bizLabel" Text="Lunch Time: " ></asp:Label>
        <asp:Label ID="Label8" runat="server" CssClass="bizLabel" Text="From: " 
            Width="75px" ></asp:Label>
        <asp:TextBox ID="lunFromTime" runat="server"></asp:TextBox>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtenderlunFromTime" runat="server"
         TargetControlID= "lunFromTime"
         Mask="99:99" 
         MaskType="Time"
         AcceptAMPM="true"
         ClearTextOnInvalid="true"
         ErrorTooltipEnabled="true" 
         UserTimeFormat="None"      
         MessageValidatorTip="true"
         AutoComplete="true"     
        
        >
 
        </cc1:MaskedEditExtender>
        
        <cc1:MaskedEditValidator ID="MaskedEditValidatorlunFromTime" runat="server"
         ControlExtender="MaskedEditExtenderlunFromTime" 
         ControlToValidate="lunFromTime"  
         IsValidEmpty="true"
         InvalidValueMessage="The time entered is invalid."  
         CssClass="btn" 
        >
        
        </cc1:MaskedEditValidator>        
    <br />
        <asp:Label ID="Label10" runat="server" CssClass="bizLabel" Text=" " ></asp:Label>
        <asp:Label ID="Label11" runat="server" CssClass="bizLabel" Text=" " ></asp:Label>
        <asp:Label ID="Label9" runat="server" CssClass="bizLabel" Text="To:" 
            Width="75px" ></asp:Label> 
        <asp:TextBox ID="lunToTime" runat="server"></asp:TextBox>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtenderlunToTime" runat="server"
         TargetControlID= "lunToTime"
         Mask="99:99" 
         MaskType="Time"
         AcceptAMPM="true"
         ClearTextOnInvalid="true"
         ErrorTooltipEnabled="true" 
         UserTimeFormat="None"      
         MessageValidatorTip="true"
         AutoComplete="true"     
        
        >
 
        </cc1:MaskedEditExtender>
        
        <cc1:MaskedEditValidator ID="MaskedEditValidatorlunToTime" runat="server"
         ControlExtender="MaskedEditExtenderlunToTime" 
         ControlToValidate="lunToTime"  
         IsValidEmpty="true"
         InvalidValueMessage="The time entered is invalid."  
         CssClass="btn" 
        >
        
        </cc1:MaskedEditValidator>        
    <br />
        <br />
        
        <asp:Label ID="Label15" runat="server" CssClass="bizLabel" Text="Special Timings:" ></asp:Label>
        <asp:Label ID="Label21" runat="server" CssClass="bizLabel" Text="Day: " width="35px"></asp:Label>
        <asp:DropDownList ID="DropDownListSplDay1" runat="server">
        <asp:ListItem Value=" " Selected="True" ></asp:ListItem>
        <asp:ListItem Value="Mon"></asp:ListItem> 
        <asp:ListItem Value="Tue"></asp:ListItem> 
        <asp:ListItem Value="Wed"></asp:ListItem> 
        <asp:ListItem Value="Thu"></asp:ListItem> 
        <asp:ListItem Value="Fri"></asp:ListItem> 
        <asp:ListItem Value="Sat"></asp:ListItem> 
        <asp:ListItem Value="Sun"></asp:ListItem> 
        
        </asp:DropDownList>
        
        <asp:Label ID="Label18" runat="server" CssClass="bizLabel" Text="From: " width="35px"></asp:Label>
        <asp:TextBox ID="splTime1From" runat="server"></asp:TextBox>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtendersplTime1From" runat="server"
         TargetControlID= "splTime1From"
         Mask="99:99" 
         MaskType="Time"
         AcceptAMPM="true"
         ClearTextOnInvalid="true"
         ErrorTooltipEnabled="true" 
         UserTimeFormat="None"      
         MessageValidatorTip="true"
         AutoComplete="true"     
        
        >
 
        </cc1:MaskedEditExtender>
        
        <cc1:MaskedEditValidator ID="MaskedEditValidatorsplTime1From" runat="server"
         ControlExtender="MaskedEditExtendersplTime1From" 
         ControlToValidate="splTime1From"  
         IsValidEmpty="true"
         InvalidValueMessage="The time entered is invalid."  
         CssClass="btn" 
        >
        
        </cc1:MaskedEditValidator>        
        <asp:Label ID="Label17" runat="server" CssClass="bizLabel" Text="To: " width="25px"></asp:Label>
        <asp:TextBox ID="splTime1To" runat="server"></asp:TextBox>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtendersplTime1To" runat="server"
         TargetControlID= "splTime1To"
         Mask="99:99" 
         MaskType="Time"
         AcceptAMPM="true"
         ClearTextOnInvalid="true"
         ErrorTooltipEnabled="true" 
         UserTimeFormat="None"      
         MessageValidatorTip="true"
         AutoComplete="true"     
        
        >
 
        </cc1:MaskedEditExtender>
        
        <cc1:MaskedEditValidator ID="MaskedEditValidatorsplTime1To" runat="server"
         ControlExtender="MaskedEditExtendersplTime1To" 
         ControlToValidate="splTime1To"  
         IsValidEmpty="true"
         InvalidValueMessage="The time entered is invalid."  
         CssClass="btn" 
        >
        
        </cc1:MaskedEditValidator>        
    <br />
 
   <asp:Label ID="Label14" runat="server" CssClass="bizLabel" Text=" " ></asp:Label>
   <asp:Label ID="Label20" runat="server" CssClass="bizLabel" Text="Day: " width="35px"></asp:Label>
        
        <asp:DropDownList ID="DropDownListSplDay2" runat="server">
        <asp:ListItem Value=" " Selected="True" ></asp:ListItem>
        <asp:ListItem Value="Mon"></asp:ListItem> 
        <asp:ListItem Value="Tue"></asp:ListItem> 
        <asp:ListItem Value="Wed"></asp:ListItem> 
        <asp:ListItem Value="Thu"></asp:ListItem> 
        <asp:ListItem Value="Fri"></asp:ListItem> 
        <asp:ListItem Value="Sat"></asp:ListItem> 
        <asp:ListItem Value="Sun"></asp:ListItem> 
        
        </asp:DropDownList>
        
        <asp:Label ID="Label16" runat="server" CssClass="bizLabel" Text="From:  " width="35px"></asp:Label>
        <asp:TextBox ID="splTime2From" runat="server"></asp:TextBox>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtendersplTime2From" runat="server"
         TargetControlID= "splTime2From"
         Mask="99:99" 
         MaskType="Time"
         AcceptAMPM="true"
         ClearTextOnInvalid="true"
         ErrorTooltipEnabled="true" 
         UserTimeFormat="None"      
         MessageValidatorTip="true"
         AutoComplete="true"     
        
        >
 
        </cc1:MaskedEditExtender>
        
        <cc1:MaskedEditValidator ID="MaskedEditValidatorsplTime2From" runat="server"
         ControlExtender="MaskedEditExtendersplTime2From" 
         ControlToValidate="splTime2From"  
         IsValidEmpty="true"
         InvalidValueMessage="The time entered is invalid."  
         CssClass="btn" 
        >
        
        </cc1:MaskedEditValidator>        
<asp:Label ID="Label19" runat="server" CssClass="bizLabel" Text="To: " width="25px"></asp:Label>
        <asp:TextBox ID="splTime2To" runat="server"></asp:TextBox>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtendersplTime2To" runat="server"
         TargetControlID= "splTime2To"
         Mask="99:99" 
         MaskType="Time"
         AcceptAMPM="true"
         ClearTextOnInvalid="true"
         ErrorTooltipEnabled="true" 
         UserTimeFormat="None"      
         MessageValidatorTip="true"
         AutoComplete="true"     
        
        >
 
        </cc1:MaskedEditExtender>
        
        <cc1:MaskedEditValidator ID="MaskedEditValidatorsplTime2To" runat="server"
         ControlExtender="MaskedEditExtendersplTime2To" 
         ControlToValidate="splTime2To"  
         IsValidEmpty="true"
         InvalidValueMessage="The time entered is invalid."  
         CssClass="btn" 
        >
        
        </cc1:MaskedEditValidator>        <br />
        <br />
        
        <asp:Label ID="Label22" runat="server" CssClass="bizLabel" Text="Closed Days:" ></asp:Label>
        <asp:DropDownList ID="DropDownListClosedDays1" runat="server">
        <asp:ListItem Value=" " Selected="True" ></asp:ListItem>
        <asp:ListItem Value="Mon"></asp:ListItem> 
        <asp:ListItem Value="Tue"></asp:ListItem> 
        <asp:ListItem Value="Wed"></asp:ListItem> 
        <asp:ListItem Value="Thu"></asp:ListItem> 
        <asp:ListItem Value="Fri"></asp:ListItem> 
        <asp:ListItem Value="Sat"></asp:ListItem> 
        <asp:ListItem Value="Sun"></asp:ListItem> 
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownListClosedDays2" runat="server">
        <asp:ListItem Value=" " Selected="True" ></asp:ListItem>
        <asp:ListItem Value="Mon"></asp:ListItem> 
        <asp:ListItem Value="Tue"></asp:ListItem> 
        <asp:ListItem Value="Wed"></asp:ListItem> 
        <asp:ListItem Value="Thu"></asp:ListItem> 
        <asp:ListItem Value="Fri"></asp:ListItem> 
        <asp:ListItem Value="Sat"></asp:ListItem> 
        <asp:ListItem Value="Sun"></asp:ListItem> 
        
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownListClosedDays3" runat="server">
        <asp:ListItem Value=" " Selected="True" ></asp:ListItem>
        <asp:ListItem Value="Mon"></asp:ListItem> 
        <asp:ListItem Value="Tue"></asp:ListItem> 
        <asp:ListItem Value="Wed"></asp:ListItem> 
        <asp:ListItem Value="Thu"></asp:ListItem> 
        <asp:ListItem Value="Fri"></asp:ListItem> 
        <asp:ListItem Value="Sat"></asp:ListItem> 
        <asp:ListItem Value="Sun"></asp:ListItem> 
        
        </asp:DropDownList>
        <asp:DropDownList ID="DropDownListClosedDays4" runat="server">
        <asp:ListItem Value=" " Selected="True" ></asp:ListItem>
        <asp:ListItem Value="Mon"></asp:ListItem> 
        <asp:ListItem Value="Tue"></asp:ListItem> 
        <asp:ListItem Value="Wed"></asp:ListItem> 
        <asp:ListItem Value="Thu"></asp:ListItem> 
        <asp:ListItem Value="Fri"></asp:ListItem> 
        <asp:ListItem Value="Sat"></asp:ListItem> 
        <asp:ListItem Value="Sun"></asp:ListItem> 
        
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonUpdatebizHrs" runat="server" CssClass="twtBtn " 
            Text="Save Changes"   OnClientClick="javascript:return updateProfileBizHrsDetails();"  />
    <br />
   </div> 
   </div> 
    
   </asp:Panel> 
            <!-- End Of Menu Profile -->            
         
            <!-- Start Of Menu Pics -->
   <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server"
        TargetControlID="PnlContentMenuPics" ExpandControlID="PnlTitleMenuPics" 
        CollapseControlID="PnlTitleMenuPics" TextLabelID="PnlPicsLabel2" 
        CollapsedText="Show Details (...)" ExpandedText="Hide Details (...)" 
        ImageControlID="PnlPicsImage" ExpandedImage= "~/Images/button_minus_blue.png"
        CollapsedImage="~/Images/button_plus_blue.png"
        Collapsed="True" SuppressPostBack="true"          
        >
    </cc1:CollapsiblePanelExtender>  
   
   <asp:Panel ID="PnlTitleMenuPics" runat="server" CssClass="collapsePanelHeader">
        <asp:Image ID = "PnlPicsImage" runat="server" ImageUrl= "~/Images/button_plus_blue.png" />  
        <asp:Label ID="PnlPicsLabel1" runat="server" Text="Pictures Gallery & Graphics.."  
        Height="26px"                                      
        CssClass="labelxr"/>
        <asp:Label ID="PnlPicsLabel2" runat="server" Text="Show Details(...)" Height="23px"
        CssClass="labelxr"
         />
   </asp:Panel> 
     
   <asp:Panel ID="PnlContentMenuPics" runat="server" CssClass="collapsePanel">
        
        
  
           <div id="tabsPics"     >
            <ul>
            
                <li><a href="#uploadPics"><span>Picture Gallery</span></a></li>
                <li><a href="#webGraphics"><span>Webpage Graphics</span></a></li>

            </ul>
           
           <div id="uploadPics" class="smallFont " > 
           
                   <iframe id="uploadpicsFrame" runat="server" src="bizPicsShow.aspx"   
                   frameborder=0; 
                   scrolling="no"
                   style="background-position: center center;
                        border-width: 0px; 
                        background-image: url('../Images/ajaxPreLoader.gif'); 
                        width: 700px; 
                        height: 650px; 
                        background-repeat: no-repeat; 
                        text-align: left;">
                   </iframe> 
           
           </div>
               
           <div id="webGraphics" class="left_Color smallFont ">
           
           <fieldset   style="width: 700px;" >
               <asp:Label ID="LabelbgColor" runat="server" Text="Background Color:" Width="160px" class="bizLabel"></asp:Label>
               <asp:TextBox ID="bgColorText" runat="server" Width="130px" CssClass="bizTxt"></asp:TextBox>
               <br />
               
               <asp:Label ID="LabelfgColor" runat="server" Text="Font Color:" Width="160px" class="bizLabel" ></asp:Label>
               <asp:TextBox ID="fgColorText" runat="server" Width="130px" CssClass="bizTxt"></asp:TextBox>
               <br />
               
               <asp:Label ID="LabeTheme" runat="server" Text="Website Theme:" Width="160px" class="bizLabel"></asp:Label>
               <asp:DropDownList ID="webTheme" runat="server" CssClass="bizTxt" ></asp:DropDownList>
               <br />
               
               <asp:Button ID="btnGraphicsSave"  Text="Save" runat="server"
                OnClientClick="javascript:return saveWebGraphics();" />
                
                
               <asp:Label ID="LabelwebGraphicsRem" runat="server"  Width="500px"    Font-Size="XX-Small"></asp:Label>
                </fieldset>
                <br />
                <iframe id="Iframe1" runat="server" src="bizBgImg.aspx"   
                   frameborder=0; 
                   scrolling="no"
                   style="background-position: center center;
                        border-width: 0px; 
                        background-image: url('../Images/ajaxPreLoader.gif'); 
                        width: 700px; 
                        height: 400px; 
                        background-repeat: no-repeat; 
                        text-align: left;">
                   </iframe> 
           
                
           </div>
           
           </div>
          
     <br />
    </asp:Panel> 
    
    <!-- End Of Menu Pics -->
    
            <!-- Start Of Menu Inventory -->
   <cc1:CollapsiblePanelExtender 
        ID="CollapsiblePanelExtenderMenuInv"
	    runat="server"	    
        TargetControlID="PnlContentMenuInv" ExpandControlID="PnlTitleMenuInv" 
        CollapseControlID="PnlTitleMenuInv" TextLabelID="PnlInvLabel2" 
        CollapsedText="Show Details (...)" ExpandedText="Hide Details (...)" 
        ImageControlID="PnlInvImage" ExpandedImage= "~/Images/button_minus_blue.png"
        CollapsedImage="~/Images/button_plus_blue.png"
        Collapsed="True" 
        SuppressPostBack="True"
      >
       
    </cc1:CollapsiblePanelExtender>  
   
   <asp:Panel ID="PnlTitleMenuInv" runat="server"  CssClass="collapsePanelHeader">
        <asp:Image ID = "PnlInvImage" runat="server" ImageUrl= "~/Images/button_plus_blue.png" />  
        <asp:Label ID="PnlInvLabel1" runat="server" Text="Update Inventory Details.... " 
        Height="26px"                                      
        CssClass="labelxr"/>
        <asp:Label ID="PnlInvLabel2" runat="server" Text="Show Details(...)" Height="23px"
        CssClass="labelxr"
         />
   </asp:Panel> 
     
   <asp:Panel ID="PnlContentMenuInv" runat="server" CssClass="collapsePanel"> 
    
   
    
    <div id="bizInventory" class="bizInv"> 
    <br />
    <fieldset  style="width: 650px; font-weight:normal "   >
    <br />
     <asp:Label ID="InvHeadingLabel" runat="server"  Text="A Name for your Inventory:" 
            Width="170px" style="text-align: right"   ></asp:Label>
     <asp:TextBox ID="invHeaderTxtBox" runat="server"  CssClass="bizTxt" 
            ></asp:TextBox><label class="smallComment italics"   > "Menu","Our Products"</label>   <br />
        <br />
        
    <asp:Label ID="invDocLabel" runat="server" 
            Text="Upload your inventory file:" Width="170px" style="text-align: right"   ></asp:Label>
    <asp:FileUpload ID="InvDocUpload" runat="server" />
    <label class="smallComment italics"   > Only ".doc",".docx" or ".pdf" documents </label> <br /><br />
    
    
    <asp:Label CssClass=" bizLabel " runat="server" ID="inventoryFile"></asp:Label> 
    <asp:Button ID="BtnDeleteInvDoc" Text="Delete Document" runat="server" Visible="false"  CssClass="btn" OnClientClick="javascript:return deleteInvFile();"    />
    
    
    <div style="float: none; text-align: center; vertical-align: middle;">
       <asp:Button ID="invHeadingSave" runat="server" CssClass="btn" 
            Text="Save Changes" 
            onclick="invHeadingSave_Click" />   
    </div>
    
    </fieldset>  
    <br /><br />
    <span class="mediumFont" style="font-weight:normal">  Create your own Inventory List using the <b>"Rattles Menu Creator"</b> wizard !!!
    </span>
    
<span  style="text-align:center "  >
        <asp:Button ID="btnCreateNewList" runat="server" CssClass=" twtBtn " 
            onclientclick="return createNewList();" Text="Create A New Inventory Section" />
        
    <span id="ListSavedRemarks" class="smallFont " >
    
    </span>
</span>
<br /><br />
    <div id="tabs"   
            style="padding: 0px; margin: 0px; overflow: auto ; width: 690px;  max-height:500px"   > 
       <ul id="tabsPanel">
            <asp:PlaceHolder runat="server" ID="placeHolderInvContainer"></asp:PlaceHolder>
       </ul>
    
    <div id="UpdatePanel5"     > 
        <asp:PlaceHolder runat="server" ID="placeHolderE1"></asp:PlaceHolder>
    </div>   
    <div id="UpdatePanel4" > 
        <asp:PlaceHolder runat="server" ID="placeHolderD1"></asp:PlaceHolder>
    </div>
    <div id="UpdatePanel3" > 
        <asp:PlaceHolder runat="server" ID="placeHolderC1"></asp:PlaceHolder>
    </div>
    <div id="UpdatePanel2" > 
        <asp:PlaceHolder runat="server" ID="placeHolderB1"></asp:PlaceHolder>
    </div>

    <div id="UpdatePanel1" > 
        <asp:PlaceHolder runat="server" ID="placeHolderA1"></asp:PlaceHolder>
    </div>
    
    
    
    </div> 
    
    </div> 
    
   
<br />
</asp:Panel> 

<div> </div>


<!-- Start Of Menu Discount -->
   <cc1:CollapsiblePanelExtender 
ID="CollapsiblePanelExtenderMenuDis"
	runat="server"	
	 
      TargetControlID="PnlContentMenuDis" 
        ExpandControlID="PnlTitleMenuDis" 
      CollapseControlID="PnlTitleMenuDis" 
        TextLabelID="PnlDisLabel2" 
      CollapsedText="Show Details (...)" 
        ExpandedText="Hide Details (...)" 
      ImageControlID="PnlDisImage" 
      ExpandedImage= "~/Images/button_minus_blue.png"
      CollapsedImage="~/Images/button_plus_blue.png"
      Collapsed="True" SuppressPostBack="false"
       >
    </cc1:CollapsiblePanelExtender>  
   
   <asp:Panel ID="PnlTitleMenuDis" runat="server"  CssClass="collapsePanelHeader">
        
        <asp:Image ID = "PnlDisImage" runat="server" ImageUrl= "~/Images/button_plus_blue.png" />  
        <asp:Label ID="PnlDisLabel1" runat="server" Text=" My Discounts and Specials.... " 
        Height="26px"                                        
        CssClass="labelxr"/>
        <asp:Label ID="PnlDisLabel2" runat="server" Text="Show Details(...)" Height="23px"
        CssClass="labelxr"/>
         
   </asp:Panel> 
     
   <asp:Panel ID="PnlContentMenuDis" runat="server" CssClass="collapsePanel" >
    
    
    <div style=" vertical-align: middle; text-align:center ;  background-color:#333333; border:1px solid white ; min-height:100px; padding-top:30px;   "   >
       <asp:Button ID="insertNewDicount" runat="server" Text="Create a New Deal"  
              CssClass=" twtBtn2 " onclientclick="javascript:return createNewCpn(this);" 
               Width="200px" />
               <asp:Button ID="viewdiscounts" runat="server" Text="View My Deals"  
              CssClass=" twtBtn2 "  PostBackUrl="~/BizUsers/disCounts.aspx"  
               Width="200px" /><span id="ajaxdisCountLoader"> </span>
    </div>
       
    <div class="disCountInsert bizInv" style="overflow:hidden;   "  >
            <div class="closeButton" onclick="return emptyNewCpn()" title="Close"  style="top: 0%; left: 96% ; "></div> 
            
            <fieldset id="discountInsertstep1" style=" padding-left:10px; display:block; position:relative ;     "   >
            <legend  style="text-align:center; text-transform:uppercase; font-size:10px; font-weight:bold;    font-family:Palatino Linotype;">
            <img src="../Images/step1.png" alt="step1"  align="middle" />Deal Creator - Step 1 
            
            </legend>
    	    <br />
		    Deal Heading: <span class="smallFont italics " >A small phrase to desribe your Deal </span>
		    <textarea  id="TextAreaDiscountHeader"  style="width: 500px; overflow:hidden ;  " rows="3" cols="100" class="bizTxt"></textarea><br />
		    <br />
            Detailed Description about the Discount :<br />
            <span class="smallFont italics ">Let your customers see your funny side. Be as creative as you can in describing your deals. </span>
		    <br />
		    <textarea id="TextAreaDiscountDesc"  style="width: 500px; overflow:hidden ;" rows="6" cols="100" class="bizTxt"></textarea>
		    <br /><br />Exceptions:  <span class="smallFont italics ">Not Valid for 
		    employees, Cannot to be combined with other offers, in short the fine print</span>  <br />
		    <textarea id="TextAreaDiscountExcep" style="width: 500px; overflow:hidden ;" rows="6" cols="100" class="bizTxt"> </textarea>
		    
            <br />
            <br />
            <button type="button" onclick="turnRight(this);"   style="font-size: 15px ;   font-family:Comic Sans MS;position:absolute ;left:70%     "  > Next <img src="../Images/go-next-7.png"  style="border-width:0px;   "  alt="Go Next" title="Step 2"  align="middle" />  </button>   
            <br />
            <br />
            <br />
            <br />
            </fieldset>
            
            <fieldset  id="discountInsertstep2" class="disCountSteps">
            <legend  style="text-align:center; text-transform:uppercase; font-size:10px; font-weight:bold;    font-family:Palatino Linotype;">
            <img src="../Images/step2.png" alt="step1"  align="middle" />Deal Creator - Step 2 
            </legend>
    	        <br />
		        <span class="bizLabel" >Coupon Start Date : </span> <input id="DiscountStartdate" type="text"  class="datepicker"/> <br />
		        <span class="bizLabel" >Coupon Expiry Date: </span> <input id="DiscountExpirydate" type="text" class="datepicker" /><br />
		        <span class="bizLabel" >Sell this coupon until: </span> <input id="DiscountStopSelling" type="text" class="datepicker" />
                <span class="smallFont italics ">How long would you want us to post this coupon online.</span>
                <br />
             <span class="bizLabel" >Total Coupon Count:</span> 
		    <input id="TextMaxPrints" type="text"   /> 
            <span class="smallFont italics ">We stop selling when this limit's reached.</span>
            <br /><br />
            <br />
            <button type="button" onclick="turnLeft(this);"   style="font-size: 15px ;   font-family:Comic Sans MS;position:absolute ;left:10%     "  > <img src="../Images/go-previous-7.png"  style="border-width:0px;   "  alt="Go Back" title="Step 2"  align="middle" /> Back </button>   
            <button type="button" onclick="turnRight(this);"   style="font-size: 15px ;   font-family:Comic Sans MS;position:absolute ;left:70%     "  > Next <img src="../Images/go-next-7.png"  style="border-width:0px;   "  alt="Go Next" title="Step 2"  align="middle" />  </button>   
            <br /><br /><br />
            </fieldset>
            
            <fieldset  id="discountInsertstep3" class="disCountSteps" >
            <legend  style="text-align:center; text-transform:uppercase; font-size:10px; font-weight:bold;    font-family:Palatino Linotype;">
            <img src="../Images/step3.png" alt="step1"  align="middle" />Deal Creator - Step 3 
            </legend>
    	        <br />
		        <span class="bizLabel" >Original Price: </span> <input id="orgPrice" type="text"  /> <br />
		        <span class="bizLabel" >Coupon Price: </span> <input id="couponPrice" type="text" /><br />
		     
             <br />
            <button type="button" onclick="turnLeft(this);"   style="font-size: 15px ;   font-family:Comic Sans MS;position:absolute ;left:10%     "  > <img src="../Images/go-previous-7.png"  style="border-width:0px;   "  alt="Go Back" title="Step 2"  align="middle" /> Back </button>   
            <button type="button" onclick="createReviewCoupon(this);"   style="font-size: 15px ;   font-family:Comic Sans MS;position:absolute ;left:70%     "  > Next <img src="../Images/go-next-7.png"  style="border-width:0px;   "  alt="Go Next" title="Step 4"  align="middle" />  </button>   
            <br /><br /><br />
            </fieldset>
            
            
            <fieldset  id="discountInsertstepFinal" class="disCountSteps" >
            <legend  style="text-align:center; text-transform:uppercase; font-size:10px; font-weight:bold;    font-family:Palatino Linotype;">
            <img src="../Images/step4.png" alt="step1"  align="middle" />Deal Creator - Review and Submit 
            </legend>
            <br />

                <div id='disCountreviewText' style="border-style:dashed; border-color:green; border-width:1px;    "   ></div>
            <br /> 


            <input id="Submit1" type="submit" value="submit" class="btn " onclick="javascript:return insertNewCpn();"  />
		    <input id="Button1" type="reset" value="Cancel" class="btn " onclick="javascript:emptyNewCpn();" />
            <br /><br />
            <button type="button" onclick="turnLeft(this);"   style="font-size: 15px ;   font-family:Comic Sans MS;position:absolute ;left:10%     "  >  <img src="../Images/go-previous-7.png"  style="border-width:0px;   "  alt="Go Back" title="Step 2"  align="middle" /> Back </button>   
            <br />
            <br /><br /><br />
            </fieldset>
		 
            
	</div> 

    
   
    </asp:Panel>     
                



    <div id="dyNumPopUp" class="dialog"  style="position:fixed;"  >
    <div id="dyNumPopUpCon" class="content "> <br />
     <br />
     <br />
    </div>
    <div class="opaque " style="background-color:blue ;"  ></div>
    <div class="closer"><img alt="Close" src="../fancybox/fancy_close.png" /></div>
</div>

</asp:Content>