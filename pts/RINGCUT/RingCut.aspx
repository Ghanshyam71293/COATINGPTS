<%@ Page Title="" Language="C#" MasterPageFile="~/pts/RINGCUT/RINGCUT.Master" AutoEventWireup="true" CodeBehind="RingCut.aspx.cs" Inherits="PTSCOATING.pts.RINGCUT.RingCut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        th, td {
            padding: 5px;
            text-align: left;
        }

        th {
            background-color: darksalmon;
        }

             .buttonActive {
            background-color: #1E78AB;
            border: 1px solid #1E78AB;
            color: #fff;
        }

             .buttonInactive {
            background-color: #fff;
            border: 1px solid #1E78AB;
            color: #1E78AB;
        }

        .backBtn, .submintBtn, .GetItem {
            font-size: 24px;
        }

        .ringCut .tile-body {
            font-size: 14px;
        }

            .ringCut .tile-body td input {
                width: 100%;
                padding: 6px;
            }

            .ringCut .tile-body tr td:nth-child(1) input, .ringCut .tile-body tr td:nth-child(2) input {
                background-color: #f1f1f1;
                border: 0;                
            }

        .submintBtn {
            font-size: 24px;
            background-color: #dc3545;
            outline: none;
            border: 0;
            border-radius: 4px;
            padding: 8px;
        }
    </style>
    <div class="col-md-12 ringCut mt-4 mt-md-5">
        <div class="tile">
            <h3 class="tile-title p-2" style="background-color: darksalmon;">RING CUT</h3>
            <hr />
            <div class="tile-body">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-12 col-sm-6 col-md-3 form-group">
                                <label>SO No</label>
                                <asp:TextBox ID="txtSOno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3 form-group">
                                <label>SO Item</label>
                                <asp:TextBox ID="txtSOitem" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3 form-group">
                                <label>Batch No.</label>
                                <asp:TextBox ID="txtBatchNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3 form-group">
                                <label>Smp. Length</label>
                                <asp:TextBox ID="txtSmpLength" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-12 col-sm-6 col-md-3 form-group">
                                <label>Orig. length</label>
                                <asp:TextBox ID="txtOrigLength" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row px-3" runat="server" id="showTable"></div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <a type="button" class="btn btn-dark backBtn" href="../homepage.aspx">Back</a>
                        <asp:Button ID="GetItemId" runat="server" Text="Get Item" CssClass="btn btn-danger GetItem" OnClick="AddPrmt" />
                        <asp:Button ID="BtnSubmit1" runat="server" OnClientClick="javascript:return PostToAsp();" Text="Post To ASP" Visible="false" CssClass="btn btn-danger submintBtn" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<script type="text/javascript">
        function PostToAsp() {
            var customers = new Array();
            var rowCount = document.querySelectorAll("#ContentPlaceHolder1_showTable tr").length;;
            for (var i = 0; i < rowCount; i++) {
                var customer = "";
                var srnoVal = $("#ContentPlaceHolder1_showTable tr #inputSrno" + i + "").val();
                var paraVal = $("#ContentPlaceHolder1_showTable tr #inputPara" + i + "").val();
                var valueVla = $("#ContentPlaceHolder1_showTable tr #inputVal" + i + "").val();
                customer += '{"SRNO":"' + srnoVal + '","PARA":"' + paraVal + '", "VAULE":"' + valueVla + '"}';
                customers.push(customer);
            }

            var Sono = JSON.stringify($("#ContentPlaceHolder1_txtSOno").val());
            var SoItem = JSON.stringify($("#ContentPlaceHolder1_txtSOitem").val());
            var Batchno = JSON.stringify($("#ContentPlaceHolder1_txtBatchNo").val());
            var SMP_Length = JSON.stringify($("#ContentPlaceHolder1_txtSmpLength").val());
            var Orig_Length = JSON.stringify($("#ContentPlaceHolder1_txtOrigLength").val());

            var arr = JSON.stringify(customers).replace(/","/g, ",").replace('["', "[").replace('"]', "]").replace(/\\/g, "");

            $.ajax({
                type: "POST",
                url: "RingCut.aspx/SendMainForm1",
                data: '{customers: ' + arr + ', Sono: ' + Sono + ', SoItem: ' + SoItem + ', Batchno: ' + Batchno + ', SMP_Length: ' + SMP_Length + ', Orig_Length: ' + Orig_Length + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    alert(data.d);
                },
                error: function (xhr, err) {
                    alert(xhr.responsetext);
                }
            });
        }
    </script>--%>
    <script type="text/javascript">
        function PostToAsp() {
            var customers = new Array();
            var rowCount = document.querySelectorAll("#ContentPlaceHolder1_showTable tr").length;;
            for (var i = 0; i < rowCount - 1; i++) {
                var customer = "";
                var srnoVal = $("#ContentPlaceHolder1_showTable tr #inputSrno" + i + "").val();
                var paraVal = $("#ContentPlaceHolder1_showTable tr #inputPara" + i + "").val();
                var valueVla = $("#ContentPlaceHolder1_showTable tr #inputVal" + i + "").val();
                customer += '{"SRNO":"' + srnoVal + '","PARA":"' + paraVal + '", "VAULE":"' + valueVla + '"}';
                customers.push(customer);
            }

            var Sono = JSON.stringify($("#ContentPlaceHolder1_txtSOno").val());
            var SoItem = JSON.stringify($("#ContentPlaceHolder1_txtSOitem").val());
            var Batchno = JSON.stringify($("#ContentPlaceHolder1_txtBatchNo").val());
            var SMP_Length = JSON.stringify($("#ContentPlaceHolder1_txtSmpLength").val());
            var Orig_Length = JSON.stringify($("#ContentPlaceHolder1_txtOrigLength").val());

            var arr = JSON.stringify(customers).replace(/","/g, ",").replace('["', "[").replace('"]', "]").replace(/\\/g, "");

            $.ajax({
                type: "POST",
                url: "RingCut.aspx/SendMainForm1",
                data: '{customers: ' + arr + ', Sono: ' + Sono + ', SoItem: ' + SoItem + ', Batchno: ' + Batchno + ', SMP_Length: ' + SMP_Length + ', Orig_Length: ' + Orig_Length + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    alert(data.d);
                },
                error: function (xhr, err) {
                    alert(xhr.responsetext);
                }
            });
        }
    </script>
</asp:Content>

