















//id = 0;
ID = 0;
const Mainform = document.getElementById("mainform")
window.onload = function onloadTable() {

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
   

    document.getElementById("txtstartdate").value = today;
    document.getElementById("txtsessionenddate").value = today;
  

    LoadDataTable();
}

function LoadDataTable() {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/LoadSession",
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
                "columnDefs": [{ "width": "10%", "targets": 0 }, { "width": "10%", "targets": 1 }, { "width": "10%", "targets": 2 }, { "width": "10%", "targets": 3 }, { "width": "10%", "targets": 4 }, { "width": "5%", "targets": 5 }, { "width": "5%", "targets": 6 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'SessionOrder' },
                     { 'data': 'Session' },

                     {
                         'data': 'SessionStartDate',
                         'render': function (data) {
                             if (data == null) {
                                 return data;

                             }

                             else {
                                 new_data = data.split("T");
                                 new_data[0] = moment(new_data[0]).format('DD-MM-YYYY');
                                 return new_data[0];

                             }
                         }
                     },



                     {
                         'data': 'SessionEndDate',
                         'render': function (data) {
                             if (data == null) {
                                 return data;

                             }

                             else {
                                 new_data = data.split("T");
                                 new_data[0] = moment(new_data[0]).format('DD-MM-YYYY');
                                 return new_data[0];

                             }
                         }
                     },




                     {

                         'render': function (data, type, full, row) {
                             return '<a id=btnBrowse class="link  link-primary">Browse Students</a>'

                         }


                     },



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



            $('#table tbody').on('click', "#btnBrowse", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["SessionID"]
                document.getElementById("txtfacultytype").value = Data["FacultyType"];

            //    window.location.href =""

                Mainform.sessionid.value = id


            });







            $('#table tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["SessionID"]
                document.getElementById("txtsessionorder").value = Data["SessionOrder"];


                document.getElementById("txtsession").value = Data["Session"];

                var startdate = Data["SessionStartDate"];

                var corradm = moment(startdate).format('YYYY-MM-DD')
                document.getElementById("txtstartdate").value = corradm;



                var enddate = Data["SessionEndDate"];

                var newenddate = moment(enddate).format('YYYY-MM-DD')
                document.getElementById("txtsessionenddate").value = newenddate;

                document.getElementById("UpdateValue").value = Data["Session"];



                Mainform.sessionid.value = id


            });

            $('#table tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["SessionID"]
                var facultyname = Data["Session"];



                var Result = confirm("You are about to delete " + facultyname + " Academic Session")
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
        url: "../api/Rpms/DeleteSession?id= " + ID + "",
        success: function (data) {
            alert("Session Deleted!");
            ID = 0;
            LoadDataTable();



        }
    })

}