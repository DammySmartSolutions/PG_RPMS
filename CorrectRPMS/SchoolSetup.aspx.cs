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

namespace CorrectRPMS
{
    public partial class SchoolSetup : System.Web.UI.Page
    {
        RPMSEntities Db = new RPMSEntities();

        string LogoAddress;

        public string Logo { get; private set; }

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


        protected void btnupload_Click(object sender, EventArgs e)
        {


            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SchoolSetup.aspx");
            }


            try { 
            string strFileName;
            string strFilePath;
            string strFolder;
            strFolder = Server.MapPath("./img/");
            strFileName = myfile.PostedFile.FileName;
            strFileName = Path.GetFileName(strFileName);

            if (myfile.Value != "")
            {
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }

                strFilePath = strFolder + strFileName;

                if (File.Exists(strFilePath))
                {
                    ShowMessage("Already Exists on the Server!");



                    SchoolLogo.Src = "img/" + strFileName;

                }

                else
                {

                    myfile.PostedFile.SaveAs(strFilePath);
                    ShowMessage("Uploaded Successfully!");

                    SchoolLogo.Src = "img/" + strFileName;

                }
            }

            else
            {
                ShowMessage("Click to select the file to Upload");
            }

            schlogo.Value = "img/" + strFileName;
            LogoAddress = schlogo.Value;


            }

            catch(Exception ex)
            {
                ShowMessage(ex.Message);
            }

        }

        protected void btnEditLogo_Click(object sender, EventArgs e)
        {
            var getlogo = (from t in Db.tblSchoolDetails
                           select new SchoolSetup
                           {
                               Logo = t.Logo,

                           }).ToArray();

            string logoaddress = getlogo[0].Logo;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SchoolSetup.aspx");
            }


            int schdetailid = 0;
            if (!string.IsNullOrEmpty(schid.Value))
            {
                schdetailid = int.Parse(schid.Value);
            }

           

        



            var saveschdetail = Db.tblSchoolDetails.Where(x => x.SchoolID == schdetailid).FirstOrDefault();
            try
            {
                if (saveschdetail == null)
                {


                    


                    var NewSchdetail = new tblSchoolDetails();
                    NewSchdetail.Name = txtname.Value.ToUpper();
                    NewSchdetail.Address = txtaddress.Value.ToUpper();
                    NewSchdetail.Phone = txtphone.Value;
                    NewSchdetail.Email = txtemail.Value;
                    NewSchdetail.Logo = schlogo.Value;
                  

                    Db.tblSchoolDetails.Add(NewSchdetail);
                    Db.SaveChanges();



                    ShowMessage("Successful");
                    txtname.Value = "";
                    txtaddress.Value = "";
                    txtphone.Value = "";
                    txtemail.Value = "";
                    SchoolLogo.Src = "";
                    
                }
                else
                {



                    string schimg = schlogo.Value;
                    saveschdetail.Name = txtname.Value.ToUpper();
                    saveschdetail.Address = txtaddress.Value.ToUpper();
                    saveschdetail.Phone = txtphone.Value;
                    saveschdetail.Email = txtemail.Value;
                    saveschdetail.Logo = schimg;
                   

                    Db.Entry(saveschdetail).State = EntityState.Modified;
                    Db.SaveChanges();
                    ShowMessage("Successful");
                    txtname.Value = "";
                    txtaddress.Value = "";
                    txtphone.Value = "";
                    txtemail.Value = "";
                    SchoolLogo.Src = "";
                    
                }

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);

            }







        }
    }
}