using Newtonsoft.Json;
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

namespace PTSCOATING.pts.Release
{
    public partial class MultipleRelease : System.Web.UI.Page
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
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtsaleorder.Text) && string.IsNullOrEmpty(txtlineitem.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Sale Order,Line Item,Squance No or Batch No.');", true);
                return;
            }
            string display = "";
            string Message = "";
            string Type = "";
            try
            {
                string APIPIPE = "";

                var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATREL");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request.AddHeader("Content-Type", "application/json");

                APIPIPE = "{\r\n\"P_FLAG\": \"G\",\r\n\"P_VBELN\": \"" + txtsaleorder.Text + "\",\r\n\"P_POSNR\": \"" + txtlineitem.Text + "\"\r\n }";
                request.AddParameter("application/json", APIPIPE, ParameterType.RequestBody);

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
                    JObject jObj = JObject.Parse(display);

                    Type = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                    Message = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];

                    if (d["MT_RESPONSE"]["IT_PEND"] != null)
                    {
                        if (Type == "E")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + Message + "');", true);
                            return;
                        }
                        else
                        {
                            var itPendToken = jObj["MT_RESPONSE"]?["IT_PEND"];

                            if (itPendToken == null)
                            {
                                throw new Exception("IT_PEND is null or not found");
                            }

                            if (itPendToken.Type == JTokenType.Object)
                            {
                                // Single object case
                                JObject itPendObj = (JObject)itPendToken;

                                // Convert single object to array
                                JArray jArray = new JArray();
                                jArray.Add(itPendObj);

                                DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(jArray.ToString());
                                TableDetails.DataSource = dt1;
                                TableDetails.DataBind();
                            }
                            else if (itPendToken.Type == JTokenType.Array)
                            {
                                // Already an array
                                JArray jArray = (JArray)itPendToken;

                                DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(jArray.ToString());
                                TableDetails.DataSource = dt1;
                                TableDetails.DataBind();
                            }
                            else
                            {
                                throw new Exception("IT_PEND is not an object or array. Actual type: " + itPendToken.Type);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + Message + "');", true);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + Message + "');", true);
                throw ex;
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


            int rowCount = TableDetails.Rows.Count;
            if (rowCount > 0)
            {
                SendMainForm();
            }
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

            string userid = Session["SAPID"].ToString();

            string postdate = DateTime.Now.ToString("yyyy-MM-dd");
            postdate = string.Concat(postdate.Substring(0, 4), postdate.Substring(5, 2), postdate.Substring(8, 2));

            string APIPIPE = "{\r\n" +
            "  \"P_FLAG\": \"P\",\r\n" +
            "  \"P_ERNAM\": \"ABAPIT001\",\r\n" +
            "  \"P_PSTDT\": \"" + postdate + "\",\r\n" +
            "  \"IT_POST\": [\r\n";

            List<string> postItems = new List<string>();

            foreach (GridViewRow row in TableDetails.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    TextBox CHARGtxt = (TextBox)row.FindControl("CHARG");
                    TextBox ACTLENtxt = (TextBox)row.FindControl("ACTLEN");
                    TextBox txtUPDLENtxt = (TextBox)row.FindControl("txtUPDLEN");
                    TextBox HEATNOtxt = (TextBox)row.FindControl("HEATNO");
                    TextBox CUSTPtxt = (TextBox)row.FindControl("CUSTP");
                    TextBox COILNOtxt = (TextBox)row.FindControl("COILNO");
                    TextBox HLDREGtxt = (TextBox)row.FindControl("HLDREG");
                    TextBox SQNUMtxt = (TextBox)row.FindControl("SQNUM");

                    string item = "{\r\n" +
                    "  \"SEL\": \"X\",\r\n" +
                    "  \"CHARG\": \"" + CHARGtxt.Text.ToString() + "\",\r\n" +
                    "  \"ACTLEN\": \"" + ACTLENtxt.Text + "\",\r\n" +
                    "  \"UPDLEN\": \"" + txtUPDLENtxt.Text + "\",\r\n" +
                    "  \"HEATNO\": \"" + HEATNOtxt.Text + "\",\r\n" +
                    "  \"CUSTP\": \"" + CUSTPtxt.Text + "\",\r\n" +
                    "  \"COILNO\": \"" + COILNOtxt.Text + "\",\r\n" +
                    "  \"HLDREG\": \"" + HLDREGtxt.Text + "\",\r\n" +
                    "  \"SQNUM\": \"" + SQNUMtxt.Text + "\"\r\n" +
                    "}";

                    postItems.Add(item);
                }
            }

            APIPIPE += string.Join(",\r\n", postItems);
            APIPIPE += "\r\n  ]\r\n}";

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
                //repDetails.DataSource = null;
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

        }


    }
}