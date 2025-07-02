<%@ Page Title="" Language="C#" MasterPageFile="~/pts/FINALINSPECTION/FINALINSPECTION.Master" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="PTSCOATING.pts.FINALINSPECTION.Print" %>
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
            <h3 class="tile-title">Print</h3>
            <div class="tile-body">
                 <div class="row">
                      <div  style="width:100%;">
                     <table style="width: 100%">
                    <tr style="background-color:darksalmon;">
                                     <th>From Batch No:</th>
                                     <td>
                      
                        <asp:TextBox ID="txtfrombatchno" ValidationGroup="qrcode" Font-Size="Larger"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                            ErrorMessage="Required" ControlToValidate="txtfrombatchno" ValidationGroup="qrcode"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                            Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                            CssClass="BlockPopup" />
                    </td>
                        </tr>
                          <tr style="background-color:darksalmon;">
                                <th>To Batch No: </th>
                                     <td>
                         
                        <asp:TextBox ID="txttobatchno" ValidationGroup="qrcode" Font-Size="Larger"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>
                                         </td>
                              </tr>
                       </table>
                          </div>
                       </div>
                     <div class="row">
                    <div class="col-md-6 ">
                        <br />
                          <a type="button" class="btn btn-dark" href="FinalScan.aspx" style="font-size: 36px; width:200px; height:85px" Font-Size="Larger"  >Back</a>
                        </div>
                    <div class="form-group col-md-6 align-content-end">
                        <br />
                      <asp:Button ID="btngetbatch" runat="server" ValidationGroup="qrcode" Font-Size="Larger"  Text="Print" CssClass="btn btn-primary" OnClick="btnprint_Click" />

                    </div>
                      </div>
                       
                         
                 <br />
                     <br />
                    </div>
            </div>
          </div>

</asp:Content>
