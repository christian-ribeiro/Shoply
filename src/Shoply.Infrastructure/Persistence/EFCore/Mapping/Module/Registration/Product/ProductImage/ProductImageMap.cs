using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

namespace Shoply.Infrastructure.Persistence.EFCore.Mapping.Module.Registration;

public class ProductImageMap : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasOne(x => x.CreationUser).WithMany(x => x.ListCreationUserProductImage).HasForeignKey(x => x.CreationUserId).HasConstraintName("fkey_imagem_produto_id_usuario_criacao").OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Product).WithMany(x => x.ListProductImage).HasForeignKey(x => x.ProductId).HasConstraintName("fkey_imagem_produto_id_produto").OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("imagem_produto");

        builder.HasKey(x => x.Id).HasName("pk_imagem_produto");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.CreationDate).HasColumnName("data_criacao");
        builder.Property(x => x.CreationDate).IsRequired();

        builder.Property(x => x.CreationUserId).HasColumnName("id_usuario_criacao");
        builder.Property(x => x.CreationUserId).IsRequired();

        builder.Property(x => x.FileName).HasColumnName("nome_arquivo");
        builder.Property(x => x.FileName).IsRequired();

        builder.Property(x => x.FileLength).HasColumnName("tamanho_arquivo");
        builder.Property(x => x.FileLength).IsRequired();

        builder.Property(x => x.ImageUrl).HasColumnName("link_imagem");
        builder.Property(x => x.ImageUrl).IsRequired();

        builder.Property(x => x.ProductId).HasColumnName("id_produto");
        builder.Property(x => x.ProductId).IsRequired();

    }
}