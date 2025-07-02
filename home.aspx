<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="PTSCOATING.home" %>


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
            <a class="app-header__logo" style="background-color: #009688!important;" href="home.aspx">
                <img style="width: 80px; height: 80px;" src="assets/Jindal_Logo.jpg" />
            </a>
            <%--  <a class="app-header__logo"  href="home.aspx" > 
              <img style="width:213px; height:80px" src="assets/Jindal_Logo.jpg" />   </a>--%>
            <%--    <a class="app-sidebar__toggle" href="#" data-toggle="sidebar" aria-label="Hide Sidebar"></a>--%>
            <ul class="app-nav">
                <li class="dropdown"><a class="app-nav__item" href="#" data-toggle="dropdown" aria-label="Open Profile Menu" style="padding-bottom: 0px!important; text-decoration: none; text-align: center!important;"><i class="fa fa-user fa-lg"></i>
                    <br />
                    <span style="font-size: 12px;">
                        <asp:Literal ID="litusername" runat="server"></asp:Literal></span></a>
                    <ul class="dropdown-menu settings-menu dropdown-menu-right">
                        <li><a class="dropdown-item" href="logout.aspx"><i class="fa fa-sign-out fa-lg"></i>Logout</a></li>
                    </ul>
                </li>
            </ul>
        </header>
        <div class="app-sidebar__overlay" data-toggle="sidebar"></div>
        <aside class="app-sidebar" runat="server" visible="false">
            <div class="app-sidebar__user">
                <%-- <p class="app-sidebar__user-name">
                    User Name-
                    <asp:Literal ID="litusername" runat="server"></asp:Literal>
                </p>--%>
            </div>
        </aside>
        <main class="app-content" style="margin-left: 0px">
            <div class="row">
                <div class="col-md-4">
                </div>
                <div class="col-md-4">
                    <%-- <h1>Login ID-
                    <asp:Literal ID="litusername" runat="server"></asp:Literal></h1>--%>
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <a href="pts/homepage.aspx" class="btn btn-info col-md-8" style="height: 120px; font-size: larger; text-align: center; line-height: 2.5">PTS COATING</a>
                </div>
                <br />
                <br />
                <br />

                <div class="col-md-12 text-center">
                    <a href="ChangePasswords.aspx" class="btn btn-secondary col-md-8" style="height: 120px; font-size: larger; line-height: 2.5">Change Password</a>
                </div>
                <br />
                <br />
                <br />
                <div class="col-md-12 text-center">
                    <a href="logout.aspx" class="btn btn-danger col-md-8" style="height: 120px; font-size: larger; line-height: 2.5">Logout</a>
                </div>
            </div>
            <br />
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

                .btn btn-info menu {
                    width: 428px;
                    height: 78px;
                    font-size: 35px;
                    margin-left: 223px;
                }
            }
        </style>

    </form>
</body>
</html>
