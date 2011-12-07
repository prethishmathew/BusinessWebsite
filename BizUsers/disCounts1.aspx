<%@ Page Language="C#" AutoEventWireup="true" CodeFile="disCounts1.aspx.cs" Inherits="BizUsers_disCounts"   %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/pradsStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >
    body{
     background-image:none; 
    }
    </style> 
</head>
<body style="background-color: #FFFFFF; height: 500px; overflow: scroll; width:700px; ">
    <form id="form1" runat="server">
    <div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            DeleteMethod="DeleteDiscount" 
            InsertMethod="InsertDiscount" SelectMethod="SelectDiscount" 
            TypeName="discountUtility" UpdateMethod="UpdateDiscount">
            <DeleteParameters>
                <asp:Parameter Name="bizDiscountID" Type="Int64" />
                <asp:Parameter Name="bizID" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="bizDiscountID" Type="Int64" />
                <asp:Parameter Name="bizID" Type="String" />
                <asp:Parameter Name="header" Type="String" />
                <asp:Parameter Name="description" Type="String" />
                <asp:Parameter Name="exceptions" Type="String" />
                <asp:Parameter Name="startDate" Type="DateTime" />
                <asp:Parameter Name="expiryDate" Type="DateTime" />
                <asp:Parameter Name="maxNumberofPrints" Type="Int32" />
            </UpdateParameters>
            <SelectParameters>
                <asp:Parameter Name="bizID"  Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    <asp:Label ID="gridErrorMessage" runat="server" ></asp:Label> 
    
        <asp:GridView ID="GridView1" runat="server"
         EnableViewState="false"  
        AutoGenerateColumns="False" 
            
             DataSourceID ="ObjectDataSource1"   
           OnRowDeleting="GridView1_RowDeleting" 
           OnRowUpdating ="GridView1_RowUpdating"
            BorderWidth="0px"   
            OnRowEditing ="GridView1_RowEditing"
                  
          >
             
           <Columns >     
               <asp:TemplateField HeaderText="My Deals "   >
               
                  <ItemTemplate >
                  <div style="width:600px; background:white; padding:5px;padding-left:7px;padding-right:7px;" > 
                  <h4 style="text-align:center;color:Blue"><%#Eval("Header") %>	</h4> 
                  <span style="float:right">ID:<asp:Label runat="server" ID="Label1"  text=<%#Eval("DisCountID")%> /> </span>  <br />
                  <span style="float:right;color:green; font-weight:bold ; "> Orginal Price: $50</span><br />
                  <span style="float:right;color:green;font-weight:bold;"> Coupon Price: $50</span>   <br />

                  <%#Eval("Description") %><br />
                  <span style="font-size:10px; "  > <%#Eval("Exceptions")%> </span> <br />
                  <span style=" font-style:italic "  > Start date:<%#Eval("StartDate") %></span>
                  <span style=" font-style:italic; float:right ; "  > End date:<%#Eval("ExpiryDate")%></span> <br />
                   Maximun Coupons to Print: <%#Eval("MaxNumberofPrints")%> <br />
                   Coupons printed till date:<%#Eval("CurrentPrints")%> <br />   
                   
                   <br />
                  
                   <asp:Button  runat="server"  Text="Edit" CommandName="Edit" ID="cmdEdit" CssClass="btn" CausesValidation ="false" CommandArgument="Edit"   />
                   <asp:Button  runat="server"  Text="Delete" CommandName="Delete" ID="btnDel" CssClass="btn" CommandArgument="Delete"  />        
                   
                    </div>    
                     
                    </ItemTemplate>  
                  
                  <EditItemTemplate >
                  <div  style="width:600px; background:white; padding:5px;padding-left:7px;padding-right:7px; color:black " >
                   <%# Eval("DisCountID")%> <br />
                  
                  
                  <b>Discount Header:</b><br />
                  <asp:TextBox id="txtHeader" text=<%# Bind("header")%> Width="95%"
                   Height="30px" runat="server" Wrap="true"   TextMode="MultiLine" MaxLength="100"  /><br />
                   <br />
                                     
                  <b>Description:</b><br />
                  <asp:TextBox id="txtDescription" text=<%# Bind("description")%> Width="95%"
                   Height="60px" runat="server" Wrap="true"   TextMode="MultiLine"  MaxLength="500" />        <br />
                  <br />
                  
                  <b>Exceptions and Restrictions applicable on the discount:</b><br />
                  <asp:TextBox id="txtExceptions" text=<%# Bind("exceptions") %> Width="95%"
                   Height="60px" runat="server" Wrap="true"   TextMode="MultiLine" MaxLength="500"/>        
                   
                   Start Date:<asp:TextBox id="txtStartDate" text=<%# Bind("startDate") %> 
                   runat="server" Wrap="true"    
                    CssClass= "datepicker"    />                    
                   <br />
                   End Date  :
                   <asp:TextBox id="txtExpiryDate" text=<%# Bind("expiryDate") %> 
                   runat="server" Wrap="true"   CssClass="datepicker"/><br />
                   # of Coupons to Print :<asp:TextBox id="txtMaxNumberofPrints" text=<%# Bind("maxNumberofPrints")%> 
                   runat="server" Wrap="true"  /> 
                   <br />
                   <asp:Button  runat="server"  Text="Update" CommandName="Update" ID="cmdUpdate" CssClass="btn" CausesValidation ="false" CommandArgument="Update"   />
                   <asp:Button  runat="server"  Text="Cancel" CommandName="Cancel" ID="btnCancel" CssClass="btn" CommandArgument="Cancel"  />        
               
                  </div>
                  
                  </EditItemTemplate >
                  
                    
               </asp:TemplateField>
                  
                  
                                           
                 </Columns> 
             
          </asp:GridView>
          
          
        
  
    
    </div>
    
    
    
    </form>
    
</body>
</html>
