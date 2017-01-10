<%@ Page Title="Question List" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AnswerList.aspx.cs" Inherits="VSQN.View.Admin.AnswerList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                        <asp:GridView ID="ResultAnswerList" runat="server" AllowPaging="true" AllowSorting="false" AutoGenerateColumns="False" PageSize="10"
                            EmptyDataText="No Questions Available. Please choose other Module" OnRowEditing="ResultAnswerList_RowAnswering" OnPageIndexChanging="Result_PageIndexChanging"
                            OnRowDataBound="ResultAnswerList_RowDataBound" OnSorting="Result_Sorting" CssClass="table table-striped table-bordered table-hover" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:BoundField DataField="Seq_Number" HeaderText="Seq." ItemStyle-Width="5%" />
                                <asp:BoundField DataField="Ref_Code" HeaderText="Reference" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Ques" HeaderText="Question" ItemStyle-Width="55%" />
                                <asp:BoundField HeaderText="Status" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Options" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_seeAnswer" runat="server" Width="100%" Text="SEE ANSWER" CommandName="Edit" CssClass="btn btn-success" />
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
