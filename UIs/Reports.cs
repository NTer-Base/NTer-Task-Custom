using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter.UI;
using N_Ter_Task_Custom.DataStructures.DataSets;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace N_Ter.Customizable.UI
{
    public class Reports
    {
        private List<Control> FilterControls = null;
        private Page PageControl = null;
        private SessionObject SesObject = null;
        private int Report_ID;
        public event EventHandler RefreshReport;

        public Reports(int _Report_ID, SessionObject _SesObject, Page _PageControl, bool _isPostBack)
        {
            Report_ID = _Report_ID;
            SesObject = _SesObject;
            PageControl = _PageControl;

            if (Report_ID == 1)
            {
                Init_Report1(_isPostBack);
            }
        }

        public void GenerateReport()
        {
            if (Report_ID == 1)
            {
                Fill_Report1();
            }
        }

        private void Init_Report1(bool isPostBack)
        {
            FilterControls = new List<Control>();

            TextBox txtFrom = new TextBox();
            txtFrom.CssClass = "form-control dtPicker";
            FilterControls.Add(txtFrom);

            TextBox txtTo = new TextBox();
            txtTo.CssClass = "form-control dtPicker";
            FilterControls.Add(txtTo);

            if (isPostBack == false)
            {
                txtFrom.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today.Date.AddMonths(-1));
                txtTo.Text = string.Format("{0:" + Constants.DateFormat + "}", DateTime.Today.Date);
            }

            From_Element objElements = new From_Element();

            HtmlGenericControl objFromCol = objElements.GetDiv("col-md-6");
            objFromCol.Controls.Add(objElements.GetFromGroup("From Date", txtFrom, true));

            HtmlGenericControl objToCol = objElements.GetDiv("col-md-6");
            objToCol.Controls.Add(objElements.GetFromGroup("To Date", txtTo, true));

            HtmlGenericControl objDate = objElements.GetDiv("row");
            objDate.Controls.Add(objFromCol);
            objDate.Controls.Add(objToCol);

            Special_Panel objFilter = new Special_Panel();
            objFilter.AddControl(objDate);

            Button cmdShowReport = new Button();
            cmdShowReport.CssClass = "btn btn-primary";
            cmdShowReport.Text = "Show Report";
            cmdShowReport.Click += Refresh_Report;
            ReportFilter = objFilter.GetPanel("Report Filter", "panel-info", true, new Button[] { cmdShowReport });
        }

        private void Fill_Report1()
        {
            Common_Actions objCom = new Common_Actions();
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();

            TextBox txtFrom = (TextBox)FilterControls[0];
            TextBox txtTo = (TextBox)FilterControls[1];

            if (objCom.ValidateDate(txtFrom.Text, ref dtFrom) == false)
            {
                ClientScriptManager cs = PageControl.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid From Date');", true);
                Report = null;
            }
            else if (objCom.ValidateDate(txtTo.Text, ref dtTo) == false)
            {
                ClientScriptManager cs = PageControl.ClientScript;
                cs.RegisterStartupScript(this.GetType(), "AKey", "ShowError('Invalid To Date');", true);
                Report = null;
            }
            else
            {
                DS_RPT_Productivity ds = ObjectCreatorCustom.GetReports(SesObject.Connection, SesObject.DB_Type).ReadProductivityReport(dtFrom, dtTo);

                if (ds.tblUpdates.Rows.Count > 0)
                {
                    rptProductivity rpt = new rptProductivity();
                    rpt.ReportCriteria = txtFrom.Text + " - " + txtTo.Text;
                    rpt.Entity_Level_2 = SesObject.EL2;
                    rpt.ReportPrintedBy = "Prepared By : " + SesObject.FullName;
                    rpt.DateFormat = Constants.DateFormat;
                    rpt.DataSource = ds;
                    Report = rpt;
                }
                else
                {
                    Report = null;
                }
            }
        }

        private void Refresh_Report(object sender, EventArgs e)
        {
            RefreshReport.Invoke(this, null);
        }

        #region Properties
        public DevExpress.XtraReports.UI.XtraReport Report { get; set; }

        public HtmlGenericControl ReportFilter { get; set; }
        #endregion
    }
}