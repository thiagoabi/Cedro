using Restaurante.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infrastructure.Store
{
    public interface IEstabelecimentoStore : IStoreBase<long, Estabelecimento>
    {
    }
}
