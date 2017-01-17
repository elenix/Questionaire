using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class ModuleList : Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection _con;
        private SqlCommand _command;
        private DataTable _dt;
        readonly List<int> _userIDModule = new List<int>();
        readonly List<string> _userModule = new List<string>();
        readonly List<int> _statusAnswer = new List<int>();
        readonly List<int> _totalQuestionModule = new List<int>();
        string _system = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && Session["user_role"].ToString() == "A")
            {
                LoadUserModule();

                if (!IsPostBack)
                {
                    SelectedSystem();

                    var company = Session["Answer_UserCompany"];
                    CompanyName.Text = "The list of available " + _system + "'s Module for <b>" + company.ToString() + "'s</b> company"; 
                    ExtractTotalQuestion();
                    ExtractStatus();
                    ShowData();
                }
            }

            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        } 

        private void SelectedSystem()
        {
            int system = Convert.ToInt32(Session["System_Selected"]);

            switch (system)
            {
                case 1:
                    _system = "HRMS";
                    break;
                case 2:
                    _system = "ESS";
                    break;
                case 3:
                    _system = "HRSS";
                    break;
                case 4:
                    _system = "SAAS";
                    break;
            }
        }

        private void ShowData()
        {
            _dt = new DataTable();
            string pstringQuery = "";
            int systemChoosed = Convert.ToInt32(Session["System_Selected"]);

            if (systemChoosed == 1)
            {
                pstringQuery = "Select Name from HRMS_module";
            }
            else if (systemChoosed == 2)
            {
                pstringQuery = "Select Name from ESS_module";
            }
            else if (systemChoosed == 3)
            {
                pstringQuery = "Select Name from HRSS_module";
            }
            else if (systemChoosed == 4)
            {
                pstringQuery = "Select Name from SAAS_module";
            }

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(pstringQuery, _con))
                {
                    _con.Open();
                    _dt = new DataTable();
                    _command.ExecuteNonQuery();
                    SqlDataReader dr = _command.ExecuteReader();
                    _dt.Load(dr);
                    _con.Close();
                    ResultModuleList.DataSource = _dt;
                    ResultModuleList.DataBind();
                }
            }

            ViewState["dirState"] = _dt;
            ViewState["sortdr"] = "Asc";
        }

        protected void LoadUserModule()
        {
            int systemChoosed = Convert.ToInt32(Session["System_Selected"]);
            var userEmail = Session["Answer_UserEmail"];
            string moduleIDQuery = "";
            string moduleNameQuery = "";

            if (systemChoosed == 1)
            {
                moduleIDQuery = "Select * from HRMS_User_Info where User_Email = @userEmail order by Module_number ASC";
                moduleNameQuery = "Select * from HRMS_module where PK = @id order by PK ASC";
            }
            else if (systemChoosed == 2)
            {
                moduleIDQuery = "Select * from ESS_User_Info where User_Email = @userEmail order by Module_number ASC";
                moduleNameQuery = "Select * from ESS_module where PK = @id order by PK ASC";
            }
            else if (systemChoosed == 3)
            {
                moduleIDQuery = "Select * from HRSS_User_Info where User_Email = @userEmail order by Module_number ASC";
                moduleNameQuery = "Select * from HRSS_module where PK = @id order by PK ASC";
            }
            else if (systemChoosed == 4)
            {
                moduleIDQuery = "Select * from SAAS_User_Info where User_Email = @userEmail order by Module_number ASC";
                moduleNameQuery = "Select * from SAAS_module where PK = @id order by PK ASC";
            }

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(moduleIDQuery, _con))
                {
                    _command.Parameters.AddWithValue("@userEmail", userEmail);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _userIDModule.Add(reader.GetInt32(3));
                    }
                    _con.Close();
                }

                foreach(int x in _userIDModule)
                {
                    using (_command = new SqlCommand(moduleNameQuery, _con))
                    {
                        _command.Parameters.AddWithValue("@id", x);
                        _con.Open();
                        _command.ExecuteNonQuery();
                        var reader = _command.ExecuteReader();

                        while (reader.Read())
                        {
                            _userModule.Add(reader.GetString(1));
                        }
                        _con.Close();
                    }
                }   
            }
        }

        private void ExtractTotalQuestion()
        {
            var systemNo = Session["System_Selected"];

            string Query = "Select Module_FK from QuestionBank where System_FK = @system ORDER BY Module_FK ASC";

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(Query, _con))
                {
                    _command.Parameters.AddWithValue("@system", systemNo);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _totalQuestionModule.Add(reader.GetInt32(0));
                    }
                    _con.Close();
                }
            }
        }

        private void ExtractStatus()
        {
            var systemNo = Session["System_Selected"]; ;
            var userEmail = Session["Answer_UserEmail"]; ;

            string textQuery = "Select [Module_FK] from User_Answer_Text where [System_FK] = @system and [user_email] = @email order by Module_FK ASC";
            string optionQuery = "Select distinct [ref_code], [Module_FK] from User_Answer_Option where [System_FK] = @system and [user_email] = @email order by Module_FK ASC";
            string attachmentQuery = "Select [Module_FK] from User_Attachment where [System_FK] = @system and [user_email] = @email order by Module_FK ASC";

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(textQuery, _con))
                {
                    _command.Parameters.AddWithValue("@system", systemNo);
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
                    _command.Parameters.AddWithValue("@system", systemNo);
                    _command.Parameters.AddWithValue("@email", userEmail);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _statusAnswer.Add(reader.GetInt32(1));
                    }
                    _con.Close();
                }

                using (_command = new SqlCommand(attachmentQuery, _con))
                {
                    _command.Parameters.AddWithValue("@system", systemNo);
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

        static int Find_Total(List<int> list, int valueToFind)
        {
            return ((from temp in list where temp.Equals(valueToFind) select temp).Count());
        }

        protected void ResultModuleList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Visible = false;

                int count = 0;

                foreach (string x in _userModule)
                {
                    if (e.Row.Cells[0].Text == x)
                    {
                        int total = Find_Total(_totalQuestionModule, _userIDModule[count]);
                        int answered = Find_Total(_statusAnswer, _userIDModule[count]);
                        e.Row.Visible = true;
                        e.Row.Cells[1].Text = answered + "/" + total;
                    }

                    count++;
                }
            }
        }

        protected void ResultModuleList_RowAnswer(object sender, GridViewEditEventArgs e)
        {
            int count = 0;
            var module = (ResultModuleList.Rows[e.NewEditIndex].Cells[0]).Text;

            foreach (var x in _userModule)
            {
                if (module == x)
                {
                    Session["Choosed_Module"] = _userIDModule[count];
                    Response.Redirect("~/View/Admin/AnswerList.aspx");    
                }

                count++;
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

                ResultModuleList.DataSource = dtrslt;
                ResultModuleList.DataBind();
            }
        }

        protected void Change_Page(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Admin/AnswerBank.aspx");
        }
    }
}