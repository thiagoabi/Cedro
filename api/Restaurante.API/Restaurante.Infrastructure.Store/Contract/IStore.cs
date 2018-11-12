using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infrastructure.Store
{
    public interface IStoreBase<TKey, TModel> : IDisposable
        where TKey : IEquatable<TKey>
        where TModel : class
    {
        Task<IEnumerable<TModel>> CreateAsync(CancellationToken cancellationToken, params TModel[] entity);
        Task<IEnumerable<TModel>> UpdateAsync(CancellationToken cancellationToken, params TModel[] entity);
        Task<bool> RemoveAsync(CancellationToken cancellationToken, params TModel[] entity);
        Task<bool> RemoveAsync(CancellationToken cancellationToken, params TKey[] id);

        Task<TModel> GetByIdAsync(CancellationToken cancellationToken, TKey id, params Expression<Func<TModel, object>>[] navigationProperties);
        Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<TModel, object>>[] navigationProperties);
        Task<IEnumerable<TModel>> GetWithFilterAsync(CancellationToken cancellationToken, Expression<Func<TModel, bool>> where, params Expression<Func<TModel, object>>[] navigationProperties);
    }
}