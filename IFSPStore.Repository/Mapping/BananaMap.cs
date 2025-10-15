using IFSPStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IFSPStore.Repository.Mapping
{
    public class BananaMap : IEntityTypeConfiguration<Banana>
    {
        public void Configure(EntityTypeBuilder<Banana> builder)
        {
            builder.ToTable("Bananas");
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
