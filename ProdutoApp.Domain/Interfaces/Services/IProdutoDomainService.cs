using ProdutoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoApp.Domain.Interfaces.Services
{
    public interface IProdutoDomainService
    {
        Produto Adicionar(Produto produto);
        Produto Atualizar(Produto produto);
        Produto Excluir(Guid id);

        List<Produto> Consultar(DateTime dataMin, DateTime dataMax);
        Produto? ObterPorId(Guid id);
    }
}
