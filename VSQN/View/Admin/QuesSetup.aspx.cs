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
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private readonly DataTable _rbTable = new DataTable();
        private readonly DataTable _cbTable = new DataTable();
        private SqlConnection _con;
        private SqlDataAdapter _adapt;
        private DataTable _dt;
        private SqlCommand _command; 
        readonly List<string> _answerOption = new List<string>();


        private enum MessageType { Success, Error };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null)
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

        //Button for Add Question
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            var typeOfInputId = int.Parse(TypeOfInput.SelectedValue);
            //checking if else
            if (SystemList.SelectedIndex == 0)
            {
                ShowMessage("Please Choose Your System.", MessageType.Error);
            }

            else if (ModuleMenu.SelectedIndex == 0)
            {
                ShowMessage("Please Choose Your Module.", MessageType.Error);
            }

            else if (String.IsNullOrWhiteSpace(SeqNum.Text))
            {
                ShowMessage("Please Enter The Sequence Number For The Question.", MessageType.Error);
            }

            else if (String.IsNullOrWhiteSpace(ques.Text))
            {
                ShowMessage("Please Enter Your Question.", MessageType.Error);
            }

            else if (typeOfInputId == 0)
            {
                ShowMessage("Please Choose Your Type Of Input.", MessageType.Error);
            }

            else
            {
                _con = new SqlConnection(_cs);
                const string query = "AddQuestionProcedure";
                const string queryText = "AddQuestionAnswerForText";
                const string queryOption = "AddQuestionAnswerOption";
                int refCode;
                var moduleId = int.Parse(ModuleMenu.SelectedValue);
                var systemId = int.Parse(SystemList.SelectedValue);

                //Store the main question into QuestionInfo table
                using (_con = new SqlConnection(_cs))
                {
                    using (_command = new SqlCommand(query, _con))
                    {
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.AddWithValue("@System", systemId);
                        _command.Parameters.AddWithValue("@Module", moduleId);
                        _command.Parameters.AddWithValue("@Seq", SeqNum.Text);
                        _command.Parameters.AddWithValue("@ques", ques.Text);
                        _command.Parameters.AddWithValue("@TOI", typeOfInputId);
                        _con.Open();
                        refCode = Convert.ToInt32(_command.ExecuteScalar());
                    }
                }

                switch (typeOfInputId)
                {
                    case 1:
                        {
                            var typeOfField = int.Parse(TBT.SelectedValue);

                            //Store the answer for textbox into Question_Answer_TextType table
                            using (_con = new SqlConnection(_cs))
                            {
                                using (_command = new SqlCommand(queryText, _con))
                                {
                                    _command.CommandType = CommandType.StoredProcedure;
                                    _command.Parameters.AddWithValue("@Ref_Cod", refCode);
                                    _command.Parameters.AddWithValue("@answer", TBAnswer.Text);
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
                            var typeOfField = int.Parse(MMT.SelectedValue);

                            using (_con = new SqlConnection(_cs))
                            {
                                using (_command = new SqlCommand(queryText, _con))
                                {
                                    _command.CommandType = CommandType.StoredProcedure;
                                    _command.Parameters.AddWithValue("@Ref_Cod", refCode);
                                    _command.Parameters.AddWithValue("@answer", MMAnswer.Text);
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
                            _answerOption.Add(((TextBox)item.FindControl(("RBanswer"))).Text);
                        }

                        //Store the answer for radio button into Question_Answer_OptionType table
                        for (var i = 0; i < _answerOption.Count(); i++)
                        {
                            using (_con = new SqlConnection(_cs))
                            {
                                _con.Open();
                                using (_command = new SqlCommand(queryOption, _con))
                                {
                                    _command.CommandType = CommandType.StoredProcedure;
                                    _command.Parameters.AddWithValue("@Ref_Code", refCode);
                                    _command.Parameters.AddWithValue("@Answer_Option", _answerOption[i]);
                                    _command.ExecuteNonQuery();
                                }
                            }
                        }
                        break;

                    case 4:
                        //read all the value inside each check box answer boxes
                        foreach (RepeaterItem item in RepeaterCBBox.Items)
                        {
                            _answerOption.Add(((TextBox)item.FindControl(("CBanswer"))).Text);
                        }

                        //Store the answer for check box into Question_Answer_OptionType table
                        for (var i = 0; i < _answerOption.Count(); i++)
                        {
                            using (_con = new SqlConnection(_cs))
                            {
                                _con.Open();
                                using (_command = new SqlCommand(queryOption, _con))
                                {
                                    _command.CommandType = CommandType.StoredProcedure;
                                    _command.Parameters.AddWithValue("@Ref_Code", refCode);
                                    _command.Parameters.AddWithValue("@Answer_Option", _answerOption[i]);
                                    _command.ExecuteNonQuery();
                                }
                            }
                        }
                        break;
                }
                _con.Close();
                ShowMessage("The Question have been saved!", MessageType.Success);
                //Response.Write("<script>alert('Data Inserted !!')</script>");
                ClearAll();
            }

            
        }
    }


}
