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
    public partial class QuesSetup : Page
    {
        readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection _con;
        readonly DataTable _rbTable = new DataTable();
        readonly DataTable _cbTable = new DataTable();
        SqlDataAdapter _adapt;
        DataTable _dt;
        SqlCommand _command; 
        readonly List<string> _answerOption = new List<string>();

        private enum MessageType { Success, Error };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && (Session["user_role"].ToString() == "A" || Session["user_role"].ToString() == "M"))
            {
                _rbTable.Columns.Add("RB_BOX");
                _cbTable.Columns.Add("CB_BOX");

                if (!IsPostBack)
                {
                    LoadSystemListDropwdown();
                }
            }
            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        }

        private void LoadSystemListDropwdown()
        {
            _con = new SqlConnection(_cs);
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
        }

        protected void SystemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SystemList.SelectedIndex == 1)
            {
                LoadModalMenuDropdown("HRMS_module");
            }
            else if (SystemList.SelectedIndex == 2)
            {
                LoadModalMenuDropdown("ESS_module");
            }
            else if (SystemList.SelectedIndex == 3)
            {
                LoadModalMenuDropdown("HRSS_module");
            }
            else if(SystemList.SelectedIndex == 4)
            {
                LoadModalMenuDropdown("SAAS_module");
            }
        }

        private void LoadModalMenuDropdown(string module)
        {
            string ModuleQuery = "";

            if(module == "HRMS_module")
            {
                ModuleQuery = "Select * from HRMS_module ";
            }
            else if(module == "ESS_module")
            {
                ModuleQuery = "Select * from ESS_module ";
            }
            else if (module == "HRSS_module")
            {
                ModuleQuery = "Select * from HRSS_module ";
            }
            else if (module == "SAAS_module")
            {
                ModuleQuery = "Select * from SAAS_module ";
            }

            using (_con = new SqlConnection(_cs))
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
                }
            }
                    
        }

        private void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);

        }

        private void ClearAll()
        {
            ShowMessage("The Question have been saved!", MessageType.Success);
            ques.Text = string.Empty;
            ModuleMenu.SelectedIndex = 0;
            SeqNum.Text = string.Empty;
            TypeOfInput.SelectedIndex = 0;
            TypeOfInputView.ActiveViewIndex = 0;
            TBAnswer.Text = string.Empty;
            MMAnswer.Text = string.Empty;
        }

        protected void TypeOfInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeOfInputView.ActiveViewIndex = Convert.ToInt32(TypeOfInput.SelectedValue);
        }

        //Bind for both Radio Button and Check Box table
        private void Bind()
        {
            RepeaterRBBox.DataSource = _rbTable;
            RepeaterCBBox.DataSource = _cbTable;
            RepeaterRBBox.DataBind();
            RepeaterCBBox.DataBind();

        }

        #region RepeaterRadioButtonBox

        //Repeater for Radio Button Answer Box
        private void PopulateDataTableRb()
        {
            foreach (RepeaterItem item in RepeaterRBBox.Items)
            {
                var txtDescription = (TextBox)item.FindControl("RBanswer");
                var row = _rbTable.NewRow();
                row["RB_BOX"] = txtDescription.Text;
                _rbTable.Rows.Add(row);
            }
        }

        protected void RepeaterRBBox_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PopulateDataTableRb();
            _rbTable.Rows[e.Item.ItemIndex].Delete();
            Bind();
        }

        protected void btnAddRB_Click(object sender, EventArgs e)
        {
            PopulateDataTableRb();
            _rbTable.Rows.Add(_rbTable.NewRow());

            Bind();
        }

        #endregion

        #region RepeaterCheckBoxAnswerBox

        //Repeater for Check Box Answer Box
        private void PopulateDataTableCb()
        {
            foreach (RepeaterItem item in RepeaterCBBox.Items)
            {
                var txtDescription = (TextBox)item.FindControl("CBanswer");
                var row = _cbTable.NewRow();
                row["CB_BOX"] = txtDescription.Text;
                _cbTable.Rows.Add(row);
            }
        }

        protected void RepeaterCBBox_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PopulateDataTableCb();
            _cbTable.Rows[e.Item.ItemIndex].Delete();
            Bind();
        }

        protected void btnAddCB_Click(object sender, EventArgs e)
        {
            PopulateDataTableCb();
            _cbTable.Rows.Add(_cbTable.NewRow());

            Bind();
        }

        #endregion

        protected int Write_Question()
        {
            const string query = "INSERT INTO QuestionBank(System_FK,Module_FK,Ques,In_Type_FK,Seq_Number) VALUES (@System,@Module,@ques,@TOI,@Seq) SELECT SCOPE_IDENTITY()";
            var typeOfInputId = int.Parse(TypeOfInput.SelectedValue);
            int refCode;
            var moduleId = int.Parse(ModuleMenu.SelectedValue);
            var systemId = int.Parse(SystemList.SelectedValue);

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(query, _con))
                {
                    //_command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.AddWithValue("@System", systemId);
                    _command.Parameters.AddWithValue("@Module", moduleId);
                    _command.Parameters.AddWithValue("@Seq", SeqNum.Text);
                    _command.Parameters.AddWithValue("@ques", ques.Text);
                    _command.Parameters.AddWithValue("@TOI", typeOfInputId);
                    _con.Open();
                    refCode = Convert.ToInt32(_command.ExecuteScalar());
                }
            }

            return refCode;
        }

        //Button for Add Question
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var typeOfInputId = int.Parse(TypeOfInput.SelectedValue);

            #region input box validation
            if (SystemList.SelectedIndex == 0)
            {
                ShowMessage("Please choose the <b>System</b>.", MessageType.Error);
            }

            else if (ModuleMenu.SelectedIndex == 0)
            {
                ShowMessage("Please choose the <b>Module</b>.", MessageType.Error);
            }

            else if (String.IsNullOrWhiteSpace(SeqNum.Text))
            {
                ShowMessage("Please enter the <b>sequence number</b> for the question.", MessageType.Error);
            }

            else if (String.IsNullOrWhiteSpace(ques.Text))
            {
                ShowMessage("Please write the <b>Question</b>.", MessageType.Error);
            }

            else if (typeOfInputId == 0)
            {
                ShowMessage("Please choose <b>type of input</b>.", MessageType.Error);
            }
            #endregion

            else
            {
                const string queryText = "INSERT INTO Question_Answer_TextType(Ref_FK,Ans_Default,Field_Type) VALUES (@Ref_Cod,@answer,@FieldNum)";
                const string queryOption = "INSERT INTO Question_Answer_OptionType(Ref_FK,Ans_Option) VALUES (@Ref_Code,@Answer_Option)";
                const string queryAttachment = "INSERT INTO Question_Answer_Attachment (Ref_FK, doc_type) VALUES (@Ref_Cod,@doc_type)";
                int refCode;
                int typeOfField;


                switch (typeOfInputId)
                {
                    case 1:

                        typeOfField = int.Parse(TBT.SelectedValue);
                        refCode = Write_Question();
                        //Store the answer for textbox into Question_Answer_TextType table
                        using (_con = new SqlConnection(_cs))
                        {
                            using (_command = new SqlCommand(queryText, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@Ref_Cod", refCode);
                                _command.Parameters.AddWithValue("@answer", TBAnswer.Text);
                                _command.Parameters.AddWithValue("@FieldNum", typeOfField);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }
                        }

                        ClearAll();

                        break;

                    case 2:

                        //Store the answer for memo into Question_Answer_TextType table
                        typeOfField = int.Parse(MMT.SelectedValue);
                        refCode = Write_Question();

                        using (_con = new SqlConnection(_cs))
                        {
                            using (_command = new SqlCommand(queryText, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@Ref_Cod", refCode);
                                _command.Parameters.AddWithValue("@answer", MMAnswer.Text);
                                _command.Parameters.AddWithValue("@FieldNum", typeOfField);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }
                        }

                        ClearAll();

                        break;

                    case 3:

                        refCode = Write_Question();

                        //read all the value inside each radio button answer boxes
                        foreach (RepeaterItem item in RepeaterRBBox.Items)
                        {
                            var optionText = ((TextBox)item.FindControl(("RBanswer"))).Text;
                            _answerOption.Add(optionText);

                        }

                        using (_con = new SqlConnection(_cs))
                        {
                            //Store the answer for radio button into Question_Answer_OptionType table
                            foreach (var x in _answerOption)
                            {
                                using (_command = new SqlCommand(queryOption, _con))
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Ref_Code", refCode);
                                    _command.Parameters.AddWithValue("@Answer_Option", x);
                                    _command.ExecuteNonQuery();
                                    _con.Close();
                                }
                            }
                        }

                        ClearAll();

                        break;

                    case 4:

                        refCode = Write_Question();

                        //read all the value inside each check box answer boxes
                        foreach (RepeaterItem item in RepeaterCBBox.Items)
                        {
                            var optionText = ((TextBox)item.FindControl(("CBanswer"))).Text;

                            _answerOption.Add(optionText);
                        }

                        using (_con = new SqlConnection(_cs))
                        {
                            //Store the answer for check box into Question_Answer_OptionType table
                            foreach (var x in _answerOption)
                            {
                                using (_command = new SqlCommand(queryOption, _con))
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Ref_Code", refCode);
                                    _command.Parameters.AddWithValue("@Answer_Option", x);
                                    _command.ExecuteNonQuery();
                                    _con.Close();
                                }
                            }
                        }

                        ClearAll();

                        break;

                    case 5:

                        //Store the answer for attachment into Question_Answer_Attachment table
                        if (TypeOfAttachment.SelectedIndex != 0)
                        {
                            refCode = Write_Question();

                            using (_con = new SqlConnection(_cs))
                            {
                                using (_command = new SqlCommand(queryAttachment, _con))
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Ref_Cod", refCode);
                                    _command.Parameters.AddWithValue("@doc_type", TypeOfAttachment.SelectedIndex);
                                    _command.ExecuteNonQuery();
                                    _con.Close();
                                }
                            }

                            ClearAll();
                        }

                        else
                        {
                            ShowMessage("Please choose the <b>Attachment Type</b>.", MessageType.Error);
                        }

                        break;
                }

            }   
        }
    }
}
