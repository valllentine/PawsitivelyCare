using Microsoft.EntityFrameworkCore;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.EntityConfiguration;

namespace PawsitivelyCare.DAL.Contexts
{
    public class PawsitivelyCareDbContext : DbContext
    {
        public PawsitivelyCareDbContext(DbContextOptions<PawsitivelyCareDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostType> PostTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(r => r.Users);

            base.OnModelCreating(modelBuilder);
        }
    }
}
