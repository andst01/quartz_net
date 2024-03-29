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
    public class AgendaMap : IEntityTypeConfiguration<Agenda>
    {
        public void Configure(EntityTypeBuilder<Agenda> builder)
        {
            builder.ToTable("AGENDA");

            builder.Property(x => x.Id).HasColumnName("AGDA_ID");

            builder.HasKey(x => x.Id).HasName("PK_AGDA");

            builder.Property(x => x.Observacao).HasColumnName("AGDA_OBSERVACAO");

            builder.Property(c => c.IdMotivo).HasColumnName("AGDA_ID_MOTIVO");

            builder.HasOne(x => x.Motivo)
                .WithMany(m => m.Agenda)
                .HasForeignKey(x => x.IdMotivo);

            builder.Property(c => c.IdFuncionario).HasColumnName("AGDA_ID_FUNCIONARIO");

            builder.HasOne(x => x.Funcionario)
                .WithMany(x => x.Agendas)
                .HasForeignKey(x => x.IdFuncionario);


            builder.Property(x => x.IdUnidadeVenda).HasColumnName("AGDA_ID_UNIDADEVENDA");

            //builder.HasOne(x => x.UnidadeVenda)
            //    .WithMany(u => u.Agendas)
            //    .HasForeignKey(x => x.IdUnidadeVenda);

            /// Atendente pode estar presente na unidade de venda a unidade de venda é preenchida se a preseça for preenchida
            builder.Property(x => x.Presenca).HasColumnName("AGDA_PRESENCA");

            builder.Property(x => x.DataFim).HasColumnName("AGDA_DATA_FIM");

            builder.Property(x => x.DataInicio).HasColumnName("AGDA_DATA_INICIO");

            builder.Ignore(x => x.Motivo);
            builder.Ignore(x => x.UnidadeVenda);

        }
    }
}
