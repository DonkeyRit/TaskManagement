using System;
using System.Collections.Generic;

namespace Models
{
    public partial class Assignedtasks
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public int IdEmployee { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? IdResult { get; set; }
        public string Comment { get; set; }

        public virtual Employees IdEmployeeNavigation { get; set; }
        public virtual Results IdResultNavigation { get; set; }
        public virtual Tasks IdTaskNavigation { get; set; }
    }
}
