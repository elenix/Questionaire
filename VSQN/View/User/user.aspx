<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="VSQN.View.User.user" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/icon/favicon.png" rel="Shortcut Icon" type="image/x-icon" />
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Alert.css" rel="stylesheet" />
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Scripts/Alert.js"></script>
    <script type="text/javascript" src="../../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-1.9.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <div class="header">
                <div class="clear hideSkiplink">
                    <asp:Menu ID="HeaderMenu" runat="server" CssClass="menutop" EnableViewState="false"
                        IncludeStyleBlock="false" Orientation="Horizontal">
                        <Items>
                            <asp:MenuItem Text="LOG OUT" NavigateUrl="~/View/Login/Login.aspx"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </div>
            </div>
        <div class="container">
    <h1>this is user view</h1>
            </div>
    </div>
    </form>
</body>
</html>
