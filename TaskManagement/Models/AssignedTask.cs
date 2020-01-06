using System;
using System.Collections.Generic;

namespace Models
{
    public class AssignedTask
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public int IdEmployee { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? IdResult { get; set; }
        public string Comment { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; }
        public virtual Result IdResultNavigation { get; set; }
        public virtual Task IdTaskNavigation { get; set; }
    }
}
