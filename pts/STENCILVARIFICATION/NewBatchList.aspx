<%@ Page Title="" Language="C#" MasterPageFile="~/pts/STENCILVARIFICATION/StencilVarification.Master" AutoEventWireup="true" CodeBehind="NewBatchList.aspx.cs" Inherits="PTSCOATING.pts.STENCILVARIFICATION.NewBatchList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/css/bootstrap-multiselect.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.13/js/bootstrap-multiselect.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=txtReason]').multiselect({
                includeSelectAllOption: true
            });
        });
        $(function () {
            $('[id*=SV_txtReason]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>

    <style>
        .ListSelect .btn-group {
            width: 100%;
            height: 57px;
            background-color: #fff;
        }

        .ListSelect .multiselect-container.dropdown-menu {
            height: 200px;
            width: 100%;
            background: rgb(255, 255, 255);
            overflow-y: scroll;
        }

        .ListSelect .multiselect.dropdown-toggle {
            text-align: left;
        }

        .ListSelect .multiselect-selected-text {
            font-size: 30px;
        }

        .ListSelect .dropdown-toggle::after {
            position: absolute;
            right: 13px;
            top: 26px;
        }
        .reasonHide{
            width: 100%;
    position: absolute;
    top: 0;
    left: 0;
    height: 100%;
    background-color: #dddddd7a;
        }
    </style>
    <script>     
        function AalertFunction() {
            if (confirm('Are you sure you want to batch hold?')) {
                return;
            } else {
                return false;
            }
        }
    </script>

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
                    <div style="width: 100%;">
                        <table style="width: 100%">
                            <asp:Repeater ID="repDetails" runat="server" OnItemDataBound="repDetails_ItemDataBound">

                                <ItemTemplate>
                                    <tr style="background-color: darksalmon;">
                                        <th>Batch:</th>
                                        <td>
                                            <asp:Literal ID="litsqno" runat="server" Visible="false" Text='<%#Eval("SQNO")%>'></asp:Literal>
                                            <asp:Literal ID="litsaleorder" runat="server" Visible="false" Text='<%#Eval("SaleOrder")%>'></asp:Literal>
                                            <asp:Literal ID="litlineitem" runat="server" Visible="false" Text='<%#Eval("LineItem")%>'></asp:Literal>

                                            <asp:Literal ID="litqrcode" runat="server" Text='<%#Eval("Code")%>'></asp:Literal>
                                            <asp:TextBox ID="TextBox1" Style="display: none" runat="server" Text='<%#Eval("Code")%>'></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr style="background-color: darksalmon;">
                                        <th>Date:</th>
                                        <td>
                                            <asp:TextBox ID="txtdate" CssClass="form-control " Font-Size="Larger" TextMode="Date" Height="75px" placeholder="DD-MM-YYYY" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                                ErrorMessage="Required" ControlToValidate="txtdate" ValidationGroup="addbatch">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                Enabled="true" TargetControlID="RequiredFieldValidator5" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Shift:</th>
                                        <td>
                                            <asp:DropDownList ID="ddlshift" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px">
                                                <asp:ListItem>A</asp:ListItem>
                                                <asp:ListItem>B</asp:ListItem>
                                                <asp:ListItem>C</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Sale Order:</th>
                                        <td>
                                            <asp:TextBox ID="txtSaleOrder" CssClass="form-control " ReadOnly="true" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Line Item:</th>
                                        <td>
                                            <asp:TextBox ID="txtLineItem" CssClass="form-control " ReadOnly="true" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Sequence No:</th>
                                        <td>
                                            <asp:TextBox ID="txtSequence" CssClass="form-control " ReadOnly="true" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>SV Sequence No:</th>
                                        <td>
                                            <asp:TextBox ID="SV_txtSequence" CssClass="form-control" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Heat No:</th>
                                        <td>
                                            <asp:TextBox ID="txtheatno" CssClass="form-control " ReadOnly="true" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>SV Heat No:</th>
                                        <td>
                                            <asp:TextBox ID="SV_txtheatno" CssClass="form-control" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Customer Pipe No:</th>
                                        <td>
                                            <asp:TextBox ID="txtcustpipeno" CssClass="form-control " ReadOnly="true" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>SV Customer Pipe No:</th>
                                        <td>
                                            <asp:TextBox ID="SV_txtcustpipeno" CssClass="form-control " Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Length:</th>
                                        <td>
                                            <asp:TextBox ID="txtLength" CssClass="form-control " ReadOnly="true" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>SV Length:</th>
                                        <td>
                                            <asp:TextBox ID="SV_txtLength" CssClass="form-control" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr style="background-color: darksalmon;">
                                        <th>Status:</th>
                                        <td>
                                            <asp:TextBox ID="textStatus" CssClass="form-control " ReadOnly="true" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>SV Batch Status:</th>
                                        <td>
                                            <asp:TextBox ID="SV_textStatus" CssClass="form-control" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%--<tr style="background-color: darksalmon;">
                                        <th>Reason:</th>
                                        <td class="ListSelect">
                                            <asp:TextBox runat="server" ID="txtReason" CssClass="form-control" ReadOnly="true" Font-Size="Larger" Height="75px"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr style="background-color: darksalmon;">
                                        <th>Hold Reason:</th>
                                        <td class="ListSelect" style="position:relative;">
                                            <asp:ListBox runat="server" ID="txtReason" SelectionMode="Multiple" CssClass="form-control" ReadOnly="true" Font-Size="Larger" Height="75px"></asp:ListBox>
                                            <div class="reasonHide"></div>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>SV Hold Reason:</th>
                                        <td class="ListSelect">
                                            <asp:ListBox ID="SV_txtReason" runat="server" SelectionMode="Multiple" CssClass="form-control" Font-Size="Larger" Height="75px"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr style="background-color: darksalmon;">
                                        <th>Rack No:</th>
                                        <td class="ListSelect">
                                            <asp:TextBox ID="RackNo" CssClass="form-control" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr style="background-color: darksalmon;">
                                        <th>Remarks:</th>
                                        <td>
                                            <asp:TextBox ID="txtRemarks" CssClass="form-control " Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
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
                            <tr style="background-color: darksalmon;">
                                <th>Enter Batch No:</th>
                                <td>

                                    <asp:TextBox ID="txtqrcode" ValidationGroup="qrcode" Font-Size="Larger" Style="text-transform: uppercase" CssClass="form-control" runat="server"></asp:TextBox>
                                    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                            ErrorMessage="Required" ControlToValidate="txtqrcode" ValidationGroup="qrcode"></asp:RequiredFieldValidator>
                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                            Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                            CssClass="BlockPopup" />--%>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th></th>
                                <td>OR
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Sequance No:</th>
                                <td>

                                    <asp:TextBox ID="txtsqno" ValidationGroup="qrcode" Font-Size="Larger" Style="text-transform: uppercase" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Sale Order:</th>
                                <td>

                                    <asp:TextBox ID="txtsaleorder" ValidationGroup="qrcode" Font-Size="Larger" Style="text-transform: uppercase" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Line Item:</th>
                                <td>

                                    <asp:TextBox ID="txtlineitem" ValidationGroup="qrcode" Font-Size="Larger" Style="text-transform: uppercase" CssClass="form-control" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-4 ">
                        <br />
                        <asp:Button ID="Button1" runat="server" ValidationGroup="qrcode" Font-Size="Larger" Text="Add Manual" CssClass="btn btn-primary" OnClick="Button1_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 ">
                        <br />
                        <a type="button" class="btn btn-dark" href="StencilVarificationScan.aspx" style="font-size: 36px; width: 200px; height: 85px" font-size="Larger">Back</a>
                    </div>
                    <div class="form-group col-md-6 align-content-end">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Batch Hold" Font-Size="Larger" ValidationGroup="addbatch" CssClass="btn btn-danger" OnClick="btnSubmit_Click" OnClientClick="return AalertFunction();" />


                        <asp:Button ID="VerifyBtn" runat="server" Text="Verify" Style="display: none"  Font-Size="Larger" ValidationGroup="addbatch" CssClass="btn btn-success" OnClick="VerifyBtn_Click" />

                    </div>
                </div>
            </div>
        </div>
    </div>




    <script>
        var hReason = $('#ContentPlaceHolder1_repDetails_SV_txtReason_0').val();

        if (hReason === null || hReason == "") {
            $('#ContentPlaceHolder1_VerifyBtn').css("display", "inline-block");
        }
        else {
            $('#ContentPlaceHolder1_VerifyBtn').css("display", "none");
        }

        $("#ContentPlaceHolder1_repDetails_SV_txtReason_0").change(function () {
            var svHold = $('#ContentPlaceHolder1_repDetails_SV_txtReason_0').val();

            if (svHold == null || svHold == "") {
                $('#ContentPlaceHolder1_VerifyBtn').css("display", "inline-block");
            }
            else {
                $('#ContentPlaceHolder1_VerifyBtn').css("display", "none");

            }
        })
    </script>



</asp:Content>
