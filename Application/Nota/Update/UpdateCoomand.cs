using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Domain.Contracts;

namespace Application.Nota.Update
{
    public class UpdateCoomand
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCoomand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<Domain.Entities.Nota>> Handle(UpdateNotaRequest request)
        {
            await _unitOfWork.BeginTransaction();
            if (request is null) return new Response<Domain.Entities.Nota>("El estudiante es nulo", 400);

            else
            {
                try
                {
                    var nota= _unitOfWork.GenericRepository<Domain.Entities.Nota>().
                        FindBy(s => s.Id == request.Id)?.FirstOrDefault();
                    if (nota != null)
                    {
                        nota.Name = request.Name;
                        nota.Valor = request.Valor;
                        nota.TeacherId = request.TeacherId;
                        nota.StudentId = request.StudentId;
                        _unitOfWork.GenericRepository<Domain.Entities.Nota>().Update(nota);
                        await _unitOfWork.Commit();
                        return new Response<Domain.Entities.Nota>($"Se Modifico la Nota: {request.Name}", 200, nota);
                    }

                    return new Response<Domain.Entities.Nota>($"LaNota con ID: {request.Id} no existe", 400);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return new Response<Domain.Entities.Nota>($"Error", 500);
                }
            }
        }
    }
}
