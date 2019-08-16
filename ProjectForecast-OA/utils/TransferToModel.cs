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
    }
}