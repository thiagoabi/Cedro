using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Restaurante.Domain.Entity;
using Restaurante.Infrastructure.Repository;
using Restaurante.Infrastructure.Store;
using System;
using System.Threading;

namespace Restaurante.Appication
{
    public class EstabelecimentoManager : ManagerBase<long, Estabelecimento>, IEstabelecimentoManager
    {
        public EstabelecimentoManager(
            ILogger<EstabelecimentoManager> logger,
            IEstabelecimentoStore store,
            IHttpContextAccessor contextAccessor)
            : base(logger, store, contextAccessor)
        {

        }
    }
}
