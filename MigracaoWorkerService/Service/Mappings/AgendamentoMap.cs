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
    public class AgendamentoMap : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("AGENDAMENTO");

            builder.Property(x => x.Id).HasColumnName("AGMT_ID");

            builder.HasKey(x => x.Id).HasName("PK_AGMT");

            builder.Property(x => x.Status).HasColumnName("AGMT_STATUS");

            builder.Property(x => x.IdAtendente).HasColumnName("AGMT_ID_ATENDENTE");

            builder.Property(x => x.IdCliente).HasColumnName("AGMT_ID_CLIENTE");

            builder.Property(x => x.IdResponsavelServico).HasColumnName("AGMT_ID_RESP_SERVICO");

            builder.Property(x => x.DataFinal).HasColumnName("AGMT_DATA_FINAL");

            builder.Property(x => x.DataInicio).HasColumnName("AGMT_DATA_INICIO");

            builder.Property(x => x.DiaInteiro).HasColumnName("AGMT_DIA_INTEIRO");

            builder.Property(x => x.VisitaEmCasa).HasColumnName("AGMT_VISITA_EM_CASA");

            builder.Property(x => x.IdUnidadeVenda).HasColumnName("AGMT_ID_UNIDADEVENDA");


            builder.HasMany(x => x.Servico)
                 .WithOne(x => x.Agendamento)
                 .HasForeignKey(f => f.IdAgendamento);

            builder.HasOne(x => x.Atendente)
                 .WithMany(x => x.Atendentes)
                 .HasForeignKey(f => f.IdAtendente).IsRequired(false);

            //builder.HasOne(x => x.Cliente)
            //  .WithMany(x => x.Agendamentos)
            //  .HasForeignKey(f => f.IdCliente).IsRequired(false);
            //.OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.ResponsavelServico)
              .WithMany(x => x.ResponsaveisServico)
              .HasForeignKey(f => f.IdResponsavelServico).IsRequired(false);

            //builder.HasOne(x => x.UnidadeVenda)
            //        .WithMany(a => a.Agendamentos)
            //        .HasForeignKey(f => f.IdUnidadeVenda).IsRequired(false);

            builder.Ignore(x => x.ResponsavelServico);
            builder.Ignore(x => x.Atendente);
            builder.Ignore(x => x.Cliente);
            builder.Ignore(x => x.Servico);
            builder.Ignore(x => x.UnidadeVenda);
        }
    }
}
