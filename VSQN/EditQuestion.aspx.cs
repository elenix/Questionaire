using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VSQN
{
    public partial class EditQuestion : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        SqlCommand command;
        DataTable RBTable = new DataTable();
        DataTable CBTable = new DataTable();
        int _typeOfInput = 0;
        int _moduleChoose = 0;
        string _fieldTypeEdit = null;
        string _textAnswer = null;
        List<string> _optionAnswer = new List<string>();

        protected enum MessageType { Success, Error, Info, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
            RBTable.Columns.Add("RB_BOX");
            CBTable.Columns.Add("CB_BOX");

            if (!IsPostBack)
            {
                if (Session["Row_Ref_Code"] != null)
                {
                    AutoGenerate.Text = Session["Row_Ref_Code"].ToString();
                }

                ExtractData();
                LoadModalMenuDropdown();
                AnswerField();

            }
        }

        protected void ExtractData()
        {
            con = new SqlConnection(cs);
            string query = "ExtractQuestionData";
            string TextData = "Select * from Question_Answer_TextType where Ref_FK = @Ref_Cod ";
            string OptionData = "Select * from Question_Answer_OptionType where Ref_FK = @Ref_Cod ";
            

            //Extract Data from QuestionInfo Table
            using (con = new SqlConnection(cs))
            {
                using (command = new SqlCommand(query, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Ref_Cod", AutoGenerate.Text);
                    con.Open();
                    DataTable data = new DataTable();
                    command.ExecuteNonQuery();
                    SqlDataReader dr = command.ExecuteReader();
                    data.Load(dr);

                    foreach (DataRow row in data.Rows)
                    {
                        EditQues.Text = row["Ques"].ToString();
                        _typeOfInput = Int32.Parse(row["In_Type_FK"].ToString());
                        _moduleChoose = Int32.Parse(row["Module_FK"].ToString());
                    }
                    con.Close();
                }

                if (_typeOfInput == 1 || _typeOfInput == 2) //Extract Question Answer with text type value from  Question_Answer_TextType table
                {
                    using (command = new SqlCommand(TextData, con))
                    {
                        command.Parameters.AddWithValue("@Ref_Cod", AutoGenerate.Text);
                        con.Open();
                        DataTable data = new DataTable();
                        command.ExecuteNonQuery();
                        SqlDataReader dr = command.ExecuteReader();
                        data.Load(dr);

                        foreach (DataRow row in data.Rows)
                        {
                            _textAnswer = row["Ans_Default"].ToString();
                            _fieldTypeEdit = row["Field_Type"].ToString();
                        }
                    }
                    con.Close();
                }

                else
                {
                    using (command = new SqlCommand(OptionData, con))
                    {
                        command.Parameters.AddWithValue("@Ref_Cod", AutoGenerate.Text);
                        con.Open();
                        DataTable data = new DataTable();
                        command.ExecuteNonQuery();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            _optionAnswer.Add(reader.GetString(2));
                        }
                    }
                    con.Close();
                }

            }
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        protected void LoadModalMenuDropdown()
        {
            con = new SqlConnection(cs);
            string com = "Select * from Module";
            adapt = new SqlDataAdapter(com, con);
            dt = new DataTable();
            adapt.Fill(dt);
            ModuleMenu.DataSource = dt;
            ModuleMenu.DataBind();
            ModuleMenu.DataTextField = "Name";
            ModuleMenu.DataValueField = "PK";
            ModuleMenu.DataBind();
            ModuleMenu.Items.Insert(0, new ListItem("--Select--", "0"));
            ModuleMenu.SelectedIndex = _moduleChoose;
        }

        protected void AnswerField()
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

                for (int i = 0; i < _optionAnswer.Count; i++) // LOAD ALL ANSWER INTO TEXT BOX FOR RADIO BUTTON
                {
                    RBTable.Rows.Add(_optionAnswer[i]);
                }
                Bind();

            }
            else if (_typeOfInput == 4)
            {
                TypeOfInputEdit.SelectedIndex = _typeOfInput;
                TypeOfInputView.ActiveViewIndex = _typeOfInput;

                for (int i = 0; i < _optionAnswer.Count; i++) // LOAD ALL ANSWER INTO TEXT BOX FOR CHECK BOX
                {
                    CBTable.Rows.Add(_optionAnswer[i]);
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

        //Repeater for Radio Button Answer Box
        private void PopulateDataTableRB()
        {
            foreach (RepeaterItem item in RepeaterRBBox.Items)
            {
                TextBox txtDescription = (TextBox)item.FindControl("RBanswer");
                DataRow row = RBTable.NewRow();
                row["RB_BOX"] = txtDescription.Text;
                RBTable.Rows.Add(row);
            }
        }

        protected void RepeaterRBBox_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PopulateDataTableRB();
            RBTable.Rows[e.Item.ItemIndex].Delete();
            Bind();
        }

        protected void btnAddRB_Click(object sender, EventArgs e)
        {
            PopulateDataTableRB();
            RBTable.Rows.Add(RBTable.NewRow());

            Bind();
        }

        //Repeater for Check Box Answer Box
        private void PopulateDataTableCB()
        {
            foreach (RepeaterItem item in RepeaterCBBox.Items)
            {
                TextBox txtDescription = (TextBox)item.FindControl("CBanswer");
                DataRow row = CBTable.NewRow();
                row["CB_BOX"] = txtDescription.Text;
                CBTable.Rows.Add(row);
            }
        }

        protected void RepeaterCBBox_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PopulateDataTableCB();
            CBTable.Rows[e.Item.ItemIndex].Delete();
            Bind();
        }

        protected void btnAddCB_Click(object sender, EventArgs e)
        {
            PopulateDataTableCB();
            CBTable.Rows.Add(CBTable.NewRow());

            Bind();
        }

        protected void button_cancel(object sender, EventArgs e)
        {
            ShowMessage("You have cancelled your question update.", MessageType.Warning);
            Response.Redirect("ViewQuestion.aspx");
        }
    }
}