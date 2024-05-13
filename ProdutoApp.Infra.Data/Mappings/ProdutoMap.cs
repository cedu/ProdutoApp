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
            builder.ToTable("PRODUTO");

            //Chave Primária
            builder.HasKey(p => p.Id);

            //mapeamento da entidade
            builder.Property(p => p.Id).HasColumnName("ID");
            builder.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
            builder.Property(p => p.Descricao).HasColumnName("DESCRICAO").HasMaxLength(250).IsRequired();
            builder.Property(p => p.DataHora).HasColumnName("DATAHORA").IsRequired();
            builder.Property(p => p.Preco).HasColumnName("PRECO").HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(p => p.Quantidade).HasColumnName("QUANTIDADE").IsRequired();
            builder.Property(p => p.Estado).HasColumnName("ESTADO").IsRequired();
            builder.Property(p => p.CadastradoEm).HasColumnName("CADASTRADOEM").IsRequired();
            builder.Property(p => p.UltimaAtualizacaoEm).HasColumnName("ULTIMAATUALIZACAOEM").IsRequired();
            builder.Property(p => p.Ativo).HasColumnName("ATIVO").IsRequired();
        }
    }
}
