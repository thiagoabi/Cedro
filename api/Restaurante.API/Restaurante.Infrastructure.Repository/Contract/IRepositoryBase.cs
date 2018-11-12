using Restaurante.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infrastructure.Repository
{
    public interface IRepositoryBase<TKey, TModel>
        where TKey : IEquatable<TKey>
        where TModel : IEntityBase<TKey>
    {
        Task<IEnumerable<TModel>> CreateAsync(CancellationToken cancellationToken, DbConnection connection, params TModel[] entity);
        Task<IEnumerable<TModel>> UpdateAsync(CancellationToken cancellationToken, DbConnection connection, params TModel[] entity);
        Task<bool> RemoveAsync(DbConnection connection, params TModel[] entity);
        Task<bool> RemoveAsync(DbConnection connection, params TKey[] id);

        Task<TModel> GetByIdAsync(CancellationToken cancellationToken, DbConnection connection, TKey id, params Expression<Func<TModel, object>>[] navigationProperties);
        Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken, DbConnection connection, params Expression<Func<TModel, object>>[] navigationProperties);
        Task<IEnumerable<TModel>> GetWithFilterAsync(CancellationToken cancellationToken, DbConnection connection, Expression<Func<TModel, bool>> where, params Expression<Func<TModel, object>>[] navigationProperties);
    }
}
