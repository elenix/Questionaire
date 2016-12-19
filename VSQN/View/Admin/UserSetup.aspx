<%@ Page Title="User Setup" Language="C#" MasterPageFile="~/View/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserSetup.aspx.cs" Inherits="VSQN.View.Admin.UserSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" style="margin-top: 20px; padding-top: 20px">
        <div class="form-group">
            <div class="form-group row">
                <h3 style="padding-left: 15px;">USER SETUP</h3>
                <hr />
            </div>
            <div class="form-group row" style="margin-top: 30px;">
                <label for="Username" class="col-md-2 col-form-label" style="padding-right: 0px;">
                    Name :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="Username" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label for="CompanyName" class="col-md-2 col-form-label" style="padding-right: 0px;">
                    Company :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="CompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group row">
                <label for="Email" class="col-md-2 col-form-label" style="padding-right: 0px;">
                    Email :</label>
                <div class="col-md-9">
                    <asp:TextBox ID="Email" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <asp:ScriptManager ID="AjaxScriptManager" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="updPanelData" runat="server">
                <ContentTemplate>
                    <div class="form-group row" style="margin-top: 30px;">
                        <ul class="nav nav-pill nav-stacked col-lg-2" style="margin-left: 40px;">
                            <li>
                                <asp:Button ID="ButtonHRMS" runat="server" Text="HRMS" CssClass="btnsetup active" OnClick="LinkHRMS_Click" /></li>
                            <li>
                                <asp:Button ID="ButtonESS" runat="server" Text="ESS" CssClass="btnsetup" OnClick="LinkESS_Click" /></li>
                            <li>
                                <asp:Button ID="ButtonHRSS" runat="server" Text="HRSS" CssClass="btnsetup" OnClick="LinkHRSS_Click" /></li>
                            <li>
                                <asp:Button ID="ButtonSAAS" runat="server" Text="SAAS" CssClass="btnsetup" OnClick="LinkSAAS_Click" /></li>
                        </ul>
                        <div class="setuppage col-lg-9">
                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                <asp:View ID="View1" runat="server">
                                    <div class="row" style="margin: 0px;">
                                        <div class="col-md-4">
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkAdmin" runat="server" />
                                                    Admin
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkEmployee" runat="server" />
                                                    Employee
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkPayroll" runat="server" />
                                                    Payroll
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkStatutory" runat="server" />
                                                    Statutory Report
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkAttendance" runat="server" />
                                                    Attendance
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkLeave" runat="server" />
                                                    Leave
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkBenefit" runat="server" />
                                                    Benefit
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkFinancial" runat="server" />
                                                    Financial 
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkData" runat="server" />
                                                    Data Import
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkTraining" runat="server" />
                                                    Training
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkPerfomance" runat="server" />
                                                    Perfomance
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkStaff" runat="server" />
                                                    Staff Inventory
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkTransport" runat="server" />
                                                    Transport 
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkManpower" runat="server" />
                                                    Manpower
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkRecruitment" runat="server" />
                                                    Recruitment
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <div class="row" style="margin: 0px;">
                                        <div class="col-md-4">
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkeLeave" runat="server" />
                                                    eLeave
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkeOvertime" runat="server" />
                                                    eOvertime
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkeAttendance" runat="server" />
                                                    eAttendance
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkeClaim" runat="server" />
                                                    eClaim
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkeStaff" runat="server" />
                                                    eStaff 
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkeCanteen" runat="server" />
                                                    eCanteen
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkePayslip" runat="server" />
                                                    ePayslip 
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkePCB" runat="server" />
                                                    ePCB
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-check">
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkeEA" runat="server" />
                                                    eEA 
                                                </label>
                                                <br />
                                                <label class="form-check-label">
                                                    <asp:CheckBox ID="chkeKIOSK" runat="server" />
                                                    eKIOSK
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <div class="row" style="margin: 0px;">
                                        <div class="col-md-4">
                                            <div class="form-check">
                                                <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkBusiness" runat="server" />
                                                    Business Scorecard
                                                </label>
                                                <br />
                                                <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkPerformanceScore" runat="server" />
                                                    Performance Scorecard
                                                </label>
                                                <br />
                                                <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkTNA" runat="server" />
                                                    TNA
                                                </label>
                                                <br />
                                                 <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkPerformanceDash" runat="server" />
                                                    Performance Dashboard
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-check">
                                                <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkTrainingManage" runat="server" />
                                                    Training Management 
                                                </label>
                                                <br />
                                                <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkRecruitmentSelection" runat="server" />
                                                    Recruitment & Selection
                                                </label>
                                                <br />
                                                <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkRecruitmentPor" runat="server" />
                                                    Recruitment Portal 
                                                </label>
                                                <br />
                                               <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkTrainingPor" runat="server" />
                                                    Training Portal
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-check">
                                                <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkEmployeeRelation" runat="server" />
                                                    Employee Relation 
                                                </label>
                                                <br />
                                                <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkOrganization" runat="server" />
                                                    Organization Chart
                                                </label>
                                                <br />
                                                <label class="form-check-label-big">
                                                    <asp:CheckBox ID="chkTalent" runat="server" />
                                                    Talent Management
                                                </label>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="View4" runat="server">
                                    <div class="row" style="margin: 0px;">
                                        <div class="col-md-4">
                                        </div>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ButtonHRMS" />
                    <asp:AsyncPostBackTrigger ControlID="ButtonESS" />
                    <asp:AsyncPostBackTrigger ControlID="ButtonHRSS" />
                    <asp:AsyncPostBackTrigger ControlID="ButtonSAAS" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Button ID="btnsave" runat="server" Text="SAVE"  CssClass="btn btn-success col-md-offset-10" Width="81px" OnClick="btn_save" />
        </div>
    </div>
</asp:Content>
