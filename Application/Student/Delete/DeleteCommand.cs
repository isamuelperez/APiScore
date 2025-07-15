using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Application.Student.Create;
using Domain.Contracts;

namespace Application.Student.Delete
{
    public class DeleteCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Domain.Entities.Student>> Handle(int id)
        {
            await _unitOfWork.BeginTransaction();

            try
            {
                var student = _unitOfWork.GenericRepository<Domain.Entities.Student>().
                    FindBy(s => s.Id == id)?.FirstOrDefault();
                if (student != null)
                {
                    _unitOfWork.GenericRepository<Domain.Entities.Student>().Delete(student);
                    await _unitOfWork.Commit();
                    return new Response<Domain.Entities.Student>($"Se Elimino correctamente el Estudiante: {student.Name}", 204);
                }

                return new Response<Domain.Entities.Student>($"El estudiante no existe", 400);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Response<Domain.Entities.Student>($"Error", 500);
            }

        }

    }
}
