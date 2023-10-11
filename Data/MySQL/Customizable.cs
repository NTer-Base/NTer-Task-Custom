using System;
using System.Collections.Generic;
using System.Linq;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;

namespace N_Ter.MySQL.Customizable
{
    public class Customizable : Base.Custom_Base
    {
        private string _strConnectionString;
        private Common_Actions objAct;

        public Customizable(string strConnectionString)
        {
            _strConnectionString = strConnectionString;
            objAct = new Common_Actions();
        }

        /// <summary>
        /// Custom Pages that should be excluded from Session Cleanup functions
        /// </summary>
        public override void CustomExcludesFromSessionClenup(ref List<string> PagesList)
        {
            //Content
        }

        /// <summary>
        /// Custom Fields that will go in to Letters Templates shouhld be added here
        /// </summary>
        public override void CustomLetterTemplateFields(ref List<LetterTemplateFields> FieldsList, DS_Tasks dsTS, DS_Entity_Level_2.tblentity_level_2Row drEL2, DS_Users.tblusersRow drUser) 
        {
            //Content
        }

        /// <summary>
        /// Custom Actions to be done at the time of Starting the App (First Login of the day)
        /// </summary>
        public override void CustomAppStartupActions(string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            //Content
        }

        /// <summary>
        /// Custom Filterations for Task List - My Tasks 
        /// </summary>
        public override void CustomTaskListFilterations(DS_Tasks dsList, int User_ID)
        {
            //Content
        }

        /// <summary>
        /// Custom Filterations for Task List - My Involvements 
        /// </summary>
        public override void CustomTaskListInvolvementFilterations(DS_Tasks dsList, int User_ID, int isAdmin)
        {
            //Content
        }

        /// <summary>
        /// Custom Filterations for Users to Access a Task 
        /// </summary>
        public override void CustomTaskAccessUsersFilterations(DS_Tasks dsTask, DS_Users dsUsers)
        {
            //Content
        }

        /// <summary>
        /// Custom Validations that should happen at the opening of a Task
        /// </summary>
        public override bool CustomTaskOpenValidations(DS_Tasks dsTS, List<int> User_ID, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            bool ret = true;
            //Content
            return ret;
        }

        /// <summary>
        /// Custom Adjuetments to the Task Submit Form should be added here.
        /// </summary>
        public override void CustomTaskPostFormAdjustments(DS_Tasks dsTS, ref Task_Controls_Main objControlsList, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            //content
        }

        /// <summary>
        /// Custom Information that should go in to Task Screen should be generated here as HTML
        /// </summary>
        public override string LoadTaskRelatedData(DS_Tasks ds, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            string ret = "";
            //Content
            return ret;
        }

