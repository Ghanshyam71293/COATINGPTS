﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PTSCOATING.pts.FINALINSPECTION.ParkList
{
    public partial class Park : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("../../../login.aspx");
            }

            litusername.Text = Convert.ToString(Session["UserId"]);
        }
    }
}