using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CorrectRPMS.Models;
using CorrectRPMS.Models.RPMS;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;

namespace CorrectRPMS
{
    public partial class SetupStaff : System.Web.UI.Page
    {
        RPMSEntities Db = new RPMSEntities();

        string taball;
        string StaffNumber;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["StaffNumber"] == null)
            {
                Response.Redirect("LoginPage.aspx");

            }
            else {


                StaffNumber = Session["StaffNumber"].ToString();




                if (!IsPostBack)
            {


                 
                LoadFaculty();
                LoadStaffRoles();
              
                LoadSessionSemester();
                LoadAssignFaculty();


                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            }


            }

            if (taball != null)
            {
                selected_tab.Value = taball;

            }

            else
            {
                selected_tab.Value = "0";

            }

            ClientScript.RegisterStartupScript(this.GetType(), "selectedTab", "$('#tabs').tabs({selected: " + tab_index.Value + "});", true);
        }

        private void LoadAssignFaculty()
        {
            var allow = (from s in Db.tblFaculty
                         select new { s.FacultyID, s.FacultyCode }).ToList();


            ddlAssignFaculty.DataTextField = "FacultyCode";
            ddlAssignFaculty.DataValueField = "FacultyID";
            ddlAssignFaculty.DataSource = allow;
            ddlAssignFaculty.DataBind();
            ddlAssignFaculty.Items.Insert(0, "--Select Faculty--");
        }

        private void LoadSessionSemester()
        {
            var allow = (from s in Db.tblSessionSemester
                         select new { s.SessionSemesterID, s.SessionSemester }).ToList();


            ddlsesssemester.DataTextField = "SessionSemester";
            ddlsesssemester.DataValueField = "SessionSemesterID";
            ddlsesssemester.DataSource = allow;
            ddlsesssemester.DataBind();
            ddlsesssemester.Items.Insert(0, "--Select Session-Semester--");
        }

      

        private void LoadStaffRoles()
        {
            var allow = (from s in Db.tblRoles
                         select new { s.RoleID, s.Roles }).ToList();


            ddlrole.DataTextField = "Roles";
            ddlrole.DataValueField = "RoleID";
            ddlrole.DataSource = allow;
            ddlrole.DataBind();
            ddlrole.Items.Insert(0, "--Select Role--");
        }

        private void LoadFaculty()
        {
            var allow = (from s in Db.tblFaculty
                         select new { s.FacultyID, s.FacultyName }).ToList();


            ddlFaculty.DataTextField = "FacultyName";
            ddlFaculty.DataValueField = "FacultyID";
            ddlFaculty.DataSource = allow;
            ddlFaculty.DataBind();
            ddlFaculty.Items.Insert(0, "--Select Faculty--");

        }




        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }

        private void ShowMessage(string Message)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "alert('" + Message + "')", true);

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupStaff.aspx");
            }


            if (txtstaffno.Value == "")
            {
                ShowMessage("Please Enter your Staff Number");
                txtstaffno.Focus();
                taball = "0";
                selected_tab.Value = taball;
                return;
            }


            if (ddltitle.SelectedItem.Text == "Select Title")
            {
                ShowMessage("Please Select Title");
                ddltitle.Focus();
                taball = "0";
                selected_tab.Value = taball;
                return;
            }


            if (txtsurname.Value == "")
            {
                ShowMessage("Please Enter your Surname/Last Name");
                txtsurname.Focus();
                taball = "0";
                selected_tab.Value = taball;
                return;
            }


            if (txtfirstname.Value == "")
            {
                ShowMessage("Please Enter your First Name");
                txtfirstname.Focus();
                taball = "0";
                selected_tab.Value = taball;
                return;
            }

            if (txtemail.Value == "")
            {
                ShowMessage("Please Enter your official/office email address");
                txtemail.Focus();
                taball = "0";
                selected_tab.Value = taball;
                return;
            }

            else { 

                    string Checkemail = txtemail.Value;
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(Checkemail);
                    if (match.Success)
                    {

                    }

                    else
                    {
                        ShowMessage("Invalid Email Address");
                        txtemail.Focus();
                    taball = "0";
                    selected_tab.Value = taball;
                    return;
                    }

            }

            if (txtphone.Value == "")
            {
                ShowMessage("Please Enter your phone number");
                txtphone.Focus();
                taball = "0";
                selected_tab.Value = taball;
                return;
            }



            if (ddlsex.SelectedItem.Text == "Select Sex")
            {
                ShowMessage("Please Select Sex");
                ddlsex.Focus();
                taball = "0";
                selected_tab.Value = taball;
                return;
            }



            if (ddlFaculty.SelectedItem.Text == "--Select Faculty--")
            {
                ShowMessage("Please Select Faculty");
                ddlFaculty.Focus();
                taball = "0";
                selected_tab.Value = taball;
                return;
            }

            if (ddlDept.SelectedItem.Text == "--Select Dept--")
            {
                ShowMessage("Please Select Department");
                ddlDept.Focus();
                taball = "0";
                selected_tab.Value = taball;
                return;
            }




            int facid = 0;
            if (!string.IsNullOrEmpty(staffid.Value))
            {
                facid = int.Parse(staffid.Value);

            }

            string staffno = txtstaffno.Value.ToUpper();
            string title = ddltitle.SelectedItem.Text.ToUpper();
            string surname = txtsurname.Value.ToUpper();
            string firstname = txtfirstname.Value.ToUpper();
            string email = txtemail.Value;
            long phone = long.Parse(txtphone.Value);
            string sex = ddlsex.SelectedItem.Text.ToUpper();
            string faculty = ddlFaculty.SelectedItem.Text.ToUpper();
            string dept = ddlDept.SelectedItem.Text.ToUpper();

            string fullname = firstname + " " + surname;

            DateTime datecreated = DateTime.Today;
            DateTime linkexpireddate = DateTime.Today.AddDays(1);


            var facultydetail = Db.tblUser.Where(x => x.StaffID == facid && x.StaffNumber == staffno).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {


                    string Role = "None";

                    bool updatepassword = false;

                    bool linkactive = false;

                    bool securityquecreated = false;

                    string strNewPassword = GeneratePassword().ToString();

                    var NewFacultydetail = new tblUser();

                    NewFacultydetail.StaffNumber = staffno;

                    NewFacultydetail.Title = title;

                    NewFacultydetail.Surname = surname;

                    NewFacultydetail.FirstName = firstname;

                    NewFacultydetail.Email = email;

                    NewFacultydetail.Mobile = phone;

                    NewFacultydetail.Sex = sex;

                    NewFacultydetail.Faculty = faculty;

                    NewFacultydetail.Department = dept;

                    NewFacultydetail.Role = Role;

                    NewFacultydetail.UpdatePassword = updatepassword;

                    NewFacultydetail.FullName = fullname;

                    NewFacultydetail.Password = strNewPassword;

                    NewFacultydetail.CreatedBy = StaffNumber;




                    SendEmail(fullname, strNewPassword, staffno, email);

                    if (!string.IsNullOrEmpty(asplbl.Text))

                    {
                        ShowMessage("Account Creation Not Successful!");
                        taball = "0";
                        selected_tab.Value = taball;
                        return;
                    }



                    NewFacultydetail.DateCreated = datecreated;
                    NewFacultydetail.LinkExpirationDate = linkexpireddate;
                    NewFacultydetail.LinkActivated = linkactive;
                    NewFacultydetail.SecurityQueCreated = securityquecreated;

                    Db.tblUser.Add(NewFacultydetail);
                    Db.SaveChanges();


                    ShowMessage("Successful");



                    txtstaffno.Value = "";
                    txtsurname.Value = "";

                    txtfirstname.Value = "";
                    txtemail.Value = "";
                    txtphone.Value = "";

                    ddltitle.SelectedIndex = 0;
                    ddlsex.SelectedIndex = 0;

                    LoadFaculty();
                  
                    staffid.Value = "";

                    taball = "0";
                    selected_tab.Value = taball;

                }
                else
                {



                


                    facultydetail.StaffNumber = staffno;

                    facultydetail.Title = title;

                    facultydetail.Surname = surname;

                    facultydetail.FirstName = firstname;

                    facultydetail.Email = email;

                    facultydetail.Mobile = phone;

                    facultydetail.Sex = sex;

                    facultydetail.Faculty = faculty;

                    facultydetail.Department = dept;


                    facultydetail.FullName = fullname;

                    facultydetail.CreatedBy = StaffNumber;









                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();


                    ShowMessage("Updated");
                  



                    txtstaffno.Value = "";
                    txtsurname.Value = "";

                    txtfirstname.Value = "";
                    txtemail.Value = "";
                    txtphone.Value = "";

                    ddltitle.SelectedIndex = 0;
                    ddlsex.SelectedIndex = 0;

                    LoadFaculty();

                    staffid.Value = "";
                    taball = "0";
                    selected_tab.Value = taball;

                }


               

            }


            
            catch (Exception ex)
            {
                //ShowMessage(ex.Message);

                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);

            }

            taball = "0";
            selected_tab.Value = taball;
        }

        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string facultyname = ddlFaculty.SelectedItem.Text;
            LoadDept(facultyname);
        }

        private void LoadDept(string facultyname)
        {
            var allow = (from s in Db.tblDept.Where(s=>s.FacultyName == facultyname)
                         select new { s.DeptID, s.DeptName }).ToList();


            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DeptID";
            ddlDept.DataSource = allow;
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "--Select Dept--");
        }




        public string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";


            char[] sep = {
            ','
        };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }


        private void SendEmail(string fullname, string strNewPassword, string staffno, string email)
        {
          
            try
            {

                string encryptstaffno = HttpUtility.UrlEncode(Encrypt(staffno.Trim()));
                string encryptemail = HttpUtility.UrlEncode(Encrypt(email.Trim()));




                StringBuilder sb = new StringBuilder();
                sb.Append("<b>Dear" + " "+  fullname + "</b> <br/>");
                sb.Append("<b> We have received your request to reset/setup your </b> <br/>");
                sb.Append("<b> account for Result Processing Management System (RPMS).</b><br/>");
                sb.Append("<b>Please follow the link:</b> <br/>");
                sb.Append("<a href=http://localhost:50860/ResetPassword.aspx?username=" + encryptstaffno + "&email=" + encryptemail + "> ");
                sb.Append("<b> Click here to reset your password </b></a><br/>");
                sb.Append("<b>Your Username: " + staffno + "</b> <br/>");
                sb.Append("<b>Your Password: " + strNewPassword + "</b> <br/>");
                sb.Append("<b> You will be prompted for the new password you wish to use. </b>");
                sb.Append("<b> This Link is valid for only 24 hours. </b> <br/>");
                sb.Append("<b> You are strongly advised not to share your password or </b>");
                sb.Append("<b> enable remember password on shared system.  </b> <br/>");
                sb.Append("<b>Enjoy the use of our robust result processing management system. </b> <br/> <br/><br/><br/><br/>");

               sb.Append("<b><a href=http://www.oouagoiwoye.edu.ng > Powered by OOU ICT</a> </b> <br/>");


                var config = (from s in Db.tblConfig
                             select new SetupConfig
                             {

                                 Email = s.Email,

                                 Password = s.Password,


                                 EmailHost = s.EmailHost,
                                 Port = s.Port,
                                 EnableSSL = s.EnableSSL,
                                 BodyHtml = s.BodyHtml,



                             }).ToArray();


                string Senderemail = config[0].Email;

                string password = config[0].Password;

                string host = config[0].EmailHost;

                int port = config[0].Port;

                bool ssl = config[0].EnableSSL;

                bool bodyhtml = config[0].BodyHtml;

                

                MailMessage message = new System.Net.Mail.MailMessage(Senderemail, email.Trim(), "RPMS", sb.ToString());

                SmtpClient smtp = new SmtpClient();

                smtp.Host = host;
                

                smtp.Port = port;

                smtp.Credentials = new System.Net.NetworkCredential(Senderemail, password);

                smtp.EnableSsl = ssl;
                

                message.IsBodyHtml = bodyhtml;

                smtp.Send(message);

            }

            catch (Exception ex)
            {
                asplbl.Text = ex.Message;
                
            }
        }

     

        private void UpdateStaffRole(string staffno, string role)
        {
            try
            {


                string role1 = "";
                string role2 = "";
                string role3 = "";
                string role4 = "";
                string role5 = "";
                string role6 = "";
                string role7 = "";
                string role8 = "";
                string role9 = "";
                string role10 = "";


                string roles = "";



                    var Allowchecks = Db.tblStaffRole.Where(x => x.StaffNumber == staffno );

                int Allownoofvalue = Allowchecks.Count();

                for (int i = 0; i < Allownoofvalue; i++)
                {

                    var getvalues = (from t in Db.tblStaffRole.Where(x => x.StaffNumber == staffno)
                                     select new SetupStaffRole
                                     {
                                         Role = t.Role,
                                       

                                     }).ToArray();

                    if (i == 0)
                    {
                         role1 = getvalues[i].Role;
                    }

                   else if (i == 1)
                    {
                        role2 = getvalues[i].Role;
                    }


                    else if (i == 2)
                    {
                        role3 = getvalues[i].Role;
                    }



                    else if (i == 3)
                    {
                        role4 = getvalues[i].Role;
                    }



                    else if (i == 4)
                    {
                        role5 = getvalues[i].Role;
                    }


                    else if (i == 5)
                    {
                        role6 = getvalues[i].Role;
                    }

                    else if (i == 6)
                    {
                        role7 = getvalues[i].Role;
                    }


                    else if (i == 7)
                    {
                        role8 = getvalues[i].Role;
                    }


                    else if (i == 8)
                    {
                        role9 = getvalues[i].Role;
                    }

                    else if (i == 9)
                    {
                        role10 = getvalues[i].Role;
                    }




                    roles = role1 +" " + role2 + " " + role3 + " " + role4 + " " + role5 + " " + role6 + " " + role7 + " " + role8 + " " + role9 + " " + role10;
                }



                var facultydetail = Db.tblUser.Where(x => x.StaffNumber == staffno).FirstOrDefault();

                facultydetail.Role = roles;
                
                Db.Entry(facultydetail).State = EntityState.Modified;
                Db.SaveChanges();







            }

            catch (Exception ex)
            {
                asplbl.Text = ex.Message;
            }

        }

        protected void btnSaveRole_Click(object sender, EventArgs e)
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupStaff.aspx");
            }


            if (txtrolestaffno.Value == "")
            {
                ShowMessage("Please Enter Staff Number");
                txtrolestaffno.Focus();
                taball = "1";
                selected_tab.Value = taball;
                return;
            }


            if (ddlrole.SelectedItem.Text == "--Select Role--")
            {
                ShowMessage("Please Select Role");
                ddlrole.Focus();
                taball = "1";
                selected_tab.Value = taball;
                return;
            }


            int facid = 0;
            if (!string.IsNullOrEmpty(staffroleid.Value))
            {
                facid = int.Parse(staffroleid.Value);

            }

            string staffno = txtrolestaffno.Value.ToUpper();
            string role = ddlrole.SelectedItem.Text.ToUpper();





            var facultydetail = Db.tblStaffRole.Where(x => x.StaffRoleID == facid).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {



                    var NewFacultydetail = new tblStaffRole();

                    NewFacultydetail.StaffNumber = staffno;

                    NewFacultydetail.Role = role;








                    Db.tblStaffRole.Add(NewFacultydetail);
                    Db.SaveChanges();

                    UpdateStaffRole(staffno, role);


                    ShowMessage("Role Added for Staff");



                    txtrolestaffno.Value = "";

                    LoadStaffRoles();

                    staffroleid.Value = "";

                    taball = "1";
                    selected_tab.Value = taball;

                }
                else
                {



                    facultydetail.StaffNumber = staffno;

                    facultydetail.Role = role;




                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();


                    ShowMessage("Staff Role Updated");


                    UpdateStaffRole(staffno, role);

                    txtrolestaffno.Value = "";

                    LoadStaffRoles();

                    staffroleid.Value = "";

                    taball = "1";
                    selected_tab.Value = taball;

                }




            }



            catch (Exception ex)
            {
                //ShowMessage(ex.Message);

                asplbl.Text = ex.Message;

                ShowMessage(asplbl.Text);

            }

            taball = "1";
            selected_tab.Value = taball;



        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupStaff.aspx");
            }


            if (txtResetStaffNo.Value == "")
            {
                ShowMessage("Please Enter Staff Number");
                txtResetStaffNo.Focus();
                taball = "2";
                selected_tab.Value = taball;
                return;
            }



            try
            {

                DateTime linkexpireddate = DateTime.Today.AddDays(1);

                bool linkactive = false;

                string staffno = txtResetStaffNo.Value.ToUpper();
                string strNewPassword = GeneratePassword().ToString();


                var getvalue = (from s in Db.tblUser.Where(s => s.StaffNumber == staffno)
                                select new SetupUser
                                {

                                    FullName = s.FullName,
                                    Email = s.Email,

                                    
                                }).ToArray();


                string fullname = getvalue[0].FullName;

                string email = getvalue[0].Email;

                bool updatepassword = false;

             




                var facultydetail = Db.tblUser.Where(x => x.StaffNumber == staffno).FirstOrDefault();


                

                facultydetail.Password = strNewPassword;

                facultydetail.UpdatePassword = updatepassword;

                SendEmail(fullname, strNewPassword, staffno, email);

                if (!string.IsNullOrEmpty(asplbl.Text))

                {
                    ShowMessage("Password Reset not Successful!");
                    taball = "2";
                    selected_tab.Value = taball;
                    return;
                }


                facultydetail.LinkExpirationDate = linkexpireddate;
                facultydetail.LinkActivated = linkactive;


                Db.Entry(facultydetail).State = EntityState.Modified;
                Db.SaveChanges();


                ShowMessage("Account Reset Successfully");

                txtResetStaffNo.Value = "";
                taball = "2";
                selected_tab.Value = taball;
            }


            catch (Exception ex)
            {

                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);
            }


            taball = "2";
            selected_tab.Value = taball;



        }

        

        protected void ddlAssignFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string facultyname = ddlAssignFaculty.SelectedItem.Text;
            LoadAssignDept(facultyname);
        }

        private void LoadAssignDept(string facultyname)
        {

            var allow = (from s in Db.tblDept.Where(s => s.FacultyCode == facultyname)
                         select new { s.DeptID, s.DeptCode }).ToList();


            ddlAssignDept.DataTextField = "DeptCode";
            ddlAssignDept.DataValueField = "DeptID";
            ddlAssignDept.DataSource = allow;
            ddlAssignDept.DataBind();
            ddlAssignDept.Items.Insert(0, "--Select Dept--");


        }

        protected void btnAssignSave_Click(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupStaff.aspx");
            }


            if (txtAssignStaffNo.Value == "")
            {
                ShowMessage("Please Enter Staff Number");
                txtAssignStaffNo.Focus();
                taball = "3";
                selected_tab.Value = taball;
                return;
            }


            if (ddlAssignFaculty.SelectedItem.Text == "--Select Faculty--")
            {
                ShowMessage("Please Select Faculty");
                ddlAssignFaculty.Focus();
                taball = "3";
                selected_tab.Value = taball;
                return;
            }



            if (ddlAssignDept.SelectedItem.Text == "--Select Dept--")
            {
                ShowMessage("Please Select Department");
                ddlAssignDept.Focus();
                taball = "3";
                selected_tab.Value = taball;
                return;
            }


            if (ddlcourse.SelectedItem.Text == "--Select Courses--")
            {
                ShowMessage("Please Select Course");
                ddlcourse.Focus();
                taball = "3";
                selected_tab.Value = taball;
                return;
            }



            if (ddlsesssemester.SelectedItem.Text == "--Select Session-Semester--")
            {
                ShowMessage("Please Select Session-Semester");
                ddlsesssemester.Focus();
                taball = "3";
                selected_tab.Value = taball;
                return;
            }




         



            int facid = 0;
            if (!string.IsNullOrEmpty(AssignCourseid.Value))
            {
                facid = int.Parse(AssignCourseid.Value);

            }

            string staffno = txtAssignStaffNo.Value.ToUpper();
            string faculty = ddlAssignFaculty.SelectedItem.Text.ToUpper();
            string dept = ddlAssignDept.SelectedItem.Text.ToUpper();
            string course = ddlcourse.SelectedItem.Text.ToUpper();
            string sessionsemester = ddlsesssemester.SelectedItem.Text.ToUpper();
           


            var facultydetail = Db.tblAssignCoursesCoordinator.Where(x => x.AssignCourseID == facid).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {


                   

                    var NewFacultydetail = new tblAssignCoursesCoordinator();

                    NewFacultydetail.StaffNumber = staffno;

                    NewFacultydetail.FacultyCode = faculty;

                    NewFacultydetail.DeptCode = dept;

                    NewFacultydetail.Course = course;

                    NewFacultydetail.SemesterSession = sessionsemester;
                                   


                    Db.tblAssignCoursesCoordinator.Add(NewFacultydetail);
                    Db.SaveChanges();


                    ShowMessage("Successful");



                    txtAssignStaffNo.Value = "";
                    LoadAssignFaculty();
                    LoadSessionSemester();

                    AssignCourseid.Value = "";

                    taball = "3";
                    selected_tab.Value = taball;

                }
                else
                {
                    


                    facultydetail.StaffNumber = staffno;

                    facultydetail.StaffNumber = staffno;

                    facultydetail.FacultyCode = faculty;

                    facultydetail.DeptCode = dept;

                    facultydetail.Course = course;

                    facultydetail.SemesterSession = sessionsemester;


                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();


                    ShowMessage("Updated");

                    txtAssignStaffNo.Value = "";
                    LoadAssignFaculty();
                    LoadSessionSemester();

                    AssignCourseid.Value = "";

                    taball = "3";
                    selected_tab.Value = taball;

                }




            }



            catch (Exception ex)
            {
                //ShowMessage(ex.Message);

                asplbl.Text = ex.Message;

                ShowMessage(asplbl.Text);

            }

            taball = "3";
            selected_tab.Value = taball;
        }

        protected void ddlAssignDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            string deptcode = ddlAssignDept.SelectedItem.Text;
            LoadAssignDeptCourses(deptcode);
        }

        private void LoadAssignDeptCourses(string deptcode)
        {

            var allow = (from s in Db.tblCourses.Where(s => s.DepartmentCode == deptcode)
                         select new { s.CourseID, s.CourseCode }).ToList();


            ddlcourse.DataTextField = "CourseCode";
            ddlcourse.DataValueField = "CourseID";
            ddlcourse.DataSource = allow;
            ddlcourse.DataBind();
            ddlcourse.Items.Insert(0, "--Select Courses--");




        }



        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }




        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            ////lblName.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["name"]));
            ////lblTechnology.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["technology"]));


            return cipherText;
        }

        protected void btnClearSecurity_Click(object sender, EventArgs e)
        {

            if (txtResetStaffNo.Value == "") 
            {
                ShowMessage("Please Enter Staff Number");
                txtResetStaffNo.Focus();
                return;
            }


            string staffno = txtResetStaffNo.Value.ToUpper();

            try
            {

                var Staff = Db.tblUserSecurityAnswer.Where(s => s.StaffNumber == staffno).FirstOrDefault();

                                            
                UpdateSecQueonUserTable(staffno);

                if (!string.IsNullOrEmpty(asplbl.Text))

                {
                    ShowMessage("Security Questions Reset Not Successful!");
                    taball = "2";
                    selected_tab.Value = taball;
                    return;
                }

                Db.Entry(Staff).State = EntityState.Deleted;

                Db.SaveChanges();


                ShowMessage("Security Questions Reset Successful");

                taball = "2";
                selected_tab.Value = taball;

            }


            catch(Exception ex)
            {
                asplbl.Text = ex.Message;

                ShowMessage(asplbl.Text);
            }


            taball = "2";
            selected_tab.Value = taball;
        }

        private void UpdateSecQueonUserTable(string staffno)
        {

            try
            {

                bool secque = false;

                var Staff = Db.tblUser.Where(s => s.StaffNumber == staffno).FirstOrDefault();

                Staff.SecurityQueCreated = secque;

                Db.Entry(Staff).State = EntityState.Modified;
                Db.SaveChanges();


                SendSetupQueLink(staffno);

                

              

            }


            catch (Exception ex)
            {
                asplbl.Text = ex.Message;

                ShowMessage(asplbl.Text);
            }
        }

        private void SendSetupQueLink(string staffno)
        {
            try
            {



                var getvalue = (from s in Db.tblUser.Where(s=> s.StaffNumber == staffno)
                              select new SetupUser
                              {

                                  FullName = s.FullName,

                                  Email = s.Email,


                                  


                              }).ToArray();


                string fullname = getvalue[0].FullName;

                string email = getvalue[0].FullName;

               








                string encryptstaffno = HttpUtility.UrlEncode(Encrypt(staffno.Trim()));
                string encryptemail = HttpUtility.UrlEncode(Encrypt(email.Trim()));




                StringBuilder sb = new StringBuilder();
                sb.Append("<b>Dear" + " " + fullname + "</b> <br/>");
                sb.Append("<b> We have received your request to reset your </b> <br/>");
                sb.Append("<b> security questions for Result Processing Management System (RPMS).</b><br/>");
                sb.Append("<b>Please follow the link below:</b> <br/>");
                sb.Append("<a href=http://localhost:50860/SetupSecurityQuestion.aspx?username=" + encryptstaffno + "&email=" + encryptemail + "> ");
                sb.Append("<b> Click here to reset your security question</b></a><br/>");
                sb.Append("<b>Your Username: " + staffno + "</b> <br/>");
                //sb.Append("<b>Your Password: " + strNewPassword + "</b> <br/>");
                sb.Append("<b> You will be prompted for the new security question you wish to use. </b>");
                sb.Append("<b> This Link is valid for only 24 hours. </b> <br/>");
                sb.Append("<b> You are strongly advised not to share your password or </b>");
                sb.Append("<b> enable remember password on shared system.  </b> <br/>");
                sb.Append("<b>Enjoy the use of our robust result processing management system. </b> <br/> <br/><br/><br/><br/>");

                sb.Append("<b><a href=http://www.oouagoiwoye.edu.ng > Powered by OOU ICT</a> </b> <br/>");


                var config = (from s in Db.tblConfig
                              select new SetupConfig
                              {

                                  Email = s.Email,

                                  Password = s.Password,


                                  EmailHost = s.EmailHost,
                                  Port = s.Port,
                                  EnableSSL = s.EnableSSL,
                                  BodyHtml = s.BodyHtml,



                              }).ToArray();


                string Senderemail = config[0].Email;

                string password = config[0].Password;

                string host = config[0].EmailHost;

                int port = config[0].Port;

                bool ssl = config[0].EnableSSL;

                bool bodyhtml = config[0].BodyHtml;



                MailMessage message = new System.Net.Mail.MailMessage(Senderemail, email.Trim(), "RPMS", sb.ToString());

                SmtpClient smtp = new SmtpClient();

                smtp.Host = host;

                smtp.Port = port;

                smtp.Credentials = new System.Net.NetworkCredential(Senderemail, password);

                smtp.EnableSsl = ssl;


                message.IsBodyHtml = bodyhtml;

                smtp.Send(message);

            }

            catch (Exception ex)
            {
                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);

            }
        }
    }
    
    }