<%@ Page Title="" Language="C#" MasterPageFile="~/pts/Release/Release.Master" AutoEventWireup="true" CodeBehind="NewBatchList.aspx.cs" Inherits="PTSCOATING.pts.Release.NewBatchList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

         <script type="text/javascript">
             $(document).ready(function () {
                 $(function () {
                     $(".Date").datepicker({dateFormat:'mm-dd-yy'});
                 });
             });
         </script>


<style>
table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}
th, td {
  padding: 5px;
  text-align: center;
}
</style>
     <div class="col-md-12">
        </div>
     <div class="col-md-12">
        <div class="tile">
            <h3 class="tile-title">Add New Batch</h3>
            <div class="tile-body">
                <div class="row">
                       <div  style="width:100%;">
                     <table style="width: 100%">
                         <asp:Repeater ID="repDetails" runat="server" OnItemDataBound="repDetails_ItemDataBound">
                             <ItemTemplate>
                                 <tr style="background-color:darksalmon;">
                                     <th>Batch:</th>
                                     <td>
                                         <asp:Literal ID="litsqno" runat="server" Visible="false" Text='<%#Eval("SQNO")%>'></asp:Literal>
                                         <asp:Literal ID="litsaleorder" runat="server" Visible="false" Text='<%#Eval("SaleOrder")%>'></asp:Literal>
                                         <asp:Literal ID="litlineitem" runat="server" Visible="false" Text='<%#Eval("LineItem")%>'></asp:Literal>

                                         <asp:Literal ID="litqrcode" runat="server" Text='<%#Eval("Code")%>'></asp:Literal>
                                         <asp:TextBox ID="TextBox1" Style="display: none" runat="server" Text='<%#Eval("Code")%>'></asp:TextBox>
                                     </td>
                                 </tr>
                                
                                 <tr style="background-color:darksalmon;">
                                     <th>Date:</th>
                                     <td>
                                           <asp:TextBox ID="txtdate"  CssClass="form-control "  Font-Size="Larger" TextMode="Date"  Height="75px" placeholder="DD-MM-YYYY" runat="server"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                                ErrorMessage="Required" ControlToValidate="txtdate" ValidationGroup="addbatch" ></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                Enabled="true" TargetControlID="RequiredFieldValidator5" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />
                                         </td>
                                 </tr>
                                   <tr style="background-color:darksalmon;">
                                     <th>Length:</th>
                                     <td>
                                           <asp:TextBox ID="txtlength"  CssClass="form-control " ReadOnly="true"  Font-Size="Larger"   Height="75px"  runat="server"></asp:TextBox>
                                
                                         </td>
                                 </tr>
                                 <tr style="background-color:darksalmon;">
                                     <th>Update Length:</th>
                                     <td>
                                           <asp:TextBox ID="txttotallength"  CssClass="form-control "  Font-Size="Larger"   Height="75px"  runat="server"></asp:TextBox>
                                
                                         </td>
                                 </tr>
                                 <tr style="background-color:darksalmon;">
                                     <th>Heat No:</th>
                                     <td>
                                           <asp:TextBox ID="txtheatno"  CssClass="form-control "  Font-Size="Larger"   Height="75px"  runat="server"></asp:TextBox>
                                
                                         </td>
                                 </tr>
                                 <tr style="background-color:darksalmon;">
                                     <th>Customer Pipe No:</th>
                                     <td>
                                           <asp:TextBox ID="txtcustpipeno"  CssClass="form-control "  Font-Size="Larger"   Height="75px"  runat="server"></asp:TextBox>
                                
                                         </td>
                                 </tr>
                                 <tr style="background-color:darksalmon;">
                                     <th>Coil No:</th>
                                     <td>
                                           <asp:TextBox ID="txtcoilno"  CssClass="form-control "  Font-Size="Larger"   Height="75px"  runat="server"></asp:TextBox>
                                
                                         </td>
                                 </tr>
                                 <tr style="background-color:darksalmon;">
                                     <th>Hold Reason:</th>
                                     <td>
                                           <asp:TextBox ID="txtholdreason"  CssClass="form-control "  Font-Size="Larger"   Height="75px"  runat="server"></asp:TextBox>
                                
                                         </td>
                                 </tr>
                                
                                
                                
                                
                             </ItemTemplate>
                         </asp:Repeater>
                     </table>
                   </div>
                    </div>
                      <div class="row">
                   
                    <div class="col-md-12 m-2" runat="server" id="divmanualscan" visible="false">
                         <table style="width: 100%">
                             <tr style="background-color:darksalmon;">
                                     <th>Enter Batch No:</th>
                                     <td>
                          
                        <asp:TextBox ID="txtqrcode" ValidationGroup="qrcode" Font-Size="Larger"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>
                    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                            ErrorMessage="Required" ControlToValidate="txtqrcode" ValidationGroup="qrcode"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                            Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                            CssClass="BlockPopup" />--%>
                                         </td>
                                 </tr>
                               <tr style="background-color:darksalmon;">
                                     <th></th>
                                     <td>
                                         OR
                                         </td>
                                   </tr>
                             <tr style="background-color:darksalmon;">
                                     <th>Sequance No:</th>
                                     <td>
                           
                        <asp:TextBox ID="txtsqno" ValidationGroup="qrcode" Font-Size="Larger"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                                 </tr>
                              <tr style="background-color:darksalmon;">
                                     <th>Sale Order:</th>
                                     <td>
                           
                        <asp:TextBox ID="txtsaleorder" ValidationGroup="qrcode" Font-Size="Larger"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>
                                         </td>
                                  </tr>
                              <tr style="background-color:darksalmon;">
                                     <th>Line Item:</th>
                                     <td>
                            
                        <asp:TextBox ID="txtlineitem" ValidationGroup="qrcode" Font-Size="Larger"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>
                                         </td>
                                  </tr>
                             </table>
                    </div>
                           <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="Button1" runat="server" ValidationGroup="qrcode" Font-Size="Larger"  Text="Add Manual" CssClass="btn btn-primary" OnClick="Button1_Click" />
                    </div>
                           </div>
                  <div class="row">
                    <div class="col-md-4 ">
                        <br />
                          <a type="button" class="btn btn-dark" href="ReleaseScan.aspx" style="font-size: 36px; width:200px; height:85px" Font-Size="Larger"  >Back</a>
                        </div>
                    <div class="form-group col-md-6 align-content-end">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Post To SAP" Font-Size="Larger" ValidationGroup="addbatch" CssClass="btn btn-danger" OnClick="btnSubmit_Click" />

                    </div>
                      </div>
            </div>
        </div>
    </div>


</asp:Content>
