using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Nota.Get
{
    public class GetResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Valor { get; set; }
        public int StudentId { get; set; }
        public StudentDto? Student { get; set; }
        public int TeacherId { get; set; }
        public TeacherDto ? Teacher { get; set; }

      

       
    }

    public class TeacherDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class StudentDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