        /// <summary>
        /// Custom JavaScript that should be included in the Task Page should be gerenared here
        /// </summary>
        public override string LoadTaskRelatedScripts(DS_Tasks ds, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            string ret = "";
            //Content
            if (ds.tbltasks[0].Current_Step_ID == 88)
            {
                List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = ds.tbltask_history.Where(x => x.Workflow_Step_ID == 86 && x.Task_ID == ds.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

                List<DS_Tasks.tbltask_update_fieldsRow> taskInquiryHolidayType = ds.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && x.Workflow_Step_Field_ID == 42 && x.Field_Value == "Yes")
                                .OrderBy(y => y.Task_Update_Field_ID)
                                .ToList();

                ret = "init.push(function () {\r\n" +
                                "CheckPaymentType();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_71').change(function() {\r\n" +
                                "CheckPaymentType();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_72').change(function() {\r\n" +
                                "DisplayCashFields();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_73').change(function() {\r\n" +
                                "DisplayCashFields();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_74').change(function() {\r\n" +
                                "DisplayVoucherField();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_78').change(function() {\r\n" +
                                "DisplayCreditFields();\r\n" +
                        "});\r\n" +
                        "function CheckPaymentType() {\r\n" +
                             "if ($('#Field_ID_71').val() == 'Cash'){\r\n" +
                                 "$('#ControlContainer_72').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_73').addClass('hide');\r\n" +
                                 "$('#ControlContainer_74').addClass('hide');\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#ControlContainer_78').addClass('hide');\r\n" +
                                 "$('#ControlContainer_79').addClass('hide');\r\n" +
                                 "$('#ControlContainer_80').addClass('hide');\r\n" +
                                 "$('#ControlContainer_81').addClass('hide');\r\n" +
                                 "$('#Field_ID_78').prop('checked', false);\r\n" +
                                 "$('#Field_ID_74').prop('checked', false);\r\n" +
                                 "$('#Field_ID_81').prop('checked', false);\r\n" +
                                 "$('#Field_ID_78').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_74').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_81').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_79').val('');\r\n" +
                                 "$('#Field_ID_75').val('');\r\n" +
                                 "$('#Field_ID_80').val('');\r\n" +
                             "}\r\n" +
                             "else if ($('#Field_ID_71').val() == 'Credit'){\r\n" +
                                 "$('#ControlContainer_78').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_72').addClass('hide');\r\n" +
                                 "$('#ControlContainer_73').addClass('hide');\r\n" +
                                 "$('#ControlContainer_74').addClass('hide');\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#ControlContainer_79').addClass('hide');\r\n" +
                                 "$('#ControlContainer_80').addClass('hide');\r\n" +
                                 "$('#ControlContainer_81').addClass('hide');\r\n" +
                                 "$('#Field_ID_72').prop('checked', false);\r\n" +
                                 "$('#Field_ID_73').prop('checked', false);\r\n" +
                                 "$('#Field_ID_74').prop('checked', false);\r\n" +
                                 "$('#Field_ID_81').prop('checked', false);\r\n" +
                                 "$('#Field_ID_72').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_73').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_74').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_81').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_75').val('');\r\n" +
                                 "$('#Field_ID_80').val('');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_72').addClass('hide');\r\n" +
                                 "$('#ControlContainer_73').addClass('hide');\r\n" +
                                 "$('#ControlContainer_74').addClass('hide');\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#ControlContainer_78').addClass('hide');\r\n" +
                                 "$('#ControlContainer_79').addClass('hide');\r\n" +
                                 "$('#ControlContainer_80').addClass('hide');\r\n" +
                                 "$('#ControlContainer_81').addClass('hide');\r\n" +
                                 "$('#Field_ID_72').prop('checked', false);\r\n" +
                                 "$('#Field_ID_73').prop('checked', false);\r\n" +
                                 "$('#Field_ID_78').prop('checked', false);\r\n" +
                                 "$('#Field_ID_74').prop('checked', false);\r\n" +
                                 "$('#Field_ID_81').prop('checked', false);\r\n" +
                                 "$('#Field_ID_72').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_73').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_78').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_74').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_81').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_79').val('');\r\n" +
                                 "$('#Field_ID_75').val('');\r\n" +
                                 "$('#Field_ID_80').val('');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function DisplayCashFields() {\r\n" +
                             "if ($('#Field_ID_72').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_73').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_73').addClass('hide');\r\n" +
                             "}\r\n" +
                             "if ($('#Field_ID_72').is(\":checked\") && $('#Field_ID_73').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_74').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_74').addClass('hide');\r\n" +
                             "}\r\n" +
                             "if (!$('#Field_ID_72').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_73').addClass('hide');\r\n" +
                                 "$('#ControlContainer_74').addClass('hide');\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#Field_ID_73').prop('checked', false);\r\n" +
                                 "$('#Field_ID_74').prop('checked', false);\r\n" +
                                 "$('#Field_ID_73').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_74').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_75').val('');\r\n" +
                             "}\r\n" +
                             "if (!$('#Field_ID_73').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_74').addClass('hide');\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#Field_ID_74').prop('checked', false);\r\n" +
                                 "$('#Field_ID_74').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_75').val('');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function DisplayCreditFields() {\r\n" +
                             "if ($('#Field_ID_78').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_79').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_74').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#Field_ID_75').val('');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_79').addClass('hide');\r\n" +
                                 "$('#ControlContainer_74').addClass('hide');\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#Field_ID_74').prop('checked', false);\r\n" +
                                 "$('#Field_ID_74').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_79').val('');\r\n" +
                                 "$('#Field_ID_75').val('');\r\n" +
                             "}\r\n" +
                         "}\r\n";

                if (taskInquiryHolidayType.Count > 0)
                {
                    ret += "function DisplayVoucherField() {\r\n" +
                             "if ($('#Field_ID_74').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_75').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_81').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#ControlContainer_81').addClass('hide');\r\n" +
                                 "$('#Field_ID_75').val('');\r\n" +
                                 "$('#Field_ID_81').prop('checked', false);\r\n" +
                                 "$('#Field_ID_81').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                           "}\r\n" +
                           "$('#Field_ID_81').change(function() {\r\n" +
                                "DisplayTTField();\r\n" +
                           "});\r\n" +
                           "function DisplayTTField() {\r\n" +
                             "if ($('#Field_ID_81').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_80').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_80').addClass('hide');\r\n" +
                             "}\r\n" +
                           "}\r\n";
                }
                else
                {
                    ret += "function DisplayVoucherField() {\r\n" +
                             "if ($('#Field_ID_74').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_75').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#Field_ID_75').val('');\r\n" +
                             "}\r\n" +
                         "}\r\n";
                }                
            }
            return ret;
        }

        /// <summary>
        /// Custom Actions that should happen after uploading a file using the Task Posting Section should be performed here
        /// </summary>
        public override void CustomTaskUploadActions(List<Task_Doc_Custom_Actions> TaskDocUploadActions, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            //Content
        }

        /// <summary>
        /// Custom Actions that should happen after uploading a file using Task Addon Sections should be performed here
        /// </summary>
        public override void CustomTaskAddonUploadActions(List<Task_Doc_Custom_Actions> TaskDocUploadActions, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            //Content
        }

