using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Employees
    {
        public Employees()
        {
            Assignedtasks = new HashSet<Assignedtasks>();
            Eventlog = new HashSet<Eventlog>();
        }

        public int Id { get; set; }
        public string Fio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int IdQualification { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int IdType { get; set; }

        public virtual Qualifications IdQualificationNavigation { get; set; }
        public virtual Type IdTypeNavigation { get; set; }
        public virtual ICollection<Assignedtasks> Assignedtasks { get; set; }
        public virtual ICollection<Eventlog> Eventlog { get; set; }
    }
}
