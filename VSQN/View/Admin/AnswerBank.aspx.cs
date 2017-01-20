using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View
{
    public partial class AnswerBank : Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection _con;
        private SqlDataAdapter _adapt;
        private SqlCommand _command;
        private DataTable _dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSystemListDropwdown();
            }
        }

        private void LoadSystemListDropwdown() //load system list from System_List.dbo
        {
            _con = new SqlConnection(_cs);
            const string com = "Select * from System_List"; 
            _adapt = new SqlDataAdapter(com, _con);
            _dt = new DataTable();
            _adapt.Fill(_dt);
            SystemList.DataSource = _dt;
            SystemList.DataBind();
            SystemList.DataTextField = "Name";
            SystemList.DataValueField = "ID";
            SystemList.DataBind();
            SystemList.Items.Insert(0, new ListItem("--Select--", "0"));

            if(Session["System_Selected"] != null)
            {
                int x = Convert.ToInt32(Session["System_Selected"]);
                SystemList.SelectedIndex = x;
            }

            ShowData();
        }

        protected void SystemList_SelectedIndexChanged(object sender, EventArgs e) //trigered when system list menu is selected.
        { 
            ShowData();
        }

        private void ShowData() // select single data in (HRMS/ESS/HRSS/SAAS)_User_Info.dbo
        {
            _dt = new DataTable();
            string query = "SELECT DISTINCT User_Email,Company FROM HRMS_User_Info where Company = 'nonedattooo';";

            if (SystemList.SelectedIndex == 1)
            {
                query = "SELECT DISTINCT User_Email,Company FROM HRMS_User_Info; ";
            }
            else if (SystemList.SelectedIndex == 2)
            {
                query = "SELECT DISTINCT User_Email,Company FROM ESS_User_Info; ";
            }
            else if (SystemList.SelectedIndex == 3)
            {
                query = "SELECT DISTINCT User_Email,Company FROM HRSS_User_Info; ";
            }
            else if (SystemList.SelectedIndex == 4)
            {
                query = "SELECT DISTINCT User_Email,Company FROM SAAS_User_Info; ";
            }
            
            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(query, _con))
                {
                    _con.Open();
                    _dt = new DataTable();
                    _command.ExecuteNonQuery();
                    SqlDataReader dr = _command.ExecuteReader();
                    _dt.Load(dr);
                    _con.Close();
                    ResultAnswerList.DataSource = _dt;
                    ResultAnswerList.DataBind();
                }
            }

            ViewState["dirState"] = _dt;
            ViewState["sortdr"] = "Asc";
        }

        protected void Result_PageIndexChanging(object sender, GridViewPageEventArgs e) //gridview page index
        {
            ResultAnswerList.PageIndex = e.NewPageIndex;
            ShowData();
        }

        protected void ResultUserList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["Answer_UserEmail"] = null;
            Session["Answer_UserCompany"] = null;
            Session["System_Selected"] = null;

            Session["Answer_UserEmail"] = ((Label)ResultAnswerList.Rows[e.NewEditIndex].FindControl("lblemail")).Text; //for use in ModuleList.dbo
            Session["Answer_UserCompany"] = ((Label)ResultAnswerList.Rows[e.NewEditIndex].FindControl("lblCompany")).Text; //for use in ModuleList.dbo
            Session["System_Selected"] = SystemList.SelectedIndex;

            Response.Redirect("~/View/Admin/ModuleList.aspx");
        }

        protected void Result_Sorting(object sender, GridViewSortEventArgs e) //gridview sorting
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

                ResultAnswerList.DataSource = dtrslt;
                ResultAnswerList.DataBind();

            }

        } 
    }
}