<%@ Page Title="View Question" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ViewQuestion.aspx.cs" Inherits="VSQN.ViewQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdateViewQuestion" runat="server">
        <ContentTemplate>--%>
            <div class="container-fluid">
                <div class="jumbotron" style="margin-top: 30px;">
                    <div class="form">
                        <div class="form-group row">
                            <label for="Module" class="col-xs-2 col-form-label" style="font-size:x-large">
                                Module:</label>
                            <div class="col-xs-4">
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
                                PageSize="10" OnRowCancelingEdit="Result_RowCancelingEdit" OnRowEditing="Result_RowEditing" OnPageIndexChanging="Result_PageIndexChanging"
                                OnRowUpdating="Result_RowUpdating" OnRowDeleting="Result_RowDeleting" CssClass="table table-striped table-bordered table-hover" PagerStyle-CssClass="pgr">
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
                                            <asp:TextBox ID="txtSequence" runat="server" Text='<%#Eval("u_seq") %>' Width="50%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Question">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("u_ques") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQuestion" runat="server" Text='<%#Eval("u_ques") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date/Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTime" runat="server" Text='<%#Eval("u_date") %>'></asp:Label>
                                        </ItemTemplate>
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
                        
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ModuleMenu" />
                            </Triggers>
                        </asp:UpdatePanel>

                        </div>
                    </div>
                </div>
            </div>
        <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ModuleMenu" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
