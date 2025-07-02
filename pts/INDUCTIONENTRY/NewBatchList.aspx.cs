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

namespace PTSCOATING.pts.INDUCTIONENTRY
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
                var DTFormate = System.DateTime.Now.ToString("yyyy-MM-dd");
                var newDt = DTFormate.Split('-');
                string year = newDt[0];
                string month = newDt[1];
                string day = newDt[2];
                txtdate.Text = ""+ day + "/"+ month + "/"+ year + "".ToString();
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["SAPID"] == null || Session["UserId"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Seesion has been Expired!!');", true);
                Response.Redirect("../../login.aspx");
                return;
            }
            try
            {
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

        public string APIPIPE;
        public string VEHNO;
        protected void SendMainForm()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPINDUCTIONCOAT");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");

            //string APIPIPE = "{\r\n\"MT_REQUEST_INDCOAT\": {\r\n\"IT_DATA\": ";
            string APIPIPE = "{\r\n\"IT_DATA\": ";
            int count = 1;

            string postdate = "";
            string userid = Session["SAPID"].ToString();

            // Batch
            postdate = txtdate.Text.Trim();
            postdate = string.Concat(postdate.Substring(6, 4), postdate.Substring(3, 2), postdate.Substring(0, 2));
            //postdate = string.Concat(postdate.Substring(0, 4), postdate.Substring(5, 2), postdate.Substring(8, 2));

            if (count == 1)
            {
                APIPIPE += "{\r\n" +
                    "  \"BUDAT\": \"" + postdate + "\",\r\n" +
                    "  \"VBELN \": \"" + txtSaleOrder.Text.Trim() + "\",\r\n" +
                    "  \"PIPEOD\": \"" + txtOD.Text.Trim() + "\",\r\n" +
                    "  \"PIPEWT\": \"" + txtWT.Text.Trim() + "\",\r\n" +
                    "  \"LINE_SPEED\": \"" + txtLinespeed.Text.Trim() + "\",\r\n" +
                    "  \"TAP\": \"" + txtTap.Text.Trim() + "\",\r\n" +
                    "  \"FREQUENCY\": \"" + txtFrequency.Text.Trim() + "\",\r\n" +
                    "  \"COIL_VOLTAGE\": \"" + txtCoatingType.Text.Trim() + "\",\r\n" +
                    "  \"DC_BUS_VOLTAGE\": \"" + txtDcBusvoltage.Text.Trim() + "\",\r\n" +
                    "  \"COATING_TYPE\": \"" + txtCoatingType.SelectedValue.ToString() + "\",\r\n" +
                    "  \"MACHINE_STATUS\": \"" + txtMachineStatus.SelectedValue.ToString() + "\",\r\n" +
                    "  \"REMARKS\": \"" + txtRemark.Text.Trim() + "\",\r\n" +
                    "  \"ERNAM\": \"" + userid + "\"\r\n" +
                "}";
            }

            // Close JSON structure
            //APIPIPE += "\r\n  }\r\n}";
            APIPIPE += "\r\n}";

            // Add to request
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
            
            display = response.Content.ToString();
            if (Convert.ToString(response.StatusCode) == "OK")
            {                
                dynamic d = JObject.Parse(display);
                status = d["MT_RESPONSE_INDCOAT"]["IT_RETURN"]["TYPE"];
                MESSAGE = d["MT_RESPONSE_INDCOAT"]["IT_RETURN"]["MESSAGE"];
            }
            else
            {
                string ResMsg = HttpUtility.JavaScriptStringEncode(display);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{ResMsg} Connection Problem, Please Retry!!');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Connection Problem, Please Retry!!');", true);
                return;
            }
            string StatusFinal = status;
            if (StatusFinal == "S")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='../homepage.aspx';", true);
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