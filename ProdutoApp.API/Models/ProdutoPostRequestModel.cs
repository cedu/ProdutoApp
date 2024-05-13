using System.ComponentModel.DataAnnotations;

namespace ProdutoApp.API.Models
{
    public class ProdutoPostRequestModel
    {
        [MinLength(8, ErrorMessage = "Por favor,informe no minimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Por favor,informe no máximo {1} caracteres.")]
        [Required(ErrorMessage ="Por favor, informe o nome do produto.")]
        public string? Nome { get; set; }

        [MinLength(8, ErrorMessage = "Por favor,informe no minimo {1} caracteres.")]
        [MaxLength(250, ErrorMessage = "Por favor,informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a descrição do produto.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data e hora da tarefa.")]
        public DateTime? DataHora { get; set; }

        [Required(ErrorMessage = "Por favor, informe o preço do produto.")]
        public double? Preco { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade do produto.")]
        public int? Quantidade { get; set; }

        [Required(ErrorMessage = "Por favor, informe o estado do produto.")]
        public int? Estado { get; set; }
    }
}
