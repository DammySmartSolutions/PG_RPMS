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
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;



namespace CorrectRPMS
{
    public partial class SetupSecurityQuestion : System.Web.UI.Page
    {

        RPMSEntities Db = new RPMSEntities();

        string staffno;

        bool security;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStaffNo();
             // //  VerifySecQue();
                LoadSecurityQuestions1();
                LoadSecurityQuestions2();
                LoadSecurityQuestions3();



                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            }
        }

        //private void VerifySecQue()
        //{
        //    var config = (from s in Db.tblUser.Where(s=>s.StaffNumber == staffno)
        //                  select new SetupUser
        //                  {

        //                      SecurityQueCreated = (bool)s.SecurityQueCreated,

                           

        //                  }).ToArray();


        //    security = config[0].SecurityQueCreated;


        //    if (security == true)
        //    {


        //    }
        //}

        private void LoadSecurityQuestions2()
        {
            var allow = (from s in Db.tblSecurityQuestionList
                         select new { s.SecurityQuestionID, s.Question }).ToList();


            ddlsecurity2.DataTextField = "Question";
            ddlsecurity2.DataValueField = "SecurityQuestionID";
            ddlsecurity2.DataSource = allow;
            ddlsecurity2.DataBind();
            ddlsecurity2.Items.Insert(0, "--Select Security Question 2--");
        }

        private void LoadSecurityQuestions3()
        {

            var allow = (from s in Db.tblSecurityQuestionList
                         select new { s.SecurityQuestionID, s.Question }).ToList();


            ddlsecurity3.DataTextField = "Question";
            ddlsecurity3.DataValueField = "SecurityQuestionID";
            ddlsecurity3.DataSource = allow;
            ddlsecurity3.DataBind();
            ddlsecurity3.Items.Insert(0, "--Select Security Question 3--");

        }

        private void LoadSecurityQuestions1()
        {
            var allow = (from s in Db.tblSecurityQuestionList
                         select new { s.SecurityQuestionID, s.Question }).ToList();


            ddlsecurity1.DataTextField = "Question";
            ddlsecurity1.DataValueField = "SecurityQuestionID";
            ddlsecurity1.DataSource = allow;
            ddlsecurity1.DataBind();
            ddlsecurity1.Items.Insert(0, "--Select Security Question 1--");
        }

        private void GetStaffNo()
        {
            //staffno = Decrypt(HttpUtility.UrlDecode(Request.QueryString["username"]));

            staffno = Details.StaffNumber; 

        }

        protected void btnSubmit_ServerClick(object sender, EventArgs e)
        {
            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("SetupSecurityQuestion.aspx");
            }



            if (ddlsecurity1.SelectedItem.Text == "--Select Security Question 1--")
            {
                ShowMessage("Please Select Faculty");
                ddlsecurity1.Focus();
                return;
            }


            if (ddlsecurity2.SelectedItem.Text == "--Select Security Question 2--")
            {
                ShowMessage("Please Select Faculty");
                ddlsecurity2.Focus();
                return;
            }


            if (ddlsecurity3.SelectedItem.Text == "--Select Security Question 3--")
            {
                ShowMessage("Please Select Faculty");
                ddlsecurity3.Focus();
                return;
            }



            if (txtsecurity1.Value== "")
            {
                ShowMessage("Please Enter Security Answer 1!");
                txtsecurity1.Focus();
                return;
            }



            if (txtsecurity2.Value == "")
            {
                ShowMessage("Please Enter Security Answer 2!");
                txtsecurity2.Focus();
                return;
            }



            if (txtsecurity3.Value == "")
            {
                ShowMessage("Please Enter Security Answer 3!");
                txtsecurity3.Focus();
                return;
            }


            staffno = Details.StaffNumber;


            try
            {
                var Staff = Db.tblUserSecurityAnswer.Where(x => x.StaffNumber == staffno).FirstOrDefault();


                if (Staff == null)
                {
                    var newque = new tblUserSecurityAnswer();

                    newque.StaffNumber = staffno;

                    newque.SecurityQue1 = ddlsecurity1.SelectedItem.Text;
                    newque.Answer1 = txtsecurity1.Value;

                    newque.SecurityQue2 = ddlsecurity2.SelectedItem.Text;
                    newque.Answer2 = txtsecurity2.Value;

                    newque.SecurityQue3 = ddlsecurity3.SelectedItem.Text;
                    newque.Answer3 = txtsecurity3.Value;

                    Db.tblUserSecurityAnswer.Add(newque);
                    Db.SaveChanges();

                    ShowMessage("Security Questions Setup Successful!!!");

                    LoadSecurityQuestions1();
                    LoadSecurityQuestions2();
                    LoadSecurityQuestions3();
                    txtsecurity1.Value = "";
                    txtsecurity2.Value = "";
                    txtsecurity3.Value = "";

                    UpdateUserTable(staffno);


                }



                else
                {


                    Staff.StaffNumber = staffno;

                    Staff.SecurityQue1 = ddlsecurity1.SelectedItem.Text;
                    Staff.Answer1 = txtsecurity1.Value;

                    Staff.SecurityQue2 = ddlsecurity2.SelectedItem.Text;
                    Staff.Answer2 = txtsecurity2.Value;

                    Staff.SecurityQue3 = ddlsecurity3.SelectedItem.Text;
                    Staff.Answer3 = txtsecurity3.Value;


                    Db.Entry(Staff).State = EntityState.Modified;
                    Db.SaveChanges();

                    ShowMessage("Security Questions Setup Successful!!!");

                    LoadSecurityQuestions1();
                    LoadSecurityQuestions2();
                    LoadSecurityQuestions3();
                    txtsecurity1.Value = "";
                    txtsecurity2.Value = "";
                    txtsecurity3.Value = "";


                }

            }

            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }



        }

        private void UpdateUserTable(string staffno)
        {
            try
            {
                var Staff = Db.tblUser.Where(x => x.StaffNumber == staffno).FirstOrDefault();


                bool secque = true;

                Staff.SecurityQueCreated = secque;


                Db.Entry(Staff).State = EntityState.Modified;
                Db.SaveChanges();

                Response.Redirect("LoginPage.aspx");

            }


            catch (Exception ex)
            {
                ShowMessage(ex.Message);

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








        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            ////lblName.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["name"]));
            ////lblTechnology.Text = Decrypt(HttpUtility.UrlDecode(Request.QueryString["technology"]));


            return cipherText;
        }


    }
}