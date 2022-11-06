﻿

















//id = 0;
ID = 0;
const Mainform = document.getElementById("mainform")
window.onload = function onloadTable() {

    let params = (new URL(document.location)).searchParams;
    let name = params.get("id");

    if (name == null) {
        LoadDataTable();
        document.getElementById("tbldeptdiv").style.display = "none";

    }

    else {
        document.getElementById("tablediv").style.display = "none";
        LoadDeptDataTable(name);

    }


}

function LoadDataTable() {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/LoadGrading",
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
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "5%", "targets": 1 }, { "width": "5%", "targets": 2 }, { "width": "5%", "targets": 3 }, { "width": "5%", "targets": 4 }, { "width": "10%", "targets": 5 }, { "width": "5%", "targets": 6 }, { "width": "5%", "targets": 7 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'MinScore' },
                     { 'data': 'MaxScore' },
                 { 'data': 'GradePoint' },
                  { 'data': 'GradeLetter' },
                  { 'data': 'GradePassed' },
                  { 'data': 'GradingSystemType' },
               



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
                id = Data["GradingID"]
                document.getElementById("txtminimun").value = Data["MinScore"];
                document.getElementById("txtmaximum").value = Data["MaxScore"];


                document.getElementById("txtgradepoint").value = Data["GradePoint"];
                document.getElementById("txtgradeletter").value = Data["GradeLetter"];

                document.getElementById("chkbox").checked = Data["GradePassed"];



                $("#ddlgradingSystem option").each(function () {
                    if ($(this).text() == Data["GradingSystemType"]) {
                        $(this).attr("selected", "selected")
                    }
                });



                document.getElementById("UpdateValue").value = Data["GradeLetter"];

                Mainform.gradingid.value = id


            });

            $('#table tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["GradingID"]
                var facultyname = Data["GradeLetter"];



                var Result = confirm("You are about to delete grade letter " + facultyname)
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
        url: "../api/Rpms/DeleteGrading?id= " + ID + "",
        success: function (data) {
            alert("Grade Deleted!");
            ID = 0;


            let params = (new URL(document.location)).searchParams;
            let name = params.get("id");

            if (name == null) {
                LoadDataTable();
                document.getElementById("tbldeptdiv").style.display = "none";

            }

            else {
                document.getElementById("tablediv").style.display = "none";
                LoadDeptDataTable(name);

            }

            //  LoadDataTable();



        }
    })

}



function LoadDeptDataTable(name) {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/LoadGradingByID?id= " + name + "",
        success: function (data) {


            $('#tbldept').DataTable().destroy();

            var datatableVariable = $('#tbldept').DataTable({


                data: data,
                "bFilter": true,
                "bInfo": false,
                "searching": true,
                "ordering": true,


                "bDisplayLength": 1000,
                "bLengthChange": true,
                "lengthMenu": [[5, 10, 15, 20, 25, 50, -1], [5, 10, 25, 50, "All"]],
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "5%", "targets": 1 }, { "width": "5%", "targets": 2 }, { "width": "5%", "targets": 3 }, { "width": "5%", "targets": 4 }, { "width": "10%", "targets": 5 }, { "width": "5%", "targets": 6 }, { "width": "5%", "targets": 7 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'MinScore' },
                     { 'data': 'MaxScore' },
                 { 'data': 'GradePoint' },
                  { 'data': 'GradeLetter' },
                  { 'data': 'GradePassed' },
                  { 'data': 'GradingSystemType' },




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
            $('#tbldept tfoot th').each(function () {
                var placeHolderTitle = $('#tbldept thead th').eq($(this).index()).text();
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


   



            $('#tbldept tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["GradingID"]
                document.getElementById("txtminimun").value = Data["MinScore"];
                document.getElementById("txtmaximum").value = Data["MaxScore"];


                document.getElementById("txtgradepoint").value = Data["GradePoint"];
                document.getElementById("txtgradeletter").value = Data["GradeLetter"];

                document.getElementById("chkbox").checked = Data["GradePassed"];



                $("#ddlgradingSystem option").each(function () {
                    if ($(this).text() == Data["GradingSystemType"]) {
                        $(this).attr("selected", "selected")
                    }
                });


                document.getElementById("UpdateValue").value = Data["GradeLetter"];

                Mainform.gradingid.value = id



            });

            $('#tbldept tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["GradingID"]
                var facultyname = Data["GradeLetter"];



                var Result = confirm("You are about to delete grade letter " + facultyname)
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