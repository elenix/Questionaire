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
                            <label for="Module" class="col-md-2 col-form-label" style="font-size:x-large">
                                Module:</label>
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
                                PageSize="10" OnRowCancelingEdit="Result_RowCancelingEdit" OnRowEditing="Result_RowEditing" OnPageIndexChanging="Result_PageIndexChanging" emptydatatext="No Data Available. Please choose other Module" 
                                OnRowUpdating="Result_RowUpdating" OnRowDeleting="Result_RowDeleting" OnSorting="Result_Sorting" CssClass="table table-striped table-bordered table-hover" PagerStyle-CssClass="pgr">
                                <Columns>
                                    <asp:TemplateField HeaderText="Reference Code" HeaderStyle-Width="15%" SortExpression="Ref_Code"> 
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferenceCode" runat="server" Text='<%#Eval("Ref_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Question" HeaderStyle-Width="55%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuestion" runat="server" Text='<%#Eval("Ques") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQuestion" runat="server" Text='<%#Eval("Ques") %>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date/Time" HeaderStyle-Width="10%" SortExpression="Date_Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTime" runat="server" Text='<%#Eval("Date_Time"/*, "{0:M-dd-yyyy}"*/) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Options" HeaderStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Button ID="btn_Edit" runat="server" Text="EDIT" CommandName="Edit" CssClass="btn btn-success" />
                                            <asp:Button ID="btn_Delete" runat="server" Text="DELETE" CommandName="Delete" CssClass="btn btn-danger" OnClientClick="return confirm('Do you want to delete this row?');" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button ID="btn_Update" runat="server" Text="UPDATE" CommandName="Update" CssClass="btn btn-info" />
                                            <asp:Button ID="btn_Cancel" runat="server" Text="CANCEL" CommandName="Cancel" CssClass="btn btn-warning" />
                                        </EditItemTemplate>
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
            </div>
        <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ModuleMenu" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
 