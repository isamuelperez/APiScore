using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Nota.Create
{
    public class CreateNotaaRequest
    {
        public string? Name { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public decimal Valor { get; set; }
    }
}
