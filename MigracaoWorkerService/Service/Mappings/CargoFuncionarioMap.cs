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
    public class CargoFuncionarioMap : IEntityTypeConfiguration<CargoFuncionario>
    {
        public void Configure(EntityTypeBuilder<CargoFuncionario> builder)
        {
            builder.ToTable("CARGO_FUNCIONARIO");

            builder.Property(x => x.Id).HasColumnName("CGFN_ID");

            builder.HasKey(x => x.Id).HasName("PK_CGFN");

            builder.Property(x => x.IdCargo).HasColumnName("CGFN_ID_CARGO");

            builder.Property(x => x.IdFuncionario).HasColumnName("CGFN_ID_FUNCIONARIO");

            builder.Property(x => x.DataFim).HasColumnName("CGFN_DATA_FIM");

            builder.Property(x => x.DataInicio).HasColumnName("CGFN_DATA_INICIO");

            builder.HasOne(x => x.Funcionario)
                .WithMany(x => x.CargosFuncionario)
                .HasForeignKey(x => x.IdFuncionario);

            //builder.HasOne(x => x.Cargo)
            //   .WithMany(x => x.CargosFuncionario)
            //   .HasForeignKey(x => x.IdCargo);

           // builder.Ignore(x => x.Cargo);
           // builder.Ignore(x => x.Funcionario);
        }
    }
}
