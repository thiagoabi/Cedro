using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.Domain
{
    public interface IEntityBase<TKey>
        where TKey: IEquatable<TKey>
    {
        TKey Id { get; set; }
        string CodigoInterno { get; set; }
        DateTime DataInclusao { get; set; }
        string UsuarioInclusao { get; set; }
        DateTime? DataUltimaAlteracao { get; set; }
        string UsuarioUltimaAlteracao { get; set; }
    }
}
