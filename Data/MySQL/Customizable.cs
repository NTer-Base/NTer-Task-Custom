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
            // displaying many checkboxes that track the status of tasks can be confusing.
            // therefore only one checkbox at a time is shown.
            // this is done using jquery.
            if (ds.tbltasks[0].Current_Step_ID == 123) // For step 1.2.2 Acknowledge Inquiry (Quotation Status)
            {
                ret = "init.push(function () {\r\n" +
                                "CheckQuotationStatus();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_379').change(function() {\r\n" + // Quotation status selection
                                "CheckQuotationStatus();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_380').change(function() {\r\n" + // Confirmation sent checkbox
                                "CheckConfirmationSent();\r\n" +
                        "});\r\n" +
                        "function CheckQuotationStatus() {\r\n" +
                             "if ($('#Field_ID_379').val() == 'Confirmation of the inquiry'){\r\n" + // Only show the next fields when the quotation status is
                                 "$('#ControlContainer_380').removeClass('hide');\r\n" +             // confirmation of inquiry
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_380').addClass('hide');\r\n" +
                                 "$('#ControlContainer_381').addClass('hide');\r\n" +
                                 "$('#ControlContainer_382').addClass('hide');\r\n" +
                             "}\r\n" +   
                         "}\r\n" +
                         "function CheckConfirmationSent() {\r\n" +
                            "if ($('#Field_ID_380').is(\":checked\")){\r\n" + // Confirmation sent checkbox
                                 "$('#ControlContainer_381').removeClass('hide');\r\n" + // Upload amended quotation
                                 "$('#ControlContainer_382').removeClass('hide');\r\n" + // Amend quotation sent checkbox
                              "}\r\n" +
                            "else {\r\n" +
                                 "$('#ControlContainer_381').addClass('hide');\r\n" + // Upload amended quotation
                                 "$('#ControlContainer_382').addClass('hide');\r\n" + // Amend quotation sent checkbox
                            "}\r\n" +
                          "}\r\n";
            }

            if (ds.tbltasks[0].Current_Step_ID == 113) // For step 2.2 Inquiry (Series group)
            {
                ret = "init.push(function () {\r\n" +
                                "CheckHotelRatesSent();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_353').change(function() {\r\n" +
                                "CheckHotelRatesSent();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_354').change(function() {\r\n" +
                                "CheckAllotmentConfirmed();\r\n" +
                        "});\r\n" +
                        "function CheckHotelRatesSent() {\r\n" +
                            "if ($('#Field_ID_353').is(\":checked\")){\r\n" + // Hotel rates sent to client checkbox
                                "$('#ControlContainer_354').removeClass('hide');\r\n" + // Allotment confirmed checkbox
                                 "$('#ControlContainer_355').addClass('hide');\r\n" + // Hotel rates confirmed checkbox
                                "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                            "}\r\n" +
                            "else {\r\n" +
                               "$('#ControlContainer_354').addClass('hide');\r\n" + // Allotment confirmed checkbox
                                "$('#ControlContainer_355').addClass('hide');\r\n" + // Hotel rates confirmed checkbox
                                "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                            "}\r\n" +
                        "}\r\n" +
                         "function CheckAllotmentConfirmed() {\r\n" +
                             "if ($('#Field_ID_354').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_355').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_355').addClass('hide');\r\n" +
                                 "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                             "}\r\n" +
                         "}\r\n";
            };


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