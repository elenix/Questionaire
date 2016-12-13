using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class UserSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                
            }
        }

        protected void LinkHRMS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            ButtonHRMS.CssClass = "btnsetup active";
            ButtonESS.CssClass = "btnsetup";
            ButtonHRSS.CssClass = "btnsetup";
            ButtonSAAS.CssClass = "btnsetup";
        }

        protected void LinkESS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            ButtonHRMS.CssClass = "btnsetup";
            ButtonESS.CssClass = "btnsetup active";
            ButtonHRSS.CssClass = "btnsetup";
            ButtonSAAS.CssClass = "btnsetup";
        }

        protected void LinkHRSS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            ButtonHRMS.CssClass = "btnsetup";
            ButtonESS.CssClass = "btnsetup";
            ButtonHRSS.CssClass = "btnsetup active";
            ButtonSAAS.CssClass = "btnsetup";
        }

        protected void LinkSAAS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            ButtonHRMS.CssClass = "btnsetup";
            ButtonESS.CssClass = "btnsetup";
            ButtonHRSS.CssClass = "btnsetup";
            ButtonSAAS.CssClass = "btnsetup active";
        }
    }
}