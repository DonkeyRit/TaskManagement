using System.Collections.Generic;

namespace Models
{
    public partial class Status
    {
        public Status()
        {
            EventlogIdCurrentStatusNavigation = new HashSet<Eventlog>();
            EventlogIdLastStatusNavigation = new HashSet<Eventlog>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Eventlog> EventlogIdCurrentStatusNavigation { get; set; }
        public virtual ICollection<Eventlog> EventlogIdLastStatusNavigation { get; set; }
    }
}
