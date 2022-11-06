














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
        url: "../api/Rpms/LoadMajor",
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
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "5%", "targets": 1 }, { "width": "30%", "targets": 2 }, { "width": "35%", "targets": 3 }, { "width": "8%", "targets": 4 }, { "width": "20%", "targets": 5 }, { "width": "5%", "targets": 6 }, { "width": "5%", "targets": 7 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'DepartmentCode' },
                     { 'data': 'MajorCode' },
                 { 'data': 'MajorName' },
                  { 'data': 'DegreeAwarded' },
                 



                  {

                      'render': function (data, type, full, row) {
                          return '<a id=btnBrowseStudents class="link-primary">Browse Students</a>'

                      }


                  },



                   {

                       'render': function (data, type, full, row) {
                           return '<a id=btnBrowseMajor class="link-primary">Browse Major Guide</a>'

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


            $('#table tbody').on('click', "#btnBrowseMajor", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["MajorID"]
                var deptname = Data["DepartmentName"];
                var deptcode = Data["DepartmentCode"];


                var facultyname = Data["FacultyName"];
                var majorcode = Data["MajorCode"];
                var majorname = Data["MajorName"];
                var degreeawarded = Data["DegreeAwarded"];


             //     window.location.href = "SetupMajor.aspx?id=" + id + "";

                Mainform.majorid.value = id


            });





            $('#table tbody').on('click', "#btnBrowseStudents", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["MajorID"]
                var deptname = Data["DepartmentName"];
                var deptcode = Data["DepartmentCode"];


                var facultyname = Data["FacultyName"];
                var majorcode = Data["MajorCode"];
                var majorname = Data["MajorName"];
                var degreeawarded = Data["DegreeAwarded"];


                //     window.location.href = "SetupMajor.aspx?id=" + id + "";

                Mainform.majorid.value = id
            });













            $('#table tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["MajorID"]
                var deptname = Data["DepartmentName"];
                var deptcode = Data["DepartmentCode"];


                var facultyname = Data["FacultyName"];
                var majorcode = Data["MajorCode"];
                var majorname = Data["MajorName"];
                var degreeawarded = Data["DegreeAwarded"];

                $("#ddlfaculty option").each(function () {
                    if ($(this).text() == Data["FacultyName"]) {
                        $(this).attr("selected", "selected")
                    }
                });

                $("#ddldept option").each(function () {
                    if ($(this).text() == Data["DepartmentName"]) {
                        $(this).attr("selected", "selected")
                    }
                });

                  
               

                document.getElementById("txtmajorcode").value = Data["MajorCode"];

                document.getElementById("txtmajorname").value = Data["MajorName"];

                document.getElementById("txtdegree").value = Data["DegreeAwarded"];


                document.getElementById("UpdateValue").value = Data["MajorName"];

                Mainform.majorid.value = id


            });

            $('#table tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["MajorID"]
                var deptname = Data["DepartmentName"];
                var deptcode = Data["DepartmentCode"];


                var facultyname = Data["FacultyName"];
                var majorcode = Data["MajorCode"];
                var majorname = Data["MajorName"];
                var degreeawarded = Data["DegreeAwarded"];



                var Result = confirm("You are about to delete Major name " + majorname)
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
        url: "../api/Rpms/DeleteMajor?id= " + ID + "",
        success: function (data) {
            alert("Major Deleted!");
            ID = 0;
          //  LoadDataTable();


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
    })

}



function LoadDeptDataTable(name) {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/LoadMajorByID?id= " + name + "",
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
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "5%", "targets": 1 }, { "width": "30%", "targets": 2 }, { "width": "35%", "targets": 3 }, { "width": "8%", "targets": 4 }, { "width": "20%", "targets": 5 }, { "width": "5%", "targets": 6 }, { "width": "5%", "targets": 7 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                  { 'data': 'DepartmentCode' },
                   { 'data': 'MajorCode' },
               { 'data': 'MajorName' },
                { 'data': 'DegreeAwarded' },




                {

                    'render': function (data, type, full, row) {
                        return '<a id=btnBrowseStudents class="link-primary">Browse Students</a>'

                    }


                },



                 {

                     'render': function (data, type, full, row) {
                         return '<a id=btnBrowseMajor class="link-primary">Browse Major Guide</a>'

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


            $('#tbldept tbody').on('click', "#btnBrowseMajor", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["MajorID"]
                var deptname = Data["DepartmentName"];
                var deptcode = Data["DepartmentCode"];


                var facultyname = Data["FacultyName"];
                var majorcode = Data["MajorCode"];
                var majorname = Data["MajorName"];
                var degreeawarded = Data["DegreeAwarded"];


                //     window.location.href = "SetupMajor.aspx?id=" + id + "";

                Mainform.majorid.value = id


            });





            $('#tbldept tbody').on('click', "#btnBrowseStudents", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["MajorID"]
                var deptname = Data["DepartmentName"];
                var deptcode = Data["DepartmentCode"];


                var facultyname = Data["FacultyName"];
                var majorcode = Data["MajorCode"];
                var majorname = Data["MajorName"];
                var degreeawarded = Data["DegreeAwarded"];


                //     window.location.href = "SetupMajor.aspx?id=" + id + "";

                Mainform.majorid.value = id
            });













            $('#tbldept tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["MajorID"]
                var deptname = Data["DepartmentName"];
                var deptcode = Data["DepartmentCode"];


                var facultyname = Data["FacultyName"];
                var majorcode = Data["MajorCode"];
                var majorname = Data["MajorName"];
                var degreeawarded = Data["DegreeAwarded"];

                $("#ddlfaculty option").each(function () {
                    if ($(this).text() == Data["FacultyName"]) {
                        $(this).attr("selected", "selected")
                    }
                });

                $("#ddldept option").each(function () {
                    if ($(this).text() == Data["DepartmentName"]) {
                        $(this).attr("selected", "selected")
                    }
                });

                document.getElementById("txtmajorcode").value = Data["MajorCode"];

                document.getElementById("txtmajorname").value = Data["MajorName"];

                document.getElementById("txtdegree").value = Data["DegreeAwarded"];

                document.getElementById("UpdateValue").value = Data["MajorName"];

                Mainform.majorid.value = id


            });

            $('#tbldept tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["MajorID"]
                var deptname = Data["DepartmentName"];
                var deptcode = Data["DepartmentCode"];


                var facultyname = Data["FacultyName"];
                var majorcode = Data["MajorCode"];
                var majorname = Data["MajorName"];
                var degreeawarded = Data["DegreeAwarded"];



                var Result = confirm("You are about to delete Major name " + majorname)
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