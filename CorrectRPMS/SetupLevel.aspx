<%@ Page Language="C#" Title="Level Setup" ClientIDMode="Static" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetupLevel.aspx.cs" Inherits="CorrectRPMS.SetupLevel" %>





<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">




    <div class="content-wrapper">

        <div class="row" style="position: relative; left: 5%">
            <div class="col-sm-8" style="position: relative; float: right">
                <h2 style="text-align: center"><%=Page.Title %></h2>

            </div>
        </div>
        <br />



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

                <asp:Label id="asplbl" runat="server"  BackColor="Blue" ForeColor="White" Font-Bold="true"  ></asp:Label> <br />


        <div class="row" style="position: relative; left: 5%">
            <div class="col-3">


                <label for="txtlevelname" class="control-label" style="position: relative; float: right">Level Name:</label>

            </div>

            <div class="col-3">
                <input type="text" id="txtlevelname" runat="server" name="txtlevelname" class="form-control" required placeholder="YEAR-1" />



            </div>







        </div>



        <div class="row" style="position: relative; margin-top:1%; left: 5%">
            <div class="col-3">


                <label for="txtfulldescription" class="control-label" style="position: relative; float: right">Full Description:</label>

            </div>

            <div class="col-3">


                <input type="text" id="txtfulldescription" runat="server" name="txtfulldescription" class="form-control" required placeholder="YEAR-1" />

            </div>



            





        </div>




        <div class="row" style="position: relative; margin-top:1%; left: 5%">
            <div class="col-3">


                <label for="txtlevelorder" class="control-label" style="position: relative; float: right">Level Order:</label>

            </div>

            <div class="col-3">


                <input type="text" id="txtlevelorder" runat="server" name="txtlevelorder" class="form-control" required placeholder="1" />

            </div>



            





        </div>



        <div class="row" style="position: relative; margin-top:1%; left: 5%">
            <div class="col-3">


                <label for="chkabsent" class="control-label" style="position: relative; float: right">Is Absent:</label>

            </div>

            <div class="col-3">


                <input type="checkbox" id="chkabsent" runat="server" name="chkabsent"  class="form-control"   />

            </div>



            





        </div>



        <div class="row" style="position: relative; margin-top:1%; left: 5%">
            <div class="col-3">


                <label for="chkreturn" class="control-label" style="position: relative; float: right">Will Return:</label>

            </div>

            <div class="col-3">


                <input type="checkbox" id="chkreturn" runat="server" name="chkreturn"  class="form-control"   />

            </div>



            





        </div>



        



        <div class="row" style="position:relative; margin-top:1%; left:5%">

                                              <div class="col-sm-3" ></div>
                                                  <div class="col-sm-3" style="position:relative">

                <asp:Button ID="btnSave"  runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"  />


                                                 </div>


             </div>

        



        <div class ="row" style="position:relative; margin-top:0%; left:15%"  id="tablediv">
                
      
                <table id="table"  class="table table-dark table-responsive  table-hover  table-striped"  style="position:relative; width:100%">
                                       <thead>
                                            <tr>
                                             
                                              <th>Level Name</th>
                                                 <th>Full Description</th>
                                                 <th>Level Order</th>
                                                 <th>Is Absent</th>
                                                 <th>Will Return</th>
                                                 <th>#</th>
                                                <th>#</th>
                                                <th>#</th>
                                            </tr>
                                        </thead>
										<tbody style="width:100%"></tbody>
                                        
                                    </table>
         
       
               

             </div>




            <%--<div class ="row" style="position:relative; margin-top:2%; left:15%" id="tbldeptdiv" >
                
      
                <table id="tbldept"  class="table table-dark table-responsive  table-hover  table-striped"  style="position:relative; width:90%">
                                       <thead>
                                            <tr>
                                             
                                               <th>Min Score</th>
                                                 <th>Max Score</th>
                                                 <th>Grade Point</th>
                                                 <th>Grade Letter</th>
                                                 <th>Grade Passed</th>
                                                 <th>Grading System</th>
                            
                                                <th>#</th>
                                                <th>#</th>
                                            </tr>
                                        </thead>
										<tbody style="width:100%"></tbody>
                                        
                                    </table>
         
       
               

             </div>--%>


        



    </div>




    <asp:HiddenField runat="server" ClientIDMode="Static" ID="levelid" />
                          <asp:HiddenField runat="server"  ID="UpdateValue" />



    <%--  <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.12.1/css/jquery.dataTables.css">
  
<script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>--%>


    <%--   <script src="Scripts/jquery-1.11.3.min.js"></script>--%>



    <script src="plugins/jquery/jquery.min.js"></script>
    <!-- Bootstrap 4 -->


    <script src="Scripts/RPMS/SetupLevel.js"></script>
   






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
    