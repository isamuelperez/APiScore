using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Contracts;

namespace Application.Nota.Create
{
    public class CreateCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Domain.Entities.Nota>> Handle(CreateNotaaRequest request)
        {
            await _unitOfWork.BeginTransaction();
            if (request is null) return new Response<Domain.Entities.Nota>("Nota nula", 400);

            else
            {
                try
                {
                    var teacher = _unitOfWork.GenericRepository<Domain.Entities.Teacher>().
                        FindBy(t => t.Id == request.TeacherId)?.FirstOrDefault();


                    var student = _unitOfWork.GenericRepository<Domain.Entities.Student>().
                        FindBy(s => s.Id == request.StudentId)?.FirstOrDefault();

                    if (student != null && teacher != null)
                    {
                        var notaCreate = _unitOfWork.GenericRepository<Domain.Entities.Nota>().AddAndReturn(MapNota(request));
                        await _unitOfWork.Commit();
                        return new Response<Domain.Entities.Nota>($"Se creo correctamente el Nota", 201);
                    }
                    
                    return new Response<Domain.Entities.Nota>("Estudioante o Profesor no existente", 400);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<Domain.Entities.Nota>($"Error", 500);
                }
            }
        }

        private Domain.Entities.Nota MapNota(CreateNotaaRequest createRequest)
        {
            return new()
            {
                Name = createRequest.Name,
                TeacherId = createRequest.TeacherId,
                StudentId = createRequest.StudentId,
                Valor = createRequest.Valor,
            };
        }
    }
}
