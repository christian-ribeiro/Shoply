using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;

namespace Shoply.Infrastructure.Persistence.Mapping.Module.Registration;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(x => x.CreationUser).WithMany(x => x.ListCreationUserUser).HasForeignKey(x => x.CreationUserId).HasConstraintName("fkey_usuario_id_usuario_criacao");
        builder.HasOne(x => x.ChangeUser).WithMany(x => x.ListChangeUserUser).HasForeignKey(x => x.ChangeUserId).HasConstraintName("fkey_usuario_id_usuario_alteracao");

        builder.ToTable("usuario");

        builder.HasKey(x => x.Id).HasName("id");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.CreationDate).HasColumnName("data_criacao");

        builder.Property(x => x.CreationUserId).HasColumnName("id_usuario_criacao");

        builder.Property(x => x.ChangeDate).HasColumnName("data_alteracao");

        builder.Property(x => x.ChangeUserId).HasColumnName("id_usuario_alteracao");

        builder.Property(x => x.Name).HasColumnName("nome");
        builder.Property(x => x.Name).HasMaxLength(150);
        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Password).HasColumnName("senha");
        builder.Property(x => x.Password).HasMaxLength(150);
        builder.Property(x => x.Password).IsRequired();

        builder.Property(x => x.Email).HasColumnName("email");
        builder.Property(x => x.Email).HasMaxLength(150);
        builder.Property(x => x.Email).IsRequired();

        builder.Property(x => x.Language).HasColumnName("idioma");
        builder.Property(x => x.Language).IsRequired();

        builder.Property(x => x.RefreshToken).HasColumnName("refresh_token");
        builder.Property(x => x.RefreshToken).HasMaxLength(100);

        builder.Property(x => x.LoginKey).HasColumnName("chave_login");

        builder.Property(x => x.PasswordRecoveryCode).HasColumnName("codigo_recuperacao_senha");
        builder.Property(x => x.PasswordRecoveryCode).HasMaxLength(6);
    }
}