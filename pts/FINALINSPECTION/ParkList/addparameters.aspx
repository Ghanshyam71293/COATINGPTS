<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addparameters.aspx.cs" Inherits="PTSCOATING.pts.FINALINSPECTION.ParkList.addparameters" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../../css/usercss/css/main.css"/>
       <link rel="stylesheet" type="text/css" href="../../../css/usercss/css/dynamic.css"/>
</head>
<body>
    <form id="form1" runat="server">
           <asp:ScriptManager ID="ScriptManager1" runat="server">
     
 </asp:ScriptManager>
        <div>
           
            <div class="row" >
            
                    <div style="width:100%; overflow:auto" >
                    <asp:GridView ID="gvDetails" runat="server" Font-Size="Medium" AutoGenerateColumns="False" Width="100%"  >
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
                                    <asp:Literal ID="litqrcode"  runat="server" Text='<%#Eval("Batch")%>'></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Adhesion(knife) Test">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtparameter1" CssClass="form-control" Text='<%#Eval("Parameter1")%>'  Font-Bold="true" Height="50px" Font-Size="Larger"  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coating Thickness 1">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtparameter2" CssClass="form-control" Text='<%#Eval("Parameter2")%>'  Font-Bold="true" Height="50px" Font-Size="Larger"  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coating Thickness 2">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtparameter3" CssClass="form-control"  Text='<%#Eval("Parameter3")%>' Font-Bold="true" Height="50px" Font-Size="Larger"  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coating Thickness 3">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtparameter4" CssClass="form-control"  Text='<%#Eval("Parameter4")%>' Font-Bold="true" Height="50px" Font-Size="Larger"  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coating Thickness 4">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtparameter5" CssClass="form-control" Text='<%#Eval("Parameter5")%>'  Font-Bold="true" Height="50px" Font-Size="Larger"  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coating Thickness 5">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtparameter6" CssClass="form-control" Text='<%#Eval("Parameter6")%>'  Font-Bold="true" Height="50px" Font-Size="Larger" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cut back">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtparameter7" CssClass="form-control" Text='<%#Eval("Parameter7")%>'  Font-Bold="true" Height="50px" Font-Size="Larger"  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Holiday detector voltage">
                                <HeaderStyle HorizontalAlign="Center"  />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" VerticalAlign="Middle" />
                                <ItemTemplate>
                                    <asp:TextBox ID="txtparameter8" CssClass="form-control" Text='<%#Eval("Parameter8")%>'  Font-Bold="true" Height="50px" Font-Size="Larger"  runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                        </Columns>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                        </div>
                
                   <div class="container" >
            <div class="row" >
                <div class="col-md-6">
                    </div>
                          <div class="form-group col-md-6">
                        <asp:Button ID="btnSubmit" runat="server" Text="Update" OnClick="Save" class="btn btn-primary" ValidationGroup="advcasenew"  />
                    </div>
                </div>
                       </div>
                      
        </div>
    </form>
</body>
</html>
