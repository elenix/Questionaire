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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null)
            {
                RBTable.Columns.Add("RB_BOX");
                CBTable.Columns.Add("CB_BOX");

                Load_Question();

                if (!IsPostBack)
                {
                    AnswerField();
                }
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

        private void AnswerField()
        {
            //For NULL value
            if(_typeOfInput == 0)
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

            //For Question with Rudio Button Answer Data
            else if (_typeOfInput == 3)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                LoadRBPanel();
            }

            //For Question with Check Box Answer Data
            else if (_typeOfInput == 4)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
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

            foreach (var t1 in _optionAnswerText)
            {
                panelRB.Controls.Add(new LiteralControl(" <div class='form-inline'>"));
                panelRB.Controls.Add(new LiteralControl("<div class='form-group'>"));

                var RBTemp = new RadioButton { ID = _optionAnswerID[t - 1].ToString() };

                var labelTemp = new Label
                {
                    ID = "lblRB" + t,
                    Text = _optionAnswerText[t - 1]
                };

                panelRB.Controls.Add(new LiteralControl("<label class='form-check-label'>"));
                panelRB.Controls.Add(RBTemp);
                panelRB.Controls.Add(new LiteralControl("<a>   </a>"));
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

            foreach (var t1 in _optionAnswerText)
            {
                panelCB.Controls.Add(new LiteralControl(" <div class='form-inline'>"));
                panelCB.Controls.Add(new LiteralControl("<div class='form-group'>"));

                var RBTemp = new CheckBox { ID = _optionAnswerID[t - 1].ToString() };

                var labelTemp = new Label
                {
                    ID = "lblCB" + t,
                    Text = _optionAnswerText[t - 1]
                };

                panelCB.Controls.Add(new LiteralControl("<label class='form-check-label'>"));
                panelCB.Controls.Add(RBTemp);
                panelCB.Controls.Add(new LiteralControl("<a>   </a>"));
                panelCB.Controls.Add(labelTemp);
                panelCB.Controls.Add(new LiteralControl("</label>"));

                t++;

                panelCB.Controls.Add(new LiteralControl("</div>"));
                panelCB.Controls.Add(new LiteralControl("</div>"));
            }
        }
    }
}