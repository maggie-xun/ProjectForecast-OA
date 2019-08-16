using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace ProjectForecast_OA.utils
{
    public class ExcelHelper
    {
        public static DataTable GetTable(string dataSource, string book)
        {
            String sWorkbook = "[" + book + "]";
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
        //public static Data Getfiles(byte[] a)
        //{
        //    DataSet ds = new DataSet();
        //    ds = ByteToDataset(a);
        //    return ds.Tables.;
        //}

        public static DataSet ByteToDataset(byte[] bArrayResult)
        {
            DataSet dsResult = new DataSet();
            MemoryStream ms = new MemoryStream(bArrayResult);
            IFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(ms);
            dsResult = (DataSet)obj;
            ms.Close();
            ms.Dispose();

            return dsResult;

        }

        public static DataSet ReadExcel(string path)
        {
            using(var stream=File.Open(path,FileMode.Open,FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());

                    // 2. Use the AsDataSet extension method
                    var result = reader.AsDataSet();
                    return result;
                    // The result of each spreadsheet is in result.Tables
                }
            }
           
        }
    }
    }