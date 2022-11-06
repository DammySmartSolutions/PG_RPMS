






//id = 0;
ID = 0;
const Mainform = document.getElementById("mainform")
window.onload = function onloadTable() {

    LoadDataTable();
}

function LoadDataTable() {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/LoadSchool",
        success: function (data) {


          $('#table').DataTable().destroy();

            var datatableVariable = $('#table').DataTable({

             
                 data: data,
                "bFilter": true,
                "bInfo": false,
                "searching": true,
                "ordering": true,
            
                
                "bDisplayLength": 1000,
                "bLengthChange": true,
               "lengthMenu": [[5, 10, 15, 20, 25, 50, -1], [5, 10, 25, 50, "All"]],
               "columnDefs": [{ "width": "10%", "targets": 0 }, { "width": "10%", "targets": 1 }, { "width": "5%", "targets": 2 }, { "width": "5%", "targets": 3 }, { "width": "5%", "targets": 4 }, { "width": "5%", "targets": 5 }],
                pageLength: 50,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'Name' },
                     { 'data': 'Address' },
                     { 'data': 'Phone' },
                      { 'data': 'Email' },
                {

                    'render': function (data, type, full, row) {
                        return '<a id=btnEdit class="btn  btn-primary">Edit</a>'

                    }


                },  

                {

                    'render': function (data, type, full, row) {
                        return '<a  class="btn  btn-primary" id=btnDelete>Delete</a>'

                    }


                },



                ]



            })
            ;

            datatableVariable.buttons(0, null).container().appendTo(datatableVariable.table().container());
            $('#table tfoot th').each(function () {
                var placeHolderTitle = $('#table thead th').eq($(this).index()).text();
                $(this).html('<input type="text" class="form-control input input-sm" placeholder = "Search ' + placeHolderTitle + '" />');
            });
            datatableVariable.columns().every(function () {
                var column = this;
                $(this.footer()).find('input').on('keyup change', function () {
                    column.search(this.value).draw();
                });
            });
            $('.showHide').on('click', function () {
                var tableColumn = datatableVariable.column($(this).attr('data-columnindex'));
                tableColumn.visible(!tableColumn.visible());
            });

            $('#table tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["SchoolID"]
                document.getElementById("txtname").value = Data["Name"];
                document.getElementById("txtaddress").value = Data["Address"];
                document.getElementById("txtphone").value = Data["Phone"];
                document.getElementById("txtemail").value = Data["Email"];
                document.getElementById("schlogo").value = Data["Logo"];

                var picpath = Data["Logo"];
                $("#SchoolLogo").attr("src", picpath);
                var comid = Data["CompanyID"];
             

                Mainform.schid.value = id


            });

            $('#table tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["SchoolID"]
             
              var name = Data["Name"];

              var Result = confirm("You are about to delete " + name)
                if (Result) {

                    DeleteItem(ID);
                }

            });


            // if it is a datatable


        }
        ,
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(xhr.responseText);
            alert(thrownError);
        }

    });

}







function DeleteItem(ID) {


    var CheckCookieValue = document.cookie.split("=");
    var getcomid = CheckCookieValue[2];
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/DeleteSchool?id= " + ID + "",
        success: function (data) {
            alert("Record Deleted!");
            ID = 0;
            LoadDataTable();



        }
    })

}