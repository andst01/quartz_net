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
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("CLIENTE");

            builder.Property(x => x.Id).HasColumnName("CLIE_ID");

            builder.HasKey(x => x.Id).HasName("PK_CLIE");

            builder.Property(x => x.IdPessoa).HasColumnName("CLIE_ID_PESSOA");

            builder.Property(x => x.CPF).HasColumnName("CLIE_CPF");

            builder.Property(x => x.DataNascimento).HasColumnName("CLIE_DATA_NASCIMENTO");

            builder.Property(x => x.Nome).HasColumnName("CLIE_NOME");

            builder.Property(x => x.RG).HasColumnName("CLIE_RG");

            builder.HasOne(x => x.Pessoa)
                   .WithOne(x => x.Cliente)
                   .HasForeignKey<Cliente>(f => f.IdPessoa);

            //builder.HasMany(x => x.Vendas)
            //     .WithOne(x => x.Cliente)
            //     .HasForeignKey(f => f.IdCliente);

            //builder.Ignore(x => x.Pessoa);
            //builder.Ignore(x => x.Agendamentos);
            //builder.Ignore(x => x.Vendas);
        }
    }
}
