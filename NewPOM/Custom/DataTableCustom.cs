using NewPOM.Bases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NewPOM.Custom
{
    public static class DataTableCustom
    {
        public static string ReadData(this List<Datacollection> dataCol, int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();
                return data.ToString();
            }
            catch (Exception e)
            {
                //LogUtil.Write("ERROR :: " + e.Message);
                return e.Message;
            }
        }

        public static List<Datacollection> ConvertDataToList(this DataTable table)
        {
            List<Datacollection> dataCol =  new List<Datacollection>();
            //Iterate through the rows and columns of the Table
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Datacollection dtTable = new Datacollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    //Add all the details for each row
                    dataCol.Add(dtTable);
                }
            }

            return dataCol;

        }
    }
}
