<%@ Page Language="C#" MasterPageFile="~/MasterPageNoScriptManager.master"  AutoEventWireup="true" CodeFile="showBiz.aspx.cs" Inherits="showBiz" Title="PatRates " %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style  type="text/css" >

 body {
  background-image: url( "~/profileImages/emblem-generic.png");
  /*background-image: url('<%= imgPath %>');*/

 }

</style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<br />

<div id="tabs" class="mainMenu" style="width: 1000px; min-height:400px; ">
<asp:HiddenField ID="memIDHide" runat="server" />
<asp:PlaceHolder runat="server" ID="placeHolderHeader"></asp:PlaceHolder>
<div id="SNBtnDiv" style="font-size:x-small; position:absolute; right:-200px;top:20px; z-index:100;      "   >
<img src="Images/ajaxPreLoaderBlocks.gif"  alt="" /> Loading ..    
</div> 
<ul id="ShowBizMainMenu"   >


<li><a href="#ShowBizMainMenuHome">Home</a> </li>
<li><a href="#ShowBizMainMenuAboutUs">About Us</a></li>
<li><a id="showInventory"  href="#ShowBizMainMenuInventory" runat="server"   >Inventory</a></li>
<li><a id="showGallery"   href="#ShowBizMainMenuGallery">Gallery</a></li>
<li><a id="showDiscounts" href="#ShowBizMainMenuDiscounts">Deals</a></li>
<li><a id="showMYRattles" href="#ShowBizMainMenuRattles" style="outline-color:none; outline-width:0px;  outline-style:none; color:transparent; "  >
    <img src="Images/myRattlesblueV2.png"  alt="rattles" title="myRattles"  /></a></li>
<asp:PlaceHolder runat="server" ID="PlaceHoldershowBizMain"></asp:PlaceHolder> 

</ul>

<div id="FlashingNews"></div>

<div id="ShowBizMainMenuHome">

<asp:PlaceHolder runat="server" ID="placeHolderMainMenuHome"></asp:PlaceHolder>

    
</div>

<div id="ShowBizMainMenuAboutUs"   >

    <ul id="sideMenuAboutUs" >
        <li><a href="#ShowBizSideMenuAboutUsContactInfo">Contact Info</a> </li>
        <li><a href="#ShowBizSideMenuAboutUsBizHrs">Our Timings</a> </li>
    </ul>

    <div id="ShowBizSideMenuAboutUsContactInfo" > 
        <asp:PlaceHolder runat="server" ID="placeHolderAboutUsContactInfo"></asp:PlaceHolder>
        
    </div>
    
    <div id="ShowBizSideMenuAboutUsBizHrs"> 
        <asp:PlaceHolder runat="server" ID="placeHolderAboutUsBizHrs"></asp:PlaceHolder>
    </div>
    

</div>
<div id="ShowBizMainMenuInventory">

        <ul id='tabsInv' >
            <asp:PlaceHolder runat="server" ID="placeHolderInvContainer"></asp:PlaceHolder>
        </ul>
        <div id="UpdatePanel5" > 
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
        
        <div id="UpdatePanelDocs" > 
            <asp:PlaceHolder runat="server" ID="placeHolderDocs"></asp:PlaceHolder>
        </div>
        
        
</div>
<div id="ShowBizMainMenuGallery" class="galleryPhotos">
        <asp:PlaceHolder runat="server" ID="placeHolderGallery"></asp:PlaceHolder>
</div>
<div id="ShowBizMainMenuDiscounts" align="center">

     <b>Fetching your deals. Please Wait!!....</b><img src="Images/ajaxPreLoader.gif" alt="fetching your deals"/>

</div>

<div id="ShowBizMainMenuRattles"  align="center"  >

     <div  class="myRattles" style="position:relative;   font-size:21px; display:block;    "           >
        <ul style="width:500px; margin:0px; padding-left:10px; padding-right:0px; text-align:left;  " id="myRattles"    >
        
        </ul>
    </div> 
    <div style="clear:both" ></div>
</div>
<asp:PlaceHolder runat="server" ID="extWebPlaceholder" ></asp:PlaceHolder>

</div>



</asp:Content>

