using System.Collections.Generic;

namespace Models
{
    public class Status
    {
        public Status()
        {
            EventlogIdCurrentStatusNavigation = new HashSet<EventLog>();
            EventlogIdLastStatusNavigation = new HashSet<EventLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EventLog> EventlogIdCurrentStatusNavigation { get; set; }
        public virtual ICollection<EventLog> EventlogIdLastStatusNavigation { get; set; }
    }
}
