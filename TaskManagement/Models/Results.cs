using System.Collections.Generic;

namespace Models
{
    public partial class Results
    {
        public Results()
        {
            Assignedtasks = new HashSet<Assignedtasks>();
        }

        public int Id { get; set; }
        public int? ResultQual1 { get; set; }
        public int? ResultQual2 { get; set; }
        public int? ResultQual3 { get; set; }
        public int? ResultQual4 { get; set; }

        public virtual ICollection<Assignedtasks> Assignedtasks { get; set; }
    }
}
