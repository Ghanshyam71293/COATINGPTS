<%@ Page Title="" Language="C#" MasterPageFile="~/pts/FINALINSPECTION/FINALINSPECTION.Master" AutoEventWireup="true" CodeBehind="Saleorderbatch.aspx.cs" Inherits="PTSCOATING.pts.FINALINSPECTION.Saleorderbatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="col-md-12">
        <div class="tile">
            <h3 class="tile-title">Get Next Batch</h3>
            <div class="tile-body">
                 <div class="row">
                   
                    <div class="col-md-12 m-2">
                         Sale Order No:  
                        <asp:TextBox ID="txtsaleorder" ValidationGroup="qrcode" Font-Size="Larger"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                            ErrorMessage="Required" ControlToValidate="txtsaleorder" ValidationGroup="qrcode"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                            Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                            CssClass="BlockPopup" />
                    </div>
                      <div class="col-md-12 m-2">
                         SO Line Item:  
                        <asp:TextBox ID="txtlineitem" ValidationGroup="qrcode" Font-Size="Larger" Text="100"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                            ErrorMessage="Required" ControlToValidate="txtlineitem" ValidationGroup="qrcode"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                            Enabled="true" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight2"
                            CssClass="BlockPopup" />
                    </div>
                           <div class="col-md-4">
                        <br />
                        <asp:Button ID="btngetbatch" runat="server" ValidationGroup="qrcode" Font-Size="Larger"  Text="Get Batch" CssClass="btn btn-primary" OnClick="btngetbatch_Click" />
                    </div>
                           </div>
                 <br />
                     <br />
                    
                <div class="row">
                   
                      <div  style="width:100%;">
                     <table style="width: 100%">
                         <asp:Repeater ID="repDetails" runat="server" OnItemDataBound="repDetails_ItemDataBound">
                             <ItemTemplate>
                                 <tr style="background-color:darksalmon;">
                                     <th>Batch:</th>
                                     <td>
                                         <asp:Literal ID="litqrcode" runat="server" Text='<%#Eval("Code")%>'></asp:Literal>
                                         <asp:TextBox ID="TextBox1" Style="display: none" runat="server" Text='<%#Eval("Code")%>'></asp:TextBox>
                                     </td>
                                 </tr>
                            </ItemTemplate>
                         </asp:Repeater>
                         </table>
                          </div>
                      <div class="row">
                    <div class="col-md-6 ">
                        <br />
                          <a type="button" class="btn btn-dark" href="FinalScan.aspx"  style="font-size: 36px; width:200px; height:85px" Font-Size="Larger"  >Back</a>
                        </div>
                    <div class="form-group col-md-6 align-content-end">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Entry Post" Font-Size="Larger" Visible="false" ValidationGroup="addbatch" CssClass="btn btn-danger" OnClick="btnSubmit_Click" />

                    </div>
                      </div>


                    </div>
                </div>
            </div>
         </div>

</asp:Content>
