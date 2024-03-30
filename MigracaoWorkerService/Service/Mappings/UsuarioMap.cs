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
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder.Property(x => x.Id).HasColumnName("USUA_ID");

            builder.HasKey(x => x.Id).HasName("PK_USUA");

            builder.Property(x => x.ConcurrencyStamp).HasColumnName("USUA_CONCURRENCY_TEMP");

            builder.Property(x => x.AccessFailedCount).HasColumnName("USUA_ACCESS_FAILED_COUNT");

            builder.Property(x => x.DataAlteracao).HasColumnName("USUA_DATA_ALTERACAO");

            builder.Property(x => x.DataCriacao).HasColumnName("USUA_DATA_CRIACAO");

            builder.Property(x => x.DataNascimento).HasColumnName("USUA_DATA_NASCIMENTO");

            builder.Property(x => x.Email).HasColumnName("USUA_EMAIL");

            builder.Property(x => x.EmailConfirmed).HasColumnName("USUA_EMAIL_CONFIRMED");

            builder.Property(x => x.Genero).HasColumnName("USUA_GENERO").IsRequired(false);

            builder.Property(x => x.LockoutEnabled).HasColumnName("USUA_LOCKOUT_ENABLED");
                //.IsRequired(false);

            builder.Property(x => x.LockoutEnd).HasColumnName("USUA_LOCKOUT_END").IsRequired(false);

            builder.Property(x => x.Nome).HasColumnName("USUA_NOME");

            builder.Property(x => x.NormalizedEmail).HasColumnName("USUA_NORMALIZED_EMAIL");

            builder.Property(x => x.NormalizedUserName).HasColumnName("USUA_NORMALIZED_NAME");

            builder.Property(x => x.PasswordHash).HasColumnName("USUA_PASSWORD_HASH");

            builder.Property(x => x.PhoneNumber).HasColumnName("USUA_PHONUMBER").IsRequired(false);

            builder.Property(x => x.PhoneNumberConfirmed).HasColumnName("USUA_PHONENUMBER_CONFIRMED");

            builder.Property(x => x.SecurityStamp).HasColumnName("USUA_SECURITY_TEMP");

            builder.Property(x => x.TwoFactorEnabled).HasColumnName("USUA_TWOFACTOR_ENABELD");

            builder.Property(x => x.UserName).HasColumnName("USUA_USERNAME");

            //builder.Ignore(x => x.Pessoa)
            builder.Ignore(x => x.Password);
            builder.Ignore(x => x.Pessoa);
            builder.Ignore(x => x.ServicoCriacao);
            builder.Ignore(x => x.ServicoResponsavel);
            builder.Ignore(x => x.FuncoesUsuarios);
        }
    }
}
