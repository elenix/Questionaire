<%@ Page Title="Questionaire" Language="C#" MasterPageFile="~/View/User/User.Master" AutoEventWireup="true" CodeBehind="QuestionGenerator.aspx.cs" Inherits="VSQN.View.User.QuestionGenerator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="UserContentPlaceHolder" runat="server">
    <div class="jumbotron" style="margin-top: 20px;">
        <div class="form-group row">
            <div class="col-md-3 col-form-label">
                <asp:Label ID="ques_seq" runat="server" Text="Label"></asp:Label>
            </div>
            <hr />
        </div>
        <div class="form-group row">
            <div class="col-md-8" style="margin-bottom: 20px;">
                <asp:Label ID="QuestionGenerate" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
            </div>
        </div>
        <%--Type Of Input Area--%>
        <asp:MultiView ID="TypeOfInputView" runat="server" ActiveViewIndex="0">
            <%--NULL VALUE--%>
            <asp:View ID="ViewEmpty" runat="server"></asp:View>
            <%--FOR TEXT BOX VIEW--%>
            <asp:View ID="ViewTB" runat="server">
                <div class="form-group row">
                    <div class="col-md-8">
                        <asp:TextBox ID="TBUserAnswerBox" runat="server" class="form-control" type="text" placeholder="Please Enter Your Answer Here"></asp:TextBox>
                        <asp:Label ID="TBLabel" runat="server" Text="Label" ForeColor="Gray"></asp:Label>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-3" style="padding-right: 0">
                        <asp:Button runat="server" ID="TBbtnDefault" Text="Default Answer" OnClick="Default_Answer" CssClass="btn btn-info" Width="100%" />
                    </div>
                    <div class="col-md-3" style="padding-left: 5px">
                        <asp:Button runat="server" ID="TBbtnSave" Text="Save" OnClick="Save_Answer" CssClass="btn btn-success btnanim" Width="100%" />
                    </div>
                </div>
            </asp:View>
            <%--FOR MEMO VIEW--%>
            <asp:View ID="ViewMM" runat="server">
                <div class="form-group row">
                    <div class="col-md-8">
                        <asp:TextBox ID="MMUserAnswerBox" runat="server" class="form-control" type="text" TextMode="MultiLine" placeholder="Please Enter Your Answer Here"
                            Height="100px"></asp:TextBox>
                        <asp:Label ID="MMLabel" runat="server" Text="Label" ForeColor="Gray"></asp:Label>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-md-3" style="padding-right: 0">
                        <asp:Button runat="server" ID="MMbtnDefault" Text="Default Answer" OnClick="Default_Answer" CssClass="btn btn-info" Width="100%" />
                    </div>
                    <div class="col-md-3" style="padding-left: 5px">
                        <asp:Button runat="server" ID="MMbtnSave" Text="Save" OnClick="Save_Answer" CssClass="btn btn-success btnanim" Width="100%" />
                    </div>
                </div>
            </asp:View>
            <%--FOR RADIO BUTTON--%>
            <asp:View ID="ViewRB" runat="server">
                <div class="form-group row">
                    <label class="col-md-2 col-form-label" style="padding-right: 0px; width: 95px;">
                        ANSWER:</label>
                    <div class="col-md-8">
                        <asp:Panel runat="server" ID="panelRB"></asp:Panel>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label" style="padding-right: 0px; width: 95px;"></label>
                    <div class="col-md-3" style="padding-left: 5px">
                        <asp:Button runat="server" ID="RBbtnSave" Text="Save" OnClick="Save_Answer" CssClass="btn btn-success btnanim" Width="100px" />
                    </div>
                </div>
            </asp:View>
            <%--FOR CHECK BOX--%>
            <asp:View ID="ViewCB" runat="server">
                <div class="form-group row">
                    <label class="col-md-2 col-form-label" style="padding-right: 0px; width: 95px;">
                        ANSWER:</label>
                    <div class="col-md-8">
                        <asp:Panel runat="server" ID="panelCB"></asp:Panel>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-md-2 col-form-label" style="padding-right: 0px; width: 95px;"></label>
                    <div class="col-md-3" style="padding-left: 5px">
                        <asp:Button runat="server" ID="CBbtnSave" Text="Save" OnClick="Save_Answer" CssClass="btn btn-success btnanim" Width="100px" />
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
        <div class="form-group row">
            <hr />
            <div class="col-md-2 col-form-label">
                <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="Change_Page" CssClass="btn btn-danger btnanim" Width="100%" />
            </div>
        </div>
    </div>
</asp:Content>
