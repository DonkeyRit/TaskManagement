using System.Collections.Generic;

namespace Models
{
    public partial class Type
    {
        public Type()
        {
            Employees = new HashSet<Employees>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
