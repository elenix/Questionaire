<%@ Page Title="User Setup" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="UserSetup.aspx.cs" Inherits="VSQN.View.Admin.UserSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" style="margin-top: 20px; padding-top: 20px">
        <div class="form-group">
            <div class="form-group row">
                <h3 style="padding-left: 15px;">USER SETUP</h3>
                <hr />
            </div>
            <div class="form-group row" style="margin-top: 30px;">
                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                    User Name :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="userName" runat="server" placeholder="Customer Name" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                    Company :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="companyId" runat="server" placeholder="Customer's Company Name" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                    Email :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="newEmail" runat="server" placeholder="ex: company@com.my" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                    Password :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="newPassword" runat="server" placeholder="Enter The Password" CssClass="form-control" TextMode="Password" Width="300px"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-2 col-form-label" style="padding-right: 0;">
                    Confirm Password :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="confirmPassword" runat="server" placeholder="Enter Again The Password" CssClass="form-control" TextMode="Password" Width="300px"></asp:TextBox>
                </div>
            </div>
            <asp:ScriptManager ID="AjaxScriptManager" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="updPanelData" runat="server">
                <ContentTemplate>
                    <div class="form-group row" style="margin-top: 30px;">
                        <ul class="nav nav-pill nav-stacked col-lg-2" style="margin-left: 40px;">
                            <li>
                                <asp:Button ID="ButtonHRMS" runat="server" Text="HRMS" CssClass="btnsetup active" OnClick="LinkHRMS_Click" /></li>
                            <li>
                                <asp:Button ID="ButtonESS" runat="server" Text="ESS" CssClass="btnsetup" OnClick="LinkESS_Click" /></li>
                            <li>
                                <asp:Button ID="ButtonHRSS" runat="server" Text="HRSS" CssClass="btnsetup" OnClick="LinkHRSS_Click" /></li>
                            <li>
                                <asp:Button ID="ButtonSAAS" runat="server" Text="SAAS" CssClass="btnsetup" OnClick="LinkSAAS_Click" /></li>
                        </ul>
                        <div class="setuppage col-lg-9">
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="View1" runat="server">
                                    <div class="row" style="margin: 0;">
                                        <asp:Panel runat="server" ID="panelHRMS"></asp:Panel>
                                    </div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <div class="row" style="margin: 0;">
                                        <asp:Panel runat="server" ID="panelESS"></asp:Panel>
                                    </div>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <div class="row" style="margin: 0;">
                                        <asp:Panel runat="server" ID="panelHRSS"></asp:Panel>
                                    </div>
                                </asp:View>
                                <asp:View ID="View4" runat="server">
                                    <div class="row" style="margin: 0;">
                                        <asp:Panel runat="server" ID="panelSAAS"></asp:Panel>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ButtonHRMS" />
                    <asp:AsyncPostBackTrigger ControlID="ButtonESS" />
                    <asp:AsyncPostBackTrigger ControlID="ButtonHRSS" />
                    <asp:AsyncPostBackTrigger ControlID="ButtonSAAS" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="form-inline col-md-offset-6">
                <asp:Button ID="btnUncheck" runat="server" Text="UNCHECK ALL" CssClass="btn btn-warning" OnClick="btn_UncheckAll" />
                <asp:Button ID="btnCheck" runat="server" Text="CHECK ALL" CssClass="btn btn-info" OnClick="btn_CheckAll" />
                <asp:Button ID="btnsave" runat="server" Text="CREATE" CssClass="btn btn-success" Width="95px" OnClick="btn_create" />
            </div>
        </div>
    </div>
</asp:Content>
