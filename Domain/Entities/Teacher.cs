using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Teacher: Person
    {
        public ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}
