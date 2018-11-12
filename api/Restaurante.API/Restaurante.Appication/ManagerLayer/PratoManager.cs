using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Restaurante.Domain.Entity;
using Restaurante.Infrastructure.Repository;
using Restaurante.Infrastructure.Store;
using System;
using System.Threading;

namespace Restaurante.Appication
{
    public class PratoManager : ManagerBase<long, Prato>, IPratoManager
    {
        public PratoManager(
            ILogger<PratoManager> logger,
            IPratoStore store,
            IHttpContextAccessor contextAccessor)
            : base(logger, store, contextAccessor)
        {

        }
    }
}
