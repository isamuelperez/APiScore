using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Contracts;

namespace Application.Teacher.Update
{
    public class UpdateCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<Domain.Entities.Teacher>> Handle(UpdateRequest request)
        {
            await _unitOfWork.BeginTransaction();
            if (request is null) return new Response<Domain.Entities.Teacher>("El Profesor es nulo", 400);

            else
            {
                try
                {
                    var student = _unitOfWork.GenericRepository<Domain.Entities.Teacher>().
                        FindBy(s => s.Id == request.Id)?.FirstOrDefault();
                    if (student != null)
                    {
                        student.Identification = request.Identification;
                        student.Name = request.Name;
                        _unitOfWork.GenericRepository<Domain.Entities.Teacher>().Update(student);
                        await _unitOfWork.Commit();
                        return new Response<Domain.Entities.Teacher>($"Se Modifico el Profesor: {request.Name}", 200, student);
                    }

                    return new Response<Domain.Entities.Teacher>($"El Profesor con ID: {request.Id} no existe", 400);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<Domain.Entities.Teacher>($"Error", 500);
                }
            }
        }
    }
}
