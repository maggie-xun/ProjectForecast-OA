using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.utils
{
    public class SplitDataTable
    {
        public static DataTable SplitDataTableHelper(DataTable originalTab,int[] rowsNum,int[] columnNum)
        {
            DataTable newDT = new DataTable();

            for (int j = columnNum[0]; j < columnNum[1]; j++)
            {
                newDT.Columns.Add(originalTab.Columns[j].ColumnName, originalTab.Columns[j].DataType);
            }

            for (int i = rowsNum[0]; i < rowsNum[1]; i++)
            {
                DataRow dr = newDT.NewRow();
                var colNew = 0;
                for (int j = columnNum[0]; j < columnNum[1]; j++)
                {                 
                    dr[colNew] = originalTab.Rows[i][j];
                    colNew++;
                }
                newDT.Rows.Add(dr);
            }

            return newDT;
        }
    }
}