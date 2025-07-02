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

namespace PTSCOATING.pts.MECHANICALLOGBOOK
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
                txtStartdate.Text = ""+ day + "/"+ month + "/"+ year + "".ToString();
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
            var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPMECHANICALLOGBOOK");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");

            string APIPIPE = "{\r\n\"IT_DATA\": ";
            int count = 1;

            string Startdate = "";
            string Enddate = "";
            string StartTime = "";
            string EndTime = "";
            string userid = Session["SAPID"].ToString();

            // Batch
            Startdate = txtStartdate.Text.Trim();
            Startdate = string.Concat(Startdate.Substring(0, 4), Startdate.Substring(5, 2), Startdate.Substring(8, 2));

            StartTime = txtStartTime.Text.Trim();
            StartTime = string.Concat(StartTime.Substring(0, 2), StartTime.Substring(3, 2), "01");

            Enddate = txtEndDate.Text.Trim();
            Enddate = string.Concat(Enddate.Substring(0, 4), Enddate.Substring(5, 2), Enddate.Substring(8, 2));

            EndTime = txtEndTime.Text.Trim();
            EndTime = string.Concat(EndTime.Substring(0, 2), EndTime.Substring(3, 2), "01");


            string description = txtRemark.Text.Trim().Replace("\"", "\\\"");

            if (count == 1)
            {
                APIPIPE += "{\r\n" +
                    "  \"BREAK_SHUT\": \"" + txtBREAKSHUT.SelectedValue.ToString() + "\",\r\n" +
                    "  \"WERKS\": \"" + txtPlant.SelectedValue.ToString() + "\",\r\n" +
                    "  \"BEGDA\": \"" + Startdate + "\",\r\n" +
                    "  \"FROM_TIME\": \"" + StartTime + "\",\r\n" +
                    "  \"ENDDA\": \"" + Enddate + "\",\r\n" +
                    "  \"TO_TIME\": \"" + EndTime + "\",\r\n" +
                    "  \"DEPTNM\": \"" + txtDepartment.SelectedValue.ToString() + "\",\r\n" +
                    "  \"LOCATION\": \"" + txtLocation.SelectedValue.ToString() + "\",\r\n" +
                    "  \"DESCRIPTION\": \"" + description + "\",\r\n" +
                    "  \"ENTBY\": \""+ txtEnterdBy.Text.Trim()+ "\"\r\n" +
                "}";
            }

            // Close JSON structure
            APIPIPE += "\r\n  }";

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
            if (Convert.ToString(response.StatusCode) == "OK")
            {
                display = response.Content.ToString();
                dynamic d = JObject.Parse(display);
                status = d["MT_RESPONSE_LOGBOOK"]["IT_RETURN"]["TYPE"];
                MESSAGE = d["MT_RESPONSE_LOGBOOK"]["IT_RETURN"]["MESSAGE"];              
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Connection Problem, Please Retry!!');", true);
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