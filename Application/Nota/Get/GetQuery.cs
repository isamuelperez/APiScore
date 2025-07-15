using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Application.Student.Get;
using Domain.Contracts;

namespace Application.Nota.Get
{
    public class GetQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetResponse>> Handle(int id)
        {
            await _unitOfWork.BeginTransaction();

            try
            {
                var nota = _unitOfWork.GenericRepository<Domain.Entities.Nota>().
                    FindBy(s => s.Id == id)?.FirstOrDefault();

                if (nota != null)
                {
                    return new Response<GetResponse>($"Estudiante encontrado", 200, null);
                }

                return new Response<GetResponse>("El estudiante  no existe", 200, null);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Response<GetResponse>($"Error", 500);
            }

        }

        public async Task<Response<List<GetResponse>>> Handle()
        {
            await _unitOfWork.BeginTransaction();

            var notas = _unitOfWork.GenericRepository<Domain.Entities.Nota>().GetAll().ToList();
            return new Response<List<GetResponse>>("Notas encontradas", 200, MapNota(notas));

        }

        private List<Domain.Entities.Nota> ListNotas(int idStudent)
        {
            var notas = _unitOfWork.GenericRepository<Domain.Entities.Nota>().
                    FindBy(n => n.StudentId == idStudent)?.ToList();
            return notas;
        }


        private List<GetResponse> MapNota(List<Domain.Entities.Nota> notas)
        {
            //List<Domain.Entities.Nota> notasList = new List<Domain.Entities.Nota>();
            List<GetResponse> lisGetResponse = new List<GetResponse>();

            GetResponse getResponse;
            foreach (var nota in notas)
            {
                getResponse = new GetResponse();
                var student = _unitOfWork.GenericRepository<Domain.Entities.Student>().FindBy(s => s.Id == nota.StudentId)?.FirstOrDefault();
                var teacher = _unitOfWork.GenericRepository<Domain.Entities.Teacher>().FindBy(t => t.Id == nota.TeacherId)?.FirstOrDefault();

                getResponse.StudentId = nota.StudentId;
                getResponse.TeacherId = nota.TeacherId;

                StudentDto studentDto = new StudentDto();
                studentDto.Id = student.Id;
                studentDto.Name = student.Name;
                getResponse.Student = studentDto;

                TeacherDto teacherDto = new TeacherDto();
                teacherDto.Id = teacher.Id;
                teacherDto.Name = teacher.Name;

                getResponse.Teacher = teacherDto;
                getResponse.Id= nota.Id;
                getResponse.Name = nota.Name;
                getResponse.Valor = nota.Valor;

                lisGetResponse.Add(getResponse);




            }

            return lisGetResponse;
           

        }
    }
}
