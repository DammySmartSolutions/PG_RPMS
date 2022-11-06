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
    public partial class SetupDepartment : System.Web.UI.Page
    {
        RPMSEntities Db = new RPMSEntities();
        OleDbConnection Econ;
       SqlConnection con;

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
                    LoadFaculty();

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







        private void LoadFaculty()
        {
            var allow = (from s in Db.tblFaculty
                         select new { s.FacultyID, s.FacultyName }).ToList();


            ddlfaculty.DataTextField = "FacultyName";
            ddlfaculty.DataValueField = "FacultyID";
            ddlfaculty.DataSource = allow;
            ddlfaculty.DataBind();
            ddlfaculty.Items.Insert(0, "--Select Faculty--");
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }


        private void ShowMessage(string Message)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "alert('" + Message + "')", true);

        }




        protected void btnupload_Click(object sender, EventArgs e)
        {


            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupDepartment.aspx");
            }



            try
            {

                Db.Database.ExecuteSqlCommand("Delete from tblDeptTemp");
                Db.SaveChanges();

                string strFileName;
                string strFilePath;
                string strFolder;
                string fileExt;
                string excelPath;

                strFolder = Server.MapPath("./Files/");
                strFileName = FileUpload1.PostedFile.FileName;
                strFileName = Path.GetFileName(strFileName);

                if (FileUpload1.Value != "")
                {
                    if (!Directory.Exists(strFolder))
                    {
                        Directory.CreateDirectory(strFolder);
                    }

                    strFilePath = strFolder + strFileName;


                }
                else
                {


                    ShowMessage("Choose CSV File to Upload!!!");
                    return;

                }



                fileExt = Path.GetExtension(FileUpload1.PostedFile.FileName);

                if (fileExt == ".csv")

                {
                    excelPath = Server.MapPath("~/Files/") + strFileName;

                    FileUpload1.PostedFile.SaveAs(excelPath);

                }

                else

                {
                    ShowMessage("Please upload valid .csv extension file!!!");
                    return;



                }



                DataTable dtExcelData = new DataTable();

                //[OPTIONAL]: It is recommended as otherwise the data will be considered as String by default.

                dtExcelData.Columns.AddRange(new DataColumn[6]
                { new DataColumn("Dept Code", typeof(string)),
                      new DataColumn("Dept Name", typeof(string)),
                       new DataColumn("Faculty Code", typeof(string)),
                         new DataColumn("Faculty Name", typeof(string)),
                           new DataColumn("Faculty Type", typeof(string)),
                            new DataColumn("Faculty ID", typeof(int)),
                });

                string csvData = File.ReadAllText(excelPath);

                string NewcsvData = csvData.Remove(0, csvData.IndexOf(Environment.NewLine) + Environment.NewLine.Length);

                //  string[] read = csvData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                // iterate over each row and Split it to New line.  



                //foreach (string Row in csvData.Split('\r'))
                //{


                foreach (string items in NewcsvData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))

                {

                    // Check for is null or empty row record  

                    if (!string.IsNullOrEmpty(items))

                    {

                        // added rows  

                        dtExcelData.Rows.Add();

                        int i = 0;

                        foreach (string FileRec in items.Split(','))

                        {

                            dtExcelData.Rows[dtExcelData.Rows.Count - 1][i] = FileRec;
                            i++;

                        }

                    }

                }




                string consString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(consString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {


                        //Set the database table name
                        sqlBulkCopy.DestinationTableName = "dbo.tblDeptTemp";

                        //[OPTIONAL]: Map the Excel columns with that of the database table
                        sqlBulkCopy.ColumnMappings.Add("Dept Code", "DeptCode");
                        sqlBulkCopy.ColumnMappings.Add("Dept Name", "DeptName");
                        sqlBulkCopy.ColumnMappings.Add("Faculty Code", "FacultyCode");

                        sqlBulkCopy.ColumnMappings.Add("Faculty Name", "FacultyName");
                        sqlBulkCopy.ColumnMappings.Add("Faculty Type", "FacultyType");
                        sqlBulkCopy.ColumnMappings.Add("Faculty ID", "FacultyID");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dtExcelData);
                        con.Close();
                    }

                }



                SaveUploadData();




            }





            catch (Exception ex)
            {
                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);
            }



        }

        private void SaveUploadData()
        {
            try

            {
                string insert;



                insert = "INSERT INTO tblDept(DeptCode, DeptName, FacultyCode, FacultyName, FacultyType, FacultyID) " +
             "select DeptCode, DeptName,FacultyCode, FacultyName, FacultyType, FacultyID from tblDeptTemp " +
                " EXCEPT " +
               " SELECT DeptCode, DeptName, FacultyCode, FacultyName, FacultyType,FacultyID  FROM tblDept";




                string consString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(consString);
                SqlCommand cmd = new SqlCommand(insert, con);
                con.Open();
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                con.Close();




                ShowMessage("Uploaded Successfully!");

                WriteToAuditUpload();

            }


            catch (Exception ex)
            {

                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupDepartment.aspx");
            }


            if (ddlfaculty.SelectedItem.Text == "--Select Faculty--")
            {
                ShowMessage("Please Select Faculty");
                ddlfaculty.Focus();
                return;
            }



            if (txtdeptcode.Value == "")
            {
                ShowMessage("Please Enter Departmental Code");
                txtdeptcode.Focus();
                return;
            }


            if (txtdept.Value == "")
            {
                ShowMessage("Please Enter Department Name");
                txtdept.Focus();
                return;
            }

           


            int facid = 0;
            if (!string.IsNullOrEmpty(deptid.Value))
            {
                facid = int.Parse(deptid.Value);
            }



            var getfaculty = (from t in Db.tblFaculty.Where(x=>x.FacultyName == ddlfaculty.SelectedItem.Text)
                           select new SetupFaculty
                           {

                               FacultyCode = t.FacultyCode,
                               FacultyType = t.FacultyType,
                               FacultyID = t.FacultyID,

                           }).ToArray();

            string facultycode = (getfaculty[0].FacultyCode).ToUpper();
            string facultytype = (getfaculty[0].FacultyType).ToUpper();
            int facultyid = (getfaculty[0].FacultyID);

            string deptcode = txtdeptcode.Value.ToUpper();
            string dept = txtdept.Value.ToUpper();


            var facultydetail = Db.tblDept.Where(x => x.DeptID == facid).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {





                    var NewFacultydetail = new tblDept();
                    NewFacultydetail.FacultyName = ddlfaculty.SelectedItem.Text.ToUpper();
                    NewFacultydetail.FacultyType = facultytype;
                    NewFacultydetail.FacultyCode = facultycode;
                    NewFacultydetail.DeptCode = deptcode;
                    NewFacultydetail.DeptName = dept;
                    NewFacultydetail.FacultyID = facultyid;

                    Db.tblDept.Add(NewFacultydetail);
                    Db.SaveChanges();



                    ShowMessage("Successful");

                    WriteToAuditInsert();
                    txtdept.Value = "";
                    txtdeptcode.Value = "";
                    deptid.Value = "";
                    LoadFaculty();

                }
                else
                {



                    facultydetail.FacultyName = ddlfaculty.SelectedItem.Text.ToUpper();
                    facultydetail.FacultyType = facultytype;
                    facultydetail.FacultyCode = facultycode;
                    facultydetail.DeptCode = deptcode;
                    facultydetail.DeptName = dept;
                    facultydetail.FacultyID = facultyid;


                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();
                    ShowMessage("Successful");

                    WriteToAuditUpdate();
                    txtdept.Value = "";
                    txtdeptcode.Value = "";
                    deptid.Value = "";
                    LoadFaculty();

                }

            }
            catch (Exception ex)
            {
                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);

            }




        }

        protected void btndownload_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "RpmsDepartUploadTemplate.csv";

                //Path of the File to be downloaded.
                string filePath = Server.MapPath(string.Format("~/UploadTemplate/{0}", fileName));

                //Content Type and Header.
                Response.ContentType = "application/csv";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

                //Writing the File to Response Stream.
                Response.WriteFile(filePath);

                //Flushing the Response.
                Response.Flush();
                Response.End();
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
                string created = txtdept.Value;

                string Description = "You Created new Department: " + created;

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
                string created = txtdept.Value;
                string update = UpdateValue.Value;
                string Description = "You updated Dept: " + update + " to a new dept name: " + created;

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



        private void WriteToAuditUpload()
        {
            try
            {


                string staffnum = StaffNumber = Session["StaffNumber"].ToString();
                string action = "Upload";
                DateTime DatePerformed = DateTime.Now;
               
                string Description = "You Uploaded new Departments";

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