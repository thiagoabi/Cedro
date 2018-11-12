using System.ComponentModel.DataAnnotations;

namespace web
{
    public class Prato
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public int EstabelecimentoId { get; set; }
        public string NomeEstabelecimento { get; set; }
    }
}