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
using System.Xml;

namespace PTSCOATING.pts.FINALINSPECTION.ParkList
{
    public partial class GetBatch : System.Web.UI.Page
    {
        string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        helperclass clsm = new helperclass();
        Hashtable parameters = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../../../login.aspx");
            }
            if (!IsPostBack)
            {

               // txtdate.Text = DateTime.Now.AddHours(-11.30).ToString("yyyy-MM-dd");

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
               
            }
        }
       
       

        protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtdate = e.Row.FindControl("txtdate") as TextBox;
                txtdate.Text = DateTime.Now.AddHours(-11.30).ToString("yyyy-MM-dd");


                DropDownList ddlstatus = (e.Row.FindControl("ddlstatus") as DropDownList);
                parameters.Clear();
                clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1   order by id asc", parameters, ddlstatus);
                Literal litstatus = (e.Row.FindControl("litstatus") as Literal);
               // ddlstatus.SelectedValue = litstatus.Text;

                Literal litcoatingtype = (e.Row.FindControl("litcoatingtype") as Literal);
                DropDownList ddlcoatingtype = (e.Row.FindControl("ddlcoatingtype") as DropDownList);
                ddlcoatingtype.SelectedValue = litcoatingtype.Text;

                DropDownList ddlreason = (e.Row.FindControl("ddlreason") as DropDownList);
                parameters.Clear();
                clsm.Fillcombo_Parameter("select ReasonName,ReasonName as b from FinalStatusReason where status=1 and StatusCode='" + ddlstatus.SelectedValue + "'  order by StatusCode asc", parameters, ddlreason);
                Literal litreg = (e.Row.FindControl("litreg") as Literal);
                if (!string.IsNullOrEmpty(litreg.Text))
                {
                    ddlreason.SelectedValue = litreg.Text;
                }

                Literal litholdat = (e.Row.FindControl("litholdat") as Literal);

                if (litstatus.Text == "QC HOLD" && ddlcoatingtype.SelectedValue == "COAT PIPE" && (litholdat.Text == "COATING APPLICATION ID" || litholdat.Text == "COATING APPLICATION OD"))
                {

                    //show only-OK or Stripping or Reject
                    parameters.Clear();
                    clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1 and StatusName not in('REWORK','QC HOLD')  order by id asc", parameters, ddlstatus);
                }
                else
                {

                    //show only-Rework or Reject
                    parameters.Clear();
                    clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1 and StatusName not in('OK','QC HOLD','STRIPPING')  order by id asc", parameters, ddlstatus);

                }



                if (litstatus.Text == "QC HOLD" && ddlcoatingtype.SelectedValue == "BARE PIPE" && (litholdat.Text == "COATING APPLICATION ID" || litholdat.Text == "COATING APPLICATION OD"))
                {

                    //show only-Rework or Reject
                    parameters.Clear();
                    clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1 and StatusName not in('OK','QC HOLD','STRIPPING')  order by id asc", parameters, ddlstatus);
                }
                if (litstatus.Text == "OK")
                {
                    parameters.Clear();
                    clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1 and StatusName not in('REWORK')  order by id asc", parameters, ddlstatus);
                    ddlstatus.SelectedValue = litstatus.Text;
                }






                Literal litFsnno = (e.Row.FindControl("litFsnno") as Literal);
                litFsnno.Text= litFsnno.Text.TrimStart(new Char[] { '0' }).Trim();

                 
                
               
            }
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvDetails.Rows.Count; i++)
            {
                DropDownList ddlreasons = (DropDownList)gvDetails.Rows[i].FindControl("ddlreason");
                DropDownList ddlstatus = (DropDownList)gvDetails.Rows[i].FindControl("ddlstatus");
                parameters.Clear();
                clsm.Fillcombo_Parameter("select ReasonName,ReasonName as b from FinalStatusReason FinalstatusMaster where status=1 and StatusCode='" + ddlstatus.SelectedValue + "' order by StatusCode asc", parameters, ddlreasons);
            }
         }

        protected void btngetbatch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtdate.Text))
            {
                string saleorder = "";
                string LineItem = "";
                DataSet ds2 = new DataSet();
                parameters.Clear();
                parameters.Add("@UserId", Session["UserId"].ToString());
                ds2 = clsm.senddataset_Parameter("select * from FinalGetNextBatch where Uname=@UserId  ", parameters);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        saleorder = ds2.Tables[0].Rows[i]["Saleorder"].ToString();
                        LineItem = ds2.Tables[0].Rows[i]["LineItem"].ToString();
                    }
                }
                if (string.IsNullOrEmpty(saleorder) || string.IsNullOrEmpty(LineItem))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sale order/ LineItem not Found!');window.location ='../FinalScan.aspx';", true);
                    return;
                }


                string APIPIPE1 = "";
                var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATGET");
                client1.Timeout = -1;
                var request1 = new RestRequest(Method.POST);
                request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request1.AddHeader("Content-Type", "application/json");


                string Status = "";
                string Massage = "";
                string display = "";
                string getdate = "";
                getdate = txtdate.Text.Trim();
                //getdate = string.Concat(getdate.Substring(0, 4), getdate.Substring(5, 2), getdate.Substring(8, 2));
                //getdate = DateTime.Now.AddHours(-11.30).ToString("yyyyMMdd");
                // string date = "20230119";


                string userid1 = Session["SAPID"].ToString();
                string Stationtype = Session["StationType"].ToString();
                string WorkStationId = Session["WorkStationId"].ToString();
                APIPIPE1 += "{\r\n\"P_ARBPL\": \"" + WorkStationId + "\",\r\n \"P_PROCT\": \"" + Stationtype + "\",\r\n \"P_KDAUF\": \"" + saleorder + "\",\r\n \"P_KDPOS\": \"" + LineItem + "\",\r\n \"P_RECORD\": \"" + getdate + "\"\r\n  } \r\n ";
                request1.AddParameter("application/json", APIPIPE1, ParameterType.RequestBody);
                IRestResponse response1 = client1.Execute(request1);
                HttpStatusCode statusCode1 = response1.StatusCode;
                if (statusCode1 == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='../FinalScan.aspx';", true);
                    return;
                }
                if (Convert.ToString(response1.StatusCode) == "OK")
                {
                    display = response1.Content.ToString();
                    dynamic d = JObject.Parse(display);
                    JObject jObj = JObject.Parse(display);
                    string a = jObj.ToString();
                    DataSet ds = new DataSet();
                    ds = ReadDataFromJson(a);

                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables["IT_PEND"];
                    Session["BatchParameters"] = null;
                    Session["BatchParametersNew"] = dt1;
                    gvDetails.DataSource = dt1;
                    gvDetails.DataBind();
                    if (gvDetails.Rows.Count > 0)
                    {
                        divgetbatch.Visible = true;
                        divbatchlist.Visible = true;
                        gvDetails.Visible = true;
                        btnSubmit.Visible = true;
                    }

                    Status = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                    Massage = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Worng- " + response1.Content.ToString() + "');window.location ='../FinalScan.aspx';", true);
                    return;
                }
                if (Status != "S")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + Massage + "');", true);
                    return;
                }

            }
            else
            {
              
                divgetbatch.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter date!');", true);
                return;
            }
        }
        private static DataSet ReadDataFromJson(String jsonString)
        {
            var xd = new XmlDocument();
            jsonString = "{ \"rootNode\": {" + jsonString.Trim().TrimStart('{').TrimEnd('}') + @"} }";
            xd = JsonConvert.DeserializeXmlNode(jsonString);
            var result = new DataSet();
            result.ReadXml(new XmlNodeReader(xd));
            return result;
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
            if (Session["BatchParameters"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Please Check Parameters!!');",
               true);
               
                return;
            }

            if (gvDetails.Rows.Count > 0)
            {

                //try
                //{
                string Parkid = "0";
                for (int i = 0; i < gvDetails.Rows.Count; i++)
                {
                    Literal litqrcode = (Literal)gvDetails.Rows[i].FindControl("litqrcode");
                    TextBox txtdate = (TextBox)gvDetails.Rows[i].FindControl("txtdate");
                    DropDownList ddlshift = (DropDownList)gvDetails.Rows[i].FindControl("ddlshift");
                
                    DropDownList ddlstatus = (DropDownList)gvDetails.Rows[i].FindControl("ddlstatus");
                    DropDownList ddlreasons = (DropDownList)gvDetails.Rows[i].FindControl("ddlreason");
                    if (string.IsNullOrEmpty(txtdate.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
             "alert",
             "alert('Please enter date!!');",
             true);
                        return;
                    }
                    if (ddlstatus.SelectedValue == "QC HOLD")
                    {
                        if (ddlreasons.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Please Select Reason!!');",
               true);
                            return;
                        }

                    }

                }

                SendMainForm();

               
            }
        }
        protected void SendMainForm()
        {
            string APIPIPE="";
            string VEHNO = ""; ;
            var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATBT01");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");


            APIPIPE = "{\r\n\"IT_DATA\":[\r\n";
            

            string batchno = "";
            string postdate = "";
            string shift = "";
            string reason = "";
            string coattype = "";
            string userid = Session["SAPID"].ToString();
            string Stationtype = Session["StationType"].ToString();
            string WorkStationId = Session["WorkStationId"].ToString();
            //Batch
            DataTable dtBatchParameters = (DataTable)Session["BatchParameters"];
            int count = gvDetails.Rows.Count;
            int j = 1;
            for (int i = 0; i < gvDetails.Rows.Count; i++)
            {
                //Parameters
                string Parameter1 = "";
                string Parameter2 = "";
                string Parameter3 = "";
                string Parameter4 = "";
                string Parameter5 = "";
                string Parameter6 = "";
                string Parameter7 = "";
                string Parameter8 = "";
                if (dtBatchParameters.Rows.Count > 0)
                {
                    Parameter1 = dtBatchParameters.Rows[i]["Parameter1"].ToString();
                    Parameter2 = dtBatchParameters.Rows[i]["Parameter2"].ToString();
                    Parameter3 = dtBatchParameters.Rows[i]["Parameter3"].ToString();
                    Parameter4 = dtBatchParameters.Rows[i]["Parameter4"].ToString();
                    Parameter5 = dtBatchParameters.Rows[i]["Parameter5"].ToString();
                    Parameter6 = dtBatchParameters.Rows[i]["Parameter6"].ToString();
                    Parameter7 = dtBatchParameters.Rows[i]["Parameter7"].ToString();
                    Parameter8 = dtBatchParameters.Rows[i]["Parameter8"].ToString();
                }
                //Parameters


                Literal litqrcode = (Literal)gvDetails.Rows[i].FindControl("litqrcode");
                TextBox txtdate = (TextBox)gvDetails.Rows[i].FindControl("txtdate");
                DropDownList ddlshift = (DropDownList)gvDetails.Rows[i].FindControl("ddlshift");
                TextBox txtheatdno = (TextBox)gvDetails.Rows[i].FindControl("txtheatno");
                TextBox txtLengthft = (TextBox)gvDetails.Rows[i].FindControl("txtlength");
                TextBox txtWeight = (TextBox)gvDetails.Rows[i].FindControl("txtweight");
                DropDownList ddlstatus = (DropDownList)gvDetails.Rows[i].FindControl("ddlstatus");
                DropDownList ddlreasons = (DropDownList)gvDetails.Rows[i].FindControl("ddlreason");
                DropDownList ddlcoattype = (DropDownList)gvDetails.Rows[i].FindControl("ddlcoatingtype");
                batchno = litqrcode.Text.Trim();
                postdate = txtdate.Text.Trim();
                postdate = string.Concat(postdate.Substring(0, 4), postdate.Substring(5, 2), postdate.Substring(8, 2));
                shift = ddlshift.SelectedValue;
                if (ddlreasons.SelectedValue != "0")
                {
                    reason = ddlreasons.SelectedValue;
                }
                if (ddlcoattype.SelectedValue != "0")
                {
                    coattype = ddlcoattype.SelectedValue;
                }
               
                if (j != count)
                {

                    //APIPIPE += "{\r\n\"BANME\": \"" + userid + "\",\r\n \"PSTDT\": \"" + postdate + "\",\r\n \"SHIFT\": \"" + shift + "\",\r\n \"ARBPL\": \"" + WorkStationId + "\",\r\n \"CHARG\": \"" + batchno + "\",\r\n \"HEATN\": \"" + txtheatdno.Text.Trim() + "\",\r\n \"KALABM\": \"" + txtLengthft.Text.Trim() + "\",\r\n \"KALABT\": \"" + txtWeight.Text.Trim() + "\",\r\n \"PROCT\": \"" + Stationtype + "\",\r\n \"BSTAT\": \"" + ddlstatus.SelectedValue + "\",\r\n \"CTYPE\":\"" + coattype + "\",\r\n \"HLDREG\": \"" + reason + "\",\r\n \"PARAM01\": \"\",\r\n \"PARAM02\": \"\",\r\n \"PARAM03\": \"\",\r\n \"PARAM04\": \"\",\r\n \"PARAM05\": \"\",\r\n \"PARAM06\": \"\",\r\n \"PARAM07\": \"\",\r\n \"PARAM08\": \"\",\r\n \"POST_PARK \": \"X\",\r\n \"BLINE\": \"X\"\r\n  }, \r\n ";

                    APIPIPE += "{\r\n\"BANME\": \"" + userid + "\",\r\n \"PSTDT\": \"" + postdate + "\",\r\n \"SHIFT\": \"" + shift + "\",\r\n \"ARBPL\": \"" + WorkStationId + "\",\r\n \"CHARG\": \"" + batchno + "\",\r\n \"HEATN\": \"" + txtheatdno.Text.Trim() + "\",\r\n \"KALABM\": \"" + txtLengthft.Text.Trim() + "\",\r\n \"KALABT\": \"" + txtWeight.Text.Trim() + "\",\r\n \"PROCT\": \"" + Stationtype + "\",\r\n \"BSTAT\": \"" + ddlstatus.SelectedValue + "\",\r\n \"CTYPE\":\"" + coattype + "\",\r\n \"HLDREG\": \"" + reason + "\",\r\n ";

                    APIPIPE +="\"PARAM01\": \""+Parameter1+"\",\r\n \"PARAM02\": \""+ Parameter2 + "\",\r\n \"PARAM03\": \""+ Parameter3 + "\",\r\n \"PARAM04\": \""+ Parameter4 + "\",\r\n \"PARAM05\": \""+ Parameter5 + "\",\r\n \"PARAM06\": \""+ Parameter6 + "\",\r\n \"PARAM07\": \""+ Parameter7 + "\",\r\n \"PARAM08\": \""+ Parameter8 + "\",\r\n \"POST_PARK \": \"X\",\r\n \"BLINE\": \"X\"\r\n  }, \r\n ";
                }
                if (j == count)
                {

                    //APIPIPE += "{\r\n\"BANME\": \"" + userid + "\",\r\n \"PSTDT\": \"" + postdate + "\",\r\n \"SHIFT\": \"" + shift + "\",\r\n \"ARBPL\": \"" + WorkStationId + "\",\r\n \"CHARG\": \"" + batchno + "\",\r\n \"HEATN\": \"" + txtheatdno.Text.Trim() + "\",\r\n \"KALABM\": \"" + txtLengthft.Text.Trim() + "\",\r\n \"KALABT\": \"" + txtWeight.Text.Trim() + "\",\r\n \"PROCT\": \"" + Stationtype + "\",\r\n \"BSTAT\": \"" + ddlstatus.SelectedValue + "\",\r\n \"CTYPE\":\"" + coattype + "\",\r\n \"HLDREG\": \"" + reason + "\",\r\n \"PARAM01\": \"\",\r\n \"PARAM02\": \"\",\r\n \"PARAM03\": \"\",\r\n \"PARAM04\": \"\",\r\n \"PARAM05\": \"\",\r\n \"PARAM06\": \"\",\r\n \"PARAM07\": \"\",\r\n \"PARAM08\": \"\",\r\n \"POST_PARK \": \"X\",\r\n \"BLINE\": \"X\"\r\n  } \r\n ";

                    APIPIPE += "{\r\n\"BANME\": \"" + userid + "\",\r\n \"PSTDT\": \"" + postdate + "\",\r\n \"SHIFT\": \"" + shift + "\",\r\n \"ARBPL\": \"" + WorkStationId + "\",\r\n \"CHARG\": \"" + batchno + "\",\r\n \"HEATN\": \"" + txtheatdno.Text.Trim() + "\",\r\n \"KALABM\": \"" + txtLengthft.Text.Trim() + "\",\r\n \"KALABT\": \"" + txtWeight.Text.Trim() + "\",\r\n \"PROCT\": \"" + Stationtype + "\",\r\n \"BSTAT\": \"" + ddlstatus.SelectedValue + "\",\r\n \"CTYPE\":\"" + coattype + "\",\r\n \"HLDREG\": \"" + reason + "\",\r\n ";

                    APIPIPE += "\"PARAM01\": \"" + Parameter1 + "\",\r\n \"PARAM02\": \"" + Parameter2 + "\",\r\n \"PARAM03\": \"" + Parameter3 + "\",\r\n \"PARAM04\": \"" + Parameter4 + "\",\r\n \"PARAM05\": \"" + Parameter5 + "\",\r\n \"PARAM06\": \"" + Parameter6 + "\",\r\n \"PARAM07\": \"" + Parameter7 + "\",\r\n \"PARAM08\": \"" + Parameter8 + "\",\r\n \"POST_PARK \": \"X\",\r\n \"BLINE\": \"X\"\r\n  } \r\n ";
                }
                j = j + 1;


                
            }
            //Batch


            APIPIPE += "]\r\n }";
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
                Session["BatchParameters"] = null;
                Session["BatchParametersNew"]= null;
                gvDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='../FinalScan.aspx';", true);
                return;
            }
            else
            {
                Session["BatchParameters"] = null;
                Session["BatchParametersNew"] = null;
                gvDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='../FinalScan.aspx';", true);
                return;
            }
        }
    }
}