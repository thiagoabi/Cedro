using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurante.Domain.Entity
{
    public class EntityBase<TKey> : IEntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        public TKey Id { get; set; }
        public string CodigoInterno { get; set; }
        public DateTime DataInclusao { get; set; }
        public string UsuarioInclusao { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
        public string UsuarioUltimaAlteracao { get; set; }
    }
}
