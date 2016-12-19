using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSQN.View.Admin
{
    public partial class UserSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void LinkHRMS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            ButtonHRMS.CssClass = "btnsetup active";
            ButtonESS.CssClass = "btnsetup";
            ButtonHRSS.CssClass = "btnsetup";
            ButtonSAAS.CssClass = "btnsetup";
        }

        protected void LinkESS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            ButtonHRMS.CssClass = "btnsetup";
            ButtonESS.CssClass = "btnsetup active";
            ButtonHRSS.CssClass = "btnsetup";
            ButtonSAAS.CssClass = "btnsetup";
        }

        protected void LinkHRSS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            ButtonHRMS.CssClass = "btnsetup";
            ButtonESS.CssClass = "btnsetup";
            ButtonHRSS.CssClass = "btnsetup active";
            ButtonSAAS.CssClass = "btnsetup";
        }

        protected void LinkSAAS_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            ButtonHRMS.CssClass = "btnsetup";
            ButtonESS.CssClass = "btnsetup";
            ButtonHRSS.CssClass = "btnsetup";
            ButtonSAAS.CssClass = "btnsetup active";
        }

        protected void btn_save(object sender, EventArgs e)
        {
            string name = Username.Text.Trim();
            string comapny = CompanyName.Text.Trim();
            string email = Email.Text.Trim();
            //For HRMS check box
            string chkedAdmin       = chkAdmin.Checked ? "Y" : "N";
            string chkedEmployee    = chkEmployee.Checked ? "Y" : "N";
            string chkedPayroll     = chkPayroll.Checked ? "Y" : "N";
            string chkedStatutory   = chkStatutory.Checked ? "Y" : "N";
            string chkedAttendance  = chkAttendance.Checked ? "Y" : "N";
            string chkedLeave       = chkLeave.Checked ? "Y" : "N";
            string chkedBenefit     = chkBenefit.Checked ? "Y" : "N";
            string chkedFinancial   = chkFinancial.Checked ? "Y" : "N";
            string chkedData        = chkData.Checked ? "Y" : "N";
            string chkedTraining    = chkTraining.Checked ? "Y" : "N";
            string chkedPerfomance  = chkPerfomance.Checked ? "Y" : "N";
            string chkedStaff       = chkStaff.Checked ? "Y" : "N";
            string chkedTransport   = chkTransport.Checked ? "Y" : "N";
            string chkedManpower    = chkManpower.Checked ? "Y" : "N";
            string chkedRecruitment = chkRecruitment.Checked ? "Y" : "N";

        }
    }
}