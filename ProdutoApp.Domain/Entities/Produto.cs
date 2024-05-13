using ProdutoApp.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoApp.Domain.Entities
{
    public class Produto
    {
        #region Propriedades

        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataHora { get; set; }
        public double? Preco { get; set; }
        public int? Quantidade { get; set; }
        public EstadoProduto? Estado { get; set; }
        public DateTime? CadastradoEm { get; set; }
        public DateTime? UltimaAtualizacaoEm { get; set; }
        public DateTime? ExcluidoEm { get; set; }
        public bool? Ativo { get; set; }

        #endregion
    }
}
