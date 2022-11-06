












//id = 0;
ID = 0;
const Mainform = document.getElementById("mainform")
window.onload = function onloadTable() {

    let params = (new URL(document.location)).searchParams;
    let name = params.get("id");

    if (name == null)
    {
        LoadDataTable();
        document.getElementById("tbldeptdiv").style.display = "none";

    }

    else

    {
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
        url: "../api/Rpms/LoadDept",
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
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "20%", "targets": 1 }, { "width": "30%", "targets": 2 }, { "width": "3%", "targets": 3 }, { "width": "8%", "targets": 4 }, { "width": "8%", "targets": 5 }, { "width": "8%", "targets": 6 }, { "width": "8%", "targets": 7 }, { "width": "5%", "targets": 8 }, { "width": "5%", "targets": 9 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'DeptCode' },
                     { 'data': 'DeptName' },
                 { 'data': 'FacultyName' },
                  { 'data': 'DeptID' },
                 {

                     'render': function (data, type, full, row) {
                         return '<a id=btnBrowseMajor class="link-primary">Browse Major</a>'

                     }


                 },



                  {

                      'render': function (data, type, full, row) {
                          return '<a id=btnBrowseStudents class="link-primary">Browse Students</a>'

                      }


                  },



                   {

                     'render': function (data, type, full, row) {
                         return '<a id=btnBrowseCourses class="link-primary">Browse Courses</a>'

                     }


                 },



                 {

                     'render': function (data, type, full, row) {
                         return '<a id=btnBrowseDeptHead class="link-primary">Browse Department Heads</a>'

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
                id = Data["DeptID"]
                var deptname = Data["DeptName"];
                var deptcode = Data["DeptCode"];


                var facultyname = Data["FacultyName"];
                var facultycode = Data["FacultyCode"];
                var facultytype = Data["FacultyType"];


            
                window.location.href = "SetupMajor.aspx?id=" + id + "";

                Mainform.deptid.value = id


            });





            $('#table tbody').on('click', "#btnBrowseStudents", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["DeptID"]
                var deptname = Data["DeptName"];
                var deptcode = Data["DeptCode"];


                var facultyname = Data["FacultyName"];
                var facultycode = Data["FacultyCode"];
                var facultytype = Data["FacultyType"];





                Mainform.deptid.value = id

            });





            $('#table tbody').on('click', "#btnBrowseCourses", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["DeptID"]
                var deptname = Data["DeptName"];
                var deptcode = Data["DeptCode"];


                var facultyname = Data["FacultyName"];
                var facultycode = Data["FacultyCode"];
                var facultytype = Data["FacultyType"];


                window.location.href = "SetupCourses.aspx?id=" + id + "";


                Mainform.deptid.value = id


            });


            $('#table tbody').on('click', "#btnBrowseDeptHead", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["DeptID"]
                var deptname = Data["DeptName"];
                var deptcode = Data["DeptCode"];


                var facultyname = Data["FacultyName"];
                var facultycode = Data["FacultyCode"];
                var facultytype = Data["FacultyType"];





                Mainform.deptid.value = id

            });








            $('#table tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["DeptID"]
                document.getElementById("txtdeptcode").value = Data["DeptCode"];
                document.getElementById("txtdept").value = Data["DeptName"];

                $("#ddlfaculty option").each(function () {
                    if ($(this).text() == Data["FacultyName"]) {
                        $(this).attr("selected", "selected")
                    }
                });

                document.getElementById("UpdateValue").value = Data["DeptName"];


                Mainform.deptid.value = id


            });

            $('#table tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["DeptID"]
                var facultyname = Data["DeptName"];



                var Result = confirm("You are about to delete department of " + facultyname)
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
        url: "../api/Rpms/DeleteDept?id= " + ID + "",
        success: function (data) {
            alert("Department Deleted!");
            ID = 0;


            let params = (new URL(document.location)).searchParams;
            let name = params.get("id");

            if (name == null)
            {
                LoadDataTable();
                document.getElementById("tbldeptdiv").style.display = "none";

            }

            else
            {
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
        url: "../api/Rpms/LoadDeptByID?id= " + name + "",
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
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "20%", "targets": 1 }, { "width": "30%", "targets": 2 }, { "width": "3%", "targets": 3 }, { "width": "8%", "targets": 4 }, { "width": "8%", "targets": 5 }, { "width": "8%", "targets": 6 }, { "width": "8%", "targets": 7 }, { "width": "5%", "targets": 8 }, { "width": "5%", "targets": 9 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'DeptCode' },
                     { 'data': 'DeptName' },
                 { 'data': 'FacultyName' },
                { 'data': 'DeptID' },

                 {

                     'render': function (data, type, full, row) {
                         return '<a id=btnBrowseMajor class="link-primary">Browse Major</a>'

                     }


                 },



                  {

                      'render': function (data, type, full, row) {
                          return '<a id=btnBrowseStudents class="link-primary">Browse Students</a>'

                      }


                  },



                   {

                       'render': function (data, type, full, row) {
                           return '<a id=btnBrowseCourses class="link-primary">Browse Courses</a>'

                       }


                   },



                 {

                     'render': function (data, type, full, row) {
                         return '<a id=btnBrowseDeptHead class="link-primary">Browse Department Heads</a>'

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
                id = Data["DeptID"]
                var deptname = Data["DeptName"];
                var deptcode = Data["DeptCode"];


                var facultyname = Data["FacultyName"];
                var facultycode = Data["FacultyCode"];
                var facultytype = Data["FacultyType"];



                window.location.href = "SetupMajor.aspx?id=" + id + "";

                Mainform.deptid.value = id

           


            });





            $('#tbldept tbody').on('click', "#btnBrowseStudents", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["DeptID"]
                var deptname = Data["DeptName"];
                var deptcode = Data["DeptCode"];


                var facultyname = Data["FacultyName"];
                var facultycode = Data["FacultyCode"];
                var facultytype = Data["FacultyType"];





                Mainform.deptid.value = id

            });





            $('#tbldept tbody').on('click', "#btnBrowseCourses", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["DeptID"]
                var deptname = Data["DeptName"];
                var deptcode = Data["DeptCode"];


                var facultyname = Data["FacultyName"];
                var facultycode = Data["FacultyCode"];
                var facultytype = Data["FacultyType"];



                window.location.href = "SetupCourses.aspx?id=" + id + "";

                Mainform.deptid.value = id


            });


            $('#tbldept tbody').on('click', "#btnBrowseDeptHead", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["DeptID"]
                var deptname = Data["DeptName"];
                var deptcode = Data["DeptCode"];


                var facultyname = Data["FacultyName"];
                var facultycode = Data["FacultyCode"];
                var facultytype = Data["FacultyType"];





                Mainform.deptid.value = id

            });








            $('#tbldept tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["DeptID"]
                document.getElementById("txtdeptcode").value = Data["DeptCode"];
                document.getElementById("txtdept").value = Data["DeptName"];

                $("#ddlfaculty option").each(function () {
                    if ($(this).text() == Data["FacultyName"]) {
                        $(this).attr("selected", "selected")
                    }
                });

                document.getElementById("UpdateValue").value = Data["DeptName"];

                Mainform.deptid.value = id


            });

            $('#tbldept tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#tbldept').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["DeptID"]
                var facultyname = Data["DeptName"];



                var Result = confirm("You are about to delete department of " + facultyname)
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