namespace ProdutoApp.API.Models
{
    public class ProdutoGetResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public double? Preco { get; set; }
        public int? Quantidade { get; set; }
        public int? Estado { get; set; }
        public DateTime? CadastradoEm { get; set; }
        public DateTime? UltimaAtualizacaoEm { get; set; }
        public DateTime? ExcluidoEm { get; set; }
        public int? Ativo { get; set; }

    }
}
