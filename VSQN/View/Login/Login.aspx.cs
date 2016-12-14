using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VSQN.View.Login
{
    public partial class Login : System.Web.UI.Page
    {
        private string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection con;
        private SqlDataAdapter adapt;
        private DataTable dt;
        private SqlCommand command;

        public enum MessageType { Success, Error, Info, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView.ActiveViewIndex = 0;
            }
        }

        protected void Login_Success(object sender, EventArgs e)
        {
            con = new SqlConnection(cs);
            string query = "select * from UserAuth where email = @email and password = @pass";
            using (con = new SqlConnection(cs))
            {
                using (command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@email", InputEmail.Text);
                    command.Parameters.AddWithValue("@pass", userpassword.Text);
                    adapt = new SqlDataAdapter(command);
                    dt = new DataTable();
                    adapt.Fill(dt);
                    con.Open();
                    int i = command.ExecuteNonQuery();
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        { 
                            Session["username"] = row["Company"].ToString();
                        }
                        Response.Redirect("~/View/Admin/QuesSetup.aspx");
                        Session.RemoveAll();
                    }
                    else
                    {
                        ShowMessage("You have entered wrong email or password. Please try again", MessageType.Error);
                    }
                }
            }     
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 0;
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 1;
        }

        private void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

        }

        protected void Register_New(object sender, EventArgs e)
        {
            con = new SqlConnection(cs);
            string query = "insert into UserAuth (username,email,password,user_role,Company) values (@Username, @Email, @Password, 'U', @company)";

            if (String.IsNullOrWhiteSpace(userName.Text))
            {
                ShowMessage("Please enter <b>Username</b> before proceed to next", MessageType.Warning);
            }
            else if (String.IsNullOrWhiteSpace(companyId.Text))
            {
                ShowMessage("Please enter <b>Company Name</b> before proceed to next", MessageType.Warning);
            }
            else if (String.IsNullOrWhiteSpace(newEmail.Text))
            {
                ShowMessage("Please enter <b>Company Email</b> before proceed to next", MessageType.Warning);
            }
            else if (String.IsNullOrWhiteSpace(newPassword.Text))
            {
                ShowMessage("Please enter your <b>Password</b> before proceed to next", MessageType.Warning);
            }
            else if (newPassword.Text != confirmPassword.Text)
            {
                ShowMessage("Your <b>Comfirm Password</b> did not match!", MessageType.Warning);
            }
            else
            {
                using (con = new SqlConnection(cs))
                {
                    using (command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@Username", userName.Text);
                        command.Parameters.AddWithValue("@Email", newEmail.Text);
                        command.Parameters.AddWithValue("@Password", newPassword.Text);
                        command.Parameters.AddWithValue("@company", companyId.Text);
                        con.Open();
                        command.ExecuteNonQuery();
                        ShowMessage("You have succesfully registered! Now please Login", MessageType.Success);
                        MultiView.ActiveViewIndex = 0;
                    }
                }
            }
        }
    }
}