using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Domain;
using Domain.Contracts;
using Domain.Entities;

namespace Application.Student.Create
{
    public class CreateCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Domain.Entities.Student>> Handle(CreateRequest request)
        {
            await _unitOfWork.BeginTransaction();
            if (request is null) return new Response<Domain.Entities.Student>("El estudiante es nulo", 400);

            else
            {
                try
                {
                    var student = _unitOfWork.GenericRepository<Domain.Entities.Student>().
                        FindBy(s => s.Identification == request.Identification)?.FirstOrDefault();
                    if (student is null)
                    {
                        var studentCreate = _unitOfWork.GenericRepository<Domain.Entities.Student>().AddAndReturn(MapStudent(request));
                        await _unitOfWork.Commit();
                        return new Response<Domain.Entities.Student>($"Se creo correctamente el Estudiante: {studentCreate.Name}", studentCreate);
                    }

                    return new Response<Domain.Entities.Student>($"El estudiante con identificación: {request.Identification} ya existe", 400);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<Domain.Entities.Student>($"Error", 500);
                }
            }
        }

        private Domain.Entities.Student MapStudent(CreateRequest createRequest)
        {
            return new()
            {
                Identification = createRequest.Identification,
                Name = createRequest.Name
            };
        }
    }
}
