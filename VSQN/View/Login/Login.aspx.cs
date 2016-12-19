using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VSQN.View.Login
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection _con;
        private SqlDataAdapter _adapt;
        private DataTable _dt;
        private SqlCommand _command;
        string _usertype;

        private enum MessageType { Success, Error };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView.ActiveViewIndex = 0;
            }
        }

        protected void Login_Success(object sender, EventArgs e)
        {
            _con = new SqlConnection(_cs);
            string query = "select * from UserAuth where email = @email and password = @pass";
            
            using (_con = new SqlConnection(_cs))
            {
                using (_command = new SqlCommand(query, _con))
                {
                    _command.Parameters.AddWithValue("@email", InputEmail.Text);
                    _command.Parameters.AddWithValue("@pass", Encrypt(userpassword.Text));
                    _adapt = new SqlDataAdapter(_command);
                    _dt = new DataTable();
                    _adapt.Fill(_dt);
                    _con.Open();
                    int i = _command.ExecuteNonQuery(); 
                    _con.Close();

                    if (_dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in _dt.Rows)
                        { 
                            Session["username"] = row["Company"].ToString();
                            _usertype = row["user_role"].ToString();
                        }

                        switch (_usertype)
                        {
                            case "A":
                                Response.Redirect("~/View/Admin/QuesSetup.aspx");
                                break;

                            case "U":
                                Response.Redirect("~/View/User/user.aspx");
                                break;
                        }

                        Session.RemoveAll();
                    }
   
                    else
                    {
                        ShowMessage("You have entered wrong email or password. Please try again", MessageType.Error);
                    }
                }
            }     
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 0;
        }

        protected void Register_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 1;
        }

        private void ShowMessage(string Message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

        }

        protected void Register_New(object sender, EventArgs e)
        {
            _con = new SqlConnection(_cs);
            string query = "insert into UserAuth (username,email,password,user_role,Company) values (@Username, @Email, @Password, 'U', @company)";

            if (String.IsNullOrWhiteSpace(userName.Text))
            {
                ShowMessage("Please enter <b>Username</b> before proceed to next", MessageType.Error);
            }
            else if (String.IsNullOrWhiteSpace(companyId.Text))
            {
                ShowMessage("Please enter <b>Company Name</b> before proceed to next", MessageType.Error);
            }
            else if (String.IsNullOrWhiteSpace(newEmail.Text))
            {
                ShowMessage("Please enter <b>Company Email</b> before proceed to next", MessageType.Error);
            }
            else if (String.IsNullOrWhiteSpace(newPassword.Text))
            {
                ShowMessage("Please enter your <b>Password</b> before proceed to next", MessageType.Error);
            }
            else if (newPassword.Text != confirmPassword.Text)
            {
                ShowMessage("Your <b>Comfirm Password</b> did not match!", MessageType.Error);
            }
            else
            {
                using (_con = new SqlConnection(_cs))
                {
                    using (_command = new SqlCommand(query, _con))
                    {
                        _command.Parameters.AddWithValue("@Username", userName.Text);
                        _command.Parameters.AddWithValue("@Email", newEmail.Text);
                        _command.Parameters.AddWithValue("@Password", Encrypt(newPassword.Text));
                        _command.Parameters.AddWithValue("@company", companyId.Text);
                        _con.Open();
                        _command.ExecuteNonQuery();
                        ShowMessage("You have succesfully registered! Now please Login", MessageType.Success);
                        MultiView.ActiveViewIndex = 0;
                    }
                }
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