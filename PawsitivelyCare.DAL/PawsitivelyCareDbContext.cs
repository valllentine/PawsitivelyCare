using Microsoft.EntityFrameworkCore;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.EntityConfiguration;

namespace PawsitivelyCare.DAL
{
    public class PawsitivelyCareDbContext : DbContext
    {
        public PawsitivelyCareDbContext(DbContextOptions<PawsitivelyCareDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostType> PostTypes { get; set; }

        // метод, который конфигурирует модель данных
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChatConfiguration());

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoles",
                    ur => ur.HasOne<Chat>().WithMany().HasForeignKey("RoleId"),
                    ur => ur.HasOne<User>().WithMany().HasForeignKey("UserId")
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
