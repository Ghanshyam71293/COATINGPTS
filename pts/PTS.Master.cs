using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using System.Data;


namespace PTSCOATING.pts
{
    public partial class PTS : System.Web.UI.MasterPage
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        helperclass clsm = new helperclass();
        Hashtable parameters = new Hashtable();
        public string stationtype = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("../login.aspx");
                }
                bindrights();
                litusername.Text = Convert.ToString(Session["UserId"]);
            }
        }
        public void bindrights()
        {
            DataSet ds = new DataSet();
            parameters.Clear();
            parameters.Add("@UserId", Session["UserId"].ToString());
            ds = clsm.senddataset_Parameter("select m.*,wcm.WorkCenter,wcm.WorkStationType from login m inner join WorkCenterRightMaster wcm on m.userid=wcm.Userid where m.UserId=@UserId and m.Status=1 ", parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i=0; i < ds.Tables[0].Rows.Count;i++)
                {

                    Session["SAPID"] = ds.Tables[0].Rows[i]["SapId"].ToString();
                    Session["StationType"] = ds.Tables[0].Rows[i]["WorkStationType"].ToString();
                    stationtype = Session["StationType"].ToString();
                    string WorkCenter = ds.Tables[0].Rows[i]["WorkCenter"].ToString();
                    if (WorkCenter == "INBOUND")
                    {
                        DIVINBOUND.Visible = true;
                    }
                    if (WorkCenter == "BLASTING")
                    {
                        DIVBLASTING.Visible = true;
                    }
                    if (WorkCenter == "STEAL INSPECTION")
                    {
                        DIVSTEALINSPECTION.Visible = true;
                    }
                    if (WorkCenter == "COATING APPLICATION")
                    {
                        DIVCOATINGAPP.Visible = true;
                    }
                    if (WorkCenter == "FINAL INSPECTION")
                    {
                        DIVFINALINSPECTION.Visible = true;
                    }
                    if (WorkCenter == "ACID")
                    {
                        DIVACID.Visible = true;
                    }
                    if (WorkCenter == "STRIPPING")
                    {
                        DIVSTRIPPING.Visible = true;
                    }
                    if (WorkCenter == "RELEASE")
                    {
                        //DIVINBOUND.Visible = false;
                        //DIVBLASTING.Visible = false;
                        //DIVSTEALINSPECTION.Visible = false;
                        //DIVCOATINGAPP.Visible = false;
                        //DIVFINALINSPECTION.Visible = false;
                        //DIVACID.Visible = false;
                        //DIVSTRIPPING.Visible = false;

                        DIVRELEASE.Visible = true;
                    }
                    if (WorkCenter == "STENCILVARIFICATION")
                    {
                        DIVSTENCILVARIFICATION.Visible = true;
                    }
                    if (WorkCenter == "CALIBRATION")
                    {
                        DIVCALIBRATION.Visible = true;
                    }
                    if (WorkCenter == "SOLUBLESALTCONTAMINATION")
                    {
                        DIVSOLUBLESALTCONTAMINATION.Visible = true;
                    }
                    if (WorkCenter == "RINGCUT")
                    {
                        DIVRINGCUT.Visible = true;
                    }
                    //Added on 28-04-25
                    if (WorkCenter == "INDUCTION")
                    {
                        DIVInductionEntry.Visible = true;
                    }
                    if (WorkCenter == "LOGBOOK")
                    {
                        DIVPLANTLOGBOOK.Visible = true;
                    }
                    //if (WorkCenter == "DIVMECHANICALLOGBOOK")
                    //{
                    //    DIVMECHANICALLOGBOOK.Visible = true;
                    //}
                }

            }
            
        }
    }
}