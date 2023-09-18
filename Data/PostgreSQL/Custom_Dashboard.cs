using N_Ter_Task_Data_Structures.DataSets;
using System.Linq;

namespace N_Ter.PostgreSQL.Customizable
{
    public class Custom_Dashboard : Base.Custom_Dashboard
    {
        private string _strConnectionString;
        Common_Database_Actions objComDB;

        public Custom_Dashboard(string strConnectionString)
        {
            _strConnectionString = strConnectionString;
            objComDB = new Common_Database_Actions(_strConnectionString);
        }

        public override DS_Workflow ReadWFStats(int Walkflow_ID, string PhysicalRoot, string WebRoot, bool ReadContent)
        {
            return GetStepCounts(Walkflow_ID, new Tasks(_strConnectionString, PhysicalRoot, WebRoot, ReadContent).ReadMyTasks(0, false, 1, new Customizable(_strConnectionString)));
        }

        public override DS_Workflow ReadWFStatsForUser(int Walkflow_ID, int User_ID, string PhysicalRoot, string WebRoot, bool ReadContent)
        {
            return GetStepCounts(Walkflow_ID, new Tasks(_strConnectionString, PhysicalRoot, WebRoot, ReadContent).ReadMyTasks(User_ID, false, 0, new Customizable(_strConnectionString)));
        }

        private DS_Workflow GetStepCounts(int Walkflow_ID, DS_Tasks dsTS)
        {
            DS_Workflow dsWF = new Workflow(_strConnectionString).ReadAllStep(Walkflow_ID);
            System.Collections.Generic.List<int> Indexes = new System.Collections.Generic.List<int>();
            int NumberOfTasks;
            for (int i = 0; i < dsWF.tblworkflow_steps.Rows.Count; i++)
            {
                NumberOfTasks = dsTS.tbltasks.Where(x => x.Current_Step_ID == dsWF.tblworkflow_steps[i].Workflow_Step_ID).Count();
                if (NumberOfTasks == 0)
                {
                    Indexes.Add(i);
                }
                else
                {
                    dsWF.tblworkflow_steps[i].Number_Of_Tasks = NumberOfTasks;
                }
            }

            for (int i = Indexes.Count - 1; i >= 0; i--)
            {
                dsWF.tblworkflow_steps[Indexes[i]].Delete();
            }
            dsWF.AcceptChanges();
            return dsWF;
        }
    }
}
