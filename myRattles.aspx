<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageNoScriptManager.master" AutoEventWireup="true" CodeFile="myRattles.aspx.cs" Inherits="myRattles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css"   >
    body {
      
     font-size:small;

     
     }
    #pageMiddle 
    {
     
    }
    .myRattles
    {
        font-size:15px!important;
         font-style: italic  ;
          font-variant: normal  ;
           font-style:normal ;
            font-weight:normal ;
            color:#444 ;    
        font-family:Times New Roman ;
        
    }

    .myRattles ul
    {
        list-style:none;
        padding: 5px 0px 0px 15px; 
        margin:10px;
    }

    .myRattles  li
    {
         list-style:none;
         padding:0px;
         padding-top:10px;
         padding-bottom: 10px;
         border-bottom-color:White;
         border-bottom-width:thin ;
         border-bottom-style:solid ; 
         background-color:#D3F9BC; 
         width:700px;
           
         }
    
    .myRattles  li a
    {

     text-decoration:none; 
     font-weight:bolder ; 
     font-style:normal ;
      
         }
    
    .myRattles  li a:hover
    {

     text-decoration:underline;
       
         }
    
    .myRattles  li:hover
    {
        background-color:#C5F7A6;  

    }

    .myRattles .rattlefooter
         {
         
            color:Black ;
            /*width:500px; 
         */
         }
    .myRattles .imgholder
    {
    float:left ; padding:10px; padding-right:0px ;
    width:55px ;
    }

    .myRattles .imgholder img
    {
    /*width:55px ;*/
    width:48px ;
    height:48px;
    outline-style:none;
    }


    .myRattles .rattleContent
    {float:left;  width:80%; }

    .myRattles .rattlefooter
    {
        font-size:10px;
        color:#789;
        padding:0px;
        padding-left:10px;
    }
    .myRattles .rattlefooter a
         {
         
          text-decoration:none; 
          font-weight:bold ; 
          font-size:10px;
            /*width:500px; 
         */
         }
    .myRattles .rattlefooter a:hover
         {
         
          text-decoration:underline;
          font-weight:bold ; 
          
            /*width:500px; 
         */
         }
    
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript"  >
    $(document).ready(function () {

        pRcommonFns.getMyrattles(1, "rattlewrapper", true);
        //$('#pageMiddle').css('background-color', 'inherit');


    });

    function getLikes(id) {
        $('#L' + id).html('[122]'); 
        //document.write('122');
        
    
    }
</script>

<asp:LoginView ID="HeaderRightLoginView" runat="server" >
     <AnonymousTemplate >
         <div style="border-style:solid ; border-color:Blue;border-width:thick ; text-align:center; padding:10px;"    >
         Login to view your Rattles....
          </div>  
     </AnonymousTemplate> 
     <LoggedInTemplate >
        hi there
        <div  class="myRattles"  >
            <ul id="rattlewrapper"></ul>
            <div  id="moreDataLink" class="rattlefooter" > <img alt='Loading rattles' src='Images/ajaxPreLoaderBlocks.gif' /> Loading Your Rattles..  </div>
        </div> 
        
     </LoggedInTemplate>
</asp:LoginView> 
        

</asp:Content>

