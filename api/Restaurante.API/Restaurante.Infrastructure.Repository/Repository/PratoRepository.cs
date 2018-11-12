using Microsoft.Extensions.Logging;
using Restaurante.Domain.Entity;
using Restaurante.Infrastructure.Data;
using System;

namespace Restaurante.Infrastructure.Repository
{
    public class PratoRepository : RepositoryBase<long, Prato>, IPratoRepository
    {
        public PratoRepository(IUnitOfWork uow, ILogger<PratoRepository> logger) 
            : base(uow, logger)
        {
        }
    }
}
