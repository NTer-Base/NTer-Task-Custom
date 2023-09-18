using MySql.Data.MySqlClient;
using N_Ter.Common;
using N_Ter_Task_Data_Structures.DataSets;

namespace N_Ter.MySQL.Customizable
{
    public class Sample_Master_Table : Base.Sample_Master_Table
    {
        private string _strConnectionString;
        Common_Database_Actions objComDB;

        public Sample_Master_Table(string strConnectionString)
        {
            _strConnectionString = strConnectionString;
            objComDB = new Common_Database_Actions(_strConnectionString);
        }

        public override DS_Master_Tables ReadAsMaster()
        {
            MySqlConnection con = new MySqlConnection(_strConnectionString);
            MySqlDataAdapter adp = new MySqlDataAdapter("SELECT Master_ID AS Data_ID, Master_Data AS Data FROM tblzm_sample", con);
            DS_Master_Tables ds = new DS_Master_Tables();
            adp.Fill(ds.tblData);
            objComDB.CloseAdapter(ref con, ref adp);
            return ds;
        }
    }
}
