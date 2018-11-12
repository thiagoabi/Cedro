using Microsoft.Extensions.Logging;
using Restaurante.Domain.Entity;
using Restaurante.Infrastructure.Context;
using Restaurante.Infrastructure.Data;
using Restaurante.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infrastructure.Store
{
    public class StoreBase<TKey, TModel> : IStoreBase<TKey, TModel>
        where TKey : IEquatable<TKey>
        where TModel : EntityBase<TKey>
    {
        private readonly ILogger<StoreBase<TKey, TModel>> _logger;
        internal readonly IRestauranteContext _context;
        internal readonly IUnitOfWork _uow;
        internal readonly IRepositoryBase<TKey, TModel> _repository;

        public StoreBase(IRepositoryBase<TKey, TModel> repository, ILogger<StoreBase<TKey, TModel>> logger, IRestauranteContext context, IUnitOfWork uow)
        {
            _repository = repository;
            _logger = logger;
            _context = context;
            _uow = uow;
        }


        public async Task<IEnumerable<TModel>> CreateAsync(CancellationToken cancellationToken, params TModel[] entity)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _repository.CreateAsync(cancellationToken, _context.Connection(), entity);
        }

        public async Task<IEnumerable<TModel>> UpdateAsync(CancellationToken cancellationToken, params TModel[] entity)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _repository.UpdateAsync(cancellationToken, _context.Connection(), entity);
        }

        public Task<bool> RemoveAsync(CancellationToken cancellationToken, params TModel[] entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAsync(CancellationToken cancellationToken, params TKey[] id)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //await CreateTransactionIfNotExists(cancellationToken);

            return await _repository.RemoveAsync(_context.Connection(), id);

        }

        public async Task<TModel> GetByIdAsync(CancellationToken cancellationToken, TKey id, params Expression<Func<TModel, object>>[] navigationProperties)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //await CreateTransactionIfNotExists(cancellationToken);

            return await _repository.GetByIdAsync(cancellationToken, _context.Connection(), id, navigationProperties);
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<TModel, object>>[] navigationProperties)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //await CreateTransactionIfNotExists(cancellationToken);

            return await _repository.GetAllAsync(cancellationToken, _context.Connection(), navigationProperties);
        }

        public async Task<IEnumerable<TModel>> GetWithFilterAsync(CancellationToken cancellationToken, Expression<Func<TModel, bool>> where, params Expression<Func<TModel, object>>[] navigationProperties)
        {
            cancellationToken.ThrowIfCancellationRequested();
            //await CreateTransactionIfNotExists(cancellationToken);

            return await _repository.GetWithFilterAsync(cancellationToken, _context.Connection(), where, navigationProperties);

        }

        internal void CommitTransaction()
        {
            if (_uow.Transaction != null)
            {
                _uow.Commit();
            }
        }

        internal async Task CreateTransactionIfNotExists(CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync(cancellationToken);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
        }
    }
}
