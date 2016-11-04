<%@ Page Title="Input" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Input.aspx.cs" Inherits="VSQN.Input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="jumbotron" style="margin-top: 30px;">
            <form>
            <div class="form-group row">
                <label for="example-text-input" class="col-xs-3 col-form-label">Reference Code:</label>
                <div class="col-xs-8">
                    <input class="form-control" type="text" value="Auto-Generate(Unique)" id="example-text-input">
                </div>
            </div>
            <div class="form-group row">
                <label for="TOI" class="col-xs-3 col-form-label">Type of input:</label>
                <div class="col-xs-8">
                    <select class="form-control" id="TOI">
                        <option>Text Box</option>
                        <option>Memo</option>
                        <option>Radio Button</option>
                        <option>Check Box</option>
                    </select>
                </div>
            </div>
            <div class="form-group row">
                <label for="FT" class="col-xs-3 col-form-label">Field type:</label>
                <div class="col-xs-8">
                    <select class="form-control" id="FT">
                        <option>Text</option>
                        <option>Numeric</option>
                    </select>
                </div>
            </div>
            <div class="form-group row">
                <label for="FL" class="col-xs-3 col-form-label">Field Label:</label>
                <div class="col-xs-8">
                    <select class="form-control" id="FL">
                        <option>Yes</option>
                        <option>No</option>
                    </select>
                </div>
            </div>
            <div class="form-group row">
                <label for="DV" class="col-xs-3 col-form-label">Default Value:</label>
                <div class="col-xs-8">
                    <select class="form-control" id="DV">
                        <option>Yes</option>
                        <option>No</option>
                    </select>
                </div>
            </div>
            <br />
            </form>
            <div class="container">
                <table id="question-table" class="table table-bordered">
                    <thead>
                        <tr>
                            <th>
                                Reference code
                            </th>
                            <th>
                                Seq
                            </th>
                            <th>
                                Question
                            </th>
                            <th>
                                Option
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                1000000111
                            </td>
                            <td>
                                1
                            </td>
                            <td>
                                Please list down you company headcount :
                            </td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Text="Edit" CssClass="btn btn-success" />
                                <asp:Button ID="Button3" runat="server" Text="Delete" CssClass="btn btn-danger" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                1000000112
                            </td>
                            <td>
                                2
                            </td>
                            <td>
                                Please list down you company headcount :
                            </td>
                            <td>
                                <asp:Button ID="Button4" runat="server" Text="Edit" CssClass="btn btn-success" />
                                <asp:Button ID="Button5" runat="server" Text="Delete" CssClass="btn btn-danger" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                1000000113
                            </td>
                            <td>
                                3
                            </td>
                            <td>
                                Please list down you company headcount :
                            </td>
                            <td>
                                <asp:Button ID="Button6" runat="server" Text="Edit" CssClass="btn btn-success" />
                                <asp:Button ID="Button7" runat="server" Text="Delete" CssClass="btn btn-danger" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
