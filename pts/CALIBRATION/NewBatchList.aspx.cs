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

namespace PTSCOATING.pts.CALIBRATION
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
            txtHDTime.Text = ""; 
            txtHDCStd.Text = ""; 
            txtHDCSID.Text = "";
            txtHDID.Text = "";
            txtHDFH.Text = "";
            txtHDMS.Text = "";
            txtHDMI.Text = "";
            txtHDRemarks.Text = "";
            txtGTime.Text = "";
            txtGCS.Text = "";
            txtGCSID.Text = "";
            txtGCTID.Text = "";
            txtGFH.Text = "";
            txtGMS.Text = "";
            txtGMI.Text = "";
            txtGRemarks.Text = "";
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
            var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "CALIBT01");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");
            //APIPIPE = "{\r\n\"IT_DATA\":\r\n";
            //string HDTime = txtHDTime.Text.Trim() + ":00.000";
            //string GTime = txtGTime.Text.Trim() + ":00.000";


            string HDTime = "";
            string inputHDTime = txtHDTime.Text;
            string hdTime = FormatTime(inputHDTime);
            if (hdTime == "" || hdTime == null)
            {
                HDTime = inputHDTime.Trim() + ":00";
            }
            else
            {
                HDTime = hdTime.Trim() + ":00";
            }

            string GTime = "";
            string inputGTime = txtGTime.Text;
            string gtTime = FormatTime(inputGTime);
            if (gtTime == "" || gtTime == null)
            {
                GTime = inputGTime.Trim() + ":00";
            }
            else
            {
                GTime = gtTime.Trim() + ":00";
            }



            APIPIPE = "{\r\n\"SalesOrder\": \""+ txtSalesOrder.Text.Trim() +"\",\r\n \"Shift\": \"" + ddlshift.SelectedValue + "\",\r\n \"Cal_Hol_Det\": {\r\n \"Time\": \"" + HDTime+ "\",\r\n \"CalibertaionStandard\": \"" + txtHDCStd.Text.Trim() + "\",\r\n \"CalibrationStandardID\": \"" + txtHDCSID.Text.Trim() + "\",\r\n \"HolidayDetectorID\": \"" + txtHDID.Text.Trim() + "\",\r\n \"Frequency_HRS\": \"" + txtHDFH.Text.Trim() + "\",\r\n \"MeasofStand\": \"" + txtHDMS.Text.Trim() + "\",\r\n \"MeasofInst\": \"" + txtHDMI.Text.Trim() + "\",\r\n \"Remarks\": \"" + txtHDRemarks.Text.Trim() + "\"\r\n},\r\n \"Cal_Coat_Thick\": {\r\n \"Time\":\"" + GTime + "\",\r\n \"CalibrationStandard\": \"" + txtGCS.Text.Trim() + "\",\r\n \"CalStandardID_SN\": \"" + txtGCSID.Text.Trim() + "\",\r\n \"CoatingThicknessID\": \"" + txtGCTID.Text.Trim() + "\",\r\n \"Frequency_HRS\": \"" + txtGFH.Text.Trim() + "\",\r\n \"MeasonStand\": \"" + txtGMS.Text.Trim() + "\",\r\n \"MeasonInst\": \"" + txtGMI.Text.Trim() + "\",\r\n \"Remarks\": \"" + txtGRemarks.Text.Trim() +"\"\r\n}\r\n\r\n}";
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
                status = d["RespCode"];
                MESSAGE = d["RespMessage"];
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Connection Problem, Please Retry!!');", true);
                return;

            }
            string StatusFinal = status;
            if (StatusFinal == "200")
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