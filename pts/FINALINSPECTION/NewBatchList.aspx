<%@ Page Title="" Language="C#" MasterPageFile="~/pts/FINALINSPECTION/FINALINSPECTION.Master" AutoEventWireup="true" CodeBehind="NewBatchList.aspx.cs" Inherits="PTSCOATING.pts.FINALINSPECTION.NewBatchList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSubmit.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $(".Date").datepicker({ dateFormat: 'mm-dd-yy' });
            });

            $('#ContentPlaceHolder1_repDetails_txtLengthft_0').on('keyup', function () {
                let prelblLength = parseFloat($("#ContentPlaceHolder1_repDetails_hiddenLengthft_0").val()) || 0;
                let prelblWeight = parseFloat($("#ContentPlaceHolder1_repDetails_hiddenWeight_0").val()) || 0;
                let txtLength = parseFloat($(this).val()) || 0;
                $('#ContentPlaceHolder1_repDetails_txtWeight_0').attr('ReadOnly', false);
                if (prelblLength !== 0 && prelblWeight !== 0 && txtLength > 0) {
                    let txtWeigth = (prelblWeight / prelblLength) * txtLength;
                    $("#ContentPlaceHolder1_repDetails_hiddenFWeight_0").val(txtWeigth.toFixed(3));
                    $("#ContentPlaceHolder1_repDetails_txtWeight_0").val(txtWeigth.toFixed(3));
                    $('#ContentPlaceHolder1_repDetails_txtWeight_0').attr('ReadOnly', true);
                }
                else {
                    if (this.val() == 0 || this.val() != "")
                        alert("Please Enter valid Length");
                }
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
    <%-- <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/css/bootstrap-multiselect.css">
        <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/js/bootstrap-multiselect.js"></script>
     <script type="text/javascript">
         $(function () {
             $('[id*=lstreasons]').multiselect({
                 includeSelectAllOption: true
             });
         });
     </script>--%>
    <div class="col-md-12">
    </div>
    <div class="col-md-12">
        <div class="tile">
            <h3 class="tile-title">Add New Batch</h3>
            <div class="tile-body">
                <div class="row">
                    <div style="width: 100%;">
                        <table style="width: 100%">
                            <asp:Repeater ID="repDetails" runat="server" OnItemDataBound="repDetails_ItemDataBound">
                                <ItemTemplate>
                                    <tr style="background-color: darksalmon;">
                                        <th>Batch:</th>
                                        <td>
                                            <asp:Literal ID="litqrcode" runat="server" Text='<%#Eval("Code")%>'></asp:Literal>
                                            <asp:TextBox ID="TextBox1" Style="display: none" runat="server" Text='<%#Eval("Code")%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Date:</th>
                                        <td>
                                            <asp:TextBox ID="txtdate" CssClass="form-control " Font-Size="Larger" TextMode="Date" Height="75px" placeholder="DD-MM-YYYY" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Shift:</th>
                                        <td>
                                            <asp:DropDownList ID="ddlshift" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px" Enabled="true">
                                                <asp:ListItem>A</asp:ListItem>
                                                <asp:ListItem>B</asp:ListItem>
                                                <asp:ListItem>C</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Customer Pipe No:</th>
                                        <td>
                                            <asp:TextBox ID="txtcustpipeno" CssClass="form-control" Font-Size="Larger" Height="75px" runat="server" ReadOnly="true"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon; display: none">
                                        <th>Feed Sequence No:</th>
                                        <td>
                                            <asp:TextBox ID="txtFeedSequenceNo" CssClass="form-control" ReadOnly="true" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Heat Code:</th>
                                        <td>
                                            <asp:TextBox ID="txtheatcode" CssClass="form-control" ReadOnly="true" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Heat No:</th>
                                        <td>
                                            <asp:TextBox ID="txtheatdno" CssClass="form-control" ReadOnly="true" Font-Size="Larger" placeholder="Enter Heat No" runat="server" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                ErrorMessage="Required" ControlToValidate="txtheatdno" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Length FT:</th>
                                        <td>
                                            <asp:HiddenField ID="hiddenLengthft" runat="server" />
                                            <asp:HiddenField ID="hiddenFLengthft" runat="server" />
                                            <asp:TextBox ID="txtLengthft" CssClass="form-control " Font-Size="Larger" Height="75px" placeholder="Length FT" runat="server" ReadOnly="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFVtxtLengthft" runat="server" Display="None"
                                                ErrorMessage="Required" ControlToValidate="txtLengthft" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="VCERFVtxtLengthft" runat="server"
                                                Enabled="true" TargetControlID="RFVtxtLengthft" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Weight:</th>
                                        <td>
                                            <asp:HiddenField ID="hiddenWeight" runat="server" />
                                            <asp:HiddenField ID="hiddenFWeight" runat="server" />
                                            <asp:TextBox ID="txtWeight" CssClass="form-control " Font-Size="Larger" Height="75px" placeholder="Weight" runat="server" ReadOnly="true"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;" id="trqcholdat" runat="server" visible="false">
                                        <th>QC HOLD at:</th>
                                        <td>
                                            <asp:Literal ID="litqcholdat" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;" id="trCoatingtype" runat="server" visible="false">
                                        <th>Coating Type:</th>
                                        <td>
                                            <asp:DropDownList ID="ddlcoattype" runat="server" Visible="false">
                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                <asp:ListItem>COAT PIPE</asp:ListItem>
                                                <asp:ListItem>BARE PIPE</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:Literal ID="litcoattype" Visible="false" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;" id="trBarePipeHold" runat="server">
                                        <th>Bare Pipe Hold Reason:</th>
                                        <td>
                                            <asp:Literal ID="litBarePipeHold" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Status:</th>
                                        <td>
                                            <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="Larger" Height="75px" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Shift Supervisor:</th>
                                        <td>
                                            <asp:TextBox ID="txtSHIFTIN" CssClass="form-control" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Shift Inspector:</th>
                                        <td>
                                            <asp:TextBox ID="txtTPINM" CssClass="form-control" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Rack No:</th>
                                        <td>
                                            <asp:TextBox ID="txtRACKNO" CssClass="form-control" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Remarks:</th>
                                        <td>
                                            <asp:TextBox ID="txtRemarks" CssClass="form-control " Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%-- <tr style="background-color:darksalmon;"  id="trReason" runat="server" >
                                     <th>Reason:</th>
                                     <td>
                                         <asp:DropDownList ID="ddlreasons" runat="server" CssClass="form-control"  Font-Size="Larger"  Height="75px">
                                         </asp:DropDownList>
                                     </td>
                                 </tr>--%>
                                    <tr style="background-color: darksalmon;" id="trReason" runat="server">
                                        <th>Reason:</th>
                                        <td>
                                            <asp:ListBox ID="lstreasons" runat="server" Height="75px" SelectionMode="Multiple" Font-Size="Large" CssClass="form-control"></asp:ListBox>

                                        </td>
                                    </tr>

                                    <th>Add Parameters</th>
                                    <td>

                                        <a id="addmore" class="buttonInactive">
                                            <img id="imgdiv" width="30px" border="0" src="plus.gif" />
                                        </a>


                                    </td>
                                    </tr>
                               <tr id="tr1" style="background-color: antiquewhite;">


                                   <th>ADHESION(KNIFE) TEST:</th>
                                   <td>
                                       <asp:TextBox ID="txtParameter1" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                           ErrorMessage="Enter valid integer or decimal number."
                                           ControlToValidate="txtParameter1" />

                                       <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                           Enabled="true" TargetControlID="RegularExpressionValidator6" HighlightCssClass="validatorCalloutHighlight2"
                                           CssClass="BlockPopup" />
                                   </td>
                               </tr>
                                    <tr id="tr2" class="divparameter" style="background-color: antiquewhite;">
                                        <th>COATING THICKNESS 1:</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter2" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                ErrorMessage="Enter valid integer or decimal number."
                                                ControlToValidate="txtParameter2" />

                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                Enabled="true" TargetControlID="RegularExpressionValidator7" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />
                                        </td>
                                    </tr>
                                    <tr id="tr3" style="background-color: antiquewhite;">
                                        <th>COATING THICKNESS 2:</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter3" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                ErrorMessage="Enter valid integer or decimal number."
                                                ControlToValidate="txtParameter3" />

                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                Enabled="true" TargetControlID="RegularExpressionValidator8" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />

                                        </td>
                                    </tr>
                                    <tr id="tr4" style="background-color: antiquewhite;">
                                        <th>COATING THICKNESS 3:</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter4" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                ErrorMessage="Enter valid integer or decimal number."
                                                ControlToValidate="txtParameter4" />

                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                                Enabled="true" TargetControlID="RegularExpressionValidator9" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />

                                        </td>
                                    </tr>
                                    <tr id="tr5" style="background-color: antiquewhite;">
                                        <th>COATING THICKNESS 4:</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter5" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>


                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                ErrorMessage="Enter valid integer or decimal number."
                                                ControlToValidate="txtParameter5" />

                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server"
                                                Enabled="true" TargetControlID="RegularExpressionValidator10" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />

                                        </td>
                                    </tr>
                                    <tr id="tr6" style="background-color: antiquewhite;">
                                        <th>COATING THICKNESS 5:</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter6" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>


                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                ErrorMessage="Enter valid integer or decimal number."
                                                ControlToValidate="txtParameter6" />

                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                Enabled="true" TargetControlID="RegularExpressionValidator11" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />

                                        </td>
                                    </tr>
                                    <tr id="tr7" style="background-color: antiquewhite;">
                                        <th>CUT BACK:</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter7" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>


                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                ErrorMessage="Enter valid integer or decimal number."
                                                ControlToValidate="txtParameter7" />

                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server"
                                                Enabled="true" TargetControlID="RegularExpressionValidator12" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />

                                        </td>
                                    </tr>
                                    <tr id="tr8" style="background-color: antiquewhite;">
                                        <th>HOLIDAY DETECTED AND REPAIRED:</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter8" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>


                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,2})?))$"
                                                ErrorMessage="Enter valid integer or decimal number."
                                                ControlToValidate="txtParameter8" />

                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                Enabled="true" TargetControlID="RegularExpressionValidator13" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />


                                        </td>
                                    </tr>
                                    <tr id="tr9" style="background-color: antiquewhite;">
                                        <th>FBE POWDER:</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter9" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" CssClass="form-control"></asp:TextBox>


                                            <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter9" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator14" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />--%>


                                        </td>
                                    </tr>
                                    <tr id="tr10" style="background-color: antiquewhite;">
                                        <th>FBE POWDER BATCH/LOT :</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter10" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" CssClass="form-control"></asp:TextBox>


                                            <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ValidationGroup="addbatch" runat="server" Display="Dynamic" ValidationExpression="((\d+)((\.\d{1,3})?))$"
                                                  ErrorMessage="Enter valid integer or decimal number."
                                                  ControlToValidate="txtParameter10" />

                                              <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                  Enabled="true" TargetControlID="RegularExpressionValidator15" HighlightCssClass="validatorCalloutHighlight2"
                                                  CssClass="BlockPopup" />--%>


                                        </td>
                                    </tr>
                                    <tr id="tr11" style="background-color: antiquewhite;">
                                        <th>ARO POWDER :</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter11" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" CssClass="form-control"></asp:TextBox>




                                        </td>
                                    </tr>
                                    <tr id="tr12" style="background-color: antiquewhite;">
                                        <th>ARO POWDER BATCH/LOT :</th>
                                        <td>
                                            <asp:TextBox ID="txtParameter12" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" CssClass="form-control"></asp:TextBox>




                                        </td>
                                    </tr>



                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
                <div class="row">

                    <div class="col-md-12 m-2" runat="server" id="divmanualscan" visible="false">
                        <div class="col-md-6">
                            Sale Order<asp:TextBox ID="txtsaleorder" CssClass="form-control" Height="75px" Font-Size="Larger" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            LineItem 
                            <asp:TextBox ID="txtlineitem" CssClass="form-control" Height="75px" Font-Size="Larger" ReadOnly="true" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            Select Batch No:
                        <asp:DropDownList ID="ddlmanualbatch" CssClass="form-control" Height="75px" Font-Size="Larger" Width="400px" runat="server"></asp:DropDownList>
                            <%--   Enter Batch No:  
                        <asp:TextBox ID="txtqrcode" ValidationGroup="qrcode" Font-Size="Larger"  style="text-transform:uppercase"  CssClass="form-control" runat="server"></asp:TextBox>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                ErrorMessage="Required" ControlToValidate="ddlmanualbatch" InitialValue="0" ValidationGroup="qrcode1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                                CssClass="BlockPopup" />
                        </div>
                    </div>
                    <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="Button1" runat="server" ValidationGroup="qrcode1" Font-Size="Larger" Text="Add Manual" CssClass="btn btn-primary" OnClick="Button1_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 ">
                        <br />
                        <a type="button" class="btn btn-dark" href="FinalScan.aspx" style="font-size: 36px; width: 200px; height: 85px" font-size="Larger">Back</a>
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
