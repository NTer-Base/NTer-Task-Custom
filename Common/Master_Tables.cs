using N_Ter.Base;
using N_Ter_Task_Data_Structures.DataSets;

namespace N_Ter.Customizable
{
    public class Master_Tables : Base.Master_Base
    {
        private string _strConnectionString;
        private int _DBType;

        public Master_Tables(string strConnectionString, int DBType)
        {
            _strConnectionString = strConnectionString;
            _DBType = DBType;
        }

        public override DS_Master_Tables GetMasterTable(int Table_ID, int Foreign_Key_Data_ID = 0, int Foreign_Key_2_Data_ID = 0)
        {
            DS_Master_Tables ds = null;
            switch (Table_ID)
            {
                case 1:
                    ds = ObjectCreator.GetEntity_Level_2(_strConnectionString, _DBType).ReadAsMaster();
                    break;
                case 2:
                    ds = ObjectCreator.GetUsers(_strConnectionString, _DBType).ReadAsMaster();
                    break;
                case 3:
                    ds = ObjectCreatorCustom.GetSample_Master_Table(_strConnectionString, _DBType).ReadAsMaster();
                    break;
                case 4:
                    if (Foreign_Key_Data_ID > 0 && Foreign_Key_2_Data_ID > 0)
                    {
                        ds = ObjectCreatorCustom.GetSample_Master_Table_FK(_strConnectionString, _DBType).ReadAsMaster(Foreign_Key_Data_ID, Foreign_Key_2_Data_ID);
                    }
                    else if (Foreign_Key_Data_ID > 0)
                    {
                        ds = ObjectCreatorCustom.GetSample_Master_Table_FK(_strConnectionString, _DBType).ReadAsMaster(Foreign_Key_Data_ID);
                    }
                    else if (Foreign_Key_2_Data_ID > 0)
                    {
                        ds = ObjectCreatorCustom.GetSample_Master_Table_FK(_strConnectionString, _DBType).ReadAsMaster(Foreign_Key_2_Data_ID);
                    }
                    else
                    {
                        ds = ObjectCreatorCustom.GetSample_Master_Table_FK(_strConnectionString, _DBType).ReadAsMaster();
                    }
                    break;
            }
            return ds;
        }
    }
}
