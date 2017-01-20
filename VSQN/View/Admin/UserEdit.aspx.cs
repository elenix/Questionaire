using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class UserEdit : Page
    {
        string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection _con;
        DataTable _dt;
        SqlCommand _command;
        SqlDataReader dr;
        readonly List<string> _hrmSmodule = new List<string>();
        readonly List<string> _esSmodule  = new List<string>();
        readonly List<string> _hrsSmodule = new List<string>();
        readonly List<string> _saaSmodule = new List<string>();
        List<int> _UserHrmSmodule = new List<int>();
        List<int> _UserEsSmodule  = new List<int>();
        List<int> _UserHrsSmodule = new List<int>();
        List<int> _UserSaaSmodule = new List<int>();

        private enum MessageType { Success, Error };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExtractData();
            }

            HideFunction();
        }

        public void HideFunction()
        {
            string userRole = Session["Edit_UserRole"].ToString();

            if(userRole == "A")
            {
                userName.Enabled = false;
                companyId.Enabled = false;
                newEmail.Enabled = false;
                newPassword.Enabled = false;
                companyModule.Visible = false;
            }

            else
            {
                ExtractUserModule();
                ExtractModule();
                LoadHrmsPanel();
                LoadEssPanel();
                LoadHrssPanel();
                LoadSaasPanel();
            }
        }

        public void ExtractData()
        {
            const string query = "Select * from UserAuth where email = @userEmail";
            var email = Session["Edit_UserEmail"];
            string status;

            //Extract Data from UserAuth Table
            using (_con = new SqlConnection(cs))
            {
                using (_command = new SqlCommand(query, _con))
                {
                    _command.Parameters.AddWithValue("@userEmail", email);
                    _con.Open();
                    _dt = new DataTable();
                    _command.ExecuteNonQuery();
                    dr = _command.ExecuteReader();
                    _dt.Load(dr);

                    foreach (DataRow row in _dt.Rows)
                    {
                        status = row["status"].ToString();

                        if(status == "E")
                        {
                            userStatus.SelectedIndex = 0;
                        }
                        else
                        {
                            userStatus.SelectedIndex = 1;
                        }

                        userName.Text = row["username"].ToString();
                        companyId.Text = row["Company"].ToString();
                        newEmail.Text = row["email"].ToString();
                        newPassword.Text = Decrypt(row["password"].ToString());
                    }
                    _con.Close();
                }
            }
        }

        public void ExtractUserModule()
        {
            const string userHrms = "Select * from HRMS_User_Info where User_Email = @userEmail ORDER BY Module_number ASC";
            const string userEss  = "Select * from ESS_User_Info where User_Email = @userEmail ORDER BY Module_number ASC";
            const string userHrss = "Select * from HRSS_User_Info where User_Email = @userEmail ORDER BY Module_number ASC";
            const string userSaas = "Select * from SAAS_User_Info where User_Email = @userEmail ORDER BY Module_number ASC";

            using (_con = new SqlConnection(cs))
            {
                using (_command = new SqlCommand(userHrms, _con))
                {
                    _command.Parameters.AddWithValue("@userEmail", newEmail.Text);
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
                    _command.Parameters.AddWithValue("@userEmail", newEmail.Text);
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
                    _command.Parameters.AddWithValue("@userEmail", newEmail.Text);
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
                    _command.Parameters.AddWithValue("@userEmail", newEmail.Text);
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

        public void ExtractModule()
        {
            const string selectHrms = "Select * from HRMS_module";
            const string selectEss  = "Select * from ESS_module";
            const string selectHrss = "Select * from HRSS_module";
            const string selectSaas = "Select * from SAAS_module";

            using (_con = new SqlConnection(cs))
            {
                using (_command = new SqlCommand(selectHrms, _con))
                {
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _hrmSmodule.Add(reader.GetString(1));
                    }
                    _con.Close();
                }

                using (_command = new SqlCommand(selectEss, _con))
                {
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _esSmodule.Add(reader.GetString(1));
                    }
                    _con.Close();
                }

                using (_command = new SqlCommand(selectHrss, _con))
                {
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _hrsSmodule.Add(reader.GetString(1));
                    }
                    _con.Close();
                }

                using (_command = new SqlCommand(selectSaas, _con))
                {
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _saaSmodule.Add(reader.GetString(1));
                    }
                    _con.Close();
                }
            }
   
        }

        private void LoadHrmsPanel()
        {
            var t = 1;
            var c = 0;
            

            foreach (var t1 in _hrmSmodule)
            {
                panelHRMS.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                panelHRMS.Controls.Add(new LiteralControl("<div class='form-check'>"));

                var chkTemp = new CheckBox { ID = "chkHRMS" + t };

                var labelTemp = new Label
                {
                    ID = "lblHRMS" + t,
                    Text = _hrmSmodule[t - 1].Replace("Module", "")
                };

                panelHRMS.Controls.Add(new LiteralControl("<label class='form-check-label'>"));

                if (c < _UserHrmSmodule.Count && t == _UserHrmSmodule[c])
                {
                    chkTemp.Checked = true;
                    panelHRMS.Controls.Add(chkTemp);
                    c++;    
                }
                else
                {
                    panelHRMS.Controls.Add(chkTemp);
                }

                panelHRMS.Controls.Add(new LiteralControl("  "));
                panelHRMS.Controls.Add(labelTemp);
                panelHRMS.Controls.Add(new LiteralControl("</label>"));

                t++;

                panelHRMS.Controls.Add(new LiteralControl("</div>"));
                panelHRMS.Controls.Add(new LiteralControl("</div>"));
            }
        }

        private void LoadEssPanel()
        {
            var t = 1;
            var c = 0;
            

            // ReSharper disable once UnusedVariable
            foreach (var t1 in _esSmodule)
            {
                panelESS.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                panelESS.Controls.Add(new LiteralControl("<div class='form-check'>"));

                var chkTemp = new CheckBox { ID = "chkESS" + t };

                var labelTemp = new Label
                {
                    ID = "lblESS" + t,
                    Text = _esSmodule[t - 1]
                };

                panelESS.Controls.Add(new LiteralControl("<label class='form-check-label'>"));

                if (c < _UserEsSmodule.Count && t == _UserEsSmodule[c])
                {
                    chkTemp.Checked = true;
                    panelESS.Controls.Add(chkTemp);
                    c++;
                }
                else
                {
                    panelESS.Controls.Add(chkTemp);
                }

                panelESS.Controls.Add(new LiteralControl("  "));
                panelESS.Controls.Add(labelTemp);
                panelESS.Controls.Add(new LiteralControl("</label>"));
                panelESS.Controls.Add(new LiteralControl("<br />"));
                t++;

                panelESS.Controls.Add(new LiteralControl("</div>"));
                panelESS.Controls.Add(new LiteralControl("</div>"));
            }
        }

        private void LoadHrssPanel()
        {
            var t = 1;
            var c = 0;


            foreach (var t1 in _hrsSmodule)
            {
                panelHRSS.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                panelHRSS.Controls.Add(new LiteralControl("<div class='form-check'>"));

                var chkTemp = new CheckBox { ID = "chkHRSS" + t };

                var labelTemp = new Label
                {
                    ID = "lblHRSS" + t,
                    Text = _hrsSmodule[t - 1]
                };

                panelHRSS.Controls.Add(new LiteralControl("<label class='form-check-label'>"));

                if (c < _UserHrsSmodule.Count && t == _UserHrsSmodule[c])
                {
                    chkTemp.Checked = true;
                    panelHRSS.Controls.Add(chkTemp);
                    c++;
                }
                else
                {
                    panelHRSS.Controls.Add(chkTemp);
                }

                panelHRSS.Controls.Add(new LiteralControl("  "));
                panelHRSS.Controls.Add(labelTemp);
                panelHRSS.Controls.Add(new LiteralControl("</label>"));

                t++;

                panelHRSS.Controls.Add(new LiteralControl("</div>"));
                panelHRSS.Controls.Add(new LiteralControl("</div>"));
            }
        }

        private void LoadSaasPanel()
        {
            var t = 1;
            var c = 0;


            foreach (var t1 in _saaSmodule)
            {
                panelSAAS.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                panelSAAS.Controls.Add(new LiteralControl("<div class='form-check'>"));

                var chkTemp = new CheckBox { ID = "chkSAAS" + t };

                var labelTemp = new Label
                {
                    ID = "lblSAAS" + t,
                    Text = _saaSmodule[t - 1].Replace("Module", "")
                };

                panelSAAS.Controls.Add(new LiteralControl("<label class='form-check-label'>"));

                if (c < _UserSaaSmodule.Count && t == _UserSaaSmodule[c])
                {
                    chkTemp.Checked = true;
                    panelSAAS.Controls.Add(chkTemp);
                    c++;
                }
                else
                {
                    panelSAAS.Controls.Add(chkTemp);
                }

                panelSAAS.Controls.Add(new LiteralControl("  "));
                panelSAAS.Controls.Add(labelTemp);
                panelSAAS.Controls.Add(new LiteralControl("</label>"));

                t++;

                panelSAAS.Controls.Add(new LiteralControl("</div>"));
                panelSAAS.Controls.Add(new LiteralControl("</div>"));
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

        private void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);

        }

        protected void button_update(object sender, EventArgs e)
        {
            string userRole = Session["Edit_UserRole"].ToString();
            string status;

            if (userStatus.SelectedIndex == 0)
            {
                status = "E";
            }
            else
            {
                status = "D";
            }

            #region updateQuery
            const string updatequery = "update UserAuth set username = @Username, Company = @company, password = @pass, status = @stats where email = @Email ";
            const string addHRMSquery = "insert into HRMS_User_Info (User_Email,Company,Module_number) values (@Email,@company,@module) ";
            const string addESSquery  = "insert into ESS_User_Info (User_Email,Company,Module_number) values (@Email,@company,@module) ";
            const string addHRSSquery = "insert into HRSS_User_Info (User_Email,Company,Module_number) values (@Email,@company,@module) ";
            const string addSAASquery = "insert into SAAS_User_Info (User_Email,Company,Module_number) values (@Email,@company,@module) ";
            #endregion

            #region deleteQuery
            const string deleteHRMSquery = "delete from HRMS_User_Info where User_Email = @Email and Module_number = @module ";
            const string deleteESSquery = "delete from ESS_User_Info where User_Email = @Email and Module_number = @module ";
            const string deleteHRSSquery = "delete from HRSS_User_Info where User_Email = @Email and Module_number = @module ";
            const string deleteSAASquery = "delete from SAAS_User_Info where User_Email = @Email and Module_number = @module ";
            #endregion

            #region EmptyBoxValidation
            if (String.IsNullOrWhiteSpace(userName.Text))
            {
                ShowMessage("Please enter the <b>Username</b>", MessageType.Error);
            }
            else if (String.IsNullOrWhiteSpace(companyId.Text))
            {
                ShowMessage("Please enter the <b>Company Name</b> of Customer", MessageType.Error);
            }
            else if (String.IsNullOrWhiteSpace(newEmail.Text))
            {
                ShowMessage("Please enter the <b>Company Email</b>", MessageType.Error);
            }
            else if (string.IsNullOrWhiteSpace(newPassword.Text))
            {
                ShowMessage("Please enter the <b>Password</b> before proceed", MessageType.Error);
            }
            #endregion

            else
            {
                using (_con = new SqlConnection(cs))
                {
                    using (_command = new SqlCommand(updatequery, _con))
                    {
                        _con.Open();
                        _command.Parameters.AddWithValue("@Email", newEmail.Text);
                        _command.Parameters.AddWithValue("@Username", userName.Text);
                        _command.Parameters.AddWithValue("@company", companyId.Text);
                        _command.Parameters.AddWithValue("@pass", Encrypt(newPassword.Text));
                        _command.Parameters.AddWithValue("@stats", status);
                        _command.ExecuteNonQuery();
                        _con.Close();
                    }

                    if(userRole == "U") {

                        #region update_new_checked_list

                        using (_command = new SqlCommand(addHRMSquery, _con))
                        {
                            List<int> newCheckedlist = new List<int>();

                            foreach (var child in panelHRMS.Controls.OfType<CheckBox>())
                            {
                                if (child.Checked)
                                {
                                    var CheckedID = child.ID.Substring(7).Trim();
                                    newCheckedlist.Add(Convert.ToInt32(CheckedID));
                                }
                            }

                            List<int> difference = newCheckedlist.Except(_UserHrmSmodule).ToList();

                            if (difference != null)
                            {
                                foreach (var x in difference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                    _command.Parameters.AddWithValue("@company", companyId.Text);
                                    _command.Parameters.AddWithValue("@module", x);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }
                        }

                        using (_command = new SqlCommand(addESSquery, _con))
                        {

                            List<int> newCheckedlist = new List<int>();

                            foreach (var child in panelESS.Controls.OfType<CheckBox>())
                            {
                                if (child.Checked)
                                {
                                    var CheckedID = child.ID.Substring(6).Trim();
                                    newCheckedlist.Add(Convert.ToInt32(CheckedID));
                                }
                            }

                            List<int> difference = newCheckedlist.Except(_UserEsSmodule).ToList();

                            if (difference != null)
                            {
                                foreach (var x in difference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                    _command.Parameters.AddWithValue("@company", companyId.Text);
                                    _command.Parameters.AddWithValue("@module", x);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }
                        }

                        using (_command = new SqlCommand(addHRSSquery, _con))
                        {
                            List<int> newCheckedlist = new List<int>();

                            foreach (var child in panelHRSS.Controls.OfType<CheckBox>())
                            {
                                if (child.Checked)
                                {
                                    var CheckedID = child.ID.Substring(7).Trim();
                                    newCheckedlist.Add(Convert.ToInt32(CheckedID));
                                }
                            }

                            List<int> difference = newCheckedlist.Except(_UserHrsSmodule).ToList();

                            if (difference != null)
                            {
                                foreach (var x in difference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                    _command.Parameters.AddWithValue("@company", companyId.Text);
                                    _command.Parameters.AddWithValue("@module", x);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }
                        }

                        using (_command = new SqlCommand(addSAASquery, _con))
                        {
                            List<int> newCheckedlist = new List<int>();

                            foreach (var child in panelSAAS.Controls.OfType<CheckBox>())
                            {
                                if (child.Checked)
                                {
                                    var CheckedID = child.ID.Substring(7).Trim();
                                    newCheckedlist.Add(Convert.ToInt32(CheckedID));
                                }
                            }

                            List<int> difference = newCheckedlist.Except(_UserSaaSmodule).ToList();

                            if (difference != null)
                            {
                                foreach (var x in difference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                    _command.Parameters.AddWithValue("@company", companyId.Text);
                                    _command.Parameters.AddWithValue("@module", x);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }
                        }

                        #endregion

                        #region delete_new_unchecked_list

                        using (_command = new SqlCommand(deleteHRMSquery, _con))
                        {
                            List<int> newCheckedlist = new List<int>();
                            List<int> newUncheckedlist = new List<int>();

                            foreach (var child in panelHRMS.Controls.OfType<CheckBox>())
                            {
                                if (child.Checked)
                                {
                                    var CheckedID = child.ID.Substring(7).Trim();
                                    newCheckedlist.Add(Convert.ToInt32(CheckedID));
                                }
                                else
                                {
                                    var unCheckedID = child.ID.Substring(7).Trim();
                                    newUncheckedlist.Add(Convert.ToInt32(unCheckedID));
                                }
                            }

                            List<int> difference = newUncheckedlist.Except(newCheckedlist).ToList();

                            if (difference != null)
                            {
                                foreach (var y in difference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                    _command.Parameters.AddWithValue("@module", y);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }
                        }

                        using (_command = new SqlCommand(deleteESSquery, _con))
                        {
                            List<int> newCheckedlist = new List<int>();
                            List<int> newUncheckedlist = new List<int>();

                            foreach (var child in panelESS.Controls.OfType<CheckBox>())
                            {
                                if (child.Checked)
                                {
                                    var CheckedID = child.ID.Substring(6).Trim();
                                    newCheckedlist.Add(Convert.ToInt32(CheckedID));
                                }
                                else
                                {
                                    var unCheckedID = child.ID.Substring(6).Trim();
                                    newUncheckedlist.Add(Convert.ToInt32(unCheckedID));
                                }
                            }

                            List<int> difference = newUncheckedlist.Except(newCheckedlist).ToList();

                            if (difference != null)
                            {
                                foreach (var y in difference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                    _command.Parameters.AddWithValue("@module", y);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }
                        }

                        using (_command = new SqlCommand(deleteHRSSquery, _con))
                        {
                            List<int> newCheckedlist = new List<int>();
                            List<int> newUncheckedlist = new List<int>();

                            foreach (var child in panelHRSS.Controls.OfType<CheckBox>())
                            {
                                if (child.Checked)
                                {
                                    var CheckedID = child.ID.Substring(7).Trim();
                                    newCheckedlist.Add(Convert.ToInt32(CheckedID));
                                }
                                else
                                {
                                    var unCheckedID = child.ID.Substring(7).Trim();
                                    newUncheckedlist.Add(Convert.ToInt32(unCheckedID));
                                }
                            }

                            List<int> difference = newUncheckedlist.Except(newCheckedlist).ToList();

                            if (difference != null)
                            {
                                foreach (var y in difference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                    _command.Parameters.AddWithValue("@module", y);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }
                        }

                        using (_command = new SqlCommand(deleteSAASquery, _con))
                        {
                            List<int> newCheckedlist = new List<int>();
                            List<int> newUncheckedlist = new List<int>();

                            foreach (var child in panelSAAS.Controls.OfType<CheckBox>())
                            {
                                if (child.Checked)
                                {
                                    var CheckedID = child.ID.Substring(7).Trim();
                                    newCheckedlist.Add(Convert.ToInt32(CheckedID));
                                }
                                else
                                {
                                    var unCheckedID = child.ID.Substring(7).Trim();
                                    newUncheckedlist.Add(Convert.ToInt32(unCheckedID));
                                }
                            }

                            List<int> difference = newUncheckedlist.Except(newCheckedlist).ToList();

                            if (difference != null)
                            {
                                foreach (var y in difference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                    _command.Parameters.AddWithValue("@module", y);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }
                        }

                        #endregion
                    }
                }                         
            }

            Session["update_message"] = "The User Data have been updated.";
            Response.Redirect("~/View/Admin/UserList.aspx");
        }

        protected void button_cancel(object sender, EventArgs e)
        {
            Session["cancel_edit"] = "You have cancelled from changing User Data.";
            Response.Redirect("~/View/Admin/UserList.aspx");
        }

        private static string Encrypt(string clearText)
        {
            string EncryptionKey = "VISUALSOLUTION1991";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                if (encryptor != null)
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "VISUALSOLUTION1991";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                if (encryptor != null)
                {
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            return cipherText;
        }
    }
}