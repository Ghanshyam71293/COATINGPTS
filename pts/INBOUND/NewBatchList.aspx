<%@ Page Title="" Language="C#" MasterPageFile="~/pts/INBOUND/INBOUND.Master" AutoEventWireup="true" CodeBehind="NewBatchList.aspx.cs" Inherits="PTSCOATING.pts.INBOUND.NewBatchList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <%-- <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
<link rel="stylesheet" href="/resources/demos/style.css" />
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>
    <style>
        /*  .autocomplete-container {
            position: relative;
        }
        .suggestion-list {
            border: 1px solid #ccc;
            background: white;
            position: absolute;
            z-index: 1000;
            max-height:150px;
            overflow-y: auto; 
        }        
        .suggestion-item {           
            cursor: pointer;
        }
        .suggestion-item:hover {
                background-color: #f0f0f0;
         }*/

        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        th, td {
            padding: 5px;
            text-align: center;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            function setupAutocomplete(inputElement, url) {             
                inputElement.autocomplete({                   
                    source: function (request, response) {
                        $.ajax({
                            url: url,
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'query': request.term }),
                            success: function (data) {
                                if (data.d && Array.isArray(data.d)) {
                                    response($.map(data.d, function (item) {
                                        return {
                                            label: item,
                                            value: item
                                        };
                                    }));
                                }
                                else {
                                    response([]);
                                }
                            },
                            error: function (xhr, status, error) {
                                console.error("AJAX Error:", status, error);
                                $("#error-message").text("An error occurred while fetching data.").show();
                            }
                            
                        });
                    },
                    minLength: 1
                });
                inputElement.on('keyup input', function () {                    
                    $(this).autocomplete("search", $(this).val());
                });
            }

            // custpipeno
            var $txtcustpipeno = $("#ContentPlaceHolder1_repDetails_txtcustpipeno_0");
            setupAutocomplete($txtcustpipeno, "NewBatchList.aspx/GetAutoCompleteCustPipedetails");

            // heatdno
            var $txtheatdno = $("#ContentPlaceHolder1_repDetails_txtheatdno_0");
            setupAutocomplete($txtheatdno, "NewBatchList.aspx/GetAutoCompleteHeatNo");

            $('#ContentPlaceHolder1_repDetails_txtbm_0').on('keyup', function () {
                let prelblbm = parseFloat($("#ContentPlaceHolder1_repDetails_hiddenbm_0").val()) || 0;
                let prelblbt = parseFloat($("#ContentPlaceHolder1_repDetails_hiddenbt_0").val()) || 0;
                let txtbm = parseFloat($(this).val()) || 0;
                $('#ContentPlaceHolder1_repDetails_txtbt_0').attr('ReadOnly', false);
                if (prelblbm !== 0 && prelblbt !== 0 && txtbm > 0) {
                    let txtbt = (prelblbt / prelblbm) * txtbm;
                    $("#ContentPlaceHolder1_repDetails_hiddenFbt_0").val(txtbt.toFixed(3));
                    $("#ContentPlaceHolder1_repDetails_txtbt_0").val(txtbt.toFixed(3));
                    $('#ContentPlaceHolder1_repDetails_txtbt_0').attr('ReadOnly', true);
                }
                else {
                    if (this.val() == 0 || this.val()!="" )
                    alert("Please Enter valid Length");
                }
            });

        });

    //$(document).ready(function () {
    //    var $txtcustpipeno = $("#ContentPlaceHolder1_repDetails_txtcustpipeno_0");
    //    var $txtheatdno = $("#ContentPlaceHolder1_repDetails_txtheatdno_0");
    //    // Initialize the autocomplete
    //    $txtcustpipeno.autocomplete({
    //        source: function (request, response) {
    //            $.ajax({
    //                url: "NewBatchList.aspx/GetAutoCompleteCustPipedetails",
    //                type: "POST",
    //                contentType: "application/json; charset=utf-8",
    //                dataType: "json",
    //                data: JSON.stringify({ 'query': request.term }),
    //                success: function (data) {
    //                    if (data.d && Array.isArray(data.d)) {
    //                        response($.map(data.d, function (item) {
    //                            return {
    //                                label: item,
    //                                value: item
    //                            };
    //                        }));
    //                    } else {
    //                        response([]);
    //                    }
    //                },
    //                error: function (xhr, status, error) {
    //                    console.error("AJAX Error:", status, error);
    //                    $("#error-message").text("An error occurred while fetching data.").show();
    //                }
    //            });
    //        },
    //        minLength: 1
    //    });


    //    $txtcustpipeno.on('keyup input', function () {
    //        $(this).autocomplete("search", $(this).val());
    //    });


    //    $txtheatdno.autocomplete({
    //        source: function (request, response) {
    //            $.ajax({
    //                url: "NewBatchList.aspx/GetAutoCompleteHeatNo",
    //                type: "POST",
    //                contentType: "application/json; charset=utf-8",
    //                dataType: "json",
    //                data: JSON.stringify({ 'query': request.term }),
    //                success: function (data) {
    //                    if (data.d && Array.isArray(data.d)) {
    //                        response($.map(data.d, function (item) {
    //                            return {
    //                                label: item,
    //                                value: item
    //                            };
    //                        }));
    //                    } else {
    //                        response([]);
    //                    }
    //                },
    //                error: function (xhr, status, error) {
    //                    console.error("AJAX Error:", status, error);
    //                    $("#error-message").text("An error occurred while fetching data.").show();
    //                }
    //            });
    //        },
    //        minLength: 1
    //    });

    //    $txtheatdno.on('keyup input', function () {
    //        $(this).autocomplete("search", $(this).val());
    //    });

    //    $('#ContentPlaceHolder1_repDetails_txtbm_0').on('keyup', function () {
    //        let prelblbm = parseFloat($("#ContentPlaceHolder1_repDetails_hiddenbm_0").val()) || 0;
    //        let prelblbt = parseFloat($("#ContentPlaceHolder1_repDetails_hiddenbt_0").val()) || 0;
    //        let txtbm = parseFloat($(this).val()) || 0;
    //        if (prelblbm !== 0 && prelblbt !== 0 && txtbm > 0) {
    //            let txtbt = (prelblbt / prelblbm) * txtbm;
    //            $("#ContentPlaceHolder1_repDetails_txtbt_0").val(txtbt.toFixed(3));
    //        }
    //        else {
    //            alert("Please Enter valid Length");
    //           // $("#ContentPlaceHolder1_repDetails_txtbt_0").val(0); 
    //        }
    //    });
    //});
    </script>

    <div class="col-md-12">
    </div>
    <div class="col-md-12">
        <div class="tile">
            <h3 class="tile-title">Add New Batch</h3>
            <div class="tile-body">
                <div class="row">
                    <div style="width: 100%;">
                        <table style="width: 100%">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
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
                                                    <asp:TextBox ID="txtdate" CssClass="form-control " Font-Size="Larger" TextMode="Date" Height="75px" placeholder="DD-MM-YYYY" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                                        ErrorMessage="Required" ControlToValidate="txtdate" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtenderop5" runat="server"
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
                                                <th>Heat Code:</th>
                                                <td>
                                                    <asp:TextBox ID="txtheatcode" CssClass="form-control" ReadOnly="true" Font-Size="Larger" runat="server"></asp:TextBox>

                                                </td>
                                            </tr>
                                            <%-- <tr style="background-color: darksalmon;" id="trmtr" runat="server">
                                        <th>MTR HEAT:</th>
                                        <td>
                                            <asp:DropDownList ID="ddlmtr" CssClass="form-control" Font-Size="Larger" runat="server" OnSelectedIndexChanged="ddlmtr_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </td>
                                    </tr>--%>
                                            <tr style="background-color: darksalmon;">
                                                <th>Heat No:</th>
                                                <td>
                                                    <asp:TextBox ID="txtheatdno" CssClass="form-control" ReadOnly="false" Font-Size="Larger" placeholder="Enter Heat No" runat="server" style="text-transform:uppercase;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtheatdno" runat="server" Display="None"
                                                        ErrorMessage="Required" ControlToValidate="txtheatdno" ValidationGroup="addbatch" Enabled="false"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                        Enabled="true" TargetControlID="RFVtxtheatdno" HighlightCssClass="validatorCalloutHighlight2"
                                                        CssClass="BlockPopup" />
                                                    <asp:DropDownList ID="ddlHeatNo" CssClass="form-control" Font-Size="Larger" runat="server" OnSelectedIndexChanged="ddlHeatNo_SelectedIndexChanged" AutoPostBack="true" Visible="false"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFVddlHeatNo" runat="server" Display="None"
                                                        ErrorMessage="Required" ControlToValidate="ddlHeatNo" InitialValue="NA" ValidationGroup="addbatch" Enabled="false"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                        Enabled="true" TargetControlID="RFVddlHeatNo" HighlightCssClass="validatorCalloutHighlight2"
                                                        CssClass="BlockPopup" />
                                                </td>
                                            </tr>
                                            <tr style="background-color: darksalmon;">
                                                <th>Length:</th>
                                                <td>
                                                    <asp:HiddenField ID="hiddenbm" runat="server" />
                                                    <asp:HiddenField ID="hiddenFbm" runat="server" />
                                                    <asp:TextBox ID="txtbm" CssClass="form-control" ReadOnly="true" Font-Size="Larger" placeholder="Enter Length" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                                        ErrorMessage="Required" ControlToValidate="txtbm" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                        Enabled="true" TargetControlID="RequiredFieldValidator3" HighlightCssClass="validatorCalloutHighlight2"
                                                        CssClass="BlockPopup" />
                                                </td>
                                            </tr>
                                            <tr style="background-color: darksalmon;">
                                                <th>Weight:</th>
                                                <td>
                                                    <asp:HiddenField ID="hiddenbt" runat="server" />
                                                     <asp:HiddenField ID="hiddenFbt" runat="server" />
                                                    <asp:TextBox ID="txtbt" CssClass="form-control"  Font-Size="Larger" placeholder="Enter Weight" runat="server" ReadOnly="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                                        ErrorMessage="Required" ControlToValidate="txtbt" ValidationGroup="addbatch"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                        Enabled="true" TargetControlID="RequiredFieldValidator4" HighlightCssClass="validatorCalloutHighlight2"
                                                        CssClass="BlockPopup" />
                                                </td>
                                            </tr>
                                            <tr style="background-color: darksalmon;">
                                                <th>Customer Pipe No:</th>
                                                <td>
                                                    <asp:Literal ID="litcustpipeno" Visible="false" runat="server"></asp:Literal>
                                                    <asp:TextBox ID="txtcustpipeno" Font-Size="Larger" Style="text-transform: uppercase;" CssClass="form-control autocompletecustpipe float-left" runat="server" autocomplete="off" OnTextChanged="txtcustpipeno_TextChanged" AutoPostBack="false"></asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr style="background-color: darksalmon;">
                                                <th>Coil No:</th>
                                                <td>
                                                    <asp:TextBox ID="txtcoilno" CssClass="form-control" Font-Size="Larger" runat="server"></asp:TextBox>
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                                                ErrorMessage="Required" ControlToValidate="txtcoilno" ValidationGroup="addbatch" ></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                Enabled="true" TargetControlID="RequiredFieldValidator6" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />--%>
                                                </td>
                                            </tr>
                                            <tr style="background-color: darksalmon;">
                                                <th>Status:</th>
                                                <td>
                                                    <asp:DropDownList ID="ddlbstatus" runat="server" AutoPostBack="true" CssClass="form-control" Font-Size="Larger" Height="75px" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                                        ErrorMessage="Required" ControlToValidate="ddlbstatus" ValidationGroup="addbatch" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                        Enabled="true" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight2"
                                                        CssClass="BlockPopup" />
                                                </td>
                                            </tr>
                                            <tr style="background-color: darksalmon; display: none">
                                                <th>Reason:</th>
                                                <td>
                                                    <asp:DropDownList ID="ddlreasons" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <%--<tr style="background-color:darksalmon;">
                                     <th>Coating Type:</th>
                                     <td>
                                         <asp:DropDownList ID="ddlcoatingpipe" runat="server" CssClass="form-control"  Font-Size="Larger"  Height="75px">
                                             <asp:ListItem Selected="True">COAT PIPE</asp:ListItem>
                                             <asp:ListItem>BARE PIPE</asp:ListItem>
                                         </asp:DropDownList>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                                                ErrorMessage="Required" ControlToValidate="ddlcoatingpipe" ValidationGroup="addbatch" InitialValue="0"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                Enabled="true" TargetControlID="RequiredFieldValidator6" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />
                                     </td>
                                 </tr>--%>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                        <a type="button" class="btn btn-dark" href="InboundScan.aspx" style="font-size: 36px; width: 200px; height: 85px" font-size="Larger">Back</a>
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
