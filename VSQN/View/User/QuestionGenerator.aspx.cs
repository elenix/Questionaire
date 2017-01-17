using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace VSQN.View.User
{
    public partial class QuestionGenerator : Page
    {
        string cs = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        SqlConnection _con;
        DataTable _dt;
        SqlCommand _command;
        int _typeOfInput;
        string _fieldTypeEdit;
        string _textAnswer;
        List<string> _optionAnswerText = new List<string>();
        List<int> _optionAnswerID = new List<int>();
        List<int> _checkedOption = new List<int>();

        private enum MessageType { Error };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_role"] != null && Session["user_role"].ToString() == "U")
            {
                Load_Question();

                if (!IsPostBack)
                {
                    ExtractUserTextAnswer();
                    AnswerTextField();
                }

                AnswerOptionField();
            }

            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        }

        protected void Change_Page(object sender, EventArgs e)
        {
            Response.Redirect("~/View/User/QuestionList.aspx");
        }

        protected void Load_Question()
        {
            string query = "SELECT System_FK, Module_FK, Ques, In_Type_FK, Seq_Number from QuestionBank where Ref_Code = @Ref_Cod;";
            string TextData = "Select * from Question_Answer_TextType where Ref_FK = @Ref_Cod ";
            string OptionData = "Select * from Question_Answer_OptionType where Ref_FK = @Ref_Cod ";
            string attachmentData = "Select * from Question_Answer_Attachment where Ref_FK = @Ref_Cod ";

            var refcode = Session["Table_Refcode"];

            //Extract Data from QuestionInfo Table
            using (_con = new SqlConnection(cs))
            {
                using (_command = new SqlCommand(query, _con))
                {
                    _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                    _con.Open();
                    DataTable data = new DataTable();
                    _command.ExecuteNonQuery();
                    SqlDataReader dr = _command.ExecuteReader();
                    data.Load(dr);

                    foreach (DataRow row in data.Rows)
                    {
                        ques_seq.Text = "<h3><b>QUESTION " + row["Seq_Number"].ToString() + ":</b></h3>";
                        QuestionGenerate.Text = row["Ques"].ToString();
                        _typeOfInput = Int32.Parse(row["In_Type_FK"].ToString());
                    }
                    _con.Close();
                }

                if (_typeOfInput == 1 || _typeOfInput == 2) //Extract Question Answer with text type value from  Question_Answer_TextType table
                {
                    using (_command = new SqlCommand(TextData, _con))
                    {
                        _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                        _con.Open();
                        DataTable data = new DataTable();
                        _command.ExecuteNonQuery();
                        SqlDataReader dr = _command.ExecuteReader();
                        data.Load(dr);

                        foreach (DataRow row in data.Rows)
                        {
                            _textAnswer = row["Ans_Default"].ToString();
                            _fieldTypeEdit = row["Field_Type"].ToString();
                        }
                    }
                    _con.Close();
                }

                else if (_typeOfInput == 3 || _typeOfInput == 4) //Extract Question Answer with option type value from  Question_Answer_Option table
                {
                    using (_command = new SqlCommand(OptionData, _con))
                    {
                        _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                        _con.Open();
                        _dt = new DataTable();
                        _command.ExecuteNonQuery();
                        SqlDataReader reader = _command.ExecuteReader();

                        while (reader.Read())
                        {
                            _optionAnswerID.Add(reader.GetInt32(0));
                            _optionAnswerText.Add(reader.GetString(2));
                        }
                    }
                    _con.Close();
                }

                else
                {
                    using (_command = new SqlCommand(attachmentData, _con))
                    {
                        _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                        _con.Open();
                        DataTable data = new DataTable();
                        _command.ExecuteNonQuery();
                        SqlDataReader dr = _command.ExecuteReader();
                        data.Load(dr);

                        foreach (DataRow row in data.Rows)
                        {
                            _fieldTypeEdit = row["doc_type"].ToString();
                        }
                    }
                    _con.Close();
                }
            }
        }

        protected void ExtractUserTextAnswer()
        {
            string TextQuery       = "Select * from User_Answer_Text where user_email = @email and ref_code = @Ref_Cod";
            string attachmentQuery = "Select * from User_Attachment where user_email = @email and ref_code = @Ref_Cod";
            var refcode            = Session["Table_Refcode"];
            var email              = Session["user_email"];

            using (_con = new SqlConnection(cs))
            {
                switch (_typeOfInput)
                {
                    case 1:
                        using (_command = new SqlCommand(TextQuery, _con))
                        {
                            _command.Parameters.AddWithValue("@email", email);
                            _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                            _con.Open();
                            DataTable data = new DataTable();
                            _command.ExecuteNonQuery();
                            SqlDataReader dr = _command.ExecuteReader();
                            data.Load(dr);

                            foreach (DataRow row in data.Rows)
                            {
                                TBUserAnswerBox.Text = row["answer_text"].ToString();
                                Session["_UserTextAnswer"] = row["answer_text"].ToString();
                            }

                            _con.Close();
                        }

                        break;

                    case 2:
                        using (_command = new SqlCommand(TextQuery, _con))
                        {
                            _command.Parameters.AddWithValue("@email", email);
                            _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                            _con.Open();
                            DataTable data = new DataTable();
                            _command.ExecuteNonQuery();
                            SqlDataReader dr = _command.ExecuteReader();
                            data.Load(dr);

                            foreach (DataRow row in data.Rows)
                            {
                                MMUserAnswerBox.Text = row["answer_text"].ToString();
                                Session["_UserTextAnswer"] = row["answer_text"].ToString();
                            }

                            _con.Close();
                        }

                        break;

                    default:
                        using (_command = new SqlCommand(attachmentQuery, _con))
                        {
                            _command.Parameters.AddWithValue("@email", email);
                            _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                            _con.Open();
                            DataTable data = new DataTable();
                            _command.ExecuteNonQuery();
                            SqlDataReader dr = _command.ExecuteReader();
                            data.Load(dr);

                            foreach (DataRow row in data.Rows)
                            {
                                Session["_UserTextAnswer"] = row["path"].ToString();

                                if(Session["_UserTextAnswer"] != null)
                                {
                                    fileUploaded.Visible = true;
                                    fileUploaded.Text = row["path"].ToString().Replace("D:\\Projects\\Visualsolution\\Questionaire\\VSQN\\Attachment", ""); ;
                                }
                            }

                            _con.Close();
                        }

                        break;
                }

            }
        }

        protected void ExtractUserOptionAnswer()
        {
            string OptionQuery = "Select * from User_Answer_Option where user_email = @email and ref_code = @Ref_Cod ORDER BY answer_ID ASC";
            var refcode = Session["Table_Refcode"];
            var email = Session["user_email"];

            using (_con = new SqlConnection(cs))
            {
                using (_command = new SqlCommand(OptionQuery, _con))
                {
                    _command.Parameters.AddWithValue("@email", email);
                    _command.Parameters.AddWithValue("@Ref_Cod", refcode);
                    _con.Open();
                    _command.ExecuteNonQuery();
                    var reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        _checkedOption.Add(reader.GetInt32(3));
                    }
                    _con.Close();
                }
            }
        }

        private void AnswerTextField()
        {
            //For NULL value
            if (_typeOfInput == 0)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
            }

            //For Question with Text Box Answer Data
            else if (_typeOfInput == 1)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;

                switch (_fieldTypeEdit)
                {
                    case "1":
                        TBLabel.Text = "<small>* Please enter in <u>Text</u> only *</small>";
                        break;

                    default:
                        TBLabel.Text = "<small>* Please enter in <u>Numbers</u> only *</small>";
                        break;
                }
            }

            //For Question with Memo Answer Data
            else if (_typeOfInput == 2)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                switch (_fieldTypeEdit)
                {
                    case "1":
                        MMLabel.Text = "<small>* Please enter in <u>Text</u> only *</small>";
                        break;

                    default:
                        MMLabel.Text = "<small>* Please enter in <u>Numbers</u> only *</small>";
                        break;
                }
            }
            else if (_typeOfInput == 5)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                switch (_fieldTypeEdit)
                {
                    case "1":
                        StatusLabel.Text = "<small>* Please upload file in <u>document</u> type only. Ex: .doc, .ppt, .xlxs, .rar, .pdf *</small>";
                        break;

                    default:
                        StatusLabel.Text = "<small>* Please upload file in <u>image</u> type only. Ex: .jpeg, .png, .tiff *</small>";
                        break;
                }

            }
        }

        private void AnswerOptionField()
        {
            //For NULL value
            if (_typeOfInput == 0)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
            }

            //For Question with Rudio Button Answer Data
            else if (_typeOfInput == 3)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                ExtractUserOptionAnswer();
                LoadRBPanel();
            }

            //For Question with Check Box Answer Data
            else if (_typeOfInput == 4)
            {
                TypeOfInputView.ActiveViewIndex = _typeOfInput;
                ExtractUserOptionAnswer();
                LoadCBPanel();
            }
        }

        protected void Default_Answer(object sender, EventArgs e)
        {
            switch (_typeOfInput)
            {
                case 1:
                    TBUserAnswerBox.Text = _textAnswer;
                    break;

                default:
                    MMUserAnswerBox.Text = _textAnswer;
                    break;
            }
        }

        private void LoadRBPanel()
        {
            var t = 1;
            var c = 0;

            foreach (var x in _optionAnswerText)
            {
                panelRB.Controls.Add(new LiteralControl(" <div class='form-inline'>"));
                panelRB.Controls.Add(new LiteralControl("<div class='form-group'>"));

                var RBTemp = new RadioButton { ID = _optionAnswerID[t - 1].ToString() };
                RBTemp.CssClass = "radio";
                RBTemp.GroupName = "radioGroup";

                var labelTemp = new Label
                {
                    ID = "lblRB" + t,
                    Text = x 
                };

                panelRB.Controls.Add(new LiteralControl("<label class='form-check-label'>"));

                if (c < _checkedOption.Count && RBTemp.ID == _checkedOption[c].ToString())
                {
                    RBTemp.Checked = true;
                    panelRB.Controls.Add(RBTemp);
                    c++;
                }
                else
                {
                    panelRB.Controls.Add(RBTemp);
                }
                panelRB.Controls.Add(new LiteralControl("<a> </a>"));
                panelRB.Controls.Add(labelTemp);
                panelRB.Controls.Add(new LiteralControl("</label>"));

                t++;

                panelRB.Controls.Add(new LiteralControl("</div>"));
                panelRB.Controls.Add(new LiteralControl("</div>"));
            }
        }

        private void LoadCBPanel()
        {
            var t = 1;
            var c = 0;

            foreach (var x in _optionAnswerText)
            {
                panelCB.Controls.Add(new LiteralControl(" <div class='form-inline'>"));
                panelCB.Controls.Add(new LiteralControl("<div class='form-group'>"));

                var CBTemp = new CheckBox { ID = _optionAnswerID[t - 1].ToString() };
                CBTemp.CssClass = "checkbox";

                var labelTemp = new Label
                {
                    ID = "lblCB" + t,
                    Text = x
                };

                panelCB.Controls.Add(new LiteralControl("<label class='form-check-label'>"));

                if (c < _checkedOption.Count && CBTemp.ID == _checkedOption[c].ToString())
                {
                    CBTemp.Checked = true;
                    panelCB.Controls.Add(CBTemp);
                    c++;
                }
                else
                {
                    panelCB.Controls.Add(CBTemp);
                }

                panelCB.Controls.Add(new LiteralControl("<a> </a>"));
                panelCB.Controls.Add(labelTemp);
                panelCB.Controls.Add(new LiteralControl("</label>"));

                t++;

                panelCB.Controls.Add(new LiteralControl("</div>"));
                panelCB.Controls.Add(new LiteralControl("</div>"));
            }
        }

        private void ShowMessage(string message, MessageType type)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), "ShowMessage('" + message + "','" + type + "');", true);
        }

        protected void success_save()
        {
            Session["_UserTextAnswer"] = null;
            if (_typeOfInput == 5)
            {
                Session["success_save"] = "Your attachment have been saved!";
            }
            else
            {
                Session["success_save"] = "Your answer have been saved!";
            }
            
            Response.Redirect("QuestionList.aspx");
        }

        protected void Save_Answer(object sender, EventArgs e)
        {
            var systemNo = Session["System"];
            var moduleNo = Session["Module"];
            var refcode  = Session["Table_Refcode"];
            var email    = Session["user_email"];

            string OneSaveQuery = "INSERT INTO User_Answer_Text(user_email,ref_code,answer_text,System_FK,Module_FK) VALUES (@email,@ref_code,@answer_text,@system,@module)";
            string updateQuery  = "update User_Answer_Text set answer_text = @answer_text where user_email = @email and ref_code = @ref_code";
            string MultiSaveQuery   = "INSERT INTO User_Answer_Option(user_email,ref_code,answer_ID,System_FK,Module_FK) VALUES (@email,@ref_code,@answer_ID,@system,@module)";
            string MultiDeleteQuery = "delete from User_Answer_Option where user_email = @email and answer_ID = @answer_ID ";
            string attachmentQuery       = "INSERT INTO User_Attachment(user_email,ref_code,doc_type,path,System_FK,Module_FK) VALUES (@email,@ref_code,@dT,@path,@system,@module)";
            string updateAttachmentQuery = "update User_Attachment set path = @path where user_email = @email and ref_code = @ref_code ";

            using (_con = new SqlConnection(cs))
            {
                switch (_typeOfInput)
                {
                    case 1:

                        #region Textbox validation

                        if (String.IsNullOrWhiteSpace(TBUserAnswerBox.Text))
                        {
                            ShowMessage("Please do not leave the answer box empty.", MessageType.Error);
                        }

                        else if(Session["_UserTextAnswer"] != null)
                        {
                            using (_command = new SqlCommand(updateQuery, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@email", email);
                                _command.Parameters.AddWithValue("@ref_code", refcode);
                                _command.Parameters.AddWithValue("@answer_text", TBUserAnswerBox.Text);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }

                            success_save();
                        }

                        else
                        {
                            using (_command = new SqlCommand(OneSaveQuery, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@email", email);
                                _command.Parameters.AddWithValue("@ref_code", refcode);
                                _command.Parameters.AddWithValue("@answer_text", TBUserAnswerBox.Text);
                                _command.Parameters.AddWithValue("@system", systemNo);
                                _command.Parameters.AddWithValue("@module", moduleNo);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }

                            success_save();
                        } 

                        break;

                    #endregion

                    case 2:

                        #region Memobox validation

                        if (String.IsNullOrWhiteSpace(MMUserAnswerBox.Text))
                        {
                            ShowMessage("Please do not leave the answer box empty.", MessageType.Error);
                        }

                        else if (Session["_UserTextAnswer"] != null)
                        {
                            using (_command = new SqlCommand(updateQuery, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@email", email);
                                _command.Parameters.AddWithValue("@ref_code", refcode);
                                _command.Parameters.AddWithValue("@answer_text", MMUserAnswerBox.Text);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }

                            success_save();
                        }
                        else
                        {
                            using (_command = new SqlCommand(OneSaveQuery, _con))
                            {
                                _con.Open();
                                _command.Parameters.AddWithValue("@email", email);
                                _command.Parameters.AddWithValue("@ref_code", refcode);
                                _command.Parameters.AddWithValue("@answer_text", MMUserAnswerBox.Text);
                                _command.Parameters.AddWithValue("@system", systemNo);
                                _command.Parameters.AddWithValue("@module", moduleNo);
                                _command.ExecuteNonQuery();
                                _con.Close();
                            }

                            success_save();
                        }

                        break;

                    #endregion

                    case 3:

                        #region radiobutton validation
                        List<int> checkedRB = new List<int>();
                        List<int> uncheckedRB = new List<int>();

                        foreach (var child in panelRB.Controls.OfType<RadioButton>())
                        {
                            if (child.Checked)
                            {
                                int id = Convert.ToInt32(child.ID.Trim());
                                checkedRB.Add(id);
                            }
                            else
                            {
                                int id = Convert.ToInt32(child.ID.Trim());
                                uncheckedRB.Add(id);
                            }
                        }

                        if (checkedRB.Count == 0)
                        {
                            ShowMessage("Please atleast choose <b>one</b> of the answer.", MessageType.Error);
                        }
                        else
                        {
                            using (_command = new SqlCommand(MultiSaveQuery, _con))
                            {
                                foreach (int c in checkedRB)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@email", email);
                                    _command.Parameters.AddWithValue("@ref_code", refcode);
                                    _command.Parameters.AddWithValue("@answer_ID", c);
                                    _command.Parameters.AddWithValue("@system", systemNo);
                                    _command.Parameters.AddWithValue("@module", moduleNo);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }

                            using (_command = new SqlCommand(MultiDeleteQuery, _con))
                            {
                                foreach (int x in uncheckedRB)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@email", email);
                                    _command.Parameters.AddWithValue("@answer_ID", x);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }

                            success_save();

                        }

                        break;

                    #endregion

                    case 4:

                        #region checkbox validation

                        List<int> checkedCB = new List<int>();
                        List<int> uncheckedCB = new List<int>();

                        foreach (var child in panelCB.Controls.OfType<CheckBox>())
                        {
                            if (child.Checked)
                            {
                                int id = Convert.ToInt32(child.ID.Trim());
                                checkedCB.Add(id);
                            }
                            else
                            {
                                int id = Convert.ToInt32(child.ID.Trim());
                                uncheckedCB.Add(id);
                            }
                        }

                        List<int> newDifference = checkedCB.Except(_checkedOption).ToList();
                        List<int> oldDifference = uncheckedCB.Except(checkedCB).ToList();

                        if (newDifference.Any())
                        {
                            using (_command = new SqlCommand(MultiSaveQuery, _con))
                            {
                                foreach (int c in newDifference)
                                {
                                    _con.Open();
                                    _command.Parameters.AddWithValue("@email", email);
                                    _command.Parameters.AddWithValue("@ref_code", refcode);
                                    _command.Parameters.AddWithValue("@answer_ID", c);
                                    _command.Parameters.AddWithValue("@system", systemNo);
                                    _command.Parameters.AddWithValue("@module", moduleNo);
                                    _command.ExecuteNonQuery();
                                    _command.Parameters.Clear();
                                    _con.Close();
                                }
                            }

                            if (oldDifference != null)
                            {
                                using (_command = new SqlCommand(MultiDeleteQuery, _con))
                                {
                                    foreach (int x in oldDifference)
                                    {
                                        _con.Open();
                                        _command.Parameters.AddWithValue("@email", email);
                                        _command.Parameters.AddWithValue("@answer_ID", x);
                                        _command.ExecuteNonQuery();
                                        _command.Parameters.Clear();
                                        _con.Close();
                                    }
                                }
                            }

                            success_save();
                        }

                        else
                        {
                            ShowMessage("Please choose <b>atlease one</b> of the answer.", MessageType.Error);
                        }

                        break;

                    #endregion

                    default:

                        #region attachment
                        string lstringXmlPath = HttpContext.Current.Server.MapPath("~/App_Data/config.xml");
                        string filename = Path.GetFileName(FileUploadControl.FileName);
                        string lstringFilePath = "";
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.Load(lstringXmlPath);
                        XmlNode lXmlNodeSingleNode;

                        #region update Attachment path
                        if (Session["_UserTextAnswer"] != null)
                        {
                            if (FileUploadControl.HasFile)
                            {
                                try
                                {
                                    if (_fieldTypeEdit == "1")
                                    {
                                        if (FileUploadControl.PostedFile.ContentType != "image/jpeg" && FileUploadControl.PostedFile.ContentType != "image/png")
                                        {
                                            if (FileUploadControl.PostedFile.ContentLength < 25102400)
                                            {
                                                lXmlNodeSingleNode = xmlDocument.SelectSingleNode("/Configuration/Upload/Path[@name='Documents']");
                                                lstringFilePath = lXmlNodeSingleNode.Attributes["value"].Value + filename;
                                                FileUploadControl.SaveAs(lstringFilePath);

                                                using (_command = new SqlCommand(updateAttachmentQuery, _con))
                                                {
                                                    _con.Open();
                                                    _command.Parameters.AddWithValue("@email", email);
                                                    _command.Parameters.AddWithValue("@ref_code", refcode);
                                                    _command.Parameters.AddWithValue("@path", lstringFilePath);
                                                    _command.ExecuteNonQuery();
                                                    _command.Parameters.Clear();
                                                    _con.Close();
                                                }

                                                success_save();
                                            }
                                            else
                                            {
                                                StatusLabel.Text = "Upload status: The file has to be less than 25 Mb.";
                                            }
                                        }
                                        else
                                        {
                                            StatusLabel.Text = "Upload status: Only .doc, .ppt, .xlxs, and .rar files are accepted.";
                                        }
                                    }

                                    else //_fieldTypeEdit == "2" 
                                    {
                                        if (FileUploadControl.PostedFile.ContentType == "image/jpeg" || FileUploadControl.PostedFile.ContentType == "image/png" || FileUploadControl.PostedFile.ContentType == "image/jpg" || FileUploadControl.PostedFile.ContentType == "image/tiff")
                                        {
                                            if (FileUploadControl.PostedFile.ContentLength < 25102400)
                                            {
                                                lXmlNodeSingleNode = xmlDocument.SelectSingleNode("/Configuration/Upload/Path[@name='Images']");
                                                lstringFilePath = lXmlNodeSingleNode.Attributes["value"].Value + filename;
                                                FileUploadControl.SaveAs(lstringFilePath);

                                                using (_command = new SqlCommand(updateAttachmentQuery, _con))
                                                {
                                                    _con.Open();
                                                    _command.Parameters.AddWithValue("@email", email);
                                                    _command.Parameters.AddWithValue("@ref_code", refcode);
                                                    _command.Parameters.AddWithValue("@path", lstringFilePath);
                                                    _command.ExecuteNonQuery();
                                                    _command.Parameters.Clear();
                                                    _con.Close();
                                                }

                                                success_save();
                                            }
                                            else
                                            {
                                                StatusLabel.Text = "Upload status: The file has to be less than 25 Mb.";
                                            }
                                        }
                                        else
                                        {
                                            StatusLabel.Text = "Upload status: Only .jpeg, .jpg, .png, and .tiff files are accepted.";
                                        }
                                    }
                                }

                                catch (Exception ex)
                                {
                                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                                }
                            }
                        }
                        #endregion

                        #region create new Attachment path
                        else
                        {
                            if (FileUploadControl.HasFile)
                            {
                                try
                                {
                                    if (_fieldTypeEdit == "1")
                                    {
                                        if (FileUploadControl.PostedFile.ContentType != "image/jpeg" && FileUploadControl.PostedFile.ContentType != "image/png")
                                        {
                                            if (FileUploadControl.PostedFile.ContentLength < 25102400)
                                            {
                                                lXmlNodeSingleNode = xmlDocument.SelectSingleNode("/Configuration/Upload/Path[@name='Documents']");
                                                lstringFilePath = lXmlNodeSingleNode.Attributes["value"].Value + filename;
                                                FileUploadControl.SaveAs(lstringFilePath);

                                                using (_command = new SqlCommand(attachmentQuery, _con))
                                                {
                                                    _con.Open();
                                                    _command.Parameters.AddWithValue("@email", email);
                                                    _command.Parameters.AddWithValue("@ref_code", refcode);
                                                    _command.Parameters.AddWithValue("@dT", Convert.ToInt32(_fieldTypeEdit));
                                                    _command.Parameters.AddWithValue("@path", lstringFilePath);
                                                    _command.Parameters.AddWithValue("@system", systemNo);
                                                    _command.Parameters.AddWithValue("@module", moduleNo);
                                                    _command.ExecuteNonQuery();
                                                    _command.Parameters.Clear();
                                                    _con.Close();
                                                }

                                                success_save();
                                            }
                                            else
                                            {
                                                StatusLabel.Text = "Upload status: The file has to be less than 25 Mb.";
                                            }
                                        }
                                        else
                                        {
                                            StatusLabel.Text = "Upload status: Only .doc, .ppt, .xlxs, and .rar files are accepted.";
                                        }
                                    }

                                    else //_fieldTypeEdit == "2" 
                                    {
                                        if (FileUploadControl.PostedFile.ContentType == "image/jpeg" || FileUploadControl.PostedFile.ContentType == "image/png" || FileUploadControl.PostedFile.ContentType == "image/jpg" || FileUploadControl.PostedFile.ContentType == "image/tiff")
                                        {
                                            if (FileUploadControl.PostedFile.ContentLength < 25102400)
                                            {
                                                lXmlNodeSingleNode = xmlDocument.SelectSingleNode("/Configuration/Upload/Path[@name='Images']");
                                                lstringFilePath = lXmlNodeSingleNode.Attributes["value"].Value + filename;
                                                FileUploadControl.SaveAs(lstringFilePath);

                                                using (_command = new SqlCommand(attachmentQuery, _con))
                                                {
                                                    _con.Open();
                                                    _command.Parameters.AddWithValue("@email", email);
                                                    _command.Parameters.AddWithValue("@ref_code", refcode);
                                                    _command.Parameters.AddWithValue("@dT", Convert.ToInt32(_fieldTypeEdit));
                                                    _command.Parameters.AddWithValue("@path", lstringFilePath);
                                                    _command.Parameters.AddWithValue("@system", systemNo);
                                                    _command.Parameters.AddWithValue("@module", moduleNo);
                                                    _command.ExecuteNonQuery();
                                                    _command.Parameters.Clear();
                                                    _con.Close();
                                                }

                                                success_save();
                                            }
                                            else
                                            {
                                                StatusLabel.Text = "Upload status: The file has to be less than 25 Mb.";
                                            }
                                        }
                                        else
                                        {
                                            StatusLabel.Text = "Upload status: Only .jpeg, .jpg, .png, and .tiff files are accepted.";
                                        }
                                    }
                                }

                                catch (Exception ex)
                                {
                                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                                }
                            }
                        }
                        #endregion

                        break;
                        #endregion
                }
            }
        }
    }
}