﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VSQN.App_Code;

namespace VSQN.View.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["username"] != null)
                    {
                        string username = HttpContext.Current.Session["username"].ToString();
                        welcomemsg.Text = "Welcome, " + (char.ToUpper(username[0]) + username.Substring(1));
                    }
                }
            }
            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        }

        protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
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