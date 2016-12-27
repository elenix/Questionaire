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
    public partial class EditQuestion : Page
    {
        string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection _con;
        SqlDataAdapter _adapt;
        DataTable _dt;
        SqlCommand _command;
        DataTable RBTable = new DataTable();
        DataTable CBTable = new DataTable();
        int _typeOfInput;
        int _systemChoose;
        int _moduleChoose;
        string _fieldTypeEdit;
        string _textAnswer;
        List<string> _optionAnswer      = new List<string>();
        List<string> AnswerOptionUpdate = new List<string>();
        string queryDelete = "DeleteQuestionAnswerOption";
        string queryOption = "AddQuestionAnswerOption";
        string queryText   = "AddQuestionAnswerForText";

        protected enum MessageType { Success, Error, Info, Warning }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null)
            {
                RBTable.Columns.Add("RB_BOX");
                CBTable.Columns.Add("CB_BOX");

                if (!IsPostBack)
                {
                    if (Session["Row_Ref_Code"] != null)
                    {
                        AutoGenerateEdit.Text = Session["Row_Ref_Code"].ToString();
                    }

                    ExtractData();
                    LoadSystemListDropwdown();
                    AnswerField();

                }
            }
            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        }

        private void ExtractData()
        {
            _con = new SqlConnection(cs);
            string query = "ExtractQuestionData";
            string TextData = "Select * from Question_Answer_TextType where Ref_FK = @Ref_Cod ";
            string OptionData = "Select * from Question_Answer_OptionType where Ref_FK = @Ref_Cod ";
            

            //Extract Data from QuestionInfo Table
            using (_con = new SqlConnection(cs))
            {
                using (_command = new SqlCommand(query, _con))
                {
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@Ref_Cod", AutoGenerateEdit.Text);
                    _con.Open();
                    DataTable data = new DataTable();
                    _command.ExecuteNonQuery();
                    SqlDataReader dr = _command.ExecuteReader();
                    data.Load(dr);

                    foreach (DataRow row in data.Rows)
                    {
                        EditQues.Text = row["Ques"].ToString();
                        _typeOfInput = Int32.Parse(row["In_Type_FK"].ToString());
                        _moduleChoose = Int32.Parse(row["Module_FK"].ToString());
                        _systemChoose = Int32.Parse(row["System_FK"].ToString());
                    }
                    _con.Close();
                }

                if (_typeOfInput == 1 || _typeOfInput == 2) //Extract Question Answer with text type value from  Question_Answer_TextType table
                {
                    using (_command = new SqlCommand(TextData, _con))
                    {
                        _command.Parameters.AddWithValue("@Ref_Cod", AutoGenerateEdit.Text);
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
                        _command.Parameters.AddWithValue("@Ref_Cod", AutoGenerateEdit.Text);
                        _con.Open();
                        _dt = new DataTable();
                        _command.ExecuteNonQuery();
                        SqlDataReader reader = _command.ExecuteReader();

                        while (reader.Read())
                        {
                            _optionAnswer.Add(reader.GetString(2));
                        }
                    }
                    _con.Close();
                }

            }
        }

        protected void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        private void LoadSystemListDropwdown()
        {
            _con = new SqlConnection(cs);
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
            SystemList.SelectedIndex = _systemChoose;

            if(_systemChoose == 1)
            {
                LoadModalMenuDropdown("HRMS_module");
            }

            else if (_systemChoose == 2)
            {
                LoadModalMenuDropdown("ESS_module");
            }
        }

        protected void SystemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SystemList.SelectedIndex == 1)
            {
                LoadModalMenuDropdown("HRMS_module");
            }
            else if (SystemList.SelectedIndex == 2)
            {
                LoadModalMenuDropdown("ESS_module");
            }
        }

        private void LoadModalMenuDropdown(string module)
        {
            string ModuleQuery = "";

            if (module == "HRMS_module")
            {
                ModuleQuery = "Select * from HRMS_module ";
            }
            else if (module == "ESS_module")
            {
                ModuleQuery = "Select * from ESS_module ";
            }

            using (_con = new SqlConnection(cs))
            {
                using (_command = new SqlCommand(ModuleQuery, _con))
                {
                    _con.Open();
                    _adapt = new SqlDataAdapter(_command);
                    _dt = new DataTable();
                    _adapt.Fill(_dt);
                    ModuleMenu.DataSource = _dt;
                    ModuleMenu.DataBind();
                    ModuleMenu.DataTextField = "Name";
                    ModuleMenu.DataValueField = "PK";
                    ModuleMenu.DataBind();
                    ModuleMenu.Items.Insert(0, new ListItem("--Select--", "0"));
                    _con.Close();
                    ModuleMenu.SelectedIndex = _moduleChoose;
                }
            }
            
        }

        private void AnswerField()
        {
            //For Question with Text Box Answer Data
            if (_typeOfInput == 1)
            {
                TypeOfInputEdit.SelectedIndex = _typeOfInput;
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                TBAnswerEditBox.Text = _textAnswer;
                TBTedit.SelectedValue = _fieldTypeEdit;
            }

            else if (_typeOfInput == 2)
            {
                //For Question with Memo Answer Data
                TypeOfInputEdit.SelectedIndex = _typeOfInput;
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                MMAnswerEditBox.Text = _textAnswer;
                MMTedit.SelectedValue = _fieldTypeEdit;
            }
            else if (_typeOfInput == 3)
            {
                TypeOfInputEdit.SelectedIndex = _typeOfInput;
                TypeOfInputView.ActiveViewIndex = _typeOfInput;

                foreach (string ans in _optionAnswer)
                {
                    RBTable.Rows.Add(ans); // LOAD ALL ANSWER INTO TEXT BOX FOR RADIO BUTTON
                }

                Bind();

            }
            else if (_typeOfInput == 4)
            {
                TypeOfInputEdit.SelectedIndex = _typeOfInput;
                TypeOfInputView.ActiveViewIndex = _typeOfInput;

                foreach (string ans in _optionAnswer)
                {
                    CBTable.Rows.Add(ans); // LOAD ALL ANSWER INTO TEXT BOX FOR CHECK BOX
                }

                Bind();
            }
        }
        //Bind for both Radio Button and Check Box table
        private void Bind()
        {
            RepeaterRBBox.DataSource = RBTable;
            RepeaterCBBox.DataSource = CBTable;
            RepeaterRBBox.DataBind();
            RepeaterCBBox.DataBind();
        }

        #region Repeater for Radio Button Answer Box

        private void PopulateDataTableRb()
        {
            foreach (RepeaterItem item in RepeaterRBBox.Items)
            {
                TextBox txtDescription = (TextBox)item.FindControl("RBanswerUpdate");
                DataRow row = RBTable.NewRow();
                row["RB_BOX"] = txtDescription.Text;
                RBTable.Rows.Add(row);
            }
        }

        protected void RepeaterRBBox_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PopulateDataTableRb();
            RBTable.Rows[e.Item.ItemIndex].Delete();
            Bind();
        }

        protected void btnAddRB_Click(object sender, EventArgs e)
        {
            PopulateDataTableRb();
            RBTable.Rows.Add(RBTable.NewRow());

            Bind();
        }
        #endregion

        #region Repeater for Check Box Answer Box

        private void PopulateDataTableCb()
        {
            foreach (RepeaterItem item in RepeaterCBBox.Items)
            {
                TextBox txtDescription = (TextBox)item.FindControl("CBanswerUpdate");
                DataRow row = CBTable.NewRow();
                row["CB_BOX"] = txtDescription.Text;
                CBTable.Rows.Add(row);
            }
        }

        protected void RepeaterCBBox_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PopulateDataTableCb();
            CBTable.Rows[e.Item.ItemIndex].Delete();
            Bind();
        }

        protected void btnAddCB_Click(object sender, EventArgs e)
        {
            PopulateDataTableCb();
            CBTable.Rows.Add(CBTable.NewRow());

            Bind();
        }

        #endregion

        //Button cancel
        protected void button_cancel(object sender, EventArgs e)
        {
            Session["cancel_edit"] = "You have cancelled your question update.";
            Session["selected_system"] = SystemList.SelectedValue;
            Session["selected_module"] = ModuleMenu.SelectedValue;
            Response.Redirect("ViewQuestion.aspx");
        }

        //Button update question
        protected void button_update(object sender, EventArgs e)
        {
            string queryQuest = "Update QuestionBank set System_FK = @System, Module_FK = @Module, Ques = @ques, Date_Time = GETDATE() where Ref_Code = @refcode ";
            string queryTextType = "Update Question_Answer_TextType set Ans_Default = @answer, Field_Type = @FT where Ref_FK = @refkode ";
            int moduleId = Int32.Parse(ModuleMenu.SelectedValue);
            int refcode = Int32.Parse(AutoGenerateEdit.Text);
            //checking if else
            if (ModuleMenu.SelectedIndex == 0)
            {
                ShowMessage("Please Choose Your Module.", MessageType.Error);
            }

            else if (String.IsNullOrWhiteSpace(EditQues.Text))
            {
                ShowMessage("Please Enter Your Question.", MessageType.Error);
            }

            else
            {
                //Store the main question into QuestionInfo table
                using (_con = new SqlConnection(cs))
                {
                    using (_command = new SqlCommand(queryQuest, _con))
                    {
                        _command.Parameters.AddWithValue("@System", moduleId);
                        _command.Parameters.AddWithValue("@Module", moduleId);
                        _command.Parameters.AddWithValue("@refcode", refcode);
                        _command.Parameters.AddWithValue("@ques", EditQues.Text);
                        _con.Open();
                        _command.ExecuteNonQuery();
                    }
                }

                if (TypeOfInputEdit.SelectedIndex == 1)
                {
                    //Update the answer for textbox into Question_Answer_TextType table
                    int typeOfField = Int32.Parse(TBTedit.SelectedValue);
                    
                    using (_con = new SqlConnection(cs))
                    {
                        using (_command = new SqlCommand(queryTextType, _con))
                        {
                            _command.Parameters.AddWithValue("@refkode", refcode);
                            _command.Parameters.AddWithValue("@answer", TBAnswerEditBox.Text);
                            _command.Parameters.AddWithValue("@FT", typeOfField);
                            _con.Open();
                            _command.ExecuteNonQuery();
                        }
                    }
                }

                else if (TypeOfInputEdit.SelectedIndex == 2)
                {
                    //Store the answer for memo into Question_Answer_TextType table
                    int typeOfField = Int32.Parse(MMTedit.SelectedValue);

                    using (_con = new SqlConnection(cs))
                    {
                        using (_command = new SqlCommand(queryTextType, _con))
                        {
                            _command.Parameters.AddWithValue("@refkode", refcode);
                            _command.Parameters.AddWithValue("@answer", MMAnswerEditBox.Text);
                            _command.Parameters.AddWithValue("@FT", typeOfField);
                            _con.Open();
                            _command.ExecuteNonQuery();
                        }
                    }
                }

                else if (TypeOfInputEdit.SelectedIndex == 3)
                {
                    //read all the value inside each radio button answer boxes
                    foreach (RepeaterItem item in RepeaterRBBox.Items)
                    {
                        AnswerOptionUpdate.Add(((TextBox)item.FindControl(("RBanswerUpdate"))).Text);
                    }

                    using (_con = new SqlConnection(cs))
                    {
                        //Delete answer inside table
                        using (_command = new SqlCommand(queryDelete, _con))
                        {
                            _command.CommandType = CommandType.StoredProcedure;
                            _command.Parameters.AddWithValue("@Ref_Code", refcode);
                            _con.Open();
                            _command.ExecuteNonQuery();
                        }
                    }

                    using (_con = new SqlConnection(cs))
                    {
                        _con.Open();
                        //insert new answer for rb button
                        for (int i = 0; i < AnswerOptionUpdate.Count(); i++)
                        {
                            using (_command = new SqlCommand(queryOption, _con))
                            {
                                _command.CommandType = CommandType.StoredProcedure;
                                _command.Parameters.AddWithValue("@Ref_Code", refcode);
                                _command.Parameters.AddWithValue("@Answer_Option", AnswerOptionUpdate[i]);
                                _command.ExecuteNonQuery();
                            }
                        }

                    }
                }

                else if (TypeOfInputEdit.SelectedIndex == 4)
                {
                    //read all the value inside each check box answer boxes
                    foreach (RepeaterItem item in RepeaterCBBox.Items)
                    {
                        AnswerOptionUpdate.Add(((TextBox) item.FindControl(("CBanswerUpdate"))).Text);
                    }

                    using (_con = new SqlConnection(cs))
                    {
                        //Delete answer inside table
                        using (_command = new SqlCommand(queryDelete, _con))
                        {
                            _command.CommandType = CommandType.StoredProcedure;
                            _command.Parameters.AddWithValue("@Ref_Code", refcode);
                            _con.Open();
                            _command.ExecuteNonQuery();
                        }
                    }

                    using (_con = new SqlConnection(cs))
                    {
                        _con.Open();
                        //insert new answer for rb button
                        for (int i = 0; i < AnswerOptionUpdate.Count(); i++)
                        {
                            using (_command = new SqlCommand(queryOption, _con))
                            {
                                _command.CommandType = CommandType.StoredProcedure;
                                _command.Parameters.AddWithValue("@Ref_Code", refcode);
                                _command.Parameters.AddWithValue("@Answer_Option", AnswerOptionUpdate[i]);
                                _command.ExecuteNonQuery();
                            }
                        }

                    }
                }

                _con.Close();
                Session["update_message"] = "Your question have been updated.";
                Session["selected_system"] = SystemList.SelectedValue;
                Session["selected_module"] = ModuleMenu.SelectedValue; 
                Response.Redirect("ViewQuestion.aspx");
            }
        }

        protected void button_create(object sender, EventArgs e)
        {
            var typeOfInputId = int.Parse(TypeOfInputEdit.SelectedValue);
            var systemId = int.Parse(SystemList.SelectedValue);
            var moduleId = int.Parse(ModuleMenu.SelectedValue);
            //checking if else
            if (ModuleMenu.SelectedIndex == 0)
            {
                ShowMessage("Please Choose Your Module.", MessageType.Error);
            }

            else if (string.IsNullOrWhiteSpace(EditQues.Text))
            {
                ShowMessage("Please Enter Your Question.", MessageType.Error);
            }

            else
            {
                _con = new SqlConnection(cs);
                const string query = "AddQuestionProcedure";
                //Store the main question into QuestionInfo table
                int refcode;
                using (_con = new SqlConnection(cs))
                {
                    using (_command = new SqlCommand(query, _con))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@System", systemId);
                        _command.Parameters.AddWithValue("@Module", moduleId);
                        _command.Parameters.AddWithValue("@ques", EditQues.Text);
                        _command.Parameters.AddWithValue("@TOI", typeOfInputId);
                        _con.Open();
                        refcode = Convert.ToInt32(_command.ExecuteScalar());
                    }
                }

                switch (typeOfInputId)
                {
                    case 1:
                    {
                        var typeOfField = int.Parse(TBTedit.SelectedValue);

                        //Store the answer for textbox into Question_Answer_TextType table
                        using (_con = new SqlConnection(cs))
                        {
                            using (_command = new SqlCommand(queryText, _con))
                            {
                                _command.CommandType = CommandType.StoredProcedure;
                                _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                                _command.Parameters.AddWithValue("@answer", TBAnswerEditBox.Text);
                                _command.Parameters.AddWithValue("@FieldNum", typeOfField);
                                _con.Open();
                                _command.ExecuteNonQuery();
                            }
                        }

                    }
                        break;
                    case 2:
                    {
                        //Store the answer for memo into Question_Answer_TextType table
                        var typeOfField = int.Parse(MMTedit.SelectedValue);

                        using (_con = new SqlConnection(cs))
                        {
                            using (_command = new SqlCommand(queryText, _con))
                            {
                                _command.CommandType = CommandType.StoredProcedure;
                                _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                                _command.Parameters.AddWithValue("@answer", MMAnswerEditBox.Text);
                                _command.Parameters.AddWithValue("@FieldNum", typeOfField);
                                _con.Open();
                                _command.ExecuteNonQuery();
                            }
                        }
                    }
                        break;
                    case 3:
                        //read all the value inside each radio button answer boxes
                        foreach (RepeaterItem item in RepeaterRBBox.Items)
                        {
                            AnswerOptionUpdate.Add(((TextBox)item.FindControl(("RBanswerUpdate"))).Text);
                        }

                        //Store the answer for radio button into Question_Answer_OptionType table
                        for (var i = 0; i < AnswerOptionUpdate.Count(); i++)
                        {
                            using (_con = new SqlConnection(cs))
                            {
                                _con.Open();
                                using (_command = new SqlCommand(queryOption, _con))
                                {
                                    _command.CommandType = CommandType.StoredProcedure;
                                    _command.Parameters.AddWithValue("@Ref_Code", refcode);
                                    _command.Parameters.AddWithValue("@Answer_Option", AnswerOptionUpdate[i]);
                                    _command.ExecuteNonQuery();
                                }
                            }
                        }
                        break;
                    case 4:
                        //read all the value inside each check box answer boxes
                        foreach (RepeaterItem item in RepeaterCBBox.Items)
                        {
                            AnswerOptionUpdate.Add(((TextBox)item.FindControl(("CBanswerUpdate"))).Text);
                        }

                        //Store the answer for check box into Question_Answer_OptionType table
                        for (var i = 0; i < AnswerOptionUpdate.Count(); i++)
                        {

                            using (_con = new SqlConnection(cs))
                            {
                                _con.Open();
                                using (_command = new SqlCommand(queryOption, _con))
                                {
                                    _command.CommandType = CommandType.StoredProcedure;
                                    _command.Parameters.AddWithValue("@Ref_Code", refcode);
                                    _command.Parameters.AddWithValue("@Answer_Option", AnswerOptionUpdate[i]);
                                    _command.ExecuteNonQuery();
                                }
                            }
                        }
                        break;
                }
                _con.Close();
                Session["create_message"] = "New Question have been created!";
                Session["selected_system"] = SystemList.SelectedValue;
                Session["selected_module"] = ModuleMenu.SelectedValue;
                Response.Redirect("ViewQuestion.aspx");
            }
        }
    }
}