using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class UserSetup : System.Web.UI.Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection _con;
        SqlDataAdapter _adapt;
        DataTable _dt;
        SqlCommand _command;
        readonly List<string> _hrmSmodule = new List<string>();
        readonly List<string> _esSmodule = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
                ExtractData();
                LoadHrmsPanel();
                LoadEssPanel();   
        }

        private void LoadHrmsPanel()
        {
            int t = 1;

            foreach (string t1 in _hrmSmodule)
            {
                panelHRMS.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                panelHRMS.Controls.Add(new LiteralControl("<div class='form-check'>"));

               
                CheckBox chkTemp = new CheckBox {ID = "chkHRMS" + t};

                Label labelTemp = new Label
                {
                    ID = "lblHRMS" + t,
                    Text = _hrmSmodule[t - 1].Replace("Module","")
                };

                panelHRMS.Controls.Add(new LiteralControl("<label class='form-check-label'>"));
                panelHRMS.Controls.Add(chkTemp);
                panelHRMS.Controls.Add(new LiteralControl("  "));
                panelHRMS.Controls.Add(labelTemp);
                panelHRMS.Controls.Add(new LiteralControl("</label>"));
                //panelHRMS.Controls.Add(new LiteralControl("<br />"));
                t++;
                

                panelHRMS.Controls.Add(new LiteralControl("</div>"));
                panelHRMS.Controls.Add(new LiteralControl("</div>"));
            }
        }

        private void LoadEssPanel()
        {
            int t = 1;

            foreach (string t1 in _esSmodule)
            {
                panelESS.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                panelESS.Controls.Add(new LiteralControl("<div class='form-check'>"));

                CheckBox chkTemp = new CheckBox {ID = "chkESS" + t};

                Label labelTemp = new Label
                {
                    ID = "lblESS" + t,
                    Text = _esSmodule[t - 1]
                };

                panelESS.Controls.Add(new LiteralControl("<label class='form-check-label'>"));
                panelESS.Controls.Add(chkTemp);
                panelESS.Controls.Add(new LiteralControl("  "));
                panelESS.Controls.Add(labelTemp);
                panelESS.Controls.Add(new LiteralControl("</label>"));
                panelESS.Controls.Add(new LiteralControl("<br />"));
                t++;

                panelESS.Controls.Add(new LiteralControl("</div>"));
                panelESS.Controls.Add(new LiteralControl("</div>"));
            }
        }

        #region menu

        protected void LinkHRMS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            ButtonHRMS.CssClass = "btnsetup active";
            ButtonESS.CssClass = "btnsetup";
            ButtonHRSS.CssClass = "btnsetup";
            ButtonSAAS.CssClass = "btnsetup";
        }

        protected void LinkESS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            ButtonHRMS.CssClass = "btnsetup";
            ButtonESS.CssClass = "btnsetup active";
            ButtonHRSS.CssClass = "btnsetup";
            ButtonSAAS.CssClass = "btnsetup";
        }

        protected void LinkHRSS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            ButtonHRMS.CssClass = "btnsetup";
            ButtonESS.CssClass = "btnsetup";
            ButtonHRSS.CssClass = "btnsetup active";
            ButtonSAAS.CssClass = "btnsetup";
        }

        protected void LinkSAAS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            ButtonHRMS.CssClass = "btnsetup";
            ButtonESS.CssClass = "btnsetup";
            ButtonHRSS.CssClass = "btnsetup";
            ButtonSAAS.CssClass = "btnsetup active";
        }

        #endregion //menu

        private void ExtractData()
        {
            _con = new SqlConnection(_cs);
            string selectHRMS = "Select * from Module";
            string selectESS = "Select * from ESS_module";

            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(selectHRMS, _con))
                {
                    _con.Open();
                    _dt = new DataTable();
                    _command.ExecuteNonQuery();
                    SqlDataReader reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _hrmSmodule.Add(reader.GetString(1));
                    }
                }
                _con.Close();

                using (_command = new SqlCommand(selectESS, _con))
                {
                    _con.Open();
                    _dt = new DataTable();
                    _command.ExecuteNonQuery();
                    SqlDataReader reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _esSmodule.Add(reader.GetString(1));
                    }
                }
                _con.Close();

            }
        }

        protected void btn_save(object sender, EventArgs e)
        {
            string name = Username.Text.Trim();
            string comapny = CompanyName.Text.Trim();
            string email = Email.Text.Trim();
            //For HRMS check box
        }
    }
}