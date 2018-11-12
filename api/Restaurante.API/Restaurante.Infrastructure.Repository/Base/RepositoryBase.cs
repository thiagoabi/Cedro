using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurante.Domain;
using Restaurante.Domain.Entity;
using Restaurante.Infrastructure.Context;
using Restaurante.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infrastructure.Repository
{
    public class RepositoryBase<TKey, TModel> : IRepositoryBase<TKey, TModel>
        where TKey : IEquatable<TKey>
        where TModel : EntityBase<TKey>
    {
        private readonly IUnitOfWork _uow;

        public RepositoryBase(IUnitOfWork uow, ILogger<RepositoryBase<TKey, TModel>> logger)
        {
            _uow = uow;
        }

        /// <summary>
        /// Retorna todos os registros da tabela
        /// </summary>
        public async Task<IEnumerable<TModel>> GetAllAsync(CancellationToken cancellationToken, DbConnection connection, params Expression<Func<TModel, object>>[] navigationProperties)
        {
            var func = new Func<DbConnection, Task<IEnumerable<TModel>>>(async (_conn) =>
            {
                var result = new List<Prato>();
                var db = _uow.GetRepository<TKey, TModel>(navigationProperties);
               
                return await db.ToListAsync<TModel>();
            });
            return await func(connection);
        }

        /// <summary>
        /// Retorna um registro da tabela pelo ID
        /// </summary>
        public async Task<TModel> GetByIdAsync(CancellationToken cancellationToken, DbConnection connection, TKey id, params Expression<Func<TModel, object>>[] navigationProperties)
        {
            var func = new Func<DbConnection, Task<TModel>>(async (_conn) =>
            {
                var result = new List<Prato>();
                var db = _uow.GetRepository<TKey, TModel>(navigationProperties);

                return await db.FirstOrDefaultAsync(a => a.Id.Equals(id));
            });
            return await func(connection);
        }

        /// <summary>
        /// Buscar os registros da tabela de acordo com o filtro
        /// </summary>
        public async Task<IEnumerable<TModel>> GetWithFilterAsync(CancellationToken cancellationToken, DbConnection connection, Expression<Func<TModel, bool>> where, params Expression<Func<TModel, object>>[] navigationProperties)
        {
            var func = new Func<DbConnection, Task<IEnumerable<TModel>>>(async (_conn) =>
            {
                var result = new List<Prato>();
                var db = _uow.GetRepository<TKey, TModel>(navigationProperties);

                return await db.Where(where)?.ToListAsync<TModel>();
            });
            return await func(connection);
        }

        /// <summary>
        /// Remove registros da tabela por entity
        /// </summary>
        public Task<bool> RemoveAsync(DbConnection connection, params TModel[] entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Remove registros da tabela por id
        /// </summary>
        public async Task<bool> RemoveAsync(DbConnection connection, params TKey[] id)
        {
            var func = new Func<DbConnection, Task<bool>>(async (_conn) =>
            {
                foreach (TKey i in id)
                {
                    var entity = _uow.GetRepository<TKey, TModel>().FirstOrDefault(a => a.Id.Equals(i));
                    if (entity != null)
                    {
                        _uow.GetContext().context.Entry(entity).State = EntityState.Deleted;
                    }
                }
                await _uow.SaveChangesASync();
                return await Task.FromResult(true);
            });
            return await func(connection);
        }

        /// <summary>
        /// Cria registros na tabela
        /// </summary>
        public async Task<IEnumerable<TModel>> CreateAsync(CancellationToken cancellationToken, DbConnection connection, params TModel[] entity)
        {
            var func = new Func<DbConnection, Task<IEnumerable<TModel>>>(async (_conn) =>
            {
                var result = new List<TModel>();
                foreach (TModel e in entity)
                {
                    if (e != null && !e.Id.Equals(0))
                    {
                        _uow.GetContext().context.Entry(e).State = EntityState.Added;
                        result.Add(e);
                    }
                }
                await _uow.SaveChangesASync();
                return await Task.FromResult(result);
            });
            return await func(connection);
        }

        /// <summary>
        /// Atualiza registros na tabela por entity
        /// </summary>
        public async Task<IEnumerable<TModel>> UpdateAsync(CancellationToken cancellationToken, DbConnection connection, params TModel[] entity)
        { 
            var func = new Func<DbConnection, Task<IEnumerable<TModel>>>(async (_conn) =>
            {

                var result = new List<TModel>();
                foreach (TModel e in entity)
                {
                    if (e != null && !e.Id.Equals(0))
                    {
                        _uow.GetContext().context.Entry(e).State = EntityState.Modified;
                        result.Add(e);
                    }
                }
                await _uow.SaveChangesASync();
                return await Task.FromResult(result);
            });
            return await func(connection);
        }
    }
}
