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

namespace PTSCOATING.pts.STENCILVARIFICATION
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
                if (Session["NewStencilVarificationBatch"] == null)
                {
                    Response.Redirect("StencilVarificationScan.aspx");
                }
                binddata();
            }
        }
        public void binddata()
        {

            DataTable dt = (DataTable)(Session["NewStencilVarificationBatch"]);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Seesion has been Expired!!');", true);
                Response.Redirect("../../login.aspx");
                return;
            }
            if (repDetails.Items.Count > 0)
            {
                try
                {
                    for (int i = 0; i < repDetails.Items.Count; i++)
                    {
                        Literal litqrcode = (Literal)repDetails.Items[i].FindControl("litqrcode");
                        TextBox txtdate = (TextBox)repDetails.Items[i].FindControl("txtdate");

                        if (string.IsNullOrEmpty(txtdate.Text))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter date!!');", true);
                            return;
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
            if (string.IsNullOrEmpty(txtqrcode.Text) && string.IsNullOrEmpty(txtsqno.Text) && string.IsNullOrEmpty(txtsaleorder.Text) && string.IsNullOrEmpty(txtlineitem.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Sale Order,Line Item,Squance No or Batch No.');", true);
                return;
            }
            if (!string.IsNullOrEmpty(txtqrcode.Text) && !string.IsNullOrEmpty(txtsqno.Text) && !string.IsNullOrEmpty(txtsaleorder.Text) && !string.IsNullOrEmpty(txtlineitem.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Sale Order,Line Item,Squance No or Batch No.');", true);
                return;
            }
            if (!string.IsNullOrEmpty(txtsqno.Text) && string.IsNullOrEmpty(txtsaleorder.Text) && string.IsNullOrEmpty(txtlineitem.Text) && string.IsNullOrEmpty(txtqrcode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Sale Order,Line Item or Batch No.');", true);
                return;
            }
            if (!string.IsNullOrEmpty(txtsqno.Text) && !string.IsNullOrEmpty(txtsaleorder.Text) && string.IsNullOrEmpty(txtlineitem.Text) && string.IsNullOrEmpty(txtqrcode.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Line Item or Batch No.');", true);
                return;
            }
            if (!string.IsNullOrEmpty(txtsqno.Text) && !string.IsNullOrEmpty(txtsaleorder.Text) && !string.IsNullOrEmpty(txtlineitem.Text) && string.IsNullOrEmpty(txtqrcode.Text))
            {
                DataTable dt = (DataTable)(Session["NewStencilVarificationBatch"]);
                DataRow dr = null;
                dr = dt.NewRow();
                dr["Codetype"] = "";
                dr["Code"] = "";
                dr["SQNO"] = txtsqno.Text.ToUpper().Trim();
                dr["SaleOrder"] = txtsaleorder.Text.ToUpper().Trim();
                dr["LineItem"] = txtlineitem.Text.ToUpper().Trim();
                dr["LineItem"] = txtlineitem.Text.ToUpper().Trim();
                dt.Rows.Add(dr);
                binddata();
                txtqrcode.Text = "";
                txtsqno.Text = "";
                txtsaleorder.Text = "";
                txtlineitem.Text = "";
                //VerifyBtn.Visible = true;
            }
            if (!string.IsNullOrEmpty(txtqrcode.Text) && string.IsNullOrEmpty(txtsqno.Text) && string.IsNullOrEmpty(txtsaleorder.Text) && string.IsNullOrEmpty(txtlineitem.Text))
            {
                DataTable dt = (DataTable)(Session["NewStencilVarificationBatch"]);
                DataRow dr = null;
                dr = dt.NewRow();
                dr["Codetype"] = "";
                dr["Code"] = txtqrcode.Text.ToUpper().Trim();
                dt.Rows.Add(dr);
                binddata();
                txtqrcode.Text = "";
                txtsqno.Text = "";
                txtsaleorder.Text = "";
                txtlineitem.Text = "";
                //VerifyBtn.Visible = true;
            }

            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(),
            //"alert",
            //"alert('Please Enter Batch No.');",
            //true);
            //        return;
            //    }
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
            string POST_PARK = "";
            string P_RACKNO = "";

            string reason = "";
            string userid = Session["SAPID"].ToString();

            //Batch
            for (int i = 0; i < repDetails.Items.Count; i++)
            {
                Literal litqrcode = (Literal)repDetails.Items[i].FindControl("litqrcode");
                TextBox txtdate = (TextBox)repDetails.Items[i].FindControl("txtdate");
                TextBox RackNo = (TextBox)repDetails.Items[i].FindControl("RackNo");
                DropDownList ddlshift = (DropDownList)repDetails.Items[i].FindControl("ddlshift");
                //DropDownList listReason = (DropDownList)repDetails.Items[i].FindControl("txtReason");


                batchno = litqrcode.Text.ToUpper().Trim();
                postdate = txtdate.Text.Trim();
                postdate = string.Concat(postdate.Substring(0, 4), postdate.Substring(5, 2), postdate.Substring(8, 2));
                shift = ddlshift.SelectedValue;
                POST_PARK = "B";
                P_RACKNO = RackNo.Text.Trim();

                ListBox ListRSN1 = (ListBox)repDetails.Items[i].FindControl("txtReason");
                ListBox ListRSN = (ListBox)repDetails.Items[i].FindControl("SV_txtReason");

                string listReason1 = "";
                foreach (ListItem item in ListRSN1.Items)
                {
                    if (item.Selected)
                    {
                        //listReason += item.Text + " " + item.Value + ",";
                        listReason1 += item.Value + ",";
                    }
                }
                string listReason = "";
                foreach (ListItem item in ListRSN.Items)
                {
                    if (item.Selected)
                    {
                        //listReason += item.Text + " " + item.Value + ",";
                        listReason += item.Value + ",";
                    }
                }

                //if (listReason1 == "" || listReason1 == null)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select hold reason');", true);
                //    return;
                //}
                if (listReason == "" || listReason == null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select sv hold reason');", true);
                    return;
                }
                string ListRSNVal1 = "";
                if (listReason1 != "")
                {
                    ListRSNVal1 = listReason1.Remove(listReason1.Length - 1, 1);
                }

                string ListRSNVal = listReason.Remove(listReason.Length - 1, 1);

                if (count == 1)
                {
                    APIPIPE += "{\r\n\"BANME\": \"" + userid + "\",\r\n \"PSTDT\": \"" + postdate + "\",\r\n \"SHIFT\": \"" + shift + "\",\r\n \"CHARG\": \"" + batchno + "\",\r\n \"POST_PARK\": \"" + POST_PARK + "\",\r\n \"P_RACKNO \": \"" + P_RACKNO + "\",\r\n \"SHLDREG \": \"" + ListRSNVal + "\",\r\n\"HLDREG\": \"" + ListRSNVal1 + "\" } \r\n ";
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
                Session["NewStencilVarificationBatch"] = null;
                repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');window.location ='StencilVarificationScan.aspx';", true);

                SendFormVarify();
                return;
            }
            else
            {
                //Session["NewStencilVarificationBatch"] = null;
                //repDetails.DataSource = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + MESSAGE + "');", true);
                return;

            }
        }

        protected void repDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtdate = e.Item.FindControl("txtdate") as TextBox;
                txtdate.Text = DateTime.Now.AddHours(-11.30).ToString("yyyy-MM-dd");
                //Api Get

                string APIPIPE1 = "";
                var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPCOATSTENSIL");
                client1.Timeout = -1;
                var request1 = new RestRequest(Method.POST);
                request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
                request1.AddHeader("Content-Type", "application/json");

                string T_SALEORDER = "";
                string T_LINE_ITEM = "";
                string T_BATCH_NO = "";
                string T_HEATE_NO = "";
                string T_CUST_PIPE_NO = "";
                string T_LENGTH = "";
                string T_SEQUENCE_NO = "";
                string T_REASON = "";
                string T_REMARKS = "";
                string T_STATUS = "";

                string Status1 = "";
                string MESSAGE1 = "";
                string display1 = "";
                string userid1 = Session["SAPID"].ToString();

                Literal litqrcode1 = e.Item.FindControl("litqrcode") as Literal;
                Literal litsqno = e.Item.FindControl("litsqno") as Literal;
                Literal litsaleorder = e.Item.FindControl("litsaleorder") as Literal;
                Literal litlineitem = e.Item.FindControl("litlineitem") as Literal;

                APIPIPE1 += "{\r\n\"P_MODE\": \"SV\",\r\n\"P_UNAME\": \"" + userid1 + "\",\r\n\"P_CHARG\": \"" + litqrcode1.Text.Trim() + "\", \r\n\"P_KDAUF\": \"" + litsaleorder.Text.Trim() + "\", \r\n\"P_KDPOS\": \"" + litlineitem.Text.Trim() + "\", \r\n\"P_SQNUM\": \"" + litsqno.Text.Trim() + "\" } \r\n ";


                request1.AddParameter("application/json", APIPIPE1, ParameterType.RequestBody);
                IRestResponse response1 = client1.Execute(request1);
                HttpStatusCode statusCode1 = response1.StatusCode;
                if (statusCode1 == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='StencilVarificationScan.aspx';", true);
                    return;
                }
                if (Convert.ToString(response1.StatusCode) == "OK")
                {
                    display1 = response1.Content.ToString();
                    dynamic d = JObject.Parse(display1);

                    if (display1.Contains("IT_DATA"))
                    {
                        T_SALEORDER = d["MT_RESPONSE"]["IT_DATA"]["KDAUF"];
                        T_LINE_ITEM = d["MT_RESPONSE"]["IT_DATA"]["KDPOS"];
                        T_BATCH_NO = d["MT_RESPONSE"]["IT_DATA"]["CHARG"];
                        T_HEATE_NO = d["MT_RESPONSE"]["IT_DATA"]["HEATN"];
                        T_CUST_PIPE_NO = d["MT_RESPONSE"]["IT_DATA"]["CUSTP"];
                        T_LENGTH = d["MT_RESPONSE"]["IT_DATA"]["LENGTH"];
                        T_SEQUENCE_NO = d["MT_RESPONSE"]["IT_DATA"]["SQNUM"];
                        T_STATUS = d["MT_RESPONSE"]["IT_DATA"]["BSTAT"];
                        T_REASON = d["MT_RESPONSE"]["IT_DATA"]["HLDREG"];
                        T_REMARKS = d["MT_RESPONSE"]["IT_DATA"]["REMARKS"];
                    }

                    if (display1.Contains("IT_RETURN"))
                    {
                        Status1 = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                        MESSAGE1 = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + response1.Content.ToString() + "');window.location ='StencilVarificationScan.aspx';", true);
                    return;
                }
                if (display1.Contains("IT_RETURN"))
                {
                    if (Status1 != "S")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + MESSAGE1 + "');window.location ='StencilVarificationScan.aspx';", true);
                        return;
                    }
                }


                TextBox txtSaleOrder = e.Item.FindControl("txtSaleOrder") as TextBox;
                TextBox txtLineItem = e.Item.FindControl("txtLineItem") as TextBox;
                TextBox txtheatno = e.Item.FindControl("txtheatno") as TextBox;
                TextBox SV_txtheatno = e.Item.FindControl("SV_txtheatno") as TextBox;

                TextBox txtcustpipeno = e.Item.FindControl("txtcustpipeno") as TextBox;
                TextBox SV_txtcustpipeno = e.Item.FindControl("SV_txtcustpipeno") as TextBox;

                TextBox txtLength = e.Item.FindControl("txtLength") as TextBox;
                TextBox SV_txtLength = e.Item.FindControl("SV_txtLength") as TextBox;

                TextBox txtSequence = e.Item.FindControl("txtSequence") as TextBox;
                TextBox SV_txtSequence = e.Item.FindControl("SV_txtSequence") as TextBox;

                TextBox txtRemarks = e.Item.FindControl("txtRemarks") as TextBox;
                TextBox textStatus = e.Item.FindControl("textStatus") as TextBox;
                TextBox SV_textStatus = e.Item.FindControl("SV_textStatus") as TextBox;

                ListBox txtReason = e.Item.FindControl("txtReason") as ListBox;
                clsm.FillListBox("select ReasonName, ReasonName as b from FinalStatusReason where status=1 order by id asc", txtReason);
                ListBox SV_txtReason = e.Item.FindControl("SV_txtReason") as ListBox;
                clsm.FillListBox("select ReasonName, ReasonName as b from FinalStatusReason where status=1 order by id asc", SV_txtReason);

                txtSaleOrder.Text = T_SALEORDER.Trim();
                txtLineItem.Text = T_LINE_ITEM.Trim();
                txtheatno.Text = T_HEATE_NO.Trim();
                SV_txtheatno.Text = T_HEATE_NO.Trim();

                txtcustpipeno.Text = T_CUST_PIPE_NO.Trim();
                SV_txtcustpipeno.Text = T_CUST_PIPE_NO.Trim();

                txtLength.Text = T_LENGTH.Trim();
                SV_txtLength.Text = T_LENGTH.Trim();

                txtSequence.Text = T_SEQUENCE_NO.Trim();
                SV_txtSequence.Text = T_SEQUENCE_NO.Trim();


                if (T_REASON != "")
                {
                    string[] RSN_Split = T_REASON.Split(',');

                    for (int i = 0; i < RSN_Split.Length; i++)
                    {
                        foreach (ListItem item in txtReason.Items)
                        {
                            if (item.Value == RSN_Split[i].ToString())
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }

                //if (T_REASON != "")
                //{
                //    string[] RSN_Split = T_REASON.Split(',');
                //    txtReason.Text = T_REASON;

                //    for (int i = 0; i < RSN_Split.Length; i++)
                //    {
                //        foreach (ListItem item in SV_txtReason.Items)
                //        {
                //            if (item.Value == RSN_Split[i].ToString())
                //            {
                //                item.Selected = true;
                //            }
                //        }
                //    }
                //}

                txtRemarks.Text = T_REMARKS.Trim();
                textStatus.Text = T_STATUS.Trim();
                SV_textStatus.Text = T_STATUS.Trim();

                if (string.IsNullOrEmpty(litqrcode1.Text.Trim()))
                {
                    litqrcode1.Text = T_BATCH_NO;
                }
                //Api Get 
            }
        }

        protected void VerifyBtn_Click(object sender, EventArgs e)
        {
            if (Session["SAPID"] == null || Session["UserId"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Seesion has been Expired!!');", true);
                Response.Redirect("../../login.aspx");
                return;
            }
            if (repDetails.Items.Count > 0)
            {
                try
                {
                    SendFormVarify();
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
        protected void SendFormVarify()
        {
            var client1 = new RestClient(ConfigurationManager.AppSettings["APIURL"] + "ZVPPCOATSTENSIL");
            client1.Timeout = -1;
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("Authorization", ConfigurationManager.AppSettings["Authorization"]);
            request1.AddHeader("Content-Type", "application/json");

            string APIPIPE1 = "";
            string userid1 = Session["SAPID"].ToString();
            string display1 = "";
            string Status1 = "";
            string MESSAGE1 = "";

            for (int i = 0; i < repDetails.Items.Count; i++)
            {
                Literal litqrcode1 = (Literal)repDetails.Items[i].FindControl("litqrcode");
                TextBox txtSaleOrder = (TextBox)repDetails.Items[i].FindControl("txtSaleOrder");
                TextBox txtSequence = (TextBox)repDetails.Items[i].FindControl("txtSequence");
                TextBox txtLineItem = (TextBox)repDetails.Items[i].FindControl("txtLineItem");

                TextBox SV_txtSequence = (TextBox)repDetails.Items[i].FindControl("SV_txtSequence");
                TextBox SV_txtheatno = (TextBox)repDetails.Items[i].FindControl("SV_txtheatno");
                TextBox SV_txtcustpipeno = (TextBox)repDetails.Items[i].FindControl("SV_txtcustpipeno");
                TextBox SV_txtLength = (TextBox)repDetails.Items[i].FindControl("SV_txtLength");
                TextBox SV_textStatus = (TextBox)repDetails.Items[i].FindControl("SV_textStatus");
                //ListBox txtReason = (ListBox)repDetails.Items[i].FindControl("txtReason");
                ListBox SV_txtReason = (ListBox)repDetails.Items[i].FindControl("SV_txtReason");

                string sv_listReason = "";
                foreach (ListItem item in SV_txtReason.Items)
                {
                    if (item.Selected)
                    {
                        sv_listReason += item.Value + ",";
                    }
                }
                //if (sv_listReason == "" || sv_listReason == null)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select sv hold reason');", true);
                //    return;
                //}
                string SV_RSN = "";
                if (sv_listReason != "")
                {
                    SV_RSN = sv_listReason.Remove(sv_listReason.Length - 1, 1);
                }


                if (!string.IsNullOrEmpty(litqrcode1.Text) || !string.IsNullOrEmpty(txtSaleOrder.Text) || !string.IsNullOrEmpty(txtSequence.Text) || !string.IsNullOrEmpty(txtLineItem.Text))
                {
                    APIPIPE1 += "{\r\n\"P_MODE\": \"PO\"," +
                        "\r\n\"P_UNAME\": \"" + userid1 + "\", " +
                        "\r\n\"P_KDAUF\": \"" + txtSaleOrder.Text.Trim() + "\", " +
                        "\r\n\"P_KDPOS\": \"" + txtLineItem.Text.Trim() + "\"," +
                        "\r\n\"P_CHARG\": \"" + litqrcode1.Text.Trim() + "\"," +
                        "\r\n\"P_SQNUM\": \"" + txtSequence.Text.Trim() + "\"," +
                        "\r\n\"S_SQNUM\": \"" + SV_txtSequence.Text.Trim() + "\"," +
                        "\r\n\"S_HEATN\": \"" + SV_txtheatno.Text.Trim() + "\"," +
                        "\r\n\"S_CUSTP\": \"" + SV_txtcustpipeno.Text.Trim() + "\"," +
                        "\r\n\"S_LENGTH\": \"" + SV_txtLength.Text.Trim() + "\"," +
                        "\r\n\"S_BSTAT\": \"" + SV_textStatus.Text.Trim() + "\"," +
                        "\r\n\"S_HLDREG\": \"" + SV_RSN + "\"} \r\n ";

                    request1.AddParameter("application/json", APIPIPE1, ParameterType.RequestBody);
                    IRestResponse response1 = client1.Execute(request1);
                    HttpStatusCode statusCode1 = response1.StatusCode;
                    if (statusCode1 == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Server Not Respond!');window.location ='NewBatchList.aspx';", true);
                        return;
                    }
                    if (Convert.ToString(response1.StatusCode) == "OK")
                    {
                        display1 = response1.Content.ToString();
                        dynamic d = JObject.Parse(display1);

                        if (display1.Contains("IT_RETURN"))
                        {
                            Status1 = d["MT_RESPONSE"]["IT_RETURN"]["TYPE"];
                            MESSAGE1 = d["MT_RESPONSE"]["IT_RETURN"]["MESSAGE"];

                            if (Status1 == "S" || Status1 == "E")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + MESSAGE1 + "');window.location ='StencilVarificationScan.aspx';", true);
                                return;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + MESSAGE1 + "')", true);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Value should not be null!')", true);
                    return;
                }
            }
        }




    }
}