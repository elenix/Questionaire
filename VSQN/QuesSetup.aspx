<%@ Page Title="Question Setup" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="QuesSetup.aspx.cs" Inherits="VSQN.QuesSetup" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
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
                        <label for="example-text-input" class="col-md-3 col-form-label" style="margin-left:0px; padding-right:0px"">
                            REFERENCE CODE :</label>
                        <div class="col-md-3">
                            <input class="form-control" type="text" value="AUTO-GENERATE(UNIQUE)" id="example-text-input"
                                disabled="disabled" style="width:100%; background-color: #FFFF00; font-family:Courier;" />
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="example-text-input" class="col-md-2 col-form-label">
                        QUESTION :<br />
                        <small>(Content)</small></label>
                    <div class="col-md-9">
                        <asp:TextBox ID="ques" runat="server" class="form-control" type="text"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="example-text-input" class="col-md-2 col-form-label">
                        SEQUENCE :</label>
                    <div class="col-md-3">
                        <asp:TextBox ID="seq_ques" runat="server" class="form-control" type="text"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="TOI" class="col-md-2 col-form-label" style="padding-right:0px">
                        TYPE OF INPUT :</label>
                    <div class="col-md-3">
                        <select class="form-control" id="TOI">
                            <option>Text Box</option>
                            <option>Memo</option>
                            <option>Radio Button</option>
                            <option>Check Box</option>
                        </select>
                    </div>
                </div>
                <%--<div class="form-group row">
                    <label for="FT" class="col-xs-3 col-form-label">
                        Field type:</label>
                    <div class="col-xs-8">
                        <select class="form-control" id="FT">
                            <option>Text</option>
                            <option>Numeric</option>
                        </select>
                    </div>
                </div>--%>
                <%--<div class="form-group row">
                    <label for="FL" class="col-xs-3 col-form-label">
                        Field Label:</label>
                    <div class="col-xs-8">
                        <select class="form-control" id="FL">
                            <option>Yes</option>
                            <option>No</option>
                        </select>
                    </div>
                </div>--%>
                <%--<div class="form-group row">
                    <label for="DV" class="col-xs-3 col-form-label">
                        Default Value:</label>
                    <div class="col-xs-8">
                        <select class="form-control" id="DV">
                            <option>Yes</option>
                            <option>No</option>
                        </select>
                        <asp:Button ID="btnCreate" runat="server" Text="Add Question" CssClass="btn btn-info form-inline"
                            Style="margin-top: 10px; width: 100%" OnClick="btnCreate_Click" />
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
