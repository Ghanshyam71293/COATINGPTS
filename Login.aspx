<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PTSCOATING.Login" %>


<!DOCTYPE html>
<html>
  <head runat="server" id="header">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
       <link rel="icon" type="image/x-icon" href="assets/Jindal_Logo.jpg">
    <!-- Main CSS-->
    <link rel="stylesheet" type="text/css" href="css/usercss/css/main.css">
        <link rel="stylesheet" type="text/css" href="css/usercss/css/dynamic.css">
    <!-- Font-icon css-->
    <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <title>Login</title>
  </head>
  <body>
      <form runat="server" id="form1">

          <style>
              span{color:red}
          </style>
               
 <asp:ScriptManager ID="ScriptManager1" runat="server">
 </asp:ScriptManager>
    <section class="material-half-bg" style="background-color:#009688">
      <div class="cover"></div>
    </section>
    <section class="login-content">
     <%-- <div class="logo">
          <image style="width:210px; height:110px"  src="assets/Jindal_Logo.jpg"></image>
          <br />
        <h4 style="font-family:'Times New Roman', Times, serif">JINDAL PTS COATING</h4>
         <h2 style="font-family:'Times New Roman', Times, serif">&nbsp;&nbsp;&nbsp;(BTO1-USA)</h2>
      </div>--%>
        <div class="logo">
         <%-- <image style="width:210px; height:110px"  src="assets/Jindal_Logo.jpg"></image>
          <br />
        <h4 style="font-family:'Times New Roman', Times, serif">JINDAL PTS COATING</h4>
         <h2 style="font-family:'Times New Roman', Times, serif">&nbsp;&nbsp;&nbsp;(BTO1-USA)</h2>--%>

      </div>
      <div class="login-box">
        <div class="login-form" action="login.aspx">
           <%--<h3 class="login-head"><i class="fa fa-lg fa-fw fa-user"></i>SIGN IN</h3>--%>
            <div class="login-head" style="margin-top:-35px!important;margin-bottom:-10px;">                 
           <img src="assets/Jindal_Logo.jpg" style="width:98px; height:70px;"/><h4 style="font-size:21px;margin-bottom: -18px;">JINDAL PTS COATING (BTO1-USA)</h4>
            </div>
          <div class="form-group" >
            <label class="control-label" style="font-size:medium">USERNAME</label>

                <asp:TextBox ID="txtusername" CssClass="form-control" Font-Size="Medium" runat="server" placeholder="Enter username"></asp:TextBox>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                        ErrorMessage="Required" ControlToValidate="txtusername" ValidationGroup="login"></asp:RequiredFieldValidator>
              
              <ajaxtoolkit:validatorcalloutextender ID="ValidatorCalloutExtender9" runat="server"
                        Enabled="true" TargetControlID="RequiredFieldValidator3" HighlightCssClass="validatorCalloutHighlight2"
                        CssClass="BlockPopup" />

            
          </div>
          <div class="form-group">
            <label class="control-label" style="font-size:medium">PASSWORD</label>
          

                <asp:TextBox ID="txtpassword" CssClass="form-control" runat="server" placeholder="Enter password" TextMode="Password"></asp:TextBox>

     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                        ErrorMessage="Required" ControlToValidate="txtpassword" ValidationGroup="login"></asp:RequiredFieldValidator>
       <ajaxtoolkit:validatorcalloutextender ID="ValidatorCalloutExtender1" runat="server"
                        Enabled="true" TargetControlID="RequiredFieldValidator4" HighlightCssClass="validatorCalloutHighlight2"
                        CssClass="BlockPopup" />
          </div>

          <div class="form-group btn-container">

              
        <asp:Button ID="Button2" CssClass="btn btn-primary btn-block" ValidationGroup="login"  runat="server" Text="Login" OnClick="Button1_Click" />

         
          </div>
                 

              <p  style="font-size:12px"><a href="#" data-toggle="flip">Forgot Password ?</a></p>
           
        </div>
    <asp:Panel ID="Panel2" runat="server" Visible="true">
        <div class="forget-form"  >
          <h3 class="login-head"><i class="fa fa-lg fa-fw fa-lock"></i>Forgot Password ?</h3>
          <div class="form-group">
            <label class="control-label" style="font-size:medium">User Id</label>
               <asp:TextBox ID="txtuserid" runat="server" CssClass="form-control" placeholder="Enter User Id" ></asp:TextBox>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                        ErrorMessage="Required" ControlToValidate="txtuserid" ValidationGroup="forget"></asp:RequiredFieldValidator>
       <ajaxtoolkit:validatorcalloutextender ID="ValidatorCalloutExtender2" runat="server"
                        Enabled="true" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight2"
                        CssClass="BlockPopup" />
            <%--<input class="form-control" type="text" placeholder="Email">--%>
          </div>
          <div class="form-group btn-container">
                  <asp:Button ID="btnsubmit" runat="server" ValidationGroup="forget" CssClass="btn btn-primary btn-block" Text="Send" OnClick="btnforget_Click" />
           <%-- <button class="btn btn-primary btn-block"><i class="fa fa-unlock fa-lg fa-fw"></i>RESET</button>--%>
          </div>
          <div class="form-group mt-3">
            <p class="semibold-text mb-0" style="font-size:medium"><a href="#" data-toggle="flip"><i class="fa fa-angle-left fa-fw"></i> Back to Login</a></p>
          </div>
        </div>

        </asp:Panel>
      </div>
    </section>
    <!-- Essential javascripts for application to work-->
    <script src="css/usercss/js/jquery-3.3.1.min.js"></script>
    <script src="css/usercss/js/popper.min.js"></script>
    <script src="css/usercss/js/bootstrap.min.js"></script>
    <script src="css/usercss/js/main.js"></script>
    <!-- The javascript plugin to display page loading on top-->
    <script src="css/usercss/js/plugins/pace.min.js"></script>
    <script type="text/javascript">
      // Login Page Flipbox control
      $('.login-content [data-toggle="flip"]').click(function() {
      	$('.login-box').toggleClass('flipped');
      	return false;
      });
    </script>
          </form>
  </body>
</html>