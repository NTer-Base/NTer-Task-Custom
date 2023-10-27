using N_Ter_Task_Data_Structures.DataSets;

namespace N_Ter.Customizable
{
    public class Custom_Lists
    {
        public DS_Extra_Sections LoadExtraSections()
        {
            DS_Extra_Sections ds = new DS_Extra_Sections();
            DS_Extra_Sections.tblSectionsRow dr = ds.tblSections.NewtblSectionsRow();
            dr.Section_ID = 1;
            dr.Section_Name = "Sample Item 1";
            dr.Show_In_Nav = true;
            dr.Section_Name_Menu = "Sample Item 1";
            dr.Page_Name = "extra_page_1.aspx";
            dr.isTask_Specific = false;
            dr.Section_Icon = "fa-cogs";
            dr.Category_ID = 1;            
            ds.tblSections.Rows.Add(dr);

            dr = ds.tblSections.NewtblSectionsRow();
            dr.Section_ID = 2;
            dr.Section_Name = "Sample Item 2";
            dr.Show_In_Nav = false;
            dr.Section_Name_Menu = "Sample Item 2";
            dr.Page_Name = "extra_page_2.aspx";
            dr.isTask_Specific = true;
            dr.Section_Icon = "fa-cogs";
            dr.Help_Text = "Testing Help";
            dr.Category_ID = 2;
            ds.tblSections.Rows.Add(dr);

            return ds;
        }

        public DS_Extra_Sections LoadExtraCategories()
        {
            DS_Extra_Sections ds = new DS_Extra_Sections();
            DS_Extra_Sections.tblCategoriesRow dr = ds.tblCategories.NewtblCategoriesRow();

            return ds;
        }

        public DS_Master_Tables LoadMasterTableNames(string EL2_Plural_Name)
        {
            DS_Master_Tables ds = new DS_Master_Tables();
            DS_Master_Tables.tblTablesRow dr = ds.tblTables.NewtblTablesRow();
            dr.Table_ID = 1;
            dr.Table_Name = EL2_Plural_Name;
            ds.tblTables.Rows.Add(dr);

            dr = ds.tblTables.NewtblTablesRow();
            dr.Table_ID = 2;
            dr.Table_Name = "Users";
            ds.tblTables.Rows.Add(dr);

            dr = ds.tblTables.NewtblTablesRow();
            dr.Table_ID = 3;
            dr.Table_Name = "Sample Master";
            ds.tblTables.Rows.Add(dr);

            dr = ds.tblTables.NewtblTablesRow();
            dr.Table_ID = 4;
            dr.Table_Name = "Sample Master 2";
            ds.tblTables.Rows.Add(dr);

            return ds;
        }

        public DS_Reports LoadReports()
        {
            DS_Reports ds = new DS_Reports();
            DS_Reports.tblReportsRow dr = ds.tblReports.NewtblReportsRow();
            dr.Report_ID = 1;
            dr.Report_Name = "Productivity Report";
            dr.Report_Name_Menu = "Productivity Report";
            dr.Help_Text = "Testing Help Text";
            ds.tblReports.Rows.Add(dr);

            return ds;
        }
    }
}
