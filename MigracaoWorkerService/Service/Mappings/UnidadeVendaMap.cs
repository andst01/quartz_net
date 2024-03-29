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
    public class UnidadeVendaMap : IEntityTypeConfiguration<UnidadeVenda>
    {
        public void Configure(EntityTypeBuilder<UnidadeVenda> builder)
        {
            builder.ToTable("UNIDADE_VENDA");

            builder.Property(x => x.Id).HasColumnName("UNVD_ID");

            builder.HasKey(x => x.Id).HasName("PK_UNVD");

            builder.Property(x => x.NomeFantasia).HasColumnName("UNVD_NOME_FANTASIA");

            builder.Property(x => x.RazaoSocial).HasColumnName("UNVD_RAZAO_SOCIAL");

            builder.Property(x => x.IdEmpresa).HasColumnName("UNVD_ID_EMPRESA");

            builder.Property(x => x.IdPessoa).HasColumnName("UNVD_ID_PESSOA");

            builder.Property(x => x.CNPJ).HasColumnName("UNVD_CNPJ");


            //builder.HasMany(x => x.Vendas)
            //     .WithOne(x => x.UnidadeVenda)
            //     .HasForeignKey(x => x.IdUnidadeVenda);




            builder.Ignore(x => x.Agendas);
            builder.Ignore(x => x.Agendamentos);
            builder.Ignore(x => x.Empresa);
            builder.Ignore(x => x.Pessoa);
            //builder.Ignore(x => x.Vendas);
        }
    }
}
