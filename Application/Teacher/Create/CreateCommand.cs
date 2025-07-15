using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Contracts;

namespace Application.Teacher.Create
{
    public class CreateCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Domain.Entities.Teacher>> Handle(CreateRequest request)
        {
            await _unitOfWork.BeginTransaction();
            if (request is null) return new Response<Domain.Entities.Teacher>("El Profesor es nulo", 400);

            else
            {
                try
                {
                    var teacher = _unitOfWork.GenericRepository<Domain.Entities.Teacher>().
                        FindBy(s => s.Identification == request.Identification)?.FirstOrDefault();
                    if (teacher is null)
                    {
                        var teacherCreate = _unitOfWork.GenericRepository<Domain.Entities.Teacher>().AddAndReturn(MapTeacher(request));
                        await _unitOfWork.Commit();
                        return new Response<Domain.Entities.Teacher>($"Se creo correctamente el Profesor: {teacherCreate.Name}", teacherCreate);
                    }

                    return new Response<Domain.Entities.Teacher>($"El Profesor con identificación: {request.Identification} ya existe", 400);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<Domain.Entities.Teacher>($"Error", 500);
                }
            }
        }

        private Domain.Entities.Teacher MapTeacher(CreateRequest createRequest)
        {
            return new()
            {
                Identification = createRequest.Identification,
                Name = createRequest.Name
            };
        }
    }
}
