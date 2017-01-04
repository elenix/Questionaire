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
    public partial class QuestionList : Page
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
            if (Session["user_role"] != null)
            {
                if (!IsPostBack)
                {
                    if (Session["success_save"] != null)
                    {
                        _message = Session["success_save"].ToString();
                        ShowMessage(_message, MessageType.Success);
                        Session["success_save"] = null;
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
            var systemNo = Session["System"];
            var moduleNo = Session["Module"];

            string Query = "Select Ref_Code,Ques,Seq_Number from QuestionBank where System_FK = @system and Module_FK = @module ORDER BY Seq_Number ASC";

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(Query, _con))
                {
                    _command.Parameters.AddWithValue("@system", systemNo);
                    _command.Parameters.AddWithValue("@module", moduleNo);
                    _con.Open();
                    _dt = new DataTable();
                    _command.ExecuteNonQuery();
                    SqlDataReader dr = _command.ExecuteReader();
                    _dt.Load(dr);
                    _con.Close();
                    ResultQuestionList.DataSource = _dt;
                    ResultQuestionList.DataBind();
                }
            }
           
            ViewState["dirState"] = _dt;
            ViewState["sortdr"] = "Asc";
        }

        protected void ResultUserList_RowAnswering(object sender, GridViewEditEventArgs e)
        {
            Session["Table_Refcode"] = (ResultQuestionList.Rows[e.NewEditIndex].Cells[1]).Text;
            Response.Redirect("~/View/User/QuestionGenerator.aspx");
        }

        protected void Page_Back(object sender, EventArgs e)
        {
            string system = Session["System"].ToString();

            switch (system)
            {
                case "1":
                    Response.Redirect("~/View/User/UserHRMS.aspx");
                    break;

                case "2":
                    Response.Redirect("~/View/User/UserESS.aspx");
                    break;

                case "3":
                    Response.Redirect("~/View/User/UserHRSS.aspx");
                    break;

                default:
                    Response.Redirect("~/View/User/UserSAAS.aspx");
                    break;
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

                ResultQuestionList.DataSource = dtrslt;
                ResultQuestionList.DataBind();

            }
        }
    }
}