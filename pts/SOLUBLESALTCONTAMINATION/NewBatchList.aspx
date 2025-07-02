<%@ Page Title="" Language="C#" MasterPageFile="~/pts/SOLUBLESALTCONTAMINATION/SOLUBLESALTCONTAMINATION.Master" AutoEventWireup="true" CodeBehind="NewBatchList.aspx.cs" Inherits="PTSCOATING.pts.SOLUBLESALTCONTAMINATION.NewBatchList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $(".Date").datepicker({ dateFormat: 'mm-dd-yy' });
            });
        });
    </script>


    <style>
        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
            font-size: 16PX;
            text-align: center;
        }

        th {
          /*color:#fff;            */
        }

        .buttonActive {
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

        .app-header__logo b {
            font-size: 23px;
            line-height: 1;
            margin-top: 28px;
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
                    $("#imgdiv").attr("src", "plus.gif");
                }
            });

            $("#addmoreGauge").click(function () {
                $('.buttonInactive').not(this).removeClass('buttonInactive');
                $(this).toggleClass('buttonActive');
                if ($(this).hasClass("buttonActive")) {
                    $("#imgdivGauge").attr("src", "minus.gif");
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
                    $("#imgdivGauge").attr("src", "plus.gif");
                }
            });

        });
    </script>
    <div class="col-md-12">
    </div>
    <div class="col-sm-8 offset-sm-2 col-md-8 offset-md-2">
        <div class="tile">
            <h3 class="tile-title">Add New Batch</h3>
            <div class="tile-body">
                <div class="row">
                    <div style="width: 100%;">
                        <table style="width: 100%">
                            <tr style="background-color: darksalmon;">
                                <th>Sales Order:</th>
                                <td>
                                    <asp:TextBox ID="txtSalesOrder" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="txtSalesOrder" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Shift:</th>
                                <td>
                                    <asp:DropDownList ID="ddlshift" runat="server" CssClass="form-control" Font-Size="Larger">
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>B</asp:ListItem>
                                         <asp:ListItem>C</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Batch No</th>
                                <td>
                                    <asp:TextBox ID="textBatch" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="textBatch" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>
                            <%--<tr style="background-color: darksalmon;">
                                <th>Customer</th>
                                <td>
                                    <asp:TextBox ID="customerid" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="customerid" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>--%>
                            <tr style="background-color: darksalmon;">
                                <th>Date</th>
                                <td>
                                    <asp:TextBox ID="txtDate" TextMode="Date" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="txtDate" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator3" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Frequency</th>
                                <td>
                                    <asp:TextBox ID="txtFrequency" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="txtFrequency" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator4" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>
                            <%--<tr style="background-color: darksalmon;">
                                <th>Size</th>
                                <td>
                                    <asp:TextBox ID="txtSize" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="txtSize" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator5" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>--%>
                           <%-- <tr style="background-color: darksalmon;">
                                <th>Plant</th>
                                <td>
                                    <asp:TextBox ID="textPlant" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="textPlant" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator6" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>--%>
                            <%--<tr style="background-color: darksalmon;">
                                <th>Pipe/Heat No.</th>
                                <td>
                                    <asp:TextBox ID="txtPipeHeat" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="txtPipeHeat" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator6" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>--%>
                            <tr style="background-color: darksalmon;">
                                <th>Time</th>
                                <td>
                                    <asp:TextBox ID="txtTime" TextMode="Time" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Heat Code</th>
                                <td>
                                    <asp:TextBox ID="txtHeatCode" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="txtHeatCode" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator9" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Potassium Ferricynade Paper Text</th>
                                <td>
                                    <asp:TextBox ID="txtPotassium" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="txtPotassium" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator10" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Quanlitative Test</th>
                                <td>
                                    <asp:TextBox ID="txtQuantitative" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="None"
                                        ErrorMessage="Required" ControlToValidate="txtQuantitative" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                        Enabled="true" TargetControlID="RequiredFieldValidator11" HighlightCssClass="validatorCalloutHighlight2"
                                        CssClass="BlockPopup" />
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Remarks</th>
                                <td>
                                    <asp:TextBox ID="txtRemarks" CssClass="form-control" Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>

                <div class="row">
                    <div class="col-md-12 text-center">
                        <a type="button" class="btn btn-dark" href="../homepage.aspx" style="font-size: 18px; width: 150px; height: 42px;">Back</a>
                        <asp:Button ID="btnSubmit" runat="server" Text="Post To SAP" Style="font-size: 18px; width: 150px; height: 42px;" ValidationGroup="addbatch" CssClass="btn btn-danger" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
