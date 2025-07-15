using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Application.Student.Create;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Student.Get
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
                var student = _unitOfWork.GenericRepository<Domain.Entities.Student>().
                    FindBy(s => s.Id == id)?.FirstOrDefault();

                if (student != null)
                {
                    return new Response<GetResponse>($"Estudiante encontrado", 200, MapStudent(student));
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

            var students = _unitOfWork.GenericRepository<Domain.Entities.Student>().GetAll().ToList();
            return new Response<List<GetResponse>>("Estudiantes encontrados", 200, MapStudent(students));

        }

        private List<Domain.Entities.Nota> ListNotas(int idStudent)
        {
            var notas = _unitOfWork.GenericRepository<Domain.Entities.Nota>().
                    FindBy(n => n.StudentId == idStudent)?.ToList();
            return notas;
        }

        private List<GetResponse> MapStudent(List<Domain.Entities.Student> students)
        {
            List<GetResponse> getResponses = new List<GetResponse>();
            List<Domain.Entities.Nota> notas = new List<Domain.Entities.Nota>();
            GetResponse getResponse;
            List<NotaDto> notasDto;

            NotaDto notaDto;


            foreach (var s in students)
            {

                notaDto = new NotaDto();
                notas = ListNotas(s.Id);
                getResponse = new GetResponse();

                notasDto = new List<NotaDto>();


                foreach (var nota in notas)
                {
                    notaDto = new NotaDto();
                    notaDto.Id = nota.Id;
                    notaDto.Name = nota.Name;
                    notaDto.Valor = nota.Valor;
                    notasDto.Add(notaDto);
                }
                
                getResponse.Id = s.Id;
                getResponse.Name = s.Name;
                getResponse.Identification = s.Identification;
                getResponse.Notas = notasDto;

                /*
                if (notas.Count > 0)
                {
                    getResponse.Notas = notasDto;
                }
                else
                {
                    getResponse.Notas = ;
                }
                */




                getResponses.Add(getResponse);
            }
            return getResponses;
        }

        private GetResponse MapStudent(Domain.Entities.Student student)
        {
            var notas = ListNotas(student.Id);
            GetResponse getResponse = new GetResponse();

            List<NotaDto> notasDto = new List<NotaDto>();
            foreach (var nota in notas)
            {
                NotaDto notaDto = new NotaDto();

                notaDto.Id = nota.Id;
                notaDto.Name = nota.Name;
                notaDto.Valor = nota.Valor;
                notasDto.Add(notaDto);
            }

            getResponse.Id = student.Id;
            getResponse.Name = student.Name;
            getResponse.Identification = student.Identification;
            getResponse.Notas = notasDto;

            return getResponse;
        }

    }
}
