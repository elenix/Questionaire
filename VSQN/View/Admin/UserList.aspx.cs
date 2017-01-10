using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class UserList : Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection _con;
        private SqlDataAdapter _adapt;
        private SqlCommand _command;
        private DataTable _dt;
        private string _message;

        protected enum MessageType { Success, Error, Info, Warning }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && Session["user_role"].ToString() == "A")
            {
                if (!IsPostBack)
                {
                    if (Session["update_message"] != null)
                    {
                        _message = Session["update_message"].ToString();
                        ShowMessage(_message, MessageType.Info);
                        Session["update_message"] = null;
                    }
                    else if (Session["cancel_edit"] != null)
                    {
                        _message = Session["cancel_edit"].ToString();
                        ShowMessage(_message, MessageType.Warning);
                        Session["cancel_edit"] = null;
                    }

                    ShowData();
                }
            }
            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        }

        protected void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        private void ShowData()
        {
            _dt = new DataTable();
            _con = new SqlConnection(_cs);
            string pstringQuery = "Select email, Company, username from UserAuth where user_role = 'U' ";
            _con.Open();
            _command = new SqlCommand(pstringQuery, _con);
            _adapt = new SqlDataAdapter(_command);
            _command.ExecuteNonQuery();
            SqlDataReader dr = _command.ExecuteReader();
            _dt.Load(dr);

            ResultUserList.DataSource = _dt;
            ResultUserList.DataBind();
            ViewState["dirState"] = _dt;
            ViewState["sortdr"] = "Asc";

            _con.Close();   
        }

        protected void Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ResultUserList.PageIndex = e.NewPageIndex;
            ShowData();
        }

        protected void ResultUserList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["Edit_UserEmail"] = ((Label)ResultUserList.Rows[e.NewEditIndex].FindControl("lblemail")).Text;
            Response.Redirect("~/View/Admin/UserEdit.aspx");
        }

        protected void ResultUserList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label refEmail = ResultUserList.Rows[e.RowIndex].FindControl("lblemail") as Label;
            var email = refEmail?.Text;
            List<string> deleteQuery = new List<string>();

            deleteQuery.Add("Delete from UserAuth Where email = '" + email + "'");
            deleteQuery.Add("Delete from HRMS_User_Info Where User_Email = '" + email + "'");
            deleteQuery.Add("Delete from ESS_User_Info Where User_Email = '" + email + "'");
            deleteQuery.Add("Delete from HRSS_User_Info Where User_Email = '" + email + "'");
            deleteQuery.Add("Delete from SAAS_User_Info Where User_Email = '" + email + "'");

            if (email != null)
            {
                using (_con = new SqlConnection(_cs))
                {
                    foreach (string x in deleteQuery)
                    {
                        using (_command = new SqlCommand(x, _con))
                        {
                            _con.Open();
                            _command.ExecuteNonQuery();
                            _con.Close();
                        }
                    }
                }

                ShowData();
                ShowMessage("The user with email: <b>" + email + "</b> have been deleted.", MessageType.Error);
            }
        }

        protected void Result_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                }

                ResultUserList.DataSource = dtrslt;
                ResultUserList.DataBind();

            }

        }
    }
}