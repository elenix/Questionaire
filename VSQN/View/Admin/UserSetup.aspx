<%@ Page Title="User Setup" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserSetup.aspx.cs" Inherits="VSQN.View.Admin.UserSetup" %>

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
                <label for="Username" class="col-md-2 col-form-label" style="padding-right: 0px;">
                    Name :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="Username" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label for="CompanyName" class="col-md-2 col-form-label" style="padding-right: 0px;">
                    Company :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="CompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label for="Email" class="col-md-2 col-form-label" style="padding-right: 0px;">
                    Email :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
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
                            <div class="row" style="margin: 0px;">
                                <div class="col-md-4">
                                    <div class="form-check">
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Admin
                                        </label>
                                        <br />
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Employee
                                        </label>
                                        <br />
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Payroll
                                        </label>
                                        <br />
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Leave
                                        </label>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-check">
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Attendance
                                        </label>
                                        <br />
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Benefit
                                        </label>
                                        <br />
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Import
                                        </label>
                                        <br />
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Statutory
                                        </label>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-check">
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            FSI
                                        </label>
                                        <br />
                                        <label class="form-check-label">
                                            <input class="form-check-input" type="checkbox">
                                            Inventory
                                        </label>
                                        <br />
                                    </div>
                                </div>
                            </div>

                        </asp:View>
                        <asp:View ID="View2" runat="server">
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
