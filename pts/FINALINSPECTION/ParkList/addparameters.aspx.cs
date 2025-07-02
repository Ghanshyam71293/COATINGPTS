using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING.pts.FINALINSPECTION.ParkList
{
    public partial class addparameters : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("../../../login.aspx");
                }
                if (Session["BatchParameters"] ==null)
                {
                    SetInitialRow();

                    //gvDetails.DataSource = Session["BatchParametersNew"];
                    //gvDetails.DataBind();
                  

                }
                else
                {
                    gvDetails.DataSource = Session["BatchParameters"];
                    gvDetails.DataBind();
                }


            }
        }
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Id", typeof(int)));
            dt.Columns.Add(new DataColumn("Batch", typeof(string)));
            dt.Columns.Add(new DataColumn("Parameter1", typeof(string)));
            dt.Columns.Add(new DataColumn("Parameter2", typeof(string)));
            dt.Columns.Add(new DataColumn("Parameter3", typeof(string)));
            dt.Columns.Add(new DataColumn("Parameter4", typeof(string)));
            dt.Columns.Add(new DataColumn("Parameter5", typeof(string)));
            dt.Columns.Add(new DataColumn("Parameter6", typeof(string)));
            dt.Columns.Add(new DataColumn("Parameter7", typeof(string)));
            dt.Columns.Add(new DataColumn("Parameter8", typeof(string)));
            dr = dt.NewRow();
            dt.Columns["Id"].AutoIncrement = true;
            dt.Columns["Id"].AutoIncrementSeed = 1;
            dr["Batch"] = string.Empty;
            dr["Parameter1"] = string.Empty;
            dr["Parameter2"] = string.Empty;
            dr["Parameter3"] = string.Empty;
            dr["Parameter4"] = string.Empty;
            dr["Parameter5"] = string.Empty;
            dr["Parameter6"] = string.Empty;
            dr["Parameter7"] = string.Empty;
            dr["Parameter8"] = string.Empty;

            DataTable dtCurrentTable = (DataTable)Session["BatchParametersNew"];
            for (int k = 0; k < dtCurrentTable.Rows.Count; k++)
            {
                dr = dt.NewRow();
                dr["Batch"] = dtCurrentTable.Rows[k]["CHARG"].ToString();
                dr["Parameter1"] = dtCurrentTable.Rows[k]["PARAM01"].ToString();
                dr["Parameter2"] = dtCurrentTable.Rows[k]["PARAM02"].ToString();
                dr["Parameter3"] = dtCurrentTable.Rows[k]["PARAM03"].ToString();
                dr["Parameter4"] = dtCurrentTable.Rows[k]["PARAM04"].ToString();
                dr["Parameter5"] = dtCurrentTable.Rows[k]["PARAM05"].ToString();
                dr["Parameter6"] = dtCurrentTable.Rows[k]["PARAM06"].ToString();
                dr["Parameter7"] = dtCurrentTable.Rows[k]["PARAM07"].ToString();
                dr["Parameter8"] = dtCurrentTable.Rows[k]["PARAM08"].ToString();
                dt.Rows.Add(dr);
            }



            ViewState["BatchParameters"] = dt;
            Session["BatchParameters"] = dt;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
        }

        protected void Save(object sender, EventArgs e)
        {

            DataTable dtCurrentTable = (DataTable)Session["BatchParameters"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {

                    Literal litqrcode = (Literal)gvDetails.Rows[i].FindControl("litqrcode");
                    TextBox txtparameter1 = (TextBox)gvDetails.Rows[i].FindControl("txtparameter1");
                    TextBox txtparameter2 = (TextBox)gvDetails.Rows[i].FindControl("txtparameter2");
                    TextBox txtparameter3 = (TextBox)gvDetails.Rows[i].FindControl("txtparameter3");
                    TextBox txtparameter4 = (TextBox)gvDetails.Rows[i].FindControl("txtparameter4");
                    TextBox txtparameter5 = (TextBox)gvDetails.Rows[i].FindControl("txtparameter5");
                    TextBox txtparameter6 = (TextBox)gvDetails.Rows[i].FindControl("txtparameter6");
                    TextBox txtparameter7 = (TextBox)gvDetails.Rows[i].FindControl("txtparameter7");
                    TextBox txtparameter8 = (TextBox)gvDetails.Rows[i].FindControl("txtparameter8");
                    dtCurrentTable.Rows[i]["Batch"] = litqrcode.Text;
                    dtCurrentTable.Rows[i]["Parameter1"] = txtparameter1.Text;
                    dtCurrentTable.Rows[i]["Parameter2"] = txtparameter2.Text;
                    dtCurrentTable.Rows[i]["Parameter3"] = txtparameter3.Text;
                    dtCurrentTable.Rows[i]["Parameter4"] = txtparameter4.Text;
                    dtCurrentTable.Rows[i]["Parameter5"] = txtparameter5.Text;
                    dtCurrentTable.Rows[i]["Parameter6"] = txtparameter6.Text;
                    dtCurrentTable.Rows[i]["Parameter7"] = txtparameter7.Text;
                    dtCurrentTable.Rows[i]["Parameter8"] = txtparameter8.Text;
                }
                ViewState["BatchParameters"] = dtCurrentTable;
                Session["BatchParameters"] = dtCurrentTable;
                gvDetails.DataSource = dtCurrentTable;
                gvDetails.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
      "alert",
      "alert('Update successful');",
      true);

          
        }
    }
}