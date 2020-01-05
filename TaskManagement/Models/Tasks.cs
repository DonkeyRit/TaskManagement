using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Tasks
    {
        public Tasks()
        {
            Assignedtasks = new HashSet<Assignedtasks>();
            Eventlog = new HashSet<Eventlog>();
            InverseIdParentTaskNavigation = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdParentTask { get; set; }
        public string Description { get; set; }
        public int IdComplexity { get; set; }
        public DateTime DateDelivery { get; set; }
        public int IdTaskManager { get; set; }

        public virtual Complexity IdComplexityNavigation { get; set; }
        public virtual Tasks IdParentTaskNavigation { get; set; }
        public virtual ICollection<Assignedtasks> Assignedtasks { get; set; }
        public virtual ICollection<Eventlog> Eventlog { get; set; }
        public virtual ICollection<Tasks> InverseIdParentTaskNavigation { get; set; }
    }
}
