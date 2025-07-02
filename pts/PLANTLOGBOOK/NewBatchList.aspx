<%@ Page Title="" Language="C#" MasterPageFile="~/pts/PLANTLOGBOOK/PLANTLOGBOOK.Master" AutoEventWireup="true" CodeBehind="NewBatchList.aspx.cs" Inherits="PTSCOATING.pts.PLANTLOGBOOK.NewBatchList" %>


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
            <h3 class="tile-title">PLANT LOG BOOK</h3>
            <div class="tile-body" id="#myForm">
                <div class="row">
                    <div style="width: 100%;">
                        <table style="width: 100%">
                            <tr style="background-color: darksalmon;">
                                <th>Maintenance type :</th>
                                <td>
                                    <asp:DropDownList ID="txtBREAKSHUT" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px">
                                        <asp:ListItem>BREAKDOWN</asp:ListItem>
                                        <asp:ListItem>SHUTDOWN</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Plant :</th>
                                <td>
                                    <asp:DropDownList ID="txtPlant" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px">
                                        <asp:ListItem>BT01 </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Start Date :</th>
                                <td>
                                    <asp:TextBox ID="txtStartdate" Required="Required" ClientIDMode="Static" CssClass="form-control" TextMode="Date" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Start Time :</th>
                                <td>
                                    <asp:TextBox ID="txtStartTime" Required="Required" ClientIDMode="Static" CssClass="form-control" TextMode="Time" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>End Date :</th>
                                <td>
                                    <asp:TextBox ID="txtEndDate" Required="Required" ClientIDMode="Static" CssClass="form-control" TextMode="Date" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>End Time :</th>
                                <td>
                                    <asp:TextBox ID="txtEndTime" Required="Required" ClientIDMode="Static" CssClass="form-control" TextMode="Time" Font-Size="Larger" Height="75px" runat="server"></asp:TextBox>
                                </td>
                            </tr>

                            <tr style="background-color: darksalmon;">
                                <th>Department :</th>
                                <td>
                                    <asp:DropDownList ID="txtDepartment" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px">
                                        <asp:ListItem>MAINTENANCE</asp:ListItem>
                                        <asp:ListItem>PRODUCTION</asp:ListItem>
                                        <asp:ListItem>IT</asp:ListItem>
                                        <asp:ListItem>STORES</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr style="background-color: darksalmon;">
                                <th>Location:</th>
                                <td>
                                    <asp:DropDownList ID="txtLocation" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px">
                                        <asp:ListItem>OD1</asp:ListItem>
                                        <asp:ListItem>OD2</asp:ListItem>
                                        <asp:ListItem>OD3</asp:ListItem>
                                        <asp:ListItem>DRILL PIPE</asp:ListItem>
                                        <asp:ListItem>ID1</asp:ListItem>
                                        <asp:ListItem>ID2</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Description :</th>
                                <td>
                                    <asp:TextBox ID="txtRemark" CssClass="form-control" Font-Size="Larger" Height="75px" Required="Required" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="background-color: darksalmon;">
                                <th>Entered by :</th>
                                <td>
                                    <asp:TextBox ID="txtEnterdBy" CssClass="form-control" Font-Size="Larger" Height="75px" Required="Required" runat="server"></asp:TextBox>
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script>
        $('#txtStartdate').on('change', function () {
            var txtStartdate = new Date($('#txtStartdate').val());
            var txtEndDate = new Date($('#txtEndDate').val());
            if (txtEndDate < txtStartdate) {
                alert("End date cannot be before start date.");
                $('#txtEndDate').val('');
                e.preventDefault();
            }
        });
        $('#txtEndDate').on('change', function () {
            var txtStartdate = new Date($('#txtStartdate').val());
            var txtEndDate = new Date($('#txtEndDate').val());
            if (txtEndDate < txtStartdate) {
                alert("End date cannot be before start date.");
                $('#txtEndDate').val('');
                e.preventDefault();
            }
        });

        $('#txtEndTime').on('change', function (e) {
            const start = $('#txtStartTime').val();
            const end = $('#txtEndTime').val();

            const startTime = new Date(`1970-01-01T${start}:00`);
            const endTime = new Date(`1970-01-01T${end}:00`);
            var txtStartdate = new Date($('#txtStartdate').val());
            var txtEndDate = new Date($('#txtEndDate').val());
            if (txtStartdate != "Invalid Date") {
                if (txtStartdate = txtEndDate) {
                    if (endTime < startTime) {
                        alert("End time cannot be before start time.");
                        $('#txtEndTime').val('');
                        e.preventDefault();
                    }
                }
            }
            else {
                alert("Please select start date.");
                $('#txtStartTime').val('');
                $('#txtEndTime').val('');
            }
        });

    </script>

</asp:Content>
