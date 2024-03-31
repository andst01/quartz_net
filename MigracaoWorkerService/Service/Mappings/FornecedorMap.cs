using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MigracaoWorkerService.Service.Models.Execute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigracaoWorkerService.Service.Mappings
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("FORNECEDOR");

            builder.Property(x => x.Id).HasColumnName("FORN_ID");

            builder.HasKey(x => x.Id).HasName("PK_FORN");

            builder.Property(x => x.CNPJ).HasColumnName("FORN_CNPJ");

            builder.Property(x => x.NomeFantasia).HasColumnName("FORN_NOME_FANTASIA");

            builder.Property(x => x.RazaoSocial).HasColumnName("FORN_RAZAO_SOCIAL");

            builder.Property(x => x.IdPessoa).HasColumnName("FORN_ID_PESSOA");

            builder.HasOne(x => x.Pessoa)
                .WithOne(f => f.Fornecedor)
                .HasForeignKey<Fornecedor>(x => x.IdPessoa);



           // builder.Ignore(x => x.Pessoa);
        }
    }
}
