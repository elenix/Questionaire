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
        List<int> _UserHrmSmodule = new List<int>();
        List<int> _UserEsSmodule  = new List<int>();

        private enum MessageType { Success, Error };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExtractData();
            }

            ExtractUserModule();
            ExtractModule();
            LoadHrmsPanel();
            LoadEssPanel();
        }

        public void ExtractData()
        {
            const string query = "Select * from UserAuth where email = @userEmail";
            var email = Session["userEmail"];

            //Extract Data from UserAuth Table
            using(_con = new SqlConnection(cs))
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
                        userName.Text = row["username"].ToString();
                        companyId.Text = row["Company"].ToString();
                        newEmail.Text = row["email"].ToString();
                        newPassword.Text = row["password"].ToString();
                    }
                    _con.Close();
                }
            }
        }

        public void ExtractUserModule()
        {
            const string userHrms = "Select * from HRMS_User_Info where User_Email = @userEmail";
            const string userEss = "Select * from ESS_User_Info where User_Email = @userEmail";

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
                        _UserHrmSmodule.Add(reader.GetInt32(2));
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
                        _UserEsSmodule.Add(reader.GetInt32(2));
                    }
                    _con.Close();

                }
            }

            SortData();
        }

        public void ExtractModule()
        {
            const string selectHrms = "Select * from HRMS_module";
            const string selectEss = "Select * from ESS_module";

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
            }
   
        }

        public void SortData()
        {
            _UserHrmSmodule = _UserHrmSmodule.OrderBy(i => i).ToList();
            _UserEsSmodule = _UserEsSmodule.OrderBy(i => i).ToList();
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

        protected void button_decrypt(object sender, EventArgs e)
        {
            newPassword.Text = Decrypt(newPassword.Text);
        }

        private void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);

        }

        protected void button_update(object sender, EventArgs e)
        {
            const string updatequery = "update UserAuth set username = @Username, Company = @company where email = @Email ";
            const string addHRMSquery = "insert into HRMS_User_Info (User_Email,Module_number) values (@Email, @module) ";
            const string addESSquery = "insert into ESS_User_Info (User_Email,Module_number) values (@Email, @module) ";
            const string deleteHRMSquery = "delete from HRMS_User_Info where User_Email = @Email and Module_number = @module ";
            const string deleteESSquery = "delete from ESS_User_Info where User_Email = @Email and Module_number = @module ";
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
                        _command.ExecuteNonQuery();
                        _con.Close();
                    }
                    

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

                        if(difference != null)
                        {
                            foreach (var x in difference)
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@Email", newEmail.Text);
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

                    #endregion
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