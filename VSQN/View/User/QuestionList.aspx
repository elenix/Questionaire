<%@ Page Title="Question List" Language="C#" MasterPageFile="~/View/User/User.Master" AutoEventWireup="true" CodeBehind="QuestionList.aspx.cs" Inherits="VSQN.View.User.QuestionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContentPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="jumbotron" style="margin-top: 20px;">
        <div class="form-group">
            <div class="form-group row">
                <h2 style="padding-left: 15px;">Select The Question</h2>
                <hr />
            </div>
            <div class="form-group row">
                <asp:UpdatePanel ID="UpdateViewQuestion" runat="server">
                    <ContentTemplate>
                        <%--put table here--%>
                        <asp:GridView ID="ResultQuestionList" runat="server" AllowSorting="false" AutoGenerateColumns="False"
                            EmptyDataText="No Questions Available. Please choose other Module" OnRowEditing="ResultUserList_RowAnswering"
                            OnSorting="Result_Sorting" CssClass="table table-striped table-bordered table-hover">
                            <Columns>
                                <asp:BoundField DataField="Seq_Number" HeaderText="Seq." ItemStyle-Width="5%" />
                                <asp:BoundField DataField="Ref_Code" HeaderText="Reference" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Ques" HeaderText="Question" ItemStyle-Width="55%" />
                                <asp:BoundField HeaderText="Status" ItemStyle-Width="10%" />
                                <asp:TemplateField HeaderText="Options" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_Answer" runat="server" Width="100%" Text="ANSWER" CommandName="Edit" CssClass="btn btn-success" />
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
                <asp:Button runat="server" ID="btnBack" Text="BACK" CssClass="btn btn-danger" OnClick="Page_Back" Width="100%"/>
            </div>
                </div>
        </div>
        </div>
</asp:Content>
