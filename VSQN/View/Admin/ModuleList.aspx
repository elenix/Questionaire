<%@ Page Title="Module List" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ModuleList.aspx.cs" Inherits="VSQN.View.Admin.ModuleList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="jumbotron" style="margin-top: 20px;">
        <div class="form-group">
            <div class="form-group row">
                <asp:Label ID="CompanyName" runat="server"></asp:Label>
                <hr />
            </div>
        </div>
        <div class="form-group row">
            <asp:UpdatePanel ID="UpdateViewQuestion" runat="server">
                <ContentTemplate>
                    <%--put table here--%>
                    <asp:GridView ID="ResultModuleList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        OnRowDataBound="ResultModuleList_RowDataBound" EmptyDataText="No Data Available. Please choose other system." OnRowEditing="ResultModuleList_RowAnswer"
                        OnSorting="Result_Sorting" CssClass="table table-striped table-bordered table-hover">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Module" ItemStyle-Width="65%" />
                            <asp:BoundField HeaderText="Progress" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Options" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Button ID="btn_View" runat="server" Width="100%" Text="VIEW QUESTION" CommandName="Edit" CssClass="btn btn-info" />
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
        <div class="form-group row">
            <hr />
            <div class="col-md-2 col-form-label">
                <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="Change_Page" CssClass="btn btn-danger btnanim" Width="100%" />
            </div>
        </div>
    </div>
</asp:Content>
