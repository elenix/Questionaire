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
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        SqlCommand command;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                con = new SqlConnection(cs);
                string com = "Select * from Module";
                adapt = new SqlDataAdapter(com, con);
                dt = new DataTable();
                adapt.Fill(dt);
                ModuleMenu.DataSource = dt;
                ModuleMenu.DataBind();
                ModuleMenu.DataTextField = "Module";
                ModuleMenu.DataValueField = "PID";
                ModuleMenu.DataBind();
                ModuleMenu.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }


        protected void clearAll()
        {
            seq_ques.Text = string.Empty;
            ques.Text = string.Empty;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            //checking if else
            if (String.IsNullOrWhiteSpace(seq_ques.Text))
            {
                Response.Write("<script>alert('Please Enter the Sequence Number')</script>");
            }

            else if (String.IsNullOrWhiteSpace(ques.Text))
            {
                Response.Write("<script>alert('Please Enter the Question before proceed')</script>");
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

                Response.Write("<script>alert('Data Inserted !!')</script>");

                con.Close();
            }

            clearAll();
        }
    }


}
