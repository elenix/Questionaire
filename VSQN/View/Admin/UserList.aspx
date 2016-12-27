<%@ Page Title="User List" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="VSQN.View.Admin.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="jumbotron" style="margin-top: 20px;">
        <div class="form-group">
            <div class="form-group row">
                <h3 style="padding-left: 15px;">LIST OF THE USERS</h3>
                <hr />
            </div>
            <div class="form-group row">
                <asp:UpdatePanel ID="UpdateViewQuestion" runat="server">
                    <ContentTemplate>
                        <%--put table here--%>
                        <asp:GridView ID="ResultUserList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            PageSize="10" OnRowEditing="ResultUserList_RowEditing" EmptyDataText="No Data Available. Please choose other Module" OnPageIndexChanging="Result_PageIndexChanging"
                            OnRowDeleting="ResultUserList_RowDeleting" OnSorting="Result_Sorting" CssClass="table table-striped table-bordered table-hover" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="Company Name" HeaderStyle-Width="25%" SortExpression="Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompany" runat="server" Text='<%#Eval("Company") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name" HeaderStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" HeaderStyle-Width="28%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Options" HeaderStyle-Width="22%">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_Edit" runat="server" Text="EDIT" CommandName="Edit" CssClass="btn btn-success" />
                                        <asp:Button ID="btn_Delete" runat="server" Text="DELETE" CommandName="Delete" CssClass="btn btn-danger" OnClientClick="return confirm('Do you want to delete this row?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#CCCCCC" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
