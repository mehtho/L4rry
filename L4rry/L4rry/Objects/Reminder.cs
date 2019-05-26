using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L4rry.Items
{
    public class Reminder
    {
        public String UserID;
        public String Task;
        public DateTime Time;
        public bool IsRepeatable;

        public Reminder(String UserID, String Task, DateTime Time, bool IsRepeatable)
        {
            this.UserID = UserID;
            this.Task = Task;
            this.Time = Time;
            this.IsRepeatable = IsRepeatable;
        }
    }
}
