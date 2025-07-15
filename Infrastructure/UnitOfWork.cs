using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Infrastructure.Data;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ScoreContext _scoreContext;
        public UnitOfWork(ScoreContext scoreContext)
        {
            _scoreContext = scoreContext;
        }
        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            return new GenericRepository<T>(_scoreContext);
        }
        public async Task BeginTransaction()
        {
            await _scoreContext.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            try
            {
                _scoreContext.SaveChanges();
                await _scoreContext.Database.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                await _scoreContext.Database.RollbackTransactionAsync();
            }
        }

        public async Task Rollback()
        {
            await _scoreContext.Database.RollbackTransactionAsync();
        }
    }
}
