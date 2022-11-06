<%@ Page Language="C#" Title="Staff Setup" ClientIDMode="Static" MasterPageFile="~/Site.Master" EnableEventValidation="false"  AutoEventWireup="true" CodeBehind="SetupStaff.aspx.cs" Inherits="CorrectRPMS.SetupStaff" %>




<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">




    <div class="content-wrapper">

        <div class="row" style="position: relative; left: 5%">
            <div class="col-sm-8" style="position: relative; float: right">
                <h2 style="text-align: center"><%=Page.Title %></h2>

            </div>
        </div>
        <br />

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>




        <%--  html panel div--%>

        <%--<div class="panel panel-default">
    <div class="panel-heading">
      <h3 class="panel-title">Batch Upload</h3>
    </div>
    <div class="panel-body">
      panel content
    </div>
  </div>--%>



        <%-- end of the   html panel div--%>

        <asp:Label id="asplbl" runat="server"  BackColor="Blue" ForeColor="White" Font-Bold="true"  ></asp:Label>
<%--           <label for="lbl" class="control-label" style="position:relative; float:right" runat="server" id="lbl"></label>--%>
      

        
        <div id="tabs" role="tabpanel" style="height:100%">
        <asp:HiddenField runat="server"  ID="tab_index" Value="0"  />
               <ul class="nav nav-tabs responsive" role="tablist" >
                <li><a runat="server" href="#tabs-1" aria-controls="tabs-1" class="btn  btn-primary" role="tab" data-toggle="tab">Create User</a> </li>
                <li><a runat="server" href ="#tabs-2" aria-controls="tabs-2" class="btn btn-primary"  role="tab" data-toggle="tab">Assign Role</a> </li>
                    <li><a runat="server" href ="#tabs-3" aria-controls="tabs-3" class="btn  btn-primary"  role="tab" data-toggle="tab">Reset Password</a> </li>
                   <li><a runat="server" href ="#tabs-4" aria-controls="tabs-4" class="btn btn-primary"  role="tab" data-toggle="tab">Assign Course Coordinator</a> </li>
            </ul>
                  <div class="tab-content" >

               

            <div id="tabs-1" role="tabpanel" class="tab-pane">

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="txtstaffno" class="control-label" style="position:relative; float:right">Staff Number:</label>

                   </div>
                  <div class="col-3">
 
     
                           <input type="text" id="txtstaffno" runat="server" name="txtstaffno"  placeholder="Staff ID" class="form-control"   />

                     


             </div>

                 



             </div>

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="ddltitle" class="control-label" style="position:relative; float:right">Title:</label>

                   </div>
                  <div class="col-3">
 
     
                        <asp:DropDownList ID="ddltitle" runat="server"  CssClass ="form-control"  Width="74%" >
                           <asp:ListItem Enabled ="true" Value="0" Text ="Select Title" ></asp:ListItem>
                           <asp:ListItem Enabled ="true" Value="1" Text ="Miss" ></asp:ListItem>
                           <asp:ListItem Enabled ="true" Value="2" Text ="Mr" ></asp:ListItem>
                           <asp:ListItem Enabled ="true" Value="3" Text ="Mrs" ></asp:ListItem>
                           <asp:ListItem Enabled ="true" Value="4" Text ="Dr" ></asp:ListItem>
                           <asp:ListItem Enabled ="true" Value="5" Text ="Prof" ></asp:ListItem>
       </asp:DropDownList>
        

                     


             </div>

                 



             </div>



                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="txtsurname" class="control-label" style="position:relative; float:right">Surname:</label>

                   </div>
                  <div class="col-3">
 
     
                 <input type="text" id="txtsurname" runat="server" name="txtsurname"  placeholder="Staff surname" class="form-control"   />

     

                     


             </div>

                 



             </div>

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="txtfirstname" class="control-label" style="position:relative; float:right">First Name:</label>

                   </div>
                  <div class="col-3">
 
     
                 <input type="text" id="txtfirstname" runat="server" name="txtfirstname"  placeholder="Staff First Name" class="form-control"   />

     

                     


             </div>

                 



             </div>


                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="txtemail" class="control-label" style="position:relative; float:right">Official Email:</label>

                   </div>
                  <div class="col-3">
 
     
                 <input type="email" id="txtemail" runat="server" name="txtemail"  placeholder="ict@oouagoiwoye.edu.ng" class="form-control"   />

     

                     


             </div>

                 



             </div>


                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="txtphone" class="control-label" style="position:relative; float:right">Mobile:</label>

                   </div>
                  <div class="col-3">
 
     
                 <input type="tel" id="txtphone" runat="server" name="txtphone"  placeholder="08070007000" class="form-control"   />

     

                     


             </div>

                 



             </div>

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="ddlsex" class="control-label" style="position:relative; float:right">Sex:</label>

                   </div>
                  <div class="col-3">
 
     
                        <asp:DropDownList ID="ddlsex" runat="server"  CssClass ="form-control" Width="74%" >
                           <asp:ListItem Enabled ="true" Value="0" Text ="Select Sex" ></asp:ListItem>
                           <asp:ListItem Enabled ="true" Value="1" Text ="Male" ></asp:ListItem>
                           <asp:ListItem Enabled ="true" Value="2" Text ="Female" ></asp:ListItem>
                           
                        </asp:DropDownList>
        

                     


             </div>

                 



             </div>
                <asp:UpdatePanel ID="upSetSession" runat="server">
            <ContentTemplate>

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="ddlFaculty" class="control-label" style="position:relative; float:right">Faculty:</label>

                   </div>
                  <div class="col-3">
 
     
                        <asp:DropDownList ID="ddlFaculty" runat="server"  CssClass ="form-control" Width="74%" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" >
                                                  
                        </asp:DropDownList>
        

                     


             </div>

                 



             </div>


                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="ddlDept" class="control-label" style="position:relative; float:right">Department:</label>

                   </div>
                  <div class="col-3">
 
     
                        <asp:DropDownList ID="ddlDept" runat="server"  CssClass ="form-control" Width="74%" >
                                                  
                        </asp:DropDownList>
        

                     


             </div>

                 



             </div>



                 </ContentTemplate>
        
        </asp:UpdatePanel>


                 <div class="row" style="position:relative; margin-top:1%; left:5%">

                                              <div class="col-sm-3" ></div>
                                                  <div class="col-sm-3" style="position:relative">

                <asp:Button ID="btnSave"  runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"  />


                                                 </div>


             </div>

                
                <div class ="row" style="position:relative; margin-top:0%; left:0%"  id="divStaffList">
               
      
                      
                <table id="tblStaffList"  class="table table-dark table-responsive  table-hover  table-striped"  style="position:relative; width:100%">
                                       <thead>
                                            <tr>
                                             
                                               <th> Staff No</th>
                                                 <th>Title</th>
                                                 <th> Surname</th>
                                                 <th> First Name </th>
                                                <th> Official Email</th>
												 <th>Mobile</th>
                                                 <th>Sex</th>
                                                <th>Department</th>
                                                 <th>Role(s)</th>
                                               
											<th >#</th>
                                          <th >#</th>
                                          
                                            </tr>
                                        </thead>
										<tbody style="width:100%"></tbody>
                                        
                                    </table>
         
               

             </div>



            </div>
           



            <div id="tabs-2" role="tabpanel" class="tab-pane">

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="txtrolestaffno" class="control-label" style="position:relative; float:right">Staff Number:</label>

                   </div>
                  <div class="col-3">
 
     
                           <input type="text" id="txtrolestaffno" runat="server" name="txtrolestaffno"  placeholder="Staff ID" class="form-control"   />

                     


             </div>

                 



             </div>

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="ddlrole" class="control-label" style="position:relative; float:right">Role:</label>

                   </div>
                  <div class="col-3">
 
     
                        <asp:DropDownList ID="ddlrole" runat="server"  CssClass ="form-control"  Width="74%" >
                                                  
                         </asp:DropDownList>
        

                     


             </div>

                 



             </div>



                
                 <div class="row" style="position:relative; margin-top:1%; left:5%">

                                              <div class="col-sm-3" ></div>
                                                  <div class="col-sm-3" style="position:relative">

                    <asp:Button ID="btnSaveRole" runat="server" Text="Add Role"    CssClass="btn btn-primary"  OnClick="btnSaveRole_Click"  />

                                                 </div>


             </div>

                
                <div class ="row" style="position:relative; margin-top:1%; left:0%"  id="divStaffRole">
               
                 <div class="col-2"></div>

                    <div class="col-5">
                      
                <table id="tblStaffRole"  class="table table-dark table-responsive  table-hover  table-striped"  style="position:relative; width:90%">
                                       <thead>
                                            <tr>
                                             
                                               <th> Staff No</th>
                                                 <th>Role</th>
                                                 
											<th >#</th>
                                          <th >#</th>
                                          
                                            </tr>
                                        </thead>
										<tbody style="width:100%"></tbody>
                                        
                                    </table>
         
               </div>

             </div>



            </div>


            <div id="tabs-3" role="tabpanel" class="tab-pane">

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="txtResetStaffNo" class="control-label" style="position:relative; float:right">Staff Number:</label>

                   </div>
                  <div class="col-3">
 
     
                           <input type="text" id="txtResetStaffNo" runat="server" name="txtResetStaffNo"  placeholder="Staff ID" class="form-control"   />

                     


             </div>

                 

                     <div class="col-3" style="position:relative; left:-7%">

                    <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password"    CssClass="btn btn-primary"  OnClick="btnResetPassword_Click"  />

                                                 </div>


                    <div class="col-3" style="position:relative; left:-21%">

                    <asp:Button ID="btnClearSecurity" runat="server" Text="Clear Security Question"    CssClass="btn btn-primary"  OnClick="btnClearSecurity_Click" />

                                                 </div>


             </div>

                



                
                 

                
                



            </div>

                      
                      <div id="tabs-4" role="tabpanel" class="tab-pane">


                          <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="txtAssignStaffNo" class="control-label" style="position:relative; float:right">Staff Number:</label>

                   </div>
                  <div class="col-3">
 
     
                           <input type="text" id="txtAssignStaffNo" runat="server" name="txtAssignStaffNo"  placeholder="Staff ID" class="form-control"   />

                     


             </div>

                 



             </div>



            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="ddlAssignFaculty" class="control-label" style="position:relative; float:right">Faculty:</label>

                   </div>
                  <div class="col-3">
 
     
                        <asp:DropDownList ID="ddlAssignFaculty" runat="server"  CssClass ="form-control" Width="74%" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignFaculty_SelectedIndexChanged" >
                                                  
                        </asp:DropDownList>
        

                     


             </div>

                 



             </div>


                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="ddlAssignDept" class="control-label" style="position:relative; float:right">Department:</label>

                   </div>
                  <div class="col-3">
 
     
                        <asp:DropDownList ID="ddlAssignDept" runat="server"  CssClass ="form-control" Width="74%" AutoPostBack="true" OnSelectedIndexChanged="ddlAssignDept_SelectedIndexChanged" >
                                                  
                        </asp:DropDownList>
        

                     


             </div>

                 



             </div>


                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="ddlcourse" class="control-label" style="position:relative; float:right">Course:</label>

                   </div>
                  <div class="col-3">
 
                <asp:DropDownList ID="ddlcourse" runat="server"  CssClass ="form-control"  Width="74%" >
                           
                        </asp:DropDownList>
        
                     


             </div>

                 



             </div>




                 </ContentTemplate>
        
        </asp:UpdatePanel>



                

                <div class="row" style="position:relative; margin-top:1%; left:5%">
                   <div class="col-3">
                      <label for="ddlsesssemester" class="control-label" style="position:relative; float:right">Session_Semester:</label>

                   </div>
                  <div class="col-3">
 
     
                        <asp:DropDownList ID="ddlsesssemester" runat="server"  CssClass ="form-control"  Width="74%" >
                           
                        </asp:DropDownList>
        

                     


             </div>

                 



             </div>



                
            


                          


                 <div class="row" style="position:relative; margin-top:1%; left:5%">

                                              <div class="col-sm-3" ></div>
                                                  <div class="col-sm-3" style="position:relative">

                <asp:Button ID="btnAssignSave"  runat="server" Text="Save" OnClick="btnAssignSave_Click" CssClass="btn btn-primary"  />


                                                 </div>


             </div>

                
                <div class ="row" style="position:relative; margin-top:0%; left:3%"  id="divAssignCourses">
               
                <div class="col-2"></div>
                     <div class="col-7">
                <table id="tblAssign"  class="table table-dark table-responsive  table-hover  table-striped"  style="position:relative; width:100%">
                                       <thead>
                                            <tr>
                                             
                                               <th> Staff No</th>
                                                 <th>Course</th>
                                                 <th> Department</th>
                                                 <th> Session_Semester </th>
                                                
                                               
											<th >#</th>
                                          <th >#</th>
                                          
                                            </tr>
                                        </thead>
										<tbody style="width:100%"></tbody>
                                        
                                    </table>
         
               </div>  

             </div>



            </div>
            


                      </div>
            </div> 


        



        


        









        


        




        







    </div>




    <asp:HiddenField runat="server" ClientIDMode="Static" ID="staffid" />

      <asp:HiddenField runat="server" ClientIDMode="Static" ID="staffroleid" />
       <asp:HiddenField runat="server" ClientIDMode="Static" ID="AssignCourseid" />

       <asp:HiddenField ID="selected_tab" runat="server" />


    <%--  <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.12.1/css/jquery.dataTables.css">
  
