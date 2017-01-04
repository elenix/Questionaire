<%@ Page Title="Question Bank" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="QuestionBank.aspx.cs" Inherits="VSQN.View.Admin.ViewQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="jumbotron" style="margin-top: 20px;">
        <div class="form">
            <div class="row">
                <label class="col-md-2 col-form-label" style="font-size: large; margin-left:0">
                    System :</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="SystemList" runat="server" AutoPostBack="true" CssClass="form-control"
                        ControlStyle-Width="100%" OnSelectedIndexChanged="SystemList_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <label class="col-md-2 col-form-label" style="font-size: large; margin-left:0">
                    Module :</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ModuleMenu" runat="server" AutoPostBack="true" CssClass="form-control"
                        ControlStyle-Width="100%" OnSelectedIndexChanged="ModelMenu_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="form-group row">
                <asp:UpdatePanel ID="UpdateViewQuestion" runat="server">
                    <ContentTemplate>
                        <%--put table here--%>
                        <asp:GridView ID="Result" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            PageSize="10" OnRowEditing="Result_RowEditing" OnPageIndexChanging="Result_PageIndexChanging" EmptyDataText="No Data Available. Please choose other Module"
                            OnRowDeleting="Result_RowDeleting" OnSorting="Result_Sorting" CssClass="table table-striped table-bordered table-hover" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="Reference Code" HeaderStyle-Width="10%" SortExpression="Ref_Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReferenceCode" runat="server" Text='<%#Eval("Ref_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Seq." HeaderStyle-Width="5%" SortExpression="Seq_Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSequence" runat="server" Text='<%#Eval("Seq_Number") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Question" HeaderStyle-Width="50%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("Ques") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQuestion" runat="server" Text='<%#Eval("Ques") %>' Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date/Time" HeaderStyle-Width="13%" SortExpression="Date_Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTime" runat="server" Text='<%#Eval("Date_Time") %>'></asp:Label>
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
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ModuleMenu" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
