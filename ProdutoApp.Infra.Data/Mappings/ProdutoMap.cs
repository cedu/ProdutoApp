using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoApp.Infra.Data.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            //Chave Primária
            builder.HasKey(p => p.Id);

            //mapeamento da entidade
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(250).IsRequired();
            builder.Property(p => p.DataHora).IsRequired();
            builder.Property(p => p.Preco).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(p => p.Quantidade).IsRequired();
            builder.Property(p => p.Estado).IsRequired();
            builder.Property(p => p.CadastradoEm).IsRequired();
            builder.Property(p => p.UltimaAtualizacaoEm).IsRequired();
            builder.Property(p => p.Ativo).IsRequired();
        }
    }
}
