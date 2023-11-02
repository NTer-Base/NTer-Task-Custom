using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraPrinting.Shape.Native;
using N_Ter.Common;
using N_Ter.Structures;
using N_Ter_Task_Custom.Data.MySQL;
using N_Ter_Task_Data_Structures.DataSets;
using Org.BouncyCastle.Asn1.X509;
using static Google.Protobuf.Reflection.UninterpretedOption.Types;

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
                        //"$('#Field_ID_380').change(function() {\r\n" + // Confirmation sent checkbox
                        //        "CheckConfirmationSent();\r\n" +
                        //"});\r\n" +
                        "function CheckQuotationStatus() {\r\n" +
                             "if ($('#Field_ID_379').val() == 'Confirmation of the inquiry'){\r\n" + // Only show the next fields when the quotation status is
                                 "$('#ControlContainer_381').removeClass('hide');\r\n" +             // confirmation of inquiry
                                 "$('#ControlContainer_382').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_447').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 //"$('#ControlContainer_380').addClass('hide');\r\n" +
                                 "$('#ControlContainer_381').addClass('hide');\r\n" +
                                 "$('#ControlContainer_382').addClass('hide');\r\n" +
                                 "$('#ControlContainer_447').addClass('hide');\r\n" +
                             "}\r\n" +
                         "}\r\n";
                         //"function CheckConfirmationSent() {\r\n" +
                         //   "if ($('#Field_ID_380').is(\":checked\")){\r\n" + // Confirmation sent checkbox
                         //        //"$('#ControlContainer_381').removeClass('hide');\r\n" + // Upload amended quotation
                         //        "$('#ControlContainer_382').removeClass('hide');\r\n" + // Amend quotation sent checkbox
                         //        // "$('#Field_ID_382').prop('checked', false);\r\n" +   // Reset amended quotation sent checkbox to default value
                         //        //"$('#Field_ID_382').parent().removeClass('checked');\r\n" +
                         //     "}\r\n" +
                         //   "else {\r\n" +
                         //        //"$('#ControlContainer_381').addClass('hide');\r\n" + // Upload amended quotation
                         //        "$('#ControlContainer_382').addClass('hide');\r\n" + // Amend quotation sent checkbox
                         //        //"$('#Field_ID_382').prop('checked', false);\r\n" +   // Uncheck amended quotation sent checkbox when coming from previous field
                         //        //"$('#Field_ID_382').parent().removeClass('checked');\r\n" + 
                         //   "}\r\n" +
                          //"}\r\n";
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
                                //"$('#Field_ID_354').prop('checked', false);\r\n" +  // Reset Allotment confirmed checkbox
                                //"$('#Field_ID_354').parent().removeClass('checked');\r\n" +
                                //"$('#Field_ID_355').prop('checked', false);\r\n" +  // Reset Hotel rates confirmed checkbox
                                //"$('#Field_ID_355').parent().removeClass('checked');\r\n" +
                                //"$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                //"$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                //"$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                //"$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                //"$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                            "}\r\n" +
                            "else {\r\n" +
                               "$('#ControlContainer_354').addClass('hide');\r\n" + // Allotment confirmed checkbox
                                "$('#ControlContainer_355').addClass('hide');\r\n" + // Hotel rates confirmed checkbox
                                "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                //"$('#Field_ID_354').prop('checked', false);\r\n" +  // Reset Allotment confirmed checkbox
                                //"$('#Field_ID_354').parent().removeClass('checked');\r\n" +
                                //"$('#Field_ID_355').prop('checked', false);\r\n" +  // Reset Hotel rates confirmed checkbox
                                //"$('#Field_ID_355').parent().removeClass('checked');\r\n" +
                                //"$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                //"$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                //"$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                //"$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                //"$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                            "}\r\n" +
                        "}\r\n" +
                         "function CheckAllotmentConfirmed() {\r\n" +
                             "if ($('#Field_ID_354').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_355').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 //"$('#Field_ID_355').prop('checked', false);\r\n" +  // Reset Hotel rates confirmed checkbox
                                 //"$('#Field_ID_355').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                 //"$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                 //"$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 //"$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_355').addClass('hide');\r\n" +
                                 "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 //"$('#Field_ID_355').prop('checked', false);\r\n" +  // Reset Hotel rates confirmed checkbox
                                 //"$('#Field_ID_355').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                 //"$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                 //"$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 //"$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function CheckHotelRatesConfirmed() {\r\n" +
                             "if ($('#Field_ID_355').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_361').removeClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').removeClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 //"$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                 //"$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                 //"$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 //"$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_361').addClass('hide');\r\n" + // Costing amount textbox
                                 "$('#ControlContainer_356').addClass('hide');\r\n" + // Costing updated checkbox
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 //"$('#Field_ID_361').val('');\r\n" +  // Reset Costing amount
                                 //"$('#Field_ID_356').prop('checked', false);\r\n" +  // Reset Costing updated checkbox
                                 //"$('#Field_ID_356').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 //"$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function CheckCostingUpdated() {\r\n" +
                             "if ($('#Field_ID_356').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_357').removeClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 //"$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 //"$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_357').addClass('hide');\r\n" + // Tour operator confirmed checkbox
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 //"$('#Field_ID_357').prop('checked', false);\r\n" +  // Reset Tour operator confirmed checkbox
                                 //"$('#Field_ID_357').parent().removeClass('checked');\r\n" +
                                 //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function CheckTourOperatorConfirmed() {\r\n" +
                             "if ($('#Field_ID_357').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_358').removeClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_358').addClass('hide');\r\n" + // Sent hotel voucher checkbox
                                 //"$('#Field_ID_358').prop('checked', false);\r\n" +  // Reset sent hotel vouchers checkbox
                                 //"$('#Field_ID_358').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                         "}\r\n";
            }

            if (ds.tbltasks[0].Current_Step_ID == 111) // For step 5.2 Refund Process
            {
                ret = "init.push(function () {\r\n" +
                        "CheckCreditNoteRaised();\r\n" +
                        "CheckRefundableProcessStatus();\r\n" +
                        "});\r\n" +
                        "if('#Field_ID_338') " +
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
                                 //"$('#Field_ID_341').val('-');\r\n" + // Reset Status of refundable process to '-'
                                 //"$('#Field_ID_339').val('');\r\n" +  // Reset Voucher Number field 
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_341').addClass('hide');\r\n" + // Status of refundable process selection
                                 "$('#ControlContainer_339').addClass('hide');\r\n" + // Voucher number textbox
                                 "$('#ControlContainer_340').addClass('hide');\r\n" + // Upload invoice file upload
                                 //"$('#Field_ID_341').val('-');\r\n" + // Reset Status of refundable process to '-'
                                 //"$('#Field_ID_339').val('');\r\n" +  // Reset Voucher Number field 
                             "}\r\n" +
                         "}\r\n" +
                         "function CheckRefundableProcessStatus() {\r\n" +
                             "if ($('#Field_ID_341').val() == 'Refund completed'){\r\n" + // Status of refundable process selection
                                 "$('#ControlContainer_339').removeClass('hide');\r\n" + // Voucher number textbox
                                 "$('#ControlContainer_340').removeClass('hide');\r\n" + // Upload invoice file upload
                                 //"$('#Field_ID_339').val('');\r\n" +  // Reset Voucher Number field 
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_339').addClass('hide');\r\n" + // Voucher number textbox
                                 "$('#ControlContainer_340').addClass('hide');\r\n" + // Upload invoice file upload
                                 //"$('#Field_ID_339').val('');\r\n" +  // Reset Voucher Number field 
                             "}\r\n" +
                         "}\r\n";
            }
            if (ds.tbltasks[0].Current_Step_ID == 83)
            {
                Users objUser = new Users(_strConnectionString);
                DS_Users dsUsr = objUser.ReadForGroup(4); // Reading Outbound Team User Group

                List<DS_Users.tblusersRow> assignedUserRow = dsUsr.tblusers
                                    .ToList();

                ret = "init.push(function () {\r\n" +
                                "DisplayPhoneNumber();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_441').change(function() {\r\n" +
                                "DisplayPhoneNumber();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_745').change(function() {\r\n" +
                                "AppendEmail();\r\n" +
                        "});\r\n" +
                        "function DisplayPhoneNumber() {\r\n" +
                                "if ($('#Field_ID_441').val() != '-'){\r\n" +
                                    "$('#ControlContainer_34').removeClass('hide');\r\n" +
                                "}\r\n" +
                                "else {\r\n" +
                                    "$('#ControlContainer_34').addClass('hide');\r\n" +
                                "}\r\n" +
                        "}\r\n" +
                        "function AppendEmail() {\r\n" +
                                "var username = $('#Field_ID_745 option:selected').text().split(' - ')[1];\r\n" +
                                "$('#Field_ID_757').val(username);\r\n" +
                        "}\r\n";
                ret += "$('#ControlContainer_757').addClass('hide');\r\n" +
                       "$('#Field_ID_745').empty();\r\n" +
                       "$('<option></option>').attr('value', '-').text('-').appendTo('#Field_ID_745');\r\n";

                foreach (var user in assignedUserRow)
                {
                    ret += "$('<option></option>').attr('value', '" + user.Full_Name + "').text('" + user.Full_Name + " - " + user.Username + "').appendTo('#Field_ID_745');\r\n";
                }
            }
            if (ds.tbltasks[0].Current_Step_ID == 86)
            {
                List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = ds.tbltask_history.Where(x => x.Workflow_Step_ID == 86 && x.Task_ID == ds.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

                List<DS_Tasks.tbltask_update_fieldsRow> taskInquiryHolidayType = ds.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && x.Workflow_Step_Field_ID == 42 && x.Field_Value == "Yes")
                                .OrderBy(y => y.Task_Update_Field_ID)
                                .ToList();

                ret = "init.push(function () {\r\n" +
                                "IsVisaChecked();\r\n" +
                        "});\r\n" +
                        "$('#Field_ID_43').change(function() {\r\n" +
                                "IsVisaChecked();\r\n" +
                        "});\r\n" +
                        "function IsVisaChecked() {\r\n" +
                             "if ($('#Field_ID_43').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_420').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_420').addClass('hide');\r\n" +
                                 "$('#Field_ID_420').val('');\r\n" +
                             "}\r\n" +
                         "}\r\n";
            }

            //3.1 Payment Process
            if (ds.tbltasks[0].Current_Step_ID == 88)
            {
                Users objUser = new Users(_strConnectionString);
                DS_Users dsUsr = objUser.ReadForGroup(8); // Reading Outbound Team User Group

                List<DS_Users.tblusersRow> assignedUserRow = dsUsr.tblusers
                                    .ToList();

                ret = "$('#Field_ID_763').change(function() {\r\n" +
                              "AppendEmail();\r\n" +
                      "});\r\n" +
                      "function AppendEmail() {\r\n" +
                              "var username = $('#Field_ID_763 option:selected').text().split(' - ')[1];\r\n" +
                              "$('#Field_ID_765').val(username);\r\n" +
                      "}\r\n" +
                      "$('#ControlContainer_765').addClass('hide');\r\n" +
                      "$('#Field_ID_763').empty();\r\n" +
                      "$('<option></option>').attr('value', '-').text('-').appendTo('#Field_ID_763');\r\n";

                foreach (var user in assignedUserRow)
                {
                    ret += "$('<option></option>').attr('value', '" + user.Full_Name + "').text('" + user.Full_Name + " - " + user.Username + "').appendTo('#Field_ID_763');\r\n";
                }
            }
            if (ds.tbltasks[0].Current_Step_ID == 128)
            {
                ret = "init.push(function () {\r\n" +
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
                        "$('#Field_ID_81').change(function() {\r\n" +
                                "DisplayTTField();\r\n" +
                        "});\r\n" +
                        "function CheckPaymentType() {\r\n" +
                             "$('#ControlContainer_72').removeClass('hide');\r\n" +
                             "$('#ControlContainer_73').addClass('hide');\r\n" +
                             "$('#ControlContainer_78').removeClass('hide');\r\n" +
                             "$('#ControlContainer_79').addClass('hide');\r\n" +
                             "$('#ControlContainer_80').addClass('hide');\r\n" +
                             "$('#ControlContainer_81').removeClass('hide');\r\n" +

                             "if ($('#Field_ID_72').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_73').removeClass('hide');\r\n" +
                             "}\r\n" +

                             "if ($('#Field_ID_78').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_79').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "if ($('#Field_ID_81').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_80').removeClass('hide');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function DisplayCashFields() {\r\n" +
                             "if ($('#Field_ID_72').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_73').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_73').addClass('hide');\r\n" +
                                 "$('#Field_ID_73').prop('checked', false);\r\n" +
                                 "$('#Field_ID_73').parent().removeClass('checked');\r\n" +
                             "}\r\n" +
                             "if (!$('#Field_ID_72').is(\":checked\") || !$('#Field_ID_73').is(\":checked\")){\r\n" +
                                 "$('#contBody_contBody_contBody_cmdSubmit').addClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#contBody_contBody_contBody_cmdSubmit').removeClass('hide');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function DisplayCreditFields() {\r\n" +
                             "if ($('#Field_ID_78').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_79').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_79').addClass('hide');\r\n" +
                                 "$('#Field_ID_79').val('');\r\n" +
                             "}\r\n" +
                         "}\r\n" +
                         "function DisplayTTField() {\r\n" +
                           "if ($('#Field_ID_81').is(\":checked\")){\r\n" +
                               "$('#ControlContainer_80').removeClass('hide');\r\n" +
                           "}\r\n" +
                           "else {\r\n" +
                               "$('#ControlContainer_80').addClass('hide');\r\n" +
                           "}\r\n" +
                         "}\r\n" +
                         "if ($('#Field_ID_78').length) {\r\n" +
                             "$('#contBody_contBody_contBody_cmdSubmit').removeClass('hide');\r\n" +
                         "}\r\n" +
                         "else if (!$('#Field_ID_72').is(\":checked\") || !$('#Field_ID_73').is(\":checked\")) {\r\n" +
                             "$('#contBody_contBody_contBody_cmdSubmit').addClass('hide');\r\n" +
                         "}\r\n" +
                         "else {\r\n" +
                             "$('#contBody_contBody_contBody_cmdSubmit').removeClass('hide');\r\n" +
                         "}\r\n";

            }
            if (ds.tbltasks[0].Current_Step_ID == 99)
            {
                List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = ds.tbltask_history.Where(x => x.Workflow_Step_ID == 87 && x.Task_ID == ds.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

                int[] countrySPFieldIDs = { 421, 422, 423, 424, 425, 426, 427, 428, 429, 430, 431, 432, 433, 434, 435, 436, 437, 438, 439, 440 };

                List<DS_Tasks.tbltask_update_fieldsRow> taskCountrySelected = ds.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && countrySPFieldIDs.Contains(x.Workflow_Step_Field_ID) && x.Field_Value != "")
                                .OrderBy(y => y.Task_Update_Field_ID)
                                .ToList();

                string fieldNames = string.Join(", ", taskCountrySelected.Select(x => x.Field_Value));

                ret = "$('#Field_ID_106').val('" + fieldNames + "');\r\n";
            }
            if (ds.tbltasks[0].Current_Step_ID == 125)
            {
                List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = ds.tbltask_history.Where(x => x.Workflow_Step_ID == 87 && x.Task_ID == ds.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

                int[] countrySPFieldIDs = { 421, 422, 423, 424, 425, 426, 427, 428, 429, 430, 431, 432, 433, 434, 435, 436, 437, 438, 439, 440 };

                List<DS_Tasks.tbltask_update_fieldsRow> taskCountrySelected = ds.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && countrySPFieldIDs.Contains(x.Workflow_Step_Field_ID) && x.Field_Value != "")
                                .OrderBy(y => y.Task_Update_Field_ID)
                                .ToList();

                string fieldNames = string.Join(", ", taskCountrySelected.Select(x => x.Field_Value));

                ret = "$('#Field_ID_685').val('" + fieldNames + "');\r\n" +
                      "$('#Field_ID_74').change(function() {\r\n" +
                             "if ($('#Field_ID_74').is(\":checked\")){\r\n" +
                                 "$('#ControlContainer_75').removeClass('hide');\r\n" +
                                 "$('#ControlContainer_416').removeClass('hide');\r\n" +
                             "}\r\n" +
                             "else {\r\n" +
                                 "$('#ControlContainer_75').addClass('hide');\r\n" +
                                 "$('#ControlContainer_416').addClass('hide');\r\n" +
                             "}\r\n" +
                      "});\r\n";
            }
            if (ds.tbltasks[0].Current_Step_ID == 120)
            {
                List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = ds.tbltask_history.Where(x => x.Workflow_Step_ID == 115 && x.Task_ID == ds.tbltasks[0].Task_ID)
                            .OrderByDescending(o => o.Task_Update_ID)
                            .ToList();

                List<DS_Tasks.tbltask_update_fieldsRow> sharedFolderLink = ds.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && x.Workflow_Step_Field_ID == 476)
                                .OrderBy(y => y.Task_Update_Field_ID)
                .ToList();

                string sharedFolderLinkVal = "";
                if (sharedFolderLink.Count > 0)
                {
                    sharedFolderLinkVal = sharedFolderLink[0].Field_Value;
                }

                string textToFind = "Click here for the link to the shared folder";

                ret = "$('.col-md-12.h4:contains(\"" + textToFind + "\")')\r\n" +
                        ".html('<a href=\"" + sharedFolderLinkVal + "\" target=\"_blank\">" + textToFind + "</a>');\r\n";

            }
            if (ds.tbltasks[0].Current_Step_ID == 121)
            {
                ret = "init.push(function() {\r\n" +
                                "ConfirmQuotation();\r\n" +
                       "});\r\n" +
                      "$('#Field_ID_375').change(function() {\r\n" +
                                "ConfirmQuotation();\r\n" +
                      "});\r\n" +
                      "function ConfirmQuotation() {\r\n" +
                          "if (!$('#Field_ID_375').is(':checked')) {\r\n" +
                                "$('#contBody_contBody_contBody_cmdSubmit').click(function(){\r\n" +
                                        "var answer = confirm('Are you sure you want to close the booking?');\r\n" +
                                        "if (!answer) { return false; }\r\n" +
                                "});\r\n" +
                          "} else {\r\n" +
                                "$('#contBody_contBody_contBody_cmdSubmit').off('click');\r\n" +
                          "}\r\n" +
                      "}";
            }
            if (ds.tbltasks[0].Current_Step_ID == 122)
            {
                ret = "init.push(function() {\r\n" +
                                "ConfirmQuotation();\r\n" +
                       "});\r\n" +
                      "$('#Field_ID_376').change(function() {\r\n" +
                                "ConfirmQuotation();\r\n" +
                      "});\r\n" +
                      "function ConfirmQuotation() {\r\n" +
                          "if (!$('#Field_ID_376').is(':checked')) {\r\n" +
                                "$('#contBody_contBody_contBody_cmdSubmit').click(function(){\r\n" +
                                        "var answer = confirm('Are you sure you want to close the booking?');\r\n" +
                                        "if (!answer) { return false; }\r\n" +
                                "});\r\n" +
                          "} else {\r\n" +
                                "$('#contBody_contBody_contBody_cmdSubmit').off('click');\r\n" +
                          "}\r\n" +
                      "}";
            }
            if (ds.tbltasks[0].Current_Step_ID == 100)
            {
                ret = "init.push(function() {\r\n" +
                                "$('#contBody_contBody_contBody_cmdSubmit').click(function(){\r\n" +
                                        "var answer = confirm('Are you sure you want to send the email?');\r\n" +
                                        "if (!answer) { return false; }\r\n" +
                                "});\r\n" +
                       "});\r\n";
            }
            if (ds.tbltasks[0].Current_Step_ID == 101)
            {
                ret = "init.push(function() {\r\n" +
                                "CloseBooking();\r\n" +
                       "});\r\n" +
                      "$('#Field_ID_116').change(function() {\r\n" +
                                "CloseBooking();\r\n" +
                      "});\r\n" +
                      "$('#Field_ID_125').change(function() {\r\n" +
                                "CloseBooking();\r\n" +
                      "});\r\n" +

                      "function CloseBooking() {\r\n" +
                          "if ((!$('#Field_ID_116').is(':checked')) && (!$('#Field_ID_125').is(':checked'))) {\r\n" +
                                "$('#contBody_contBody_contBody_cmdSubmit').click(function(){\r\n" +
                                        "var answer = confirm('Are you sure you want to close the booking?');\r\n" +
                                        "if (!answer) { return false; }\r\n" +
                                "});\r\n" +
                          "} else {\r\n" +
                                "$('#contBody_contBody_contBody_cmdSubmit').off('click');\r\n" +
                          "}\r\n" +
                      "}";
            }
            if (ds.tbltasks[0].Current_Step_ID == 103)
            {
                Users objUser = new Users(_strConnectionString);
                DS_Users dsUsr = objUser.ReadForGroup(8); // Reading Outbound Team User Group

                List<DS_Users.tblusersRow> assignedUserRow = dsUsr.tblusers
                                    .ToList();

                ret = "$('#Field_ID_760').change(function() {\r\n" +
                              "AppendEmail();\r\n" +
                      "});\r\n" +
                      "function AppendEmail() {\r\n" +
                              "var username = $('#Field_ID_760 option:selected').text().split(' - ')[1];\r\n" +
                              "$('#Field_ID_762').val(username);\r\n" +
                      "}\r\n" +
                      "$('#ControlContainer_762').addClass('hide');\r\n" +
                      "$('#Field_ID_760').empty();\r\n" +
                      "$('<option></option>').attr('value', '-').text('-').appendTo('#Field_ID_760');\r\n";

                foreach (var user in assignedUserRow)
                {
                    ret += "$('<option></option>').attr('value', '" + user.Full_Name + "').text('" + user.Full_Name + " - " + user.Username + "').appendTo('#Field_ID_760');\r\n";
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
            Users objUser = new Users(_strConnectionString);
            DS_Users dsUsr = objUser.ReadForGroup(4); // Reading Outbound Team User Group

            Tasks objTask = new Tasks(_strConnectionString, PhysicalRootFolder, WebRootFolder, ReadContent);
            //DS_Users dS_Users = new DS_Users();

            if (!AtBeginingOfStep)
            {
                if (dsTS.tbltasks[0].Current_Step_ID == 83)
                {

                    List<DS_Tasks.tbltask_historyRow> taskHistoryMatch = dsTS.tbltask_history.Where(x => x.Workflow_Step_ID == 83 && x.Task_ID == dsTS.tbltasks[0].Task_ID)
                           .OrderByDescending(o => o.Task_Update_ID)
                           .ToList();

                    List<DS_Tasks.tbltask_update_fieldsRow> taskAssigneeField = dsTS.tbltask_update_fields.Where(x => x.Task_Update_ID == taskHistoryMatch[0].Task_Update_ID && x.Workflow_Step_Field_ID == 745)
                                    .OrderBy(y => y.Task_Update_Field_ID)
                                    .ToList();

                    string assigneeFullName = "";
                    if (taskAssigneeField.Count() > 0)
                    {
                        assigneeFullName = taskAssigneeField[0].Field_Value;
                    }

                    List<DS_Users.tblusersRow> assignedUserRow = dsUsr.tblusers.Where(x => x.Full_Name == assigneeFullName)
                                    .ToList();

                    DS_Tasks.tbltasksRow activeTask = dsTS.tbltasks.FirstOrDefault(x => x.Task_ID == dsTS.tbltasks[0].Task_ID);

                    bool isTaskedAssigned = false;

                    if (activeTask != null && assignedUserRow.Count>0)
                    {
                        isTaskedAssigned = objTask.Update_Task_Lock(dsTS.tbltasks[0].Task_ID, taskHistoryMatch[0].Task_Update_ID, assignedUserRow[0].User_ID);
                    }

                    if (isTaskedAssigned) dsTS.AcceptChanges();
                }
            }
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
                List<Task_Controls> emailUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 35).ToList();
                List<Task_Controls> phoneUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 34).ToList();
                foreach (Task_Controls ctrl in emailUI)
                {
                    TextBox email = (TextBox)ctrl.UI_Control;
                    if (!Utilities.IsValidEmail(email.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Your email is not a valid email address";
                    }
                }
                foreach (Task_Controls ctrl in phoneUI)
                {
                    TextBox phone = (TextBox)ctrl.UI_Control;
                    if (!Utilities.IsValidPhoneNumber(phone.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Your phone number is not valid";
                    }
                }
            }
            if (Current_Step_ID == 86)
            {
                //Validate pax count
                List<Task_Controls> paxCountUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 126).ToList();
                List<Task_Controls> countryCountUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 420).ToList();
                foreach (Task_Controls ctrl in paxCountUI)
                {
                    TextBox paxCount = (TextBox)ctrl.UI_Control;
                    if (!Utilities.IsValidInteger(paxCount.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Pax count is not a valid number";
                    }
                }
                if (countryCountUI.Count > 0)
                {
                    foreach (Task_Controls ctrl in countryCountUI)
                    {
                        TextBox countryCount = (TextBox)ctrl.UI_Control;
                        if(countryCount.Text != "")
                        {
                            if (!Utilities.IsValidInteger(countryCount.Text))
                            {
                                ret.Validated = false;
                                ret.Reason = "No of countries should be a valid number";
                            }
                        }                      
                    }
                }
                
            }
            if (Current_Step_ID == 87)
            {
                int[] contactNoArray = { 49, 133, 146, 159, 172, 185, 198, 211, 224, 237, 250, 263, 276, 289, 301 };
                int[] emailArray = { 51, 135, 148, 161, 174, 187, 200, 213, 226, 239, 252, 265, 278, 291, 303 };
                int[] passportIssueDateArray = { 47, 131, 144, 157, 170, 183, 196, 209, 222, 235, 248, 261, 274, 287, 299 };
                int[] passportExpiryDateArray = { 48, 132, 145, 158, 171, 184, 197, 210, 223, 236, 249, 262, 275, 288, 300 };

                List<Task_Controls> emailUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && emailArray.Contains(x.Field_ID)).ToList();
                List<Task_Controls> phoneUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && contactNoArray.Contains(x.Field_ID)).ToList();
                List<Task_Controls> passportIssueDateUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && passportIssueDateArray.Contains(x.Field_ID)).ToList();
                List<Task_Controls> passportExpiryDateUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && passportExpiryDateArray.Contains(x.Field_ID)).ToList();
                List<Task_Controls> departureDateUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 58).ToList();
                List<Task_Controls> arrivalDateUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 59).ToList();
                foreach (Task_Controls ctrl in emailUI)
                {
                    TextBox email = (TextBox)ctrl.UI_Control;
                    if (!Utilities.IsValidEmail(email.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Your email is not a valid email address";
                    }
                }
                foreach (Task_Controls ctrl in phoneUI)
                {
                    TextBox phone = (TextBox)ctrl.UI_Control;
                    if (!Utilities.IsValidPhoneNumber(phone.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Your phone number is not valid";
                    }
                }
                if (passportExpiryDateUI.Count > 0)
                {
                    foreach (Task_Controls date1 in passportIssueDateUI)
                    {
                        foreach (Task_Controls date2 in passportExpiryDateUI)
                        {
                            TextBox firstDate = (TextBox)date1.UI_Control;
                            TextBox secondDate = (TextBox)date2.UI_Control;
                            if (!Utilities.IsValidDateRange(firstDate.Text, secondDate.Text))
                            {
                                ret.Validated = false;
                                ret.Reason = "The passport's expiration date must be later than its issue date.";
                            }
                            if (!Utilities.IsDateValid(secondDate.Text))
                            {
                                ret.Validated = false;
                                ret.Reason = "Passport expiry date must be a date in the future";
                            }
                        }
                    }
                }
                if (departureDateUI.Count > 0)
                {
                    foreach (Task_Controls date1 in departureDateUI)
                    {
                        foreach (Task_Controls date2 in passportExpiryDateUI)
                        {
                            TextBox travelDate = (TextBox)date1.UI_Control;
                            TextBox expiryDate = (TextBox)date2.UI_Control;
                            if (!Utilities.IsPassportEligible(expiryDate.Text, travelDate.Text))
                            {
                                ret.Validated = false;
                                ret.Reason = "Your passport expiry date is in less than 7 months.";
                            }

                        }
                    }
                    foreach (Task_Controls date1 in departureDateUI)
                    {
                        foreach (Task_Controls date2 in arrivalDateUI)
                        {
                            TextBox departureDate = (TextBox)date1.UI_Control;
                            TextBox arrivalDate = (TextBox)date2.UI_Control;
                            if (!Utilities.IsValidDateRange(departureDate.Text, arrivalDate.Text))
                            {
                                ret.Validated = false;
                                ret.Reason = "Date of arrival should not come before date of departure";
                            }
                        }
                    }
                }
            }
            if (Current_Step_ID == 105) // Inbound workflow, Acknowledgement Process (Non-series group)
            {
                // Validate agent contact number, agent email
                List<Task_Controls> phoneUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 312).ToList();
                List<Task_Controls> emailUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 313).ToList();
                foreach (Task_Controls ctrl in phoneUI)
                {
                    TextBox phone = (TextBox)ctrl.UI_Control;
                    if (!Utilities.IsValidPhoneNumber(phone.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Your phone number is not valid";
                    }
                }
                foreach (Task_Controls ctrl in emailUI)
                {
                    TextBox email = (TextBox)ctrl.UI_Control;
                    if (!Utilities.IsValidEmail(email.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Your email is not a valid email address";
                    }
                }

            }

            if (Current_Step_ID == 113) // Inquiry (Series group)
            {

                List<Task_Controls> date1UI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 350).ToList();
                List<Task_Controls> date2UI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 351).ToList();
                List<Task_Controls> roomsUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 352).ToList();
                List<Task_Controls> costUI = objControlsList.Controls.Where(x => x.UI_Type == UI_Types.TextBoxes && x.Field_ID == 361).ToList();

                foreach (Task_Controls date1 in date1UI)
                {
                    foreach (Task_Controls date2 in date2UI)
                    {
                        TextBox firstDate = (TextBox)date1.UI_Control;
                        TextBox secondDate = (TextBox)date2.UI_Control;
                        if (!Utilities.IsValidDateRange(firstDate.Text, secondDate.Text))
                        {
                            ret.Validated = false;
                            ret.Reason = "Invalid date";
                        }
                    }
                }
                foreach (Task_Controls noRooms in roomsUI)
                {
                    TextBox noOfRooms = (TextBox)noRooms.UI_Control;
                    if (!Utilities.IsValidInteger(noOfRooms.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Invalid number of rooms";
                    }
                }
                foreach (Task_Controls costAmount in costUI)
                {
                    TextBox costingAmount = (TextBox)costAmount.UI_Control;
                    if (!Utilities.IsValidNumber(costingAmount.Text))
                    {
                        ret.Validated = false;
                        ret.Reason = "Enter positive numbers only";
                    }
                }
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