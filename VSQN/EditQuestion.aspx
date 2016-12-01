<%@ Page Title="Edit Question" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditQuestion.aspx.cs" Inherits="VSQN.EditQuestion" %>
<asp:Content ID="HeadContent1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="HeadContent2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="jumbotron" style="margin-top: 30px;">
            <div class="form-group">
                <div class="form-group row">
                    <div class="form-inline">
                        <label for="Module" class="col-md-2 col-form-label">
                            MODULE :</label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ModuleMenu" runat="server" AutoPostBack="true" CssClass="form-control"
                                ControlStyle-Width="100%">
                            </asp:DropDownList>
                        </div>
                        <label for="example-text-input" class="col-md-3 col-form-label" style="margin-left: 0px;
                            padding-right: 0px; width: 200px">
                            REFERENCE CODE :</label>
                        <div class="col-md-3">
                            <asp:TextBox ID="AutoGenerate" runat="server" CssClass="form-control" Enabled="False" BackColor="Yellow"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="example-text-input" class="col-md-2 col-form-label">
                        QUESTION :<br />
                        <small>(Content)</small></label>
                    <div class="col-md-9">
                        <asp:TextBox ID="EditQues" runat="server" class="form-control" type="text" TextMode="MultiLine" Height="100px"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label" style="padding-right: 0px">
                        TYPE OF INPUT :</label>
                    <div class="col-md-3">
                       <asp:TextBox ID="TypeOfInput" runat="server" CssClass="form-control" Enabled="False" BackColor="Yellow"></asp:TextBox>
                        
                    </div>
                </div>
            <div class="form-group row">
                <div class="col-md-3">
                    <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="btn btn-danger form-inline" OnClick="button_back"
                        Style="margin-top: 10px; margin-left: 50px;" />
                </div>
                <div class="col-md-6">
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnUpdate" runat="server" Text="UPDATE QUESTION" CssClass="btn btn-info form-inline"
                        Style="margin-top: 10px;" />
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
