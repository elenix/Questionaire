<%@ Page Title="Settings" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminSettings.aspx.cs" Inherits="VSQN.View.Admin.AdminSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="AjaxScriptManager" runat="server"></asp:ScriptManager>
    <div class="jumbotron" style="margin-top: 20px;">
        <div class="form-group">
            <div class="form-group row">
                <h3 style="padding-left: 15px;">SETTINGS</h3>
                <hr />
            </div>
            <div class="form-group row" style="margin-top: 30px;">
                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                    Admin Name:</label>
                <div class="col-md-9">
                    <asp:TextBox ID="adminName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                    Company :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="adminCompany" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                    Email :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="adminEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <asp:UpdatePanel ID="updPanelData" runat="server">
                <ContentTemplate>
                    <asp:MultiView ID="MultiViewPassword" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">
                            <div class="form-group row">
                                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                                    Password :</label>
                                <div class="col-md-4" style="padding-right: 5px">
                                    <asp:TextBox ID="adminPassword" runat="server" CssClass="form-control" BackColor="Yellow" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-3" style="padding-left: 0px; padding-right: 5px">
                                    <asp:Button ID="btnDecrypt" runat="server" Text="SEE PASSWORD" CssClass="btn btn-info form-inline" Width="100%" OnClick="button_decrypt" Enabled="true" />
                                </div>
                                <div class="col-md-2" style="padding-left: 0px">
                                    <asp:Button ID="btnChange" runat="server" Text="CHANGE" CssClass="btn btn-success form-inline" Width="100%" OnClick="button_changePassword" />
                                </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="form-group row">
                                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                                    Password :</label>
                                <div class="col-md-4" style="padding-right: 5px">
                                    <asp:TextBox ID="newPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                                    Re-type Password :</label>
                                <div class="col-md-4" style="padding-right: 5px">
                                    <asp:TextBox ID="matchPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="col-md-3" style="padding-left: 0px; padding-right: 5px">
                                    <asp:Button ID="savePass" runat="server" Text="SAVE" CssClass="btn btn-success form-inline" Width="100%" OnClick="button_savePassword" OnClientClick="return confirm('Do you want to save this new password and username?');" />
                                </div>
                                <div class="col-md-2" style="padding-left: 0px">
                                    <asp:Button ID="cancelPass" runat="server" Text="CANCEL" CssClass="btn btn-danger form-inline" Width="100%" OnClick="button_cancelPassword" />
                                </div>
                        </asp:View>
                    </asp:MultiView>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
