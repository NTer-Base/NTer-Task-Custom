using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter_Task_Custom.Data.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace N_Ter.Customizable.UI
{
    public class Task_Posts
    {
        private string _HelpScript, _HelpPanelResizeScript, _LoadingScript, _FormulaFieldsScript, _NextStepScript;
        private Task_Controls_Main _ControlsSet = new Task_Controls_Main();
        private Task_Post_Elements _PostElements = new Task_Post_Elements();
        private Panel _Display = new Panel();

        public void GenerateScreen(SessionObject objSes, bool IsPostBack, Page SourcePage)
        {
            _Display.Controls.Clear();

            _PostElements.ShowSubmitFooter = false;
            _PostElements.ShowSaveFields = false;
            _PostElements.RequiredFieldsValidationScript = "";
            _PostElements.OldFieldsValidationScript = "";
            _PostElements.LoadingScript = "";
            _PostElements.FormulaFieldsScript = "";
            _PostElements.HaveHelpText = false;
            _PostElements.HelpPanelResizeScript = "";
            _PostElements.NextStepScript = "0";
            _PostElements.AddNewEL2 = false;
            _PostElements.RefreshTask = false;
            _PostElements.NextStep = 0;

            _ControlsSet.Controls.Clear();
            _HelpScript = "";
            _HelpPanelResizeScript = "";
            _LoadingScript = "";
            _FormulaFieldsScript = "";
            _NextStepScript = "";

            DS_Tasks dsTask = ObjectCreator.GetTasks(objSes.Connection, objSes.DB_Type, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading).Read(_PostElements.Task_ID, false, false, false);
            DS_Workflow dsWorkflow = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type).ReadStep(dsTask.tbltasks[0].Current_Step_ID);

            if (dsTask.tbltasks[0].Current_Step_ID == 87)
            {
                //implement other screens
                ShowInquiryTypeFields(dsTask, dsWorkflow, objSes, IsPostBack);
            }
            else if (dsTask.tbltasks[0].Current_Step_ID == 92 || dsTask.tbltasks[0].Current_Step_ID == 95)
            {
                //implement other screens
                ShowHolidayPaymentFields(dsTask, dsWorkflow, objSes, IsPostBack);
            }
            //else if (dsTask.tbltasks[0].Current_Step_ID == 99)
            //{
            //    //implement other screens
            //    ShowClosingInquiryFields(dsTask, dsWorkflow, objSes, IsPostBack);
            //}
            else if (dsTask.tbltasks[0].Current_Step_ID == 107)
            {
                //implement other screens
                ShowInboundNonSeriesInquiryFields(dsTask, dsWorkflow, objSes, IsPostBack);
            }
            else if (dsTask.tbltasks[0].Current_Step_ID == 125)
            {
                //implement other screens
                ShowClosingInquiryBackupFields(dsTask, dsWorkflow, objSes, IsPostBack);
            }
            else if (dsTask.tbltasks[0].Current_Step_ID == 128)
            {
                //implement other screens
                ShowFinancePaymentFields(dsTask, dsWorkflow, objSes, IsPostBack);
            }
            else
            {
                LoadTaskDetails(objSes, dsWorkflow, dsTask, IsPostBack);
            }

            _PostElements.HelpScript = _HelpScript;
            _PostElements.HelpPanelResizeScript = _HelpPanelResizeScript;
            _PostElements.LoadingScript = _LoadingScript;
            _PostElements.FormulaFieldsScript = _FormulaFieldsScript;
            _PostElements.NextStepScript = _NextStepScript;
            _PostElements.ControlsSet = _ControlsSet;
        }

        private void LoadTaskDetails(SessionObject objSes, DS_Workflow dsWorkflow, DS_Tasks dsTask, bool IsPostBack)
        {
            _PostElements.ShowSubmitFooter = true;
            _PostElements.ShowSaveFields = true;

            bool blnValidateOldFieds = false;
            string strOldFieldValidation = "";

            Script_Generator objScripts = new Script_Generator();
            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
            {
                blnValidateOldFieds = true;
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n";
            }
            else
            {
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n" +
                                                                "return true;" + "\r\n" +
                                                          "}";
            }

            string strRequiredFieldValidation = "";
            if (_PostElements.AddNewEL2 == true)
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = checkEL2()" + "\r\n";
            }
            else
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = true;" + "\r\n";
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            _ControlsSet.Task_ID = dsTask.tbltasks[0].Task_ID;
            _ControlsSet.Current_Step_ID = dsTask.tbltasks[0].Current_Step_ID;

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            Common_Task_Actions objTskAct = new Common_Task_Actions();

            List<DS_Tasks> dsTasks = new List<DS_Tasks>();
            dsTasks.Add(dsTask);

            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Workflow_Step_Field_Cat_ID == 0).Count() > 0)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                int rowWidth = 0;

                _Display.CssClass = "row";
                foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields)
                {
                    divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                    if (rowStepField.Help_Text.Trim() != "")
                    {
                        Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                        ControlIndex++;
                    }
                    if (rowWidth == 12)
                    {
                        _Display.Controls.Add(divMainRowControl);
                        divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                        rowWidth = 0;
                    }
                }
                if (rowWidth > 0)
                {
                    _Display.Controls.Add(divMainRowControl);
                }
            }
            else
            {
                _Display.CssClass = "panel-group no-margin-b";
                int tabIndex = 0;
                foreach (DS_Workflow.tblworkflow_step_field_catsRow rowCats in dsWorkflow.tblworkflow_step_field_cats)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl divOuterPanel = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divOuterPanel.Attributes.Add("class", "panel");
                    System.Web.UI.HtmlControls.HtmlGenericControl divPanelHeader = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divPanelHeader.Attributes.Add("class", "panel-heading");
                    System.Web.UI.HtmlControls.HtmlGenericControl aPanelHeader = new System.Web.UI.HtmlControls.HtmlGenericControl("a");
                    if (tabIndex == 0)
                    {
                        aPanelHeader.Attributes.Add("class", "accordion-toggle");
                    }
                    else
                    {
                        aPanelHeader.Attributes.Add("class", "accordion-toggle collapsed");
                    }
                    aPanelHeader.Attributes.Add("data-toggle", "collapse");
                    aPanelHeader.Attributes.Add("data-parent", "#" + _Display.ClientID);
                    aPanelHeader.Attributes.Add("href", "#collapse" + tabIndex);
                    aPanelHeader.InnerHtml = rowCats.Workflow_Step_Field_Cat;
                    divPanelHeader.Controls.Add(aPanelHeader);
                    divOuterPanel.Controls.Add(divPanelHeader);

                    System.Web.UI.HtmlControls.HtmlGenericControl divPanelBody = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divPanelBody.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    divPanelBody.ID = "collapse" + tabIndex;
                    if (tabIndex == 0)
                    {
                        divPanelBody.Attributes.Add("class", "panel-collapse in");
                    }
                    else
                    {
                        divPanelBody.Attributes.Add("class", "panel-collapse collapse");
                    }
                    System.Web.UI.HtmlControls.HtmlGenericControl divPanelInner = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divPanelInner.Attributes.Add("class", "panel-body");
                    System.Web.UI.HtmlControls.HtmlGenericControl divPanelInnerRow = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divPanelInnerRow.Attributes.Add("class", "row");

                    System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                    int rowWidth = 0;

                    foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields.Where(x => x.Workflow_Step_Field_Cat_ID == rowCats.Workflow_Step_Field_Cat_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            divPanelInnerRow.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                    if (rowWidth > 0)
                    {
                        divPanelInnerRow.Controls.Add(divMainRowControl);
                    }

                    divPanelInner.Controls.Add(divPanelInnerRow);
                    divPanelBody.Controls.Add(divPanelInner);
                    divOuterPanel.Controls.Add(divPanelBody);

                    _Display.Controls.Add(divOuterPanel);
                    tabIndex++;
                }
            }

            if (blnValidateOldFieds == true)
            {
                if (strOldFieldValidation.Length > 0)
                {
                    strOldFieldValidation = strOldFieldValidation.Substring(5);
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + strOldFieldValidation + "else {" + "\r\n" +
                                                                                                "return true;" + "\r\n" +
                                                                                           "}" + "\r\n" +
                                                                                       "}";
                }
                else
                {
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + "return true;" + "\r\n" +
                                                              "}";
                }
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}";

            if (Help_Texts.Count > 0)
            {
                _PostElements.HaveHelpText = true;
                objScripts.LoadHelpScripts(ref _HelpScript, ref _HelpPanelResizeScript, Help_Texts);
            }

            ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostFormAdjustments(dsTask, ref _ControlsSet, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            objScripts.LoadFormulaScripts(ref _LoadingScript, ref _FormulaFieldsScript, objTskAct.GetControlNamePrefix(FieldOrigin.Task_Field), dsWorkflow.tblworkflow_formulas);
            objScripts.LoadNextStepScript(ref _NextStepScript, _PostElements.Task_ID, _PostElements.Workflow_ID, _PostElements.CurrentStep_ID, dsWorkflow, _ControlsSet);
        }

        #region Show Inquiry Type Fields
        protected void ShowInquiryTypeFields(DS_Tasks dsTask, DS_Workflow dsWorkflow, SessionObject objSes, bool IsPostBack)
        {
            _PostElements.ShowSubmitFooter = true;
            _PostElements.ShowSaveFields = true;

            bool blnValidateOldFieds = false;
            string strOldFieldValidation = "";

            Script_Generator objScripts = new Script_Generator();
            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
            {
                blnValidateOldFieds = true;
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n";
            }
            else
            {
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n" +
                                                                "return true;" + "\r\n" +
                                                          "}";
            }

            string strRequiredFieldValidation = "";
            if (_PostElements.AddNewEL2 == true)
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = checkEL2()" + "\r\n";
            }
            else
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = true;" + "\r\n";
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            _ControlsSet.Task_ID = dsTask.tbltasks[0].Task_ID;
            _ControlsSet.Current_Step_ID = dsTask.tbltasks[0].Current_Step_ID;

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            Common_Task_Actions objTskAct = new Common_Task_Actions();

            List<DS_Tasks> dsTasks = new List<DS_Tasks>();
            dsTasks.Add(dsTask);

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            List<DS_Tasks.tbltask_historyRow> taskHistoryMatchInquiry = dsTask.tbltask_history.Where(x => x.Workflow_Step_ID == 86 && x.Task_ID == dsTask.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> taskInquiryTypes = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatchInquiry[0].Task_Update_ID && x.Field_Value == "Yes")
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList();

            List<DS_Tasks.tbltask_historyRow> taskHistoryMatchAmendment = dsTask.tbltask_history.Where(x => x.Workflow_Step_ID == 101 && x.Task_ID == dsTask.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> taskAmendment = taskHistoryMatchAmendment.Count > 0 ? dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatchAmendment[0].Task_Update_ID && x.Workflow_Step_Field_ID == 116 && x.Field_Value == "Yes")
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList() : new List<DS_Tasks.tbltask_update_fieldsRow>();

            List<DS_Tasks.tbltask_update_fieldsRow> paxCountList = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatchInquiry[0].Task_Update_ID && x.Workflow_Step_Field_ID == 126)
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> paxCount = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatchInquiry[0].Task_Update_ID && x.Workflow_Step_Field_ID == 126)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> countryCount = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatchInquiry[0].Task_Update_ID && x.Workflow_Step_Field_ID == 420)
                            .ToList();

            int paxCountVal = 0;
            if (paxCount.Count > 0)
            {
                paxCountVal = int.Parse(paxCount[0].Field_Value);             
            }

            int countryCountVal = 0;
            if (countryCount[0].Field_Value != "")
            {
                countryCountVal = int.Parse(countryCount[0].Field_Value);
            }

            int[] SPFieldCategories = { 1, 2, 3, 4, 5, 31, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

            int[] SPFieldsCommon = { 45, 46, 47, 48, 49, 50, 51, 127, 128, 129, 130, 131, 132, 133, 134, 135, 141, 142, 143, 144, 145, 146, 147, 148, 154, 155, 156, 157, 158, 159, 160, 161, 167, 168, 169, 170, 171, 172, 173, 174, 180, 181, 182, 183, 184, 185, 186, 187, 193, 194, 195, 196, 197, 198, 199, 200, 206, 207, 208, 209, 210, 211, 212, 213, 219, 220, 221, 222, 223, 224, 225, 226, 232, 233, 234, 235, 236, 237, 238, 239, 245, 246, 247, 248, 249, 250, 251, 252, 258, 259, 260, 261, 262, 263, 264, 265, 271, 272, 273, 274, 275, 276, 277, 278, 284, 285, 286, 287, 288, 289, 290, 291, 297, 298, 309, 299, 300, 301, 302, 303 };
            int[] SPFieldsFCM = { 45, 49, 51, 52, 53, 127, 128, 129, 133, 135, 136, 137, 141, 142, 146, 148, 149, 150, 154, 155, 159, 161, 162, 163, 167, 168, 172, 174, 175, 176, 180, 181, 185, 187, 188 ,189, 193, 194, 198, 200, 201, 202, 206, 207, 211, 213, 214, 215, 219, 220, 224, 226, 227, 228, 232, 233, 237, 239, 240, 241, 245, 246, 250, 252, 253, 254, 258, 259, 263, 265, 266, 267, 271, 272, 276, 278, 279, 280, 284, 285, 289, 291, 292, 293, 297, 298, 301, 303, 304, 305 };
            int[] SPFieldsTicketing = { 52, 53, 136, 137, 149, 150, 162, 163, 175, 176, 188, 189, 201, 202, 214, 215, 227, 228, 240, 241, 253, 254, 266, 267, 279, 280, 292, 293, 304, 305, 419, 54, 55, 56, 57, 58, 59, 60, 138, 139, 140, 151, 152, 153, 164, 165, 166, 177, 178, 179, 190, 191, 192, 203, 204, 205, 216, 217, 218, 229, 230, 231, 242, 243, 244, 255, 256, 257, 268, 269, 270, 281, 282, 283, 294, 295, 296, 306, 307, 308 };
            int[] SPFieldsHoliday = { 62, 63, 64, 65, 418, 54, 138, 151, 164, 177, 190, 203, 216, 229, 242, 255, 268, 281, 294, 306 };
            int[] SPFieldsVisa = { 415, 66, 67, 68, 69, 421, 422, 423, 424, 425, 426, 427, 428, 429, 430, 431, 432, 433, 434, 435, 436, 437, 438, 439, 440, 54, 55, 56, 138, 139, 140, 151, 152, 153, 164, 165, 166, 177, 178, 179, 190, 191, 192, 203, 204, 205, 216, 217, 218, 229, 230, 231, 242, 243, 244, 255, 256, 257, 268, 269, 270, 281, 282, 283, 294, 295, 296, 306, 307, 308 };
            int SPFieldInsurance = 61;
            int SPAmendment = 119;

            _Display.CssClass = "row";
            foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields)
            {
                if(SPFieldCategories.Take(paxCountVal + 5).Contains(rowStepField.Workflow_Step_Field_Cat_ID))
                {
                    if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 44))
                    {
                        if (SPFieldsFCM.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    else
                    {
                        if (SPFieldsCommon.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41) && taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 44))
                    {
                        if (SPFieldsTicketing.Skip(30).Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    else if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41))
                    {
                        if (SPFieldsTicketing.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    if ((taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41) && taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 42)) || (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 42) && taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 43)))
                    {
                        if (SPFieldsHoliday.Take(5).Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    else if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 42))
                    {
                        if (SPFieldsHoliday.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41) && taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 43))
                    {
                        if (SPFieldsVisa.Take(countryCountVal + 5).Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    else if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 43))
                    {
                        if (SPFieldsVisa.Take(5).Concat(SPFieldsVisa.Skip(5).Take(countryCountVal)).Concat(SPFieldsVisa.Skip(25)).Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 70))
                    {
                        if (SPFieldInsurance == rowStepField.Workflow_Step_Field_ID)
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                }
                else if (taskAmendment.Count > 0)
                {
                    if (SPAmendment == rowStepField.Workflow_Step_Field_ID)
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
            }
            if (rowWidth > 0)
            {
               _Display.Controls.Add(divMainRowControl);
            }

            if (blnValidateOldFieds == true)
            {
                if (strOldFieldValidation.Length > 0)
                {
                    strOldFieldValidation = strOldFieldValidation.Substring(5);
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + strOldFieldValidation + "else {" + "\r\n" +
                                                                                                "return true;" + "\r\n" +
                                                                                           "}" + "\r\n" +
                                                                                       "}";
                }
                else
                {
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + "return true;" + "\r\n" +
                                                              "}";
                }
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}";

            if (Help_Texts.Count > 0)
            {
                _PostElements.HaveHelpText = true;
                objScripts.LoadHelpScripts(ref _HelpScript, ref _HelpPanelResizeScript, Help_Texts);
            }

            ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostFormAdjustments(dsTask, ref _ControlsSet, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            objScripts.LoadFormulaScripts(ref _LoadingScript, ref _FormulaFieldsScript, objTskAct.GetControlNamePrefix(FieldOrigin.Task_Field), dsWorkflow.tblworkflow_formulas);
            objScripts.LoadNextStepScript(ref _NextStepScript, _PostElements.Task_ID, _PostElements.Workflow_ID, _PostElements.CurrentStep_ID, dsWorkflow, _ControlsSet);
        }
        #endregion

        #region Show Holiday Payment Fields
        protected void ShowHolidayPaymentFields(DS_Tasks dsTask, DS_Workflow dsWorkflow, SessionObject objSes, bool IsPostBack)
        {
            _PostElements.ShowSubmitFooter = true;
            _PostElements.ShowSaveFields = true;

            bool blnValidateOldFieds = false;
            string strOldFieldValidation = "";

            Script_Generator objScripts = new Script_Generator();
            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
            {
                blnValidateOldFieds = true;
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n";
            }
            else
            {
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n" +
                                                                "return true;" + "\r\n" +
                                                          "}";
            }

            string strRequiredFieldValidation = "";
            if (_PostElements.AddNewEL2 == true)
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = checkEL2()" + "\r\n";
            }
            else
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = true;" + "\r\n";
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            _ControlsSet.Task_ID = dsTask.tbltasks[0].Task_ID;
            _ControlsSet.Current_Step_ID = dsTask.tbltasks[0].Current_Step_ID;

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            Common_Task_Actions objTskAct = new Common_Task_Actions();

            List<DS_Tasks> dsTasks = new List<DS_Tasks>();
            dsTasks.Add(dsTask);

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = dsTask.tbltask_history.Where(x => x.Workflow_Step_ID == 86 && x.Task_ID == dsTask.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> taskInquiryHolidayType = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && x.Workflow_Step_Field_ID == 42 && x.Field_Value == "Yes")
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList();

            int[] SPFieldsHolidayCash = { 75, 81 };
            int[] SPFieldsHolidayCredit = { 77, 82 };

            _Display.CssClass = "row";
            foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields)
            {
                if (dsTask.tbltasks[0].Current_Step_ID == 92)
                {
                    if (taskInquiryHolidayType.Count == 0)
                    {
                        if (SPFieldsHolidayCash[0] == rowStepField.Workflow_Step_Field_ID)
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    else
                    {
                        if (SPFieldsHolidayCash.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                }
                else if (dsTask.tbltasks[0].Current_Step_ID == 95)
                {
                    {
                        if (taskInquiryHolidayType.Count == 0)
                        {
                            if (SPFieldsHolidayCredit[0] == rowStepField.Workflow_Step_Field_ID)
                            {
                                divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                                if (rowStepField.Help_Text.Trim() != "")
                                {
                                    Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                    ControlIndex++;
                                }
                                if (rowWidth == 12)
                                {
                                    _Display.Controls.Add(divMainRowControl);
                                    divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                    rowWidth = 0;
                                }
                            }
                        }
                        else
                        {
                            if (SPFieldsHolidayCredit.Contains(rowStepField.Workflow_Step_Field_ID))
                            {
                                divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                                if (rowStepField.Help_Text.Trim() != "")
                                {
                                    Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                    ControlIndex++;
                                }
                                if (rowWidth == 12)
                                {
                                    _Display.Controls.Add(divMainRowControl);
                                    divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                    divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                    rowWidth = 0;
                                }
                            }
                        }
                    }
                }
            }
            if (rowWidth > 0)
            {
                _Display.Controls.Add(divMainRowControl);
            }

            if (blnValidateOldFieds == true)
            {
                if (strOldFieldValidation.Length > 0)
                {
                    strOldFieldValidation = strOldFieldValidation.Substring(5);
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + strOldFieldValidation + "else {" + "\r\n" +
                                                                                                "return true;" + "\r\n" +
                                                                                           "}" + "\r\n" +
                                                                                       "}";
                }
                else
                {
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + "return true;" + "\r\n" +
                                                              "}";
                }
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}";

            if (Help_Texts.Count > 0)
            {
                _PostElements.HaveHelpText = true;
                objScripts.LoadHelpScripts(ref _HelpScript, ref _HelpPanelResizeScript, Help_Texts);
            }

            ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostFormAdjustments(dsTask, ref _ControlsSet, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            objScripts.LoadFormulaScripts(ref _LoadingScript, ref _FormulaFieldsScript, objTskAct.GetControlNamePrefix(FieldOrigin.Task_Field), dsWorkflow.tblworkflow_formulas);
            objScripts.LoadNextStepScript(ref _NextStepScript, _PostElements.Task_ID, _PostElements.Workflow_ID, _PostElements.CurrentStep_ID, dsWorkflow, _ControlsSet);
        }
        #endregion

        #region Show Holiday Payment Fields
        protected void ShowFinancePaymentFields(DS_Tasks dsTask, DS_Workflow dsWorkflow, SessionObject objSes, bool IsPostBack)
        {
            _PostElements.ShowSubmitFooter = true;
            _PostElements.ShowSaveFields = true;

            bool blnValidateOldFieds = false;
            string strOldFieldValidation = "";

            Script_Generator objScripts = new Script_Generator();
            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
            {
                blnValidateOldFieds = true;
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n";
            }
            else
            {
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n" +
                                                                "return true;" + "\r\n" +
                                                          "}";
            }

            string strRequiredFieldValidation = "";
            if (_PostElements.AddNewEL2 == true)
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = checkEL2()" + "\r\n";
            }
            else
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = true;" + "\r\n";
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            _ControlsSet.Task_ID = dsTask.tbltasks[0].Task_ID;
            _ControlsSet.Current_Step_ID = dsTask.tbltasks[0].Current_Step_ID;

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            Common_Task_Actions objTskAct = new Common_Task_Actions();

            List<DS_Tasks> dsTasks = new List<DS_Tasks>();
            dsTasks.Add(dsTask);

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            List<DS_Tasks.tbltask_historyRow> taskHistoryMatch1 = dsTask.tbltask_history.Where(x => x.Workflow_Step_ID == 86 && x.Task_ID == dsTask.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> taskInquiryHolidayType = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch1[0].Task_Update_ID && x.Workflow_Step_Field_ID == 42 && x.Field_Value == "Yes")
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList();

            List<DS_Tasks.tbltask_historyRow> taskHistoryMatch2 = dsTask.tbltask_history.Where(x => x.Workflow_Step_ID == 88 && x.Task_ID == dsTask.tbltasks[0].Task_ID)
                        .OrderByDescending(o => o.Task_Update_ID)
                        .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> taskPaymentType = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch2[0].Task_Update_ID && x.Workflow_Step_Field_ID == 71)
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList();

            string taskPaymentTypeVal = "";

            if (taskPaymentType.Count > 0)
            {
                taskPaymentTypeVal = taskPaymentType[0].Field_Value;
            }

            int[] SPFieldsCash = { 72, 73 };
            int[] SPFieldsCredit = { 78, 79 };
            int[] SPFieldsHoliday = { 80, 81 };

            _Display.CssClass = "row";
            foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields)
            {
                if (taskPaymentTypeVal == "Cash")
                {
                    if (SPFieldsCash.Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
                else if (taskPaymentTypeVal == "Credit")
                {
                    if (SPFieldsCredit.Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
                if (taskInquiryHolidayType.Count > 0)
                {
                    if (SPFieldsHoliday.Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
            }
            if (rowWidth > 0)
            {
                _Display.Controls.Add(divMainRowControl);
            }

            if (blnValidateOldFieds == true)
            {
                if (strOldFieldValidation.Length > 0)
                {
                    strOldFieldValidation = strOldFieldValidation.Substring(5);
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + strOldFieldValidation + "else {" + "\r\n" +
                                                                                                "return true;" + "\r\n" +
                                                                                           "}" + "\r\n" +
                                                                                       "}";
                }
                else
                {
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + "return true;" + "\r\n" +
                                                              "}";
                }
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}";

            if (Help_Texts.Count > 0)
            {
                _PostElements.HaveHelpText = true;
                objScripts.LoadHelpScripts(ref _HelpScript, ref _HelpPanelResizeScript, Help_Texts);
            }

            ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostFormAdjustments(dsTask, ref _ControlsSet, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            objScripts.LoadFormulaScripts(ref _LoadingScript, ref _FormulaFieldsScript, objTskAct.GetControlNamePrefix(FieldOrigin.Task_Field), dsWorkflow.tblworkflow_formulas);
            objScripts.LoadNextStepScript(ref _NextStepScript, _PostElements.Task_ID, _PostElements.Workflow_ID, _PostElements.CurrentStep_ID, dsWorkflow, _ControlsSet);
        }
        #endregion

        #region Show Inquiry Type Fields
        protected void ShowClosingInquiryFields(DS_Tasks dsTask, DS_Workflow dsWorkflow, SessionObject objSes, bool IsPostBack)
        {
            _PostElements.ShowSubmitFooter = true;
            _PostElements.ShowSaveFields = true;

            bool blnValidateOldFieds = false;
            string strOldFieldValidation = "";

            Script_Generator objScripts = new Script_Generator();
            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
            {
                blnValidateOldFieds = true;
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n";
            }
            else
            {
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n" +
                                                                "return true;" + "\r\n" +
                                                          "}";
            }

            string strRequiredFieldValidation = "";
            if (_PostElements.AddNewEL2 == true)
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = checkEL2()" + "\r\n";
            }
            else
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = true;" + "\r\n";
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            _ControlsSet.Task_ID = dsTask.tbltasks[0].Task_ID;
            _ControlsSet.Current_Step_ID = dsTask.tbltasks[0].Current_Step_ID;

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            Common_Task_Actions objTskAct = new Common_Task_Actions();

            List<DS_Tasks> dsTasks = new List<DS_Tasks>();
            dsTasks.Add(dsTask);

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = dsTask.tbltask_history.Where(x => x.Workflow_Step_ID == 86 && x.Task_ID == dsTask.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> taskInquiryTypes = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && x.Field_Value == "Yes")
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList();

            int[] SPFieldsCommon = { 85, 86, 87, 88, 89, 90, 91, 111, 112, 113 };
            int[] SPFieldsFCM = { 85, 89, 91, 92, 93, 111, 112, 113 };
            int[] SPFieldsTicketing = { 92, 93, 96, 97, 98, 99, 100 };
            int[] SPFieldsHoliday = {102, 103, 104, 105, 110, 114 };
            int[] SPFieldsVisa = { 96, 106, 107, 108, 109 };
            int SPFieldInsurance = 101;

            _Display.CssClass = "row";
            foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields)
            {
                if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 44))
                {
                    if (SPFieldsFCM.Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
                else
                {
                    if (SPFieldsCommon.Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
                if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41) && taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 44))
                {
                    if (SPFieldsTicketing.Skip(2).Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
                else if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41))
                {
                    if (SPFieldsTicketing.Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
                if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 42))
                {
                    if (SPFieldsHoliday.Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
                if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41) && taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 43))
                {
                    if (SPFieldsVisa.Skip(1).Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
                else if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 43))
                {
                    if (SPFieldsVisa.Contains(rowStepField.Workflow_Step_Field_ID))
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
                if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 70))
                {
                    if (SPFieldInsurance == rowStepField.Workflow_Step_Field_ID)
                    {
                        divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                        if (rowStepField.Help_Text.Trim() != "")
                        {
                            Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                            ControlIndex++;
                        }
                        if (rowWidth == 12)
                        {
                            _Display.Controls.Add(divMainRowControl);
                            divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                            rowWidth = 0;
                        }
                    }
                }
            }
            if (rowWidth > 0)
            {
                _Display.Controls.Add(divMainRowControl);
            }

            if (blnValidateOldFieds == true)
            {
                if (strOldFieldValidation.Length > 0)
                {
                    strOldFieldValidation = strOldFieldValidation.Substring(5);
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + strOldFieldValidation + "else {" + "\r\n" +
                                                                                                "return true;" + "\r\n" +
                                                                                           "}" + "\r\n" +
                                                                                       "}";
                }
                else
                {
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + "return true;" + "\r\n" +
                                                              "}";
                }
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}";

            if (Help_Texts.Count > 0)
            {
                _PostElements.HaveHelpText = true;
                objScripts.LoadHelpScripts(ref _HelpScript, ref _HelpPanelResizeScript, Help_Texts);
            }

            ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostFormAdjustments(dsTask, ref _ControlsSet, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            objScripts.LoadFormulaScripts(ref _LoadingScript, ref _FormulaFieldsScript, objTskAct.GetControlNamePrefix(FieldOrigin.Task_Field), dsWorkflow.tblworkflow_formulas);
            objScripts.LoadNextStepScript(ref _NextStepScript, _PostElements.Task_ID, _PostElements.Workflow_ID, _PostElements.CurrentStep_ID, dsWorkflow, _ControlsSet);
        }
        #endregion

        #region Show Inquiry Type Close Inquiry Backup Fields
        protected void ShowClosingInquiryBackupFields(DS_Tasks dsTask, DS_Workflow dsWorkflow, SessionObject objSes, bool IsPostBack)
        {
            _PostElements.ShowSubmitFooter = true;
            _PostElements.ShowSaveFields = true;

            bool blnValidateOldFieds = false;
            string strOldFieldValidation = "";

            Script_Generator objScripts = new Script_Generator();
            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
            {
                blnValidateOldFieds = true;
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n";
            }
            else
            {
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n" +
                                                                "return true;" + "\r\n" +
                                                          "}";
            }

            string strRequiredFieldValidation = "";
            if (_PostElements.AddNewEL2 == true)
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = checkEL2()" + "\r\n";
            }
            else
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = true;" + "\r\n";
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            _ControlsSet.Task_ID = dsTask.tbltasks[0].Task_ID;
            _ControlsSet.Current_Step_ID = dsTask.tbltasks[0].Current_Step_ID;

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            Common_Task_Actions objTskAct = new Common_Task_Actions();

            List<DS_Tasks> dsTasks = new List<DS_Tasks>();
            dsTasks.Add(dsTask);

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            List<DS_Tasks.tbltask_historyRow> taskHistoryMatchInquiry = dsTask.tbltask_history.Where(x => x.Workflow_Step_ID == 86 && x.Task_ID == dsTask.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> taskInquiryTypes = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatchInquiry[0].Task_Update_ID && x.Field_Value == "Yes")
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> paxCountList = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatchInquiry[0].Task_Update_ID && x.Workflow_Step_Field_ID == 126)
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> paxCount = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatchInquiry[0].Task_Update_ID && x.Workflow_Step_Field_ID == 126)
                            .ToList();

            int paxCountVal = 0;
            if (paxCount.Count > 0)
            {
                paxCountVal = int.Parse(paxCount[0].Field_Value);
            }

            int[] SPFieldCategories = { 47, 48, 49, 50, 53, 54, 55, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 };

            int[] SPFieldsCommon = { 759, 74, 75, 416, 479, 480, 481, 482, 483, 484, 485, 486, 492, 493, 494, 495, 496, 497, 498, 499, 505, 506, 507, 510, 511, 512, 518, 519, 520, 523, 524, 525, 531, 532, 533, 535, 536, 537, 538, 544, 545, 546, 547, 548, 549, 550, 551, 557, 558, 559, 560, 561, 562, 563, 564, 570, 571, 572, 573, 574, 575, 576, 577, 583, 584, 585, 586, 587, 588, 589, 590, 596, 597, 598, 599, 600, 601, 602, 603, 609, 610, 611, 612, 613, 614, 615, 616, 622, 623, 624, 625, 626, 627, 628, 629, 635, 636, 637, 638, 639, 640, 641, 642, 648, 649, 650, 651, 652, 653, 654, 655, 661, 662, 663, 664, 665, 666, 667, 668, 740, 741, 742, 743, 744, 752, 753, 754, 755, 756 };
            int[] SPFieldsFCM = { 759, 74, 75, 416, 479, 480, 484, 486, 487, 488, 492, 493, 497, 499, 500, 501, 505, 506, 510, 512, 513, 514, 518, 519, 523, 525, 526, 527, 531, 532, 536, 538, 539, 540, 544, 546, 549, 551, 552, 553, 557, 558, 562, 564, 565, 566, 570, 571, 575, 577, 578, 579, 583, 584, 588, 590, 591, 592, 596, 597, 601, 603, 604, 605, 609, 610, 614, 616, 617, 618, 622, 623, 627, 629, 630, 631, 635, 636, 640, 642, 643, 644, 648, 649, 653, 655, 656, 657, 661, 662, 666, 668, 669, 670, 752, 753, 754, 755, 756 };
            int[] SPFieldsTicketing = { 487, 488, 500, 501, 513, 514, 526, 527, 539, 540, 552, 553, 565, 566, 578, 579, 591, 592, 604, 605, 617, 618, 630, 631, 643, 644, 656, 657, 669, 670, 674, 675, 676, 677, 678, 491, 504, 517, 530, 543, 556, 569, 582, 595, 608, 621, 634, 647, 660, 673 };
            int[] SPFieldsHoliday = { 680, 681, 682, 683, 684};
            int[] SPFieldsVisa = { 685, 736, 737, 738, 739, 491, 504, 517, 530, 543, 556, 569, 582, 595, 608, 621, 634, 647, 660, 673 };
            int SPFieldInsurance = 679;

            _Display.CssClass = "row";
            foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields)
            {
                if (SPFieldCategories.Take(paxCountVal + 7).Contains(rowStepField.Workflow_Step_Field_Cat_ID))
                {
                    if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 44))
                    {
                        if (SPFieldsFCM.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    else
                    {
                        if (SPFieldsCommon.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41) && taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 44))
                    {
                        if (SPFieldsTicketing.Skip(30).Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    else if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41))
                    {
                        if (SPFieldsTicketing.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 42))
                    {
                        if (SPFieldsHoliday.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 41) && taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 43))
                    {
                        if (SPFieldsVisa.Take(5).Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    else if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 43))
                    {
                        if (SPFieldsVisa.Contains(rowStepField.Workflow_Step_Field_ID))
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                    if (taskInquiryTypes.Exists(x => x.Workflow_Step_Field_ID == 70))
                    {
                        if (SPFieldInsurance == rowStepField.Workflow_Step_Field_ID)
                        {
                            divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                            if (rowStepField.Help_Text.Trim() != "")
                            {
                                Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                                ControlIndex++;
                            }
                            if (rowWidth == 12)
                            {
                                _Display.Controls.Add(divMainRowControl);
                                divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                                divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                                rowWidth = 0;
                            }
                        }
                    }
                }
            }
            if (rowWidth > 0)
            {
                _Display.Controls.Add(divMainRowControl);
            }

            if (blnValidateOldFieds == true)
            {
                if (strOldFieldValidation.Length > 0)
                {
                    strOldFieldValidation = strOldFieldValidation.Substring(5);
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + strOldFieldValidation + "else {" + "\r\n" +
                                                                                                "return true;" + "\r\n" +
                                                                                           "}" + "\r\n" +
                                                                                       "}";
                }
                else
                {
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + "return true;" + "\r\n" +
                                                              "}";
                }
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}";

            if (Help_Texts.Count > 0)
            {
                _PostElements.HaveHelpText = true;
                objScripts.LoadHelpScripts(ref _HelpScript, ref _HelpPanelResizeScript, Help_Texts);
            }

            ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostFormAdjustments(dsTask, ref _ControlsSet, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            objScripts.LoadFormulaScripts(ref _LoadingScript, ref _FormulaFieldsScript, objTskAct.GetControlNamePrefix(FieldOrigin.Task_Field), dsWorkflow.tblworkflow_formulas);
            objScripts.LoadNextStepScript(ref _NextStepScript, _PostElements.Task_ID, _PostElements.Workflow_ID, _PostElements.CurrentStep_ID, dsWorkflow, _ControlsSet);
        }
        #endregion

        #region Show Inbound (Non-Series) Inquiry Fields
        protected void ShowInboundNonSeriesInquiryFields(DS_Tasks dsTask, DS_Workflow dsWorkflow, SessionObject objSes, bool IsPostBack)
        {
            _PostElements.ShowSubmitFooter = true;
            _PostElements.ShowSaveFields = true;

            bool blnValidateOldFieds = false;
            string strOldFieldValidation = "";

            Script_Generator objScripts = new Script_Generator();
            if (dsWorkflow.tblworkflow_step_fields.Where(x => x.Validate_With_Field_ID > 0).Count() > 0)
            {
                blnValidateOldFieds = true;
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n";
            }
            else
            {
                _PostElements.OldFieldsValidationScript = "function ValidateWithOldField() {" + "\r\n" +
                                                                "return true;" + "\r\n" +
                                                          "}";
            }

            string strRequiredFieldValidation = "";
            if (_PostElements.AddNewEL2 == true)
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = checkEL2()" + "\r\n";
            }
            else
            {
                _PostElements.RequiredFieldsValidationScript = "function ValidatRequiredFields() {" + "\r\n" +
                                                                    "remove_field_erros();" + "\r\n" +
                                                                    "var ret = true;" + "\r\n";
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostScripts(dsTask, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            _ControlsSet.Task_ID = dsTask.tbltasks[0].Task_ID;
            _ControlsSet.Current_Step_ID = dsTask.tbltasks[0].Current_Step_ID;

            int ControlIndex = 0;
            List<string> Help_Texts = new List<string>();

            N_Ter.Customizable.Master_Tables objMasterTables = new N_Ter.Customizable.Master_Tables(objSes.Connection, objSes.DB_Type);
            Common_Task_Actions objTskAct = new Common_Task_Actions();

            List<DS_Tasks> dsTasks = new List<DS_Tasks>();
            dsTasks.Add(dsTask);

            System.Web.UI.HtmlControls.HtmlGenericControl divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
            divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
            int rowWidth = 0;

            List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = dsTask.tbltask_history.Where(x => x.Workflow_Step_ID == 123 && x.Task_ID == dsTask.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

            List<DS_Tasks.tbltask_update_fieldsRow> hotelCount = dsTask.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && x.Workflow_Step_Field_ID == 447)
                            .OrderBy(y => y.Task_Update_Field_ID)
                            .ToList();

            int hotelCountVal = 0;
            if (hotelCount.Count > 0)
            {
                hotelCountVal = int.Parse(hotelCount[0].Field_Value);
            }

            int[] SPFieldsHotels = { 322, 448, 449, 450, 451, 452, 453, 454, 455, 456, 457, 458, 459, 460, 461 };
            int[] SPFieldsHotelAvailable = { 323, 462, 463, 464, 465, 466, 467, 468, 469, 470, 471, 472, 473, 474, 475 };
            int[] SPFieldsCommon = { 324, 325, 326, 327 };

            _Display.CssClass = "row";
            foreach (DS_Workflow.tblworkflow_step_fieldsRow rowStepField in dsWorkflow.tblworkflow_step_fields)
            {
                if (SPFieldsCommon.Contains(rowStepField.Workflow_Step_Field_ID))
                {
                    divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                    if (rowStepField.Help_Text.Trim() != "")
                    {
                        Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                        ControlIndex++;
                    }
                    if (rowWidth == 12)
                    {
                        _Display.Controls.Add(divMainRowControl);
                        divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                        rowWidth = 0;
                    }
                }
                if (SPFieldsHotels.Take(hotelCountVal).Contains(rowStepField.Workflow_Step_Field_ID))
                {
                    divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                    if (rowStepField.Help_Text.Trim() != "")
                    {
                        Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                        ControlIndex++;
                    }
                    if (rowWidth == 12)
                    {
                        _Display.Controls.Add(divMainRowControl);
                        divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                        rowWidth = 0;
                    }
                }
                if (SPFieldsHotelAvailable.Take(hotelCountVal).Contains(rowStepField.Workflow_Step_Field_ID))
                {
                    divMainRowControl.Controls.Add(objTskAct.GetTaskObject(objScripts, IsPostBack, objMasterTables, objSes.Currency_Sbl, dsWorkflow, dsTasks, rowStepField, ref _ControlsSet, ref strRequiredFieldValidation, ref strOldFieldValidation, ref rowWidth, ControlIndex, "GetHelp", false, true));
                    if (rowStepField.Help_Text.Trim() != "")
                    {
                        Help_Texts.Add(rowStepField.Field_Name + "|" + rowStepField.Help_Text);
                        ControlIndex++;
                    }
                    if (rowWidth == 12)
                    {
                        _Display.Controls.Add(divMainRowControl);
                        divMainRowControl = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                        divMainRowControl.Attributes.Add("class", "row padding-xs-hr");
                        rowWidth = 0;
                    }
                }
            }
            if (rowWidth > 0)
            {
                _Display.Controls.Add(divMainRowControl);
            }

            if (blnValidateOldFieds == true)
            {
                if (strOldFieldValidation.Length > 0)
                {
                    strOldFieldValidation = strOldFieldValidation.Substring(5);
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + strOldFieldValidation + "else {" + "\r\n" +
                                                                                                "return true;" + "\r\n" +
                                                                                           "}" + "\r\n" +
                                                                                       "}";
                }
                else
                {
                    _PostElements.OldFieldsValidationScript = _PostElements.OldFieldsValidationScript + "return true;" + "\r\n" +
                                                              "}";
                }
            }

            _PostElements.RequiredFieldsValidationScript = _PostElements.RequiredFieldsValidationScript + strRequiredFieldValidation + "return ret;" + "\r\n" +
                                                                "}";

            if (Help_Texts.Count > 0)
            {
                _PostElements.HaveHelpText = true;
                objScripts.LoadHelpScripts(ref _HelpScript, ref _HelpPanelResizeScript, Help_Texts);
            }

            ObjectCreatorCustom.GetCustomizable(objSes.Connection, objSes.DB_Type).CustomTaskPostFormAdjustments(dsTask, ref _ControlsSet, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);

            objScripts.LoadFormulaScripts(ref _LoadingScript, ref _FormulaFieldsScript, objTskAct.GetControlNamePrefix(FieldOrigin.Task_Field), dsWorkflow.tblworkflow_formulas);
            objScripts.LoadNextStepScript(ref _NextStepScript, _PostElements.Task_ID, _PostElements.Workflow_ID, _PostElements.CurrentStep_ID, dsWorkflow, _ControlsSet);
        }
        #endregion


        #region Properties
        public Task_Post_Elements PostElements
        {
            get
            {
                return _PostElements;
            }
            set
            {
                _PostElements = value;
            }
        }

        public Panel Display
        {
            get
            {
                return _Display;
            }
        }
        #endregion
    }
}
