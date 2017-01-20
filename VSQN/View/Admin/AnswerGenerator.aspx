<%@ Page Title="Question & Answer" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AnswerGenerator.aspx.cs" Inherits="VSQN.View.Admin.AnswerGenerator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                        <asp:TextBox ID="TBUserAnswerBox" runat="server" class="form-control" type="text" placeholder="There is no answer yet." Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </asp:View>
            <%--FOR MEMO VIEW--%>
            <asp:View ID="ViewMM" runat="server">
                <div class="form-group row">
                    <div class="col-md-8">
                        <asp:TextBox ID="MMUserAnswerBox" runat="server" class="form-control" type="text" TextMode="MultiLine" placeholder="There is no answer yet." Enabled ="false"
                            Height="100px"></asp:TextBox>
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
            </asp:View>
            <%--FOR ATTACHMENT--%>
                <asp:View ID="ViewAtt" runat="server">
                    <div class="form-group row">
                        <label class="col-md-3 col-form-label">
                            ATTACHMENT:</label>
                        <div class="col-md-8">
                            <div style="background-color:white; padding:20px; width:150px; border:solid">
                                <asp:Image ID="uploadedImage" runat="server" Style="width:100px; height:auto" />
                            </div>
                            <br />
                            <asp:Label runat="server" ID="fileUploaded" Text="" />
                            <br />
                            <br />
                            <asp:Button runat="server" ID="downloadButton" Text="Download" CssClass="btn btn-success"/>
                        </div>
                    </div>
                </asp:View>
        </asp:MultiView>
        <div class="form-group row">
            <hr />
            <div class="col-md-2 col-form-label">
                <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="btnBack_ChangePage" CssClass="btn btn-danger btnanim" Width="100%" />
            </div>
        </div>
    </div>
</asp:Content>
