using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

namespace Shoply.Infrastructure.Persistence.EFCore.Mapping.Module.Registration;

public class BrandMap : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasOne(x => x.CreationUser).WithMany(x => x.ListCreationUserBrand).HasForeignKey(x => x.CreationUserId).HasConstraintName("fkey_marca_id_usuario_criacao").OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.ChangeUser).WithMany(x => x.ListChangeUserBrand).HasForeignKey(x => x.ChangeUserId).HasConstraintName("fkey_marca_id_usuario_alteracao").OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("marca");

        builder.HasKey(x => x.Id).HasName("pk_marca");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.CreationDate).HasColumnName("data_criacao");
        builder.Property(x => x.CreationDate).IsRequired();

        builder.Property(x => x.CreationUserId).HasColumnName("id_usuario_criacao");
        builder.Property(x => x.CreationUserId).IsRequired();

        builder.Property(x => x.ChangeDate).HasColumnName("data_alteracao");

        builder.Property(x => x.ChangeUserId).HasColumnName("id_usuario_alteracao");

        builder.Property(x => x.Code).HasColumnName("codigo");
        builder.Property(x => x.Code).HasMaxLength(6);
        builder.Property(x => x.Code).IsRequired();

        builder.Property(x => x.Description).HasColumnName("descricao");
        builder.Property(x => x.Description).HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired();
    }
}