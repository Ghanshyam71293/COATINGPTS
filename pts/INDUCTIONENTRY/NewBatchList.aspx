<%@ Page Title="" Language="C#" MasterPageFile="~/pts/INDUCTIONENTRY/INDUCTIONENTRY.Master" AutoEventWireup="true" CodeBehind="NewBatchList.aspx.cs" Inherits="PTSCOATING.pts.INDUCTIONENTRY.NewBatchList" %>


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

        .reasonHide {
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
            <h3 class="tile-title">INDUCTION ENTRY</h3>
            <div class="tile-body" id="#myForm">
                <div class="row">
                    <div style="width: 100%;">
                        <table style="width: 100%">
                            <tr style="background-color: darksalmon;">
                                <th>Date:</th>
                                <td>
                                    <asp:TextBox ID="txtdate" CssClass="form-control " Font-Size="Larger" ReadOnly="true" Height="75px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
<tr style="background-color: darksalmon;">
                                <th>Sale Order:</th>
                                <td>
                                    <asp:TextBox ID="txtSaleOrder" CssClass="form-control" Font-Size="Larger" Height="75px" Required="Required" runat="server" />
                                    <%--<span id="errortxtOD" style="color: red;"></span>--%>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>OD:</th>
                                <td>
                                    <asp:TextBox ID="txtOD" CssClass="form-control" Font-Size="Larger" Height="75px" Required="Required" runat="server" />
                                    <%--<span id="errortxtOD" style="color: red;"></span>--%>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>WT:</th>
                                <td>
                                    <asp:TextBox ID="txtWT" CssClass="form-control" Font-Size="Larger" Height="75px" Required="Required" runat="server"></asp:TextBox>
                                    <%--<span id="errortxtWT" style="color: red;"></span>--%>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Line speed:</th>
                                <td>
                                    <asp:TextBox ID="txtLinespeed" CssClass="form-control"  Font-Size="Larger" Height="75px" Required="Required" runat="server"></asp:TextBox>
                                    <%--<span id="errortxtLinespeed" style="color: red;"></span>--%>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Tap:</th>
                                <td>
                                    <asp:TextBox ID="txtTap" CssClass="form-control" Font-Size="Larger" Height="75px" Required="Required" runat="server"></asp:TextBox>
                                    <%--<span id="errortxtTap" style="color: red;"></span>--%>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Frequency:</th>
                                <td>
                                    <asp:TextBox ID="txtFrequency" CssClass="form-control" Font-Size="Larger" Height="75px" Required="Required" runat="server"></asp:TextBox>
                                    <%--<span id="errortxtFrequency" style="color: red;"></span>--%>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Coil Voltage:</th>
                                <td>
                                    <asp:TextBox ID="txtCoilvoltage" CssClass="form-control" Font-Size="Larger" Height="75px" Required="Required" runat="server"></asp:TextBox>
                                    <%--<span id="errortxtCoilvoltage" style="color: red;"></span>--%>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>DC Bus Voltage:</th>
                                <td>
                                    <asp:TextBox ID="txtDcBusvoltage" CssClass="form-control"  Font-Size="Larger" Height="75px" Required="Required" runat="server"></asp:TextBox>
                                    <%--<span id="errortxtDcBusvoltage" style="color: red;"></span>--%>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Coating Type:</th>
                                <td>
                                    <asp:DropDownList ID="txtCoatingType" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px">
                                        <asp:ListItem>FBE</asp:ListItem>
                                        <asp:ListItem>ARO</asp:ListItem>
                                        <asp:ListItem>FBE+ARO</asp:ListItem>
                                        <asp:ListItem>BARE</asp:ListItem>
                                        <asp:ListItem>LACQUER</asp:ListItem>
                                        <asp:ListItem>ZAP WRAP</asp:ListItem>
                                        <asp:ListItem>STRIPPING</asp:ListItem>
                                        <asp:ListItem>END CLEANING</asp:ListItem>
                                        <asp:ListItem>STRIPPING & FBE</asp:ListItem>
                                        <asp:ListItem>STRIPPING & RE-LACQUER</asp:ListItem>
                                        <asp:ListItem>MACRO EPOXY</asp:ListItem>
                                        <asp:ListItem>STRIPPING & ARO</asp:ListItem>
                                        <asp:ListItem>CUT & RE-BEVEL</asp:ListItem>
                                        <asp:ListItem>BLAST & RE-LACQUER</asp:ListItem>
                                        <asp:ListItem>END CAPS</asp:ListItem>
                                        <asp:ListItem>ID BLASTING</asp:ListItem>
                                        <asp:ListItem>OD CLEANING</asp:ListItem>
                                        <asp:ListItem>STENCIL</asp:ListItem>
                                        <asp:ListItem>FBE GOLD</asp:ListItem>
                                        <asp:ListItem>ID JSU3</asp:ListItem>
                                        <asp:ListItem>ID JSU5</asp:ListItem>
                                        <asp:ListItem>JSU-3</asp:ListItem>
                                        <asp:ListItem>NONE</asp:ListItem>
                                        <asp:ListItem>JSU-5</asp:ListItem>
                                        <asp:ListItem>BURN OFF</asp:ListItem>
                                        <asp:ListItem>3LPE</asp:ListItem>
                                        <asp:ListItem>3LPP</asp:ListItem>
                                        <asp:ListItem>MRO</asp:ListItem>
                                        <asp:ListItem>STRIPPING & ZAP WRAP</asp:ListItem>
                                        <asp:ListItem>FBE+ROUGHCOAT</asp:ListItem>
                                        <asp:ListItem>FBE+UV</asp:ListItem>
                                        <asp:ListItem>ID FLOWLINE</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Machine Status:</th>
                                <td>
                                    <asp:DropDownList ID="txtMachineStatus" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px">
                                        <asp:ListItem>Running</asp:ListItem>
                                        <asp:ListItem>Not Running</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Remark: :</th>
                                <td>
                                    <asp:TextBox ID="txtRemark" CssClass="form-control" TextMode="MultiLine" Font-Size="Larger" Height="75px" Required="Required" runat="server" onkeydown="return blockEnter(event);"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="form-group col-md-12 mt-5 text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Post To SAP" Font-Size="Larger" ValidationGroup="addbatch" CssClass="btn btn-danger" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script>
    $(document).ready(function () {
        $('input[type="text"]').on('input', function () {
            var value = $(this).val();
            var valid = value.match(/^\d*(\.\d{0,3})?$/);
            if (!valid) {
                $(this).val(value.slice(0, -1));
            }
        });
    });
    function blockEnter(e) {
        if (e.key === "Enter" || e.keyCode === 13) {
            return false; // Prevent the default action
        }
        return true;
    }
</script>

</asp:Content>
