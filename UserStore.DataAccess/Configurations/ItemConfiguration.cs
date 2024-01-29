using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserStore.DataAccess.Entities;

namespace UserStore.DataAccess.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<ItemEntity>
    {
        public void Configure(EntityTypeBuilder<ItemEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasOne(i => i.Group)
                .WithMany(g => g.Items)
                .HasForeignKey(i => i.GroupId);

            builder.Property(u => u.GroupId)
                .IsRequired();

            builder.Property(i => i.Title)
                .IsRequired();

            builder.Property(i => i.Description)
                .IsRequired();

            builder.Property(i => i.Status)
                .IsRequired();
        }
    }
}
