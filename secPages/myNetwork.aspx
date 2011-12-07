<%@ Page Title="myRattles - where the world connects to its local life" Language="C#" MasterPageFile="~/MasterPageNoScriptManager.master" AutoEventWireup="true" CodeFile="myNetwork.aspx.cs" Inherits="secPages_myNetwork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title > myRattles - where the world connects to its local life</title> 
<style type="text/css" >
    </style> 
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<!--[if gte IE 5]><![if lt IE 8]>
<style type="text/css">
.menuDrop
{
background:blue;
display:inline;
}

.menuDrop ul
{
display:"inline" ;
}

.menuDrop ul li{
left:0;
}


.menuDrop ul li a {
display:"inline" ;
}

.menuDrop ul li ul {
display: none;
position: absolute;
top: 35px;
left: 5px;
width: 150px;
border: 1px solid #ccc;
background: white;
float:left;
z-index:1200!imp;               			  
}
.menuDrop ul li ul li {
display:inline;
float: none;
padding: 0; 
margin: 0;
height: 100%;
test-align:left;
}
.menuDrop ul li ul li a {
display: inline;
float: left;
padding: 5px 0 0 10px;
width: 140px;
text-align:left ;
padding-left:0px;
}				

</style>
<![endif]><![endif]-->

<div class ="mainDiv " style="width: 980px;">

<asp:LoginView runat="server" >
<RoleGroups >
    <asp:RoleGroup Roles="bizOwner" >
    <ContentTemplate >
        <ul>        
            <li><a id="showShops" href="#myShops"><span>My Shops</span></a></li>
            <li><a id="showFans" href="#myFans"  ><span>My Fans</span></a></li>
        </ul>
    </ContentTemplate>
 </asp:RoleGroup>
    <asp:RoleGroup Roles="surfer" >
    <ContentTemplate >
        <ul>        
            <li><a id="showShops" href="#myShops"><span>My Shops</span></a></li>
        </ul>
    </ContentTemplate>
    </asp:RoleGroup> 
</RoleGroups>
</asp:LoginView> 
<div id="myShops"   style="width: 100%; min-height:330px;"> 
     <div  class="myShopsTab"   style="float: left; width:60%  ">
     
     <table cellspacing="0" cellpadding="0" border="0"   id="tableShops"   > 
     <thead    >
     <tr>
        <th style="width:10%"  >
        </th><th></th>
        <th></th>
        <th style="width:40px"></th>
        <th style="width:40px"></th>
        <th style="width:70px"></th>
        
      </tr>
     </thead>
     <tbody >
          
     </tbody>
     
     </table>
         
  </div> 
    <div id="myShopsGrp"  style="float: left ; width:28%; ">
  <div   id="myShopGrpsHeader"    class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix"  > 
  My Groups
  </div>
  <div id="myShopsGrps" class="groupsDiv" style=" width:96%" >
        Loading ....
  
  </div>         
         </div> 
     
</div>
<asp:LoginView  runat="server" >
<RoleGroups >
<asp:RoleGroup  Roles="bizOwner"  >
<ContentTemplate >
<div id="myFans" style="width: 100%; min-height:330px;">
     <div  class="myShopsTab"   style="float: left; width:60%  ">
     
     <table cellspacing="0" cellpadding="0" border="0"   id="tableFans"   > 
     <thead    >
     <tr>
        <th style="width:10%"  ></th>
        <th></th>
        <th></th>
        <th style="width:40px"></th>
        <th style="width:40px"></th>
        <th style="width:70px"></th>
        </tr>
     </thead>
     <tbody >
          
     </tbody>
     
     </table>
         
  </div> 
    <div id="myFansGrp"  style="float: left ; width:28%; ">
  <div   id="myFansGrpsHeader"    class="fg-toolbar ui-toolbar ui-widget-header ui-corner-tl ui-corner-tr ui-helper-clearfix"  > 
  My Groups
  </div>
  <div id="myFansGrps" class="groupsDiv" style=" width:96%" >
        Loading ....
  
  </div>         
         </div> 
</div>
</ContentTemplate>
</asp:RoleGroup>  
</RoleGroups>
</asp:LoginView>
</div>


<div id="dyNumPopUp" class="dialog" >
    <div id="dyNumPopUpCon" class="content "> <br />
    
            
            
     
     <br />
     <br />
    
    </div>
    <div class="opaque " ></div>
    <div class="closer"><img alt="Close" src="../fancybox/fancy_close.png" /></div>
</div>
   
</asp:Content>

