using ProdutoApp.Domain.Entities;
using ProdutoApp.Domain.Interfaces.Repositories;
using ProdutoApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoApp.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        public void Add(Produto produto)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(produto);
                dataContext.SaveChanges();
            }
        }

        public void Update(Produto produto)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(produto);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Produto produto)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Remove(produto);
                dataContext.SaveChanges();
            }
        }

        public List<Produto> Get(DateTime dataMin, DateTime dataMax)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Produto>() //entidade
                    .Where(t => t.DataHora >= dataMin && t.DataHora <= dataMax) //filtro
                    .OrderByDescending(t => t.DataHora) //ordenação
                    .ToList();
            }
        }

        public Produto? GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Produto>().Find(id);
            }
        }
    }
}
