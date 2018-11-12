using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurante.Domain.Entity
{
    public class Estabelecimento : EntityBase<long>
    {
        public Estabelecimento(string nome) {
            Nome = nome;
        }

        private Estabelecimento() { }

        [StringLength(100, MinimumLength = 6)]
        public string Nome { get; set; }

        public virtual IEnumerable<Prato> Pratos { get; set; }
    }
}
