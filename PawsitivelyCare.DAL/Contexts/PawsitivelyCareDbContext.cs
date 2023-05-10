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
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PostCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.Entity<User>()
                .HasMany(f => f.Chats)
                .WithMany(g => g.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "ChatUser",
                        j => j.HasOne<Chat>().WithMany().OnDelete(DeleteBehavior.Cascade),
                        j => j.HasOne<User>().WithMany().OnDelete(DeleteBehavior.NoAction));

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(r => r.Users);

            base.OnModelCreating(modelBuilder);
        }
    }
}
