using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace VSQN
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string str = @"Data Source=NTBK20\SQL2014;Initial Catalog=CRUD;User Id=sa;Password=p@ssw0rd;";
            SqlConnection db = new SqlConnection(str);
            db.Open();

            string insert = "insert into qs_tbl (u_seq,u_ques) values ('" + seq_ques.Text + "','" + ques.Text + "')";
            SqlCommand cmd = new SqlCommand(insert, db);
            int m = cmd.ExecuteNonQuery();
            if (m != 0)
            {
                Response.Write("<script>alert('Data Inserted !!')</script>");
            }
            else
            {
                Response.Write("<script>alert('Data Inserted !!')</script>");
            }
            db.Close();  
        }
    }


}
