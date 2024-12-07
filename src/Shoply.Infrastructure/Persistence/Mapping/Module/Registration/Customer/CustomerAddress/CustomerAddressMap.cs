using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;

namespace Shoply.Infrastructure.Persistence.Mapping.Module.Registration;

public class CustomerAddressMap : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.HasOne(x => x.CreationUser).WithMany(x => x.ListCreationUserCustomerAddress).HasForeignKey(x => x.CreationUserId).HasConstraintName("fkey_endereco_cliente_id_usuario_criacao").OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.ChangeUser).WithMany(x => x.ListChangeUserCustomerAddress).HasForeignKey(x => x.ChangeUserId).HasConstraintName("fkey_endereco_cliente_id_usuario_alteracao").OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Customer).WithMany(x => x.ListCustomerAddress).HasForeignKey(x => x.CustomerId).HasConstraintName("fkey_endereco_cliente_id_cliente").OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("endereco_cliente");

        builder.HasKey(x => x.Id).HasName("pk_endereco_cliente");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.CreationDate).HasColumnName("data_criacao");
        builder.Property(x => x.CreationDate).IsRequired();

        builder.Property(x => x.CreationUserId).HasColumnName("id_usuario_criacao");
        builder.Property(x => x.CreationUserId).IsRequired();

        builder.Property(x => x.ChangeDate).HasColumnName("data_alteracao");

        builder.Property(x => x.ChangeUserId).HasColumnName("id_usuario_alteracao");

        builder.Property(x => x.CustomerId).HasColumnName("id_cliente");
        builder.Property(x => x.CustomerId).IsRequired();

        builder.Property(x => x.AddressType).HasColumnName("tipo_endereco");
        builder.Property(x => x.AddressType).IsRequired();

        builder.Property(x => x.PublicPlace).HasColumnName("logradouro");
        builder.Property(x => x.PublicPlace).HasMaxLength(100);
        builder.Property(x => x.PublicPlace).IsRequired();

        builder.Property(x => x.Number).HasColumnName("numero");
        builder.Property(x => x.Number).HasMaxLength(10);
        builder.Property(x => x.Number).IsRequired();

        builder.Property(x => x.Complement).HasColumnName("complemento");
        builder.Property(x => x.Complement).HasMaxLength(50);

        builder.Property(x => x.Neighborhood).HasColumnName("bairro");
        builder.Property(x => x.Neighborhood).HasMaxLength(50);
        builder.Property(x => x.Neighborhood).IsRequired();

        builder.Property(x => x.PostalCode).HasColumnName("cep");
        builder.Property(x => x.PostalCode).HasMaxLength(8);
        builder.Property(x => x.PostalCode).IsRequired();

        builder.Property(x => x.Reference).HasColumnName("referencia");
        builder.Property(x => x.Reference).HasMaxLength(200);

        builder.Property(x => x.Observation).HasColumnName("observacao");
        builder.Property(x => x.Observation).HasMaxLength(400);
    }
}