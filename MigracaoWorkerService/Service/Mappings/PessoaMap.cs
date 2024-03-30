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
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("PESSOA");

            builder.Property(x => x.Id).HasColumnName("PESS_ID");

            builder.HasKey(x => x.Id).HasName("PK_PESS");

            builder.Property(x => x.Observacao).HasColumnName("PESS_OBSERVACAO").IsRequired(false);

            builder.Property(x => x.Celular).HasColumnName("PESS_CELULAR").IsRequired(false);

            builder.Property(x => x.DataAlteracao).HasColumnName("PESS_DATA_ALTERACAO").IsRequired(false);

            builder.Property(x => x.DataCadastro).HasColumnName("PESS_DATA_CADASTRO");

            builder.Property(x => x.EhEstrangeiro).HasColumnName("PESS_ESTRANGEIRO").IsRequired(false);

            builder.Property(x => x.Telefone).HasColumnName("PESS_TELEFONE").IsRequired(false);

            builder.Property(x => x.Email).HasColumnName("PESS_EMAIL").IsRequired(false);

            builder.Property(x => x.IdUsuario).HasColumnName("PESS_ID_USUARIO"); 

            // builder.Navigation(x => x.Funcionario).AutoInclude();

            builder.HasOne(x => x.Usuario)
                .WithOne(x => x.Pessoa)
                .HasForeignKey<Pessoa>(x => x.IdUsuario);

            builder.HasOne(x => x.UnidadeVenda)
              .WithOne(x => x.Pessoa)
              .HasForeignKey<UnidadeVenda>(k => k.IdPessoa);


            builder.Ignore(x => x.Fornecedor);
           // builder.Ignore(x => x.Endereco);
            builder.Ignore(x => x.Cliente);
           // builder.Ignore(x => x.UnidadeVenda);
           // builder.Ignore(x => x.Usuario);
           // builder.Ignore(x => x.Funcionario);
            builder.Ignore(x => x.Empresa);
        }
    }
}
