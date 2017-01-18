using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace VSQN.View.Login
{
    public partial class Login : Page
    {
        private readonly string _cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        private SqlConnection _con;
        private SqlDataAdapter _adapt;
        private DataTable _dt;
        private SqlCommand _command;
        string _usertype;
        string _status;

        private enum MessageType { Success, Error, Warning };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["user_role"] = null;
                MultiView.ActiveViewIndex = 0;
                Session.RemoveAll();
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
                    //_command.Parameters.AddWithValue("@pass", userpassword.Text);
                    _command.Parameters.AddWithValue("@pass", Encrypt(userpassword.Text));
                    _adapt = new SqlDataAdapter(_command);
                    _dt = new DataTable();
                    _adapt.Fill(_dt);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    _con.Close();

                    if (_dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in _dt.Rows)
                        { 
                            Session["username"] = row["Company"].ToString();
                            Session["user_email"] = row["email"].ToString();
                            Session["user_role"] = row["user_role"].ToString();
                            _usertype = row["user_role"].ToString();
                            _status = row["status"].ToString();
                        }

                        switch (_usertype)
                        {
                            case "A":
                                Response.Redirect("~/View/Admin/QuestionSetup.aspx");
                                break;

                            case "U":
                                if(_status == "E")
                                {
                                    Response.Redirect("~/View/User/UserAbout.aspx");
                                }
                                else
                                {
                                    ShowMessage("You account have been disable by admin.", MessageType.Warning);
                                }

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

        protected void Forgot_Click(object sender, EventArgs e)
        {
            MultiView.ActiveViewIndex = 1;
        }

        private void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, type: GetType(), key: Guid.NewGuid().ToString(), script: "ShowMessage('" + message + "','" + type + "');", addScriptTags: true);

        }

        protected void Register_New(object sender, EventArgs e)
        {
           
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
    }
}