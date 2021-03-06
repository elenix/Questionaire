﻿using System;
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
    public partial class HRSS : Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection _con;
        private SqlDataAdapter _adapt;
        private SqlCommand _command;
        private DataTable _dt;
        readonly List<int> _idHrsSmodule = new List<int>();
        readonly List<string> _UserHrsSmodule = new List<string>();
        readonly List<int> _statusAnswer = new List<int>();
        readonly List<int> _totalQuestionModule = new List<int>();
        //private string _message;

        protected enum MessageType { Success, Error, Info, Warning }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && Session["user_role"].ToString() == "U")
            {
                LoadUserHRSSmodule();

                if (!IsPostBack)
                {
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

        protected void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        private void ShowData()
        {
            _dt = new DataTable();
            _con = new SqlConnection(_cs);
            string pstringQuery = "Select PK, Name from HRSS_module";
            _con.Open();
            _command = new SqlCommand(pstringQuery, _con);
            _adapt = new SqlDataAdapter(_command);
            _command.ExecuteNonQuery();
            SqlDataReader dr = _command.ExecuteReader();
            _dt.Load(dr);
            _con.Close();
            ResultHRSSList.DataSource = _dt;
            ResultHRSSList.DataBind();
            ViewState["dirState"] = _dt;
            ViewState["sortdr"] = "Asc";
        }

        protected void LoadUserHRSSmodule()
        {
            const string userHrms = "Select * from HRSS_User_Info where User_Email = @userEmail order by Module_number ASC";
            const string moduleNameQuery = "Select * from HRSS_module where PK = @id order by PK ASC";
            var userEmail = Session["user_email"];

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(userHrms, _con))
                {
                    _command.Parameters.AddWithValue("@userEmail", userEmail);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _idHrsSmodule.Add(reader.GetInt32(3));
                    }
                    _con.Close();
                }

                foreach (int x in _idHrsSmodule)
                {
                    using (_command = new SqlCommand(moduleNameQuery, _con))
                    {
                        _command.Parameters.AddWithValue("@id", x);
                        _con.Open();
                        _command.ExecuteNonQuery();
                        var reader = _command.ExecuteReader();

                        while (reader.Read())
                        {
                            _UserHrsSmodule.Add(reader.GetString(1));
                        }
                        _con.Close();
                    }
                }
            }
        }

        private void ExtractTotalQuestion()
        {
            var systemNo = 3;

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
            var systemNo = 3;
            var userEmail = Session["user_email"];

            string textQuery = "Select [Module_FK] from User_Answer_Text where [System_FK] = @system and [user_email] = @email order by Module_FK ASC";
            string attachmentQuery = "Select [Module_FK] from User_Attachment where [System_FK] = @system and [user_email] = @email order by Module_FK ASC";
            string optionQuery = "Select distinct [ref_code], [Module_FK] from User_Answer_Option where [System_FK] = @system and [user_email] = @email order by Module_FK ASC";

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

            }
        }

        static int Find_Total(List<int> list, int valueToFind)
        {
            return ((from temp in list where temp.Equals(valueToFind) select temp).Count());
        }

        protected void ResultUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Visible = false;
                int count = 0;

                foreach (string x in _UserHrsSmodule)
                {
                    if (e.Row.Cells[0].Text == x)
                    {
                        int total = Find_Total(_totalQuestionModule, _idHrsSmodule[count]);
                        int answered = Find_Total(_statusAnswer, _idHrsSmodule[count]);
                        e.Row.Visible = true;
                        e.Row.Cells[1].Text = answered + "/" + total;
                    }

                    count++;
                }
            }
        }

        protected void ResultUserList_RowAnswering(object sender, GridViewEditEventArgs e)
        {
            int count = 0;
            var module = (ResultHRSSList.Rows[e.NewEditIndex].Cells[0]).Text;

            foreach (var x in _UserHrsSmodule)
            {
                if (module == x)
                {
                    Session["System"] = "3";
                    Session["Module"] = _idHrsSmodule[count];
                    Response.Redirect("~/View/User/QuestionList.aspx");
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

                ResultHRSSList.DataSource = dtrslt;
                ResultHRSSList.DataBind();

            }
        }
    }
}
