using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class UpdateRequest
    {
        public virtual int Id { get; set; }
        public virtual string? Name { get; set; }
        public string? Identification { get; set; }
    }
}
