using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace PTSCOATING
{
    public partial class ChangePasswords : System.Web.UI.Page
    {
        helperclass hp = new helperclass();
        Hashtable parameters = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null || Session["UserId"] == "")
                {
                    Response.Redirect("login.aspx");
                }
                litusername.Text = Convert.ToString(Session["UserId"]);
            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                txtoldpassword.Text = txtoldpassword.Text.Replace("'", "");
                txtoldpassword.Text = txtoldpassword.Text.Replace(";", "");
                txtoldpassword.Text = txtoldpassword.Text.Replace("=", "");
                txtoldpassword.Text = txtoldpassword.Text.Replace("drop", "");

                txtnewpassword.Text = txtnewpassword.Text.Replace("'", "");
                txtnewpassword.Text = txtnewpassword.Text.Replace(";", "");
                txtnewpassword.Text = txtnewpassword.Text.Replace("=", "");
                txtnewpassword.Text = txtnewpassword.Text.Replace("drop", "");

                if (Page.IsValid)
                {

                    string Upwd = null;
                    parameters.Clear();
                    parameters.Add("Userid", Session["UserId"]);
                    Upwd = hp.SendValue_SP("select_userpassword", parameters).ToString();
                    if (Upwd == (txtoldpassword.Text.Trim()))
                    {
                        hp.ExecuteQry("Update Login set Password='" + (txtnewpassword.Text.Trim()) + "' where Userid='" + Session["UserId"] + "'");
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Password Change Sucessfully!!.');",
                 true);
                        txtnewpassword.Text = "";
                        txtoldpassword.Text = "";
                        txtrenewpassword.Text = "";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Invalid Old Password.');",
                 true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }
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