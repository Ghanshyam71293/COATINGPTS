using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING.pts.MECHANICALLOGBOOK
{
    public partial class MECHANICALLOGBOOK : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("../../login.aspx");
                }

                litusername.Text = Convert.ToString(Session["UserId"]);
            }
        }
    }
}