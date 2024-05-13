using ProdutoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoApp.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        void Add(Produto produto);
        void Update(Produto produto);
        void Delete(Produto produto);

        List<Produto> Get(DateTime dataMin, DateTime dataMax);
        Produto? GetById(Guid id);
    }
}
