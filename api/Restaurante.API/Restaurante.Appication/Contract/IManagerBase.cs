using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Appication
{
    public interface IManagerBase<TKey, TModel>
        where TKey : IEquatable<TKey>
        where TModel : class
    {
        Task<IEnumerable<TModel>> CreateAsync(params TModel[] entity);
        Task<IEnumerable<TModel>> UpdateAsync(params TModel[] entity);
        Task<bool> RemoveAsync(params TModel[] entity);
        Task<bool> RemoveAsync(params TKey[] id);

        Task<TModel> GetByIdAsync(TKey id, params Expression<Func<TModel, object>>[] navigationProperties);
        Task<IEnumerable<TModel>> GetAllAsync(params Expression<Func<TModel, object>>[] navigationProperties);
        Task<IEnumerable<TModel>> GetWithFilterAsync(Expression<Func<TModel, bool>> where, params Expression<Func<TModel, object>>[] navigationProperties);

    }
}
