using Restaurante.Infrastructure.CrossCutti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Appication.TO
{
    public class EstabelecimentoDTO : IApiModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
