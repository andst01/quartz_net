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
    public class FuncaoUsuarioMap : IEntityTypeConfiguration<FuncaoUsuario>
    {
        public void Configure(EntityTypeBuilder<FuncaoUsuario> builder)
        {
            builder.ToTable("FUNCAO_USUARIO");

            builder.Property(x => x.RoleId).HasColumnName("FNUS_ID_FUNCAO");

            builder.Property(x => x.UserId).HasColumnName("FNUS_ID_USUARIO");

            builder.HasNoKey();

            builder.HasKey(x => new { x.RoleId, x.UserId }).HasName("PK_FNUS");

            builder.Property(x => x.DataInicio).HasColumnName("FUNC_DT_INICIO");

            builder.Property(x => x.DataFim).HasColumnName("FUNC_DT_FIM");
        }
    }
}
