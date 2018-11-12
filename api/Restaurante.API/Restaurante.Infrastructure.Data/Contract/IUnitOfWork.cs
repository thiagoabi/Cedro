using Microsoft.EntityFrameworkCore;
using Restaurante.Domain;
using Restaurante.Domain.Entity;
using Restaurante.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Restaurante.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        Transaction Transaction { get; set; }

        IRestauranteContext GetContext();
        IQueryable<TModel> GetRepository<TKey, TModel>(params Expression<Func<TModel, object>>[] navigationProperties)
            where TModel: EntityBase<TKey> where TKey: IEquatable<TKey>;        
        int SaveChanges();
        Task SaveChangesASync();
        void Commit();
        void Rollback();
        Task BeginTransactionAsync(CancellationToken cancellationToken);
    }
}
