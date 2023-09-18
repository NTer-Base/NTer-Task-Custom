using System.Data.SqlClient;
using System.Data;
using System;
using N_Ter_Task_Data_Structures.DataSets;
using N_Ter_Task_Custom.DataStructures.DataSets;

namespace N_Ter.SQL_Server.Customizable
{
    public class Reports : Base.Reports
    {
        private string _strConnectionString;
        Common_Database_Actions objComDB;

        public Reports(string strConnectionString)
        {
            _strConnectionString = strConnectionString;
            objComDB = new Common_Database_Actions(_strConnectionString);
        }

        public override DS_RPT_Productivity ReadProductivityReport(DateTime FromDate, DateTime ToDate)
        {
            SqlConnection con = new SqlConnection(_strConnectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT tbltask_history.Task_Update_ID, CONCAT(tblusers.First_Name, ' ', tblusers.Last_Name) AS Full_Name, tblworkflow_steps.Step_Status, tbltask_history.Step_Started_Date, tbltask_history.Step_Finished_Date, tbltask_history.Task_ID, " +
                                                                    "CONCAT(tbltime.Time_Taken / 60, ':', RIGHT(REPLICATE('0', 2) + LEFT((tbltime.Time_Taken % 60), 2), 2)) AS Active_Time " +
                                                        "FROM     tbltask_history INNER JOIN " +
                                                                        "(SELECT Task_Update_ID, SUM(DATEDIFF(MINUTE, Start_Date_Time, ISNULL(End_Date_Time, GETDATE()))) AS Time_Taken " +
                                                                         "FROM   tbltask_history_durations " +
                                                                         "GROUP BY Task_Update_ID) tbltime ON tbltask_history.Task_Update_ID = tbltime.Task_Update_ID INNER JOIN " +
                                                                    "tblusers ON tbltask_history.Posted_By = tblusers.User_ID INNER JOIN " +
                                                                    "tblworkflow_steps ON tbltask_history.Workflow_Step_ID = tblworkflow_steps.Workflow_Step_ID INNER JOIN " +
                                                                        "(SELECT Task_ID " +
                                                                         "FROM   tbltasks " +
                                                                         "WHERE  (Task_Date >= @FromDate) AND (Task_Date <= @ToDate)) tbldtasks ON tbltask_history.Task_ID = tbldtasks.Task_ID " +
                                                        "ORDER BY tbltask_history.Task_ID, tbltask_history.Task_Update_ID", con);
            adp.SelectCommand.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
            adp.SelectCommand.Parameters["@FromDate"].Value = FromDate.Date;
            adp.SelectCommand.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime));
            adp.SelectCommand.Parameters["@ToDate"].Value = ToDate.Date.AddDays(1).AddSeconds(-1);
            DS_RPT_Productivity ds = new DS_RPT_Productivity();
            adp.Fill(ds.tblUpdates);
            adp.SelectCommand.CommandText = "SELECT tbltasks.Task_ID, tbltasks.Task_Number, tblwalkflow.Workflow_Name, tbltasks.Entity_L2_ID, CONCAT(tblactive_time.Active_Time / 60, ':', RIGHT(REPLICATE('0', 2) + LEFT((tblactive_time.Active_Time % 60), 2), 2)) AS Active_Time " +
                                            "FROM     tbltasks INNER JOIN " +
                                                            "(SELECT tbltask_history.Task_ID, SUM(tbltime.Time_Taken) AS Active_Time " +
                                                             "FROM   tbltask_history INNER JOIN " +
                                                                            "(SELECT Task_Update_ID, SUM(DATEDIFF(MINUTE, Start_Date_Time, ISNULL(End_Date_Time, GETDATE()))) AS Time_Taken " +
                                                                             "FROM   tbltask_history_durations " +
                                                                             "GROUP BY Task_Update_ID) tbltime ON tbltask_history.Task_Update_ID = tbltime.Task_Update_ID " +
                                                             "WHERE  (tbltask_history.Workflow_Step_ID <> - 1) AND (tbltask_history.Workflow_Step_ID <> - 2) " +
                                                             "GROUP BY tbltask_history.Task_ID) tblactive_time ON tbltasks.Task_ID = tblactive_time.Task_ID INNER JOIN " +
                                                        "tblwalkflow ON tbltasks.Walkflow_ID = tblwalkflow.Walkflow_ID " +
                                            "WHERE  (tbltasks.Task_Date >= @FromDate) AND (tbltasks.Task_Date <= @ToDate) " +
                                            "ORDER BY tbltasks.Task_ID";
            adp.Fill(ds.tblTasks);
            adp.SelectCommand.CommandText = "SELECT tbldel2.Entity_L2_ID, tblentity_level_2.Display_Name " +
                                            "FROM   (SELECT DISTINCT Entity_L2_ID " +
                                                    "FROM   tbltasks " +
                                                    "WHERE  (Task_Date >= @FromDate) AND (Task_Date <= @ToDate)) tbldel2 INNER JOIN " +
                                                    "tblentity_level_2 ON tbldel2.Entity_L2_ID = tblentity_level_2.Entity_L2_ID " +
                                            "ORDER BY tblentity_level_2.Display_Name";
            adp.Fill(ds.tblReport);
            objComDB.CloseAdapter(ref con, ref adp);
            return ds;
        }
    }
}