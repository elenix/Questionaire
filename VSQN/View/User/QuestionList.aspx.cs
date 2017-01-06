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
        readonly List<int> _statusAnswer = new List<int>();

        protected enum MessageType { Success, Error, Info, Warning }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && Session["user_role"].ToString() == "U")
            {
                if (!IsPostBack)
                {
                    if (Session["success_save"] != null)
                    {
                        _message = Session["success_save"].ToString();
                        ShowMessage(_message, MessageType.Success);
                        Session["success_save"] = null;
                    }

                    ExtractStatus();
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

        private void ExtractStatus()
        {
            var userEmail = Session["user_email"];

            string textQuery = "Select ref_code from User_Answer_Text where user_email = @email order by ID ASC";
            string optionQuery = "Select ref_code from User_Answer_Option where user_email = @email order by ID ASC";

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(textQuery, _con))
                {
                    _command.Parameters.AddWithValue("@email", userEmail);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _statusAnswer.Add(reader.GetInt32(0));
                    }
                    _con.Close();
                }

                using (_command = new SqlCommand(optionQuery, _con))
                {
                    _command.Parameters.AddWithValue("@email", userEmail);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _statusAnswer.Add(reader.GetInt32(0));
                    }
                    _con.Close();
                }

            }
        }

        protected void ResultQuestionList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!_statusAnswer.Any())
                {
                    e.Row.Cells[3].Text = "<span class='glyphicon glyphicon-remove text-danger' aria-hidden='true' />";
                }
                else
                {
                    int referenceCode = Convert.ToInt32(e.Row.Cells[1].Text);

                    if (_statusAnswer.Contains(referenceCode))
                    {
                        e.Row.Cells[3].Text = "<span class='glyphicon glyphicon-ok text-success' aria-hidden='true' />";
                    }
                    else
                    {
                        e.Row.Cells[3].Text = "<span class='glyphicon glyphicon-remove text-danger' aria-hidden='true' />"; 
                    }
                }
            }
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