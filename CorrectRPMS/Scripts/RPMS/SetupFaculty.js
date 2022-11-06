









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
        url: "../api/Rpms/LoadFaculty",
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
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "50%", "targets": 1 }, { "width": "10%", "targets": 2 }, { "width": "3%", "targets": 3 }, { "width": "5%", "targets": 4 }, { "width": "5%", "targets": 5 }, { "width": "5%", "targets": 6 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'FacultyCode' },
                     { 'data': 'FacultyName' },
                 { 'data': 'FacultyType' },
                { 'data': 'FacultyID' },
                 {

                     'render': function (data, type, full, row) {
                         return '<a id=btnBrowse class="link-primary">Browse Departments</a>'

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
                id = Data["FacultyID"]
                var facultycode = Data["FacultyCode"];
                var facultyname = Data["FacultyName"];

             ///   window.location.href = "SetupDepartment.aspx?id=" + facultyname + "";


                window.location.href = "SetupDepartment.aspx?id=" + id + "";

                Mainform.facultyid.value = id


            });





            $('#table tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["FacultyID"]
                document.getElementById("txtfacultycode").value = Data["FacultyCode"];
                document.getElementById("txtfacultyname").value = Data["FacultyName"];
               
                $("#ddlfacultytype option").each(function () {
                    if ($(this).text() == Data["FacultyType"]) {
                        $(this).attr("selected", "selected")
                    }
                });

                document.getElementById("UpdateValue").value = Data["FacultyName"];

                Mainform.facultyid.value = id


            });

            $('#table tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#table').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["FacultyID"]
               var facultyname = Data["FacultyName"];



               var Result = confirm("You are about to delete faculty/college of " + facultyname)
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
        url: "../api/Rpms/DeleteFaculty?id= " + ID + "",
        success: function (data) {
            alert("Faculty/College Deleted!");
            ID = 0;
            LoadDataTable();



        }
    })

}




function LoadDeptDataTable() {

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
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "40%", "targets": 1 }, { "width": "15%", "targets": 2 }, { "width": "5%", "targets": 3 }, { "width": "10%", "targets": 4 }, { "width": "5%", "targets": 5 }, { "width": "20%", "targets": 6 }, { "width": "5%", "targets": 7 }, { "width": "5%", "targets": 8 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'DeptCode' },
                     { 'data': 'DeptName' },
                 { 'data': 'FacultyName' },

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

              ///  document.getElementById("UpdateValue").value = Data["FacultyName"];


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