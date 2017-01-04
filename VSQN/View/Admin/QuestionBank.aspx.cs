using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class ViewQuestion : Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection _con;
        private SqlDataAdapter _adapt;
        private SqlCommand _command;
        private DataTable _dt;
        private string _message;

        protected enum MessageType { Success, Error, Info, Warning }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null)
            {
                if (!IsPostBack)
                {
                    LoadSystemListDropwdown();

                    if (Session["update_message"] != null)
                    {
                        _message = Session["update_message"].ToString();
                        ShowMessage(_message, MessageType.Info);
                        Session["update_message"] = null;
                    }
                    else if (Session["create_message"] != null)
                    {
                        _message = Session["create_message"].ToString();
                        ShowMessage(_message, MessageType.Success);
                        Session["create_message"] = null;
                    }
                    else if (Session["cancel_edit"] != null)
                    {
                        _message = Session["cancel_edit"].ToString();
                        ShowMessage(_message, MessageType.Warning);
                        Session["cancel_edit"] = null;
                    }

                    ShowData();
                }
            }
            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }

        }

        private void LoadSystemListDropwdown()
        {
            _con = new SqlConnection(_cs);
            const string com = "Select * from System_List";
            _adapt = new SqlDataAdapter(com, _con);
            _dt = new DataTable();
            _adapt.Fill(_dt);
            SystemList.DataSource = _dt;
            SystemList.DataBind();
            SystemList.DataTextField = "Name";
            SystemList.DataValueField = "ID";
            SystemList.DataBind();
            SystemList.Items.Insert(0, new ListItem("--Select--", "0"));
            SystemList.SelectedIndex = Convert.ToInt32(Session["selected_system"]);

            if(SystemList.SelectedIndex == 1)
            {
                LoadModalMenuDropdown("HRMS_module");
            }

            else if (SystemList.SelectedIndex == 2)
            {
                LoadModalMenuDropdown("ESS_module");
            }

            else if (SystemList.SelectedIndex == 3)
            {
                LoadModalMenuDropdown("HRSS_module");
            }

            else if (SystemList.SelectedIndex == 4)
            {
                LoadModalMenuDropdown("SAAS_module");
            }

            Session["selected_system"] = null;
        }

        protected void SystemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SystemList.SelectedIndex == 1)
            {
                LoadModalMenuDropdown("HRMS_module");
            }
            else if (SystemList.SelectedIndex == 2)
            {
                LoadModalMenuDropdown("ESS_module");    
            }
            else if (SystemList.SelectedIndex == 3)
            {
                LoadModalMenuDropdown("HRSS_module");
            }

            else if (SystemList.SelectedIndex == 4)
            {
                LoadModalMenuDropdown("SAAS_module");
            }

            ShowData();
        }

        private void LoadModalMenuDropdown(string module)
        {
            string ModuleQuery = "";

            if (module == "HRMS_module")
            {
                ModuleQuery = "Select * from HRMS_module ";
            }
            else if (module == "ESS_module")
            {
                ModuleQuery = "Select * from ESS_module ";
            }
            else if (module == "HRSS_module")
            {
                ModuleQuery = "Select * from HRSS_module ";
            }
            else if (module == "SAAS_module")
            {
                ModuleQuery = "Select * from SAAS_module ";
            }

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(ModuleQuery, _con))
                {
                    _con.Open();
                    _adapt = new SqlDataAdapter(_command);
                    _dt = new DataTable();
                    _adapt.Fill(_dt);
                    ModuleMenu.DataSource = _dt;
                    ModuleMenu.DataBind();
                    ModuleMenu.DataTextField = "Name";
                    ModuleMenu.DataValueField = "PK";
                    ModuleMenu.DataBind();
                    ModuleMenu.Items.Insert(0, new ListItem("--Select--", "0"));
                    if(Session["selected_module"] != null)
                    {
                        ModuleMenu.SelectedIndex = Convert.ToInt32(Session["selected_module"]);
                    }                   
                    _con.Close();
                }
            }

            Session["selected_module"] = null;
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
        private void ShowData()
        {
            _dt = new DataTable();
            _con = new SqlConnection(_cs);
            _con.Open();
            string pstringQuery = "Select Ref_Code, Ques, Date_Time, Seq_Number from QuestionBank where System_FK = @SystemID AND Module_FK = @ModuleID ORDER BY Seq_Number ASC";
            _command = new SqlCommand(pstringQuery, _con);
            _command.Parameters.AddWithValue("@SystemID", SystemList.SelectedValue);
            _command.Parameters.AddWithValue("@ModuleID", ModuleMenu.SelectedValue);
            _command.ExecuteNonQuery();
            SqlDataReader dr = _command.ExecuteReader();
            _dt.Load(dr);

            Result.DataSource = _dt;
            Result.DataBind();
            ViewState["dirState"] = _dt;
            ViewState["sortdr"] = "Asc";

            _con.Close();
        }

        protected void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        protected void Result_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["Row_Ref_Code"] = ((Label)Result.Rows[e.NewEditIndex].FindControl("lblReferenceCode")).Text;
            Response.Redirect("EditQuestion.aspx");
        }

        protected void Result_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label refNum = Result.Rows[e.RowIndex].FindControl("lblReferenceCode") as Label;
            string deleteQuery = "Delete from QuestionBank where Ref_Code= @id";
            if (refNum != null)
            {
                var id = Convert.ToInt32(refNum.Text);

                if (id != 0)
                {
                    _con = new SqlConnection(_cs);
                    _con.Open();
                    _command = new SqlCommand(deleteQuery, _con);
                    _command.Parameters.AddWithValue("@id", id);
                    _command.ExecuteNonQuery();
                    _con.Close();
                    ShowData();
                    ShowMessage("Your Question have been deleted.", MessageType.Error);
                }
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