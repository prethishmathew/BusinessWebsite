<%@ Page Title="myrattlePons - My Deals" Language="C#" MasterPageFile="~/MasterPageNoScriptManager.master" AutoEventWireup="true" CodeFile="myrattlepons.aspx.cs" Inherits="secPages_myrattlepons"   %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title >rattlePons</title>
<style type="text/css" >
    
    .schemer { 
        position:relative ;
        text-align:Left ;
        width:800px;
         font-size:12px; 
         color:#3D3D3D; 
    }

    .schemer .postion1
    {
        margin:2px;    
        border-color:Blue;
        border-width:thin ;
        border-style:solid ; 
        left:0px;           
        width:250px;
         background-color:White; 
        height:250px;
        padding-right:10px;
        padding-top:10px;
        float:left;
    }

    
    .schemer .postion1 a{ 
     
     border-width:1px;
     border-color:Blue;     
     border-style:solid ;
     background-color:#078CE0;  
     float:right;
     font-size:12px; 
     text-decoration:none;
     color:White;
     font-weight:bolder ;
     padding:5px;
       }

    .schemer .postion1 a:hover{ 
     text-decoration:underline;
      }
    .schemer .postion1 img
    {
      border-width:1px;
      border-color:#3D3D3D;     
      border-style:solid ;
       margin:5px;     
    }
    
    #nextRecords{
     font-style:normal;
     font-weight:lighter ;
     font-size:10px;  
    }

    #nextRecords a{
     
     border-width:1px;
     border-color:Blue;     
     border-style:solid ;
     background-color:#078CE0;  
     float:right;
     font-size:12px; 
     text-decoration:none;
     color:White;
     font-weight:bolder ;
     padding:5px;
}

</style> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="schemer">
   
        
    </div>    
    <br  style="clear:both "  />
    
    <div id="nextRecords" >
    <a href="#" onclick="getmyCoupons(2);return false;">Get Next</a>  
    
    </div>
    <div id="loader">
        <img alt="Loading..." src="../Images/ajaxPreLoader.gif" /> Loading Please wait...
    </div>
    
</asp:Content>

