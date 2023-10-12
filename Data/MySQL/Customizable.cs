﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Custom.Data.MySQL;
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
                             //"$('#Field_ID_323').prop('checked', false);\r\n" +
                             //"$('#Field_ID_323').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_323').addClass('hide');\r\n" +
                             "$('#ControlContainer_324').addClass('hide');\r\n" +
                             "$('#ControlContainer_326').addClass('hide');\r\n" +
                             "$('#ControlContainer_325').addClass('hide');\r\n" +
                             "$('#ControlContainer_327').addClass('hide');\r\n" +
                             //"$('#Field_ID_323').prop('checked', false);\r\n" +
                             //"$('#Field_ID_323').parent().removeClass('checked');\r\n" +
                             //"$('#Field_ID_324').prop('checked', false);\r\n" +
                             //"$('#Field_ID_324').parent().removeClass('checked');\r\n" +
                             //"$('#Field_ID_326').prop('checked', false);\r\n" +
                             //"$('#Field_ID_326').parent().removeClass('checked');\r\n" +
                             //"$('#Field_ID_327').prop('checked', false);\r\n" +
                             //"$('#Field_ID_327').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "}\r\n" +

                        //HotelAvailable function
                        "function HotelAvailable() {\r\n" +
                        "if ($('#Field_ID_323').is(':checked')) {\r\n" +
                             "$('#ControlContainer_324').removeClass('hide');\r\n" +
                             //"$('#Field_ID_324').prop('checked', false);\r\n" +
                             //"$('#Field_ID_324').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_324').addClass('hide');\r\n" +
                             "$('#ControlContainer_326').addClass('hide');\r\n" +
                             "$('#ControlContainer_325').addClass('hide');\r\n" +
                             "$('#ControlContainer_327').addClass('hide');\r\n" +
                             //"$('#Field_ID_324').prop('checked', false);\r\n" +
                             //"$('#Field_ID_324').parent().removeClass('checked');\r\n" +
                             //"$('#Field_ID_326').prop('checked', false);\r\n" +
                             //"$('#Field_ID_326').parent().removeClass('checked');\r\n" +
                             //"$('#Field_ID_327').prop('checked', false);\r\n" +
                             //"$('#Field_ID_327').parent().removeClass('checked');\r\n" +

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
                             //"$('#Field_ID_326').prop('checked', false);\r\n" +
                             //"$('#Field_ID_326').parent().removeClass('checked');\r\n" +
                             //"$('#Field_ID_327').prop('checked', false);\r\n" +
                             //"$('#Field_ID_327').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "}\r\n" +

                        //HotelConfirm function
                        "function HotelConfirm() {\r\n" +
                        "if ($('#Field_ID_326').is(':checked')) {\r\n" +
                             "$('#ControlContainer_325').removeClass('hide');\r\n" +
                             "$('#ControlContainer_327').removeClass('hide');\r\n" +
                             //"$('#Field_ID_327').prop('checked', false);\r\n" +
                             //"$('#Field_ID_327').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_325').addClass('hide');\r\n" +
                             "$('#ControlContainer_327').addClass('hide');\r\n" +
                             //"$('#Field_ID_327').prop('checked', false);\r\n" +
                             //"$('#Field_ID_327').parent().removeClass('checked');\r\n" +
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
                             //"$('#Field_ID_329').prop('checked', false);\r\n" +
                             //"$('#Field_ID_329').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_329').addClass('hide');\r\n" +
                             "$('#ControlContainer_360').addClass('hide');\r\n" +
                             //"$('#Field_ID_329').prop('checked', false);\r\n" +
                             //"$('#Field_ID_329').parent().removeClass('checked');\r\n" +
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
                             //"$('#Field_ID_331').prop('checked', false);\r\n" +
                             //"$('#Field_ID_331').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_331').addClass('hide');\r\n" +
                             "$('#ControlContainer_333').addClass('hide');\r\n" +
                             "$('#ControlContainer_334').addClass('hide');\r\n" +
                             "$('#ControlContainer_335').addClass('hide');\r\n" +
                             "$('#ControlContainer_336').addClass('hide');\r\n" +
                             "$('#ControlContainer_337').addClass('hide');\r\n" +
                             //"$('#Field_ID_331').prop('checked', false);\r\n" +
                             //"$('#Field_ID_331').parent().removeClass('checked');\r\n" +
                             //"$('#Field_ID_334').prop('checked', false);\r\n" +
                             //"$('#Field_ID_334').parent().removeClass('checked');\r\n" +
                             //"$('#Field_ID_336').prop('checked', false);\r\n" +
                             //"$('#Field_ID_336').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "}\r\n"+

                        //PreparePacket function
                        "function PreparePacket() {\r\n" +
                        "if ($('#Field_ID_331').is(':checked')) {\r\n" +
                             "$('#ControlContainer_334').removeClass('hide');\r\n" +
                             "$('#ControlContainer_335').removeClass('hide');\r\n" +
                             //"$('#Field_ID_334').prop('checked', false);\r\n" +
                             //"$('#Field_ID_334').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_334').addClass('hide');\r\n" +
                             "$('#ControlContainer_335').addClass('hide');\r\n" +
                             "$('#ControlContainer_336').addClass('hide');\r\n" +
                             "$('#ControlContainer_337').addClass('hide');\r\n" +
                             //"$('#Field_ID_334').prop('checked', false);\r\n" +
                             //"$('#Field_ID_334').parent().removeClass('checked');\r\n" +
                             //"$('#Field_ID_336').prop('checked', false);\r\n" +
                             //"$('#Field_ID_336').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "}\r\n"+

                        //GuideBriefing function
                        "function GuideBriefing() {\r\n" +
                        "if ($('#Field_ID_334').is(':checked')) {\r\n" +
                             "$('#ControlContainer_336').removeClass('hide');\r\n" +
                             "$('#ControlContainer_337').removeClass('hide');\r\n" +
                             //"$('#Field_ID_336').prop('checked', false);\r\n" +
                             //"$('#Field_ID_336').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "else {\r\n" +
                             "$('#ControlContainer_336').addClass('hide');\r\n" +
                             "$('#ControlContainer_337').addClass('hide');\r\n" +
                             //"$('#Field_ID_336').prop('checked', false);\r\n" +
                             //"$('#Field_ID_336').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                        "}\r\n";
            }

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
            if (ds.tbltasks[0].Current_Step_ID == 99)
            {
                List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = ds.tbltask_history.Where(x => x.Workflow_Step_ID == 87 && x.Task_ID == ds.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

                int[] countrySPFieldIDs = { 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 402, 403, 404, 405, 406, 407, 408, 409, 410, 411, 412, 413, 414 };

                List<DS_Tasks.tbltask_update_fieldsRow> taskCountrySelected = ds.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && countrySPFieldIDs.Contains(x.Workflow_Step_Field_ID) && x.Field_Value == "Yes")
                                .OrderBy(y => y.Task_Update_Field_ID)
                                .ToList();

                string fieldNames = string.Join(", ", taskCountrySelected.Select(x => x.Field_Name));

                ret = "$('#Field_ID_106').val('" + fieldNames + "');\r\n";
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
            if (Current_Step_ID == 83)
            {
                //List<Task_Controls> dr = objControlsList.Controls.Where(x => x.Field_ID == 35).ToList();
                //if (dr.Count > 0)
                //{
                //Task_Field_Data objTaskData = new Task_Field_Data(_strConnectionString);
                //string email = objTaskData.GetFieldForTask(Task_ID, 35);
                //if (!Utilities.IsValidEmail(email))
                //{
                //    ret.Validated = false;
                //    ret.Reason = "Your email is not a valid email address";
                //}
                //}
                List<Task_Controls> drUI2 = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 35).ToList();
                foreach (Task_Controls ctrl in drUI2)
                {
                    TextBox txrt = (TextBox)ctrl.UI_Control;
                    if (!Utilities.IsValidEmail(txrt.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Your email is not a valid email address";
                    }
                }
            }
            if (Current_Step_ID == 86)
            {
                //Validate pax count
            }
            if (Current_Step_ID == 87)
            {
                //Validate contat no, email, date of exipiry, date range of passport, date range of ticketing, ffn
            }
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