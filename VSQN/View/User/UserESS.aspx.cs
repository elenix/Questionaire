using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.User
{
    public partial class UserESS : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                ShowData();
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
            string pstringQuery = "Select PK, Name from ESS_module";
            _con.Open();
            _command = new SqlCommand(pstringQuery, _con);
            _adapt = new SqlDataAdapter(_command);
            _command.ExecuteNonQuery();
            SqlDataReader dr = _command.ExecuteReader();
            _dt.Load(dr);

            ResultESSList.DataSource = _dt;
            ResultESSList.DataBind();
            ViewState["dirState"] = _dt;
            ViewState["sortdr"] = "Asc";

            _con.Close();
        }

        protected void Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ResultESSList.PageIndex = e.NewPageIndex;
            ShowData();
        }

        protected void ResultUserList_RowAnswering(object sender, GridViewEditEventArgs e)
        {

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

                ResultESSList.DataSource = dtrslt;
                ResultESSList.DataBind();

            }
        }
    }
}