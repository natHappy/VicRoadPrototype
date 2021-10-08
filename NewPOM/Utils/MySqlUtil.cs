using MySql.Data.MySqlClient;
using NewPOM.Bases;
using NewPOM.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace NewPOM.Utils
{
    public static class MySqlUtil 
    {
        private static List<Datacollection> _dataCol = new List<Datacollection>();
        public static MySqlConnection DBConnect()
        {
            try
            {
                MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=sakila;port=3306;password=password");
                con.Open();
                return con;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR :: " + e.Message);
                LogUtil.Write("ERROR :: " + e.Message);
            }
            return null;
        }
        //Closing the connection 
        public static void DBClose(this MySqlConnection sqlConnection)
        {
            try
            {
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                LogUtil.Write("ERROR :: " + e.Message);
            }
        }
        //Execution
        public static List<Datacollection> GetData(this MySqlConnection sqlConnection, string queryString)
        {
           try
            {
                //Checking the state of the connection
                if (sqlConnection == null || ((sqlConnection != null && (sqlConnection.State == ConnectionState.Closed ||
                    sqlConnection.State == ConnectionState.Broken))))
                    sqlConnection.Open();

                MySqlDataAdapter dataAdaptor = new MySqlDataAdapter();
                dataAdaptor.SelectCommand = new MySqlCommand(queryString, sqlConnection);
                dataAdaptor.SelectCommand.CommandType = CommandType.Text;

                DataSet dataset  = new DataSet();
                dataAdaptor.Fill(dataset, "table");
                sqlConnection.Close();
                DataTable table = dataset.Tables["table"];
                _dataCol = table.ConvertDataToList();

                return _dataCol;
            }
            catch (Exception e)
            {
                _dataCol = null;
                sqlConnection.Close();
                LogUtil.Write("ERROR :: " + e.Message);
                return null;
            }
        }

        public static string ReadData(int rowNumber, string columnName)
        {
            return _dataCol.ReadData(rowNumber, columnName);

        }
    }
}

