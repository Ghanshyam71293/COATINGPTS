using Newtonsoft.Json;
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

namespace PTSCOATING.pts.RINGCUT
{
    public partial class RingCut : System.Web.UI.Page
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
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Id", typeof(int)));
                dt.Columns.Add(new DataColumn("SRNO", typeof(string)));
                dt.Columns.Add(new DataColumn("PARA", typeof(string)));
                dt.Columns.Add(new DataColumn("VAULE", typeof(string)));
                dr = dt.NewRow();
                dt.Columns["Id"].AutoIncrement = true;
                dt.Columns["Id"].AutoIncrementSeed = 1;
                dt.Columns["Id"].AutoIncrementStep = 1;
                dr["SRNO"] = string.Empty;
                dr["PARA"] = string.Empty;
                dr["VAULE"] = string.Empty;
                Session["NewRingCut"] = dt;
            }
        }
        public string APIPIPE;

        protected void AddPrmt(object sender, EventArgs e)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "RINGCUT_BT01");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");

            string Sono = txtSOno.Text;
            string SoItem = txtSOitem.Text;
            string Batchno = txtBatchNo.Text;
            string SMP_Length = txtSmpLength.Text;
            string Orig_Length = txtOrigLength.Text;

            APIPIPE = "{" +
                "\r\n \"IT_ITEM\": {\r\n \"SRNO\": \"\", \r\n \"PARA\": \"\",\r\n \"VAULE\": \"\"}," +
                "\r\n\"P_KDAUF\": \"" + Sono + "\", \r\n \"P_KDPOS\": \"" + SoItem + "\",\r\n \"P_CHARG\": \"" + Batchno + "'\"," +
                "\r\n \"P_SAMP_LEN\": \"" + SMP_Length + "\",\r\n \"P_ORGI_LEN\": \"" + Orig_Length + "\",\r\n \"P_FLAG\": \"G\"" +
                "\r\n}";
            request.AddParameter("application/json", APIPIPE, ParameterType.RequestBody);
            string status = "";
            string MESSAGE = "";
            string display = "";
            // getData
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
                var d = JObject.Parse(display);

                string text = "";
                text += "<table class='table'>";
                text += "<tr>";
                text += "<th width='20%'>SRNO</th>";
                text += "<th>PARA</th>";
                text += "<th>VALUE</th>";
                text += "</tr>";
                for (int i = 0; i < d["MT_RING_Res"]["IT_ITEM_E"].Count(); i++)
                {
                    text += "<tr>";
                    text += "<td><input readonly id='inputSrno" + i + "' type='text' value='" + d["MT_RING_Res"]["IT_ITEM_E"][i]["SRNO"] + "'></td>";
                    text += "<td><input readonly id='inputPara" + i + "' type='text' value='" + d["MT_RING_Res"]["IT_ITEM_E"][i]["PARA"] + "'></td>";
                    text += "<td><input id='inputVal" + i + "' type='text' value'" + d["MT_RING_Res"]["IT_ITEM_E"][i]["VAULE"] + "'></td>";
                    text += "</tr>";
                }
                text += "</table>";
                showTable.InnerHtml = text;

                GetItemId.Visible = false;
                BtnSubmit1.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Parameter Not Available');", true);
                return;
            }
        }

        [WebMethod]
        public static string SendMainForm1(List<Customer> customers, string Sono, string SoItem, string Batchno, string SMP_Length, string Orig_Length)
        {
            string status = "";
            string MESSAGE = "";
            string display = "";
            try
            {
                DataTable dt = (DataTable)(HttpContext.Current.Session["NewRingCut"]);
                if (customers == null)
                {
                    customers = new List<Customer>();
                }
                foreach (Customer c in customers)
                {
                    DataRow dr = null;
                    dr = dt.NewRow();
                    dr["SRNO"] = c.SRNO;
                    dr["PARA"] = c.PARA.ToUpper().Trim();
                    dr["VAULE"] = c.VAULE.ToUpper().Trim();
                    dt.Rows.Add(dr);
                }
                var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "RINGCUT_BT01");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request.AddHeader("Content-Type", "application/json");

                string APIPIPE;

                String IT_ITEM_E_PARA = "";
                int j = 1;
                if (dt.Rows.Count > j)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i + 1 < dt.Rows.Count)
                        {
                            IT_ITEM_E_PARA += "{\r\n \"SRNO\": \"" + dt.Rows[i]["SRNO"] + "\", \r\n \"PARA\": \"" + dt.Rows[i]["PARA"] + "\",\r\n \"VAULE\": \"" + dt.Rows[i]["VAULE"] + "\"},";
                        }
                        else
                        {
                            IT_ITEM_E_PARA += "{\r\n \"SRNO\": \"" + dt.Rows[i]["SRNO"] + "\", \r\n \"PARA\": \"" + dt.Rows[i]["PARA"] + "\",\r\n \"VAULE\": \"" + dt.Rows[i]["VAULE"] + "\"}";
                        }
                    }
                }
                else
                {
                    IT_ITEM_E_PARA += "{\r\n \"SRNO\": \"" + dt.Rows[j]["SRNO"] + "\", \r\n \"PARA\": \"" + dt.Rows[j]["PARA"] + "\",\r\n \"VAULE\": \"" + dt.Rows[j]["VAULE"] + "\"}";
                }

                APIPIPE = "{\r\n \"IT_ITEM\": [" + IT_ITEM_E_PARA + "]," +
                "\r\n\"P_KDAUF\": \"" + Sono + "\", \r\n \"P_KDPOS\": \"" + SoItem + "\",\r\n \"P_CHARG\": \"" + Batchno + "\",\r\n \"P_SAMP_LEN\": \"" + SMP_Length + "\"," +
                "\r\n \"P_ORGI_LEN\": \"" + Orig_Length + "\",\r\n \"P_FLAG\": \"P\"\r\n}";

                request.AddParameter("application/json", APIPIPE, ParameterType.RequestBody);

                // posting
                IRestResponse response = client.Execute(request);
                HttpStatusCode statusCode = response.StatusCode;
                if (Convert.ToString(response.StatusCode) == "OK")
                {
                    display = response.Content.ToString();
                    dynamic d = JObject.Parse(display);
                    status = d["MT_RING_Res"]["IT_RETURN"]["TYPE"];
                    MESSAGE = d["MT_RING_Res"]["IT_RETURN"]["MESSAGE"];
                }
            }
            catch (Exception)
            {

                throw;
            }
            return JsonConvert.SerializeObject(MESSAGE);
        }
        public class Customer
        {
            public string SRNO { get; set; }
            public string PARA { get; set; }
            public string VAULE { get; set; }
        }
    }
}