using N_Ter.API;
using N_Ter.Base;
using N_Ter.Structures;
using N_Ter_Task_Data_Structures.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace N_Ter.Customizable
{
    public class CustomizableController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetDashboardStats(string Workflow_ID)
        {
            SessionObject objSes = (SessionObject)System.Web.HttpContext.Current.Session["dt"];

            string SessionKey = objSes.SesKey;
            int UserID = objSes.UserID;

            if (new Task_Support().ValidateSession(objSes.Connection, objSes.DB_Type, SessionKey, UserID) == true)
            {
                List<Workflow_Stats_WS> StatsItem = new List<Workflow_Stats_WS>();
                List<Workflow_Stats_Sub_WS> StatsItemMilestone = new List<Workflow_Stats_Sub_WS>();
                List<Workflow_Stats_Sub_WS> StatsItemSteps = new List<Workflow_Stats_Sub_WS>();

                Custom_Dashboard objDash = ObjectCreatorCustom.GetCustomDashboard(objSes.Connection, objSes.DB_Type);
                DS_Workflow dsStats;

                if (objSes.isAdmin == 1 || objSes.isAdmin == 2)
                {
                    dsStats = objDash.ReadWFStats(Convert.ToInt32(Workflow_ID), objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                }
                else
                {
                    dsStats = objDash.ReadWFStatsForUser(Convert.ToInt32(Workflow_ID), objSes.UserID, objSes.PhysicalRoot, objSes.WebRoot, objSes.EnableReading);
                }

                DS_Workflow dsMS = ObjectCreator.GetWorkflow_Milestones(objSes.Connection, objSes.DB_Type).ReadAll(Convert.ToInt32(Workflow_ID));

                int Total_Tasks = dsStats.tblworkflow_steps.Sum(x => x.Number_Of_Tasks);

                if (dsStats.tblworkflow_steps.Rows.Count > 0)
                {
                    decimal TasksPercentage = 0;

                    N_Ter.Security.URL_Manager objURL = new N_Ter.Security.URL_Manager();

                    Workflow objWF = ObjectCreator.GetWorkflow(objSes.Connection, objSes.DB_Type);
                    DS_Workflow dsWF = objWF.ReadAll();

                    foreach (DS_Workflow.tblworkflow_stepsRow row in dsStats.tblworkflow_steps)
                    {
                        if (Total_Tasks > 0)
                        {
                            TasksPercentage = Math.Round((decimal)row.Number_Of_Tasks / (decimal)Total_Tasks * (decimal)100);
                        }
                        else
                        {
                            TasksPercentage = 0;
                        }

                        StatsItemSteps.Add(new Workflow_Stats_Sub_WS
                        {
                            Step_Status = row.Step_Status,
                            Number_Of_Tasks = row.Number_Of_Tasks.ToString(),
                            Tasks_Percentage = TasksPercentage.ToString(),
                            WF_Step_URL = "task_list_alt.aspx?wcid=" + objURL.Encrypt(Convert.ToString(dsWF.tblwalkflow.Where(x => x.Walkflow_ID == row.Walkflow_ID).First().Workflow_Category_ID)) + "&wid=" + objURL.Encrypt(Convert.ToString(row.Walkflow_ID)) + "&sid=" + objURL.Encrypt(Convert.ToString(row.Workflow_Step_ID)),
                            Band_Col = "g" //"b", "r"
                        });
                    }

                    int TotTasksAdded = 0;
                    int TaskCount = 0;
                    foreach (DS_Workflow.tblworkflow_milestonesRow row in dsMS.tblworkflow_milestones)
                    {
                        TaskCount = dsStats.tblworkflow_steps.Where(x => x.Workflow_Milestone_ID == row.Workflow_Milestone_ID).Sum(y => y.Number_Of_Tasks);
                        if (TaskCount > 0)
                        {
                            TasksPercentage = Math.Round((decimal)TaskCount / (decimal)Total_Tasks * (decimal)100);
                            StatsItemMilestone.Add(new Workflow_Stats_Sub_WS
                            {
                                Step_Status = row.Milestone_Name,
                                Number_Of_Tasks = TaskCount.ToString(),
                                Tasks_Percentage = TasksPercentage.ToString(),
                                Band_Col = "b" //"g", "r"
                            });
                            TotTasksAdded = TotTasksAdded + TaskCount;
                        }
                    }

                    if (Total_Tasks - TotTasksAdded > 0)
                    {
                        TasksPercentage = Math.Round((decimal)(Total_Tasks - TotTasksAdded) / (decimal)Total_Tasks * (decimal)100);
                        StatsItemMilestone.Add(new Workflow_Stats_Sub_WS
                        {
                            Step_Status = "No Milestone",
                            Number_Of_Tasks = (Total_Tasks - TotTasksAdded).ToString(),
                            Tasks_Percentage = TasksPercentage.ToString(),
                            Band_Col = "b" //"g", "r"
                        });
                    }

                    StatsItem.Add(new Workflow_Stats_WS
                    {
                        Total_Tasks = Total_Tasks.ToString(),
                        Step_Data = StatsItemSteps,
                        Milestone_Data = StatsItemMilestone
                    });
                }

                return Request.CreateResponse(HttpStatusCode.OK, StatsItem);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ResponceMessages(MessagesType.Authentication_Failed));
            }
        }

        private string ResponceMessages(MessagesType Err)
        {
            string ErrorMsg = "";
            switch (Err)
            {
                case MessagesType.Blank_Message:
                    ErrorMsg = "";
                    break;
                case MessagesType.No_Records:
                    ErrorMsg = "";
                    break;
                case MessagesType.Authentication_Failed:
                    ErrorMsg = "";
                    break;
                case MessagesType.Bad_Request:
                    ErrorMsg = "";
                    break;
                default:
                    ErrorMsg = "";
                    break;
            }
            return ErrorMsg;
        }

        private enum MessagesType
        {
            Blank_Message, No_Records, Authentication_Failed, Bad_Request
        }
    }
}
