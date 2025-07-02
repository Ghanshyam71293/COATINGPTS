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
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace PTSCOATING.pts.INBOUND
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
                if (Session["NewINBOUNDBatch"] == null)
                {
                    Response.Redirect("InboundScan.aspx");
                }
                DataSet ds = new DataSet();
                parameters.Clear();
                parameters.Add("@UserId", Session["UserId"].ToString());
                ds = clsm.senddataset_Parameter("select m.*,wcm.WorkCenter,wcm.WorkStationId from login m inner join WorkCenterRightMaster wcm on m.userid=wcm.Userid where m.UserId=@UserId and WorkCenter='INBOUND' and m.Status=1 ", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Session["WorkStationId"] = ds.Tables[0].Rows[i]["WorkStationId"].ToString();
                    }
                }
                bindManual();
                binddata();

            }
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<string> GetAutoCompleteCustPipedetails(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<string>();
            }
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session["DtCustPipeDetails"];
            if (dt == null || !dt.Columns.Contains("CUSTP"))
            {
                return new List<string>();
            }
            var suggestions = dt.AsEnumerable()
                .Where(row => !row.IsNull("CUSTP") &&
                              row.Field<string>("CUSTP").IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(row => row.Field<string>("CUSTP")).Distinct().Take(5)
                .ToList();
            return suggestions;
        }

        public void binddata()
        {
            DataTable dt = (DataTable)(Session["NewINBOUNDBatch"]);
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
        public void bindManual()
        {
            string saleorder = "";
            string LineItem = "";
            DataSet ds1 = new DataSet();
            parameters.Clear();
            parameters.Add("@UserId", Session["UserId"].ToString());
            ds1 = clsm.senddataset_Parameter("select * from InboundGetNextBatch where Uname=@UserId  ", parameters);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    saleorder = ds1.Tables[0].Rows[i]["Saleorder"].ToString();
                    LineItem = ds1.Tables[0].Rows[i]["LineItem"].ToString();
                    txtsaleorder.Text = saleorder;
                    txtlineitem.Text = LineItem;
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
                getdate = "20230119";

                string userid1 = Session["SAPID"].ToString();
                string Stationtype = Session["StationType"].ToString();
                string WorkStationId = Session["WorkStationId"].ToString();
                APIPIPE1 += "{\r\n\"P_PROCT\": \"" + "IB" + "\",\r\n \"P_KDAUF\": \"" + saleorder + "\",\r\n \"P_KDPOS\": \"" + LineItem + "\"\r\n  } \r\n ";
                request1.AddParameter("application/json", APIPIPE1, ParameterType.RequestBody);
                IRestResponse response1 = client1.Execute(request1);
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
                    JObject jObj = JObject.Parse(display);
                    string a = jObj.ToString();
                    DataSet ds = new DataSet();
                    ds = ReadDataFromJson(a);

                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables["IT_PEND"];

                    ddlmanualbatch.DataSource = dt1;
                    ddlmanualbatch.DataTextField = "CHARG";
                    ddlmanualbatch.DataValueField = "CHARG";
                    ddlmanualbatch.DataBind();
                    ddlmanualbatch.Items.Insert(0, "- -Select- -");
                    ddlmanualbatch.Items[0].Value = "0";
                    ddlmanualbatch.SelectedIndex = ddlmanualbatch.Items.IndexOf(ddlmanualbatch.Items.FindByText("- -Select- -"));
                    //Session["BatchParameters"] = null;
                    //Session["BatchParametersNew"] = dt1;

                    Status = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                    Massage = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Worng- " + response1.Content.ToString() + "');window.location ='FinalScan.aspx';", true);
                    return;
                }
                if (Status != "S")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + Massage + "');", true);
                    return;
                }

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
                SqlTransaction objTrans = null;
                SqlConnection cn = new SqlConnection(connect);
                cn.Open();
                objTrans = cn.BeginTransaction();
                try
                {                    
                    for (int i = 0; i < repDetails.Items.Count; i++)
                    {
                        Literal litqrcode = (Literal)repDetails.Items[i].FindControl("litqrcode");
                        TextBox txtdate = (TextBox)repDetails.Items[i].FindControl("txtdate");
                        DropDownList ddlshift = (DropDownList)repDetails.Items[i].FindControl("ddlshift");
                        TextBox txtheatdno = (TextBox)repDetails.Items[i].FindControl("txtheatdno");
                        TextBox txtbm = (TextBox)repDetails.Items[i].FindControl("txtbm");
                        TextBox txtbt = (TextBox)repDetails.Items[i].FindControl("txtbt");
                        DropDownList ddlbstatus = (DropDownList)repDetails.Items[i].FindControl("ddlbstatus");
                        DropDownList ddlreasons = (DropDownList)repDetails.Items[i].FindControl("ddlreasons");
                        if (string.IsNullOrEmpty(txtdate.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Please enter date!!');",
                 true);
                            return;
                        }

                        if(ddlbstatus.SelectedValue=="QC HOLD")
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

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.Transaction = objTrans;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "InboundMastersp";
                        cmd.Parameters.AddWithValue("@BatchNo", litqrcode.Text);
                        cmd.Parameters.AddWithValue("@Empcode", Session["UserId"]);
                        cmd.Parameters.AddWithValue("@Heatno", txtheatdno.Text.Trim()) ;
                        cmd.Parameters.AddWithValue("@Length", txtbm.Text.Trim());
                        cmd.Parameters.AddWithValue("@Width", txtbt.Text.Trim());
                        cmd.Parameters.AddWithValue("@Bstatus", ddlbstatus.SelectedValue);
                        cmd.Parameters.Add("@MID", SqlDbType.Int, 0, "@MID").Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        var Parkid = cmd.Parameters["@MID"].Value.ToString();
                        objTrans.Commit();
                    } 
                    SendMainForm();
                }
                catch (Exception ex)
                {
                    objTrans.Rollback();
                    throw ex;
                }
                finally
                {

                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtsaleorder.Text))
            {
                DataTable dt = (DataTable)(Session["NewINBOUNDBatch"]);
                DataRow dr = null;
                dr = dt.NewRow();
                dr["Codetype"] = "";
                dr["Code"] = ddlmanualbatch.SelectedValue; //txtsaleorder.Text.ToUpper().Trim();
                dt.Rows.Add(dr);
                binddata();
                txtsaleorder.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        "alert",
        "alert('Please Enter Batch No.');",
        true);
                return;
            }
        }
        public string APIPIPE;
        public string VEHNO;
        protected void SendMainForm()
        {
          
            var client = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATBT01");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");

            APIPIPE = "{\r\n\"IT_DATA\":\r\n";
            int count = 1;

            string batchno = "";
            string postdate = "";
            string shift = "";
            string bstatus = "";
            string reason = "";
            string userid = Session["SAPID"].ToString();
            string Stationtype = Session["StationType"].ToString();
            string WorkStationId = Session["WorkStationId"].ToString();
            //Batch
            for (int i = 0; i < repDetails.Items.Count; i++)
            {
                Literal litqrcode = (Literal)repDetails.Items[i].FindControl("litqrcode");
                TextBox txtdate = (TextBox)repDetails.Items[i].FindControl("txtdate");
                DropDownList ddlshift = (DropDownList)repDetails.Items[i].FindControl("ddlshift");
                TextBox txtheatdno = (TextBox)repDetails.Items[i].FindControl("txtheatdno");
                 TextBox txtbm = (TextBox)repDetails.Items[i].FindControl("txtbm");
                TextBox txtbt = (TextBox)repDetails.Items[i].FindControl("txtbt");
                DropDownList ddlbstatus = (DropDownList)repDetails.Items[i].FindControl("ddlbstatus");
                DropDownList ddlreasons = (DropDownList)repDetails.Items[i].FindControl("ddlreasons");
                //DropDownList ddlcoatingpipe = (DropDownList)repDetails.Items[i].FindControl("ddlcoatingpipe");
                TextBox txtcustpipeno = (TextBox)repDetails.Items[i].FindControl("txtcustpipeno");
                TextBox txtcoilno = (TextBox)repDetails.Items[i].FindControl("txtcoilno");                
               

                batchno = litqrcode.Text.ToUpper().Trim();
                postdate = txtdate.Text.Trim();
                postdate = string.Concat(postdate.Substring(0, 4), postdate.Substring(5, 2), postdate.Substring(8, 2));
               //postdate = string.Concat(postdate.Substring(6, 4), postdate.Substring(0, 2), postdate.Substring(3, 2));
                shift = ddlshift.SelectedValue;
                bstatus = ddlbstatus.SelectedValue;
                if (ddlreasons.SelectedValue != "0")
                {
                    reason = ddlreasons.SelectedValue;
                }                
                if (count == 1)
                {
                    
                    APIPIPE += "{\r\n\"BANME\": \"" + userid + "\",\r\n \"PSTDT\": \"" + postdate + "\",\r\n \"SHIFT\": \"" + shift + "\",\r\n \"ARBPL\": \"" + WorkStationId + "\",\r\n \"CHARG\": \"" + batchno + "\",\r\n \"HEATN\": \"" + txtheatdno.Text.Trim() + "\",\r\n \"KALABM\": \"" +txtbm.Text.ToUpper().Trim() + "\",\r\n \"KALABT\": \"" + txtbt.Text.ToUpper().Trim() + "\",\r\n \"BSTAT\": \"" + ddlbstatus.SelectedValue + "\",\r\n \"PROCT\": \"" + Stationtype + "\",\r\n \"HLDREG\": \"" + reason + "\",\r\n \"CUSTP\": \"" + txtcustpipeno.Text.ToUpper().Trim() + "\",\r\n \"COILN\": \"" + txtcoilno.Text.ToUpper().Trim() + "\",\r\n \"BLINE\": \"X\"\r\n  } \r\n ";
                }
            }
            //Batch


            APIPIPE += "\r\n }";
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
                Session["NewINBOUNDBatch"] = null;
                repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='InboundScan.aspx';", true);
                return;
            }
            else
            {
                //Session["NewINBOUNDBatch"] = null;
                //repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');", true);
                return;
              
            }
        }
        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < repDetails.Items.Count; i++)
            {
                DropDownList ddlreasons = (DropDownList)repDetails.Items[i].FindControl("ddlreasons");
                DropDownList ddlstatus = (DropDownList)repDetails.Items[i].FindControl("ddlbstatus");
                parameters.Clear();
                clsm.Fillcombo_Parameter("select ReasonName,ReasonName as b from InboundStatusReason where status=1 and StatusCode='" + ddlstatus.SelectedValue + "' order by StatusCode asc", parameters, ddlreasons);
            }
        }
        //Added on 20-10-2023 by om
        private dynamic SetHeatNoEditable()
        {
            dynamic d = null;
            string display = "";
            try
            {
                string APIPIPE1 = "";
                var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPBATCHDETGET");
                client1.Timeout = -1;
                var request1 = new RestRequest(Method.POST);
                request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request1.AddHeader("Content-Type", "application/json");
                APIPIPE1 += "{\r\n\"P_ARBPL\": \"COAT\",\r\n \"P_BATCH\": \"" + txtsaleorder.Text.Trim() + "\"\r\n  } \r\n ";
                request1.AddParameter("application/json", APIPIPE1, ParameterType.RequestBody);
                IRestResponse response1 = client1.Execute(request1);
                HttpStatusCode statusCode1 = response1.StatusCode;
                if (statusCode1 == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='FinalScan.aspx';", true);

                }
                if (Convert.ToString(response1.StatusCode) == "OK")
                {
                    display = response1.Content.ToString();
                    d = JObject.Parse(display);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return d;
        }
        protected void ddlmtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < repDetails.Items.Count; i++)
            {
                DropDownList ddlmtr = (DropDownList)repDetails.Items[i].FindControl("ddlmtr");
                TextBox txtheatdno = (TextBox)repDetails.Items[i].FindControl("txtheatdno");
                TextBox txtbm = (TextBox)repDetails.Items[i].FindControl("txtbm");
                if (ddlmtr.SelectedValue != "NA")
                {
                    if (ddlmtr.SelectedItem.ToString().Contains('-'))
                    {
                        txtheatdno.Text = ddlmtr.SelectedItem.ToString().Split('-')[1];
                    }
                    else
                    {
                        txtheatdno.Text = ddlmtr.SelectedItem.ToString();
                    }
                   // txtbm.Text = ddlmtr.SelectedValue;
                   // txtheatdno.Enabled = false;
                }
                else
                {
                    txtheatdno.Text = "";
                }
            }
        }
        private void GetCustPipeDetails(DropDownList ddlmtr)
        {
            DataTable dt = new DataTable();
            try
            {
                string APIPIPENM = "", APIPIPEHN="";
                var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATGET");
                client1.Timeout = -1;
                var request1 = new RestRequest(Method.POST);
                request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request1.AddHeader("Content-Type", "application/json");
                APIPIPENM += "{\r\n\"P_PROCT\": \"" + "NM" + "\"\r\n,\r\n\"P_KDAUF\": \"" + txtsaleorder.Text + "\"\r\n,\r\n\"P_KDPOS\": \"" + txtlineitem.Text + "\"\r\n  } \r\n ";
                request1.AddParameter("application/json", APIPIPENM, ParameterType.RequestBody);
                IRestResponse response1 = client1.Execute(request1);
                HttpStatusCode statusCode1 = response1.StatusCode;
                if (statusCode1 == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='InboundScan.aspx';", true);
                    return;
                }
                Session["DtCustPipeDetails"] = null;
                if (Convert.ToString(response1.StatusCode) == "OK")
                {
                    var display = response1.Content.ToString();
                    dynamic d = JObject.Parse(display);
                    if (display.Contains("IT_PEND"))
                    {
                        JObject jObj = JObject.Parse(display);
                        string a = jObj.ToString();
                        DataSet ds = new DataSet();
                        ds = ReadDataFromJson(a);
                        dt = ds.Tables["IT_PEND"];
                        if (dt != null && dt.Rows.Count > 0)//Non-MTR
                        {
                            Session["DtCustPipeDetails"] = dt;
                        }
                    }
                }
                if (Session["DtCustPipeDetails"] == null)//MTR
                {                   
                    request1 = new RestRequest(Method.POST);
                    request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                    request1.AddHeader("Content-Type", "application/json");
                    APIPIPEHN += "{\r\n\"P_PROCT\": \"" + "HN" + "\"\r\n,\r\n\"P_KDAUF\": \"" + txtsaleorder.Text + "\"\r\n,\r\n\"P_KDPOS\": \"" + txtlineitem.Text + "\"\r\n  } \r\n ";
                    request1.AddParameter("application/json", APIPIPEHN, ParameterType.RequestBody);
                    IRestResponse response2 = client1.Execute(request1);
                    HttpStatusCode statusCode2 = response2.StatusCode;
                    if (statusCode2 == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='InboundScan.aspx';", true);
                        return;
                    }
                    if (Convert.ToString(response2.StatusCode) == "OK")
                    {
                        var display2 = response2.Content.ToString();
                        dynamic d2 = JObject.Parse(display2);
                        if (display2.Contains("IT_PEND"))
                        {
                            JObject jObj = JObject.Parse(display2);
                            string a = jObj.ToString();
                            DataSet ds = new DataSet();
                            ds = ReadDataFromJson(a);
                            DataTable dt1 = new DataTable();
                            dt1 = ds.Tables["IT_PEND"];
                            if (dt1 != null && dt1.Rows.Count > 0)
                            {
                                ddlmtr.DataSource = dt1;
                                ddlmtr.DataTextField = "HEATN";
                                ddlmtr.DataValueField = "HEATN"; //"KALABM";
                                ddlmtr.DataBind();
                                ddlmtr.Items.Insert(0, "- -Select- -");
                                ddlmtr.Items[0].Value = "NA";
                                ddlmtr.SelectedIndex = ddlmtr.Items.IndexOf(ddlmtr.Items.FindByText("- -Select- -"));
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Worng- " + response2.Content.ToString() + "');window.location ='InboundScan.aspx';", true);
                            return;
                        }
                    }
                }
                              
            }
            catch (Exception ex)
            {

                throw ex;
            }

           // return dt;
        }
        protected void repDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litqrcode = e.Item.FindControl("litqrcode") as Literal;
                Literal litcustpipeno = e.Item.FindControl("litcustpipeno") as Literal;
                TextBox txtcustpipeno = e.Item.FindControl("txtcustpipeno") as TextBox;
                TextBox txtcoilno = e.Item.FindControl("txtcoilno") as TextBox;

                DropDownList ddlstatus = (e.Item.FindControl("ddlbstatus") as DropDownList);
                parameters.Clear();
                clsm.Fillcombo_Parameter("select StatusName,StatusName as b from InboundstatusMaster where status=1 order by id asc", parameters, ddlstatus);

                ddlstatus.SelectedValue = "OK";

                TextBox txtdate = e.Item.FindControl("txtdate") as TextBox;
                txtdate.Text = DateTime.Now.AddHours(-11.30).ToString("yyyy-MM-dd");

                //Api Get Heat No
               string WorkStationId= Session["WorkStationId"].ToString();
                DropDownList ddlmtr = e.Item.FindControl("ddlmtr") as DropDownList;
                DropDownList ddlHeatNo = e.Item.FindControl("ddlHeatNo") as DropDownList;
                HtmlTableRow trmtr = e.Item.FindControl("trmtr") as HtmlTableRow;

                TextBox txtheatdno = e.Item.FindControl("txtheatdno") as TextBox;
                TextBox txtbm = e.Item.FindControl("txtbm") as TextBox;
                TextBox txtbt = e.Item.FindControl("txtbt") as TextBox;
                TextBox txtheatcode = e.Item.FindControl("txtheatcode") as TextBox;
                Literal litqrcode1 = e.Item.FindControl("litqrcode") as Literal;
                HiddenField hiddenbm = e.Item.FindControl("hiddenbm") as HiddenField;
                HiddenField hiddenbt = e.Item.FindControl("hiddenbt") as HiddenField;
                RequiredFieldValidator RFVddlHeatNo = e.Item.FindControl("RFVddlHeatNo") as RequiredFieldValidator;
                RequiredFieldValidator RFVtxtheatdno = e.Item.FindControl("RFVtxtheatdno") as RequiredFieldValidator;
                string APIPIPE1 = "";

                //Bind Dropdown
                //var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATGET");
                // client1.Timeout = -1;
                //var request1 = new RestRequest(Method.POST);
                //request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                //request1.AddHeader("Content-Type", "application/json");               
                //APIPIPE1 += "{\r\n\"P_PROCT\": \"" +"HN"+ "\"\r\n,\r\n\"P_KDAUF\": \"" + txtsaleorder.Text+ "\"\r\n,\r\n\"P_KDPOS\": \"" + txtlineitem.Text + "\"\r\n  } \r\n ";
                //request1.AddParameter("application/json", APIPIPE1, ParameterType.RequestBody);
                //IRestResponse response2 = client1.Execute(request1);
                //HttpStatusCode statusCode2 = response2.StatusCode;
                //if (statusCode2 == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='HeatScan.aspx';", true);
                //    return;
                //}
                //if (Convert.ToString(response2.StatusCode) == "OK")
                //{
                //   var display2 = response2.Content.ToString();
                //    dynamic d = JObject.Parse(display2);
                //    if (display2.Contains("IT_PEND"))
                //    {
                //        trmtr.Visible = true;
                //        JObject jObj = JObject.Parse(display2);
                //        string a = jObj.ToString();
                //        DataSet ds = new DataSet();
                //        ds = ReadDataFromJson(a);
                //        DataTable dt1 = new DataTable();
                //        dt1 = ds.Tables["IT_PEND"];
                //        if (dt1 != null && dt1.Rows.Count > 0)//MTR
                //        {
                //            ddlmtr.DataSource = dt1;
                //            ddlmtr.DataTextField = "HEATN";
                //            ddlmtr.DataValueField = "HEATN";
                //            ddlmtr.DataBind();
                //            ddlmtr.Items.Insert(0, "- -Select- -");
                //            ddlmtr.Items[0].Value = "0";
                //            ddlmtr.SelectedIndex = ddlmtr.Items.IndexOf(ddlmtr.Items.FindByText("- -Select- -"));
                //        }
                //        else//Non-MTR
                //        {
                //            DataTable dt = GetCustPipeDetails();
                //        }
                //    }
                //}
                //else
                //{
                //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Worng- " + response2.Content.ToString() + "');window.location ='InboundScan.aspx';", true);
                //    //return;
                //    ddlmtr.SelectedIndex = ddlmtr.Items.IndexOf(ddlmtr.Items.FindByText("- -Select- -"));

                //}
                ///
                //string APIPIPE1 = "";
                GetCustPipeDetails(ddlmtr);
                if (Session["DtCustPipeDetails"] != null && !string.IsNullOrEmpty(Session["DtCustPipeDetails"].ToString()))
                {
                    //GetCustPipeDetails(ddlHeatNo);
                    ddlHeatNo.Visible = true;
                    txtheatdno.Visible = false;
                    trmtr.Visible = false;
                    RFVddlHeatNo.Enabled = true;
                    RFVtxtheatdno.Enabled = false;
                }
                else
                {
                   // GetCustPipeDetails(ddlmtr);
                    ddlHeatNo.Visible = false;
                    txtheatdno.Visible = true;
                    trmtr.Visible = true;
                    RFVddlHeatNo.Enabled = false;
                    RFVtxtheatdno.Enabled = true;
                }

                var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPBATCHDETGET");
                client1.Timeout = -1;
                var request1 = new RestRequest(Method.POST);
                request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request1.AddHeader("Content-Type", "application/json");

                string Heatno1 = "";
                string V_LENGTH = "";
                string V_WEIGHT = "";
                string V_BSTAT = "";
                string V_CUSTP = "";
                string V_COILN = "";
                string V_HEATCODE = "";
                string Status1 = "";
                string Massage1 = "";
                string display1 = "";
                string userid1 = Session["SAPID"].ToString();
                string Stationtype = Session["StationType"].ToString();
                //Literal litqrcode1 = e.Item.FindControl("litqrcode") as Literal;
                APIPIPE1 += "{\r\n\"P_BATCH\": \"" + litqrcode1.Text.ToUpper().Trim() + "\",\r\n \"P_PROCT\": \"" + Stationtype + "\",\r\n \"P_ARBPL\": \"" + WorkStationId + "\"\r\n  } \r\n ";
                request1.AddParameter("application/json", APIPIPE1, ParameterType.RequestBody);
                IRestResponse response1 = client1.Execute(request1);
                HttpStatusCode statusCode1 = response1.StatusCode;
                if (statusCode1 == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='InboundScan.aspx';", true);
                    return;
                }
                if (Convert.ToString(response1.StatusCode) == "OK")
                {
                    display1 = response1.Content.ToString();
                    dynamic d = JObject.Parse(display1);
                    //Added on 20-10-2023 by om
                    dynamic d1 = SetHeatNoEditable();
                    if (d1["MT_RESPONSE"]["V_HEATNO"].ToString() == "X")
                    {
                        txtheatdno.ReadOnly = true;
                        txtcustpipeno.ReadOnly = true;
                    }
                    else
                    {
                        txtheatdno.ReadOnly = false;
                        txtcustpipeno.ReadOnly = false;
                    }
                    Heatno1 = d["MT_RESPONSE"]["V_HEATNO"];
                    V_LENGTH = d["MT_RESPONSE"]["V_LENGTH"];
                    hiddenbm.Value = V_LENGTH;
                    V_WEIGHT = d["MT_RESPONSE"]["V_WEIGHT"];
                    hiddenbt.Value = V_WEIGHT;
                    V_BSTAT = d["MT_RESPONSE"]["V_BSTAT"];
                    V_CUSTP = d["MT_RESPONSE"]["V_CUSTP"];
                    V_COILN = d["MT_RESPONSE"]["V_COILN"];
                    V_HEATCODE = d["MT_RESPONSE"]["V_HEATCODE"];


                    Status1 = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                    Massage1 = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];
                  
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Worng- " + response1.Content.ToString() + "');window.location ='InboundScan.aspx';", true);
                    return;
                }
                if (Status1 != "S")
                {
                    if (Status1 != "I")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + Massage1 + "');window.location ='InboundScan.aspx';", true);
                        return;
                    }
                  
                }
                
                txtheatdno.Text = Heatno1.Trim();
                txtbt.Text = V_WEIGHT.Trim();
                txtbm.Text = V_LENGTH.Trim();
                txtcustpipeno.Text = V_CUSTP.Trim();

                txtcoilno.Text = V_COILN.Trim();
                txtheatcode.Text = V_HEATCODE.Trim();
                if (!string.IsNullOrEmpty(V_BSTAT))
                {
                    ddlstatus.SelectedValue = V_BSTAT;
                }
                DropDownList ddlreasons = e.Item.FindControl("ddlreasons") as DropDownList;
                parameters.Clear();
                clsm.Fillcombo_Parameter("select ReasonName,ReasonName as b from InboundStatusReason where status=1 and StatusCode='" + ddlstatus.SelectedValue + "' order by StatusCode asc", parameters, ddlreasons);

                //Api Get Heat No

            }
        }      
        protected void txtcustpipeno_TextChanged(object sender, EventArgs e)
        {
            if (Session["DtCustPipeDetails"] is DataTable dt && dt.Rows.Count > 0)
            {
                TextBox txtcustpipeno = (TextBox)sender;
                RepeaterItem item = (RepeaterItem)txtcustpipeno.NamingContainer;
                TextBox txtbm = (TextBox)item.FindControl("txtbm");
                DropDownList ddlHeatNo = (DropDownList)item.FindControl("ddlHeatNo");

                string inputValue = txtcustpipeno.Text.Trim();
                ddlHeatNo.Items.Clear(); // Clear items before adding new ones
                txtbm.Text = "";

                if (!string.IsNullOrEmpty(inputValue))
                {
                    var results = dt.AsEnumerable()
                        .Where(row => !row.IsNull("CUSTP") &&
                                      row.Field<string>("CUSTP").Equals(inputValue, StringComparison.OrdinalIgnoreCase))
                        .Select(row => new
                        {
                            Value1 = row.Field<string>("HEATN").ToUpper(),
                            Value2 = row.Field<string>("KALABM").ToUpper(),
                            Value3 = row.Field<string>("HEATN") + '-' + row.Field<string>("KALABM").ToUpper()
                        }).Distinct().ToArray();

                    if (results.Length > 0)
                    {
                        ddlHeatNo.DataSource = results;
                        ddlHeatNo.DataTextField = "Value1";
                        ddlHeatNo.DataValueField = "Value3";
                        ddlHeatNo.DataBind();
                    }
                }

                ddlHeatNo.Items.Insert(0, new ListItem("- -Select- -", "NA"));
                ddlHeatNo.SelectedIndex = 0; // Ensure the first item is selected
            }
            else
            {
                // Clear dropdown if no data is available
                DropDownList ddlHeatNo = (DropDownList)((TextBox)sender).NamingContainer.FindControl("ddlHeatNo");
                ddlHeatNo.Items.Clear();
                ddlHeatNo.Items.Insert(0, new ListItem("- -Select- -", "NA"));
                ddlHeatNo.SelectedIndex = 0;
            }
        }

        protected void ddlHeatNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < repDetails.Items.Count; i++)
            {
                DropDownList ddlHeatNo = (DropDownList)repDetails.Items[i].FindControl("ddlHeatNo");
                TextBox txtheatdno = (TextBox)repDetails.Items[i].FindControl("txtheatdno");
                TextBox txtbm = (TextBox)repDetails.Items[i].FindControl("txtbm");
                if (ddlHeatNo.SelectedValue != "NA")
                {
                    string[] heatvalue = ddlHeatNo.SelectedValue.Split('-');
                    txtbm.Text = heatvalue[heatvalue.Length-1].ToString();
                    txtheatdno.Text = ddlHeatNo.SelectedItem.Text.ToString();
                    txtbm.Enabled = false;
                }
            }
        }
    }
}