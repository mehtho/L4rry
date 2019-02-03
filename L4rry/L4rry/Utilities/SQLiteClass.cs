using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.IO;

namespace L4rry.Utilities
{
    class SQLiteClass
    {
        private SQLiteConnection m_dbConnection;
        private String fileLocation;

        public SQLiteClass()
        {
            Directory.CreateDirectory(ProgramFilesx86() + "\\L4rry");
            fileLocation = ProgramFilesx86() + "\\L4rry\\Information.dat";
            if (File.Exists(fileLocation) && new FileInfo(fileLocation).Length == 0)
            {
                File.Delete(fileLocation);
            }

            if (!File.Exists(fileLocation))
            {
                Console.WriteLine("Writing a new DB at " + fileLocation);
                SQLiteConnection.CreateFile(fileLocation);
                m_dbConnection = new SQLiteConnection("Data Source=" + fileLocation + ";Version=3;");
                m_dbConnection.Open();

                string sql = "CREATE TABLE Images (Image BLOB PRIMARY KEY NOT NULL )";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }

            fileLocation = ProgramFilesx86() + "\\L4rry\\Information.dat";
            m_dbConnection = new SQLiteConnection("Data Source=" + fileLocation + ";Version=3;");
            m_dbConnection.Open();
        }

        public void CloseCon()
        {
            m_dbConnection.Close();
        }

        public static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }
}
