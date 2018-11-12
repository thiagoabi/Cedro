using Microsoft.Extensions.Logging;
using Restaurante.Domain.Entity;
using Restaurante.Infrastructure.Context;
using Restaurante.Infrastructure.Data;
using Restaurante.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infrastructure.Store
{
    public class PratoStore : StoreBase<long, Prato>, IPratoStore
    {
        public PratoStore(IPratoRepository repository, ILogger<PratoStore> logger, IRestauranteContext context, IUnitOfWork uow)
            : base(repository, logger, context, uow)
        {  }
    }
}
