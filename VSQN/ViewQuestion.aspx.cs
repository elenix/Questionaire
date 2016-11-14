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
    public partial class ViewQuestion : System.Web.UI.Page
    {

        string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection con;
        SqlDataAdapter adapt;
        SqlCommand command;
        DataTable dt;

        public enum MessageType { Success, Error, Info, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadModalMenuDropdown();
                ShowData();
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

        protected void ModelMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        protected void Result_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Result.PageIndex = e.NewPageIndex;
            ShowData();
        }

        //ShowData method for Displaying Data in Gridview  
        protected void ShowData()
        {
            dt = new DataTable();
            con = new SqlConnection(cs);
            con.Open();
            string pstringQuery = "Select u_id, u_seq,u_ques,u_date from Question where FID = @ModuleID;";
            command = new SqlCommand(pstringQuery, con);
            command.Parameters.AddWithValue("@ModuleID", ModuleMenu.SelectedValue);
            command.ExecuteNonQuery();
            SqlDataReader dr = command.ExecuteReader();
            dt.Load(dr);

            Result.DataSource = dt;
            Result.DataBind();

            con.Close();
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
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
            SqlCommand cmd = new SqlCommand("Update Question set u_seq='" + seq_num.Text + "',u_ques='" + ques_num.Text + "' where u_id=" + Convert.ToInt32(ref_num.Text), con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            Result.EditIndex = -1;
            ShowMessage("Your Question have been updated.", MessageType.Info);
            //Call ShowData method for displaying updated data  
            ShowData();
        }

        protected void Result_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            Result.EditIndex = -1;
            ShowMessage("Your update question have been canceled.", MessageType.Warning);
            ShowData();
        }

        protected void Result_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = 0;

            Label ref_num = Result.Rows[e.RowIndex].FindControl("lblReferenceCode") as Label;

            ID = Convert.ToInt32(ref_num.Text);

            if (ID != 0)
            {
                con = new SqlConnection(cs);

                con.Open();
                command = new SqlCommand("Delete from Question Where u_id='" + ID + "'", con);
                command.ExecuteNonQuery();
                
                con.Close();
                ShowMessage("Your Question have been deleted.", MessageType.Error);
                ShowData();
            }
        }
    }
}