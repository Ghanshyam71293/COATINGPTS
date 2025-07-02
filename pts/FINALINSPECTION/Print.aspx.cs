using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING.pts.FINALINSPECTION
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                string frombatch = txtfrombatchno.Text.Trim();
                string tobatch = txttobatchno.Text.Trim();
                if (string.IsNullOrEmpty(frombatch))
              {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter from batch no');", true);
                    return;
              }
               


                string APIPIPE = "";
                var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSQRCODE");
                client1.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request.AddHeader("Content-Type", "application/json");


                string display = "";
                string status = "";
                string Massage = "";
              
              
                APIPIPE += "{\r\n\"F_BATCHNO\": \"" + frombatch + "\",\r\n \"T_BATCHNO\": \"" + tobatch + "\",\r\n \"V_PRINT_TYPE\": \"F\"\r\n  } \r\n ";
                request.AddParameter("application/json", APIPIPE, ParameterType.RequestBody);
                IRestResponse response1 = client1.Execute(request);
                HttpStatusCode statusCode1 = response1.StatusCode;
                if (statusCode1 == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='FinalScan.aspx';", true);
                    return;
                }
                if (Convert.ToString(response1.StatusCode) == "OK")
                {
                    display = response1.Content.ToString();
                    dynamic d = JObject.Parse(display);

                    status = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                    Massage = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + response1.Content.ToString() + "');", true);
                    return;
                }
                string StatusFinal = status;
                if (StatusFinal == "S")
                {
                    
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + Massage + "');window.location ='FinalScan.aspx';", true);
                    return;
                }
                else
                {
                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + Massage + "');window.location ='FinalScan.aspx';", true);
                    return;
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}