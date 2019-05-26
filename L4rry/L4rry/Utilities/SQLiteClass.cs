using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.IO;
using L4rry.Items;

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

                string sql = "CREATE TABLE Reminders (User TEXT, Task TEXT, Time TEXT, Repeat NUMERIC)";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }

            fileLocation = ProgramFilesx86() + "\\L4rry\\Information.dat";
            m_dbConnection = new SQLiteConnection("Data Source=" + fileLocation + ";Version=3;");
            m_dbConnection.Open();
        }

        public void RemoveReminder(Reminder r)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("DELETE FROM Reminders WHERE User=$u AND Task=$t AND Time=$d AND Repeat=$r", m_dbConnection))
            {
                cmd.Parameters.AddWithValue("$u", r.UserID);
                cmd.Parameters.AddWithValue("$t", r.Task);
                cmd.Parameters.AddWithValue("$d", r.Time);
                cmd.Parameters.AddWithValue("$r", r.IsRepeatable);

                cmd.ExecuteNonQuery();
            }
        }

        public void AddReminder(Reminder r)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO Reminders " +
                    "(User, Task, Time, Repeat) VALUES ($u,$t,$d,$r)", m_dbConnection))
            {
                cmd.Parameters.AddWithValue("$u", r.UserID);
                cmd.Parameters.AddWithValue("$t", r.Task);
                cmd.Parameters.AddWithValue("$d", r.Time);
                cmd.Parameters.AddWithValue("$r", r.IsRepeatable);

                cmd.ExecuteNonQuery();
            }
        }

        public Reminder GetLatestReminder()
        {
            String sql = "SELECT * FROM Reminders ORDER BY Time asc";
            using (SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Reminder(reader.GetString(0),reader.GetString(1),reader.GetDateTime(2),reader.GetBoolean(3));
                    }
                }
            }
            return null;
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
