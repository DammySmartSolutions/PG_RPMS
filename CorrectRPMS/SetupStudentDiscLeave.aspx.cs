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
    public partial class SetupStudentDiscLeave : System.Web.UI.Page
    {

        RPMSEntities Db = new RPMSEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadStatus();
                LoadSessSemester();

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

        private void LoadSessSemester()
        {
            var allow = (from s in Db.tblSessionSemester
                         select new { s.SessionSemesterID, s.SessionSemester }).ToList();


            ddlSessionSem.DataTextField = "SessionSemester";
            ddlSessionSem.DataValueField = "SessionSemesterID";
            ddlSessionSem.DataSource = allow;
            ddlSessionSem.DataBind();
            ddlSessionSem.Items.Insert(0, "--Select Session_Semester--");
        }

        private void LoadStatus()
        {
            var allow = (from s in Db.tblStatus
                         select new { s.StatusID, s.Status }).ToList();


            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataSource = allow;
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, "--Select Status--");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupStudentDiscLeave.aspx");
            }


            if (txtmatricno.Value == "")
            {
                ShowMessage("Please Enter Student Matric Number");
                txtmatricno.Focus();
                return;
            }

            if (ddlStatus.SelectedItem.Text == "--Select Status--")
            {
                ShowMessage("Please Select Status");
                ddlStatus.Focus();
                return;
            }




            if (ddlSessionSem.SelectedItem.Text == "--Select Session_Semester--")
            {
                ShowMessage("Please Select Session_Semester");
                ddlSessionSem.Focus();
                return;
            }


            if (txtduration.Value == "")
            {
                ShowMessage("Please Enter Duration");
                txtduration.Focus();
                return;
            }




            int facid = 0;
            if (!string.IsNullOrEmpty(studdiscleaveID.Value))
            {
                facid = int.Parse(studdiscleaveID.Value);
            }

            string matricno = txtmatricno.Value.ToUpper();
            string status = ddlStatus.SelectedItem.Text.ToUpper();
            string session = ddlSessionSem.SelectedItem.Text.ToUpper();

            int duration = int.Parse(txtduration.Value);

            DateTime datecreated = DateTime.Today;



            var facultydetail = Db.tblStudentDisciplineLeave.Where(x =>x.MatricNumber == matricno).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {





                    var NewFacultydetail = new tblStudentDisciplineLeave();
                    NewFacultydetail.MatricNumber = matricno;
                    NewFacultydetail.Status = status;
                    NewFacultydetail.SessSemester = session;
                    NewFacultydetail.Duration = duration;

                    NewFacultydetail.DateCreated = datecreated;


                    Db.tblStudentDisciplineLeave.Add(NewFacultydetail);
                    Db.SaveChanges();



                    ShowMessage("Successful");

                    txtmatricno.Value = "";
                    LoadStatus();
                    LoadSessSemester();
                    txtduration.Value = "";


                    studdiscleaveID.Value = "";

                }
                else
                {



                  

                    facultydetail.MatricNumber = matricno;
                    facultydetail.Status = status;
                    facultydetail.SessSemester = session;
                    facultydetail.Duration = duration;

                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();


                    ShowMessage("Successful");
                    txtmatricno.Value = "";
                    LoadStatus();
                    LoadSessSemester();
                    txtduration.Value = "";


                    studdiscleaveID.Value = "";

                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);

            }

        }
    }
}