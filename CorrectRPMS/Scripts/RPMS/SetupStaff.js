


















//id = 0;
ID = 0;
const Mainform = document.getElementById("mainform")
window.onload = function onloadTable() {

    LoadDataTable();
    LoadStaffRoleTable();
    LoadCourseCoordinator();
}

function LoadDataTable() {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/LoadUser",
        success: function (data) {


            $('#tblStaffList').DataTable().destroy();

            var datatableVariable = $('#tblStaffList').DataTable({


                data: data,
                "bFilter": true,
                "bInfo": false,
                "searching": true,
                "ordering": true,


                "bDisplayLength": 1000,
                "bLengthChange": true,
                "lengthMenu": [[5, 10, 15, 20, 25, 50, -1], [5, 10, 25, 50, "All"]],
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "3%", "targets": 1 }, { "width": "10%", "targets": 2 }, { "width": "10%", "targets": 3 }, { "width": "10%", "targets": 4 }, { "width": "5%", "targets": 5 }, { "width": "3%", "targets": 6 }, { "width": "30%", "targets": 7 }, { "width": "20%", "targets": 8 }, { "width": "5%", "targets": 9 }, { "width": "5%", "targets": 10 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'StaffNumber' },

                      { 'data': 'Title' },

                       { 'data': 'Surname' },

                          { 'data': 'FirstName' },

                           { 'data': 'Email' },


                           { 'data': 'Mobile' },

                             { 'data': 'Sex' },

                              { 'data': 'Department' },


                               { 'data': 'Role' },
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
            $('#tblStaffList tfoot th').each(function () {
                var placeHolderTitle = $('#tblStaffList thead th').eq($(this).index()).text();
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





            $('#tblStaffList tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tblStaffList').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["StaffID"]
                document.getElementById("txtstaffno").value = Data["StaffNumber"];

                $("#ddltitle option").each(function () {
                    if ($(this).text() == Data["Title"]) {
                        $(this).attr("selected", "selected")
                    }
                });



                document.getElementById("txtsurname").value = Data["Surname"];


                document.getElementById("txtfirstname").value = Data["FirstName"];

                document.getElementById("txtemail").value = Data["Email"];

                document.getElementById("txtphone").value = Data["Mobile"];

                Mainform.staffid.value = id



                $("#ddlsex option").each(function () {
                    if ($(this).text() == Data["Sex"]) {
                        $(this).attr("selected", "selected")
                    }
                });



                $("#ddlFaculty option").each(function () {
                    if ($(this).text() == Data["Faculty"]) {
                        $(this).attr("selected", "selected")
                    }
                });

            });

            $('#tblStaffList tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#tblStaffList').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["StaffID"]
                var facultyname = Data["FullName"];



                var Result = confirm("You are about to delete " + facultyname + " from RPMS")
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
        url: "../api/Rpms/DeleteUser?id= " + ID + "",
        success: function (data) {
            alert("User Deleted!");
           
            ID = 0;

             alert("Go to Role Tab to Delete Role(s) assigned to User");
            LoadDataTable();



        }
    })

}



