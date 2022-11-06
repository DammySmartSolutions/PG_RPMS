<%@ Page Language="C#" Title="Institution Setup" ClientIDMode="Static" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SchoolSetup.aspx.cs" Inherits="CorrectRPMS.SchoolSetup" %>

<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">




    <div class="content-wrapper" > 

        <div class="row" style="position:relative; left:5%">
            <div class="col-sm-8" style="position:relative; float:right">
                         <h2 style="text-align:center"><%=Page.Title %></h2>

            </div>
        </div>
        <br />
        
      

              <div class="row" style="position:relative; left:5%">
                <div class="col-3">
 
               <div class="form-group">
              <label for="myfile" class="control-label">Select Logo</label>
               <input type="file" id="myfile" runat="server" name="myfile"   class ="file-body" />

                        </div>


             </div>
              <div class="col-1" style="position:relative;float:right;  left:3%">
                   <div class="form-group">
                               <label for="btnupload" class="control-label"></label>

                    <asp:Button ID="btnupload" type="submit" runat="server" Text="Upload Logo" OnClick="btnupload_Click" CssClass=" btn btn-primary"   />
                    </div>
              </div>

             <div class="col-1"  style="display: none; position:relative;float:right;  left:3%">

                  <div class="form-group">
                       <label for="btnEditLogo" class="control-label"></label>
                <asp:Button ID="btnEditLogo" type="submit" runat="server" Text="Edit Logo" OnClick="btnEditLogo_Click" CssClass="form-control; btn btn-default btn-primary" />
            </div>
                 </div>

              <div class="col-5" style="position:relative;float:right;  left:10%">
                   <div class="form-group">
                        <label for="SchoolLogo" class="control-label"></label>
                  <img alt="Logo" runat="server" id="SchoolLogo" width="100" height="100" src=""  />

                    </div>  
              </div>
                
         </div>


        
            
             <div class="row" style="position:relative; margin-top:2%; left:5%">
                  <div class="col-3">
 
      <div class="form-group">
              <label for="txtname" class="control-label">Name</label>
                          <input type="text" id="txtname" runat="server" name="txtname" placeholder="Institution Name" class="form-control" />

                        </div>


             </div>

                 

                 <div class="col-3" style="position:relative;float:right;  left:3%">
 
      <div class="form-group">
              <label for="txtaddress" class="control-label">Address</label>
                          <input type="text" id="txtaddress" runat="server" name="txtaddress"  placeholder="Institution Address" class="form-control" />

                        </div>


             </div>



             </div>





             <div class="row" style="position:relative; margin-top:2%; left:5%">
                  <div class="col-3">
 
      <div class="form-group">
              <label for="txtphone" class="control-label">Phone</label>
                           <input type="tel" id="txtphone" runat="server" name="txtphone"  placeholder="08077777777" class="form-control" />

                        </div>


             </div>

                 <div class="col-3" style="position:relative;float:right;  left:3%">
 
      <div class="form-group">
              <label for="txtemail" class="control-label">Email</label>
                               <input type="email" id="txtemail" runat="server" name="txtemail"  placeholder="google@gmail.com" class="form-control" />

                        </div>


             </div>



             </div>

             
             <div class="row" style="position:relative; margin-top:2%; left:5%">
                                             <div class="col-sm-3" style="position:relative;float:right">
                                                 </div>
                                                  <div class="col-sm-2" style="position:relative">

                <asp:Button ID="btnSave"  runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"  />


                                                 </div>


             </div>




             <div class ="row" style="position:relative; margin-top:2%; left:5%" >
                
      
                <table id="table"  class="table table-dark table-responsive  table-hover  table-striped"  style="position:relative; width:90%">
                                       <thead>
                                            <tr>
                                             
                                               <th> Name</th>
                                                 <th> Address</th>
												 <th> Phone</th>
                                                <th> Email</th>
											<th >#</th>
                                            <th >#</th>
                                            </tr>
                                        </thead>
										<tbody style="width:100%"></tbody>
                                        
                                    </table>
         
       
               

             </div>

    
           </div>




     <asp:HiddenField runat="server" ClientIDMode="Static" ID="schid" />
       <asp:HiddenField runat="server" ClientIDMode="Static" ID="schlogo" />
     <asp:HiddenField runat="server" ClientIDMode="Static" ID="companyid" />

  <%--  <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.12.1/css/jquery.dataTables.css">
  
<script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>--%>

 
  <%--   <script src="Scripts/jquery-1.11.3.min.js"></script>--%>

   

     <script src="plugins/jquery/jquery.min.js"></script>
<!-- Bootstrap 4 -->



    <script src="Scripts/RPMS/SchoolSetup.js"></script>


 

        
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