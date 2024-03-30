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
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("ENDERECO");

            builder.Property(x => x.Id).HasColumnName("ENDE_ID");

            builder.HasKey(x => x.Id).HasName("PK_ENDE");

            builder.Property(x => x.Bairro).HasColumnName("ENDE_BAIRRO");

            builder.Property(x => x.Complemento).HasColumnName("ENDE_COMPLEMENTO").IsRequired(false);

            builder.Property(x => x.Logradouro).HasColumnName("ENDE_LOGRADOURO");

            builder.Property(x => x.Numero).HasColumnName("ENDE_NUMERO").IsRequired(false);

            builder.Property(x => x.IdCidade).HasColumnName("ENDE_ID_CIDADE");

            builder.Property(x => x.Cep).HasColumnName("ENDE_CEP").IsRequired(false);

            //builder.HasOne(x => x.Cidade)
            //       .WithMany(x => x.Endereco)
            //       .HasForeignKey(x => x.IdCidade);

            builder.Property(x => x.IdPessoa).HasColumnName("ENDE_ID_PESSOA");

            builder.HasOne(x => x.Pessoa)
                   .WithOne(x => x.Endereco)
                   .HasForeignKey<Endereco>(x => x.IdPessoa);

            builder.Ignore(x => x.Cidade);
            //builder.Ignore(X => X.Pessoa);
        }
    }
}
