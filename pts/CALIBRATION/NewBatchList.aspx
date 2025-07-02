<%@ Page Title="" Language="C#" MasterPageFile="~/pts/CALIBRATION/CALIBRATION.Master" AutoEventWireup="true" CodeBehind="NewBatchList.aspx.cs" Inherits="PTSCOATING.pts.CALIBRATION.NewBatchList" %>

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
    <div class="col-md-12">
        <div class="tile">
            <h3 class="tile-title">Add New Batch</h3>
            <div class="tile-body">
                <div class="row">
                     <div style="width: 100%;">
                        <table style="width: 100%">
                                    <tr style="background-color: darksalmon;">
                                        <th>Sales Order:</th>
                                        <td>
                                            <asp:TextBox ID="txtSalesOrder" CssClass="form-control" Height="75px"  Font-Size="Larger" runat="server" Text=""></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                ErrorMessage="Required" ControlToValidate="txtSalesOrder" ValidationGroup="addbatch" ></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />
                                        </td>
                                    </tr>
                                   <%-- <tr style="background-color: darksalmon;">
                                        <th>Date:</th>
                                        <td>
                                            <asp:TextBox ID="txtdate" CssClass="form-control " Font-Size="Larger" TextMode="Date" Height="75px" placeholder="DD-MM-YYYY" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                                ErrorMessage="Required" ControlToValidate="txtdate" ValidationGroup="addbatch" ></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                Enabled="true" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />
                                        </td>
                                    </tr>--%>
                                    <tr style="background-color: darksalmon;">
                                        <th>Shift:</th>
                                        <td>
                                            <asp:DropDownList ID="ddlshift" runat="server" CssClass="form-control" Font-Size="Larger" Height="75px">
                                                <asp:ListItem>A</asp:ListItem>
                                                <asp:ListItem>B</asp:ListItem>
<%--                                                <asp:ListItem>C</asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                                  
                                   
                                    <tr style="background-color: burlywood;">
                                        <th colspan="2">Calibration of Holiday Detector

                                            <a id="addmore" class="buttonInactive">
                                                <img id="imgdiv" width="30px" border="0" src="plus.gif" />
                                            </a>


                                        </th>
                                    </tr>
                                    <tr id="tr1" style="background-color: antiquewhite;">


                                        <th>Time:</th>
                                        <td>
                                            <asp:TextBox ID="txtHDTime" runat="server" TextMode="Time" Height="75px" Font-Size="Larger"  CssClass="form-control"></asp:TextBox>                                            
                                        </td>
                                    </tr>
                                    <tr id="tr2" class="divparameter" style="background-color: antiquewhite;">
                                        <th>Calibertaion Standard:</th>
                                        <td>
                                            <asp:TextBox ID="txtHDCStd" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="tr3" style="background-color: antiquewhite;">
                                        <th>Calibration Standard ID:</th>
                                        <td>
                                            <asp:TextBox ID="txtHDCSID" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="tr4" style="background-color: antiquewhite;">
                                        <th>Holiday Detector ID:</th>
                                        <td>
                                            <asp:TextBox ID="txtHDID" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="tr5" style="background-color: antiquewhite;">
                                        <th>Frequency HRS:</th>
                                        <td>
                                            <asp:TextBox ID="txtHDFH" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr id="tr6" style="background-color: antiquewhite;">
                                        <th>Meas of Stand:</th>
                                        <td>
                                            <asp:TextBox ID="txtHDMS" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr id="tr7" style="background-color: antiquewhite;">
                                        <th>Meas of Inst:</th>
                                        <td>
                                            <asp:TextBox ID="txtHDMI" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr id="tr8" style="background-color: antiquewhite;">
                                        <th>Remarks:</th>
                                        <td>
                                            <asp:TextBox ID="txtHDRemarks" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr style="background-color: burlywood;">
                                        <th colspan="2">Calibration of Coating Thickness Gauge
                                            <a id="addmoreGauge" class="buttonInactive">
                                                <img id="imgdivGauge" width="30px" border="0" src="plus.gif" />
                                            </a>
                                        </th>
                                    </tr>
                            <tr id="tr9" style="background-color: antiquewhite;">


                                        <th>Time:</th>
                                        <td>
                                            <asp:TextBox ID="txtGTime" runat="server" TextMode="Time" Height="75px" Font-Size="Larger" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="tr10" class="divparameter" style="background-color: antiquewhite;">
                                        <th>Calibration Standard:</th>
                                        <td>
                                            <asp:TextBox ID="txtGCS" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr id="tr11" style="background-color: antiquewhite;">
                                        <th>Cal Standard ID SN:</th>
                                        <td>
                                            <asp:TextBox ID="txtGCSID" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr id="tr12" style="background-color: antiquewhite;">
                                        <th>Coating Thickness ID:</th>
                                        <td>
                                            <asp:TextBox ID="txtGCTID" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr id="tr13" style="background-color: antiquewhite;">
                                        <th>Frequency HRS:</th>
                                        <td>
                                            <asp:TextBox ID="txtGFH" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr id="tr14" style="background-color: antiquewhite;">
                                        <th>Meason Stand:</th>
                                        <td>
                                            <asp:TextBox ID="txtGMS" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr id="tr15" style="background-color: antiquewhite;">
                                        <th>Meason Inst:</th>
                                        <td>
                                            <asp:TextBox ID="txtGMI" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr id="tr16" style="background-color: antiquewhite;">
                                        <th>Remarks:</th>
                                        <td>
                                            <asp:TextBox ID="txtGRemarks" runat="server" Style="text-transform: uppercase" Font-Size="Larger" Height="75px" onkeypress="return IsNumberWithOneDecimal(this,event);" CssClass="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                        </table>
                    </div>
                 
                </div>
             
                <div class="row">
                    <div class="col-md-4 ">
                        <br />
                        <a type="button" class="btn btn-dark" href="../homepage.aspx" style="font-size: 36px; width: 200px; height: 85px" font-size="Larger">Back</a>
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
