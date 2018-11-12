using Microsoft.Extensions.Logging;
using Restaurante.Domain.Entity;
using Restaurante.Infrastructure.Data;
using System;

namespace Restaurante.Infrastructure.Repository
{
    public class EstabelecimentoRepository : RepositoryBase<long, Estabelecimento>, IEstabelecimentoRepository
    {
        public EstabelecimentoRepository(IUnitOfWork uow, ILogger<EstabelecimentoRepository> logger) 
            : base(uow, logger)
        {
        }
    }
}
