using System;
using System.Collections.Generic;

namespace Models
{
    public class Employee
    {
        public Employee()
        {
            Assignedtasks = new HashSet<AssignedTask>();
            Eventlog = new HashSet<EventLog>();
        }

        public int Id { get; set; }
        public string Fio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int IdQualification { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int IdType { get; set; }

        public virtual Qualification IdQualificationNavigation { get; set; }
        public virtual Type IdTypeNavigation { get; set; }
        public virtual ICollection<AssignedTask> Assignedtasks { get; set; }
        public virtual ICollection<EventLog> Eventlog { get; set; }
    }
}
