<%@ Page Title="" Language="C#" MasterPageFile="~/pts/Release/Release.Master" AutoEventWireup="true" CodeBehind="MultipleRelease.aspx.cs" Inherits="PTSCOATING.pts.Release.MultipleRelease" %>

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
                    <div class="col-md-12 m-2" runat="server" id="divmanualscan">
                        <table style="width: 100%">
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
                        <asp:Button ID="Button1" runat="server" ValidationGroup="qrcode" Font-Size="Larger" Text="Get Details" CssClass="btn btn-primary" OnClick="Button1_Click" />
                    </div>
                </div>
                <div class="row">
                    <div style="width: 100%;">
                         <asp:GridView ID="TableDetails" runat="server"  AutoGenerateColumns="False" DataKeyNames="SEL" CssClass="table table-bordered" Style="width: 100%; font-size: 14px;">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <HeaderTemplate>
                                        <div class="checkBoxDiv" style="white-space: nowrap; position: relative;">
                                            <span>Select All</span>
                                            <input type="checkbox" id="chkAll" onclick="toggleSelectAll(this);" style="position: absolute; left: 23px; top: 18px;" />
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="rowCheckbox" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Batch No" HeaderStyle-Width="124px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CHARG" ReadOnly="true" runat="server" Text='<%# Bind("CHARG") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Length" HeaderStyle-Width="124px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="ACTLEN" ReadOnly="true" runat="server" Text='<%# Bind("ACTLEN") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update Length" HeaderStyle-Width="124px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUPDLEN" runat="server" Text='<%# Bind("UPDLEN") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Heat No." HeaderStyle-Width="124px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="HEATNO" ReadOnly="true" runat="server" Text='<%# Bind("HEATNO") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Pipe No." HeaderStyle-Width="124px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="CUSTP" ReadOnly="true" runat="server" Text='<%# Bind("CUSTP") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coil No." HeaderStyle-Width="124px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="COILNO" ReadOnly="true" runat="server" Text='<%# Bind("COILNO") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hold Reason" HeaderStyle-Width="124px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="HLDREG" ReadOnly="true" runat="server" Text='<%# Bind("HLDREG") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SQNUM" HeaderStyle-Width="124px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="SQNUM" ReadOnly="true" runat="server" Text='<%# Bind("SQNUM") %>' CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4 ">
                        <br />
                        <a type="button" class="btn btn-dark" href="ReleaseScan.aspx" style="font-size: 36px; width: 200px; height: 85px" font-size="Larger">Back</a>
                    </div>
                    <div class="form-group col-md-6 align-content-end">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Text="Post To SAP" Font-Size="Larger" ValidationGroup="addbatch" CssClass="btn btn-danger" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function toggleSelectAll(source) {
            var checkboxes = document.querySelectorAll(".rowCheckbox input[type='checkbox']");
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = source.checked;
            }
        }

        function getSelectedRows() {
            var checkboxes = document.querySelectorAll(".rowCheckbox input[type='checkbox']");
            var selectedIndexes = [];

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    selectedIndexes.push(i);
                }
            }

            alert("Selected Rows: " + selectedIndexes.join(", "));
        }
    </script>


</asp:Content>
