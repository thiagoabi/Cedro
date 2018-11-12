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
    public interface IRestauranteContext
    {
        DbContext context { get; }

        DbConnection Connection();
        DbSet<Estabelecimento> Restaurantes { get; set; }
        DbSet<Prato> Pratos { get; set; }
    }
}
