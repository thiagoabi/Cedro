using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infrastructure.Context
{
    public class RestauranteContext : DbContext, IRestauranteContext
    {
        public DbContext context => this;
        
        public RestauranteContext() { }


        public RestauranteContext(DbContextOptions<RestauranteContext> options)
            : base(options) { }

        //TODO: Não utilizado até o momento
        public DbSet<Estabelecimento> Restaurantes { get; set; }
        public DbSet<Prato> Pratos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prato>()
                .HasOne(a => a.Estabelecimento)
                .WithMany(b => b.Pratos)
                .IsRequired()
                .HasForeignKey(p => p.EstabelecimentoId)                
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public DbConnection Connection()
        {
            try
            {
                 return Database.GetDbConnection();
            }
            catch (AggregateException)
            {
                throw new OperationCanceledException("Ocorreu um erro ao buscar conexão com o banco");
            }
            finally
            {
            }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await SaveChangesAsync();
        }
    }
}
