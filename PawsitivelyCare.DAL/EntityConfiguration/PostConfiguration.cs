using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsitivelyCare.DAL.Entities;

namespace PawsitivelyCare.DAL.EntityConfiguration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.Type).IsRequired().HasConversion<int>();
            builder.Property(x => x.PostCategoryId).IsRequired();
            builder.Property(x => x.CreatorId).IsRequired();

            builder.HasMany(x => x.Comments)
               .WithOne(w => w.Post)
               .HasForeignKey(x => x.PostId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Posts");
        }
    }
}
