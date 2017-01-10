using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class AnswerGenerator : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection _con;
        DataTable _dt;
        SqlCommand _command;
        DataTable RBTable = new DataTable();
        DataTable CBTable = new DataTable();
        int _typeOfInput;
        string _fieldTypeEdit;
        string _textAnswer;
        List<string> _optionAnswerText = new List<string>();
        List<int> _optionAnswerID = new List<int>();
        List<int> _checkedOption = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && Session["user_role"].ToString() == "A")
            {
                Load_Question();

                if (!IsPostBack)
                {
                    ExtractUserTextAnswer();
                    AnswerTextField();
                }

                AnswerOptionField();
            }

            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        }

        protected void Change_Page(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Admin/AnswerList.aspx");
        }

        protected void Load_Question()
        {
            string query = "ExtractQuestionData";
            string TextData = "Select * from Question_Answer_TextType where Ref_FK = @Ref_Cod ";
            string OptionData = "Select * from Question_Answer_OptionType where Ref_FK = @Ref_Cod ";
            var refcode = Session["Table_answerRefcode"];

            //Extract Data from QuestionInfo Table
            using (_con = new SqlConnection(cs))
            {
                using (_command = new SqlCommand(query, _con))
                {
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                    _con.Open();
                    DataTable data = new DataTable();
                    _command.ExecuteNonQuery();
                    SqlDataReader dr = _command.ExecuteReader();
                    data.Load(dr);

                    foreach (DataRow row in data.Rows)
                    {
                        ques_seq.Text = "<h3><b>QUESTION " + row["Seq_Number"].ToString() + ":</b></h3>";
                        QuestionGenerate.Text = row["Ques"].ToString();
                        _typeOfInput = Int32.Parse(row["In_Type_FK"].ToString());
                    }
                    _con.Close();
                }

                if (_typeOfInput == 1 || _typeOfInput == 2) //Extract Question Answer with text type value from  Question_Answer_TextType table
                {
                    using (_command = new SqlCommand(TextData, _con))
                    {
                        _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                        _con.Open();
                        DataTable data = new DataTable();
                        _command.ExecuteNonQuery();
                        SqlDataReader dr = _command.ExecuteReader();
                        data.Load(dr);

                        foreach (DataRow row in data.Rows)
                        {
                            _textAnswer = row["Ans_Default"].ToString();
                        }
                    }
                    _con.Close();
                }

                else
                {
                    using (_command = new SqlCommand(OptionData, _con))
                    {
                        _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                        _con.Open();
                        _dt = new DataTable();
                        _command.ExecuteNonQuery();
                        SqlDataReader reader = _command.ExecuteReader();

                        while (reader.Read())
                        {
                            _optionAnswerID.Add(reader.GetInt32(0));
                            _optionAnswerText.Add(reader.GetString(2));
                        }
                    }
                    _con.Close();
                }
            }
        }

        protected void ExtractUserTextAnswer()
        {
            string TextQuery = "Select * from User_Answer_Text where user_email = @email and ref_code = @Ref_Cod";
            var refcode = Session["Table_answerRefcode"];
            var email = Session["Answer_UserEmail"];

            using (_con = new SqlConnection(cs))
            {
                switch (_typeOfInput)
                {
                    case 1:
                        using (_command = new SqlCommand(TextQuery, _con))
                        {
                            _command.Parameters.AddWithValue("@email", email);
                            _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                            _con.Open();
                            DataTable data = new DataTable();
                            _command.ExecuteNonQuery();
                            SqlDataReader dr = _command.ExecuteReader();
                            data.Load(dr);

                            foreach (DataRow row in data.Rows)
                            {
                                TBUserAnswerBox.Text = row["answer_text"].ToString();
                            }

                            _con.Close();
                        }

                        break;

                    default:
                        using (_command = new SqlCommand(TextQuery, _con))
                        {
                            _command.Parameters.AddWithValue("@email", email);
                            _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                            _con.Open();
                            DataTable data = new DataTable();
                            _command.ExecuteNonQuery();
                            SqlDataReader dr = _command.ExecuteReader();
                            data.Load(dr);

                            foreach (DataRow row in data.Rows)
                            {
                                MMUserAnswerBox.Text = row["answer_text"].ToString();
                            }

                            _con.Close();
                        }

                        break;
                }

            }
        }

        protected void ExtractUserOptionAnswer()
        {
            string OptionQuery = "Select * from User_Answer_Option where user_email = @email and ref_code = @Ref_Cod ORDER BY answer_ID ASC";
            var refcode = Session["Table_answerRefcode"];
            var email = Session["Answer_UserEmail"];

            using (_con = new SqlConnection(cs))
            {
                using (_command = new SqlCommand(OptionQuery, _con))
                {
                    _command.Parameters.AddWithValue("@email", email);
                    _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _checkedOption.Add(reader.GetInt32(3));
                    }
                    _con.Close();
                }
            }
        }

        private void AnswerTextField()
        {
            //For NULL value
            if (_typeOfInput == 0)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
            }

            //For Question with Text Box Answer Data
            else if (_typeOfInput == 1)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
          
            }

            //For Question with Memo Answer Data
            else if (_typeOfInput == 2)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
            }
        }

        private void AnswerOptionField()
        {
            //For NULL value
            if (_typeOfInput == 0)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
            }

            //For Question with Rudio Button Answer Data
            else if (_typeOfInput == 3)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                ExtractUserOptionAnswer();
                LoadRBPanel();
            }

            //For Question with Check Box Answer Data
            else if (_typeOfInput == 4)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                ExtractUserOptionAnswer();
                LoadCBPanel();
            }
        }

        private void LoadRBPanel()
        {
            var t = 1;
            var c = 0;

            foreach (var x in _optionAnswerText)
            {
                panelRB.Controls.Add(new LiteralControl(" <div class='form-inline'>"));
                panelRB.Controls.Add(new LiteralControl("<div class='form-group'>"));

                var RBTemp = new RadioButton { ID = _optionAnswerID[t - 1].ToString() };
                RBTemp.CssClass = "radio";
                RBTemp.GroupName = "radioGroup";
                RBTemp.Enabled = false;

                var labelTemp = new Label
                {
                    ID = "lblRB" + t,
                    Text = x
                };

                panelRB.Controls.Add(new LiteralControl("<label class='form-check-label'>"));

                if (c < _checkedOption.Count && RBTemp.ID == _checkedOption[c].ToString())
                {
                    RBTemp.Checked = true;
                    panelRB.Controls.Add(RBTemp);
                    c++;
                }
                else
                {
                    panelRB.Controls.Add(RBTemp);
                }

                panelRB.Controls.Add(new LiteralControl("<a> </a>"));
                panelRB.Controls.Add(labelTemp);
                panelRB.Controls.Add(new LiteralControl("</label>"));

                t++;

                panelRB.Controls.Add(new LiteralControl("</div>"));
                panelRB.Controls.Add(new LiteralControl("</div>"));
            }
        }

        private void LoadCBPanel()
        {
            var t = 1;
            var c = 0;

            foreach (var x in _optionAnswerText)
            {
                panelCB.Controls.Add(new LiteralControl(" <div class='form-inline'>"));
                panelCB.Controls.Add(new LiteralControl("<div class='form-group'>"));

                var CBTemp = new CheckBox { ID = _optionAnswerID[t - 1].ToString() };
                CBTemp.CssClass = "checkbox";
                CBTemp.Enabled = false;

                var labelTemp = new Label
                {
                    ID = "lblCB" + t,
                    Text = x
                };

                panelCB.Controls.Add(new LiteralControl("<label class='form-check-label'>"));

                if (c < _checkedOption.Count && CBTemp.ID == _checkedOption[c].ToString())
                {
                    CBTemp.Checked = true;
                    panelCB.Controls.Add(CBTemp);
                    c++;
                }
                else
                {
                    panelCB.Controls.Add(CBTemp);
                }

                panelCB.Controls.Add(new LiteralControl("<a> </a>"));
                panelCB.Controls.Add(labelTemp);
                panelCB.Controls.Add(new LiteralControl("</label>"));

                t++;

                panelCB.Controls.Add(new LiteralControl("</div>"));
                panelCB.Controls.Add(new LiteralControl("</div>"));
            }
        }
    }
}