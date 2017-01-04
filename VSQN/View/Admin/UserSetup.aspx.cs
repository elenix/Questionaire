﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class UserSetup : Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection _con;
        SqlCommand _command;
        readonly List<string> _hrmSmodule = new List<string>();
        readonly List<string> _esSmodule = new List<string>();

        private enum MessageType { Success, Error };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null)
            {
                ExtractData();
                LoadHrmsPanel();
                LoadEssPanel();
            }

            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        }

        private void ExtractData()
        {
            _con = new SqlConnection(_cs);
            const string selectHrms = "Select * from HRMS_module";
            const string selectEss = "Select * from ESS_module";

            using (_con = new SqlConnection(_cs))
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
                }
                _con.Close();

                using (_command = new SqlCommand(selectEss, _con))
                {
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _esSmodule.Add(reader.GetString(1));
                    }
                }
                _con.Close();

            }
        }

        private void LoadHrmsPanel()
        {
            var t = 1;

            // Extract user info from HRMS_Module based on User_email.
            foreach (var x in _hrmSmodule)
            {
                panelHRMS.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                panelHRMS.Controls.Add(new LiteralControl("<div class='form-check'>"));

                var chkTemp = new CheckBox {ID = "c"+ t};

                var labelTemp = new Label
                {
                    ID = "lblHRMS" + t,
                    Text = x.Replace("Module", "")
                };

                panelHRMS.Controls.Add(new LiteralControl("<label class='form-check-label'>"));
                panelHRMS.Controls.Add(chkTemp);
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

            // Extract user info from ESS_Module based on User_email.
            foreach (var x in _esSmodule)
            {
                panelESS.Controls.Add(new LiteralControl("<div class='col-md-4'>"));
                panelESS.Controls.Add(new LiteralControl("<div class='form-check'>"));

                var chkTemp = new CheckBox {ID = "c" + t};

                var labelTemp = new Label
                {
                    ID = "lblESS" + t,
                    Text = x
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

        private void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        protected void btn_UncheckAll(object sender, EventArgs e)
        {
            foreach (var child in panelHRMS.Controls.OfType<CheckBox>())
            {
                child.Checked = false;
            }

            foreach (var child in panelESS.Controls.OfType<CheckBox>())
            {
                child.Checked = false;
            }
        }

        protected void btn_CheckAll(object sender, EventArgs e)
        {
            foreach (var child in panelHRMS.Controls.OfType<CheckBox>())
            {
                child.Checked = true;
            }

            foreach (var child in panelESS.Controls.OfType<CheckBox>())
            {
                child.Checked = true;
            }
        }

        protected void btn_create(object sender, EventArgs e)
        {
            string query = "insert into UserAuth (username,email,password,user_role,Company) values (@Username, @Email, @Password, 'U', @company)";
            string HRMSquery = "insert into HRMS_User_Info (User_Email,Module_number) values (@Email, @module) ";
            string ESSquery = "insert into ESS_User_Info (User_Email,Module_number) values (@Email, @module) ";

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
            else if (confirmPassword.Text != newPassword.Text)
            {
                ShowMessage("The password entered did not match with the first password!", MessageType.Error);
            }

            #endregion

            else
            {
                using (_con = new SqlConnection(_cs))
                {
                    using (_command = new SqlCommand(query, _con))
                    {
                        _con.Open();
                        _command.Parameters.AddWithValue("@Username", userName.Text);
                        _command.Parameters.AddWithValue("@Email", newEmail.Text);
                        _command.Parameters.AddWithValue("@Password", Encrypt(newPassword.Text));
                        _command.Parameters.AddWithValue("@company", companyId.Text);
                        _command.ExecuteNonQuery();
                        _con.Close();
                    }

                    using (_command = new SqlCommand(HRMSquery, _con))
                    {
                        foreach (var child in panelHRMS.Controls.OfType<CheckBox>())
                        {
                            if (child.Checked)
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                _command.Parameters.AddWithValue("@module", child.ID.Substring(1).Trim());
                                _command.ExecuteNonQuery();
                                _command.Parameters.Clear();
                                _con.Close();
                            }
                        }
                    }

                    using (_command = new SqlCommand(ESSquery, _con))
                    {
                        foreach (var child in panelESS.Controls.OfType<CheckBox>())
                        {
                            if (child.Checked)
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@Email", newEmail.Text);
                                _command.Parameters.AddWithValue("@module", child.ID.Substring(1).Trim());
                                _command.ExecuteNonQuery();
                                _command.Parameters.Clear();
                                _con.Close();
                            }
                        }
                    }
                }

                ClearAll();
                ShowMessage("The Account for <b>" + userName.Text + "</b> Have Been Created!", MessageType.Success);
            }
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

        private void ClearAll()
        {
            userName.Text = string.Empty;
            newEmail.Text = string.Empty;
            companyId.Text = string.Empty;
        }
    }
}