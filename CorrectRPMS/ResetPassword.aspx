<%@ Page Language="C#" Title="Password Creation" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="CorrectRPMS.ResetPassword" %>



<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><%:Page.Title %></title>
</head>
<body>
    
    <link href="Content/style.css" rel="stylesheet" />
<link href="Content/font-awesome.min.css" rel="stylesheet" />
    <section class="w3l-hotair-form">

        <h1>RPMS Reset Account Form</h1>
        <div class="container" >
            <!-- /form -->
            <div class="workinghny-form-grid">
                <div class="main-hotair">
                    <div class="content-wthree " >
                
                      <asp:Label ID="asplbl" runat="server" BackColor="Blue"  ForeColor="White" ></asp:Label>
                        <form action="#" runat="server" method="post">
                            <asp:ScriptManager runat="server" EnablePageMethods="true" ID="scriptmanager1"></asp:ScriptManager>
                            <a href="" ID="Link" runat="server"></a>

                            <input type="text" runat="server" class="text" name="text" id="txtusername" placeholder="staff number" required="required"  autofocus/>
                           

                            <input type="password" runat="server" class="password" name="password" id="txtpassword" placeholder="Password Sent to Your Mail" required="required" autofocus/>

                            <input type="password" runat="server" class="password" name="txtnewpassword" id="txtnewpassword" placeholder="New Password" required="required" autofocus/>

                              <input type="password" runat="server" class="password" name="txtconfirmpassword" id="txtconfirmpassword" placeholder="Confirm Password" required="required" autofocus/>


                          <button class="btn" type="submit" runat="server" id="btnlogin" onserverclick="btnlogin_ServerClick">Change Password</button>

                            <p class="account"><a href="LoginPage.aspx" style="position:relative; left:-7%">Goto Login Page</a></p>

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
