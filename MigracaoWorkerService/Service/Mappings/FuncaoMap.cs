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
    public class FuncaoMap : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
            builder.ToTable("FUNCAO");

            builder.Property(x => x.Id).HasColumnName("FUNC_ID");

            builder.HasKey(x => x.Id).HasName("PK_FUNC");

            builder.Property(x => x.ConcurrencyStamp).HasColumnName("FUNC_CONCURRENCY_TEMP");

            builder.Property(x => x.Name).HasColumnName("FUNC_NOME");

            builder.Property(x => x.NormalizedName).HasColumnName("FUNC_NORMALIZED_NAME");
        }
    }
}
