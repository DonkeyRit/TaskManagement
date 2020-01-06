using System;

namespace Models
{
    public class EventLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? IdLastStatus { get; set; }
        public int? IdCurrentStatus { get; set; }
        public int IdEmployee { get; set; }
        public int IdTask { get; set; }

        public virtual Status IdCurrentStatusNavigation { get; set; }
        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual Status IdLastStatusNavigation { get; set; }
        public virtual Task IdTaskNavigation { get; set; }
    }
}
