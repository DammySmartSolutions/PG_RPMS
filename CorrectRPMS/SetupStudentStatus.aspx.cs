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

namespace CorrectRPMS
{
    public partial class SetupStudentStatus : System.Web.UI.Page
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

                Response.Redirect("SetupStudentStatus.aspx");
            }


            if (txtstatus.Value == "")
            {
                ShowMessage("Please Enter Student Status");
                txtstatus.Focus();
                return;
            }



            int facid = 0;
            if (!string.IsNullOrEmpty(statusid.Value))
            {
                facid = int.Parse(statusid.Value);
            }

            string status = txtstatus.Value.ToUpper();

            var facultydetail = Db.tblStatus.Where(x => x.StatusID == facid).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {





                    var NewFacultydetail = new tblStatus();
                    NewFacultydetail.Status = status;



                    Db.tblStatus.Add(NewFacultydetail);
                    Db.SaveChanges();



                    ShowMessage("Successful");
                    txtstatus.Value = "";

                    statusid.Value = "";

                }
                else
                {



                    facultydetail.Status = status;


                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();


                    ShowMessage("Successful");
                    txtstatus.Value = "";

                    statusid.Value = "";

                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);

            }

        }
    }
}