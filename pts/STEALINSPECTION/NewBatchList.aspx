<%@ Page Title="" Language="C#" MasterPageFile="~/pts/STEALINSPECTION/STEALINSPECTION.Master" AutoEventWireup="true" CodeBehind="NewBatchList.aspx.cs" Inherits="PTSCOATING.pts.STEALINSPECTION.NewBatchList" %>
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

    .buttonActive
    {
        background-color: #1E78AB;
        border: 1px solid #1E78AB;
        color: #fff;
    }
    .buttonInactive
    {
        background-color: #fff;
        border: 1px solid #1E78AB;
        color: #1E78AB;
    }

</style>
   
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#tr1").hide();
            $("#tr2").hide();
            $("#tr3").hide();
            $("#tr4").hide();
            $("#tr5").hide();
            $("#tr6").hide();
            $("#tr7").hide();
            $("#tr8").hide();
            $("#tr9").hide();
            $("#tr10").hide();
            $("#tr11").hide();
            $("#tr12").hide();
            $("#tr13").hide();
            $("#tr14").hide();
            $("#tr15").hide();
            $("#tr16").hide();
            $("#tr17").hide();
            $("#tr18").hide();
            $("#imgdiv").attr("src", "plus.gif");
            $("#addmore").click(function () {
                $('.buttonInactive').not(this).removeClass('buttonInactive');
                $(this).toggleClass('buttonActive');
                if ($(this).hasClass("buttonActive")) {
                    $("#imgdiv").attr("src", "minus.gif");
                    $("#tr1").show();
                    $("#tr2").show();
                    $("#tr3").show();
                    $("#tr4").show();
                    $("#tr5").show();
                    $("#tr6").show();
                    $("#tr7").show();
                    $("#tr8").show();
                    $("#tr9").show();
                    $("#tr10").show();
                    $("#tr11").show();
                    $("#tr12").show();
                    $("#tr13").show();
                    $("#tr14").show();
                    $("#tr15").show();
                    $("#tr16").show();
                    $("#tr17").show();
                    $("#tr18").show();

                }
                else {
                    $("#tr1").hide();
                    $("#tr2").hide();
                    $("#tr3").hide();
                    $("#tr4").hide();
                    $("#tr5").hide();
                    $("#tr6").hide();
                    $("#tr7").hide();
                    $("#tr8").hide();
                    $("#tr9").hide();
                    $("#tr10").hide();
                    $("#tr11").hide();
                    $("#tr12").hide();
                    $("#tr13").hide();
                    $("#tr14").hide();
                    $("#tr15").hide();
                    $("#tr16").hide();
                    $("#tr17").hide();
                    $("#tr18").hide();
                    $("#imgdiv").attr("src", "plus.gif");
                }
            });

        });
    </script>
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
                                         <asp:Literal ID="litqrcode" runat="server" Text='<%#Eval("Code")%>'></asp:Literal>
                                         <asp:TextBox ID="TextBox1" Style="display: none" runat="server" Text='<%#Eval("Code")%>'></asp:TextBox>
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
                                 <tr style="background-color:darksalmon;" id="trqcholdat" runat="server" visible="false"  >
                                     <th>QC HOLD at:</th>
                                     <td>
                                         <asp:Literal ID="litqcholdat" runat="server"></asp:Literal>
                                     </td>
                                 </tr>
                                <tr style="background-color:darksalmon;">
                                     <th>Status:</th>
                                     <td>
                                         <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="form-control"  Font-Size="Larger"  Height="75px" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                         
                                         </asp:DropDownList>
                                     </td>
                                 </tr>
                                  <tr style="background-color:darksalmon;">
                                     <th>Reason:</th>
                                     <td>
                                         <asp:DropDownList ID="ddlreasons" runat="server" CssClass="form-control"  Font-Size="Larger"  Height="75px">
                                         </asp:DropDownList>
                                     </td>
                                 </tr>
                                          <tr style="background-color:burlywood;">
                                     <th>Add Parameters</th>
                                     <td>

                                      <a id="addmore" class="buttonInactive" >
