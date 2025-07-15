using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Student.Get
{
    public class GetResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Identification { get; set; }

        public List<NotaDto> ? Notas { get; set; }
    }

    public class NotaDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Valor { get; set; }

    }
}
