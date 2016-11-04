<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="QuestionaireS.aspx.cs" Inherits="Questionaire._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container-fluid">
                <div class="jumbotron" style="margin-top: 30px;">
                    <form>
                    <div class="form-group row">
                        <label for="example-text-input" class="col-xs-3 col-form-label">
                            Reference Code:</label>
                        <div class="col-xs-8">
                            <input class="form-control" type="text" value="Auto-Generate(Unique)" id="example-text-input">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-text-input" class="col-xs-3 col-form-label">
                            Sequence:</label>
                        <div class="col-xs-8">
                            <input class="form-control" type="text" value="" id="Text1">
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="example-text-input" class="col-xs-3 col-form-label">
                            Question:<br />
                            <small>(Content)</small></label>
                        <div class="col-xs-8">
                            <input class="form-control" type="text" value="Type Here.." id="Text2">
                            <asp:Button ID="Button1" runat="server" Text="Add Question" CssClass="btn btn-info form-inline" Style="margin-top:10px; width:100%" />
                        </div>
                    </div><br />
                    </form>
                    <div class="container">
                    <table id="question-table" class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Reference code</th>                                                                   
                                <th>Seq</th>                                
                                <th>Question </th>
                                <th>Option</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1000000111</td>
                                <td>1</td> 
                                <td>Please list down you company headcount :</td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Edit" CssClass="btn btn-success" />
                                    <asp:Button ID="Button3" runat="server" Text="Delete" CssClass="btn btn-danger"/> 
                                </td>        
                            </tr>
                            <tr>
                               <td>1000000112</td>
                               <td>2</td> 
                               <td>Please list down you company headcount :</td>
                               <td>
                                    <asp:Button ID="Button4" runat="server" Text="Edit" CssClass="btn btn-success" />
                                    <asp:Button ID="Button5" runat="server" Text="Delete" CssClass="btn btn-danger"/> 
                                </td>           
                            </tr>
                            <tr>
                               <td>1000000113</td>
                               <td>3</td> 
                               <td>Please list down you company headcount :</td>
                               <td>
                                    <asp:Button ID="Button6" runat="server" Text="Edit" CssClass="btn btn-success" />
                                    <asp:Button ID="Button7" runat="server" Text="Delete" CssClass="btn btn-danger"/> 
                                </td>           
                            </tr>
                        </tbody>
                    </table>
                </div>
        </div>
    </div>
</asp:Content>
