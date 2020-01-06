using System;
using System.Collections.Generic;

namespace Models
{
    public class Task
    {
        public Task()
        {
            Assignedtasks = new HashSet<AssignedTask>();
            Eventlog = new HashSet<EventLog>();
            InverseIdParentTaskNavigation = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdParentTask { get; set; }
        public string Description { get; set; }
        public int IdComplexity { get; set; }
        public DateTime DateDelivery { get; set; }
        public int IdTaskManager { get; set; }

        public virtual Complexity IdComplexityNavigation { get; set; }
        public virtual Task IdParentTaskNavigation { get; set; }
        public virtual ICollection<AssignedTask> Assignedtasks { get; set; }
        public virtual ICollection<EventLog> Eventlog { get; set; }
        public virtual ICollection<Task> InverseIdParentTaskNavigation { get; set; }
    }
}
