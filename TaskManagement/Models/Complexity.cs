using System.Collections.Generic;

namespace Models
{
    public partial class Complexity
    {
        public Complexity()
        {
            Tasks = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public int? ComplexityQual1 { get; set; }
        public int? ComplexityQual2 { get; set; }
        public int? ComplexityQual3 { get; set; }
        public int? ComplexityQual4 { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
