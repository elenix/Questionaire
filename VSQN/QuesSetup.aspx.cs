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
        DataTable myTable = new DataTable();
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        SqlCommand command;


        public enum MessageType { Success, Error, Info, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
            myTable.Columns.Add("RB_BOX");

            if (!IsPostBack)
            {
                loadModalMenuDropdown();

                if (!this.IsPostBack)
                {
                    for (int i = 0; i < 1; i++) // LOAD THREE TEXTBOX FOR EXAMPLE
                    {
                        myTable.Rows.Add(myTable.NewRow());
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
            ModuleMenu.DataTextField = "Module";
            ModuleMenu.DataValueField = "PID";
            ModuleMenu.DataBind();
            ModuleMenu.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

        }

        protected void clearAll()
        {
            seq_ques.Text = string.Empty;
            ques.Text = string.Empty;
            ModuleMenu.SelectedIndex = 0;
        }

        protected void Bind()
        {
            RepeaterRBBox.DataSource = myTable;
            RepeaterRBBox.DataBind();
        }

        protected void PopulateDataTable()
        {
            foreach (RepeaterItem item in RepeaterRBBox.Items)
            {
                TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                DataRow row = myTable.NewRow();
                row["RB_BOX"] = txtDescription.Text;
                myTable.Rows.Add(row);
            }
        }

        protected void TypeOfInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeOfInputView.ActiveViewIndex = Convert.ToInt32(TypeOfInput.SelectedValue);
        }

        protected void RepeaterRBBox_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PopulateDataTable();
            myTable.Rows[e.Item.ItemIndex].Delete();
            Bind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PopulateDataTable();
            myTable.Rows.Add(myTable.NewRow());

            Bind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //checking if else
            if (ModuleMenu.SelectedIndex == 0)
            {
                ShowMessage("Please Choose Your Module.", MessageType.Error);
            }
            else if (String.IsNullOrWhiteSpace(seq_ques.Text))
            {
                ShowMessage("Please Enter Your Sequence number.", MessageType.Error);
            }

            else if (String.IsNullOrWhiteSpace(ques.Text))
            {
                ShowMessage("Please Enter Your Question.", MessageType.Error);
            }

            else
            {
                con = new SqlConnection(cs);
                con.Open();

                int ModuleID = Int32.Parse(ModuleMenu.SelectedValue.ToString());

                command = new SqlCommand("insert into Question (u_seq, u_ques, FID) VALUES(@seq,@ques,@Module)", con);
                command.Parameters.AddWithValue("@seq", seq_ques.Text);
                command.Parameters.AddWithValue("@ques", ques.Text);
                command.Parameters.AddWithValue("@Module", ModuleID);
                int m = command.ExecuteNonQuery();

                ShowMessage("The Question have been saved!", MessageType.Success);
                //Response.Write("<script>alert('Data Inserted !!')</script>");

                con.Close();
                clearAll();
            }

            
        }
    }


}
