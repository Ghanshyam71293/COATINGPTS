using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING.pts.STENCILVARIFICATION
{
    public partial class StencilVarificationScan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                dt.Columns.Add(new DataColumn("SQNO", typeof(string)));
                dt.Columns.Add(new DataColumn("SaleOrder", typeof(string)));
                dt.Columns.Add(new DataColumn("LineItem", typeof(string)));
                dr = dt.NewRow();
                dt.Columns["Id"].AutoIncrement = true;
                dt.Columns["Id"].AutoIncrementSeed = 1;
                dt.Columns["Id"].AutoIncrementStep = 1;
                dr["Codetype"] = string.Empty;
                dr["Code"] = string.Empty;
                dr["SQNO"] = string.Empty;
                dr["SaleOrder"] = string.Empty;
                dr["LineItem"] = string.Empty;
                Session["NewStencilVarificationBatch"] = dt;
            }
        }

        [WebMethod]
        public static void SaveUser(List<Customer> customers)
        {


            DataTable dt = (DataTable)(HttpContext.Current.Session["NewStencilVarificationBatch"]);
            if (customers == null)
            {
                customers = new List<Customer>();
            }
            foreach (Customer c in customers)
            {

                DataRow dr = null;
                dr = dt.NewRow();
                dr["Codetype"] = c.types;
                dr["Code"] = c.code.ToUpper().Trim();
                dt.Rows.Add(dr);
                HttpContext.Current.Session["NewStencilVarificationBatch"] = dt;
                HttpContext.Current.Session["btnclickData"] = c.btnclickData;
                //Customer cs = new Customer();
                //cs.counts = c.counts;
                //cs.types = c.types;
                //cs.code = c.code;
                //cs1.Add(c);
            }
            //string a=user.UserName;
            //string b = user.batch;

        }
        public class Customer
        {
            public string counts { get; set; }
            public string types { get; set; }
            public string code { get; set; }
            public string btnclickData { get; set; }
        }
        public class Users
        {
            public string UserName { get; set; }
            public string batch { get; set; }

        }
    }
}