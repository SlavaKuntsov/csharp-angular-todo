using Microsoft.EntityFrameworkCore;
using UserStore.DataAccess.Configurations;
using UserStore.DataAccess.Entities;

namespace UserStore.DataAccess
{
    public class UserStoreDbContext : DbContext
    {
        public UserStoreDbContext(DbContextOptions<UserStoreDbContext> options) : base(options)
        { }
        // создание таблиц
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<ItemEntity> Items { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}