<img id="imgdiv" width="30px" border="0" src="plus.gif" />
</a>
                                     
                                           
                                     </td>
                                 </tr>
                               <tr  id="tr1" style="background-color: antiquewhite;">


                                          <th>ANCHOR PATTERN PROFILE:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter1" runat="server" style="text-transform:uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>
                                     
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter1" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator6" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />
                                          </td>
                                      </tr>
                                      <tr  id="tr2" class="divparameter" style="background-color: antiquewhite;">
                                          <th>BLAST LINE SPEED:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter2" runat="server" style="text-transform:uppercase" Font-Size="Larger"  Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>
                                    
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter2" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator7" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />
                                      </td>
                                      </tr>
                                      <tr  id="tr3"  style="background-color: antiquewhite;">
                                          <th>BLAST MACHINE 1 AMPS:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter3" runat="server"  style="text-transform:uppercase" Font-Size="Larger"  Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter3" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator8" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />

                                          </td>
                                      </tr>
                                      <tr id="tr4" style="background-color: antiquewhite;">
                                          <th>BLAST MACHINE 2 AMPS:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter4" runat="server" style="text-transform:uppercase" Font-Size="Larger"  Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>
                                                                       
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter4" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator9" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />

                                          </td>
                                      </tr>
                                      <tr id="tr5" style="background-color: antiquewhite;">
                                          <th>BLAST MACHINE 3 AMPS:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter5" runat="server" style="text-transform:uppercase" Font-Size="Larger"  Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                                                       
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter5" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator10" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />

                                          </td>
                                      </tr>
                                      <tr id="tr6" style="background-color: antiquewhite;">
                                          <th>CLEANLINESS OF BLASTED PIPE:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter6" runat="server" style="text-transform:uppercase" Font-Size="Larger"  Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                                                      
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter6" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator11" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />

                                          </td>
                                      </tr>
                                      <tr id="tr7" style="background-color: antiquewhite;">
                                          <th>DEW POINT TEMPERATURE:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter7" runat="server" style="text-transform:uppercase" Font-Size="Larger"  Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                                                    
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter7" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator12" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />

                                          </td>
                                      </tr>
                                      <tr id="tr8" style="background-color: antiquewhite;">
                                          <th>HUMIDITY:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter8" runat="server" style="text-transform:uppercase" Font-Size="Larger"  Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                                          
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter8" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator13" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />


                                          </td>
                                      </tr>
                                      <tr id="tr9" style="background-color: antiquewhite;">
                                          <th>PIPE PRE-BLAST SURFACE:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter9" runat="server" style="text-transform:uppercase" Font-Size="Larger"  Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                                  
                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter9" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator14" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />

                                          </td>
                                      </tr>
                                      <tr id="tr10" style="background-color: antiquewhite;">
                                          <th>PIPE SURFACE CONDITION AT:</th>
                                          <td>
                                              <asp:TextBox ID="txtParameter10" runat="server" style="text-transform:uppercase" Font-Size="Larger"  Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>


                                               <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter10" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator15" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />

                                          </td>
                                      </tr>
                                
                             </ItemTemplate>
                         </asp:Repeater>
                     </table>
                   </div>
                    </div>
                      <div class="row">
                   
                    <div class="col-md-12 m-2" runat="server" id="divmanualscan" visible="false">
                        Enter Batch No:  
                        <asp:TextBox ID="txtqrcode" ValidationGroup="qrcode" Font-Size="Larger"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                            ErrorMessage="Required" ControlToValidate="txtqrcode" ValidationGroup="qrcode"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                            Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                            CssClass="BlockPopup" />
                    </div>
                           <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="Button1" runat="server" ValidationGroup="qrcode" Font-Size="Larger"  Text="Add Manual" CssClass="btn btn-primary" OnClick="Button1_Click" />
                    </div>
                           </div>
                  <div class="row">
                    <div class="col-md-4 ">
                        <br />
                          <a type="button" class="btn btn-dark" href="StealInsScan.aspx" style="font-size: 36px; width:200px; height:85px" Font-Size="Larger"  >Back</a>
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
