<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="CorrectRPMS.LoginPage" %>



<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>
</head>
<body>
    
    <link href="Content/style.css" rel="stylesheet" />
<link href="Content/font-awesome.min.css" rel="stylesheet" />
    <section class="w3l-hotair-form">

        <h1>RPMS Login Form</h1>
        <div class="container" >
            <!-- /form -->
            <div class="workinghny-form-grid">
                <div class="main-hotair">
                    <div class="content-wthree " >
                
                      
                        <form action="#" runat="server" method="post">
                            <asp:ScriptManager runat="server" EnablePageMethods="true" ID="scriptmanager1"></asp:ScriptManager>

                         <asp:Label id="asplbl" runat="server"  BackColor="Blue" ForeColor="White" Font-Bold="true"  ></asp:Label> <br />
                            <br />
                            <input type="text" runat="server" class="text" name="text" id="txtusername" placeholder="Staff Number" required autofocus/>
                           

                            <input type="password" runat="server" class="password" name="password" id="txtpassword" placeholder="*****" required autofocus/>

                          <button class="btn" type="submit" runat="server" id="btnlogin" onserverclick="btnlogin_ServerClick">Log In</button>

                            <p class="account" ><a href="ForgotPassword.aspx" style="position:relative; left:-7%">Forgot Password</a></p>

                        </form>


                    </div>
                    <div class="w3l_form align-self">
                        <div class="left_grid_info">
                          
                            <img src="images/download.png" alt="" class="img-fluid" width="300"  height="300"  style="align-content:center"  />
                        </div>
                    </div>
                </div>
            </div>
            <!-- //form -->
        </div>
        <!-- copyright-->
        <div class="copyright text-center">
            <p class="copy-footer-29">
              &copy; <%=DateTime.Now.Year %> Result Processing Management System All rights reserved | Developed by <a href="https://oouagoiwoye.edu.ng">OOU-ICT</a>
            </p>
        </div>
        <!-- //copyright-->
    </section>

</body>
</html>

