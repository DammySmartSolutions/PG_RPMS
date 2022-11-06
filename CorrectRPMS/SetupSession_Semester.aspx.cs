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
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Net;




namespace CorrectRPMS
{
    public partial class SetupSession_Semester : System.Web.UI.Page
    {
        RPMSEntities Db = new RPMSEntities();

        string StaffNumber;
        string StaffName;
        string StaffEmail;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["StaffNumber"] == null)
            {

                Session["Page"] = HttpContext.Current.Request.Url.AbsolutePath;
                Response.Redirect("LoginPage.aspx");

            }


            else
            {


                StaffNumber = Session["StaffNumber"].ToString();
                StaffName = Session["FullName"].ToString();
                StaffEmail = Session["Email"].ToString();


                SendMail(StaffNumber, StaffName, StaffEmail);






                if (!IsPostBack)
                {
                    LoadSession();
                    LoadSemester();

                    Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                }

            }
        }



        private void SendMail(string staffNumber, string staffName, string staffEmail)
        {
            try
            {

                DateTime LoginTime = DateTime.Now;

                string name = Environment.MachineName;
                string hostname = System.Net.Dns.GetHostName();

                string Computername = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

                string myIP = Dns.GetHostByName(hostname).AddressList[0].ToString();

                string page = HttpContext.Current.Request.Url.AbsolutePath;



                StringBuilder sb = new StringBuilder();
                sb.Append("<b>Dear" + " " + staffName + "," + "</b> <br/>");
                sb.Append("<b> You visited a new web page on the Result Processing Management System (RPMS) </b> <br/>");
                sb.Append("<b> Your User details are given below:</b><br/>");
                sb.Append("<b>Username: " + StaffNumber + "</b> <br/>");
                sb.Append("<b>Time Page Visited: " + LoginTime + "</b> <br/>");
                sb.Append("<b> Page Name: " + page + "</b> <br/>");
                sb.Append("<b> System Name: " + name + "</b> <br/>");
                sb.Append("<b> System IP Address: " + myIP + "</b> <br/>");
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



                MailMessage message = new System.Net.Mail.MailMessage(Senderemail, staffEmail.Trim(), "RPMS", sb.ToString());

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




        private void LoadSemester()
        {
            var allow = (from s in Db.tblSemester
                         select new { s.SemesterID, s.Semester }).ToList();


            ddlsemester.DataTextField = "Semester";
            ddlsemester.DataValueField = "SemesterID";
            ddlsemester.DataSource = allow;
            ddlsemester.DataBind();
            ddlsemester.Items.Insert(0, "--Select Semester--");
        }

        private void LoadSession()
        {
            var allow = (from s in Db.tblSession
                         select new { s.SessionID, s.Session }).ToList();


            ddlsession.DataTextField = "Session";
            ddlsession.DataValueField = "SessionID";
            ddlsession.DataSource = allow;
            ddlsession.DataBind();
            ddlsession.Items.Insert(0, "--Select Session--");
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

                Response.Redirect("SetupSession_Semester.aspx");
            }


            if (ddlsession.SelectedItem.Text == "--Select Session--")
            {
                ShowMessage("Please Select Session");
                ddlsession.Focus();
                return;
            }



            if (ddlsemester.SelectedItem.Text == "--Select Semester--")
            {
                ShowMessage("Please Select Semester");
                ddlsemester.Focus();
                return;
            }



           


            int facid = 0;
            if (!string.IsNullOrEmpty(sessionsemesterid.Value))
            {
                facid = int.Parse(sessionsemesterid.Value);
            }





            string session = ddlsession.SelectedItem.Text.ToUpper();

            string semester = ddlsemester.SelectedItem.Text.ToUpper();

         
            string sessemester  = session + " " + semester;





            var facultydetail = Db.tblSessionSemester.Where(x => x.SessionSemesterID == facid).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {





                    var NewFacultydetail = new tblSessionSemester();

                    NewFacultydetail.SessionSemester = sessemester;
                    NewFacultydetail.Session = session;
                    NewFacultydetail.Semester = semester;


                    Db.tblSessionSemester.Add(NewFacultydetail);
                    Db.SaveChanges();



                    ShowMessage("Successful");


                    WriteToAuditInsert();


                    sessionsemesterid.Value = "";

                    LoadSession();
                    LoadSemester();

                }
                else
                {



                    facultydetail.SessionSemester = sessemester;

                    facultydetail.Session = session;
                    facultydetail.Semester = semester;




                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();
                    ShowMessage("Successful");

                    WriteToAuditUpdate();

                    sessionsemesterid.Value = "";

                    LoadSession();
                    LoadSemester();


                }

            }
            catch (Exception ex)
            {
                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);

            }


        }



        private void WriteToAuditInsert()
        {
            try
            {



                string session = ddlsession.SelectedItem.Text.ToUpper();

                string semester = ddlsemester.SelectedItem.Text.ToUpper();


                string sessemester = session + " " + semester;


                string staffnum = StaffNumber = Session["StaffNumber"].ToString();
                string action = "Insert";
                DateTime DatePerformed = DateTime.Now;
                string created = sessemester;

                string Description = "You Added new Session/Semester: " + created;

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
                ShowMessage(ex.Message);
            }
        }




        private void WriteToAuditUpdate()
        {
            try
            {


                string session = ddlsession.SelectedItem.Text.ToUpper();

                string semester = ddlsemester.SelectedItem.Text.ToUpper();


                string sessemester = session + " " + semester;




                string staffnum = StaffNumber = Session["StaffNumber"].ToString();
                string action = "Update";
                DateTime DatePerformed = DateTime.Now;
                string created = sessemester;
                string update = UpdateValue.Value;
                string Description = "You updated Session/Semester: " + update + " to a new Session/Semester: " + created;

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
                ShowMessage(ex.Message);
            }

        }






    }
}