<script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>--%>


    <%--   <script src="Scripts/jquery-1.11.3.min.js"></script>--%>

    <link href="plugins/jquery-ui/jquery-ui.css" rel="stylesheet" />
    <script src="plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap 4 -->
    <script src="plugins/jquery-ui/jquery-ui.js"></script>

  
    <script src="Scripts/RPMS/SetupStaff.js"></script>



    <script type="text/javascript">
      //  this one is working
      



        var value = document.getElementById("selected_tab").value;

        $(function () {
            $("#tabs").tabs({

                active: value

            });


        });

        </script>





    <%--  html panel css and script--%>
    <%-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>--%>




    <%--    End of the html panel css and script--%>





    <%--<script src="Scripts/jquery-1.11.3.js"></script>--%>


    <%-- <link href="https://cdn.datatables.net/1.11.4/css/jquery.dataTables.min.css" rel="stylesheet" />
   <link href="https://cdn.datatables.net/buttons/2.2.2/css/buttons.dataTables.min.css" rel="stylesheet" />
   <script src="https://code.jquery.com/jquery-3.5.1.js"></script>
 <script src="https://cdn.datatables.net/1.11.4/js/jquery.dataTables.min.js"></script>
 <script src="https://cdn.datatables.net/buttons/2.2.2/js/dataTables.buttons.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
 <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
 <script src="https://cdn.datatables.net/buttons/2.2.2/js/buttons.html5.min.js"></script>
 <script src="https://cdn.datatables.net/buttons/2.2.2/js/buttons.print.min.js"></script>--%>
</asp:Content>