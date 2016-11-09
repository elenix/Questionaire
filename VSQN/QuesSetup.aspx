<%@ Page Title="Question Setup" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="QuesSetup.aspx.cs" Inherits="VSQN.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="container-fluid">
        <div class="jumbotron" style="margin-top: 30px;">
            <div class="form-group">
                <div class="form-group row">
                    <label for="Module" class="col-xs-3 col-form-label">
                        Module:</label>
                    <div class="col-xs-3">
                        <select class="form-control" id="Module">
                            <option>Main Module</option>
                            <option>Employee Module</option>
                            <option>Payroll Module</option>
                            <option>Statutory Reporting Module</option>
                            <option>Attendance Module</option>
                            <option>Leave Module</option>
                            <option>Benefit Module</option>
                            <option>Financial Interface Module</option>
                            <option>Data Import Module</option>
                            <option>Training Module</option>
                            <option>Perfomance Module</option>
                            <option>Staff Inventory Module</option>
                            <option>Transport Module</option>
                            <option>Manpower Module</option>
                            <option>Recruitment Module</option>
                            <option>ESOS Module</option>
                        </select>
                    </div>
                    <label for="example-text-input" class="col-xs-2 col-form-label">
                        Reference Code:</label>
                    <div class="col-xs-3">
                        <input class="form-control" type="text" value="Auto-Generate(Unique)" id="example-text-input" />
                    </div>
                </div>
                <div class="form-group row">
                    <label for="example-text-input" class="col-xs-3 col-form-label">
                        Sequence:</label>
                    <div class="col-xs-8">
                        <!--<input class="form-control" type="text" value="" id="seq_num" /-->
                        <asp:TextBox ID="seq_ques" runat="server" class="form-control" type="text"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="example-text-input" class="col-xs-3 col-form-label">
                        Question:<br />
                        <small>(Content)</small></label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="ques" runat="server" class="form-control" type="text"></asp:TextBox>
                        <asp:Button ID="btnCreate" runat="server" Text="Add Question" CssClass="btn btn-info form-inline"
                            Style="margin-top: 10px; width: 100%" OnClick="btnCreate_Click" />
                    </div>
                </div>
                <br />
            </div>
            <div class="container">

            
                <asp:GridView ID="Result" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    PageSize="10" OnRowCancelingEdit="Result_RowCancelingEdit" OnRowEditing="Result_RowEditing"
                    OnRowUpdating="Result_RowUpdating" OnRowDeleting="Result_RowDeleting" CssClass="table table-striped table-bordered table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="Reference Code">
                            <ItemTemplate>
                                <asp:Label ID="lblReferenceCode" runat="server" Text='<%#Eval("u_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seq">
                            <ItemTemplate>
                                <asp:Label ID="lblSequence" runat="server" Text='<%#Eval("u_seq") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSequence" runat="server" Text='<%#Eval("u_seq") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Question"  ControlStyle-Width="100%">
                            <ItemTemplate>
                                <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("u_ques") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQuestion" runat="server" Text='<%#Eval("u_ques") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Options">
                            <ItemTemplate>
                                <asp:Button ID="btn_Edit" runat="server" Text="EDIT" CommandName="Edit" CssClass="btn btn-success" />
                                <asp:Button ID="btn_Delete" runat="server" Text="DELETE" CommandName="Delete" CssClass="btn btn-danger" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="btn_Update" runat="server" Text="UPDATE" CommandName="Update" CssClass="btn btn-info" />
                                <asp:Button ID="btn_Cancel" runat="server" Text="CANCEL" CommandName="Cancel" CssClass="btn btn-warning" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
