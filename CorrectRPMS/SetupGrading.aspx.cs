﻿using System;
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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Security.Cryptography;

namespace CorrectRPMS
{
    public partial class SetupGrading : System.Web.UI.Page
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
                    LoadGradingSystem();

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





        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }


        private void ShowMessage(string Message)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "alert('" + Message + "')", true);

        }

        private void LoadGradingSystem()
        {
            var allow = (from s in Db.tblGradingSystem
                         select new { s.GradingSystemID, s.GradingSystem }).ToList();


            ddlgradingSystem.DataTextField = "GradingSystem";
            ddlgradingSystem.DataValueField = "GradingSystemID";
            ddlgradingSystem.DataSource = allow;
            ddlgradingSystem.DataBind();
            ddlgradingSystem.Items.Insert(0, "--Select Grading System--");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupGrading.aspx");
            }


            if (ddlgradingSystem.SelectedItem.Text == "--Select Grading System--")
            {
                ShowMessage("Please Select Faculty");
                ddlgradingSystem.Focus();
                return;
            }



            if (txtminimun.Value == "")
            {
                ShowMessage("Please Enter Minimum Score");
                txtminimun.Focus();
                return;
            }


            if (txtmaximum.Value == "")
            {
                ShowMessage("Please Enter Maximum Score");
                txtmaximum.Focus();
                return;
            }


            if (txtgradepoint.Value == "")
            {
                ShowMessage("Please Enter Grade Point");
                txtgradepoint.Focus();
                return;
            }


            if (txtgradeletter.Value == "")
            {
                ShowMessage("Please Enter Grade Letter");
                txtgradeletter.Focus();
                return;
            }



            int facid = 0;
            if (!string.IsNullOrEmpty(gradingid.Value))
            {
                facid = int.Parse(gradingid.Value);
            }



            //var getfaculty = (from t in Db.tblFaculty.Where(x => x.FacultyName == ddlfaculty.SelectedItem.Text)
            //                  select new SetupFaculty
            //                  {

            //                      FacultyCode = t.FacultyCode,
            //                      FacultyType = t.FacultyType,
            //                      FacultyID = t.FacultyID,

            //                  }).ToArray();

            //string facultycode = (getfaculty[0].FacultyCode).ToUpper();
            //string facultytype = (getfaculty[0].FacultyType).ToUpper();


            //int facultyid = (getfaculty[0].FacultyID);


            string gradingsystem = ddlgradingSystem.SelectedItem.Text;
            int gradingsystemid  = int.Parse(ddlgradingSystem.SelectedItem.Value);

            decimal minscore = decimal.Parse(txtminimun.Value);
            decimal maxscore = decimal.Parse(txtmaximum.Value);
            decimal gradepoint = decimal.Parse(txtgradepoint.Value);


            string gradeletter = txtgradeletter.Value.ToUpper();


            var facultydetail = Db.tblGrading.Where(x => x.GradingID == facid).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {





                    var NewFacultydetail = new tblGrading();
                    NewFacultydetail.GradingSystemType = gradingsystem;
                    NewFacultydetail.GradingSystemTypeID = gradingsystemid;
                    NewFacultydetail.MinScore = minscore;
                    NewFacultydetail.MaxScore = maxscore;
                    NewFacultydetail.GradePoint = gradepoint;
                    NewFacultydetail.GradeLetter = gradeletter;
                    NewFacultydetail.GradePassed = chkbox.Checked;

                    Db.tblGrading.Add(NewFacultydetail);
                    Db.SaveChanges();



                    ShowMessage("Successful");

                    WriteToAuditInsert();

                    LoadGradingSystem();
                    txtgradeletter.Value = "";
                    txtgradepoint.Value = "";

                    txtmaximum.Value = "";
                    txtminimun.Value = "";
                    chkbox.Checked = false;

                    gradingid.Value = "";
               

                }
                else
                {



                    facultydetail.GradingSystemType = gradingsystem;
                    facultydetail.GradingSystemTypeID = gradingsystemid;
                    facultydetail.MinScore = minscore;
                    facultydetail.MaxScore = maxscore;
                    facultydetail.GradePoint = gradepoint;
                    facultydetail.GradeLetter = gradeletter;
                    facultydetail.GradePassed = chkbox.Checked;

                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();
                    ShowMessage("Successful");
                    WriteToAuditUpdate();

                    LoadGradingSystem();
                    txtgradeletter.Value = "";
                    txtgradepoint.Value = "";

                    txtmaximum.Value = "";
                    txtminimun.Value = "";
                    chkbox.Checked = false;

                    gradingid.Value = "";
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


                string staffnum = StaffNumber = Session["StaffNumber"].ToString();
                string action = "Insert";
                DateTime DatePerformed = DateTime.Now;
                string created = txtgradeletter.Value;

                string Description = "You Added new Grade Letter: " + created;

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


                string staffnum = StaffNumber = Session["StaffNumber"].ToString();
                string action = "Update";
                DateTime DatePerformed = DateTime.Now;
                string created = txtgradeletter.Value;
                string update = UpdateValue.Value;
                string Description = "You updated Grade Letter: " + update + " to a new Grade Letter: " + created;

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