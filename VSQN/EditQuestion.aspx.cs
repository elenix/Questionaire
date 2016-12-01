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
        int Type_Of_Field = 0;
        int Module_Choose = 0;

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
                TypeOfField();

            }
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
            ModuleMenu.SelectedIndex = Module_Choose;
        }

        protected void button_back(object sender, EventArgs e)
        {
            Response.Redirect("ViewQuestion.aspx");
        }

        protected void ExtractData()
        {
            con = new SqlConnection(cs);
            string query = "ExtractQuestionData";

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
                        Type_Of_Field = Int32.Parse(row["In_Type_FK"].ToString());
                        Module_Choose = Int32.Parse(row["Module_FK"].ToString());
                    }
                }
            }
        }

        protected void TypeOfField()
        {
            if (Type_Of_Field == 1)
            {
                TypeOfInput.Text = "Text Box";
            }
            else if (Type_Of_Field == 2)
            {
                TypeOfInput.Text = "Memo";
            }
            else if (Type_Of_Field == 3)
            {
                TypeOfInput.Text = "Radio Button";
            }
            else if (Type_Of_Field == 4)
            {
                TypeOfInput.Text = "Check Box";
            }
        }

    }
}