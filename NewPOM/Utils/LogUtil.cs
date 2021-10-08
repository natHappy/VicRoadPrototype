using NewPOM.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NewPOM.Utils
{
    public static class LogUtil
    {
        //Global Declaration
        private static string _logFileName = string.Format("{0:yyyymmddhhmmss}", DateTime.Now);
        private static StreamWriter _streamWriter = null;

        //Create a file which can store the log information
        public static void CreateLogFile()
        {
            string dir = Environment.CurrentDirectory.ToString() + Settings.LogPath;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            _streamWriter = File.AppendText(dir + _logFileName + ".log");
        }

        /// <summary>
        /// create logfile with name <paramref name="name"/>_<yyyymmddhhmmss>.log
        /// </summary>
        /// <param name="name"></param>
        public static void CreateLogFile(string name)
        {
            string dir = Environment.CurrentDirectory.ToString() + Settings.LogPath;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            _logFileName = name + _logFileName;
            _streamWriter = File.AppendText(dir + _logFileName + ".log");
        }



        //Create a method which can write the text in the log file
        public static void Write(string logMessage)
        {
            
            _streamWriter.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            _streamWriter.WriteLine("    {0}", logMessage);
            _streamWriter.Flush();
        }

        public static void CloseLog()
        {
            _streamWriter.Close();
        }

    }
}
