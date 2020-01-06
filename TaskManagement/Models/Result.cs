using System.Collections.Generic;

namespace Models
{
    public class Result
    {
        public Result()
        {
            Assignedtasks = new HashSet<AssignedTask>();
        }

        public int Id { get; set; }
        public int? ResultQual1 { get; set; }
        public int? ResultQual2 { get; set; }
        public int? ResultQual3 { get; set; }
        public int? ResultQual4 { get; set; }

        public virtual ICollection<AssignedTask> Assignedtasks { get; set; }
    }
}