        /// <summary>
        /// Custom Actins that should happen at the time of Posting a Task
        /// </summary>
        public override void CustomTaskPostActions(DS_Tasks dsTS, int User_ID, string PhysicalRootFolder, string WebRootFolder, bool AtBeginingOfStep, string CurrencySymbol, bool ReadContent)
        {
            //content
        }

        /// <summary>
        /// Custom Actins that should happen at the time of Posting an Addon
        /// </summary>
        public override void CustomAddonPostActions(Task_Controls_Main objControlsList, int User_ID, string PhysicalRootFolder, string WebRootFolder, string CurrencySymbol, bool ReadContent)
        {
            //content
        }

        /// <summary>
        /// Custom Actins that should happen at the time of Rejecting a Task
        /// </summary>
        public override void CustomTaskRejectActions(DS_Tasks dsTS, int User_ID, string PhysicalRootFolder, string WebRootFolder, bool AtBeginingOfStep, string CurrencySymbol, bool ReadContent)
        {
            //content
        }

        /// <summary>
        /// Custom Validations that should happen before a task is being Posted
        /// </summary>
        public override ActionValidated CustomTaskPostValidations(int Task_ID, int Workflow_ID, int Current_Step_ID, Task_Controls_Main objControlsList, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            ActionValidated ret = new ActionValidated();
            ret.Validated = true;
            //Content
            ret.Reason = objAct.CleanJavaScript(ret.Reason);
            return ret;
        }

        /// <summary>
        /// Custom Validations that should happen before an Addon is being Posted
        /// </summary>
        public override ActionValidated CustomAddonPostValidations(Task_Controls_Main objControlsList, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            ActionValidated ret = new ActionValidated();
            ret.Validated = true;
            //Content
            ret.Reason = objAct.CleanJavaScript(ret.Reason);
            return ret;
        }

        /// <summary>
        /// Custom Javascript Validations that should happen on Task Post
        /// </summary>
        public override string CustomTaskPostScripts(DS_Tasks dsTS, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            string ret = "";
            //Content
            return ret;
        }

        /// <summary>
        /// Custom Validations that should happen at the time of Starting a Task
        /// </summary>
        public override ActionValidated CustomTaskStartValidations(ref int Task_Owner_ID, ref int Workflow_ID, ref int Current_Step_ID, ref string ExtraField1, ref string ExtraField2, ref string AdditionalComment, ref List<StepPostData> StepPosts, int Entity_L2_ID, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            ActionValidated ret = new ActionValidated();
            ret.Validated = true;
            //Content
            ret.Reason = objAct.CleanJavaScript(ret.Reason);
            return ret;
        }

        /// <summary>
        /// Custom Actions that should happen at the Time of Starting a Task on E-Mail Rules
        /// </summary>
        public override ActionValidated CustomEmailTaskStartValidations(ref int Task_Owner_ID, ref int Workflow_ID, ref int Current_Step_ID, ref string ExtraField1, ref string ExtraField2, ref string AdditionalComment, ref List<StepPostData> StepPosts, int Entity_L2_ID, string MailTitle, string FromAddress, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            ActionValidated ret = new ActionValidated();
            ret.Validated = true;
            //Content
            return ret;
        }

        /// <summary>
        /// Custom Actions that should happen at the Time of Starting a Task on Folder Read Rules
        /// </summary>
        public override ActionValidated CustomFolderReadTaskStartValidations(ref int Task_Owner_ID, ref int Workflow_ID, ref int Current_Step_ID, ref string ExtraField1, ref string ExtraField2, ref string AdditionalComment, ref List<StepPostData> StepPosts, int Entity_L2_ID, string FileName, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            ActionValidated ret = new ActionValidated();
            ret.Validated = true;
            //Content
            return ret;
        }

        /// <summary>
        /// Custom Actions that should happen at the Time after a succesfull API Call
        /// </summary>
        public override void CustomTaskAPICallActions(DS_Tasks dsTS, int User_ID, string PhysicalRootFolder, string WebRootFolder, bool ReadContent, bool AtBeginingOfStep, ref string API_Call_Result)
        {
            //Content
        }

        /// <summary>
        /// Custom Actions that should happen After any Automated Task Creation
        /// </summary>
        public override void CustomPostAutomaticTaskCreationActions(string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            //Content
        }

        /// <summary>
        /// Custom Actions that should happen after Deleting a Task
        /// </summary>
        public override void CustomTaskDeleteActions(int Task_ID, string PhysicalRootFolder, string WebRootFolder, bool ReadContent)
        {
            //Content
        }

        private string AddTaskRelatedDataRow(string RowDescription, string RowValue, bool RowBottomPadding = false)
        {
            return "<div class=\"row" + (RowBottomPadding ? " pb5" : "") + "\">" +
                        "<div class=\"col-md-7\">" +
                            RowDescription +
                        "</div>" +
                        "<div class=\"col-md-5 text-right\">" +
                            RowValue +
                        "</div>" +
                    "</div> ";
        }
    }
}