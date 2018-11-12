using Restaurante.Infrastructure.CrossCutti;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Appication.TO
{
    public class PratoDTO : IApiModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public long EstabelecimentoId { get; set; }
        public string NomeEstabelecimento { get; set; }
    }
}
