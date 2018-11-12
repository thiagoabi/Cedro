﻿using Restaurante.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Infrastructure.Repository
{
    public interface IPratoRepository : IRepositoryBase<long, Prato>
    {
    }
}