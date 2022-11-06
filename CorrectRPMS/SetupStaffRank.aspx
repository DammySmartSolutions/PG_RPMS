<%@ Page Language="C#" Title="Staff Rank Setup" ClientIDMode="Static" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetupStaffRank.aspx.cs" Inherits="CorrectRPMS.SetupStaffRank" %>




<asp:Content ID="content1" ContentPlaceHolderID="MainContent" runat="server">


     

    <div class="content-wrapper" > 

        <div class="row" style="position:relative; left:5%">
            <div class="col-sm-8" style="position:relative; float:right">
                         <h2 style="text-align:center"><%=Page.Title %></h2>

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


        <div class="row" style="position:relative; left:5%">
                <div class="col-3">
 
               
              <label for="txtstaffrank" class="control-label"  style="position:relative; float:right">Staff Rank:</label>

                        </div>

             <div class="col-3">
                                <input type="text" id="txtstaffrank" runat="server" name="txtstaffrank"   class="form-control" required placeholder="Graduate Assistant/Professor" />

             </div>


           
              <div class="col-2" style="position:relative;left:-5% ">
                   

              <asp:Button ID="btnSave"  runat="server" Text="Save"  OnClick="btnSave_Click"      CssClass=" btn btn-primary"   />
                    </div  >
           

             

              
                
         </div>

              


        
            
             




             <div class ="row" style="position:relative; margin-top:2%; left:5%" >
                
      <div class="col-2"></div>

                 <div class="col-4">
                <table id="table"  class="table table-dark table-responsive  table-hover  table-striped"  style="position:relative; width:100%">
                                       <thead>
                                            <tr>
                                             
                                               <th> Staff Rank</th>
                                                 <th> #</th>
											<th >#</th>
                                          
                                            </tr>
                                        </thead>
										<tbody style="width:100%"></tbody>
                                        
                                    </table>
         
       </div>
               

             </div>

    
           </div>




     <asp:HiddenField runat="server" ClientIDMode="Static" ID="staffrankid" />

                          <asp:HiddenField runat="server"  ID="UpdateValue" />

     

  <%--  <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.12.1/css/jquery.dataTables.css">
  
<script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.js"></script>--%>

 
  <%--   <script src="Scripts/jquery-1.11.3.min.js"></script>--%>

   

     <script src="plugins/jquery/jquery.min.js"></script>
<!-- Bootstrap 4 -->


    <script src="Scripts/RPMS/SetupStaffRank.js"></script>
 
 



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