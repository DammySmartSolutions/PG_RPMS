﻿





















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
        url: "../api/Rpms/LoadSemester",
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
                "columnDefs": [{ "width": "10%", "targets": 0 }, { "width": "5%", "targets": 1 }, { "width": "5%", "targets": 2 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'Semester' },







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
                id = Data["SemesterID"]
                document.getElementById("txtsemester").value = Data["Semester"];

                document.getElementById("UpdateValue").value = Data["Semester"];


                Mainform.semesterid.value = id


            });

            $('#table tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["SemesterID"]
                var facultyname = Data["Semester"];



                var Result = confirm("You are about to delete " + facultyname + " Semester")
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
        url: "../api/Rpms/DeleteSemester?id= " + ID + "",
        success: function (data) {
            alert("Semester Deleted!");
            ID = 0;
            LoadDataTable();



        }
    })

}