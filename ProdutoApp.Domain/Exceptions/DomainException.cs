using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoApp.Domain.Exceptions
{
    public class DomainException : Exception
    {
        //método construtor
        public DomainException(string errorMessage) : base(errorMessage)
        {

        }

        public static void When(bool hasError, string errorMessage)
        {
            if (hasError)
                throw new DomainException(errorMessage);
        }
    }
}