function LoadStaffRoleTable() {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/LoadStaffRole",
        success: function (data) {


            $('#tblStaffRole').DataTable().destroy();

            var datatableVariable = $('#tblStaffRole').DataTable({


                data: data,
                "bFilter": true,
                "bInfo": false,
                "searching": true,
                "ordering": true,


                "bDisplayLength": 1000,
                "bLengthChange": true,
                "lengthMenu": [[5, 10, 15, 20, 25, 50, -1], [5, 10, 25, 50, "All"]],
                "columnDefs": [{ "width": "10%", "targets": 0 }, { "width": "35%", "targets": 1 }, { "width": "5%", "targets": 2 }, { "width": "5%", "targets": 3 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'StaffNumber' },

                      { 'data': 'Role' },

                       
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
            $('#tblStaffRole tfoot th').each(function () {
                var placeHolderTitle = $('#tblStaffRole thead th').eq($(this).index()).text();
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





            $('#tblStaffRole tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tblStaffRole').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["StaffRoleID"]
                document.getElementById("txtrolestaffno").value = Data["StaffNumber"];

                $("#ddlrole option").each(function () {
                    if ($(this).text() == Data["Role"]) {
                        $(this).attr("selected", "selected")
                    }
                });



               


                Mainform.staffroleid.value = id



               

            });

            $('#tblStaffRole tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#tblStaffRole').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["StaffRoleID"]
                var facultyname = Data["StaffNumber"];

                var role = Data["Role"];


                var Result = confirm("You are about to delete the role of " + role + " for staff number: " + facultyname + " ")
                if (Result) {

                    DeleteItemRole(ID);
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







function DeleteItemRole(ID) {


    var CheckCookieValue = document.cookie.split("=");
    var getcomid = CheckCookieValue[2];
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/DeleteStaffRole?id= " + ID + "",
        success: function (data) {
            alert("User Role Deleted!");
            ID = 0;
            LoadStaffRoleTable();



        }
    })

}



function LoadCourseCoordinator() {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/LoadAssignCourseCoordinator",
        success: function (data) {


            $('#tblAssign').DataTable().destroy();

            var datatableVariable = $('#tblAssign').DataTable({


                data: data,
                "bFilter": true,
                "bInfo": false,
                "searching": true,
                "ordering": true,


                "bDisplayLength": 1000,
                "bLengthChange": true,
                "lengthMenu": [[5, 10, 15, 20, 25, 50, -1], [5, 10, 25, 50, "All"]],
                "columnDefs": [{ "width": "5%", "targets": 0 }, { "width": "5%", "targets": 1 }, { "width": "5%", "targets": 2 }, { "width": "50%", "targets": 3 }, { "width": "3%", "targets": 4 }, { "width": "3%", "targets": 5 }],
                pageLength: 10,
                dom: "Bfrtip",
                buttons: [

               	'copy', 'csv', 'excel', 'pdf', 'print'


                ],
                columns: [

                    { 'data': 'StaffNumber' },

                      { 'data': 'Course' },

                       { 'data': 'DeptCode' },

                         { 'data': 'SemesterSession' },
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
            $('#tblAssign tfoot th').each(function () {
                var placeHolderTitle = $('#tblAssign thead th').eq($(this).index()).text();
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





            $('#tblAssign tbody').on('click', "#btnEdit", function () {
                // fetch the value on the grid
                var currentrow = $(this).closest("tr")
                var table = $('#tblAssign').DataTable()
                var Data = table.row(currentrow).data();
                id = Data["AssignCourseID"]
                document.getElementById("txtAssignStaffNo").value = Data["StaffNumber"];

                $("#ddlAssignFaculty option").each(function () {
                    if ($(this).text() == Data["FacultyCode"]) {
                        $(this).attr("selected", "selected")
                    }
                });

                $("#ddlAssignDept option").each(function () {
                    if ($(this).text() == Data["DeptCode"]) {
                        $(this).attr("selected", "selected")
                    }
                });


                $("#ddlcourse option").each(function () {
                    if ($(this).text() == Data["Course"]) {
                        $(this).attr("selected", "selected")
                    }
                });


                $("#ddlsesssemester option").each(function () {
                    if ($(this).text() == Data["SemesterSession"]) {
                        $(this).attr("selected", "selected")
                    }
                });

                Mainform.AssignCourseid.value = id





            });

            $('#tblAssign tbody').on('click', "#btnDelete", function () {

                var currentrow = $(this).closest("tr")
                var table = $('#tblAssign').DataTable()
                var Data = table.row(currentrow).data();
                ID = Data["AssignCourseID"]
                var course = Data["Course"];

                var staffno = Data["StaffNumber"];


                var Result = confirm("You are about to delete " + course + " assigned to staff number: " + staffno + " ")
                if (Result) {

                    DeleteItemAssignCoursesCoordinator(ID);
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







function DeleteItemAssignCoursesCoordinator(ID) {


    var CheckCookieValue = document.cookie.split("=");
    var getcomid = CheckCookieValue[2];
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        data: "{}",
        url: "../api/Rpms/DeleteAssignCourseCoordinator?id= " + ID + "",
        success: function (data) {
            alert("Course Allocation to Staff Deleted!");
            ID = 0;
            LoadCourseCoordinator();



        }
    })

}