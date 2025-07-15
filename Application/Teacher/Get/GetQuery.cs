using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Contracts;

namespace Application.Teacher.Get
{
    public class GetQuery
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<GetResponse>>Handle(int id)
        {
            await _unitOfWork.BeginTransaction();

            try
            {
                var teachers = _unitOfWork.GenericRepository<Domain.Entities.Teacher>().
                    FindBy(s => s.Id == id)?.FirstOrDefault();

                var getResponse = new GetResponse();

                getResponse.Id = teachers.Id;
                getResponse.Name = teachers.Name;
                getResponse.Identification = teachers.Identification;

                if (teachers != null)
                {
                    return new Response<GetResponse>($"Profesor encontrado: ", 200, getResponse);
                }

                return new Response<GetResponse>("El Profesor  no existe", 200, getResponse);
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

            var teachers = _unitOfWork.GenericRepository<Domain.Entities.Teacher>().GetAll().ToList();

            return new Response<List<GetResponse>>("Profesores encontrados: ", 200, MapTeachers(teachers));

        }

        private List<GetResponse> MapTeachers(List<Domain.Entities.Teacher> teachers)
        {
            List<GetResponse> lista = new List<GetResponse>();
            foreach (var t in teachers)
            {
                var teacher = new GetResponse();

                teacher.Id = t.Id;
                teacher.Name = t.Name;
                teacher.Identification = t.Identification;

                lista.Add(teacher);
            }
            return lista;
        }
    }
}
