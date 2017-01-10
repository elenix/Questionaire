using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Linq;

namespace VSQN.View.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection _con;
        SqlCommand _command;
        List<int> _UserHrmSmodule = new List<int>();
        List<int> _UserEsSmodule  = new List<int>();
        List<int> _UserHrsSmodule = new List<int>();
        List<int> _UserSaaSmodule = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && Session["user_role"].ToString() == "U")
            {
                if (!IsPostBack)
                {
                    if (Session["username"] != null)
                    {
                        string username = HttpContext.Current.Session["username"].ToString();
                        welcomemsg.Text = "Welcome, " + (char.ToUpper(username[0]) + username.Substring(1));
                    }

                    ExtractData();
                    SetMenu();
                }
            }
            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        }

        protected void ExtractData()
        {
            const string userHrms = "Select * from HRMS_User_Info where User_Email = @userEmail ORDER BY Module_number ASC";
            const string userEss  = "Select * from ESS_User_Info where User_Email  = @userEmail ORDER BY Module_number ASC";
            const string userHrss = "Select * from HRSS_User_Info where User_Email = @userEmail ORDER BY Module_number ASC";
            const string userSaas = "Select * from SAAS_User_Info where User_Email = @userEmail ORDER BY Module_number ASC";
            string email = Session["user_email"].ToString();

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(userHrms, _con))
                {
                    _command.Parameters.AddWithValue("@userEmail", email);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _UserHrmSmodule.Add(reader.GetInt32(3));
                    }
                    _con.Close();
                }

                using (_command = new SqlCommand(userEss, _con))
                {
                    _command.Parameters.AddWithValue("@userEmail", email);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _UserEsSmodule.Add(reader.GetInt32(3));
                    }
                    _con.Close();
                }

                using (_command = new SqlCommand(userHrss, _con))
                {
                    _command.Parameters.AddWithValue("@userEmail", email);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _UserHrsSmodule.Add(reader.GetInt32(3));
                    }
                    _con.Close();
                }

                using (_command = new SqlCommand(userSaas, _con))
                {
                    _command.Parameters.AddWithValue("@userEmail", email);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _UserSaaSmodule.Add(reader.GetInt32(3));
                    }
                    _con.Close();
                }
            }
        }

        protected void SetMenu()
        {
            if(!_UserHrmSmodule.Any())
            {
                menuHRMS.Style.Add("display", "none");
            }

            if (!_UserEsSmodule.Any())
            {
                menuESS.Style.Add("display", "none");

            }

            if (!_UserHrsSmodule.Any())
            {
                menuHRSS.Style.Add("display", "none");

            }

            if (!_UserSaaSmodule.Any())
            {
                menuSAAS.Style.Add("display", "none");

            }
        }

    }
}