using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

namespace Shoply.Infrastructure.Persistence.EFCore.Mapping.Module.Registration;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(x => x.CreationUser).WithMany(x => x.ListCreationUserUser).HasForeignKey(x => x.CreationUserId).HasConstraintName("fkey_usuario_id_usuario_criacao").OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.ChangeUser).WithMany(x => x.ListChangeUserUser).HasForeignKey(x => x.ChangeUserId).HasConstraintName("fkey_usuario_id_usuario_alteracao").OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("usuario");

        builder.HasKey(x => x.Id).HasName("pk_usuario");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.CreationDate).HasColumnName("data_criacao");
        builder.Property(x => x.CreationDate).IsRequired();

        builder.Property(x => x.CreationUserId).HasColumnName("id_usuario_criacao");

        builder.Property(x => x.ChangeDate).HasColumnName("data_alteracao");

        builder.Property(x => x.ChangeUserId).HasColumnName("id_usuario_alteracao");

        builder.Property(x => x.Name).HasColumnName("nome");
        builder.Property(x => x.Name).HasMaxLength(150);
        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.Email).HasMaxLength(150);
        builder.Property(x => x.Email).IsRequired();

        builder.Property(x => x.Password).HasColumnName("senha");
        builder.Property(x => x.Password).HasMaxLength(150);
        builder.Property(x => x.Password).IsRequired();

        builder.Property(x => x.Language).HasColumnName("idioma");
        builder.Property(x => x.Language).IsRequired();

        builder.Property(x => x.RefreshToken).HasColumnName("refresh_token");
        builder.Property(x => x.RefreshToken).HasMaxLength(100);

        builder.Property(x => x.LoginKey).HasColumnName("chave_login");

        builder.Property(x => x.PasswordRecoveryCode).HasColumnName("codigo_recuperacao_senha");
        builder.Property(x => x.PasswordRecoveryCode).HasMaxLength(6);

        builder.HasData(new User("Usuario Padrão", "default@shoply.com", "$2a$11$252h2vGrxOa1D/ZO.SCreeO3NWC4cSzKJlF.dyzxIQlbJ24ooULO2", EnumLanguage.Portuguese, default, default, default).SetInternalData(1, new DateTime(2025, 01, 01, 0, 0, 0), default, default, default, default, default));
    }
}