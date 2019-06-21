using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.utils
{
    public class ExcelHelper
    {
        public static DataTable GetTable(string dataSource,string book)
        {
            String sWorkbook = "["+ book + "]";
            String sExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dataSource + ";Extended Properties=Excel 12.0";
            //String sExcelConnectionString = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + filename + ";Extended Properties=Excel 8.0;Mode=ReadWrite|Share Deny None;Persist Security Info=False";
            OleDbConnection OleDbConn = new OleDbConnection(sExcelConnectionString);

            OleDbConn.Open();
            OleDbCommand OleDbCmd = new OleDbCommand(("SELECT * FROM " + sWorkbook), OleDbConn);

            DataSet ds = new DataSet();

            OleDbDataAdapter sda = new OleDbDataAdapter(OleDbCmd);
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            OleDbConn.Close();
            //List<Summery> model = new List<Summery>();

            //if (ds != null)
            //{
            //    //Pass datatable from dataset to our DAL Method.
            //    model = CreateListFromTable<Summery>(dt);
            //}
            return dt;
        }
    }
}