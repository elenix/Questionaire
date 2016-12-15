<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VSQN.View.Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/icon/favicon.png" rel="Shortcut Icon" type="image/x-icon" />
    <link href="../../Styles/Login.css" rel="stylesheet" />
    <link href="../../Styles/Alert.css" rel="stylesheet" />
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/Alert.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
    <script src="../../Scripts/jquery-1.9.1.min.js"></script>
    <title>Login page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <div class="hideSkiplink">
            </div>
        </div>
        
        <div class="container">
            <div class="messagealert" id="alert_container">
        </div>
            <div class="row">
                <div class="col-md-6">
                    <%--visual solution logo--%> 
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/icon/ca.png" Width="100%" />
                </div>
                <div class="col-md-6">
                    <div class="jumbotron">
                        <asp:MultiView ID="MultiView" runat="server" ActiveViewIndex="0">
                            <asp:View ID="LoginView" runat="server">
                                <h2>Log In</h2>
                                <hr class="page-line" />
                                <div class="form-group">
                                    <label for="InputEmail">Email address</label>
                                    <asp:TextBox ID="InputEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                                    <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
                                </div>
                                <div class="form-group">
                                    <label for="userpassword">Password</label>
                                    <asp:TextBox ID="userpassword" runat="server" placeholder="Enter Your Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnlogin" runat="server" Text="Login" CssClass="btn btn-success" OnClick="Login_Success" />
                                <asp:Button ID="btnregister" runat="server" Text="Register" CssClass="btn btn-warning" OnClick="Register_Click" />
                            </asp:View>
                            <asp:View ID="RegisterView" runat="server">
                                <h2>Register</h2>
                                <hr class="page-line" />
                                <div class="form-group">
                                    <label for="userName">User Name</label>
                                    <asp:TextBox ID="userName" runat="server" placeholder="Enter user name" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="companyId">Company</label>
                                    <asp:TextBox ID="companyId" runat="server" placeholder="Enter company name" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="newEmail">Email address</label>
                                    <asp:TextBox ID="newEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="newPassword">Password</label>
                                    <asp:TextBox ID="newPassword" runat="server" placeholder="Enter Your Password" CssClass="form-control" TextMode="Password" Width="300px"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="confirmPassword">Confirm Password</label>
                                    <asp:TextBox ID="confirmPassword" runat="server" placeholder="Enter Again Your Password" CssClass="form-control" TextMode="Password" Width="300px"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnnewregister" runat="server" Text="Register" CssClass="btn btn-warning" OnClick="Register_New" />
                                <asp:LinkButton ID="HyperLink1" runat="server" OnClick="Login_Click">Already have an account?</asp:LinkButton>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
