using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;

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
                LoadModalMenuDropdown();
                ShowData();
            }

        }

        protected void LoadModalMenuDropdown()
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
            string pstringQuery = "Select Ref_Code, Ques, Date_Time from QuestionInfo where Module_FK = @ModuleID;";
            command = new SqlCommand(pstringQuery, con);
            command.Parameters.AddWithValue("@ModuleID", ModuleMenu.SelectedValue);
            command.ExecuteNonQuery();
            SqlDataReader dr = command.ExecuteReader();
            dt.Load(dr);

            Result.DataSource = dt;
            Result.DataBind();
            ViewState["dirState"] = dt;
            ViewState["sortdr"] = "Asc";

            con.Close();
        }

        protected void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
        }

        protected void Result_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            Session["Row_Ref_Code"] = ((Label)Result.Rows[e.NewEditIndex].FindControl("lblReferenceCode")).Text;
            //Result.EditIndex = e.NewEditIndex;
            //ShowData();
            Response.Redirect("EditQuestion.aspx");
        }

        protected void Result_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            //Finding the controls from Gridview for the row which is going to update  
            Label id = Result.Rows[e.RowIndex].FindControl("lblReferenceCode") as Label;
            TextBox ques = Result.Rows[e.RowIndex].FindControl("txtQuestion") as TextBox;
            con = new SqlConnection(cs);
            con.Open();
            //updating the record  
            SqlCommand cmd = new SqlCommand("Update QuestionInfo set Ques='"+ques.Text+"', Date_Time=GETDATE() where Ref_Code="+Convert.ToInt32(id.Text),con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            Result.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
            ShowMessage("Your question have been updated.", MessageType.Info);
        }

        protected void Result_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            Result.EditIndex = -1;
            ShowData();
            ShowMessage("You have cancelled your question update.", MessageType.Warning);
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
                command = new SqlCommand("Delete from QuestionInfo Where Ref_Code='" + ID + "'", con);
                command.ExecuteNonQuery();
                
                con.Close();
                ShowData();
                ShowMessage("Your Question have been deleted.", MessageType.Error);
            }
        }

        protected void Result_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
               
                }
                else
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
             
                }

                Result.DataSource = dtrslt;
                Result.DataBind();
               
            }

        }

      
    }
}