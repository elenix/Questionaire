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
        int _typeOfField = 0;
        int _moduleChoose = 0;
        string _fieldTypeEdit = null;
        string _textAnswer = null;

        public enum MessageType { Success, Error, Info, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Row_Ref_Code"] != null)
                {
                    AutoGenerate.Text = Session["Row_Ref_Code"].ToString();
                }

                ExtractData();
                loadModalMenuDropdown();
                AnswerField();

            }
        }

        protected void ExtractData()
        {
            con = new SqlConnection(cs);
            string query = "ExtractQuestionData";
            string TextData = "Select * from Question_Answer_TextType where Ref_FK = @Ref_Cod ";

            //Extract Data from QuestionInfo Table
            using (con = new SqlConnection(cs))
            {
                using (command = new SqlCommand(query, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Ref_Cod", AutoGenerate.Text);
                    con.Open();
                    DataTable data = new DataTable();
                    int m = command.ExecuteNonQuery();
                    SqlDataReader dr = command.ExecuteReader();
                    data.Load(dr);

                    foreach (DataRow row in data.Rows)
                    {
                        EditQues.Text = row["Ques"].ToString();
                        _typeOfField = Int32.Parse(row["In_Type_FK"].ToString());
                        _moduleChoose = Int32.Parse(row["Module_FK"].ToString());
                    }
                }
            }

            //Extract Data from Question_Answer_TextType Table
            using (con = new SqlConnection(cs))
            {
                using (command = new SqlCommand(TextData, con))
                {
                    command.Parameters.AddWithValue("@Ref_Cod", AutoGenerate.Text);
                    con.Open();
                    DataTable data = new DataTable();
                    int m = command.ExecuteNonQuery();
                    SqlDataReader dr = command.ExecuteReader();
                    data.Load(dr);

                    foreach (DataRow row in data.Rows)
                    {
                        _textAnswer = row["Ans_Default"].ToString();
                        _fieldTypeEdit = row["Field_Type"].ToString();
                    }
                }
            }
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        protected void loadModalMenuDropdown()
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
            if (_typeOfField == 1)
            {
                TypeOfInputEdit.SelectedIndex = _typeOfField;
                TypeOfInputView.ActiveViewIndex = _typeOfField;
                TBAnswerEditBox.Text = _textAnswer;
                TBTedit.SelectedValue = _fieldTypeEdit;
            }

            else if (_typeOfField == 2)
            {
                //For Question with Memo Answer Data
                TypeOfInputEdit.SelectedIndex = _typeOfField;
                TypeOfInputView.ActiveViewIndex = _typeOfField;
                MMAnswerEditBox.Text = _textAnswer;
                MMTedit.SelectedValue = _fieldTypeEdit;
            }
            else if (_typeOfField == 3)
            {
                TypeOfInputEdit.SelectedIndex = _typeOfField;
                TypeOfInputView.ActiveViewIndex = _typeOfField;
            }
            else if (_typeOfField == 4)
            {
                TypeOfInputEdit.SelectedIndex = _typeOfField;
                TypeOfInputView.ActiveViewIndex = _typeOfField;
            }
        }

        protected void button_cancel(object sender, EventArgs e)
        {
            ShowMessage("You have cancelled your question update.", MessageType.Warning);
            Response.Redirect("ViewQuestion.aspx");
        }
    }
}