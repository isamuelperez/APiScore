using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Contracts;

namespace Application.Teacher.Delete
{
    public class DeleteCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Domain.Entities.Teacher>> Handle(int id)
        {
            await _unitOfWork.BeginTransaction();

            try
            {
                var teacher = _unitOfWork.GenericRepository<Domain.Entities.Teacher>().
                    FindBy(s => s.Id == id)?.FirstOrDefault();
                if (teacher != null)
                {
                    _unitOfWork.GenericRepository<Domain.Entities.Teacher>().Delete(teacher);
                    await _unitOfWork.Commit();
                    return new Response<Domain.Entities.Teacher>($"Se Elimino correctamente el Profesor: {teacher.Name}", 204);
                }

                return new Response<Domain.Entities.Teacher>($"El Profesor no existe", 400);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Response<Domain.Entities.Teacher>($"Error", 500);
            }

        }
    }
}
