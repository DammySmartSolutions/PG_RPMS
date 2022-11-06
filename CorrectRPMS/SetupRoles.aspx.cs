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
    public partial class SetupRoles : System.Web.UI.Page
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

                Response.Redirect("SetupRoles.aspx");
            }


            if (txtrole.Value == "")
            {
                ShowMessage("Please Enter Role");
                txtrole.Focus();
                return;
            }



            int facid = 0;
            if (!string.IsNullOrEmpty(roleid.Value))
            {
                facid = int.Parse(roleid.Value);
            }

            string role = txtrole.Value.ToUpper();

            var facultydetail = Db.tblRoles.Where(x => x.RoleID == facid).FirstOrDefault();
            try
            {
                if (facultydetail == null)
                {





                    var NewFacultydetail = new tblRoles();
                    NewFacultydetail.Roles = role;



                    Db.tblRoles.Add(NewFacultydetail);
                    Db.SaveChanges();



                    ShowMessage("Successful");
                    txtrole.Value = "";

                    roleid.Value = "";

                }
                else
                {



               
                    facultydetail.Roles = role;

                    Db.Entry(facultydetail).State = EntityState.Modified;
                    Db.SaveChanges();


                    ShowMessage("Successful");
                    txtrole.Value = "";

                    roleid.Value = "";


                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);

            }


        }
    }
}