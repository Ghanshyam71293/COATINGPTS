using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING
{
    public partial class Login : System.Web.UI.Page
    {

        string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        Hashtable Parameters = new Hashtable();
        helperclass clhp = new helperclass();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["User"] = null;
            Session["UserId"] = null;
            Session["SAPID"] = null;
            if (!IsPostBack)
            {

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Select Count(*) From Login Where UserId=@UserId And Password=@Password and status=1 ";
                int result = 0;
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", txtusername.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtpassword.Text.Trim());
                        conn.Open();
                        result = (int)cmd.ExecuteScalar();
                    }
                }
                if (result > 0)
                {
                    Session["User"] = txtusername.Text.Trim().ToLower();
                    Session["UserId"] = txtusername.Text.Trim().ToLower();
                    // Response.Redirect("homepage.aspx");
                    Response.Redirect("home.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Invalid credentials')",
                  true);
                    return;
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnforget_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtuserid.Text))
            {
                DataSet ds = new DataSet();
                string sqrqry = null;
                Parameters.Clear();
                Parameters.Add("@userid", txtuserid.Text.Trim());
                sqrqry += @"select * from Login where  UserId=@UserId and status=1  ";
                ds = clhp.senddataset_Parameter(sqrqry, Parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string userpassword = (ds.Tables[0].Rows[0]["Password"].ToString());
                    string UserEmailId = ds.Tables[0].Rows[0]["Emailid"].ToString();
                    if (string.IsNullOrEmpty(UserEmailId))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your emailid is not registered.')", true);
                        return;
                    }

                    String userName = "sapadmin@jindalsaw.com";
                    String password = "jindalsaw@12";
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(UserEmailId.Trim()));
                    msg.From = new MailAddress(userName);
                    msg.Subject = "Your login Password for BarCode Scan Portal";
                    msg.Body = "Sir/Maám,<br><br>Your login Password for BarCode Scan Portal is following:<br><br><b>" + userpassword + "</b><br><br>Regards,<br>SAP Admin";
                    msg.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient();
                    client.Host = "172.16.1.18";
                    client.Credentials = new System.Net.NetworkCredential(userName, password);
                    client.Port = 25;
                    client.EnableSsl = false;
                    client.Send(msg);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your password is sent on your email id.');window.location ='Login.aspx';", true);

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Invalid User Id')",
                  true);
                    return;
                }



            }


        }
    }
}



    