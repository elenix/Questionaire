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


        public enum MessageType { Success, Error, Info, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
            RBTable.Columns.Add("RB_BOX");
            CBTable.Columns.Add("CB_BOX");

            if (!IsPostBack)
            {
                loadModalMenuDropdown();

                if (!this.IsPostBack)
                {
                    for (int i = 0; i < 1; i++) // LOAD ONE TEXTBOX FOR EXAMPLE
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
                TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
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
                TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
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
