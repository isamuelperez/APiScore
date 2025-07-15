using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Contracts;

namespace Application.Nota.Delete
{
    public class DeleteCommand
    {

        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Domain.Entities.Nota>> Handle(int id)
        {
            await _unitOfWork.BeginTransaction();

            try
            {
                var nota = _unitOfWork.GenericRepository<Domain.Entities.Nota>().
                    FindBy(s => s.Id == id)?.FirstOrDefault();
                if (nota != null)
                {
                    _unitOfWork.GenericRepository<Domain.Entities.Nota>().Delete(nota);
                    await _unitOfWork.Commit();
                    return new Response<Domain.Entities.Nota>($"Se Elimino correctamente la nota: {nota.Name}", 204);
                }

                return new Response<Domain.Entities.Nota>($"La Nota no existe", 400);
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return new Response<Domain.Entities.Nota>($"Error", 500);
            }

        }
    }
}
