using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
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

using System.Security.Cryptography;
using System.Net;

namespace CorrectRPMS
{
    public partial class LoginPage : System.Web.UI.Page
    {


        RPMSEntities Db = new RPMSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
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

        protected void btnlogin_ServerClick(object sender, EventArgs e)
        {



            



            try
            {


                if (txtusername.Value == "")
                {
                    txtusername.Focus();
                    ShowMessage("Please Enter Staff Number");
                    return;
                }


                if (txtpassword.Value == "")
                {
                    ShowMessage("Please Enter password");
                    txtpassword.Focus();

                    return;
                }




                string staffno = txtusername.Value.ToUpper();

                string password = txtpassword.Value;



                string pass = encryption(password);


                var User = Db.tblUser.Where(x => x.StaffNumber == staffno && x.Password == pass).FirstOrDefault();


                
                if (User == null)
                {

                    asplbl.Text = "Invalid Username or Password";

                    ShowMessage(asplbl.Text);
                    return;
                }


                else 
                {

                    var GetUserDetails = (from s in Db.tblUser.Where(s => s.StaffNumber == staffno)
                                  select new SetupUser
                                  {

                                      FullName = s.FullName,
                                      Email  = s.Email,
                                      Role = s.Role,
                                      Faculty = s.Faculty,
                                      StaffNumber = s.StaffNumber,
                                      Department =s.Department,
                                     


                                  }).ToArray();


                  string fullname = GetUserDetails[0].FullName;
                    string email = GetUserDetails[0].Email;
                    string Role = GetUserDetails[0].Role;
                    string Faculty = GetUserDetails[0].Faculty;
                    string StaffNumber = GetUserDetails[0].StaffNumber;
                    string dept = GetUserDetails[0].Department;


                    Session["FullName"] = fullname;
                    Session["Email"] = email;
                    Session["Role"] = Role;

                    Session["Faculty"] = Faculty;
                    Session["StaffNumber"] = StaffNumber;
                    Session["Dept"] = dept;


                    SendMail(fullname, StaffNumber, email);


                    string previousurl = Request.QueryString["RedirectUr"];
                    // string item1 = base.Request.QueryString["Details.Page"];


                    string item1;


                       

                    if (Session["Page"] == null)
                    {
                        item1 = null;

                    }

                    else
                    {
                        item1 = Session["Page"].ToString();
                    }

                    {
                        if (!string.IsNullOrEmpty(item1))
                        {
                          ///  Response.Redirect(Server.UrlDecode(item1));

                            Response.Redirect(item1);
                        }
                        else
                        {
                            Response.Redirect("~/Dashboard.aspx");
                        }




                        //  Response.Redirect("Dashboard.aspx");

                    }
                   


                }



               






              




            }


            catch(Exception ex)
            {

                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);
            }
    }

        private void SendMail( string fullname, string StaffNumber, string email)
        {
            try
            {

                DateTime LoginTime = DateTime.Now;

                string name = Environment.MachineName;
                string hostname = System.Net.Dns.GetHostName();
             
                string Computername = System.Environment.GetEnvironmentVariable("COMPUTERNAME");

                string myIP = Dns.GetHostByName(hostname).AddressList[0].ToString();



                StringBuilder sb = new StringBuilder();
                sb.Append("<b>Dear" + " " + fullname +","+"</b> <br/>");
                sb.Append("<b> You're Welcome to Result Processing Management System (RPMS) </b> <br/>");
                sb.Append("<b> Your Login details are given below:</b><br/>");
                sb.Append("<b>Username: " + StaffNumber + "</b> <br/>");
                sb.Append("<b>Login Time: " + LoginTime + "</b> <br/>");
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

        public string encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }




    }
}
