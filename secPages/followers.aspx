<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoScriptManager.master" AutoEventWireup="true" CodeFile="followers.aspx.cs" Inherits="secPages_followers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



<style type="text/css" >
/*

My Followers CSS

*/

.myFlwers h3
{
 text-align:Left ;
 color:Black ; 
}
.myFlwers  table
{
	background-color: white;
	color:Black ;
	font:13px/16px "Lucida Grande","Book Antiqua",Sans-serif;
	word-wrap: break-word; 
	width: 700px;
    border-collapse:collapse ; 
    border:0;
    padding:0;
    margin:0; 
    table-layout: auto ;  
}

.myFlwers th{
 text-align:left ; 
 background: #d3f9bc;
}



.myFlwers tr,td{

   border-collapse: collapse;   border: 0;   padding: 0;   margin: 0;
   }


.myFlwers tr{

    border:0;
	background: #e4ffcb;
	 border-bottom-style:solid ;
	  border-bottom-width:1px;  
	   border-bottom-color:White ; 
	font-size:small ;  
	
	color: black; 
}
.myFlwers tr:hover  {
     font-size :small ;
	 background: #d3f9bc;
	 cursor: pointer ;
	  
}

.myFlwers tr:focus  {
     font-size :small ;
	 background: #d0dafd;
	  cursor: pointer ;	  
}



/*************************************************/
/*************************************************/
/*************************************************
This is for the My Mall CSS
*************************************************/
.myShopsTab
{
 background-color:#d0dafd; 
width:70%;


 }
.myShopsTab h3
{
text-align:Left ;
 color:Black ; 
  
}
.myShopsTab  table
{
	background-color: white;
	color:Black ;
	font:13px/16px "Lucida Grande","Book Antiqua",Sans-serif;
	word-wrap: break-word; 
     width:100%; 
    border-collapse:collapse ; 
    border:0;
    padding:0;
    margin:0; 
    table-layout: auto ;  
}

.myShopsTab th{
 text-align:left ; 
 background: #d0dafd;
}


.myShopsTab tr,td{

   border-collapse: collapse;   border: 0;   padding: 0;   margin: 0;
   }


.myShopsTab tr{

    border:0;
	background: #e8edff;
	 border-bottom-style:solid ;
	  border-bottom-width:1px;  
	   border-bottom-color:White ; 
	font-size:small ;  
	
	color: black; 
}
.myShopsTab tr:hover  {
     font-size :small ;
	 background: #d0dafd;
	 cursor: pointer ;
	  
}

.myShopsTab tr:focus  {
     font-size :small ;
	 background: #d0dafd;
	  cursor: pointer ;	  
}


.groupsDiv
{
 
 top:0; 
   right:0; 
   background-color:#CED8FD;    
   width:30%;   
   position:absolute ;  
   height:90%  ;
   padding-left:10px;   
    color:Black ;  
     border:solid thin black;
    border-top: solid 13px black;
    
  
     
}
.groupsDiv a
{
  text-decoration:none; 
   font-size:small ;
    font-family:Courier New ;  
     color:Black ;  
}

.groupsDiv a:hover
{
  text-decoration:underline; 
    color:Red;  
}

.groupsDiv a:focus
{
  text-decoration:underline; 
    color:Blue;  
}


.mainDiv
{
	font:13px/16px "Lucida Grande","Book Antiqua",Sans-serif;
    width:700px;
 	
}

.editmainDiv
{

width:100%;
color:Blue ;
font-weight:bold ;     
}


.rightDiv
{
overflow:hidden ;
width:50%;
float:  right ; 	   	
}


.leftDiv
{
	width:49%;
	float:  left ; 	
     padding-bottom:40px;  	      
}




</style> 

<style type="text/css"  >
    .menuDrop {
	background: blue;
	
	padding:  0px 0px 0px 0px; 
    position:relative ; 
	text-align:left ; 
	  
	}
	.menuDrop ul {
		display:  table-row    ;
		list-style-type:none;
		list-style-position:inside ;
		        width: 150px;
				border: 1px solid #ccc;
				background: #d0dafd;
				padding: 10px 0 0 0;
				
				
		}
		.menuDrop ul li {
			
			padding:   10px 0px 0px 0px;
			position: relative;
			float: left;
			 padding-right:10px; 
			  border-right:solid thin white; 
			  
			
			}
			.menuDrop ul li:hover {
				background-color: #e8edff;
			}
			.menuDrop ul li a {
				display:  table-row ;
				float: left;
				height: 23px;
				position: relative;
				top: -5px;
				right: -5px;
				padding-right: 3px;
				
				color: #383838;
				font-weight: bold ;
				font-size: 1.1em;
				vertical-align: text-top ;  
				text-decoration: none;
				}
				.menuDrop ul li a:hover, .menuDrop ul li a.zoneCur {
					
					
				}
			    .menuDrop ul li a:focus
			    {
			       outline-style:none;  
			    }
				.menuDrop ul li a span {
					position: relative;
					top: 6px;
					   
				}
				.menuDrop ul li a span em {
				          height:100%; 
				          text-decoration:none;             
				}
				.menuDrop ul li a span em:hover {
				           text-decoration:underline;  
				                       
				}
				
			.menuDrop ul li ul {
				display: none;
				position: absolute;
				top: 35px;
				left: 0px;
				width: 150px;
				border: 1px solid #ccc;
				background: white;
				padding: 10px 0 0 0;
				}
				.menuDrop ul li ul li {
					float: none;
					padding: 0; margin: 0;
					height: 100%;
				}
				.menuDrop ul li ul li:hover {
					background: none;
					}
					.menuDrop ul li ul li a {
						display: block;
						float: none;
						margin-left: -5px;
						padding: 5px 0 0 10px;
						width: 140px;
						text-align:left ;
					}
					.menuDrop ul li ul li a:hover {
						background: #d9f0b7;
					}
					    
    
    
