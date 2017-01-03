<%@ Page Title="Question Setup" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="QuesSetup.aspx.cs" Inherits="VSQN.View.Admin.QuesSetup" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <div class="jumbotron" style="margin-top: 20px;">
        <div class="form-group">
            <div class="form-group row">
                <label class="col-md-3 col-form-label">
                    SYSTEM :</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="SystemList" runat="server" AutoPostBack="true" CssClass="form-control"
                        ControlStyle-Width="100%" OnSelectedIndexChanged="SystemList_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-3 col-form-label">
                    MODULE :</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="ModuleMenu" runat="server" AutoPostBack="true" CssClass="form-control"
                        ControlStyle-Width="100%">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-3 col-form-label" style="padding-right: 0;">
                    REFERENCE CODE :</label>
                <div class="col-md-4">
                    <asp:TextBox ID="AutoGenerate" runat="server" CssClass="form-control autogeneratelabel" Enabled="False" placeholder="AUTO-GENERATE(UNIQUE)" BackColor="yellow"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-3 col-form-label" style="padding-right: 0;">
                    SEQUENCE :</label>
                <div class="col-md-4">
                    <asp:TextBox ID="SeqNum" runat="server" CssClass="form-control autogeneratelabel" Width="100px"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-3 col-form-label">
                    QUESTION :<br />
                    <small>(Content)</small></label>
                <div class="col-md-8">
                    <asp:TextBox ID="ques" runat="server" class="form-control" type="text" TextMode="MultiLine" placeholder="Please write your question here.."
                        Height="100px"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label class="col-md-3 col-form-label" style="padding-right: 0">
                    TYPE OF INPUT :</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="TypeOfInput" OnSelectedIndexChanged="TypeOfInput_SelectedIndexChanged"
                        runat="server" AutoPostBack="True" CssClass="form-control">
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">Text Box</asp:ListItem>
                        <asp:ListItem Value="2">Memo</asp:ListItem>
                        <asp:ListItem Value="3">Radio Button</asp:ListItem>
                        <asp:ListItem Value="4">Check Box</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </div>
            </div>

            <%--Type Of Input Area--%>
            <asp:MultiView ID="TypeOfInputView" runat="server" ActiveViewIndex="0">
                <%--NULL VALUE--%>
                <asp:View ID="ViewEmpty" runat="server"></asp:View>
                <%--FOR TEXT BOX VIEW--%>
                <asp:View ID="ViewTB" runat="server">
                    <div class="form-group row">
                        <label class="col-md-3 col-form-label">
                            FIELD TYPE:</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="TBT" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">Text</asp:ListItem>
                                <asp:ListItem Value="2">Numeric</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-3 col-form-label">
                            ANSWER :<br />
                        </label>
                        <div class="col-md-8">
                            <asp:TextBox ID="TBAnswer" runat="server" class="form-control" type="text" placeholder="Place your default answer here.."></asp:TextBox>
                        </div>
                    </div>
                </asp:View>
                <%--FOR MEMO VIEW--%>
                <asp:View ID="ViewMM" runat="server">
                    <div class="form-group row">
                        <label class="col-md-3 col-form-label">
                            FIELD TYPE:</label>
                        <div class="col-md-4">
                            <asp:DropDownList ID="MMT" runat="server" CssClass="form-control">
                                <asp:ListItem Value="1">Text</asp:ListItem>
                                <asp:ListItem Value="2">Numeric</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-md-3 col-form-label">
                            ANSWER :<br />
                        </label>
                        <div class="col-md-8">
                            <asp:TextBox ID="MMAnswer" runat="server" class="form-control" type="text" TextMode="MultiLine" placeholder="Write your default answer here.."
                                Height="100px"></asp:TextBox>
                        </div>
                    </div>
                </asp:View>
                <%--FOR RADIO BUTTON--%>
                <asp:View ID="ViewRB" runat="server">
                    <div class="form-group row">
                        <label class="col-md-3 col-form-label">
                            ANSWER:</label>
                        <div class="col-md-8">
                            <asp:Repeater ID="RepeaterRBBox" runat="server" OnItemCommand="RepeaterRBBox_ItemCommand">
                                <ItemTemplate>
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label class="form-check-label">
                                                <asp:RadioButton ID="RadioButton" runat="server" CssClass="radio" />
                                                <asp:TextBox ID="RBanswer" Text='<%# Eval("RB_BOX").ToString() %>' runat="server"
                                                    CssClass="form-control textbox-anim"></asp:TextBox></label>
                                            <asp:Button ID="btn_Remove" runat="server" Text="REMOVE" CommandName="Remove" CssClass="btn btn-danger btnQS"
                                                OnClientClick="return confirm('Do you want to delete this row?');" />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div style="margin-left: 17px; margin-top: 10px">
                                <asp:Button ID="btnAddRBClick" runat="server" Text="ADD MORE ANSWER" CommandName="Add"
                                    CssClass="btn btn-success thisbtnanim" OnClick="btnAddRB_Click" />
                            </div>
                        </div>
                    </div>
                </asp:View>
                <%--FOR CHECK BOX--%>
                <asp:View ID="ViewCB" runat="server">
                    <div class="form-group row">
                        <label class="col-md-3 col-form-label">
                            ANSWER:</label>
                        <div class="col-md-8">
                            <asp:Repeater ID="RepeaterCBBox" runat="server" OnItemCommand="RepeaterCBBox_ItemCommand">
                                <ItemTemplate>
                                    <div class="form-inline">
                                        <div class="form-group">
                                            <label class="form-check-label">
                                                <asp:CheckBox ID="CheckBox" runat="server" CssClass="checkbox" />
                                                <asp:TextBox ID="CBanswer" Text='<%# Eval("CB_BOX").ToString() %>' runat="server"
                                                    CssClass="form-control textbox-anim"></asp:TextBox>
                                            </label>
                                            <asp:Button ID="btn_Remove" runat="server" Text="REMOVE" CommandName="Remove" CssClass="btn btn-danger btnQS"
                                                OnClientClick="return confirm('Do you want to delete this row?');" />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div style="margin-left: 17px; margin-top: 10px">
                                <asp:Button ID="btnAddCBClick" runat="server" Text="ADD MORE ANSWER" CommandName="Add"
                                    CssClass="btn btn-success thisbtnanim" OnClick="btnAddCB_Click" />
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
        <div class="form-group row">
            <div class="col-md-8">
            </div>
            <div class="col-md-3">
                <asp:Button ID="btnCreate" runat="server" Text="ADD QUESTION" CssClass="btn btn-info form-inline"
                    Style="margin-top: 10px; width: 180px; height: 50px;" OnClick="btnCreate_Click" />
            </div>
        </div>
    </div>

</asp:Content>
