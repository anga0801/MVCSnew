using System;
using System.Data.SqlClient;
using System.IO;

namespace CRUDSuppliers_DAL.Logging
{
    public class Logger
    {
        public static string ErrorLogPath;

        public static void SqlExceptionLog(SqlException sqlEx)
        {
            ExceptionLog(sqlEx, sqlEx.Server);
        }

        public static void ExceptionLog(Exception ex, string server = "")
        {
            ex.Data["Logged"] = true;
            StreamWriter sw = new StreamWriter(ErrorLogPath, true);

            sw.WriteLine("*****************************");
            sw.WriteLine();
            sw.WriteLine();
            sw.WriteLine("{0:MMMM dd, yyyy, hh:mm tt}", DateTime.Now);
            sw.WriteLine();
            sw.WriteLine(ex.Message);
            sw.WriteLine();

            if (server != string.Empty)
            {
                sw.WriteLine(server);
                sw.WriteLine();
            }

            sw.WriteLine(ex.StackTrace);


            sw.Close();
            sw.Dispose();
        }

    }
}
