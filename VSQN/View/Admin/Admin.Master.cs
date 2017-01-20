using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && (Session["user_role"].ToString() == "A" || Session["user_role"].ToString() == "M")) //validation
            {
                if (!IsPostBack)
                {
                    if (Session["username"] != null)
                    {
                        string username = HttpContext.Current.Session["username"].ToString();
                        welcomemsg.Text = "Welcome, " + (char.ToUpper(username[0]) + username.Substring(1)); //welcome msg on top of page
                    }
                }
            }

            else
            {
               Response.Redirect("~/View/Login/Login.aspx");
            }               
        }

        protected void OnMenuItemDataBound(object sender, MenuEventArgs e) //to active the menu. node can be change in Web.sitemap
        {
            if (SiteMap.CurrentNode != null)
            {
                if (e.Item.Text == SiteMap.CurrentNode.Title)
                {
                    if (e.Item.Parent != null)
                    {
                        e.Item.Parent.Selected = true;
                    }
                    else
                    {
                        e.Item.Selected = true;
                    }
                }
            }
        }

        
    }
}
