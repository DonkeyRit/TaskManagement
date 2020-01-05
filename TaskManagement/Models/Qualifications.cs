using System.Collections.Generic;

namespace Models
{
    public partial class Qualifications
    {
        public Qualifications()
        {
            Employees = new HashSet<Employees>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Coefficient { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
