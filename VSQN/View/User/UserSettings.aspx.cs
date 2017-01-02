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

namespace VSQN.View.User
{
    public partial class UserSettings : Page
    {
        string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection _con;
        DataTable _dt;
        SqlCommand _command;
        SqlDataReader dr;

        private enum MessageType { Success, Error };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ExtractData();
            }
        }

        private void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);

        }

        public void ExtractData()
        {
            const string query = "Select * from UserAuth where email = @userEmail";
            var email = Session["user_email"];

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
                        userName.Text = row["username"].ToString();
                        userCompany.Text = row["Company"].ToString();
                        userEmail.Text = row["email"].ToString();
                        userPassword.Text = row["password"].ToString();
                    }
                    _con.Close();
                }
            }

        }

        protected void button_decrypt(object sender, EventArgs e)
        {
                userPassword.Text = Decrypt(userPassword.Text);
                btnDecrypt.Enabled = false;     
        }

        protected void button_changePassword(object sender, EventArgs e)
        {
            MultiViewPassword.ActiveViewIndex = 1;
        }

        protected void button_savePassword(object sender, EventArgs e)
        {
            string updateQuery = "update UserAuth set username = @Username, password = @Password where email = @email";

            if ((String.IsNullOrWhiteSpace(userName.Text)))
            {
                ShowMessage("Please enter your <b>New Username</b>", MessageType.Error);
            }

            else if ((String.IsNullOrWhiteSpace(newPassword.Text)))
            {
                ShowMessage("Please enter your <b>New Password</b>", MessageType.Error);
            }

            else if (matchPassword.Text != newPassword.Text)
            {
                ShowMessage("Your re-enter password did not match!", MessageType.Error);
            }

            else
            {
                using (_con = new SqlConnection(cs))
                {
                    using (_command = new SqlCommand(updateQuery, _con))
                    {
                        _con.Open();
                        _command.Parameters.AddWithValue("@Username", userName.Text);
                        _command.Parameters.AddWithValue("@Password", Encrypt(newPassword.Text.Trim()));
                        _command.Parameters.AddWithValue("@email", userEmail.Text);
                        _command.ExecuteNonQuery();
                        _command.Parameters.Clear();
                        _con.Close();
                    }
                }

                ShowMessage("Your <b>new username and password</b> have been saved!", MessageType.Success);
                Response.Redirect("~/View/User/UserSettings.aspx");
            }
        }

        protected void button_cancelPassword(object sender, EventArgs e)
        {
            MultiViewPassword.ActiveViewIndex = 0;
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