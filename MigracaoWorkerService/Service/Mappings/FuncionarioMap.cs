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
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.ToTable("FUNCIONARIO");

            builder.Property(x => x.Id).HasColumnName("FNCR_ID");

            builder.HasKey(x => x.Id).HasName("PK_FNCR");

            builder.Property(x => x.CPF).HasColumnName("FNCR_CPF");

            builder.Property(x => x.DataNascimento).HasColumnName("FNCR_DATA_NASCIMENTO");

            builder.Property(x => x.Nome).HasColumnName("FNCR_NOME");

            builder.Property(x => x.RG).HasColumnName("FNCR_RG");

            builder.Property(x => x.IdPessoa).HasColumnName("FNCR_ID_PESSOA");

            //  builder.Navigation(x => x.Pessoa).AutoInclude();

            builder.HasOne(x => x.Pessoa)
                .WithOne(x => x.Funcionario)
                .HasForeignKey<Funcionario>(x => x.IdPessoa);



            builder.Ignore(x => x.Atendentes);
            //builder.Ignore(x => x.Pessoa);
            builder.Ignore(x => x.CargosFuncionario);
            builder.Ignore(x => x.ResponsaveisServico);
            //builder.Ignore(x => x.Vendas);

        }
    }
}
