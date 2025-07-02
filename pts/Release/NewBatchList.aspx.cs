using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING.pts.Release
{
    public partial class NewBatchList : System.Web.UI.Page
    {
        string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        helperclass clsm = new helperclass();
        Hashtable parameters = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["NewRELEASEBatch"] == null)
                {
                    Response.Redirect("ReleaseScan.aspx");
                }
               
                binddata();
            }
        }
        public void binddata()
        {
            DataTable dt = (DataTable)(Session["NewRELEASEBatch"]);
            if (dt.Rows.Count > 1)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow recRow = dt.Rows[0];
                    recRow.Delete();
                }
            }
            repDetails.DataSource = dt;
            repDetails.DataBind();
            if (repDetails.Items.Count == 0)
            {
                btnSubmit.Visible = false;
                divmanualscan.Visible = true;
            }
            else
            {
                btnSubmit.Visible = true; ;
                divmanualscan.Visible = false;
                Button1.Visible = false;
                if (repDetails.Items.Count == 1)
                {
                    divmanualscan.Visible = false;
                    Button1.Visible = false;
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["SAPID"] == null || Session["UserId"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Your Seesion has been Expired!!');",
                true);
                Response.Redirect("../../login.aspx");
                return;

            }

            if (repDetails.Items.Count > 0)
            {
              
                try
                {

                    for (int i = 0; i < repDetails.Items.Count; i++)
                    {
                        Literal litqrcode = (Literal)repDetails.Items[i].FindControl("litqrcode");
                        TextBox txtdate = (TextBox)repDetails.Items[i].FindControl("txtdate");
                        
                        if (string.IsNullOrEmpty(txtdate.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Please enter date!!');",
                 true);
                            return;
                        }
                    }
                    SendMainForm();

                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
                finally
                {

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtqrcode.Text) && string.IsNullOrEmpty(txtsqno.Text) && string.IsNullOrEmpty(txtsaleorder.Text) && string.IsNullOrEmpty(txtlineitem.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
       "alert",
       "alert('Please Enter Sale Order,Line Item,Squance No or Batch No.');",
       true);
                return;
            }
            if (!string.IsNullOrEmpty(txtqrcode.Text) && !string.IsNullOrEmpty(txtsqno.Text) && !string.IsNullOrEmpty(txtsaleorder.Text) && !string.IsNullOrEmpty(txtlineitem.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
       "alert",
       "alert('Please Enter Sale Order,Line Item,Squance No or Batch No.');",
       true);
                return;
            }
            if (!string.IsNullOrEmpty(txtsqno.Text) && string.IsNullOrEmpty(txtsaleorder.Text) && string.IsNullOrEmpty(txtlineitem.Text) && string.IsNullOrEmpty(txtqrcode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
       "alert",
       "alert('Please Enter Sale Order,Line Item or Batch No.');",
       true);
                return;
            }
            if (!string.IsNullOrEmpty(txtsqno.Text) && !string.IsNullOrEmpty(txtsaleorder.Text) && string.IsNullOrEmpty(txtlineitem.Text) && string.IsNullOrEmpty(txtqrcode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
       "alert",
       "alert('Please Enter Line Item or Batch No.');",
       true);
                return;
            }
            if (!string.IsNullOrEmpty(txtsqno.Text) && !string.IsNullOrEmpty(txtsaleorder.Text) && !string.IsNullOrEmpty(txtlineitem.Text) && string.IsNullOrEmpty(txtqrcode.Text))
            {
                DataTable dt = (DataTable)(Session["NewRELEASEBatch"]);
                DataRow dr = null;
                dr = dt.NewRow();
                dr["Codetype"] = "";
                dr["Code"] = "";
                dr["SQNO"] =txtsqno.Text.ToUpper().Trim();
                dr["SaleOrder"] = txtsaleorder.Text.ToUpper().Trim();
                dr["LineItem"] = txtlineitem.Text.ToUpper().Trim();
                dt.Rows.Add(dr);
                binddata();
                txtqrcode.Text = "";
                txtsqno.Text = "";
                txtsaleorder.Text = "";
                txtlineitem.Text = "";
            }
            if (!string.IsNullOrEmpty(txtqrcode.Text) && string.IsNullOrEmpty(txtsqno.Text) && string.IsNullOrEmpty(txtsaleorder.Text) && string.IsNullOrEmpty(txtlineitem.Text))
            {
                DataTable dt = (DataTable)(Session["NewRELEASEBatch"]);
                DataRow dr = null;
                dr = dt.NewRow();
                dr["Codetype"] = "";
                dr["Code"] = txtqrcode.Text.ToUpper().Trim();
                dt.Rows.Add(dr);
                binddata();
                txtqrcode.Text = "";
                txtsqno.Text = "";
                txtsaleorder.Text = "";
                txtlineitem.Text = "";
            }
            
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(),
        //"alert",
        //"alert('Please Enter Batch No.');",
        //true);
        //        return;
        //    }
        }
        public string APIPIPE;
        public string VEHNO;
        protected void SendMainForm()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATREL");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");



            //APIPIPE = "{\r\n\"IT_DATA\":\r\n";
            int count = 1;

            string batchno = "";
            string postdate = "";
            string shift = "";
            string bstatus = "";
            string reason = "";
            string userid = Session["SAPID"].ToString();

            //Batch
            for (int i = 0; i < repDetails.Items.Count; i++)
            {
                Literal litqrcode = (Literal)repDetails.Items[i].FindControl("litqrcode");
                TextBox txtdate = (TextBox)repDetails.Items[i].FindControl("txtdate");
                TextBox txtlength = (TextBox)repDetails.Items[i].FindControl("txtlength");
                TextBox txttotallength = (TextBox)repDetails.Items[i].FindControl("txttotallength");
                TextBox txtheatno = (TextBox)repDetails.Items[i].FindControl("txtheatno");
                TextBox txtcustpipeno = (TextBox)repDetails.Items[i].FindControl("txtcustpipeno");
                TextBox txtcoilno = (TextBox)repDetails.Items[i].FindControl("txtcoilno");
                TextBox txtholdreason = (TextBox)repDetails.Items[i].FindControl("txtholdreason");

               

                batchno = litqrcode.Text.ToUpper().Trim();
                postdate = txtdate.Text.Trim();
                postdate = string.Concat(postdate.Substring(0, 4), postdate.Substring(5, 2), postdate.Substring(8, 2));
              
               

                if (count == 1)
                {

                    APIPIPE += "{\r\n \"P_ERNAM \": \"" + userid + "\",\r\n\"P_CHARG\": \"" + batchno + "\",\r\n \"P_PSTDT\": \"" + postdate + "\",\r\n \"P_LENGTH\": \"" + txttotallength.Text.Trim() + "\",\r\n \"P_HEATN\": \"" + txtheatno.Text.Trim() + "\",\r\n \"P_CUSTP\": \"" + txtcustpipeno.Text.Trim() + "\",\r\n \"P_COILN\": \"" + txtcoilno.Text.Trim() + "\",\r\n \"P_FLAG\": \"\"\r\n  } \r\n ";
                }
            }
            //Batch


            //APIPIPE += "\r\n }";
            request.AddParameter("application/json", APIPIPE, ParameterType.RequestBody);
            string status = "";
            string MESSAGE = "";
            string display = "";
            // posting
            IRestResponse response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond');", true);
                return;
            }
            if (Convert.ToString(response.StatusCode) == "OK")
            {
                display = response.Content.ToString();
                dynamic d = JObject.Parse(display);
                status = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                MESSAGE = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Connection Problem, Please Retry!!');", true);
                return;
            }

            string StatusFinal = status;
            if (StatusFinal == "S")
            {
                Session["NewRELEASEBatch"] = null;
                repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='ReleaseScan.aspx';", true);
                return;
            }
            else
            {
                //Session["NewRELEASEBatch"] = null;
                //repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');", true);
                return;

            }
        }
       
        protected void repDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtdate = e.Item.FindControl("txtdate") as TextBox;
                txtdate.Text = DateTime.Now.AddHours(-11.30).ToString("yyyy-MM-dd");
                //Api Get

                string APIPIPE1 = "";
                var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATREL");
                client1.Timeout = -1;
                var request1 = new RestRequest(Method.POST);
                request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request1.AddHeader("Content-Type", "application/json");

                string F_LENGTH = "";
                string T_LENGTH = "";
                string V_HEATN = "";
                string V_CUSTP = "";
                string V_COILN = "";
                string V_HLDREG = "";
                string V_CHARG = "";
                string Status1 = "";
                string MESSAGE1 = "";
                string display1 = "";
                string userid1 = Session["SAPID"].ToString();
                
                Literal litqrcode1 = e.Item.FindControl("litqrcode") as Literal;

                Literal litsqno = e.Item.FindControl("litsqno") as Literal;
                Literal litsaleorder = e.Item.FindControl("litsaleorder") as Literal;
                Literal litlineitem = e.Item.FindControl("litlineitem") as Literal;


                APIPIPE1 += "{\r\n\"P_CHARG\": \"" + litqrcode1.Text.ToUpper().Trim() + "\",\r\n\"P_SQNUM\": \"" + litsqno.Text.ToUpper().Trim() + "\",\r\n\"P_POSNR\": \"" + litlineitem.Text.ToUpper().Trim() + "\",\r\n\"P_VBELN\": \"" + litsaleorder.Text.ToUpper().Trim() + "\",\r\n \"P_FLAG\": \"X\"\r\n  } \r\n ";

                request1.AddParameter("application/json", APIPIPE1, ParameterType.RequestBody);
                IRestResponse response1 = client1.Execute(request1);
                HttpStatusCode statusCode1 = response1.StatusCode;
                if (statusCode1 == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='ReleaseScan.aspx';", true);
                    return;
                }
                if (Convert.ToString(response1.StatusCode) == "OK")
                {
                    display1 = response1.Content.ToString();
                    dynamic d = JObject.Parse(display1);
                    F_LENGTH = d["MT_RESPONSE"]["F_LENGTH"];
                    T_LENGTH = d["MT_RESPONSE"]["T_LENGTH"];
                    V_HEATN = d["MT_RESPONSE"]["V_HEATN"];
                    V_CUSTP = d["MT_RESPONSE"]["V_CUSTP"];
                    V_COILN = d["MT_RESPONSE"]["V_COILN"];
                    V_HLDREG = d["MT_RESPONSE"]["V_HLDREG"];
                    V_CHARG  = d["MT_RESPONSE"]["V_CHARG"];
                    if (display1.Contains("IT_RETURN"))
                    {
                        Status1 = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                        MESSAGE1 = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + response1.Content.ToString() + "');window.location ='ReleaseScan.aspx';", true);
                    return;
                }
                if (display1.Contains("IT_RETURN"))
                {
                    if (Status1 != "S")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + MESSAGE1 + "');window.location ='ReleaseScan.aspx';", true);
                        return;
                    }
                }


                TextBox txtlength = e.Item.FindControl("txtlength") as TextBox;
                TextBox txttotallength = e.Item.FindControl("txttotallength") as TextBox;
                TextBox txtheatno = e.Item.FindControl("txtheatno") as TextBox;
                TextBox txtcustpipeno = e.Item.FindControl("txtcustpipeno") as TextBox;
                TextBox txtcoilno = e.Item.FindControl("txtcoilno") as TextBox;
                TextBox txtholdreason = e.Item.FindControl("txtholdreason") as TextBox;
                

                txtlength.Text = F_LENGTH.Trim();
                txttotallength.Text = T_LENGTH.Trim();
                txtheatno.Text = V_HEATN.Trim();
                txtcustpipeno.Text = V_CUSTP.Trim();
                txtcoilno.Text = V_COILN.Trim();
                txtholdreason.Text = V_HLDREG.Trim();
                if(string.IsNullOrEmpty(litqrcode1.Text.Trim()))
                {
                    litqrcode1.Text = V_CHARG;
                }
               
                //Api Get 

            }
        }


    }
}