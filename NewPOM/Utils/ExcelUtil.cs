using ExcelDataReader;
using NewPOM.Bases;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace NewPOM.Utils
{
    public class ExcelUtil
    {
        private static List<Datacollection> _dataCol = new List<Datacollection>();


        /// <summary>
        /// Storing all the excel values in to the in-memory collections
        /// </summary>
        /// <param name="fileName"></param>
        public static void PopulateInCollection(string fileName)
        {
            DataTable table = ExcelToDataTable(fileName);

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
                    _dataCol.Add(dtTable);
                }
            }
        }

        /// <summary>
        /// Reading all the datas from Excelsheet
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static DataTable ExcelToDataTable(string fileName)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            FileStream stream;
            try
            {
                //open file and returns as Stream
                stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch
            {//filestream did not open, common error is because the file is already open in Excel/OOo lets try a temp file
                string tmpFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
                File.Copy(fileName, tmpFile, true);
                stream = new FileStream(tmpFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
      
            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream); //.xlsx
            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };
            
            //Return as DataSet
            DataSet result = excelReader.AsDataSet(conf);
            stream.Close();
            //Get all the Tables
            DataTableCollection table = result.Tables;
            //Store it in DataTable
            DataTable resultTable = table["Sheet1"];
           
            //return
            return resultTable;
        }

        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in _dataCol
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
        public void killExcellProcess()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }

    }

}
