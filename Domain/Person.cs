using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class Person
    {
        public virtual int Id { get; private set; }
        public virtual string ? Name { get; set; }
        public virtual string ? Identification { get; set; }
    }
}
