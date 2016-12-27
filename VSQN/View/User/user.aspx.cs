using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.User
{
    public partial class user : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] != null)
            {
                string username = Session["username"].ToString();
                //panelWelcome.Controls.Add(new LiteralControl("<h2>WELCOME"+username+ "</h2>"));
                welcomemsg.Text = "Welcome, " + (char.ToUpper(username[0]) + username.Substring(1)); ;

            }
        }
    }
}