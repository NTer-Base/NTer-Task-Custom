using System;
using System.Collections.Generic;

namespace N_Ter.Structures
{
    public class Workflow_Stats_WS
    {
        public string Total_Tasks { get; set; }
        public List<Workflow_Stats_Sub_WS> Milestone_Data { get; set; }
        public List<Workflow_Stats_Sub_WS> Step_Data { get; set; }
    }

    public class Workflow_Stats_Sub_WS
    {
        public string Step_Status { get; set; }
        public string Number_Of_Tasks { get; set; }
        public string Tasks_Percentage { get; set; }
        public string Band_Col { get; set; }
        public string WF_Step_URL { get; set; }
    }
}
