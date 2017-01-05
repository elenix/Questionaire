using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.User
{
    public partial class QuestionGenerator : Page
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

        private enum MessageType { Error };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && Session["user_role"].ToString() == "U")
            {
                RBTable.Columns.Add("RB_BOX");
                CBTable.Columns.Add("CB_BOX");
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
            Response.Redirect("~/View/User/QuestionList.aspx");
        }

        protected void Load_Question()
        {
            string query = "ExtractQuestionData";
            string TextData = "Select * from Question_Answer_TextType where Ref_FK = @Ref_Cod ";
            string OptionData = "Select * from Question_Answer_OptionType where Ref_FK = @Ref_Cod ";
            var refcode = Session["Table_Refcode"];


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
                            _fieldTypeEdit = row["Field_Type"].ToString();
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
            var refcode = Session["Table_Refcode"];
            var email = Session["user_email"];

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
                                Session["_UserTextAnswer"] = row["answer_text"].ToString();
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
                                Session["_UserTextAnswer"] = row["answer_text"].ToString();
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
            var refcode = Session["Table_Refcode"];
            var email = Session["user_email"];

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
                switch (_fieldTypeEdit)
                {
                    case "1":
                        TBLabel.Text = "<small>* Please enter in <u>Text</u> only *</small>";
                        break;

                    default:
                        TBLabel.Text = "<small>* Please enter in <u>Numbers</u> only *</small>";
                        break;
                }
            }

            //For Question with Memo Answer Data
            else if (_typeOfInput == 2)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                switch (_fieldTypeEdit)
                {
                    case "1":
                        MMLabel.Text = "<small>* Please enter in <u>Text</u> only *</small>";
                        break;

                    default:
                        MMLabel.Text = "<small>* Please enter in <u>Numbers</u> only *</small>";
                        break;
                }
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

        protected void Default_Answer(object sender, EventArgs e)
        {
            switch (_typeOfInput)
            {
                case 1:
                    TBUserAnswerBox.Text = _textAnswer;
                    break;

                default:
                    MMUserAnswerBox.Text = _textAnswer;
                    break;
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

        private void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        protected void Save_Answer(object sender, EventArgs e)
        {
            string OneSaveQuery = "INSERT INTO User_Answer_Text(user_email,ref_code,answer_text) VALUES (@email,@ref_code,@answer_text)";
            string updateQuery = "update User_Answer_Text set answer_text = @answer_text where user_email = @email and ref_code = @ref_code";
            string MultiSaveQuery = "INSERT INTO User_Answer_Option(user_email,ref_code,answer_ID) VALUES (@email,@ref_code,@answer_ID)";
            string MultiDeleteQuery = "delete from User_Answer_Option where user_email = @email and answer_ID = @answer_ID ";
            var refcode = Session["Table_Refcode"];
            var email = Session["user_email"];

            using (_con = new SqlConnection(cs))
            {
                switch (_typeOfInput)
                {
                    case 1:

                        #region Textbox validation

                        if(Session["_UserTextAnswer"] != null)
                        {
                            using (_command = new SqlCommand(updateQuery, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@email", email);
                                _command.Parameters.AddWithValue("@ref_code", refcode);
                                _command.Parameters.AddWithValue("@answer_text", TBUserAnswerBox.Text);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }
                        }
                        else
                        {
                            using (_command = new SqlCommand(OneSaveQuery, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@email", email);
                                _command.Parameters.AddWithValue("@ref_code", refcode);
                                _command.Parameters.AddWithValue("@answer_text", TBUserAnswerBox.Text);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }
                        }

                        Session["_UserTextAnswer"] = null;

                        break;

                    #endregion

                    case 2:

                        #region Memobox validation

                        if (Session["_UserTextAnswer"] != null)
                        {
                            using (_command = new SqlCommand(updateQuery, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@email", email);
                                _command.Parameters.AddWithValue("@ref_code", refcode);
                                _command.Parameters.AddWithValue("@answer_text", MMUserAnswerBox.Text);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }
                        }
                        else
                        {
                            using (_command = new SqlCommand(OneSaveQuery, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@email", email);
                                _command.Parameters.AddWithValue("@ref_code", refcode);
                                _command.Parameters.AddWithValue("@answer_text", MMUserAnswerBox.Text);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }
                        }

                        Session["_UserTextAnswer"] = null;

                        break;

                    #endregion

                    case 3:

                        #region radiobutton validation
                        List<int> checkedRB = new List<int>();
                        List<int> uncheckedRB = new List<int>();

                        foreach (var child in panelRB.Controls.OfType<RadioButton>())
                        {
                            if (child.Checked)
                            {
                                int id = Convert.ToInt32(child.ID.Trim());
                                checkedRB.Add(id);
                            }
                            else
                            {
                                int id = Convert.ToInt32(child.ID.Trim());
                                uncheckedRB.Add(id);
                            }
                        }

                        if (checkedRB.Count == 0)
                        {
                            ShowMessage("Please choose <b>one</b> of the answer.", MessageType.Error);
                        }
                        else
                        {
                            using (_command = new SqlCommand(MultiSaveQuery, _con))
                            {
                                foreach (int c in checkedRB)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@email", email);
                                    _command.Parameters.AddWithValue("@ref_code", refcode);
                                    _command.Parameters.AddWithValue("@answer_ID", c);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }

                            using (_command = new SqlCommand(MultiDeleteQuery, _con))
                            {
                                foreach (int x in uncheckedRB)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@email", email);
                                    _command.Parameters.AddWithValue("@answer_ID", x);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }

                        }

                        break;

                    #endregion

                    default:

                        #region checkbox validation

                        List<int> checkedCB = new List<int>();
                        List<int> uncheckedCB = new List<int>();

                        foreach (var child in panelCB.Controls.OfType<CheckBox>())
                        {
                            if (child.Checked)
                            {
                                int id = Convert.ToInt32(child.ID.Trim());
                                checkedCB.Add(id);
                            }
                            else
                            {
                                int id = Convert.ToInt32(child.ID.Trim());
                                uncheckedCB.Add(id);
                            }
                        }

                        List<int> newDifference = checkedCB.Except(_checkedOption).ToList();
                        List<int> oldDifference = uncheckedCB.Except(checkedCB).ToList();

                        if (newDifference != null)
                        {
                            using (_command = new SqlCommand(MultiSaveQuery, _con))
                            {
                                foreach (int c in newDifference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@email", email);
                                    _command.Parameters.AddWithValue("@ref_code", refcode);
                                    _command.Parameters.AddWithValue("@answer_ID", c);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }    
                        }
                        else
                        {
                            ShowMessage("Please choose <b>atlease one</b> of the answer.", MessageType.Error);
                        }

                        if (oldDifference != null)
                        { 
                            using (_command = new SqlCommand(MultiDeleteQuery, _con))
                            {
                                foreach (int x in oldDifference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@email", email);
                                    _command.Parameters.AddWithValue("@answer_ID", x);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }
                        }

                        break;

                        #endregion
                }
            }

            Session["success_save"] = "Your answer have been saved!";
            Response.Redirect("QuestionList.aspx");
        }
    }
}