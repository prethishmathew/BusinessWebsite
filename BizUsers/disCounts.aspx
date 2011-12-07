<%@ Page Title="" Language="C#" MasterPageFile="~/master2.master" AutoEventWireup="true" CodeFile="disCounts.aspx.cs" Inherits="BizUsers_disCounts" enableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="float:right; margin-top:5px; "  >

     <asp:Button ID="insertNewDicount" runat="server" Text="Create a New Deal"  
              CssClass=" twtBtn2 "  onclientclick="javascript:return createNewCpn(this);" 
               Width="200px" />
         </div>      
    
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
                <asp:Parameter Name="orgPrice" Type="Decimal" />
                 <asp:Parameter Name="cpnPrice" Type="Decimal" />
            </UpdateParameters>
            <SelectParameters>
                <asp:Parameter Name="bizID"  Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    <asp:Label ID="gridErrorMessage" runat="server" ></asp:Label> 
    
        <asp:GridView ID="GridView1" runat="server"
         EnableViewState="false"  
        AutoGenerateColumns="False" 
             BorderStyle="None" 
              BorderColor="Transparent"    
            ForeColor="Black"
             Font-Names="georgia, Helvetica, Arial, sans-serif"       
              Font-Size="1.1em" 
           OnRowDeleting="GridView1_RowDeleting" 
           OnRowUpdating ="GridView1_RowUpdating"
            BorderWidth="0px"   
            OnRowEditing ="GridView1_RowEditing"
                  
          >
             
           <Columns  >     
               <asp:TemplateField HeaderText="My Deals " ItemStyle-BorderWidth="0px"  HeaderStyle-HorizontalAlign="Center"       >
               
                  <ItemTemplate   >
                  <div style="width:700px; background:white; padding:5px;padding-left:7px;padding-right:7px;" > 
                  <h3 style="text-align:center;color:Blue"><%#Eval("Header") %>	</h3> 
                  <span style="float:right">ID:<asp:Label runat="server" ID="lblDisCntID"  text=<%#Eval("DisCountID")%>></asp:Label> </span>  <br />
                  <span style="float:right;color:green; font-weight:bold ; "> Orginal Price: <%#Eval("orgPrice")%></span><br />
                  <span style="float:right;color:green;font-weight:bold;"> Coupon Price: <%#Eval("cpnPrice")%></span>   <br />

                  <%#Eval("Description") %><br />
                  <span style="font-size:10px; "  > <%#Eval("Exceptions")%> </span> <br /><br />
                  <span style=" font-style:italic "  > Start date:<%#Eval("StartDate") %></span>
                  <span style=" font-style:italic; float:right ; "  > End date:<%#Eval("ExpiryDate")%></span> <br /><br />
                   <span style=" color:Green ; font-weight:bold;  float:right ; "  >Maximun Coupons to Print: <%#Eval("MaxNumberofPrints")%> </span ><br />
                   <span style=" color:Green ; font-weight:bold;  float:right ; "  >Coupons printed till date:<%#Eval("CurrentPrints")%> </span>   <br />
                   
                   <br />
                   
                   <asp:LinkButton  runat="server"   CommandName="Edit" ID="cmdEdit" CssClass="btn" CausesValidation ="false" CommandArgument="Edit" ToolTip="Edit"  >
                   <asp:Image ImageUrl="~/Images/edit-3.png" runat="server" AlternateText="Edit"/>
                   Edit
                   </asp:LinkButton> 
                   &nbsp; 
                   <asp:LinkButton  runat="server"   CommandName="Delete" ID="btnDel" CssClass="btn" CommandArgument="Delete" ToolTip="Delete">        
                   <asp:Image ID="Image1" ImageUrl="~/Images/edit-delete-2.png" AlternateText="Delete"  runat="server" />
                   Delete
                   </asp:LinkButton> 
                   
                    </div>    
                     
                    </ItemTemplate>  
                  
                  <EditItemTemplate >
                  <div  style="width:600px; background:white; padding:5px;padding-left:7px;padding-right:7px; color:black " >
                   <asp:Label runat="server" ID="lblDisCntID"  Text=<%# Eval("DisCountID")%> ></asp:Label><br />
                  
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
                   Orginal Price :<asp:TextBox id="txtOrgPrice" text=<%# Bind("orgPrice")%> 
                   runat="server" Wrap="true"  /> 
                   Coupon  Price :<asp:TextBox id="txtCpnPrice" text=<%# Bind("cpnPrice")%> 
                   runat="server" Wrap="true"  /> 
                   <br />
                   <asp:LinkButton  runat="server"  Text="Update" CommandName="Update" ID="cmdUpdate" CssClass="btn" CausesValidation ="false" CommandArgument="Update" />
                   <asp:LinkButton  runat="server"  Text="Cancel" CommandName="Cancel" ID="btnCancel" CssClass="btn" CommandArgument="Cancel"  />        <br />
                   <br />
                   <asp:Label ID="errorTxt"  ForeColor="red" Font-Size="10px" Font-Italic="true"    runat="server"  ></asp:Label>
                    <br />
                  </div>
                  
                  </EditItemTemplate >
                  
                    
               </asp:TemplateField>
                  
                  
                                           
                 </Columns> 
             
          </asp:GridView>
          
          
        
  
    
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
            Detailed Description about the Discount :
		    <br />
		    <textarea id="TextAreaDiscountDesc"  style="width: 500px; overflow:hidden ;" rows="6" cols="100" class="bizTxt"></textarea>
		    <br /><br />Exceptions:  <span class="smallFont italics ">Not Valid for 
		    employees, Cannot to be combined with other offers</span>  <br />
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
		     
             <span class="bizLabel" >Amount of coupons to generate:</span> 
		    <input id="TextMaxPrints" type="text" class="bizTxt"/> <br /><br />
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
    
</asp:Content>

