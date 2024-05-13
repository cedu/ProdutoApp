using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoApp.Domain.Entities.Enums
{
    public enum EstadoProduto
    {
        Esgotado = 0,
        EmEstoque = 1,
        Encomendado = 2,
        EmTransito = 3
    }
}
