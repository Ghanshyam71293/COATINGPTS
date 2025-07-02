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
using System.Xml;

namespace PTSCOATING.pts.FINALINSPECTION
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
                if (Session["NewFinalInsBatch"] == null)
                {
                    Response.Redirect("FinalScan.aspx");
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
                bindManual();
                binddata();
            }
        }
        public void bindManual()
        {

            string saleorder = "";
            string LineItem = "";
            DataSet ds1 = new DataSet();
            parameters.Clear();
            parameters.Add("@UserId", Session["UserId"].ToString());
            ds1 = clsm.senddataset_Parameter("select * from FinalGetNextBatch where Uname=@UserId  ", parameters);
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
                //getdate = string.Concat(getdate.Substring(0, 4), getdate.Substring(5, 2), getdate.Substring(8, 2));
                //getdate = DateTime.Now.AddHours(-11.30).ToString("yyyyMMdd");
                // string date = "20230119";


                string userid1 = Session["SAPID"].ToString();
                string Stationtype = Session["StationType"].ToString();
                string WorkStationId = Session["WorkStationId"].ToString();
                APIPIPE1 += "{\r\n\"P_ARBPL\": \"" + WorkStationId + "\",\r\n \"P_PROCT\": \"" + Stationtype + "\",\r\n \"P_KDAUF\": \"" + saleorder + "\",\r\n \"P_KDPOS\": \"" + LineItem + "\"\r\n  } \r\n ";
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
                    //string abc = dt1.Rows[0]["HLDREG"].ToString();

                    if (dt1 != null && dt1.Rows[0]["HLDREG"].ToString() != null)
                    {
                        Session["HLDREG_Val"] = dt1.Rows[0]["HLDREG"].ToString();
                        //Session["HLDREG_Val"] = d["MT_RESPONSE"]["IT_RETURN"]["IT_PEND"]["HLDREG"];
                    }

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

        public void binddata()
        {
            DataTable dt = (DataTable)(Session["NewFinalInsBatch"]);
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

                //try
                //{
                string Parkid = "0";
                for (int i = 0; i < repDetails.Items.Count; i++)
                {
                    Literal litqrcode = (Literal)repDetails.Items[i].FindControl("litqrcode");
                    TextBox txtdate = (TextBox)repDetails.Items[i].FindControl("txtdate");
                    DropDownList ddlshift = (DropDownList)repDetails.Items[i].FindControl("ddlshift");
                    TextBox txtheatdno = (TextBox)repDetails.Items[i].FindControl("txtheatdno");
                    DropDownList ddlstatus = (DropDownList)repDetails.Items[i].FindControl("ddlstatus");
                    //DropDownList ddlreasons = (DropDownList)repDetails.Items[i].FindControl("ddlreasons");
                    DropDownList ddlcoattype = (DropDownList)repDetails.Items[i].FindControl("ddlcoattype");
                    if (string.IsNullOrEmpty(txtdate.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
             "alert",
             "alert('Please enter date!!');",
             true);
                        return;
                    }
                    if (ddlstatus.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Please Select Status!!');",
                   true);
                        return;
                    }
                    if (ddlcoattype.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Please Select Coat Type!!');",
                   true);
                        return;
                    }
                    ListBox lstreasons = (ListBox)repDetails.Items[i].FindControl("lstreasons");
                    int rcount = 0;
                    if (ddlstatus.SelectedValue == "QC HOLD")
                    {
                        string message11 = "";
                        foreach (ListItem item in lstreasons.Items)
                        {
                            if (item.Selected)
                            {
                                message11 += item.Value + ",";
                                rcount += 1;
                            }
                        }
                        //message11 = message11.TrimEnd(new Char[] { ',' }).Trim();
                        //if (rcount > 0)
                        //{
                        //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message11 + "');", true);
                        //    return;
                        //}
                        if (rcount == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Please Select Reason from list!!');",
                   true);
                            return;
                        }
                        //     if (ddlreasons.SelectedValue == "0")
                        //         {
                        //             ScriptManager.RegisterStartupScript(this, this.GetType(),
                        //"alert",
                        //"alert('Please Select Reason!!');",
                        //true);
                        //             return;
                        //         }

                    }

                }



                SendMainForm();

                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                //finally
                //{

                //}
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //    if (!string.IsNullOrEmpty(txtqrcode.Text))
            //    {
            //        DataTable dt = (DataTable)(Session["NewFinalInsBatch"]);
            //        DataRow dr = null;
            //        dr = dt.NewRow();
            //        dr["Codetype"] = "";
            //        dr["Code"] = txtqrcode.Text.ToUpper().Trim();
            //        dt.Rows.Add(dr);
            //        binddata();
            //        txtqrcode.Text = "";
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(),
            //"alert",
            //"alert('Please Enter Batch No.');",
            //true);
            //        return;
            //    }

            if (ddlmanualbatch.SelectedValue != "0")
            {
                DataTable dt = (DataTable)(Session["NewFinalInsBatch"]);
                DataRow dr = null;
                dr = dt.NewRow();
                dr["Codetype"] = "";
                dr["Code"] = ddlmanualbatch.SelectedValue.Trim();
                dt.Rows.Add(dr);
                binddata();
                ddlmanualbatch.ClearSelection();

                //txtqrcode.Text = "";
                for (int i = 0; i < repDetails.Items.Count; i++)
                {
                    TextBox txtSHIFTIN = (TextBox)repDetails.Items[i].FindControl("txtSHIFTIN");
                    TextBox txtTPINM = (TextBox)repDetails.Items[i].FindControl("txtTPINM");
                    TextBox txtRACKNO = (TextBox)repDetails.Items[i].FindControl("txtRACKNO");
                    using (SqlConnection conn = new SqlConnection(connect))
                    {
                        string userid = Session["SAPID"].ToString();
                        string query = "SELECT * FROM SaveFinalInspectionDetails where UserId=@UserId";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@UserId", userid);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            txtSHIFTIN.Text = reader["ShiftSupervisor"].ToString();
                            txtTPINM.Text = reader["ShiftInspector"].ToString();
                            txtRACKNO.Text = reader["RackNo"].ToString();
                        }
                        reader.Close();
                        conn.Close();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
        "alert",
        "alert('Please Select Batch No.');",
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
            string coattype = "";
            string batchno = "";
            string postdate = "";
            string shift = "";
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
                TextBox txtLengthft = (TextBox)repDetails.Items[i].FindControl("txtLengthft");
                TextBox txtWeight = (TextBox)repDetails.Items[i].FindControl("txtWeight");
                TextBox txtcustpipeno = (TextBox)repDetails.Items[i].FindControl("txtcustpipeno");
                HiddenField hiddenFWeight = (HiddenField)repDetails.Items[i].FindControl("hiddenFWeight") as HiddenField;
                DropDownList ddlstatus = (DropDownList)repDetails.Items[i].FindControl("ddlstatus");
                TextBox txtRemarks = (TextBox)repDetails.Items[i].FindControl("txtRemarks");
                TextBox txtSHIFTIN = (TextBox)repDetails.Items[i].FindControl("txtSHIFTIN");
                TextBox txtTPINM = (TextBox)repDetails.Items[i].FindControl("txtTPINM");
                TextBox txtRACKNO = (TextBox)repDetails.Items[i].FindControl("txtRACKNO");

                //DropDownList ddlreasons = (DropDownList)repDetails.Items[i].FindControl("ddlreasons");
                ListBox lstreasons = (ListBox)repDetails.Items[i].FindControl("lstreasons");
                string message11 = "";
                int rcount = 0;
                foreach (ListItem item in lstreasons.Items)
                {
                    if (item.Selected)
                    {
                        message11 += item.Value + ",";
                        rcount += 1;
                    }
                }
                message11 = message11.TrimEnd(new Char[] { ',' }).Trim();


                Literal litcoattype = (Literal)repDetails.Items[i].FindControl("litcoattype");
                DropDownList ddlcoattype = (DropDownList)repDetails.Items[i].FindControl("ddlcoattype");


                TextBox txtParameter1 = (TextBox)repDetails.Items[i].FindControl("txtParameter1");
                TextBox txtParameter2 = (TextBox)repDetails.Items[i].FindControl("txtParameter2");
                TextBox txtParameter3 = (TextBox)repDetails.Items[i].FindControl("txtParameter3");
                TextBox txtParameter4 = (TextBox)repDetails.Items[i].FindControl("txtParameter4");
                TextBox txtParameter5 = (TextBox)repDetails.Items[i].FindControl("txtParameter5");
                TextBox txtParameter6 = (TextBox)repDetails.Items[i].FindControl("txtParameter6");
                TextBox txtParameter7 = (TextBox)repDetails.Items[i].FindControl("txtParameter7");
                TextBox txtParameter8 = (TextBox)repDetails.Items[i].FindControl("txtParameter8");
                TextBox txtParameter9 = (TextBox)repDetails.Items[i].FindControl("txtParameter9");
                TextBox txtParameter10 = (TextBox)repDetails.Items[i].FindControl("txtParameter10");
                TextBox txtParameter11 = (TextBox)repDetails.Items[i].FindControl("txtParameter11");
                TextBox txtParameter12 = (TextBox)repDetails.Items[i].FindControl("txtParameter12");



                batchno = litqrcode.Text.ToUpper().Trim();
                postdate = txtdate.Text.Trim();
                postdate = string.Concat(postdate.Substring(0, 4), postdate.Substring(5, 2), postdate.Substring(8, 2));
                //postdate = string.Concat(postdate.Substring(6, 4), postdate.Substring(0, 2), postdate.Substring(3, 2));
                shift = ddlshift.SelectedValue;
                reason = message11;
                //if (ddlreasons.SelectedValue != "0")
                //{
                //    reason = ddlreasons.SelectedValue;
                //}
                if (ddlcoattype.SelectedValue != "0")
                {
                    coattype = ddlcoattype.SelectedValue;
                }
                if (count == 1)
                {
                    txtWeight.Text = hiddenFWeight.Value.Trim();
                    APIPIPE += "{\r\n\"BANME\": \"" + userid + "\",\r\n \"PSTDT\": \"" + postdate + "\",\r\n \"SHIFT\": \"" + shift + "\",\r\n \"ARBPL\": \"" + WorkStationId + "\",\r\n \"CHARG\": \"" + batchno + "\",\r\n \"HEATN\": \"" + txtheatdno.Text.Trim() + "\",\r\n \"CUSTP\": \"" + txtcustpipeno.Text.Trim() + "\",\r\n \"KALABM\": \"" + txtLengthft.Text.Trim() + "\",\r\n \"KALABT\": \"" + txtWeight.Text.Trim() + "\",\r\n \"PROCT\": \"" + Stationtype + "\",\r\n \"BSTAT\": \"" + ddlstatus.SelectedValue + "\",\r\n \"SHIFTIN \": \"" + txtSHIFTIN.Text.Trim() + "\",\r\n \"TPINM\": \"" + txtTPINM.Text.Trim() + "\",\r\n \"RACKNO \": \"" + txtRACKNO.Text.Trim() + "\",\r\n \"REMARKS\": \"" + txtRemarks.Text.Trim() + "\",\r\n \"CTYPE\":\"" + coattype + "\",\r\n \"HLDREG\": \"" + reason + "\",\r\n \"PARAM01\": \"" + txtParameter1.Text.ToUpper().Trim() + "\",\r\n \"PARAM02\": \"" + txtParameter2.Text.ToUpper().Trim() + "\",\r\n \"PARAM03\": \"" + txtParameter3.Text.ToUpper().Trim() + "\",\r\n \"PARAM04\": \"" + txtParameter4.Text.ToUpper().Trim() + "\",\r\n \"PARAM05\": \"" + txtParameter5.Text.ToUpper().Trim() + "\",\r\n \"PARAM06\": \"" + txtParameter6.Text.ToUpper().Trim() + "\",\r\n \"PARAM07\": \"" + txtParameter7.Text.ToUpper().Trim() + "\",\r\n \"PARAM08\": \"" + txtParameter8.Text.ToUpper().Trim() + "\",\r\n \"PARAM09\": \"" + txtParameter9.Text.ToUpper().Trim() + "\",\r\n \"PARAM10\": \"" + txtParameter10.Text.ToUpper().Trim() + "\",\r\n \"PARAM11\": \"" + txtParameter11.Text.ToUpper().Trim() + "\",\r\n \"PARAM12\": \"" + txtParameter12.Text.ToUpper().Trim() + "\",\r\n \"BLINE\": \"X\"\r\n  } \r\n ";
                }

                using (SqlConnection conn = new SqlConnection(connect))
                {
                    string query = "Update SaveFinalInspectionDetails set ShiftSupervisor=@ShiftSupervisor,ShiftInspector=@ShiftInspector, RackNo=@RackNo where UserId=@UserId";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ShiftSupervisor", "" + txtSHIFTIN.Text.Trim() + "");
                    cmd.Parameters.AddWithValue("@ShiftInspector", "" + txtTPINM.Text.Trim() + "");
                    cmd.Parameters.AddWithValue("@RackNo", "" + txtRACKNO.Text.Trim() + "");
                    cmd.Parameters.AddWithValue("@UserId", userid);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

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
                Session["NewFinalInsBatch"] = null;
                repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='FinalScan.aspx';", true);
                return;
            }
            else
            {
                //Session["NewFinalInsBatch"] = null;
                //repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');", true);
                return;
            }
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < repDetails.Items.Count; i++)
            {
                /*DropDownList ddlreasons = (DropDownList)repDetails.Items[i].FindControl("ddlreasons")*/
                ;
                DropDownList ddlstatus = (DropDownList)repDetails.Items[i].FindControl("ddlstatus");
                //parameters.Clear();
                //clsm.Fillcombo_Parameter("select ReasonName,ReasonName as b from FinalStatusReason FinalstatusMaster where status=1 and StatusCode='" + ddlstatus.SelectedValue + "' order by StatusCode asc", parameters, ddlreasons);

                ListBox lstreasons = (ListBox)repDetails.Items[i].FindControl("lstreasons");
                clsm.FillListBox("select ReasonName,ReasonName as b from FinalStatusReason where status=1 and StatusCode='" + ddlstatus.SelectedValue + "' order by StatusCode asc", lstreasons);

                if (ddlstatus.SelectedValue == "QC HOLD")
                {
                    HtmlTableRow trreason = (HtmlTableRow)repDetails.Items[i].FindControl("trreason");

                    trreason.Visible = true;
                }
                else
                {
                    HtmlTableRow trreason = (HtmlTableRow)repDetails.Items[i].FindControl("trreason");

                    trreason.Visible = false;
                }
            }
        }
        //Added on 24-10-2023 by om
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
                APIPIPE1 += "{\r\n\"P_ARBPL\": \"COAT\",\r\n \"P_KDAUF\": \"" + txtsaleorder.Text.Trim() + "\",\r\n \"P_KDPOS\": \"" + txtlineitem.Text.Trim() + "\"\r\n  } \r\n ";
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
        private void GetCustPipeDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                string APIPIPENM = "";
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='FinalScan.aspx';", true);
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
                DropDownList ddlstatus = (e.Item.FindControl("ddlstatus") as DropDownList);
                parameters.Clear();
                clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1 order by id asc", parameters, ddlstatus);
                //ddlstatus.SelectedValue = "OK";

                TextBox txtdate = e.Item.FindControl("txtdate") as TextBox;
                txtdate.Text = DateTime.Now.AddHours(-11.30).ToString("yyyy-MM-dd");

                Literal litqrcode = (e.Item.FindControl("litqrcode") as Literal);
                TextBox txtheatdno = e.Item.FindControl("txtheatdno") as TextBox;
                TextBox txtLengthft = e.Item.FindControl("txtLengthft") as TextBox;
                TextBox txtWeight = e.Item.FindControl("txtWeight") as TextBox;
                //Literal litBarePipeHold = (TextBox)repDetails.Items[i].FindControl("litBarePipeHold");
                TextBox txtcustpipeno = e.Item.FindControl("txtcustpipeno") as TextBox;
                TextBox txtFeedSequenceNo = e.Item.FindControl("txtFeedSequenceNo") as TextBox;
                TextBox txtheatcode = e.Item.FindControl("txtheatcode") as TextBox;
                HiddenField hiddenLengthft = e.Item.FindControl("hiddenLengthft") as HiddenField;
                HiddenField hiddenFLengthft = e.Item.FindControl("hiddenFLengthft") as HiddenField;
                HiddenField hiddenWeight = e.Item.FindControl("hiddenWeight") as HiddenField;
                HiddenField hiddenFWeight = e.Item.FindControl("hiddenFWeight") as HiddenField;

                //Api Get Heat No
                string WorkStationId = Session["WorkStationId"].ToString();


                string APIPIPE1 = "";
                var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPBATCHDETGET");//ZVPPPTSCOATGET
                client1.Timeout = -1;
                var request1 = new RestRequest(Method.POST);
                request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request1.AddHeader("Content-Type", "application/json");


                string Parameter1 = "";
                string Parameter2 = "";
                string Parameter3 = "";
                string Parameter4 = "";
                string Parameter5 = "";
                string Parameter6 = "";
                string Parameter7 = "";
                string Parameter8 = "";
                string Parameter9 = "";
                string Parameter10 = "";
                string Parameter11 = "";
                string Parameter12 = "";

                string Heatno1 = "";
                string V_LENGTH = "";
                string V_WEIGHT = "";
                string V_BSTAT = "";
                string V_OPNAME = "";
                string V_CTYPE = "";
                string V_CUSTP = "";
                string V_FSNUM = "";
                string V_HEATCODE = "";
                string Status1 = "";
                string Massage1 = "";
                string display1 = "";
                string userid1 = Session["SAPID"].ToString();
                string Stationtype = Session["StationType"].ToString();
                Literal litqrcode1 = e.Item.FindControl("litqrcode") as Literal;
                APIPIPE1 += "{\r\n\"P_BATCH\": \"" + litqrcode1.Text.ToUpper().Trim() + "\",\r\n \"P_PROCT\": \"" + Stationtype + "\",\r\n \"P_ARBPL\": \"" + WorkStationId + "\"\r\n  } \r\n ";
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
                    GetCustPipeDetails();
                    display1 = response1.Content.ToString();
                    dynamic d = JObject.Parse(display1);
                    //Added on 20-10-2023 by om
                    dynamic d1 = SetHeatNoEditable();
                    if (Session["DtCustPipeDetails"] == null)//MTR
                    {
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
                    }
                    else
                    {
                        txtcustpipeno.ReadOnly = true;
                        txtheatdno.ReadOnly = true;

                    }
                    Heatno1 = d["MT_RESPONSE"]["V_HEATNO"];
                    V_LENGTH = d["MT_RESPONSE"]["V_LENGTH"];
                    V_WEIGHT = d["MT_RESPONSE"]["V_WEIGHT"];
                    V_BSTAT = d["MT_RESPONSE"]["V_BSTAT"];
                    V_OPNAME = d["MT_RESPONSE"]["V_OPNAME"];
                    V_CTYPE = d["MT_RESPONSE"]["V_CTYPE"];
                    V_CUSTP = d["MT_RESPONSE"]["V_CUSTP"];
                    V_FSNUM = d["MT_RESPONSE"]["V_FSNUM"];
                    V_HEATCODE = d["MT_RESPONSE"]["V_HEATCODE"];


                    Parameter1 = d["MT_RESPONSE"]["V_PARAM01"];
                    Parameter2 = d["MT_RESPONSE"]["V_PARAM02"];
                    Parameter3 = d["MT_RESPONSE"]["V_PARAM03"];
                    Parameter4 = d["MT_RESPONSE"]["V_PARAM04"];
                    Parameter5 = d["MT_RESPONSE"]["V_PARAM05"];
                    Parameter6 = d["MT_RESPONSE"]["V_PARAM06"];
                    Parameter7 = d["MT_RESPONSE"]["V_PARAM07"];
                    Parameter8 = d["MT_RESPONSE"]["V_PARAM08"];
                    Parameter9 = d["MT_RESPONSE"]["V_PARAM09"];
                    Parameter10 = d["MT_RESPONSE"]["V_PARAM10"];
                    Parameter11 = d["MT_RESPONSE"]["V_PARAM11"];
                    Parameter12 = d["MT_RESPONSE"]["V_PARAM12"];

                    Status1 = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                    Massage1 = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Worng- " + response1.Content.ToString() + "');window.location ='FinalScan.aspx';", true);
                    return;
                }
                if (Status1 != "S")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + Massage1 + "');window.location ='FinalScan.aspx';", true);
                    return;
                }

                txtheatdno.Text = Heatno1.Trim();
                txtWeight.Text = V_WEIGHT.Trim();
                hiddenWeight.Value = V_WEIGHT.Trim();
                hiddenFWeight.Value = V_WEIGHT.Trim();
                txtLengthft.Text = V_LENGTH.Trim();
                hiddenLengthft.Value = V_LENGTH.Trim();
                hiddenFLengthft.Value = V_LENGTH.Trim();
                txtcustpipeno.Text = V_CUSTP.Trim();
                //litBarePipeHold.Text = IT_PEND-HLDREG.trim();
                txtFeedSequenceNo.Text = V_FSNUM.TrimStart(new Char[] { '0' }).Trim();
                txtheatcode.Text = V_HEATCODE.Trim();
                if (!string.IsNullOrEmpty(V_BSTAT))
                {
                    //if (V_BSTAT != "QC HOLD")
                    //{
                    //    ddlstatus.SelectedValue = V_BSTAT;

                    //}
                    //if (V_BSTAT != "STRIPPING")
                    //{
                    //    foreach (ListItem li in ddlstatus.Items)
                    //    {
                    //        if (li.Value == "STRIPPING")
                    //        {
                    //            li.Attributes.Add("style", "display:none");
                    //        }
                    //    }
                    //}


                    if (V_BSTAT == "QC HOLD" && V_CTYPE == "COAT PIPE" && (V_OPNAME == "COATING APPLICATION ID" || V_OPNAME == "COATING APPLICATION OD"))
                    {
                        Literal litqcholdat = e.Item.FindControl("litqcholdat") as Literal;
                        HtmlTableRow trqcholdat = e.Item.FindControl("trqcholdat") as HtmlTableRow;
                        trqcholdat.Visible = true;
                        litqcholdat.Text = V_OPNAME;

                        foreach (ListItem li in ddlstatus.Items)
                        {
                            if (li.Value == "REWORK" || li.Value == "QC HOLD")
                            {
                                li.Attributes.Add("style", "display:none");
                            }
                        }
                        HtmlTableRow trreason = e.Item.FindControl("trreason") as HtmlTableRow;
                        trreason.Visible = false;
                        //show only-OK or Stripping or Reject
                        parameters.Clear();
                        clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1 and StatusName not in('REWORK','QC HOLD')  order by id asc", parameters, ddlstatus);
                    }
                    else
                    {
                        Literal litqcholdat = e.Item.FindControl("litqcholdat") as Literal;
                        HtmlTableRow trqcholdat = e.Item.FindControl("trqcholdat") as HtmlTableRow;
                        trqcholdat.Visible = true;
                        litqcholdat.Text = V_OPNAME;

                        foreach (ListItem li in ddlstatus.Items)
                        {
                            if (li.Value == "OK" || li.Value == "STRIPPING" || li.Value == "QC HOLD")
                            {
                                li.Attributes.Add("style", "display:none");
                            }
                        }
                        HtmlTableRow trreason = e.Item.FindControl("trreason") as HtmlTableRow;
                        trreason.Visible = false;
                        //show only-Rework or Reject
                        parameters.Clear();
                        clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1 and StatusName not in('OK','QC HOLD','STRIPPING')  order by id asc", parameters, ddlstatus);

                    }
                    if (V_BSTAT == "QC HOLD" && V_CTYPE == "BARE PIPE" && (V_OPNAME == "COATING APPLICATION ID" || V_OPNAME == "COATING APPLICATION OD"))
                    {
                        Literal litqcholdat = e.Item.FindControl("litqcholdat") as Literal;
                        HtmlTableRow trqcholdat = e.Item.FindControl("trqcholdat") as HtmlTableRow;
                        trqcholdat.Visible = true;
                        litqcholdat.Text = V_OPNAME;

                        foreach (ListItem li in ddlstatus.Items)
                        {
                            if (li.Value == "OK" || li.Value == "STRIPPING" || li.Value == "QC HOLD")
                            {
                                li.Attributes.Add("style", "display:none");
                            }
                        }
                        HtmlTableRow trreason = e.Item.FindControl("trreason") as HtmlTableRow;
                        trreason.Visible = false;
                        //show only-Rework or Reject
                        parameters.Clear();
                        clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1 and StatusName not in('OK','QC HOLD','STRIPPING')  order by id asc", parameters, ddlstatus);
                    }


                    if (V_BSTAT == "OK")
                    {

                        foreach (ListItem li in ddlstatus.Items)
                        {
                            if (li.Value == "STRIPPING" || li.Value == "REWORK")
                            {
                                li.Attributes.Add("style", "display:none");
                            }
                        }
                        //show only-OK or QC Hold or Reject
                        HtmlTableRow trreason = e.Item.FindControl("trreason") as HtmlTableRow;
                        trreason.Visible = false;
                        parameters.Clear();
                        clsm.Fillcombo_Parameter("select StatusName,StatusName as b from FinalstatusMaster where status=1 and StatusName not in('STRIPPING','REWORK')  order by id asc", parameters, ddlstatus);
                        if (Session["HLDREG_Val"] != "")
                        {
                            Literal litBarePipeHold = e.Item.FindControl("litBarePipeHold") as Literal;
                            litBarePipeHold.Text = Session["HLDREG_Val"].ToString();
                            ddlstatus.SelectedValue = "QC HOLD";                            
                            Session["HLDREG_Val"] = null;
                        }
                        else
                        {
                            ddlstatus.SelectedValue = V_BSTAT;
                        }
                        
                    }
                    if (!string.IsNullOrEmpty(V_CTYPE))
                    {
                        Literal litcoattype = e.Item.FindControl("litcoattype") as Literal;
                        DropDownList ddlcoattype = e.Item.FindControl("ddlcoattype") as DropDownList;
                        litcoattype.Text = V_CTYPE;
                        ddlcoattype.SelectedValue = V_CTYPE;
                        ddlcoattype.Visible = true;
                        HtmlTableRow trCoatingtype = e.Item.FindControl("trCoatingtype") as HtmlTableRow;
                        trCoatingtype.Visible = true;



                    }

                }

                //DropDownList ddlreasons = e.Item.FindControl("ddlreasons") as DropDownList;
                //parameters.Clear();
                //clsm.Fillcombo_Parameter("select ReasonName,ReasonName as b from FinalStatusReason where status=1 and StatusCode='" + ddlstatus.SelectedValue + "' order by StatusCode asc", parameters, ddlreasons);

                ListBox lstreasons = e.Item.FindControl("lstreasons") as ListBox;
                clsm.FillListBox("select ReasonName,ReasonName as b from FinalStatusReason where status=1 and StatusCode='" + ddlstatus.SelectedValue + "' order by StatusCode asc", lstreasons);

                TextBox txtParameter1 = (e.Item.FindControl("txtParameter1") as TextBox);
                TextBox txtParameter2 = (e.Item.FindControl("txtParameter2") as TextBox);
                TextBox txtParameter3 = (e.Item.FindControl("txtParameter3") as TextBox);
                TextBox txtParameter4 = (e.Item.FindControl("txtParameter4") as TextBox);
                TextBox txtParameter5 = (e.Item.FindControl("txtParameter5") as TextBox);
                TextBox txtParameter6 = (e.Item.FindControl("txtParameter6") as TextBox);
                TextBox txtParameter7 = (e.Item.FindControl("txtParameter7") as TextBox);
                TextBox txtParameter8 = (e.Item.FindControl("txtParameter8") as TextBox);
                TextBox txtParameter9 = (e.Item.FindControl("txtParameter9") as TextBox);
                TextBox txtParameter10 = (e.Item.FindControl("txtParameter10") as TextBox);
                TextBox txtParameter11 = (e.Item.FindControl("txtParameter11") as TextBox);
                TextBox txtParameter12 = (e.Item.FindControl("txtParameter12") as TextBox);

                if (!string.IsNullOrEmpty(Parameter1))
                {
                    txtParameter1.Text = Parameter1;
                }
                if (!string.IsNullOrEmpty(Parameter2))
                {
                    txtParameter2.Text = Parameter2;
                }
                if (!string.IsNullOrEmpty(Parameter3))
                {
                    txtParameter3.Text = Parameter3;
                }
                if (!string.IsNullOrEmpty(Parameter4))
                {
                    txtParameter4.Text = Parameter4;
                }
                if (!string.IsNullOrEmpty(Parameter5))
                {
                    txtParameter5.Text = Parameter5;
                }
                if (!string.IsNullOrEmpty(Parameter6))
                {
                    txtParameter6.Text = Parameter6;
                }
                if (!string.IsNullOrEmpty(Parameter7))
                {
                    txtParameter7.Text = Parameter7;
                }
                if (!string.IsNullOrEmpty(Parameter8))
                {
                    txtParameter8.Text = Parameter8;
                }
                if (!string.IsNullOrEmpty(Parameter9))
                {
                    txtParameter9.Text = Parameter9;
                }
                if (!string.IsNullOrEmpty(Parameter10))
                {
                    txtParameter10.Text = Parameter10;
                }
                if (!string.IsNullOrEmpty(Parameter11))
                {
                    txtParameter11.Text = Parameter11;
                }
                if (!string.IsNullOrEmpty(Parameter12))
                {
                    txtParameter12.Text = Parameter12;
                }
            }
        }

    }
}