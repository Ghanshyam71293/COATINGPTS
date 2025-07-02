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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PTSCOATING.pts.SOLUBLESALTCONTAMINATION
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
                if (Session["UserId"] == null)
                {
                    Response.Redirect("../../login.aspx");
                    return;
                }
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
            else
            {
                SendMainForm();
            }
            
        }
        private void Clear()
        {
            txtSalesOrder.Text = "";
            txtDate.Text = "";
            textBatch.Text = "";
            txtFrequency.Text = "";
            txtTime.Text = "";
            txtHeatCode.Text = "";
            txtPotassium.Text = "";
            txtQuantitative.Text = "";
            txtRemarks.Text = "";
        }
        protected string FormatTime(string inputTime)
        {
            string outputTime = string.Empty;
            string timeFormat = inputTime.Substring(inputTime.Length - 2);
            switch (timeFormat)
            {
                case ("AM"):
                    outputTime = inputTime.Replace("AM", "");
                    break;
                case ("PM"):
                    DateTime d = DateTime.Parse(inputTime);
                    outputTime = d.ToString("HH:mm");
                    break;
            }
            return outputTime;
        }

        public string APIPIPE;
        public string VEHNO;
        protected void SendMainForm()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPSSCBT01");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");

            string HDTime = "";
            string inputHDTime = txtTime.Text.Replace(":", "");
            string hdTime = FormatTime(inputHDTime);
            if (hdTime == "" || hdTime == null)
            {
                HDTime = inputHDTime.Trim() + "00";
            }
            else
            {
                HDTime = hdTime.Trim() + "00";
            }

            string HDDate = txtDate.Text.Replace("-", "");

            APIPIPE = "{\r\n\"IT_DATA\":\r\n[\r\n{" +
                "\r\n\"VBELN\": \"" + txtSalesOrder.Text.Trim() + "\"," +
                "\r\n\"CHARG\": \"" + textBatch.Text + "\"," +
                "\r\n\"SHIFT\": \"" + ddlshift.SelectedValue + "\"," +                
                "\r\n\"ZDATE\": \"" + HDDate + "\"," +
                "\r\n\"TIME\": \"" + HDTime + "\"," +
                "\r\n\"FREQUENCY\": \"" + txtFrequency.Text.Trim() + "\"," +
                "\r\n\"HEAT_CODE\": \"" + txtHeatCode.Text.Trim() + "\"," +
                "\r\n\"PAPPER_TEST\": \"" + txtPotassium.Text.Trim() + "\"," +
                "\r\n\"QUANT_TEST\": \"" + txtQuantitative.Text.Trim() + "\"," +
                "\r\n\"REMARKS\": \"" + txtRemarks.Text.Trim() + "\"" +
                "}\r\n]\r\n}";

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
                MESSAGE = d["MT_RESPONSE"]["IT_RETURN"]["STATUS"];
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Connection Problem, Please Retry!!');", true);
                return;

            }
            string StatusFinal = status;
            if (StatusFinal == "S")
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='\\homepage.aspx';", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');", true);
                Clear();
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