using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserStore.DataAccess.Entities;

namespace UserStore.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .HasMany(u => u.Groups)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.Password)
                .IsRequired();

            builder.Property(u => u.Token);
        }
    }
}
