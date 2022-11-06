<%@ Page Language="C#" Title="Setup Secret Question" AutoEventWireup="true" CodeBehind="SetupSecurityQuestion.aspx.cs" Inherits="CorrectRPMS.SetupSecurityQuestion" %>



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

        <h1>Security Question Form</h1>
        <div class="container" >
            <!-- /form -->
            <div class="workinghny-form-grid">
                <div class="main-hotair">
                    <div class="content-wthree " >
                
                      
                        <form action="#" runat="server" method="post">
                            <asp:ScriptManager runat="server" EnablePageMethods="true" ID="scriptmanager1"></asp:ScriptManager>

                           
                              <asp:DropDownList ID="ddlsecurity1" runat="server" Width="77%"  ></asp:DropDownList> <br />

                           <br />
                            <input type="text" runat="server" class="text" name="txtsecurity1" id="txtsecurity1" placeholder="Security answer 1" required="required"  autofocus/>
                           

                             <asp:DropDownList ID="ddlsecurity2" runat="server" Width="77%"></asp:DropDownList>   <br />

                            <br />
                            <input type="text" runat="server" class="text" name="txtsecurity2" id="txtsecurity2" placeholder="Security answer 2" required="required"  autofocus/>
                           

                            <asp:DropDownList ID="ddlsecurity3" runat="server"  Width="77%" ></asp:DropDownList>  <br />


                             <br />
                            <input type="text" runat="server" class="text" name="txtsecurity3" id="txtsecurity3" placeholder="Security answer 3" required="required"  autofocus/>
                           






                          
                          <button class="btn" type="submit" runat="server" id="btnSubmit" onserverclick="btnSubmit_ServerClick">Submit</button> <br />
                           

<%--                              <p class="account"><a href="LoginPage.aspx" style="position:relative; left:-7%">Goto Login Page</a></p>--%>


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