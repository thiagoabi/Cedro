using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Restaurante.Domain.Entity
{
    public class Prato : EntityBase<long>
    {
        public Prato(string nome, double valor)
        {
            Nome = nome;
            Valor = valor;
        }

        [ForeignKey("FK_Prato_Estabelecimento")]
        public long EstabelecimentoId { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string Nome { get; set; }
           
        public double Valor { get; set; }

        public virtual Estabelecimento Estabelecimento { get; set; }
        public virtual string DTO_NomeEstabelecimento => Estabelecimento == null ? string.Empty : Estabelecimento.Nome;
    }
}
