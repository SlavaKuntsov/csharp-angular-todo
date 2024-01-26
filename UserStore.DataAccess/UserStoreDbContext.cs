using Microsoft.EntityFrameworkCore;
using UserStore.DataAccess.Entities;

namespace UserStore.DataAccess
{
    public class UserStoreDbContext : DbContext
    {
        // создание таблиц
        public DbSet<UserEntity> Users { get; set; }

        public UserStoreDbContext(DbContextOptions<UserStoreDbContext> options) : base(options)
        {

        }
    }
}