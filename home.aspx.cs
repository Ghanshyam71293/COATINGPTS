using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null || Session["UserId"] == "")
                {
                    Response.Redirect("login.aspx");
                }

                litusername.Text = Convert.ToString(Session["UserId"]);
            }
        }
    }
}