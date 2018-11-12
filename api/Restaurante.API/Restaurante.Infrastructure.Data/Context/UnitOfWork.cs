using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Domain;
using Restaurante.Domain.Entity;
using Restaurante.Infrastructure.Context;

namespace Restaurante.Infrastructure.Data
{    
    /// <summary>
    /// Controle de transações do banco
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IRestauranteContext _context;
        public DbLoggerCategory.Database.Transaction Transaction { get; set; }

        public UnitOfWork(IRestauranteContext context)
        {
            _context = context;
        }
        
        public IRestauranteContext GetContext()
        {
            return _context;
        }

        /// <summary>
        /// Retorna um dbset do model 
        /// </summary>
        /// <typeparam name="TKey">Tipo da chave</typeparam>
        /// <typeparam name="TModel">Classe da model</typeparam>
        /// <param name="navigationProperties">Relacionamentos que serão carregados</param>
        /// <returns>Lista com dados do banco</returns>
        public IQueryable<TModel> GetRepository<TKey, TModel>(params Expression<Func<TModel, object>>[] navigationProperties) 
            where TModel : EntityBase<TKey>
            where TKey: IEquatable<TKey>
        {
            var db = _context.context.Set<TModel>().AsQueryable();
            if (navigationProperties != null)
            {
                foreach (Expression<Func<TModel, object>> navigationProperty in navigationProperties)
                {
                    var n = _context.context.Model.FindEntityType(typeof(TModel)).GetNavigations().FirstOrDefault(a => a.Name == navigationProperty.Body.Type.Name);
                    if (n != null)
                        db = db.Include(n.Name);
                }
            }
            return db;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            await _context.context.Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            _context.context.Database.CommitTransaction();
        }

        public void Rollback()
        {
            _context.context.Database.RollbackTransaction();
        }

        public void Dispose()
        {
            GC.Collect();
        }

        public int SaveChanges()
        {
            return _context.context.SaveChanges();
        }

        public async Task SaveChangesASync()
        {
            await _context.context.SaveChangesAsync();
        }
                
        public static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<RestauranteContext>())
                {
                    context.Database.Migrate();
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
