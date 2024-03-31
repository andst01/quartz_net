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
    public class CargoMap : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.ToTable("CARGO");

            builder.Property(x => x.Id).HasColumnName("CARG_ID");

            builder.HasKey(x => x.Id).HasName("PK_CARG");

            builder.Property(x => x.Ativo).HasColumnName("CARG_ATIVO");

            builder.Property(x => x.Descricao).HasColumnName("CARG_DESCRICAO");

            builder.HasMany(x => x.CargosFuncionario)
                    .WithOne(m => m.Cargo)
                    .HasForeignKey(x => x.IdCargo)
                    .OnDelete(DeleteBehavior.Restrict);

           // builder.Ignore(x => x.CargosFuncionario);

        }
    }
}
