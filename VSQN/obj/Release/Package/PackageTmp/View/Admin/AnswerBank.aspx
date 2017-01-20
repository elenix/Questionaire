<%@ Page Title="User List" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AnswerBank.aspx.cs" Inherits="VSQN.View.AnswerBank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<div class="jumbotron" style="margin-top: 20px;">
		<div class="form-group">
			<div class="form-group row">
				<label class="col-md-2 col-form-label" style="font-size: large; margin-left:0">
					System :</label>
				<div class="col-md-4">
					<asp:DropDownList ID="SystemList" runat="server" AutoPostBack="true" CssClass="form-control"
						ControlStyle-Width="100%" OnSelectedIndexChanged="SystemList_SelectedIndexChanged">
					</asp:DropDownList>
				</div>
			</div>
			<br />
			<div class="form-group row">
				<asp:UpdatePanel ID="UpdateViewQuestion" runat="server">
					<ContentTemplate>
						<%--put table here--%>
						<asp:GridView ID="ResultAnswerList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
							PageSize="10" OnRowEditing="ResultUserList_RowEditing" EmptyDataText="No Users Available." OnPageIndexChanging="Result_PageIndexChanging"
							OnSorting="Result_Sorting" CssClass="table table-striped table-bordered table-hover" PagerStyle-CssClass="pgr">
							<Columns>
								<asp:TemplateField HeaderText="No." HeaderStyle-Width="5%">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Company Name" HeaderStyle-Width="40%" SortExpression="Company">
									<ItemTemplate>
										<asp:Label ID="lblCompany" runat="server" Text='<%#Eval("Company") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Email" HeaderStyle-Width="33%">
									<ItemTemplate>
										<asp:Label ID="lblemail" runat="server" Text='<%#Eval("User_Email") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Options" HeaderStyle-Width="22%">
									<ItemTemplate>
										<asp:Button ID="btn_Edit" runat="server" Text="View User Answer" CommandName="Edit" CssClass="btn btn-info" Width="100%" />
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
