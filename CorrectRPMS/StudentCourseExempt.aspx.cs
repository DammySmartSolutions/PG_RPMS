using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CorrectRPMS.Models;
using CorrectRPMS.Models.RPMS;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;




namespace CorrectRPMS
{
    public partial class StudentCourseExempt : System.Web.UI.Page
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


        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("StudentCourseExempt.aspx");
            }


            if (txtmatricno.Value == "")
            {
                ShowMessage("Please Enter Student Matric Number");
                txtmatricno.Focus();
                return;
            }


            if (txtcoursecode.Value == "")
            {
                ShowMessage("Please Enter Course Code");
                txtcoursecode.Focus();
                return;
            }




            if (ddlStatus.SelectedItem.Text == "Select Status")
            {
                ShowMessage("Please Select Status");
                ddlStatus.Focus();
                return;
            }




          

            if (txtreason.Value == "")
            {
                ShowMessage("Please Enter Reason for Course Exemption");
                txtreason.Focus();
                return;
            }




            int facid = 0;
            if (!string.IsNullOrEmpty(studcourseexemptID.Value))
            {
                facid = int.Parse(studcourseexemptID.Value);
            }

            string matricno = txtmatricno.Value.ToUpper();
            string coursecode = txtcoursecode.Value.ToUpper();
            string status = ddlStatus.SelectedItem.Text.ToUpper();
            string reason = txtreason.Value.ToUpper();

          

            DateTime datecreated = DateTime.Today;



            var facultydetail = Db.tblStudentCourseExempt.Where(x => x.MatricNumber == matricno && x.StudCourseExemptID == facid).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {





                    var NewFacultydetail = new tblStudentCourseExempt();
                    NewFacultydetail.MatricNumber = matricno;
                    NewFacultydetail.CourseCode = coursecode;
                    NewFacultydetail.Status = status;
                    NewFacultydetail.Reason = reason;
                    NewFacultydetail.DateCreated = datecreated;


                    Db.tblStudentCourseExempt.Add(NewFacultydetail);
                    Db.SaveChanges();



                    ShowMessage("Successful");

                    txtmatricno.Value = "";
                    txtreason.Value = "";
                    txtcoursecode.Value = "";
                    ddlStatus.SelectedIndex = 0;

                    studcourseexemptID.Value = "";

                }
                else
                {





                 
                    facultydetail.MatricNumber = matricno;
                    facultydetail.CourseCode = coursecode;
                    facultydetail.Status = status;
                    facultydetail.Reason = reason;
                    facultydetail.DateCreated = datecreated;

                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();


                    ShowMessage("Successful");

                    txtmatricno.Value = "";
                    txtreason.Value = "";
                    txtcoursecode.Value = "";
                    ddlStatus.SelectedIndex = 0;

                    studcourseexemptID.Value = "";

                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);

            }

        }
    }
}