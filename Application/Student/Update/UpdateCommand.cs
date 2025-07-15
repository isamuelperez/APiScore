using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Application.Student.Create;
using Domain.Contracts;

namespace Application.Student.Update
{
    public class UpdateCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<Domain.Entities.Student>> Handle(UpdateRequest request)
        {
            await _unitOfWork.BeginTransaction();
            if (request is null) return new Response<Domain.Entities.Student>("El estudiante es nulo", 400);

            else
            {
                try
                {
                    var student = _unitOfWork.GenericRepository<Domain.Entities.Student>().
                        FindBy(s => s.Id == request.Id)?.FirstOrDefault();
                    if (student != null)
                    {
                        student.Identification = request.Identification;
                        student.Name = request.Name;
                        _unitOfWork.GenericRepository<Domain.Entities.Student>().Update(student);
                        await _unitOfWork.Commit();
                        return new Response<Domain.Entities.Student>($"Se Modifico el Estudiante: {request.Name}", 200, student);
                    }

                    return new Response<Domain.Entities.Student>($"El estudiante con ID: {request.Id} no existe", 400);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<Domain.Entities.Student>($"Error", 500);
                }
            }
        }
    }
}
