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
    public partial class About : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowData();
            }
        }

        //ShowData method for Displaying Data in Gridview  
        protected void ShowData()
        {
            dt = new DataTable();
            con = new SqlConnection(cs);
            con.Open();
            adapt = new SqlDataAdapter("Select u_id, u_seq,u_ques from qs_tbl", con); //nanti ubah
            adapt.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Result.DataSource = dt;
                Result.DataBind();
            }
            con.Close();
        }

        protected void Result_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            Result.EditIndex = e.NewEditIndex;
            ShowData();  
        }

        protected void Result_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            //Finding the controls from Gridview for the row which is going to update  
            Label ref_num = Result.Rows[e.RowIndex].FindControl("lblReferenceCode") as Label;
            TextBox seq_num = Result.Rows[e.RowIndex].FindControl("txtSequence") as TextBox;
            TextBox ques_num = Result.Rows[e.RowIndex].FindControl("txtQuestion") as TextBox;
            con = new SqlConnection(cs);
            con.Open();
            //updating the record  
            SqlCommand cmd = new SqlCommand("Update qs_tbl set u_seq='" + seq_num.Text + "',u_ques='" + ques_num.Text + "' where u_id=" + Convert.ToInt32(ref_num.Text), con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            Result.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();  
        }

        protected void Result_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            Result.EditIndex = -1;
            ShowData();  
        }

        protected void Result_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = Convert.ToInt32(e.RowIndex);
            int ID = 0;

            Label ref_num = Result.Rows[e.RowIndex].FindControl("lblReferenceCode") as Label;

            ID = Convert.ToInt16(ref_num.Text);

            if (ID != 0)
            {

                con = new SqlConnection(cs);
                
                try
                {
                    con.Open();
                    SqlCommand objcmd = new SqlCommand("Delete from qs_tbl Where u_id='" + ID + "'", con);
                    objcmd.ExecuteNonQuery();
                    Response.Redirect("QuesSetup.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message.ToString());
                }
                finally
                {
                    con.Close();
                }

            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            // Buat checking if else
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
                //string str = @"Data Source=NTBK20\SQL2014;Initial Catalog=CRUD;User Id=sa;Password=p@ssw0rd;";
                //string conn = "";
                //conn = ConfigurationManager.ConnectionStrings["conn"].ToString();
                con = new SqlConnection(cs);
                con.Open();

                string insert = "insert into qs_tbl (u_seq,u_ques) values ('" + seq_ques.Text + "','" + ques.Text + "')";
                SqlCommand cmd = new SqlCommand(insert, con);
                int m = cmd.ExecuteNonQuery();
                if (m != 0)
                {
                    Response.Write("<script>alert('Data Inserted !!')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Data Inserted !!')</script>");
                }
                con.Close();
            }

            ShowData();
        }
    }


}
