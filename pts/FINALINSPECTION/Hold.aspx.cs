using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING.pts.FINALINSPECTION
{
    public partial class Hold : System.Web.UI.Page
    {
        string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        helperclass clsm = new helperclass();
        Hashtable parameters = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                DataSet ds = new DataSet();
                parameters.Clear();
                parameters.Add("@UserId", Session["UserId"].ToString());
                ds = clsm.senddataset_Parameter("select m.*,wcm.WorkCenter,wcm.WorkStationId from login m inner join WorkCenterRightMaster wcm on m.userid=wcm.Userid where m.UserId=@UserId and WorkCenter='FINAL INSPECTION' and m.Status=1 ", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Session["WorkStationId"] = ds.Tables[0].Rows[i]["WorkStationId"].ToString();
                    }
                }

                txtdate.Text = DateTime.Now.AddHours(-11.30).ToString("yyyy-MM-dd");
            }
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
            string APIPIPE;
             string VEHNO;
            if(string.IsNullOrEmpty(txtqrcode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        "alert",
        "alert('Please Enter Batch No.');",
        true);
                return;
            }

            var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATBT01");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");

            string batchno = "";
            string postdate = "";
            string shift = "";
         
            string userid = Session["SAPID"].ToString();
            string Stationtype = Session["StationType"].ToString();
            string WorkStationId = Session["WorkStationId"].ToString();
            //Batch

            batchno = txtqrcode.Text.ToUpper().Trim();
            postdate = txtdate.Text.Trim();
            postdate = string.Concat(postdate.Substring(0, 4), postdate.Substring(5, 2), postdate.Substring(8, 2));
            shift = ddlshift.SelectedValue;

            APIPIPE = "{\r\n\"IT_DATA\":\r\n";
            APIPIPE += "{\r\n\"BANME\": \"" + userid + "\",\r\n \"PSTDT\": \"" + postdate + "\",\r\n \"SHIFT\": \"" + shift + "\",\r\n \"CHARG\": \"" + batchno + "\",\r\n \"POST_PARK\": \"B\"\r\n  } \r\n ";
            APIPIPE += "\r\n }";

            //Batch

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

            string StatusFinal = status;
            if (StatusFinal == "S")
            {
                txtqrcode.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');", true);
                return;
            }
            else
            {
             
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');", true);
                return;
            }
        }
    }
}