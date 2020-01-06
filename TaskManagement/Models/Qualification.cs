using System.Collections.Generic;

namespace Models
{
    public class Qualification
    {
        public Qualification()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Coefficient { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
