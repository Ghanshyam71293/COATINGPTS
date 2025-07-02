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

namespace PTSCOATING.pts.BLASTING
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
                if (Session["NewBLASTINGBatch"] == null)
                {
                    Response.Redirect("BlastingScan.aspx");
                }
                DataSet ds = new DataSet();
                parameters.Clear();
                parameters.Add("@UserId", Session["UserId"].ToString());
                ds = clsm.senddataset_Parameter("select m.*,wcm.WorkCenter,wcm.WorkStationId from login m inner join WorkCenterRightMaster wcm on m.userid=wcm.Userid where m.UserId=@UserId and WorkCenter='BLASTING' and m.Status=1 ", parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Session["WorkStationId"] = ds.Tables[0].Rows[i]["WorkStationId"].ToString();
                    }
                }

                binddata();
            }
        }
        public void binddata()
        {
            DataTable dt = (DataTable)(Session["NewBLASTINGBatch"]);
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
                if(Convert.ToString(ViewState["Status"]) == "QC HOLD")
                {
                    btnSubmit.Visible = false; 
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

                try
                {
                    string Parkid = "0";
                    for (int i = 0; i < repDetails.Items.Count; i++)
                    {
                        Literal litqrcode = (Literal)repDetails.Items[i].FindControl("litqrcode");
                        TextBox txtdate = (TextBox)repDetails.Items[i].FindControl("txtdate");
                        DropDownList ddlshift = (DropDownList)repDetails.Items[i].FindControl("ddlshift");
                       
                        DropDownList ddlstatus = (DropDownList)repDetails.Items[i].FindControl("ddlstatus");
                        DropDownList ddlreasons = (DropDownList)repDetails.Items[i].FindControl("ddlreasons");
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
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtqrcode.Text))
            {
                DataTable dt = (DataTable)(Session["NewBLASTINGBatch"]);
                DataRow dr = null;
                dr = dt.NewRow();
                dr["Codetype"] = "";
                dr["Code"] = txtqrcode.Text.ToUpper().Trim();
                dt.Rows.Add(dr);
                binddata();
                txtqrcode.Text = "";
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
              
                DropDownList ddlstatus = (DropDownList)repDetails.Items[i].FindControl("ddlstatus");
                DropDownList ddlreasons = (DropDownList)repDetails.Items[i].FindControl("ddlreasons");

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

                batchno = litqrcode.Text.ToUpper().Trim();
                postdate = txtdate.Text.Trim();
                postdate = string.Concat(postdate.Substring(0, 4), postdate.Substring(5, 2), postdate.Substring(8, 2));
                // postdate = string.Concat(postdate.Substring(6, 4), postdate.Substring(0, 2), postdate.Substring(3, 2));
                shift = ddlshift.SelectedValue;
                if (ddlreasons.SelectedValue != "0")
                {
                    reason = ddlreasons.SelectedValue;
                }

                if (count == 1)
                {
                    APIPIPE += "{\r\n\"BANME\": \"" + userid + "\",\r\n \"PSTDT\": \"" + postdate + "\",\r\n \"SHIFT\": \"" + shift + "\",\r\n \"ARBPL\": \"" + WorkStationId + "\",\r\n \"CHARG\": \"" + batchno + "\",\r\n \"BSTAT\": \"" + ddlstatus.SelectedValue + "\",\r\n \"PROCT\": \"" + Stationtype + "\",\r\n \"HLDREG\": \"" + reason + "\",\r\n \"PARAM01\": \"" + txtParameter1.Text.ToUpper().Trim() + "\",\r\n \"PARAM02\": \"" + txtParameter2.Text.ToUpper().Trim() + "\",\r\n \"PARAM03\": \"" + txtParameter3.Text.ToUpper().Trim() + "\",\r\n \"PARAM04\": \"" + txtParameter4.Text.ToUpper().Trim() + "\",\r\n \"PARAM05\": \"" + txtParameter5.Text.ToUpper().Trim() + "\",\r\n \"PARAM06\": \"" + txtParameter6.Text.ToUpper().Trim() + "\",\r\n \"PARAM07\": \"" + txtParameter7.Text.ToUpper().Trim() + "\",\r\n \"PARAM08\": \"" + txtParameter8.Text.ToUpper().Trim() + "\",\r\n \"PARAM09\": \"" + txtParameter9.Text.ToUpper().Trim() + "\",\r\n \"PARAM10\": \"" + txtParameter10.Text.ToUpper().Trim() + "\",\r\n \"BLINE\": \"X\"\r\n  } \r\n ";
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

            string StatusFinal = status;
            if (StatusFinal == "S")
            {
                Session["NewBLASTINGBatch"] = null;
                repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='BlastingScan.aspx';", true);
                return;
            }
            else
            {
                Session["NewBLASTINGBatch"] = null;
                repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='BlastingScan.aspx';", true);
                return;
            }
        }
        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < repDetails.Items.Count; i++)
            {
                DropDownList ddlreasons = (DropDownList)repDetails.Items[i].FindControl("ddlreasons");
                DropDownList ddlstatus = (DropDownList)repDetails.Items[i].FindControl("ddlstatus");
                parameters.Clear();
                clsm.Fillcombo_Parameter("select ReasonName,ReasonName as b from BlastingStatusReason where status=1 and StatusCode='" + ddlstatus.SelectedValue + "' order by StatusCode asc", parameters, ddlreasons);
            }
        }
        protected void repDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DropDownList ddlstatus = (e.Item.FindControl("ddlstatus") as DropDownList);
                parameters.Clear();
                clsm.Fillcombo_Parameter("select StatusName,StatusName as b from BlastingstatusMaster where status=1 order by id asc", parameters, ddlstatus);
                ddlstatus.SelectedValue = "OK";
                TextBox txtdate = e.Item.FindControl("txtdate") as TextBox;
                txtdate.Text = DateTime.Now.AddHours(-11.30).ToString("yyyy-MM-dd");



                //Api Get Heat No
                string WorkStationId = Session["WorkStationId"].ToString();
                string APIPIPE1 = "";
                var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPBATCHDETGET");
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

                string V_BSTAT = "";
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='BlastingScan.aspx';", true);
                    return;
                }
                if (Convert.ToString(response1.StatusCode) == "OK")
                {
                    display1 = response1.Content.ToString();
                    dynamic d = JObject.Parse(display1);
                    V_BSTAT = d["MT_RESPONSE"]["V_BSTAT"];

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

                    Status1 = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                    Massage1 = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Worng- " + response1.Content.ToString() + "');window.location ='BlastingScan.aspx';", true);
                    return;
                }
                if (Status1 != "S")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + Massage1 + "');window.location ='BlastingScan.aspx';", true);
                    return;
                }
                if (!string.IsNullOrEmpty(V_BSTAT))
                {
                    ddlstatus.SelectedValue = V_BSTAT;
                    ViewState["Status"]= V_BSTAT;
                    if (V_BSTAT == "QC HOLD")
                    {
                        ddlstatus.Enabled = false;
                       
                    }
                }
                DropDownList ddlreasons = e.Item.FindControl("ddlreasons") as DropDownList;
                parameters.Clear();
                clsm.Fillcombo_Parameter("select ReasonName,ReasonName as b from BlastingStatusReason where status=1 and StatusCode='" + ddlstatus.SelectedValue + "' order by StatusCode asc", parameters, ddlreasons);


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

                //Api Get Heat No
            }
        }

      
    }
}