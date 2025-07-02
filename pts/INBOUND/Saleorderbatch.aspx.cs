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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING.pts.INBOUND
{
    public partial class Saleorderbatch : System.Web.UI.Page
    {
        string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        helperclass clsm = new helperclass();
        Hashtable parameters = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string connect = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            helperclass clsm = new helperclass();
            Hashtable parameters = new Hashtable();
            if (!IsPostBack)
            {
                if (Session["UserId"] == null || Session["UserId"] == "")
                {
                    Response.Redirect("../../login.aspx");
                }
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Id", typeof(int)));
                dt.Columns.Add(new DataColumn("Codetype", typeof(string)));
                dt.Columns.Add(new DataColumn("Code", typeof(string)));
                dr = dt.NewRow();
                dt.Columns["Id"].AutoIncrement = true;
                dt.Columns["Id"].AutoIncrementSeed = 1;
                dt.Columns["Id"].AutoIncrementStep = 1;
                dr["Codetype"] = string.Empty;
                dr["Code"] = string.Empty;
                Session["NewINBOUNDBatch"] = dt;


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
                if (string.IsNullOrEmpty(Request.QueryString["Type"]))
                {
                    string saleorder = "";
                    string LineItem = "";
                    ds = new DataSet();
                    parameters.Clear();
                    parameters.Add("@UserId", Session["UserId"].ToString());
                    ds = clsm.senddataset_Parameter("select Saleorder,LineItem from InboundGetNextBatch where Uname=@UserId  ", parameters);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            saleorder = ds.Tables[0].Rows[i]["Saleorder"].ToString();
                            LineItem = ds.Tables[0].Rows[i]["LineItem"].ToString();
                        }

                        //txtsaleorder.Text = saleorder;
                        //txtlineitem.Text = LineItem;

                        if (!string.IsNullOrEmpty(saleorder))
                        {
                            string APIPIPE = "";
                            var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATGET");
                            client1.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                            request.AddHeader("Content-Type", "application/json");


                            string display = "";
                            string status = "";
                            string Massage = "";
                            string V_NEXT_BATCH = "";
                            string WorkStationId = Session["WorkStationId"].ToString();
                            APIPIPE += "{\r\n\"P_ARBPL\": \"" + WorkStationId + "\",\r\n \"P_KDAUF\": \"" + saleorder + "\",\r\n \"P_KDPOS\": \"" + LineItem + "\"\r\n  } \r\n ";
                            request.AddParameter("application/json", APIPIPE, ParameterType.RequestBody);
                            IRestResponse response1 = client1.Execute(request);
                            HttpStatusCode statusCode1 = response1.StatusCode;
                            if (statusCode1 == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='InboundScan.aspx';", true);
                                return;
                            }
                            if (Convert.ToString(response1.StatusCode) == "OK")
                            {

                                display = response1.Content.ToString();
                                dynamic d = JObject.Parse(display);
                                V_NEXT_BATCH = d["MT_RESPONSE"]["V_NEXT_BATCH"];


                                status = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                                Massage = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];

                                if (!string.IsNullOrEmpty(V_NEXT_BATCH))
                                {
                                    DataTable dt1 = (DataTable)(HttpContext.Current.Session["NewFinalInsBatch"]);
                                    DataRow dr1 = null;
                                    dr1 = dt1.NewRow();
                                    dr1["Codetype"] = "";
                                    dr1["Code"] = V_NEXT_BATCH.ToUpper().Trim();
                                    dt1.Rows.Add(dr1);
                                    HttpContext.Current.Session["NewFinalInsBatch"] = dt1;

                                   // Response.Redirect("NewBatchList.aspx");

                                    //repDetails.DataSource = dt1;
                                    //repDetails.DataBind();
                                    //if(repDetails.Items.Count>0)
                                    //{
                                    //    btnSubmit.Visible = true;
                                    //}
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + Massage + "');", true);
                                    return;
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Worng- " + Massage + "');", true);
                                return;
                            }
                        }
                    }
                }
            }
        }
        protected void btngetbatch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Id", typeof(int)));
            dt.Columns.Add(new DataColumn("Codetype", typeof(string)));
            dt.Columns.Add(new DataColumn("Code", typeof(string)));
            dr = dt.NewRow();
            dt.Columns["Id"].AutoIncrement = true;
            dt.Columns["Id"].AutoIncrementSeed = 1;
            dt.Columns["Id"].AutoIncrementStep = 1;
            dr["Codetype"] = string.Empty;
            dr["Code"] = string.Empty;
            Session["NewFinalInsBatch"] = dt;

            string APIPIPE = "";

            string saleorder=txtsaleorder.Text.Trim();
            string lineitem = txtlineitem.Text.Trim();

            string APIPIPE1 = "";
            var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPPTSCOATGET");
            client1.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request.AddHeader("Content-Type", "application/json");


            string display = "";
            string status = "";
            string Massage = "";
            string V_NEXT_BATCH = "";
            string WorkStationId = Session["WorkStationId"].ToString();
            APIPIPE += "{\r\n\"P_PROCT\": \"" + "IB" + "\",\r\n \"P_KDAUF\": \"" + saleorder + "\",\r\n \"P_KDPOS\": \"" + lineitem + "\"\r\n  } \r\n ";
            request.AddParameter("application/json", APIPIPE, ParameterType.RequestBody);
            IRestResponse response1 = client1.Execute(request);
            HttpStatusCode statusCode1 = response1.StatusCode;
            if (statusCode1 == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='InboundScan.aspx';", true);
                return;
            }
            if (Convert.ToString(response1.StatusCode) == "OK")
            {

                display = response1.Content.ToString();
                dynamic d = JObject.Parse(display);
                V_NEXT_BATCH = d["IT_PEND"];



                status = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                Massage = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];

                if (!string.IsNullOrEmpty(status) && status=="S")
                {
                    SqlTransaction objTrans = null;
                    SqlConnection cn = new SqlConnection(connect);
                    cn.Open();
                    objTrans = cn.BeginTransaction();
                    try
                    {                      
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.Transaction = objTrans;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "InboundGetNextBatchSp";
                        cmd.Parameters.AddWithValue("@Saleorder", saleorder);
                        cmd.Parameters.AddWithValue("@LineItem", lineitem);
                        cmd.Parameters.AddWithValue("@Uname", Session["UserId"].ToString());
                        cmd.ExecuteNonQuery();

                        objTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        objTrans.Rollback();
                    }
                    finally
                    {
                        cn.Close();
                    }


                    //DataTable dt1 = (DataTable)(HttpContext.Current.Session["NewFinalInsBatch"]);
                    //DataRow dr1 = null;
                    //dr1 = dt1.NewRow();
                    //dr1["Codetype"] = "";
                    //dr1["Code"] = V_NEXT_BATCH.ToUpper().Trim();
                    //dt1.Rows.Add(dr1);
                    //HttpContext.Current.Session["NewFinalInsBatch"] = dt1;

                    // Response.Redirect("NewBatchList.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('SO/LineItem Updated successfully!');window.location ='InboundScan.aspx';", true);
                    return;
                    // Response.Redirect("FinalScan.aspx");

                    //repDetails.DataSource = dt1;
                    //repDetails.DataBind();
                    //if(repDetails.Items.Count>0)
                    //{
                    //    btnSubmit.Visible = true;
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + Massage + "');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Worng- " + Massage + "');", true);
                return;
            }
            
        }
        protected void repDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(repDetails.Items.Count>0)
            {

                Response.Redirect("NewBatchList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Batch not found!');", true);
                return;
            }
        }
    }
}