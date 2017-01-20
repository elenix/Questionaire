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
                <div class="col-md-2">
                    <h3 style="margin-top: 0">Filter:</h3>
                </div>
                <div class="col-md-3" style="padding-left:0">
                    <asp:DropDownList ID="userRole" OnSelectedIndexChanged="userRole_SelectedIndexChanged"
                        runat="server" AutoPostBack="True" CssClass="form-control">
                        <asp:ListItem Value="0">All</asp:ListItem>
                        <asp:ListItem Value="1">Admin</asp:ListItem>
                        <asp:ListItem Value="2">Customer</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <h3 style="margin-top: 0">Search:</h3>
                </div>
                <div class="col-md-3" style="padding:0px;">
                    <asp:TextBox ID="nameSearch" runat="server" CssClass="form-control" placeholder="Company name"></asp:TextBox>
                </div>
                <div class="col-md-2" style="padding-left:5px;">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info" onClick="Name_Search" />
                </div>
            </div>
            <div class="form-group row">
                <asp:UpdatePanel ID="UpdateViewQuestion" runat="server">
                    <ContentTemplate>
                        <%--put table here--%>
                        <asp:GridView ID="ResultUserList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            PageSize="10" OnRowEditing="ResultUserList_RowEditing" EmptyDataText="No Data Available. Please create more user." OnPageIndexChanging="Result_PageIndexChanging"
                            OnRowDeleting="ResultUserList_RowDeleting" OnSorting="Result_Sorting" CssClass="table table-striped table-bordered table-hover" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="Company Name" HeaderStyle-Width="20%" SortExpression="Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompany" runat="server" Text='<%#Eval("Company") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lbluserrole" runat="server" Text='<%#Eval("user_role") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusername" runat="server" Text='<%#Eval("username") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email" HeaderStyle-Width="28%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status") %>'></asp:Label>
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