.btnMenu {

     color:Green; 
     font-weight:bold ;
     font-family:"trebuchet ms", helvetica, sans-serif;
     font-size:x-small ; 
     /*width:70px;*/
        
}

.btnMenu:focus
{
     
     color:Blue;
     border-color:Black ; 
}

.btnMenu:hover
{
    background-color:silver; 
     cursor:pointer ; 
    border-color:Black ; 
}
    
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<div class ="mainDiv " style="width: 980px">
<ul>
            
<li><a href="#myShops"><span>My Shops</span></a></li>
<li><a href="#myFans"><span>My Fans</span></a></li>

</ul>
<div id="myShops"  style="position: relative"> 
      <div>
    <div class="myShopsTab" >
   <table cellspacing="0" cellpadding="0" border="0"  id="tableFollowers" > 
   <tr>
   
   <th colspan="3"  class="menuDrop "   > 
   
    <ul >
    <li >
    <a href="#">		        <span >
						
						<em >
							   Select 
							 </em> </span>   
							   
				</a>
					<ul >
					                <li><a href="#">All</a></li>
					</ul>
    
    </li>
    <li>
					<a href="#"><span >
						
						<em >
							  Delete
						</em>
					</span></a>
					<ul >
					 <li><a href="changeRole.aspx" >All</a></li>
					 <li><a href="#" onclick="alert('here');" >Only selected</a></li>
					</ul>
				</li>
				
				<li>
				<a href="#"><span   >
						
						<em >
						Move To Group
						</em>
					</span></a>
				<ul  >
						<li><a href="#">New Jersey Jets</a></li>
						<li><a href="#">New York Giants</a></li>
				
				</ul>
				</li>
				</ul> 
    </th>
    </tr>
   <tr>
   <td style=" width:10%" >
   <asp:CheckBox ID="CheckBox2" runat="server" />    
   </td>
   <td style=" width:20%" >
   
   <img alt="provider" src="http://patrates.com/Images/login.png" />   
   </td>
   <td style=" width:70%" >
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox3" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image1" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox4" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image2" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox5" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image3" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox6" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image4" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox7" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image5" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox8" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image6" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox9" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image7" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox10" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image8" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox11" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image9" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox12" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image10" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox13" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image11" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   
   </table> 
   </div>
    <div id="myShopsGrp"  
   class="groupsDiv"  >
   <br />
     <a href="#" >OlderFolks(10)</a>
     <br /><a href="#" >youngerOnes(10)</a>
     <br /><a href="#" >Aitkin(12)</a>
     <br /><a href="#" >McGregor(33)</a>
     <br /><a href="#" >Twins(1366)</a>
     <br /><a href="#" >Mets(178993)</a>
     <br /><a href="#" >Chicago Clubs(122313)</a>
     <br /><a href="#" >New York Giants(131222)</a>
     <br /><a href="#" >New England Eagles(12122)</a>
     <br /><a href="#" >Rediff(1121344)</a>
     
   </div>
</div>
</div>   
    <div id="myFans" style="position: relative">
<div class="myShopsTab" >
   <table cellspacing="0" cellpadding="0" border="0"  id="table1" > 
   <tr>
   
   <th colspan="3" >
   <asp:CheckBox ID="CheckBox14" runat="server" Text="Select All" CssClass="btn"   />    
   
   <asp:Button ID="Button1" class="btn" runat="server" Text="Delete"    /> 
   
   <asp:Button ID="Button2" class="btn" runat ="server" Text =" Move To" />
   </th>
   </tr>
   <tr>
   <td style=" width:10%"  >
   <asp:CheckBox ID="CheckBox15" runat="server" />    
   </td>
   <td style=" width:20%">
   <asp:Image ID="Image12" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td style=" width:70%">
   New Palce Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox16" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image13" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox17" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image14" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox18" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image15" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox19" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image16" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox20" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image17" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox21" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image18" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox22" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image19" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox23" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image20" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox24" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image21" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox25" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image22" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   <tr>
   <td>
   <asp:CheckBox ID="CheckBox26" runat="server" />    
   </td>
   <td >
   <asp:Image ID="Image23" runat="server"   ImageUrl="http://testprofilepicture.s3.amazonaws.com/Images/thumbNails/14922c76-1b7b-4cf9-a981-762ff3dcc5c2/profileImg.png" Height="48" Width="48" ImageAlign="Middle" />
   </td>
   <td>
   PatRates Inc.
   </td>
   </tr> 
   
   </table> 
   </div>
   
   <div id="myFansGrp"  
    class="groupsDiv"  >
   <br />
     <a href="#" >OlderFolks(10)</a>
     <br /><a href="#"   >youngerOnes(10)</a>
     <br /><a href="#" >Aitkin(12)</a>
     <br /><a href="#" >McGregor(33)</a>
     <br /><a href="#" >Twins(1366)</a>
     <br /><a href="#" >Mets(178993)</a>
     <br /><a href="#" >Chicago Clubs(122313)</a>
     <br /><a href="#" >New York Giants(131222)</a>
     <br /><a href="#" >New England Eagles(12122)</a>
     <br /><a href="#" >Rediff(1121344)</a>
     
   </div>
</div>



</div> 

</asp:Content>

