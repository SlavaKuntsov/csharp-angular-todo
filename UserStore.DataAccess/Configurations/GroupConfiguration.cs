using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserStore.DataAccess.Entities;

namespace UserStore.DataAccess.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<GroupEntity>
    {
        public void Configure(EntityTypeBuilder<GroupEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasOne(g => g.User)
                .WithMany(u => u.Groups)
                .HasForeignKey(g => g.UserId);

            builder
                .HasMany(g => g.Items)
                .WithOne(i => i.Group)
                .HasForeignKey(i => i.GroupId);

            builder.Property(g => g.Title)
                .IsRequired();
        }
    }
}
