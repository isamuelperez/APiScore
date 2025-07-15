using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Teacher.Get
{
    public class GetResponse
    {
        public virtual int Id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Identification { get; set; }
    }
}
