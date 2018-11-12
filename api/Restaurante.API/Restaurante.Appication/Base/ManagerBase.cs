using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Restaurante.Infrastructure.Repository;
using Restaurante.Infrastructure.Store;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Appication
{
    public class ManagerBase<TKey, TModel> : IManagerBase<TKey, TModel>
        where TKey : IEquatable<TKey>
        where TModel : class
    {
        private readonly HttpContext _context;
        protected CancellationToken CancellationToken => _context?.RequestAborted ?? CancellationToken.None;

        private IStoreBase<TKey, TModel> _store;

        public ManagerBase(
            ILogger<IManagerBase<TKey, TModel>> logger,
            IStoreBase<TKey, TModel> store,
            IHttpContextAccessor contextAccessor)
        {
            _context = contextAccessor?.HttpContext;
            _store = store;
        }

        /// <summary>
        /// Insere registros da tabela por entity
        /// </summary>
        public async Task<IEnumerable<TModel>> CreateAsync(params TModel[] entity)
        {
            return await _store.CreateAsync(CancellationToken, entity);
        }

        /// <summary>
        /// Atualiza registros da tabela por entity
        /// </summary>
        public async Task<IEnumerable<TModel>> UpdateAsync(params TModel[] entity)
        {
            return await _store.UpdateAsync(CancellationToken, entity);
        }

        /// <summary>
        /// Remove registros da tabela por entity
        /// </summary>
        public Task<bool> RemoveAsync(params TModel[] entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insere registros da tabela por id
        /// </summary>
        public async Task<bool> RemoveAsync(params TKey[] id)
        {
            return await _store.RemoveAsync(CancellationToken, id);
        }

        /// <summary>
        /// busca um registro da tabela por id
        /// </summary>
        public async Task<TModel> GetByIdAsync(TKey id, params Expression<Func<TModel, object>>[] navigationProperties)
        {
            return await _store.GetByIdAsync(CancellationToken, id, navigationProperties);
        }

        /// <summary>
        /// busca todos registros da tabela
        /// </summary>
        public async Task<IEnumerable<TModel>> GetAllAsync(params Expression<Func<TModel, object>>[] navigationProperties)
        {
            return await _store.GetAllAsync(CancellationToken, navigationProperties);
        }

        /// <summary>
        /// busca todos registros da tabela de acordo com o filtro
        /// </summary>
        public async Task<IEnumerable<TModel>> GetWithFilterAsync(Expression<Func<TModel, bool>> where, params Expression<Func<TModel, object>>[] navigationProperties)
        {
            return await _store.GetWithFilterAsync(CancellationToken, where, navigationProperties);
        }
    }
}
