using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Nota
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public int TeacherId { get; set; }
        public Teacher ? Teacher { get; set; }

        public int StudentId { get; set; }
        public Student ? Student { get; set; }

        public decimal Valor { get; set; }
    }
}
