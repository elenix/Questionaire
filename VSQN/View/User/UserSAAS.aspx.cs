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
    public partial class SAAS : System.Web.UI.Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection _con;
        private SqlDataAdapter _adapt;
        private SqlCommand _command;
        private DataTable _dt;
        List<int> _UserSaaSmodule = new List<int>();
        List<string> _UserSaaSmoduleString = new List<string>();
        //private string _message;

        protected enum MessageType { Success, Error, Info, Warning }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserSAASmodule();
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
            string pstringQuery = "Select PK, Name from SAAS_module";
            _con.Open();
            _command = new SqlCommand(pstringQuery, _con);
            _adapt = new SqlDataAdapter(_command);
            _command.ExecuteNonQuery();
            SqlDataReader dr = _command.ExecuteReader();
            _dt.Load(dr);
            _con.Close();
            ResultSAASList.DataSource = _dt;
            ResultSAASList.DataBind();
            ViewState["dirState"] = _dt;
            ViewState["sortdr"] = "Asc";
        }

        protected void LoadUserSAASmodule()
        {
            const string userSaas = "Select * from SAAS_User_Info where User_Email = @userEmail";
            var userEmail = Session["user_email"];

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(userSaas, _con))
                {
                    _command.Parameters.AddWithValue("@userEmail", userEmail);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _UserSaaSmodule.Add(reader.GetInt32(2));
                    }
                    _con.Close();
                }
            }

            SortData();
        }

        public void SortData()
        {
            _UserSaaSmodule = _UserSaaSmodule.OrderBy(i => i).ToList();
            _UserSaaSmoduleString = _UserSaaSmodule.ConvertAll<string>(delegate (int i) { return i.ToString(); });
        }

        protected void ResultUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (GridViewRow grv in ResultSAASList.Rows)
                {
                    e.Row.Visible = false;
                }

                foreach (string x in _UserSaaSmoduleString)
                {
                    if (e.Row.Cells[0].Text == x)
                    {
                        e.Row.Visible = true;
                    }
                }
            }

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

                ResultSAASList.DataSource = dtrslt;
                ResultSAASList.DataBind();

            }


        }
    }
}