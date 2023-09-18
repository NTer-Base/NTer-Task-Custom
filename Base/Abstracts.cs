using N_Ter_Task_Custom.DataStructures.DataSets;
using N_Ter_Task_Data_Structures.DataSets;
using System;

namespace N_Ter.Base
{
    public abstract class Custom_Dashboard
    {
        public abstract DS_Workflow ReadWFStats(int Walkflow_ID, string PhysicalRoot, string WebRoot, bool ReadContent);
        public abstract DS_Workflow ReadWFStatsForUser(int Walkflow_ID, int User_ID, string PhysicalRoot, string WebRoot, bool ReadContent);
    }

    public abstract class Reports
    {
        public abstract DS_RPT_Productivity ReadProductivityReport(DateTime FromDate, DateTime ToDate);
    }

    public abstract class Sample_Master_Table
    {
        public abstract DS_Master_Tables ReadAsMaster();
    }

    public abstract class Sample_Master_Table_FK
    {
        public abstract DS_Master_Tables ReadAsMaster();
        public abstract DS_Master_Tables ReadAsMaster(int Foreign_Key_ID);
        public abstract DS_Master_Tables ReadAsMaster(int Foreign_Key_ID, int Foreign_Key_2_ID);
    }
}
