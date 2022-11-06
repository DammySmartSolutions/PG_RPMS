using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System.Web.SessionState;
using System.Web;
using CorrectRPMS.Models;
using CorrectRPMS.Models.RPMS;




namespace CorrectRPMS.App_Code
{
    public class RpmsController : ApiController
    {

        RPMSEntities Db = new RPMSEntities();
     






        [HttpGet, HttpPost]
        [ActionName("LoadSchool")]
        public IEnumerable<tblSchoolDetails> LoadSchool()
        {

            var query = Db.tblSchoolDetails.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteSchool")]

        public int DeleteSchool(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblSchoolDetails where SchoolID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }




        [HttpGet, HttpPost]
        [ActionName("LoadFaculty")]
        public IEnumerable<tblFaculty> LoadFaculty()
        {

            var query = Db.tblFaculty.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteFaculty")]

        public int DeleteFaculty(int id)
        {


            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteFaculty(staffnumber, id);



            var query = Db.Database.ExecuteSqlCommand("Delete from tblFaculty where FacultyID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteFaculty(string staffnumber, int id)
        {
            try
            {

                var GetUserDetails = (from s in Db.tblFaculty.Where(s => s.FacultyID == id)
                                      select new SetupFaculty
                                      {

                                          FacultyName = s.FacultyName,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].FacultyName;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;
              
                string update = facultytype;
                string Description = "You deleted Faculty of : " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {
               
            }
        }

        [HttpGet, HttpPost]
        [ActionName("LoadFacultyType")]
        public IEnumerable<tblFacultyType> LoadFacultyType()
        {

            var query = Db.tblFacultyType.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteFacultyType")]

        public int DeleteFacultyType(int id)
        {


            string staffnumber  ="";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDelete(staffnumber, id);

            var query = Db.Database.ExecuteSqlCommand("Delete from tblFacultyType where FacultyTypeID = '" + id + "'");


            Db.SaveChanges();

        


            return query;

        }

        private void WriteToAuditDelete(string staffnumber, int id)
        {
            try
            {

                var GetUserDetails = (from s in Db.tblFacultyType.Where(s => s.FacultyTypeID == id)
                                      select new SetupFacultyType
                                      {

                                   FacultyType = s.FacultyType,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].FacultyType;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;
            //  //  string created = txtfacultytype.Value;
                string update = facultytype;
                string Description = "You deleted Faculty Type : " + update ;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {
              //  ShowMessage(ex.Message);
            }

        }

        [HttpGet, HttpPost]
        [ActionName("LoadDept")]
        public IEnumerable<tblDept> LoadDept()
        {

            var query = Db.tblDept.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteDept")]

        public int DeleteDept(int id)
        {

            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteDept(staffnumber, id);


            var query = Db.Database.ExecuteSqlCommand("Delete from tblDept where DeptID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteDept(string staffnumber, int id)
        {


            try
            {

                var GetUserDetails = (from s in Db.tblDept.Where(s => s.DeptID == id)
                                      select new SetupDept
                                      {

                                          DeptName = s.DeptName,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].DeptName;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;
              
                string update = facultytype;
                string Description = "You deleted Department : " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {
               
            }




        }

        [HttpGet, HttpPost]
        [ActionName("LoadDeptByID")]
        public IEnumerable <tblDept> LoadDeptByID(int id)
        {





            var query = Db.tblDept.Where(s => s.FacultyID == id).ToArray();





            return query;

        }





        [HttpGet, HttpPost]
        [ActionName("LoadMajor")]
        public IEnumerable<tblMajor> LoadMajor()
        {

            var query = Db.tblMajor.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteMajor")]

        public int DeleteMajor(int id)
        {

            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteMajor(staffnumber, id);


            var query = Db.Database.ExecuteSqlCommand("Delete from tblMajor where MajorID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteMajor(string staffnumber, int id)
        {

            try
            {

                var GetUserDetails = (from s in Db.tblMajor.Where(s => s.MajorID == id)
                                      select new SetupMajor
                                      {

                                          MajorName = s.MajorName,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].MajorName;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;

                string update = facultytype;
                string Description = "You deleted Major: " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {

            }






        }

        [HttpGet, HttpPost]
        [ActionName("LoadMajorByID")]
        public IEnumerable<tblMajor> LoadMajorByID(int id)
        {





            var query = Db.tblMajor.Where(s => s.DeptID == id).ToArray();





            return query;

        }



        [HttpGet, HttpPost]
        [ActionName("LoadCourses")]
        public IEnumerable<tblCourses> LoadCourses()
        {

            var query = Db.tblCourses.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteCourse")]

        public int DeleteCourse(int id)
        {

            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteCourses(staffnumber, id);







            var query = Db.Database.ExecuteSqlCommand("Delete from tblCourses where CourseID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteCourses(string staffnumber, int id)
        {
            try
            {

                var GetUserDetails = (from s in Db.tblCourses.Where(s => s.CourseID == id)
                                      select new SetupCourses
                                      {

                                          CourseName = s.CourseName,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].CourseName;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;

                string update = facultytype;
                string Description = "You deleted Course: " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {

            }

        }

        [HttpGet, HttpPost]
        [ActionName("LoadCourseByID")]
        public IEnumerable<tblCourses> LoadCourseByID(int id)
        {





            var query = Db.tblCourses.Where(s => s.DeptID == id).ToArray();





            return query;

        }






        [HttpGet, HttpPost]
        [ActionName("LoadGradingSystem")]
        public IEnumerable<tblGradingSystem> LoadGradingSystem()
        {

            var query = Db.tblGradingSystem.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteGradingSystemType")]

        public int DeleteGradingSystemType(int id)
        {



            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteGradingSystem(staffnumber, id);




            var query = Db.Database.ExecuteSqlCommand("Delete from tblGradingSystem where GradingSystemID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteGradingSystem(string staffnumber, int id)
        {

            try
            {

                var GetUserDetails = (from s in Db.tblGradingSystem.Where(s => s.GradingSystemID == id)
                                      select new SetupGradingSystem
                                      {

                                          GradingSystem = s.GradingSystem,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].GradingSystem;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;

                string update = facultytype;
                string Description = "You deleted Grading System: " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {

            }



        }

        [HttpGet, HttpPost]
        [ActionName("LoadGrading")]
        public IEnumerable<tblGrading> LoadGrading()
        {

            var query = Db.tblGrading.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteGrading")]

        public int DeleteGrading(int id)
        {


            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteGrading(staffnumber, id);







            var query = Db.Database.ExecuteSqlCommand("Delete from tblGrading where GradingID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteGrading(string staffnumber, int id)
        {

            try
            {

                var GetUserDetails = (from s in Db.tblGrading.Where(s => s.GradingID == id)
                                      select new SetupGrading
                                      {

                                          GradeLetter = s.GradeLetter,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].GradeLetter;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;

                string update = facultytype;
                string Description = "You deleted Grade Letter: " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {

            }





        }

        [HttpGet, HttpPost]
        [ActionName("LoadGradingByID")]
        public IEnumerable<tblGrading> LoadGradingByID(int id)
        {





            var query = Db.tblGrading.Where(s => s.GradingSystemTypeID == id).ToArray();





            return query;

        }







        [HttpGet, HttpPost]
        [ActionName("LoadLevel")]
        public IEnumerable<tblLevel> LoadLevel()
        {

            var query = Db.tblLevel.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteLevel")]

        public int DeleteLevel(int id)
        {


            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteLevel(staffnumber, id);







            var query = Db.Database.ExecuteSqlCommand("Delete from tblLevel where LevelID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteLevel(string staffnumber, int id)
        {


            try
            {

                var GetUserDetails = (from s in Db.tblLevel.Where(s => s.LevelID == id)
                                      select new SetupLevel
                                      {

                                          LevelName = s.LevelName,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].LevelName;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;

                string update = facultytype;
                string Description = "You deleted Level: " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {

            }



        }

        [HttpGet, HttpPost]
        [ActionName("LoadStates")]
        public IEnumerable<tblState> LoadStates()
        {

            var query = Db.tblState.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteState")]

        public int DeleteState(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblState where StateId = '" + id + "'");


            Db.SaveChanges();
            return query;

        }





        [HttpGet, HttpPost]
        [ActionName("LoadLocalGovt")]
        public IEnumerable<tblLocalGovt> LoadLocalGovt()
        {

            var query = Db.tblLocalGovt.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteLocalGovt")]

        public int DeleteLocalGovt(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblLocalGovt where LocalGovtID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }



        [HttpGet, HttpPost]
        [ActionName("LoadLocalGovtByID")]
        public IEnumerable<tblLocalGovt> LoadLocalGovtByID(int id)
        {





            var query = Db.tblLocalGovt.Where(s => s.StateID == id).ToArray();





            return query;

        }





        [HttpGet, HttpPost]
        [ActionName("LoadMarital")]
        public IEnumerable<tblMaritalStatus> LoadMarital()
        {

            var query = Db.tblMaritalStatus.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteMarital")]

        public int DeleteMarital(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblMaritalStatus where MaritalID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }





        [HttpGet, HttpPost]
        [ActionName("LoadSession")]
        public IEnumerable<tblSession> LoadSession()
        {

            var query = Db.tblSession.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteSession")]

        public int DeleteSession(int id)
        {



            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteSession(staffnumber, id);



            var query = Db.Database.ExecuteSqlCommand("Delete from tblSession where SessionID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteSession(string staffnumber, int id)
        {
            try
            {

                var GetUserDetails = (from s in Db.tblSession.Where(s => s.SessionID == id)
                                      select new SetupSession
                                      {

                                          Sessio = s.Session,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].Sessio;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;

                string update = facultytype;
                string Description = "You deleted Session: " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {

            }




        }

        [HttpGet, HttpPost]
        [ActionName("LoadStaffRank")]
        public IEnumerable<tblStaffRank> LoadStaffRank()
        {

            var query = Db.tblStaffRank.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteStaffRank")]

        public int DeleteStaffRank(int id)
        {


            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteStaffRank(staffnumber, id);






            var query = Db.Database.ExecuteSqlCommand("Delete from tblStaffRank where StaffRankID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteStaffRank(string staffnumber, int id)
        {

            try
            {

                var GetUserDetails = (from s in Db.tblStaffRank.Where(s => s.StaffRankID == id)
                                      select new SetupStaffRank
                                      {

                                          StaffRank = s.StaffRank,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].StaffRank;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;

                string update = facultytype;
                string Description = "You deleted Staff Rank: " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {

            }




        }

        [HttpGet, HttpPost]
        [ActionName("LoadPosition")]
        public IEnumerable<tblPosition> LoadPosition()
        {

            var query = Db.tblPosition.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeletePosition")]

        public int DeletePosition(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblPosition where PositionID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }




        [HttpGet, HttpPost]
        [ActionName("LoadSemester")]
        public IEnumerable<tblSemester> LoadSemester()
        {

            var query = Db.tblSemester.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteSemester")]

        public int DeleteSemester(int id)
        {



            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteSemester(staffnumber, id);




            var query = Db.Database.ExecuteSqlCommand("Delete from tblSemester where SemesterID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteSemester(string staffnumber, int id)
        {


            try
            {

                var GetUserDetails = (from s in Db.tblSemester.Where(s => s.SemesterID == id)
                                      select new SetupSemester
                                      {

                                          Semester = s.Semester,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].Semester;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;

                string update = facultytype;
                string Description = "You deleted Semester: " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {

            }




        }

        [HttpGet, HttpPost]
        [ActionName("LoadSessionSemester")]
        public IEnumerable<tblSessionSemester> LoadSessionSemester()
        {

            var query = Db.tblSessionSemester.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteSessionsemester")]

        public int DeleteSessionsemester(int id)
        {


            string staffnumber = "";

            var session = HttpContext.Current.Session;
            if (session != null)
            {
                staffnumber = session["StaffNumber"].ToString();

            }

            else
            {
                Redirect("LoginPage.aspx");

            }

            WriteToAuditDeleteSessionSemester(staffnumber, id);






            var query = Db.Database.ExecuteSqlCommand("Delete from tblSessionSemester where SessionSemesterID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }

        private void WriteToAuditDeleteSessionSemester(string staffnumber, int id)
        {


            try
            {

                var GetUserDetails = (from s in Db.tblSessionSemester.Where(s => s.SessionSemesterID == id)
                                      select new SetupSessionSemester
                                      {

                                          SessionSemester = s.SessionSemester,



                                      }).ToArray();


                string facultytype = GetUserDetails[0].SessionSemester;


                string staffnum = staffnumber;
                string action = "delete";
                DateTime DatePerformed = DateTime.Now;

                string update = facultytype;
                string Description = "You deleted Session/Semester: " + update;

                var facultydetail = Db.tblAuditTray;



                var NewAudit = new tblAuditTray();
                NewAudit.ActionPerformed = action;

                NewAudit.DatePerformed = DatePerformed;
                NewAudit.Desccription = Description;
                NewAudit.StaffNumber = staffnum;


                Db.tblAuditTray.Add(NewAudit);
                Db.SaveChanges();


            }

            catch (Exception ex)
            {

            }





        }

        [HttpGet, HttpPost]
        [ActionName("LoadRoles")]
        public IEnumerable<tblRoles> LoadRoles()
        {

            var query = Db.tblRoles.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteRole")]

        public int DeleteRole(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblRoles where RoleID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }



        [HttpGet, HttpPost]
        [ActionName("LoadUser")]
        public IEnumerable<tblUser> LoadUser()
        {

            var query = Db.tblUser.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteUser")]

        public int DeleteUser(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblUser where StaffID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }




        [HttpGet, HttpPost]
        [ActionName("LoadStaffRole")]
        public IEnumerable<tblStaffRole> LoadStaffRole()
        {

            var query = Db.tblStaffRole.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteStaffRole")]

        public int DeleteStaffRole(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblStaffRole where StaffRoleID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }



        [HttpGet, HttpPost]
        [ActionName("LoadAssignCourseCoordinator")]
        public IEnumerable<tblAssignCoursesCoordinator> LoadAssignCourseCoordinator()
        {

            var query = Db.tblAssignCoursesCoordinator.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteAssignCourseCoordinator")]

        public int DeleteAssignCourseCoordinator(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblAssignCoursesCoordinator where AssignCourseID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }





        [HttpGet, HttpPost]
        [ActionName("LoadStudentStatus")]
        public IEnumerable<tblStatus> LoadStudentStatus()
        {

            var query = Db.tblStatus.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteStudentStatus")]

        public int DeleteStudentStatus(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblStatus where StatusID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }





        [HttpGet, HttpPost]
        [ActionName("LoadStudentDiscLeave")]
        public IEnumerable<tblStudentDisciplineLeave> LoadStudentDiscLeave()
        {

            var query = Db.tblStudentDisciplineLeave.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteStudentDiscLeave")]

        public int DeleteStudentDiscLeave(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblStudentDisciplineLeave where DiscLeaveID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }




        [HttpGet, HttpPost]
        [ActionName("LoadStudentCourseExempt")]
        public IEnumerable<tblStudentCourseExempt> LoadStudentCourseExempt()
        {

            var query = Db.tblStudentCourseExempt.ToArray();
            return query;

        }


        [HttpPost, HttpGet]
        [ActionName("DeleteStudentCourseExempt")]

        public int DeleteStudentCourseExempt(int id)
        {



            var query = Db.Database.ExecuteSqlCommand("Delete from tblStudentCourseExempt where StudCourseExemptID = '" + id + "'");


            Db.SaveChanges();
            return query;

        }





    }
}
