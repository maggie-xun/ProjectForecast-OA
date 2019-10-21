using ProjectForecast_OA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProjectForecast_OA.utils
{
    public static class TransferToModel
    {
        public static List<T> CreateListFromTable<T>(DataTable tbl) where T : new()
        {
            // define return list
            List<T> lst = new List<T>();

            // go through each row
            foreach (DataRow r in tbl.Rows)
            {
                // add to the list
                lst.Add(CreateItemFromRow<T>(r));
            }

            // return the list
            return lst;
        }
        public static T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            // create a new object
            T item = new T();
            //set Columns
            
            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                //if(c.ColumnName.Split(' ').Length>1)
                //{
                //    c.ColumnName = c.ColumnName.Trim();
                //}
                //System.Reflection.PropertyInfo[] propertys = item.GetType().GetProperties();

                PropertyInfo p = item.GetType().GetProperty(c.ColumnName.Replace(" ", ""));

                // if exists, set the value

                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
            //for(int i = 0; i < row.ItemArray.Length; i++)
            //{
            //    DataColumn c = new DataColumn(row.ItemArray[i].ToString());
            //    PropertyInfo p = item.GetType().GetProperty(c.ColumnName.Replace(" ", ""));

            //    // if exists, set the value

            //    if (p != null && row[c] != DBNull.Value)
            //    {
            //        p.SetValue(item, row[c], null);
            //    }
            //}
        }

        //public static List<T> ToList<T>(this DataTable table) where T : new()
        //{
        //    List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        //    List<T> result = new List<T>();

        //    foreach (var row in table.Rows)
        //    {
        //        var item = CreateItemFromRow<T>((DataRow)row, properties);
        //        result.Add(item);
        //    }

        //    return result;
        //}

        //private static T CreateItemFromRow<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
        //{
        //    T item = new T();
        //    foreach (var property in properties)
        //    {
        //        property.SetValue(item, row[property.Name], null);
        //    }
        //    return item;
        //}

        public static List<Project_Financial_Report> CreateFinanceItem(DataTable dt, string projectNo)
        {
            List<Project_Financial_Report> lst = new List<Project_Financial_Report>();
            for (int i = 1; i <= 12; i++)
            {
                Project_Financial_Report pfp = new Project_Financial_Report();
                pfp.ProjectNo = projectNo;
                pfp.Month = MonthConverter(i);
                pfp.Revenue = dt.Rows[0][i].ToString() == "" ? 0 : float.Parse(dt.Rows[0][i].ToString());
                pfp.HeadCountCost = dt.Rows[1][i].ToString() == "" ? 0 : float.Parse(dt.Rows[1][i].ToString());
                pfp.ChargesIn = dt.Rows[2][i].ToString() == "" ? 0 : float.Parse(dt.Rows[2][i].ToString());
                pfp.Contractors = dt.Rows[3][i].ToString() == "" ? 0 : float.Parse(dt.Rows[3][i].ToString());
                pfp.Expenses = dt.Rows[4][i].ToString() == "" ? 0 : float.Parse(dt.Rows[4][i].ToString());
                pfp.IT = dt.Rows[5][i].ToString() == "" ? 0 : float.Parse(dt.Rows[5][i].ToString());
                pfp.Materials = dt.Rows[6][i].ToString() == "" ? 0 : float.Parse(dt.Rows[6][i].ToString());
                lst.Add(pfp);
            }
            return lst;
        }


        private static string MonthConverter(int month)
        {
            switch (month)
            {
                case 1:return "Jan";
                case 2:return "Feb";
                case 3:return "Mar";
                case 4:return "Apr";
                case 5:return "May";
                case 6:return "Jun";
                case 7:return "Jul";
                case 8:return "Aug";
                case 9:return "Sep";
                case 10:return "Oct";
                case 11:return "Nov";
                case 12:return "Dec";
                default:return "";
            }
        }

        internal static List<Consultant_Workday_Details> CreateEmployeeWorkingDetailsItem(DataTable dtEmployees, string projectNo)
        {
            List<Consultant_Workday_Details> lst = new List<Consultant_Workday_Details>();
            for (int i = 1; i <= 12; i++)
            {
                foreach (DataRow row in dtEmployees.Rows)
                {
                    Consultant_Workday_Details cwd = new Consultant_Workday_Details();
                    cwd.ProjectNo = projectNo;
                    cwd.Month = MonthConverter(i);
                    cwd.Consultant_Name = row[0].ToString();
                    cwd.Type = row[1].ToString();
                    cwd.CostRate =int.Parse(row[2].ToString());
                    cwd.WorkDays = row[i+2].ToString() == "" ? 0 : int.Parse(row[i+2].ToString());
                    lst.Add(cwd);
                }              
            }
            return lst;
        }

        internal static List<Consultant> CreateEmployeeItem(DataTable dtEmployees, string projectNo)
        {
            List<Consultant> lst = new List<Consultant>();
            //for (int i = 0; i <3; i++)
            //{
                foreach (DataRow row in dtEmployees.Rows)
                {
                    Consultant consultant = new Consultant();
                    consultant.Type = row[1].ToString();
                    consultant.Consultant_Name = row[0].ToString();
                    consultant.CostRate =int.Parse(row[2].ToString());
                    lst.Add(consultant);
                }
            //}
            return lst;
        }
    }
}