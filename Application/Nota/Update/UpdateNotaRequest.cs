using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Nota.Update
{
    public class UpdateNotaRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }

        public decimal Valor { get; set; }
    }
}
