namespace N_Ter.Base
{
    public partial class ObjectCreatorCustom
    {
        public static Custom_Dashboard GetCustomDashboard(string ConnectionString, int DBType)
        {
            if (DBType == 1)
            {
                return new MySQL.Customizable.Custom_Dashboard(ConnectionString);
            }
            else if (DBType == 2)
            {
                return new SQL_Server.Customizable.Custom_Dashboard(ConnectionString);
            }
            else
            {
                return new PostgreSQL.Customizable.Custom_Dashboard(ConnectionString);
            }
        }

        public static Custom_Base GetCustomizable(string ConnectionString, int DBType)
        {
            if (DBType == 1)
            {
                return new MySQL.Customizable.Customizable(ConnectionString);
            }
            else if (DBType == 2)
            {
                return new SQL_Server.Customizable.Customizable(ConnectionString);
            }
            else
            {
                return new PostgreSQL.Customizable.Customizable(ConnectionString);
            }
        }

        public static Reports GetReports(string ConnectionString, int DBType)
        {
            if (DBType == 1)
            {
                return new MySQL.Customizable.Reports(ConnectionString);
            }
            else if (DBType == 2)
            {
                return new SQL_Server.Customizable.Reports(ConnectionString);
            }
            else
            {
                return new PostgreSQL.Customizable.Reports(ConnectionString);
            }
        }

        public static Sample_Master_Table GetSample_Master_Table(string ConnectionString, int DBType)
        {
            if (DBType == 1)
            {
                return new MySQL.Customizable.Sample_Master_Table(ConnectionString);
            }
            else if (DBType == 2)
            {
                return new SQL_Server.Customizable.Sample_Master_Table(ConnectionString);
            }
            else
            {
                return new PostgreSQL.Customizable.Sample_Master_Table(ConnectionString);
            }
        }

        public static Sample_Master_Table_FK GetSample_Master_Table_FK(string ConnectionString, int DBType)
        {
            if (DBType == 1)
            {
                return new MySQL.Customizable.Sample_Master_Table_FK(ConnectionString);
            }
            else if (DBType == 2)
            {
                return new SQL_Server.Customizable.Sample_Master_Table_FK(ConnectionString);
            }
            else
            {
                return new PostgreSQL.Customizable.Sample_Master_Table_FK(ConnectionString);
            }
        }
    }
}
