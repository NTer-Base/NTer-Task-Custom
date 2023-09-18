using N_Ter.Base;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
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

            if (dsTask.tbltasks[0].Current_Step_ID == -10)
            {
                //implement other screens
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
