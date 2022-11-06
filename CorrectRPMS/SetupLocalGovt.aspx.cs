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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Security.Cryptography;

namespace CorrectRPMS
{
    public partial class SetupLocalGovt : System.Web.UI.Page
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
                    LoadNation();

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


        private void LoadNation()
        {
            var allow = (from s in Db.tblNation
                         select new { s.NationID, s.Nation }).ToList();


            ddlnationName.DataTextField = "Nation";
            ddlnationName.DataValueField = "NationID";
            ddlnationName.DataSource = allow;
            ddlnationName.DataBind();
            ddlnationName.Items.Insert(0, "--Select Nation--");
        }





        protected void ddlnationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nation = ddlnationName.SelectedItem.Text;

            var allow = (from s in Db.tblState.Where(x => x.NationName == nation)
                         select new { s.StateId, s.Statename }).ToList();


            ddlstate.DataTextField = "Statename";
            ddlstate.DataValueField = "StateId";
            ddlstate.DataSource = allow;
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, "--Select State--");

            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupLocalGovt.aspx");
            }


            if (ddlnationName.SelectedItem.Text == "--Select Nation--")
            {
                ShowMessage("Please Select Nation");
                ddlnationName.Focus();
                return;
            }



            if (ddlstate.SelectedItem.Text == "--Select State--")
            {
                ShowMessage("Please Select State");
                ddlstate.Focus();
                return;
            }



            if (txtlganame.Value == "")
            {
                ShowMessage("Please Enter Local Govt. Name");
                txtlganame.Focus();
                return;
            }




            int facid = 0;
            if (!string.IsNullOrEmpty(lgaid.Value))
            {
                facid = int.Parse(lgaid.Value);
            }





            string nationname = ddlnationName.SelectedItem.Text.ToUpper();

            string statename = ddlstate.SelectedItem.Text.ToUpper();

            string lganame = txtlganame.Value.ToUpper();

            int stateid = int.Parse(ddlstate.SelectedItem.Value);


          


          


            var facultydetail = Db.tblLocalGovt.Where(x => x.LocalGovtID == facid).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {





                    var NewFacultydetail = new tblLocalGovt();

                    NewFacultydetail.NationName = nationname;
                    NewFacultydetail.State = statename;

                    NewFacultydetail.LocalGovt = lganame;
                    NewFacultydetail.StateID = stateid;
                    


                    Db.tblLocalGovt.Add(NewFacultydetail);
                    Db.SaveChanges();



                    ShowMessage("Successful");

                    txtlganame.Value = "";
                    
                    lgaid.Value = "";
                    LoadNation();

                }
                else
                {


                  
                    facultydetail.NationName = nationname;
                    facultydetail.State = statename;

                    facultydetail.LocalGovt = lganame;
                    facultydetail.StateID = stateid;




                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();
                    ShowMessage("Successful");

                    txtlganame.Value = "";

                    lgaid.Value = "";
                    LoadNation();


                }

            }
            catch (Exception ex)
            {
                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);

            }



        }
    }
}