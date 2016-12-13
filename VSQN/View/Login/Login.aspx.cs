using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView.ActiveViewIndex = 0;
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 0;
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 1;
        }
    }
}