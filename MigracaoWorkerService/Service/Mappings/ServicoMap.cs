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
    public class ServicoMap : IEntityTypeConfiguration<Servico>
    {
        public void Configure(EntityTypeBuilder<Servico> builder)
        {
            builder.ToTable("SERVICO");

            builder.Property(x => x.Id).HasColumnName("SERV_ID");

            builder.HasKey(x => x.Id).HasName("PK_SERV");

            builder.Property(x => x.CodigoServico).HasColumnName("SERV_COD_SERVICO");

            builder.Property(x => x.DataCadastro).HasColumnName("SERV_DATA_CADASTRO");

            builder.Property(x => x.QuantidadeParcelas).HasColumnName("SERV_QTD_PARCELAS");

            builder.Property(x => x.ValorTotal).HasColumnType("decimal(10,2)").HasColumnName("SERV_VL_TOTAL");

            builder.Property(x => x.IdAgendamento).HasColumnName("SERV_ID_AGENDAMENTO");

            builder.Property(x => x.IdUsuarioCriacao).HasColumnName("SERV_ID_USUARIO");

            builder.Property(x => x.IdUsuarioResponsavel).HasColumnName("SERV_ID_RESPONSAVEL");



            builder.HasOne(x => x.UsuarioCriacao)
                   .WithMany(x => x.ServicoCriacao)
                   .HasForeignKey(f => f.IdUsuarioCriacao).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.UsuarioResponsavel)
                   .WithMany(x => x.ServicoResponsavel)
                   .HasForeignKey(x => x.IdUsuarioResponsavel).OnDelete(DeleteBehavior.Restrict);


            builder.Ignore(x => x.Agendamento);
            builder.Ignore(x => x.UsuarioCriacao);
            builder.Ignore(x => x.UsuarioResponsavel);
        }
    }
}
