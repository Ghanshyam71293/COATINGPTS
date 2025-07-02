<%@ Page Title="" Language="C#" MasterPageFile="~/pts/FINALINSPECTION/FINALINSPECTION.Master" AutoEventWireup="true" CodeBehind="Hold.aspx.cs" Inherits="PTSCOATING.pts.FINALINSPECTION.Hold" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <div class="tile">
            <h3 class="tile-title">Add New Batch</h3>
            <div class="tile-body">

                <div class="row">

                    <div style="width: 100%;">
                        <table style="width: 100%">
                            <tr style="background-color: darksalmon;">
                                <th>Enter Batch No:</th>
                                <td>
                                    <asp:TextBox ID="txtqrcode" ValidationGroup="qrcode" Font-Size="Larger" Style="text-transform: uppercase" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="txtqrcode"  ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>
                             <tr style="background-color:darksalmon;">
                                     <th>Date:</th>
                                     <td>
                                           <asp:TextBox ID="txtdate"  CssClass="form-control "  Font-Size="Larger" TextMode="Date"  Height="75px" placeholder="DD-MM-YYYY" runat="server"></asp:TextBox>
                                 
                                         </td>
                                 </tr>
                                 <tr style="background-color:darksalmon;">
                                     <th>Shift:</th>
                                     <td>
                                         <asp:DropDownList ID="ddlshift" runat="server" CssClass="form-control"  Font-Size="Larger"  Height="75px">
                                             <asp:ListItem>A</asp:ListItem>
                                             <asp:ListItem>B</asp:ListItem>
                                             <asp:ListItem>C</asp:ListItem>
                                         </asp:DropDownList>
                                     </td>
                                 </tr>
                        </table>

                    </div>
                </div>


                <div class="row">
                    <div class="col-md-4 ">
                        <br />
                        <a type="button" class="btn btn-dark" href="FinalScan.aspx" style="font-size: 36px; width: 200px; height: 85px" font-size="Larger">Back</a>
                    </div>
                    <div class="form-group col-md-6 align-content-end">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Post To SAP" Font-Size="Larger" ValidationGroup="addbatch" CssClass="btn btn-danger" OnClick="btnSubmit_Click"  />

                    </div>
                </div>
            </div>
        </div>

    </div>


</asp:Content>
