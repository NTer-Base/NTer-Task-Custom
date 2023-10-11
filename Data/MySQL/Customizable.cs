﻿using System;
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

            //Inquiry (Non-series group)
            if (ds.tbltasks[0].Current_Step_ID == 107)
            {
                ret = "init.push(function () {\r\n" +
                                "HotelName();\r\n" +
                        "});\r\n" +

                        "$('#Field_ID_322').change(function() {\r\n" +
                                "HotelName();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_323').change(function() {\r\n" +
                                "HotelAvailable();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_324').change(function() {\r\n" +
                                "HotelBooking();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_326').change(function() {\r\n" +
                                "HotelConfirm();\r\n" +
                        "});\r\n" +

                        //HotelName function
                        "function HotelName() {\r\n" +
                        "if ($('#Field_ID_322').val() != '-'){\r\n" +
                             "$('#ControlContainer_323').removeClass('hide');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_323').addClass('hide');\r\n" +
                             "$('#ControlContainer_324').addClass('hide');\r\n" +
                             "$('#ControlContainer_326').addClass('hide');\r\n" +
                             "$('#ControlContainer_325').addClass('hide');\r\n" +
                             "$('#ControlContainer_327').addClass('hide');\r\n" +
                             "}\r\n" +
                        "}\r\n" +

                        //HotelAvailable function
                        "function HotelAvailable() {\r\n" +
                        "if ($('#Field_ID_323').is(':checked')) {\r\n" +
                             "$('#ControlContainer_324').removeClass('hide');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_324').addClass('hide');\r\n" +
                             "$('#ControlContainer_326').addClass('hide');\r\n" +
                             "$('#ControlContainer_325').addClass('hide');\r\n" +
                             "$('#ControlContainer_327').addClass('hide');\r\n" +
                             "}\r\n" +
                        "}\r\n" +

                        //HotelBooking function
                        "function HotelBooking() {\r\n" +
                        "if ($('#Field_ID_324').is(':checked')) {\r\n" +
                             "$('#ControlContainer_326').removeClass('hide');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_326').addClass('hide');\r\n" +
                             "$('#ControlContainer_325').addClass('hide');\r\n" +
                             "$('#ControlContainer_327').addClass('hide');\r\n" +
                             "}\r\n" +
                        "}\r\n" +

                        //HotelConfirm function
                        "function HotelConfirm() {\r\n" +
                        "if ($('#Field_ID_326').is(':checked')) {\r\n" +
                             "$('#ControlContainer_325').removeClass('hide');\r\n" +
                             "$('#ControlContainer_327').removeClass('hide');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_325').addClass('hide');\r\n" +
                             "$('#ControlContainer_327').addClass('hide');\r\n" +
                             "}\r\n" +
                        "}\r\n";
            }

            //Payment Process
            if (ds.tbltasks[0].Current_Step_ID == 108)
            {
                ret = "init.push(function () {\r\n" +
                                "PaymentProcess();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_328').change(function() {\r\n" +
                                "PaymentProcess();\r\n" +
                        "});\r\n" +

                        //PaymentProcess function
                        "function PaymentProcess() {\r\n" +
                        "if ($('#Field_ID_328').is(':checked')) {\r\n" +
                             "$('#ControlContainer_329').removeClass('hide');\r\n" +
                             "$('#ControlContainer_360').removeClass('hide');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_329').addClass('hide');\r\n" +
                             "$('#ControlContainer_360').addClass('hide');\r\n" +
                             "}\r\n" +
                        "}\r\n";
            }

            //Close Inquiry
            if (ds.tbltasks[0].Current_Step_ID == 109)
            {
                ret = "init.push(function () {\r\n" +
                                "TransportRequisition();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_330').change(function() {\r\n" + //TransportRequisition function
                                "TransportRequisition();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_331').change(function() {\r\n" + //PreparePacket function
                                "PreparePacket();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_334').change(function() {\r\n" + //GuideBriefing function
                                "GuideBriefing();\r\n" +
                        "});\r\n" +

                        //TransportRequisition function
                        "function TransportRequisition() {\r\n" +
                        "if ($('#Field_ID_330').is(':checked')) {\r\n" +
                             "$('#ControlContainer_331').removeClass('hide');\r\n" +
                             "$('#ControlContainer_333').removeClass('hide');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_331').addClass('hide');\r\n" +
                             "$('#ControlContainer_333').addClass('hide');\r\n" +
                             "$('#ControlContainer_334').addClass('hide');\r\n" +
                             "$('#ControlContainer_335').addClass('hide');\r\n" +
                             "$('#ControlContainer_336').addClass('hide');\r\n" +
                             "$('#ControlContainer_337').addClass('hide');\r\n" +
                             "}\r\n" +
                        "}\r\n"+

                        //PreparePacket function
                        "function PreparePacket() {\r\n" +
                        "if ($('#Field_ID_331').is(':checked')) {\r\n" +
                             "$('#ControlContainer_334').removeClass('hide');\r\n" +
                             "$('#ControlContainer_335').removeClass('hide');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_334').addClass('hide');\r\n" +
                             "$('#ControlContainer_335').addClass('hide');\r\n" +
                             "$('#ControlContainer_336').addClass('hide');\r\n" +
                             "$('#ControlContainer_337').addClass('hide');\r\n" +
                             "}\r\n" +
                        "}\r\n"+

                        //GuideBriefing function
                        "function GuideBriefing() {\r\n" +
                        "if ($('#Field_ID_334').is(':checked')) {\r\n" +
                             "$('#ControlContainer_336').removeClass('hide');\r\n" +
                             "$('#ControlContainer_337').removeClass('hide');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_336').addClass('hide');\r\n" +
                             "$('#ControlContainer_337').addClass('hide');\r\n" +
                             "}\r\n" +
                        "}\r\n";
            }

            //Content
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