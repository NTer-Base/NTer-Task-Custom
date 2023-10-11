using System;
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
                                 "$('#Field_ID_380').prop('checked', false);\r\n" +
                                 "$('#Field_ID_380').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_382').prop('checked', false);\r\n" +
                                 "$('#Field_ID_382').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_380').addClass('hide');\r\n" +
                                 "$('#ControlContainer_381').addClass('hide');\r\n" +
                                 "$('#ControlContainer_382').addClass('hide');\r\n" +
                                 "$('#Field_ID_380').prop('checked', false);\r\n" +
                                 "$('#Field_ID_380').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_382').prop('checked', false);\r\n" +
                                 "$('#Field_ID_382').parent().removeClass('checked');\r\n" +
                             "}\r\n" +   
                         "}\r\n" +
                         "function CheckConfirmationSent() {\r\n" +
                            "if ($('#Field_ID_380').is(\":checked\")){\r\n" + // Confirmation sent checkbox
                                 "$('#ControlContainer_381').removeClass('hide');\r\n" + // Upload amended quotation
                                 "$('#ControlContainer_382').removeClass('hide');\r\n" + // Amend quotation sent checkbox
                                  "$('#Field_ID_382').prop('checked', false);\r\n" +   // Reset amended quotation sent checkbox to default value
                                 "$('#Field_ID_382').parent().removeClass('checked');\r\n" +
                              "}\r\n" +
                            "else {\r\n" +
                                 "$('#ControlContainer_381').addClass('hide');\r\n" + // Upload amended quotation
                                 "$('#ControlContainer_382').addClass('hide');\r\n" + // Amend quotation sent checkbox
                                 "$('#Field_ID_382').prop('checked', false);\r\n" +   // Uncheck amended quotation sent checkbox when coming from previous field
                                 "$('#Field_ID_382').parent().removeClass('checked');\r\n" + 
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
                         "$('#Field_ID_355').change(function() {\r\n" +
                                "CheckHotelRatesConfirmed();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_356').change(function() {\r\n" +
                                "CheckCostingUpdated();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_357').change(function() {\r\n" +
                                "CheckTourOperatorConfirmed();\r\n" +
                        "});\r\n" +
                        "function CheckHotelRatesSent() {\r\n" +
                            "if ($('#Field_ID_353').is(\":checked\")){\r\n" + // Hotel rates sent to client checkbox
                                "$('#ControlContainer_354').removeClass('hide');\r\n" + // Allotment confirmed checkbox
                                "$('#ControlContainer_355').addClass('hide');\r\n" + // Hotel rates confirmed checkbox
                                "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                "$('#Field_ID_354').prop('checked', false);\r\n" +  // Reset Allotment confirmed checkbox
                                "$('#Field_ID_354').parent().removeClass('checked');\r\n" +
                                "$('#Field_ID_355').prop('checked', false);\r\n" +  // Reset Hotel rates confirmed checkbox
                                "$('#Field_ID_355').parent().removeClass('checked');\r\n" +
                                "$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                "$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                "$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                "$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                "$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                            "}\r\n" +
                            "else {\r\n" +
                               "$('#ControlContainer_354').addClass('hide');\r\n" + // Allotment confirmed checkbox
                                "$('#ControlContainer_355').addClass('hide');\r\n" + // Hotel rates confirmed checkbox
                                "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                "$('#Field_ID_354').prop('checked', false);\r\n" +  // Reset Allotment confirmed checkbox
                                "$('#Field_ID_354').parent().removeClass('checked');\r\n" +
                                "$('#Field_ID_355').prop('checked', false);\r\n" +  // Reset Hotel rates confirmed checkbox
                                "$('#Field_ID_355').parent().removeClass('checked');\r\n" +
                                "$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                "$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                "$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                "$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                "$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                            "}\r\n" +
                        "}\r\n" +
                         "function CheckAllotmentConfirmed() {\r\n" +
                             "if ($('#Field_ID_354').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_355').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 "$('#Field_ID_355').prop('checked', false);\r\n" +  // Reset Hotel rates confirmed checkbox
                                 "$('#Field_ID_355').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                 "$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                 "$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 "$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_355').addClass('hide');\r\n" +
                                 "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 "$('#Field_ID_355').prop('checked', false);\r\n" +  // Reset Hotel rates confirmed checkbox
                                 "$('#Field_ID_355').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                 "$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                 "$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 "$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function CheckHotelRatesConfirmed() {\r\n" +
                             "if ($('#Field_ID_355').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_361').removeClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').removeClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 "$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                 "$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                 "$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 "$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 "$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                 "$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                 "$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 "$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function CheckCostingUpdated() {\r\n" +
                             "if ($('#Field_ID_356').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_357').removeClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 "$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 "$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 "$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 "$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function CheckTourOperatorConfirmed() {\r\n" +
                             "if ($('#Field_ID_357').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_358').removeClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 "$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 "$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                         "}\r\n";
            }

            if (ds.tbltasks[0].Current_Step_ID == 111) // For step 5.2 Refund Process
            {
                ret = "init.push(function () {\r\n" +
                        "CheckCreditNoteRaised();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_338').change(function() {\r\n" + // attach event listner to call CheckCreditNoteRaised(); when the given field value is changed
                                "CheckCreditNoteRaised();\r\n" +
                        "});\r\n" +
                         "$('#Field_ID_341').change(function() {\r\n" + // attach event listner to call CheckRefundableProcessStatus(); when the given field value is changed
                                "CheckRefundableProcessStatus();\r\n" +
                        "});\r\n" +
                        "function CheckCreditNoteRaised() {\r\n" +
                             "if ($('#Field_ID_338').is(\":checked\")){\r\n" + // Is credit note raised checkbox
                                 "$('#ControlContainer_341').removeClass('hide');\r\n" + // Status of refundable process selection
                                 "$('#ControlContainer_339').addClass('hide');\r\n" + // Voucher number textbox
                                 "$('#ControlContainer_340').addClass('hide');\r\n" + // Upload invoice file upload
                                 "$('#Field_ID_341').val('-');\r\n" + // Reset Status of refundable process to '-'
                                 "$('#Field_ID_339').val('');\r\n" +  // Reset Voucher Number field 
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_341').addClass('hide');\r\n" + // Status of refundable process selection
                                 "$('#ControlContainer_339').addClass('hide');\r\n" + // Voucher number textbox
                                 "$('#ControlContainer_340').addClass('hide');\r\n" + // Upload invoice file upload
                                 "$('#Field_ID_341').val('-');\r\n" + // Reset Status of refundable process to '-'
                                 "$('#Field_ID_339').val('');\r\n" +  // Reset Voucher Number field 
                             "}\r\n" +
                         "}\r\n" +
                         "function CheckRefundableProcessStatus() {\r\n" +
                             "if ($('#Field_ID_341').val() == 'Refund completed'){\r\n" + // Status of refundable process selection
                                 "$('#ControlContainer_339').removeClass('hide');\r\n" + // Voucher number textbox
                                 "$('#ControlContainer_340').removeClass('hide');\r\n" + // Upload invoice file upload
                                 "$('#Field_ID_339').val('');\r\n" +  // Reset Voucher Number field 
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_339').addClass('hide');\r\n" + // Voucher number textbox
                                 "$('#ControlContainer_340').addClass('hide');\r\n" + // Upload invoice file upload
                                 "$('#Field_ID_339').val('');\r\n" +  // Reset Voucher Number field 
                             "}\r\n" +
                         "}\r\n";
            }
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
                    Task_Field_Data objTaskData = new Task_Field_Data(_strConnectionString);
                    string email = objTaskData.GetFieldForTask(Task_ID, 35);
                    if (!Utilities.IsValidEmail(email))
                    {
                        ret.Validated = false;
                        ret.Reason = "Your email is not a valid email address";
                    }
                //}
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