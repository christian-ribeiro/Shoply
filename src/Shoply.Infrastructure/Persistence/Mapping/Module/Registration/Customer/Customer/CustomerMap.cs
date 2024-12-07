using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;

namespace Shoply.Infrastructure.Persistence.Mapping.Module.Registration;

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasOne(x => x.CreationUser).WithMany(x => x.ListCreationUserCustomer).HasForeignKey(x => x.CreationUserId).HasConstraintName("fkey_cliente_id_usuario_criacao");
        builder.HasOne(x => x.ChangeUser).WithMany(x => x.ListChangeUserCustomer).HasForeignKey(x => x.ChangeUserId).HasConstraintName("fkey_cliente_id_usuario_alteracao");

        builder.ToTable("cliente");

        builder.HasKey(x => x.Id).HasName("pk_cliente");

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

        builder.Property(x => x.FirstName).HasColumnName("primeiro_nome");
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.FirstName).IsRequired();

        builder.Property(x => x.LastName).HasColumnName("sobrenome");
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired();

        builder.Property(x => x.BirthDate).HasColumnName("data_nascimento");

        builder.Property(x => x.Document).HasColumnName("documento");
        builder.Property(x => x.Document).HasMaxLength(14);
        builder.Property(x => x.Document).IsRequired();

        builder.Property(x => x.PersonType).HasColumnName("tipo_pessoa");
        builder.Property(x => x.PersonType).IsRequired();
    }
}