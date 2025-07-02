<%@ Page Title="" Language="C#" MasterPageFile="~/pts/FINALINSPECTION/ParkList/Park.Master" AutoEventWireup="true" CodeBehind="GetBatch.aspx.cs" Inherits="PTSCOATING.pts.FINALINSPECTION.ParkList.GetBatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <div class="col-md-12">
        <div class="tile">
            <h3 class="tile-title">Get Batch</h3>
            <div class="tile-body">
                  <br />
                   <div class="row" id="divgetbatch"   runat="server" visibl="true">
                         <div class="col-md-6">
                       <asp:TextBox ID="txtdate" runat="server" Font-Size="Larger" PlaceHolder="Enter no of records"  CssClass="form-control"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                ErrorMessage="Required" ControlToValidate="txtdate" ValidationGroup="getbatch" ></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                                                CssClass="BlockPopup" />
                             </div>

                          <div class="col-md-6">
                                    <asp:Button ID="btngetbatch" runat="server" ValidationGroup="getbatch" Font-Size="Larger"  Text="Get Batch" CssClass="btn btn-primary" OnClick="btngetbatch_Click" />

                              </div>
                       </div>
                  <br />
                   
                <div class="row" runat="server" id="divbatchlist" visible="false">
                  
                    <div style="width:100%; overflow:auto" >
                    <asp:GridView ID="gvDetails" runat="server" Font-Size="Medium" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvDetails_RowDataBound" >
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" >
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                       <%#Container.DataItemIndex+1 %>
                                  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Batch List">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                    <asp:Literal ID="litqrcode"  runat="server" Text='<%#Eval("CHARG")%>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                         

                              <asp:TemplateField HeaderText="SQN No">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                      <asp:Literal ID="litFsnno" runat="server" Text='<%#Eval("FSNUM")%>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Heat No">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                      <asp:Literal ID="litheatno" runat="server"  Text='<%#Eval("HEATN")%>'></asp:Literal>
                                    <asp:TextBox ID="txtheatno" Visible="false" CssClass="form-control" Height="50px" ReadOnly="true" Font-Size="Larger" Width="120px" runat="server" Text='<%#Eval("HEATN")%>'></asp:TextBox>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust. P.No">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                    <asp:Literal ID="litCUSTP" runat="server" Text='<%#Eval("CUSTP")%>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Coating Type">
                                <HeaderStyle HorizontalAlign="Center" Width="80px"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" Width="80px"  />
                             <ItemTemplate>
                                 <asp:Literal ID="litcoatingtype" runat="server" Visible="false" Text='<%#Eval("CTYPE")%>'></asp:Literal>
                                    <asp:DropDownList ID="ddlcoatingtype" Font-Bold="true" Width="100px" Height="50px" CssClass="form-control" Font-Size="Larger"  runat="server">
                                        <asp:ListItem>COAT PIPE</asp:ListItem>
                                              <asp:ListItem>BARE PIPE</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Status">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle"  />
                             <ItemTemplate>
                                  <asp:Literal ID="litstatus"  runat="server"  Visible="false" Text='<%#Eval("BSTAT")%>'></asp:Literal>
                                   <asp:DropDownList ID="ddlstatus" Width="100px"  Font-Bold="true" Font-Size="Larger" Height="50px" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" AutoPostBack="true">
                                     
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                      <asp:Literal ID="litreg" runat="server" Visible="false" Text='<%#Eval("HLDREG")%>'></asp:Literal>
                                  <asp:DropDownList ID="ddlreason" Width="150px"  Font-Bold="true" Height="50px" CssClass="form-control" Font-Size="Larger"  runat="server">
                                       <asp:ListItem Value="0">-Select-</asp:ListItem> 
                                      
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Length">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                    <asp:Literal ID="litlength" Visible="false" runat="server" Text='<%#Eval("KALABM")%>'></asp:Literal>
                                      <asp:TextBox ID="txtlength" CssClass="form-control"  Font-Bold="true" Height="50px" Font-Size="Larger" Width="100px" runat="server" Text='<%#Eval("KALABM")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Weight">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                    <asp:Literal ID="litweight" Visible="false"  runat="server" Text='<%#Eval("KALABT")%>'></asp:Literal>
                                      <asp:TextBox ID="txtweight" CssClass="form-control"  Font-Bold="true" Height="50px" Font-Size="Larger" Width="100px" runat="server" Text='<%#Eval("KALABT")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="Date" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                 <asp:TextBox ID="txtdate"  CssClass="form-control"   Font-Bold="true" Font-Size="Larger" TextMode="Date"  Height="50px" placeholder="DD-MM-YYYY" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Shift" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlshift" runat="server"  Font-Bold="true" Width="100px" CssClass="form-control"  Font-Size="Larger"  Height="50px">
                                             <asp:ListItem>A</asp:ListItem>
                                             <asp:ListItem>B</asp:ListItem>
                                             <asp:ListItem>C</asp:ListItem>
                                         </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Coil No">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle"  />
                                <ItemTemplate>
                                    <asp:Literal ID="litcoilno" runat="server" Text='<%#Eval("COILN")%>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="QC Hold at">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                      <asp:Literal ID="litholdat" runat="server"    Text='<%#Eval("OPNAME")%>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                           
                        </Columns>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                         <a class='btn btn-info addpatameterss'   id="addpatameter" href='addparameters.aspx'>
                                                Add Parameters</a>
                        </div>
                    <br />
                     </div> 
                <br />  
                <div class="row">
                       <div class="col-md-4 ">
                        <br />
                          <a type="button" class="btn btn-dark" href="../FinalScan.aspx" style="font-size: 36px; width:200px; height:85px" Font-Size="Larger"  >Back</a>
                        </div>

                    <div class="form-group col-md-6 align-content-end">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="Post To SAP" Font-Size="Larger" ValidationGroup="addbatch" CssClass="btn btn-danger" OnClick="btnSubmit_Click" />

                    </div>
                </div>
            
                 
            </div>
        </div>
    
   </div>
    
     
</asp:Content>
