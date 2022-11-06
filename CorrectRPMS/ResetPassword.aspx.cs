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
    public partial class ResetPassword : System.Web.UI.Page
    {


        RPMSEntities Db = new RPMSEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckLinkValidity();


               Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            }
        }

        private void CheckLinkValidity()
        {

            string staffno;

            string email;

            try
            {
                staffno = Decrypt(HttpUtility.UrlDecode(Request.QueryString["username"]));
                email = Decrypt(HttpUtility.UrlDecode(Request.QueryString["email"]));


                var getvalues = (from s in Db.tblUser.Where(s=>s.StaffNumber == staffno && s.Email == email)
                              select new SetupUser
                              {

                                  LinkExpirationDate = (DateTime)s.LinkExpirationDate,

                                 
                              }).ToArray();


                DateTime expiredlinkdate = getvalues[0].LinkExpirationDate;


                DateTime today = DateTime.Today;



                if (expiredlinkdate > today || expiredlinkdate == today)
                {

                    txtusername.Value = staffno;
                }


                else if (expiredlinkdate < today)
                {


                    asplbl.Text = "Link Expired Contact System Administrator for further action!";
                    ShowMessage(asplbl.Text);

                    Response.Redirect("LoginPage.aspx");
                }



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




        protected void btnlogin_ServerClick(object sender, EventArgs e)
        {


            if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
            {

                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            }

            else
            {

                Response.Redirect("ResetPassword.aspx");
            }




            try
            {


                if(txtusername.Value == "")
                {
                    txtusername.Focus();
                    ShowMessage("Please Enter Staff Number");
                    return;
                }


                if (txtpassword.Value == "")
                {
                    ShowMessage("Please Enter the  password sent to your email");
                    txtpassword.Focus();
                    
                    return;
                }


                if(txtnewpassword.Value == "")
                {
                    ShowMessage("Please Enter New Password");
                    txtnewpassword.Focus();

                    return;
                }



                if (txtconfirmpassword.Value == "")
                {
                    ShowMessage("Please Re-enter New Password");
                    txtconfirmpassword.Focus();

                    return;
                }


                string staffno = txtusername.Value.ToUpper();

                string password = txtpassword.Value;

                string newpassword = txtnewpassword.Value;

                string confirmpassword = txtconfirmpassword.Value;

                string entrypassword;

                if(newpassword == confirmpassword)
                {
                    entrypassword = newpassword;
                }

                else
                {

                    ShowMessage("Password MisMatch");
                    txtnewpassword.Focus();
                    return;
                }


                var facultydetail = Db.tblUser.Where(x => x.StaffNumber == staffno).FirstOrDefault();


                bool linkactive = true;

                bool passwordupdated = true;


                string pass = encryption(entrypassword);



                facultydetail.LinkActivated = linkactive;

                facultydetail.UpdatePassword = passwordupdated;

                facultydetail.Password = pass;


                Db.Entry(facultydetail).State = EntityState.Modified;
                Db.SaveChanges();

                asplbl.Text = "Account Creation Successfully";

                ShowMessage(asplbl.Text);

                  VerifySecQue(staffno);

              


            }


            catch (Exception ex)
            {

                asplbl.Text = ex.Message;
                ShowMessage(asplbl.Text);

            }

        


        }

        private void VerifySecQue(string staffno)
        {

            try
            {

            
            var config = (from s in Db.tblUser.Where(s => s.StaffNumber == staffno)
                         select new SetupUser
                         {

                             SecurityQueCreated = (bool)s.SecurityQueCreated,



                         }).ToArray();


         bool   security = config[0].SecurityQueCreated;


            if (security == true)
            {


            }


            else
            {


                    Details.StaffNumber = staffno;



                    string encryptstaffno = HttpUtility.UrlEncode(Encrypt(staffno.Trim()));


                 
                   ///// Link.HRef = "SetupSecurityQuestion.aspx?username = " + encryptstaffno;

                    Response.Redirect("SetupSecurityQuestion.aspx");
            }


            }



            catch (Exception ex)
            {
                asplbl.Text = ex.Message;

                ShowMessage(asplbl.Text);
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






        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }



    }
}