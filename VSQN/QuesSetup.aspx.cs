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
    public partial class QuesSetup : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        DataTable RBTable = new DataTable();
        DataTable CBTable = new DataTable();
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        SqlCommand command;
        string queryText = "AddQuestionAnswerForText";
        string queryOption = "AddQuestionAnswerOption";


        public enum MessageType { Success, Error, Info, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
            RBTable.Columns.Add("RB_BOX");
            CBTable.Columns.Add("CB_BOX");

            if (!IsPostBack)
            {
                loadModalMenuDropdown();

                if (this.IsPostBack)
                {
                    for (int i = 0; i < 1; i++) // LOAD ONE TEXTBOX FOR EXAMPLE FOR CHECK BOX AND RADIO BUTTON
                    {
                        RBTable.Rows.Add(RBTable.NewRow());
                        CBTable.Rows.Add(CBTable.NewRow());
                    }

                    Bind();
                }

            }
        }

        protected void loadModalMenuDropdown()
        {
            con = new SqlConnection(cs);
            string com = "Select * from Module";
            adapt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            ModuleMenu.DataSource = dt;
            ModuleMenu.DataBind();
            ModuleMenu.DataTextField = "Name";
            ModuleMenu.DataValueField = "PK";
            ModuleMenu.DataBind();
            ModuleMenu.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

        }

        protected void clearAll()
        {
            ques.Text = string.Empty;
            ModuleMenu.SelectedIndex = 0;
            TypeOfInput.SelectedIndex = 0;
            TypeOfInputView.ActiveViewIndex = 0;
        }

        protected void TypeOfInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeOfInputView.ActiveViewIndex = Convert.ToInt32(TypeOfInput.SelectedValue);
        }

        //Bind for both Radio Button and Check Box table
        protected void Bind()
        {
            RepeaterRBBox.DataSource = RBTable;
            RepeaterCBBox.DataSource = CBTable;
            RepeaterRBBox.DataBind();
            RepeaterCBBox.DataBind();

        }

        //Repeater for Radio Button Answer Box
        protected void PopulateDataTableRB()
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
        protected void PopulateDataTableCB()
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

        //Button for Add Question
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            int typeOfInputId = Int32.Parse(TypeOfInput.SelectedValue.ToString());
            //checking if else
            if (ModuleMenu.SelectedIndex == 0)
            {
                ShowMessage("Please Choose Your Module.", MessageType.Error);
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
                con = new SqlConnection(cs);
                string query = "AddQuestionProcedure";
                int refCode;
                int moduleId = Int32.Parse(ModuleMenu.SelectedValue.ToString());

                //Store the main question into QuestionInfo table
                using (con = new SqlConnection(cs))
                {
                    using (command = new SqlCommand(query, con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ques", ques.Text);
                        command.Parameters.AddWithValue("@Module", moduleId);
                        command.Parameters.AddWithValue("@TOI", typeOfInputId);
                        con.Open();
                        refCode = Convert.ToInt32(command.ExecuteScalar());
                    }
                }

                if (typeOfInputId == 1)
                {
                    int typeOfField = Int32.Parse(TBT.SelectedValue.ToString());

                    //Store the answer for textbox into Question_Answer_TextType table
                    using (con = new SqlConnection(cs))
                    {
                        using (command = new SqlCommand(queryText, con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Ref_Cod", refCode);
                            command.Parameters.AddWithValue("@answer", TBAnswer.Text);
                            command.Parameters.AddWithValue("@FieldNum", typeOfField);
                            con.Open();
                            int m = command.ExecuteNonQuery();
                        }
                    }

                }

                else if (typeOfInputId == 2)
                {
                    //Store the answer for memo into Question_Answer_TextType table
                    int typeOfField = Int32.Parse(MMT.SelectedValue.ToString());

                    using (con = new SqlConnection(cs))
                    {
                        using (command = new SqlCommand(queryText, con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Ref_Cod", refCode);
                            command.Parameters.AddWithValue("@answer", MMAnswer.Text);
                            command.Parameters.AddWithValue("@FieldNum", typeOfField);
                            con.Open();
                            int m = command.ExecuteNonQuery();
                        }
                    }
                }

                else if (typeOfInputId == 3)
                {
                    List<string> radioAnswerOption = new List<string>();

                    //read all the value inside each radio button answer boxes
                    foreach (RepeaterItem item in RepeaterRBBox.Items)
                    {
                        radioAnswerOption.Add(((TextBox) item.FindControl(("RBanswer"))).Text);
                    }

                    //Store the answer for radio button into Question_Answer_OptionType table
                    for (int i = 0; i < radioAnswerOption.Count(); i++)
                    {
                        using (con = new SqlConnection(cs))
                        {
                            using (command = new SqlCommand(queryOption, con))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Ref_Code", refCode);
                                command.Parameters.AddWithValue("@Answer_Option", radioAnswerOption[i]);
                                con.Open();
                                int m = command.ExecuteNonQuery();
                            }
                        }
                    }
                }

                else if (typeOfInputId == 4)
                {
                    List<string> checkBoxAnswerOption = new List<string>();

                    //read all the value inside each check box answer boxes
                    foreach (RepeaterItem item in RepeaterCBBox.Items)
                    {
                        checkBoxAnswerOption.Add(((TextBox) item.FindControl(("CBanswer"))).Text);
                    }

                    //Store the answer for check box into Question_Answer_OptionType table
                    for (int i = 0; i < checkBoxAnswerOption.Count(); i++)
                    {
                        using (con = new SqlConnection(cs))
                        {
                            using (command = new SqlCommand(queryOption, con))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Ref_Code", refCode);
                                command.Parameters.AddWithValue("@Answer_Option", checkBoxAnswerOption[i]);
                                con.Open();
                                int m = command.ExecuteNonQuery();
                            }
                        }
                    }
                }
                con.Close();
                ShowMessage("The Question have been saved!", MessageType.Success);
                //Response.Write("<script>alert('Data Inserted !!')</script>");
                clearAll();
            }

            
        }
    }


}
