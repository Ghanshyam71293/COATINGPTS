<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePasswords.aspx.cs" Inherits="PTSCOATING.ChangePasswords" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server" id="header">
    <title>Jindal PTS COATING</title>
    <!-- Main CSS-->
    <link rel="stylesheet" type="text/css" href="css/usercss/css/main.css">
    <link rel="stylesheet" type="text/css" href="css/usercss/css/dynamic.css">
    <!-- Font-icon css-->
    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
</head>
<body class="app sidebar-mini">

    <form runat="server" id="form1">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <!-- Navbar-->
        <header class="app-header">
                         <a class="app-header__logo"  href="home.aspx" > 
              <img style="width:213px; height:80px" src="assets/Jindal_Logo.jpg" />   </a>
            <%--    <a class="app-sidebar__toggle" href="#" data-toggle="sidebar" aria-label="Hide Sidebar"></a>--%>
       
            
            <ul class="app-nav">
                <li class="dropdown"><a class="app-nav__item" href="#" data-toggle="dropdown" aria-label="Open Profile Menu"  style="padding-bottom:0px!important; text-decoration: none; text-align:center!important;"><i class="fa fa-user fa-lg"></i><br /><span style="font-size:12px;"><asp:Literal ID="litusername" runat="server"></asp:Literal></span></a>
                    <ul class="dropdown-menu settings-menu dropdown-menu-right">
                        <li><a class="dropdown-item" href="logout.aspx"><i class="fa fa-sign-out fa-lg"></i>Logout</a></li>
                    </ul>
                </li>
            </ul>
        </header>




       


        <main class="app-content" style="margin-left:0px">
            <br />
               <div class="row">
                      <div class="col-md-4">
                          </div>
                      <div class="col-md-4">
                   <%--        <h1>Login ID-
                    <asp:Literal ID="litusername" runat="server"></asp:Literal></h1>--%>
                          </div>
                     <div class="col-md-4">
                   
                         </div>
                 
                    </div>
            <div class="row">


                 <div class="col-md-12">
          <div class="tile">
            <h3 class="tile-title">Change Password</h3>
            <div class="tile-body">
              <div class="row">
           
                <div class="form-group col-md-6">

                      <label class="control-label">Enter Old Password</label>
                       <asp:TextBox ID="txtoldpassword" runat="server" class="form-control"  TextMode="Password" placeholder="Enter Old Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                        ErrorMessage="Required" ControlToValidate="txtoldpassword" ValidationGroup="cp"></asp:RequiredFieldValidator>

                    <ajaxtoolkit:validatorcalloutextender ID="ValidatorCalloutExtender9" runat="server"
                        Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                        CssClass="BlockPopup" />
                    </div>
                  <div class="form-group col-md-6">

                      <label class="control-label">Enter New Password</label>
                       <asp:TextBox ID="txtnewpassword" runat="server" class="form-control" TextMode="Password"  placeholder="Enter New Password"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                        ErrorMessage="Required" ControlToValidate="txtnewpassword" ValidationGroup="cp"></asp:RequiredFieldValidator>

                    <ajaxtoolkit:validatorcalloutextender ID="ValidatorCalloutExtender1" runat="server"
                        Enabled="true" TargetControlID="RequiredFieldValidator2" HighlightCssClass="validatorCalloutHighlight2"
                        CssClass="BlockPopup" />

                      <asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtnewpassword" Display="Dynamic"
                                        ErrorMessage="Enter only a-z,A-Z, 0-9  Max length=10 and Min. length=3" ValidationExpression="([a-zA-Z0-9]{3,20})$"></asp:RegularExpressionValidator>


                    </div>

                   <div class="form-group col-md-6">

                      <label class="control-label">Confirm  Password</label>
                       <asp:TextBox ID="txtrenewpassword" runat="server" class="form-control" TextMode="Password"  placeholder="Enter Confirm Password"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                        ErrorMessage="Required" ControlToValidate="txtrenewpassword" ValidationGroup="cp"></asp:RequiredFieldValidator>

                    <ajaxtoolkit:validatorcalloutextender ID="ValidatorCalloutExtender2" runat="server"
                        Enabled="true" TargetControlID="RequiredFieldValidator3" HighlightCssClass="validatorCalloutHighlight2"
                        CssClass="BlockPopup" />

                         <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtnewpassword"
                                    ControlToValidate="txtrenewpassword" Display="Dynamic" ErrorMessage="Confirm Password should be same as New Password"></asp:CompareValidator>
                    </div>
                 

                  

                  <div class="form-group col-md-6 m-4">
                      <asp:Button ID="btnsubmit" CssClass="btn btn-primary" ValidationGroup="cp" runat="server" Text="Update" OnClick="btnsubmit_Click"  />
                      </div>


                  </div>
                </div>
              </div>
          </div>

               

            </div>

        </main>

        <script src="css/usercss/js/jquery-3.3.1.min.js"></script>
        <script src="css/usercss/js/popper.min.js"></script>
        <script src="css/usercss/js/bootstrap.min.js"></script>
        <script src="css/usercss/js/main.js"></script>
        <script src="css/usercss/js/plugins/pace.min.js"></script>
        <script type="text/javascript" src="css/usercss/js/plugins/chart.js"></script>


        <script src="fancybox/jquery-1.4.3.min.js"></script>
        <script type="text/javascript" src="fancybox/jquery.mousewheel-3.0.4.pack.js"></script>
        <script type="text/javascript" src="fancybox/jquery.fancybox-1.3.4.pack.js"></script>
        <link rel="stylesheet" type="text/css" href="fancybox/jquery.fancybox-1.3.4.css"
            media="screen" />

        <script type="text/javascript">
            $(document).ready(function () {
                $('#newpark').fancybox({
                    'width': '70%',
                    'height': '100%',
                    'autoScale': true,
                    'scrolling': true,
                    'transitionIn': 'elastic',
                    'transitionOut': 'elastic',
                    'type': 'iframe'
                });

            });
        </script>

        <style>

            @media(max-width:300px) {
                .app-content {
                    width: 300px;
                }
                .btn btn-info menu{
                     width: 428px;height: 78px;font-size: 35px;margin-left: 223px;
                }
               
}

        </style>









    </form>
</body>
</html>