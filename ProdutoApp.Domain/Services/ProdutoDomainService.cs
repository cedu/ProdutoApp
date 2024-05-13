using ProdutoApp.Domain.Entities;
using ProdutoApp.Domain.Exceptions;
using ProdutoApp.Domain.Interfaces.Repositories;
using ProdutoApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoApp.Domain.Services
{
    public class ProdutoDomainService : IProdutoDomainService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoDomainService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Produto Adicionar(Produto produto)
        {
            produto.Id = Guid.NewGuid();
            produto.CadastradoEm = DateTime.Now;
            produto.UltimaAtualizacaoEm = DateTime.Now;
            produto.Ativo = true;

            _produtoRepository.Add(produto);

            return produto;
        }

        public Produto Atualizar(Produto produto)
        {
            var produtoEdicao = _produtoRepository.GetById(produto.Id.Value);
            DomainException.When(produtoEdicao == null, "O produto é inválido para edição. Por favor, verifique o ID do produto");

            produtoEdicao.Nome = produto.Nome;
            produtoEdicao.Descricao = produto.Descricao;
            produtoEdicao.DataHora = produto.DataHora;
            produtoEdicao.Preco = produto.Preco;
            produtoEdicao.Quantidade = produto.Quantidade;
            produtoEdicao.Estado = produto.Estado;
            produtoEdicao.UltimaAtualizacaoEm = DateTime.Now;

            _produtoRepository.Update(produtoEdicao);

            return produtoEdicao;
        }

        public Produto Excluir(Guid id)
        {
            var produtoExclusao = _produtoRepository.GetById(id);
            DomainException.When(produtoExclusao == null, "O produto é inválido para exclusão. Por favor, verifique o ID do produto");

            _produtoRepository.Delete(produtoExclusao);
            produtoExclusao.ExcluidoEm = DateTime.Now;
            return produtoExclusao;
        }

        public List<Produto> Consultar(DateTime dataMin, DateTime dataMax)
        {
            return _produtoRepository.Get(dataMin, dataMax);
        }

        public Produto? ObterPorId(Guid id)
        {
            return _produtoRepository.GetById(id);
        }
    }
}
