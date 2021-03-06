﻿<%@ Page Title="SAAS page" Language="C#" MasterPageFile="~/View/User/User.Master" AutoEventWireup="true" CodeBehind="UserSAAS.aspx.cs" Inherits="VSQN.View.User.SAAS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContentPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="jumbotron" style="margin-top: 20px;">
        <div class="form-group">
            <div class="form-group row">
                <h2 style="padding-left: 15px;">Select The Module</h2>
                <hr />
            </div>
            <div class="form-group row">
                <asp:UpdatePanel ID="UpdateViewQuestion" runat="server">
                    <ContentTemplate>
                        <%--put table here--%>
                        <asp:GridView ID="ResultSAASList" runat="server" AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="ResultUserList_RowDataBound"
                            OnRowEditing="ResultUserList_RowAnswering" EmptyDataText="No Data Available. Please choose other system."
                            OnSorting="Result_Sorting" CssClass="table table-striped table-bordered table-hover">
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="SAAS Module" ItemStyle-Width="60%" />
                                <asp:BoundField HeaderText="Progress" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
                                <asp:TemplateField HeaderText="Options" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_Answer" runat="server" Width="100%" Text="START" CommandName="Edit" CssClass="btn btn-info" />
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
